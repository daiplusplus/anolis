using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Core.Packages;
using Anolis.Installer.Pages;

using W3b.Wizards;

using Env = Anolis.Core.Utility.Environment;
using Symbols = System.Collections.Generic.Dictionary<System.String, System.Double>;

namespace Anolis.Installer {
	
	public static class Program {
		
		private static String GetUninstallation(String[] args) {
			
			const String needle = "/uninstall:";
			
			String relativeFilename = null;
			
			foreach(String arg in args) {
				
				if( arg.StartsWith(needle) ) {
					
					relativeFilename = arg.Substring(needle.Length);
				}
				
			}
			
			if( relativeFilename == null ) return null;
			
			String thisDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
			thisDirectory = Path.GetDirectoryName( thisDirectory );
			
			return Path.Combine( thisDirectory, relativeFilename );
		}
		
		[STAThread]
		public static void Main(String[] args) {
			
			try {
				
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				
				ProgramMode = ProgramMode.None;
				
				if( InstallerResources.IsCustomized ) {
					
					PackageInfo.IgnoreCondition = InstallerResources.CustomizedSettings.DisablePackageCheck;
				}
				
				if( args.Length > 0 ) {
					
					//////////////////////////////////////////
					// Pause
					if( args.IndexOf("/pause") > -1 ) MessageBox.Show("Paused", "Anolis Installer", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
					
					//////////////////////////////////////////
					// Package Condition
					if( args.IndexOf("/ignoreCondition") > -1 ) PackageInfo.IgnoreCondition = true;
					
					//////////////////////////////////////////
					// Uninstall Mode
					String uninstall = GetUninstallation( args );
					if( uninstall != null ) {
						InstallationInfo.UninstallPackage = new FileInfo( uninstall );
						ProgramMode = ProgramMode.UninstallPackage;
					}
					
					//////////////////////////////////////////
					// Jump-to-Package
//
//					String filename = args[0];
//					if( File.Exists( filename ) ) {
//						
//						String ext = Path.GetExtension( filename ).ToUpperInvariant();
//						if(ext == ".ANOP") { // package archive
//							
//						} else if( ext == ".XML") { // package definition
//							
//						}
//						
//					}
				}
				
				
				
				// preload resources
				System.Drawing.Image
					image = InstallerResources.GetImage("Background");
					image = InstallerResources.GetImage("Banner");
				
				// Set up the wizard
				
				String title = InstallerResources.IsCustomized ? InstallerResources.CustomizedSettings.InstallerFullName : "Anolis Package Installer";
				
				// create the pages
				PageAWelcome        = new WelcomePage();
				PageBMainAction     = new MainActionPage();
				
				PageCASelectPackage = new SelectPackagePage();
				PageCBExtracting    = new ExtractingPage();
				PageCCUpdatePackage = new UpdatePackagePage();
				PageCDReleaseNotes  = new ReleaseNotesPage();
				PageCEModifyPackage = new ModifyPackagePage();
				PageCFInstallOpts   = new InstallationOptionsPage();
				PageCGInstalling    = new InstallingPage();
				
				PageDADestination   = new DestinationPage();
				PageDBDownloading   = new DownloadingPage();
				
				PageEASelectBackup  = new SelectBackupPage();
				
				PageFFinished       = new FinishedPage();
				
				WizardForm          = InstallationInfo.CreateWizard();
				
				WizardForm.CancelClicked += new EventHandler(wiz_CancelClicked);
				WizardForm.HasHelp        = false;
				WizardForm.Title          = title;
				WizardForm.Icon           = InstallerResources.GetIcon("Package");
				
				WizardForm.NextText   = InstallerResources.GetString("Wiz_Next");
				WizardForm.BackText   = InstallerResources.GetString("Wiz_Prev");
				WizardForm.CancelText = InstallerResources.GetString("Wiz_Cancel");
				
			} catch(Exception ex) {
				
				StringBuilder sb = new StringBuilder();
				while(ex != null) {
					
					sb.Append( ex.Message );
					sb.Append("\r\n");
					sb.Append( ex.StackTrace );
					
					if(ex.InnerException != null) {
						sb.Append("\r\n\r\n");
					}
					
					ex = ex.InnerException;
				}
				
				MessageBox.Show( sb.ToString(), "Anolis Installer - Initialisation Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				return;
			}
			
			switch(ProgramMode) {
				case ProgramMode.UninstallPackage:
					WizardForm.LoadPage( PageEASelectBackup );
					break;
					
				case ProgramMode.None:
				default:
					WizardForm.LoadPage( PageAWelcome );
					break;
			}
			
			WizardForm.Run();
			
			
			
			// Clean-up
			if( PackageInfo.Package != null && ( PackageInfo.Source == PackageSource.Embedded || PackageInfo.Source == PackageSource.Archive ) ) {
				
				PackageInfo.Package.DeleteFiles();
			}
			
		}
		
		private static void wiz_CancelClicked(object sender, EventArgs e) {
			
			String message = InstallerResources.GetString("Wiz_CancelConfirm");
			
			if( MessageBox.Show(message, "Anolis Installer", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes ) {
				
				Application.Exit();
			}
			
		}
		
		internal static IWizardForm             WizardForm          { get; private set; }
		
		internal static WelcomePage             PageAWelcome        { get; private set; }
		internal static MainActionPage          PageBMainAction     { get; private set; }
		
		internal static SelectPackagePage       PageCASelectPackage { get; private set; }
		internal static ExtractingPage          PageCBExtracting    { get; private set; }
		internal static UpdatePackagePage       PageCCUpdatePackage { get; private set; }
		internal static ReleaseNotesPage        PageCDReleaseNotes  { get; private set; }
		internal static ModifyPackagePage       PageCEModifyPackage { get; private set; }
		internal static InstallationOptionsPage PageCFInstallOpts   { get; private set; }
		internal static InstallingPage          PageCGInstalling    { get; private set; }
		
		internal static DestinationPage         PageDADestination   { get; private set; }
		internal static DownloadingPage         PageDBDownloading   { get; private set; }
		
		internal static SelectBackupPage        PageEASelectBackup  { get; private set; }
		
		internal static FinishedPage            PageFFinished       { get; private set; }
		
		
		internal static ProgramMode             ProgramMode         { get; set; }
	}
	
	/// <summary>Meta-information about the installation</summary>
	internal static class InstallationInfo {
		
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
		
		public static void WriteException(Exception ex) {
			
			using(FileStream fs = new FileStream("Anolis.Installer.Error.log", FileMode.Append, FileAccess.Write))
			using(StreamWriter wtr = new StreamWriter(fs)) {
				
				wtr.WriteLine( DateTime.Now.ToString("s") );
				
				while( ex != null ) {
					
					wtr.WriteLine( ex.Message );
					wtr.WriteLine( ex.StackTrace );
					
					ex = ex.InnerException;
				}
				
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
