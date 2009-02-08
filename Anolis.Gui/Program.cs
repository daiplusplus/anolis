using System;
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
			PageAWelcome       = new WelcomePage();
			PageBMainAction    = new MainActionPage();
			PageCSelectPackage = new SelectPackagePage();
			PageDExtracting    = new ExtractingPage();
			PageEModifyPackage = new ModifyPackagePage();
			
			IWizardForm wiz = WizardForm.Create();
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
		
		internal static WelcomePage       PageAWelcome       { get; private set; }
		internal static MainActionPage    PageBMainAction    { get; private set; }
		internal static SelectPackagePage     PageCSelectPackage { get; private set; }
		internal static ExtractingPage    PageDExtracting    { get; private set; }
		internal static ModifyPackagePage PageEModifyPackage { get; private set; }
		
	}
}
