using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

using W3b.Wizards;

using Anolis.Gui.Pages;

namespace Anolis.Gui {
	
	public static class Program {
		
		[STAThread]
		public static void Main() {
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			// Set up the wizard
			
			// create the pages
			PageAWelcome        = new WelcomePage();
			PageBMainAction     = new MainActionPage();
			
			PageICSelectPackage = new SelectPackagePage();
			PageIDExtracting    = new ExtractingPage();
			PageIEModifyPackage = new ModifyPackagePage();
			PageIFInstallationOptions = new InstallationOptionsPage();
			
			PageDCDestination = new DestinationPage();
			PageDDDownloading = new DownloadingPage();
			
			PageUCSelectBackup = new SelectBackupPage();
			
			IWizardForm wiz = WizardForm.Create();
			//wiz.Title = "Anolis Installer";
			wiz.HasHelp = false;
			wiz.CancelClicked += new EventHandler(wiz_CancelClicked);
			
			wiz.LoadPage( PageAWelcome );
			wiz.ShowDialog();
			
		}
		
		private static void wiz_CancelClicked(object sender, EventArgs e) {
			
			String message = "Are you sure you want to cancel installation?";
			
			if( MessageBox.Show(message, "Anolis Installer", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes ) {
				
				Application.Exit();
			}
			
		}
		
		internal static WelcomePage             PageAWelcome       { get; private set; }
		internal static MainActionPage          PageBMainAction    { get; private set; }
		
		internal static SelectPackagePage       PageICSelectPackage { get; private set; }
		internal static ExtractingPage          PageIDExtracting    { get; private set; }
		internal static ModifyPackagePage       PageIEModifyPackage { get; private set; }
		internal static InstallationOptionsPage PageIFInstallationOptions { get; private set; }
		
		internal static DestinationPage         PageDCDestination { get; private set; }
		internal static DownloadingPage         PageDDDownloading { get; private set; }
		
		internal static SelectBackupPage        PageUCSelectBackup { get; private set; }
		
		internal static Stream                  PackageStream      { get; set; }
		
	}
}
