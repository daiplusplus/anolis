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
			
			IWizardForm wiz = null;
			
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
							
						} else if( ext == ".ANUP" ) { // undo/uninstallation
							
						}
						
					}
					
				}
				
				ProgramMode = ProgramMode.None;
				
				// Set up the wizard
				
				// create the pages
				PageAWelcome        = new WelcomePage();
				PageBMainAction     = new MainActionPage();
				
				PageCASelectPackage = new SelectPackagePage();
				PageCBExtracting    = new ExtractingPage();
				PageCCUpdatePackage = new UpdatePackagePage();
				PageCDModifyPackage = new ModifyPackagePage();
				PageCEInstallOpts   = new InstallationOptionsPage();
				PageCFInstalling    = new InstallingPage();
				
				PageDADestination   = new DestinationPage();
				PageDBDownloading   = new DownloadingPage();
				
				PageEASelectBackup  = new SelectBackupPage();
				
				PageFFinished       = new FinishedPage();
				
				wiz                 = 
//				                      new W3b.Wizards.WindowsForms.Aero.AeroWizardForm();
//				                      new W3b.Wizards.WindowsForms.Wizard97.Wizard97WizardForm();
				                      WizardFactory.Create();
				
				wiz.CancelClicked  += new EventHandler(wiz_CancelClicked);
				wiz.HasHelp         = false;
				wiz.Title           = "Anolis Package Installer";
				(wiz as Form).Icon  = InstallerResources.GetIcon("Package");
				wiz.LoadPage( PageAWelcome );
				
				// preload resources
				System.Drawing.Image
					image = InstallerResources.GetImage("Background");
					image = InstallerResources.GetImage("Banner");
				
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
			
			
			wiz.Run();
			
		}
		
		private static void wiz_CancelClicked(object sender, EventArgs e) {
			
			String message = "Are you sure you want to cancel installation?";
			
			if( MessageBox.Show(message, "Anolis Installer", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes ) {
				
				Application.Exit();
			}
			
		}
		
		internal static WelcomePage             PageAWelcome        { get; private set; }
		internal static MainActionPage          PageBMainAction     { get; private set; }
		
		internal static SelectPackagePage       PageCASelectPackage { get; private set; }
		internal static ExtractingPage          PageCBExtracting    { get; private set; }
		internal static UpdatePackagePage       PageCCUpdatePackage { get; private set; }
		internal static ModifyPackagePage       PageCDModifyPackage { get; private set; }
		internal static InstallationOptionsPage PageCEInstallOpts   { get; private set; }
		internal static InstallingPage          PageCFInstalling    { get; private set; }
		
		internal static DestinationPage         PageDADestination   { get; private set; }
		internal static DownloadingPage         PageDBDownloading   { get; private set; }
		
		internal static SelectBackupPage        PageEASelectBackup  { get; private set; }
		
		internal static FinishedPage            PageFFinished       { get; private set; }
		
		
		internal static ProgramMode             ProgramMode         { get; set; }
	}
	
	internal static class ToolsInfo {
		
		public static Boolean ProgramGroup         { get; set; }
		public static Boolean ProgramGroupEveryone { get; set; }
		
		public static String  DestinationDirectory { get; set; }
		
	}
	
	internal static class PackageInfo {
		
		public static PackageSource  Source     { get; set; }
		public static String         SourcePath { get; set; }
		
		public static PackageArchive Archive    { get; set; }
		
		public static Package        Package    { get; set; }
		
		public static Boolean        SystemRestore { get; set; }
		
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
