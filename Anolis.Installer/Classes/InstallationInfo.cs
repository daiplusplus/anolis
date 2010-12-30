using System;
using System.IO;

using Anolis.Packages;

using W3b.Wizards;

using Env = Anolis.Core.Utility.Environment;
using Symbols = System.Collections.Generic.Dictionary<System.String, System.Double>;

namespace Anolis.Installer {
	
	/// <summary>Meta-information about the installation</summary>
	internal static class InstallationInfo {
		
		internal static ProgramMode ProgramMode { get; set; }
		
		public static String InstallerTitle {
			get {
				return InstallerResources.IsCustomized ? InstallerResources.CustomizedSettings.InstallerFullName : "Anolis Package Installer";
			}
		}
		
#region Wizard Style
		
		public static WizardStyle WizStyle { get; set; }
		
		public enum WizardStyle {
			PlatformDefault,
			Wizard97,
			Aero
		}
		
		public static IWizardForm CreateWizard() {
			
			switch(WizStyle) {
				case WizardStyle.Aero:
					return new W3b.Wizards.WindowsForms.Aero.AeroWizardForm();
				case WizardStyle.Wizard97:
					return new W3b.Wizards.WindowsForms.Wizard97.Wizard97WizardForm();
				case WizardStyle.PlatformDefault:
				default:
					return WizardFactory.Create();
			}
			
		}
#endregion
		
		public static FileInfo UninstallPackage { get; set; }
		
		//////////////////////////////////////
		
		public static Boolean FailedCondition { get; set; }
		
		public static Boolean EvaluateInstallerCondition() {
			
			if( !InstallerResources.IsCustomized ) return true;
			
			String exprStr = InstallerResources.CustomizedSettings.InstallerCondition;
			if( String.IsNullOrEmpty( exprStr ) ) return true;
			
			Expression expr = new Expression( exprStr );
			return expr.Evaluate( GetSymbols() ) == 1;
		}
		
		private static Symbols GetSymbols() {
			
			return new Symbols() {
				
				{"osversion"   , Env.OSVersion.Version.Major + ( (Double)Env.OSVersion.Version.Minor ) / 10 },
				{"servicepack" , Env.ServicePack },
				{"architecture", Env.IsX64 ? 64 : 32 }
			};
		}
		
		//////////////////////////////////////
		
		public static Boolean InstallationAborted { get; set; }
		public static String  FeedbackErrorReportPath { get; set; }
		
		public static void WriteException(Exception ex) {
			
			if( FeedbackErrorReportPath == null ) {
				FeedbackErrorReportPath = Path.GetFullPath("Anolis.Installer.Error.log");
			}
			
			using(FileStream fs = new FileStream(FeedbackErrorReportPath, FileMode.Append, FileAccess.Write))
			using(StreamWriter wtr = new StreamWriter(fs)) {
				
				wtr.WriteLine( DateTime.Now.ToString("s") );
				
				while( ex != null ) {
					
					wtr.WriteLine( ex.Message );
					wtr.WriteLine( ex.StackTrace );
					
					ex = ex.InnerException;
				}
				
			}
			
		}
		
		//////////////////////////////////////
		
		public static Boolean? UseSelector { get; set; }
		
		public static Boolean FeedbackSend    { get; set; }
		public static String  FeedbackMessage { get; set; }
		public static Boolean FeedbackCanSend {
			get {
				if( PackageInfo.Package             == null ) return false;
				if( PackageInfo.Package.FeedbackUri == null ) return false;
				return true;
			}
		}
		
#region Tools
		
		public static readonly Uri ToolsInfoUri = new Uri("http://anol.is/tools/toolsInfo.txt");
		
		public static DirectoryInfo ToolsDestination { get; set; }
		
		public static StartMenu ToolsStartMenu { get; set; }
		
		internal enum StartMenu {
			None,
			Myself,
			AllUsers
		}
		
#endregion
		
	}
	
	
	internal static class PackageInfo {
		
		//////////////////////////////////////
		// Common
		
		public static PackageSource  Source     { get; set; }
		public static String         SourcePath { get; set; }
		public static PackageArchive Archive    { get; set; }
		
		public static Package        Package    { get; set; }
		
		public static Boolean        RequiresRestart { get; set; }
		
		public static Boolean        IgnoreCondition { get; set; }
		
		public static Boolean        LiteMode        { get; set; }
		
		//////////////////////////////////////
		// Regular-specific
		
		public static Boolean        SystemRestore { get; set; }
		public static String         BackupPath    { get; set; }
		
		//////////////////////////////////////
		// I386-specific
		
		public static Boolean        I386Install   { get; set; }
		public static DirectoryInfo  I386Directory { get; set; }
		
	}
	
	internal enum PackageSource {
		Archive,
		Embedded,
		File
	}
	
	internal enum ProgramMode {
		None             = 0,
		InstallPackage,
		UninstallPackage,
		InstallTools
	}
}
