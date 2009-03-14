using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards;

namespace Anolis.Installer.Pages {
	
	public partial class InstallationOptionsPage : BaseInteriorPage {
		
		public InstallationOptionsPage() {
			
			InitializeComponent();
			
			this.PageLoad   += new EventHandler(InstallationOptionsPage_PageLoad);
			this.PageUnload += new EventHandler<PageChangeEventArgs>(InstallationOptionsPage_PageUnload);
		}
		
		private void InstallationOptionsPage_PageLoad(object sender, EventArgs e) {
			
			WizardForm.NextText = "Install";
		}
		
		private void InstallationOptionsPage_PageUnload(object sender, PageChangeEventArgs e) {
			
			WizardForm.NextText = "Next";
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageIEModifyPackage; }
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageIGInstalling; }
		}
		
	}
}
