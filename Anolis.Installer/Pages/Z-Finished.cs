using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards;

namespace Anolis.Installer.Pages {
	
	public partial class FinishedPage : W3b.Wizards.Wizard97.ExteriorPage {
		
		public FinishedPage() {
			InitializeComponent();
			
			this.PageLoad += new EventHandler(FinishedPage_PageLoad);
		}
		
		private void FinishedPage_PageLoad(object sender, EventArgs e) {
			
			this.WizardForm.EnablePrev = false;
			this.WizardForm.EnableNext = true;
			
			this.WizardForm.NextText = "Restart";
			this.WizardForm.NextClicked += new EventHandler(WizardForm_NextClicked);
		}
		
		private void WizardForm_NextClicked(object sender, EventArgs e) {
			
			// restart the computer system
			Anolis.Core.Packages.PackageUtility.InitRestart();
		}
		
		public override BaseWizardPage PrevPage {
			get { return null; }
		}
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
	}
}
