using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Anolis.Core.Packages;
using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public partial class FinishedPage : ExteriorPage {
		
		public FinishedPage() {
			InitializeComponent();
			
			this.PageLoad += new EventHandler(FinishedPage_PageLoad);
			
			Localize();
		}
		
		private void Localize() {
			
			base.WatermarkImage     = InstallerResources.GetImage("Background");
			base.WatermarkAlignment = ContentAlignment.BottomLeft;
			base.WatermarkWidth     = WatermarkImage.Width; // 273;
			
			__title.Text = InstallerResources.GetString("F_Title");
		}
		
		private void FinishedPage_PageLoad(object sender, EventArgs e) {
			
			this.WizardForm.EnableBack   = false;
			this.WizardForm.EnableNext   = true;
			this.WizardForm.EnableCancel = false;
			
			switch(Program.ProgramMode) {
				case ProgramMode.InstallPackage:
				case ProgramMode.UninstallPackage:
					
					if(Program.ProgramMode == ProgramMode.InstallPackage && PackageInfo.RequiresRestart ) {
						
						this.__installationComplete.Text = InstallerResources.GetString("F_installationCompleteRestart");
						
					} else if(Program.ProgramMode == ProgramMode.InstallPackage) {
						
						this.__installationComplete.Text = InstallerResources.GetString("F_installationComplete");
						
					} else {
						
						this.__installationComplete.Text = InstallerResources.GetString("F_uninstallationComplete");
						
					}
					
					if( PackageInfo.RequiresRestart ) {
						
						this.WizardForm.NextText     = InstallerResources.GetString("F_restartButton");
						this.WizardForm.NextClicked += new EventHandler(WizardForm_NextClicked_Restart);
						
					} else {
						
						this.WizardForm.NextText = InstallerResources.GetString("F_finishButton");
						this.WizardForm.NextClicked += new EventHandler(WizardForm_NextClicked_Quit);
					}
					
					
					break;
					
				case ProgramMode.None:
				case ProgramMode.InstallTools:
					
					this.WizardForm.NextText = InstallerResources.GetString("F_finishButton");
					this.WizardForm.NextClicked += new EventHandler(WizardForm_NextClicked_Quit);
					
					this.__installationComplete.Text = InstallerResources.GetString("F_installTools");
					
					break;
			}
			
		}
		
		private void WizardForm_NextClicked_Restart(Object sender, EventArgs e) {
			
			// restart the computer system
			PackageUtility.InitRestart();
			
			// TODO: Show a modal window telling the user to wait as restarting can take a while?
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
