using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

using W3b.Wizards;

using Anolis.Installer.Pages;
using Anolis.Core.Packages;
using System.Text;

namespace Anolis.Installer {
	
	public static class Program {
		
		[STAThread]
		public static void Main() {
			
			IWizardForm wiz = null;
			
			try {
				
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				
				ProgramMode = ProgramMode.None;
				
				// Set up the wizard
				
				// create the pages
				PageAWelcome        = new WelcomePage();
				PageBMainAction     = new MainActionPage();
				
				PageICSelectPackage = new SelectPackagePage();
				PageIDExtracting    = new ExtractingPage();
				PageIEModifyPackage = new ModifyPackagePage();
				PageIFInstallationOptions = new InstallationOptionsPage();
				PageIGInstalling    = new InstallingPage();
				
				PageDCDestination   = new DestinationPage();
				PageDDDownloading   = new DownloadingPage();
				
				PageUCSelectBackup  = new SelectBackupPage();
				
				PageZFinished       = new FinishedPage();
				
				wiz = new W3b.Wizards.Wizard97.Wizard97WizardForm(); // WizardFactory.Create();
				wiz.CancelClicked  += new EventHandler(wiz_CancelClicked);
				wiz.HasHelp         = false;
				wiz.Title           = "Anolis Package Installer";
				wiz.LoadPage( PageAWelcome );
				
				// preload resources
				System.Drawing.Image image = Anolis.Installer.Properties.Resources.Background;
				image = Anolis.Installer.Properties.Resources.Banner;
				
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
		
		internal static SelectPackagePage       PageICSelectPackage { get; private set; }
		internal static ExtractingPage          PageIDExtracting    { get; private set; }
		internal static ModifyPackagePage       PageIEModifyPackage { get; private set; }
		internal static InstallationOptionsPage PageIFInstallationOptions { get; private set; }
		internal static InstallingPage          PageIGInstalling    { get; private set; }
		
		internal static DestinationPage         PageDCDestination   { get; private set; }
		internal static DownloadingPage         PageDDDownloading   { get; private set; }
		
		internal static SelectBackupPage        PageUCSelectBackup  { get; private set; }
		
		internal static FinishedPage            PageZFinished       { get; private set; }
		
		
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
