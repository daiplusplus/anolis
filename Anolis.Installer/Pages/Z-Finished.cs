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
			
			this.WizardForm.EnableCancel = false;
			
			switch(Program.ProgramMode) {
				case ProgramMode.InstallPackage:
				case ProgramMode.UninstallPackage:
					
					this.WizardForm.NextText = "Restart";
					this.WizardForm.NextClicked += new EventHandler(WizardForm_NextClicked_Restart);
					
					if(Program.ProgramMode == ProgramMode.InstallPackage) {
						
						this.__installationComplete.Text = 
@"Installation has been completed succesfully. Your computer will now restart.

Close all open applications before continuing. Do not attempt to open any new applications.";
						
					} else {
						
						this.__installationComplete.Text = 
@"Uninstallation has been completed succesfully. Your computer will now restart.

Close all open applications before continuing. Do not attempt to open any new applications.";
						
					}
					
					
					break;
					
				case ProgramMode.None:
				case ProgramMode.InstallTools:
					
					this.WizardForm.NextText = "Finish";
					this.WizardForm.NextClicked += new EventHandler(WizardForm_NextClicked_Quit);
					
					this.__installationComplete.Text = 
@"Installation has been completed succesfully.";
					
					break;
			}
			
		}
		
		private void WizardForm_NextClicked_Restart(Object sender, EventArgs e) {
			
			// restart the computer system
			Anolis.Core.Packages.PackageUtility.InitRestart();
		}
		
		private void WizardForm_NextClicked_Quit(Object sender, EventArgs e) {
			
			Application.Exit();
		}
		
		public override BaseWizardPage PrevPage {
			get { return null; }
		}
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
	}
}
