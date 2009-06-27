using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

using W3b.Wizards;

using Anolis.Core;
using Anolis.Installer.Pages;
using Anolis.Core.Packages;
using System.Text;

namespace Anolis.Installer {
	
	public static class Program {
		
		[STAThread]
		public static void Main(String[] args) {
			
			try {
				
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				
				if( args.Length > 0 ) {
					
					if( args.IndexOf("/pause") >= 0 ) {
						
						MessageBox.Show("Paused", "Anolis Installer", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
					}
					
					String filename = args[0];
					if( File.Exists( filename ) ) {
						
						String ext = Path.GetExtension( filename ).ToUpperInvariant();
						if(ext == ".ANOP") { // package archive
						
						} else if( ext == ".XML") { // package definition
							
						}
						
					}
					
				}
				
				// preload resources
				System.Drawing.Image
					image = InstallerResources.GetImage("Background");
					image = InstallerResources.GetImage("Banner");

					InstallerResources.CurrentLanguageChanged += new EventHandler(InstallerResources_CurrentLanguageChanged);
				
				ProgramMode = ProgramMode.None;
				
				// Set up the wizard
				
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
				WizardForm.Title          = "Anolis Package Installer";
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
			
			WizardForm.LoadPage( PageAWelcome );
			WizardForm.Run();
			
		}
		
		private static void InstallerResources_CurrentLanguageChanged(object sender, EventArgs e) {
			
		}
		
		private static void wiz_CancelClicked(object sender, EventArgs e) {
			
			String message = "Are you sure you want to cancel installation?";
			
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
		
		public static WizardStyle WizStyle = WizardStyle.PlatformDefault;
		
		public enum WizardStyle {
			PlatformDefault,
			Wizard97,
			Aero
		}
		
		public static String JumpToPackagePath;
		
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
		
	}
	
	internal static class ToolsInfo {
		
		public static Boolean ProgramGroup         { get; set; }
		public static Boolean ProgramGroupEveryone { get; set; }
		
		public static String  DestinationDirectory { get; set; }
		
		public static readonly Uri ToolsInfoUri = new Uri("http://anol.is/installer/toolsInfo.txt");
		
	}
	
	internal static class PackageInfo {
		
		public static PackageSource  Source     { get; set; }
		public static String         SourcePath { get; set; }
		
		public static PackageArchive Archive    { get; set; }
		
		public static Package        Package    { get; set; }
		
		public static Boolean        SystemRestore { get; set; }
		public static String         BackupPath    { get; set; }
		
		public static Boolean        I386Install   { get; set; }
		public static DirectoryInfo  I386Directory { get; set; }
		
		public static Boolean        RequiresRestart { get; set; }
		
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
