using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Packages;
using Anolis.Installer.Pages;

using W3b.Wizards;


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
				
				InstallerResources.CurrentLanguageChanged += new EventHandler(InstallerResources_CurrentLanguageChanged);
				
				InstallationInfo.ProgramMode = ProgramMode.None;
				
#region Initial PackageInfo Options
				
				// set this here because there's nowhere else to put it
				// HACK: Devise a shortlist of known languages that have this problem
				// also, does this affect Vista?
				if( Environment.OSVersion.Version.Major == 5 ) {
					
					PackageInfo.LiteMode = System.Globalization.CultureInfo.InstalledUICulture.DisplayName.IndexOf("English", StringComparison.OrdinalIgnoreCase) == -1;
				}
				
				if( InstallerResources.IsCustomized ) {
					
					PackageInfo.IgnoreCondition = InstallerResources.CustomizedSettings.DisablePackageCheck;
				}
				
				PackageInfo.SystemRestore = true;
				
				InstallationInfo.FeedbackSend = true;
				
#endregion
				
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
						InstallationInfo.ProgramMode = ProgramMode.UninstallPackage;
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
				
				// create the pages
				PageAWelcome         = new WelcomePage();
				PageBMainAction      = new MainActionPage();
				
				PageCASelectPackage  = new SelectPackagePage();
				PageCBExtracting     = new ExtractingPage();
				PageCCUpdatePackage  = new UpdatePackagePage();
				PageCDReleaseNotes   = new ReleaseNotesPage();
				PageCE1Selector      = new SelectorPage();
				PageCE2ModifyPackage = new ModifyPackagePage();
				PageCFInstallOpts    = new InstallationOptionsPage();
				PageCFInstallOptForm = new InstallationOptionsForm();
				PageCGInstalling     = new InstallingPage();
				
				PageDADestination    = new DestinationPage();
				PageDBDownloading    = new DownloadingPage();
				
				PageEASelectBackup   = new SelectBackupPage();
				
				PageFFinished        = new FinishedPage();
				
				WizardForm           = InstallationInfo.CreateWizard();
				
				WizardForm.CancelClicked += new EventHandler(wiz_CancelClicked);
				WizardForm.HasHelp        = false;
				WizardForm.Title          = InstallationInfo.InstallerTitle;
				WizardForm.Icon           = InstallerResources.GetIcon("Package");
				
				WizardForm.NextText   = InstallerResources.GetString("Wiz_Next");
				WizardForm.BackText   = InstallerResources.GetString("Wiz_Prev");
				WizardForm.CancelText = InstallerResources.GetString("Wiz_Cancel");
				
				InstallerResources.ForceLocalize();
				
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
			
			switch(InstallationInfo.ProgramMode) {
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
		
		private static void InstallerResources_CurrentLanguageChanged(object sender, EventArgs e) {
			
			// you need to wrap the RightToLeft set in SuspendLayout / ResumeLayout to prevent AccessViolationExceptions from occurring
			// I only get the exception when the program is being run standalone, not attached to the debugger
			
			RightToLeft orig = WizardForm.RightToLeft;
			RightToLeft newg = InstallerResources.CurrentLanguage.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
			
			if( orig == newg ) return;
			
			Form wizForm = WizardForm as Form;
			if( wizForm != null ) wizForm.SuspendLayout();
			
			WizardForm.RightToLeft = newg;
			
			if( wizForm != null ) wizForm.ResumeLayout();
		}
		
		private static void wiz_CancelClicked(object sender, EventArgs e) {
			
			String message = InstallerResources.GetString("Wiz_CancelConfirm");
			
			if( MessageBox.Show(message, "Anolis Installer", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes ) {
				
				Application.Exit();
			}
			
		}
		
		internal static IWizardForm             WizardForm           { get; private set; }
		
		internal static WelcomePage             PageAWelcome         { get; private set; }
		internal static MainActionPage          PageBMainAction      { get; private set; }
		
		internal static SelectPackagePage       PageCASelectPackage  { get; private set; }
		internal static ExtractingPage          PageCBExtracting     { get; private set; }
		internal static UpdatePackagePage       PageCCUpdatePackage  { get; private set; }
		internal static ReleaseNotesPage        PageCDReleaseNotes   { get; private set; }
		internal static SelectorPage            PageCE1Selector      { get; private set; }
		internal static ModifyPackagePage       PageCE2ModifyPackage { get; private set; }
		internal static InstallationOptionsPage PageCFInstallOpts    { get; private set; }
		internal static InstallationOptionsForm PageCFInstallOptForm { get; private set; }
		internal static InstallingPage          PageCGInstalling     { get; private set; }
		
		internal static DestinationPage         PageDADestination    { get; private set; }
		internal static DownloadingPage         PageDBDownloading    { get; private set; }
		
		internal static SelectBackupPage        PageEASelectBackup   { get; private set; }
		
		internal static FinishedPage            PageFFinished        { get; private set; }
		
	}
	
}
