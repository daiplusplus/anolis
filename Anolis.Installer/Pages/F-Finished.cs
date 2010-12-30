using System;
using System.Drawing;
using System.Windows.Forms;

using Anolis.Packages.Utility;

using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public partial class FinishedPage : BaseExteriorPage {
		
		public FinishedPage() {
			InitializeComponent();
			
			this.PageLoad += new EventHandler(FinishedPage_PageLoad);
			
			this.__anolisWebsite.Click += new EventHandler(Website_Click);
			this.__authorWebsite.Click += new EventHandler(Website_Click);
			
			__anolisWebsite.Tag = new Uri("http://anol.is");
		}
		
		protected override String LocalizePrefix {
			get { return "F"; }
		}
		
		protected override void Localize() {
			
			base.Localize();
			
			base.WatermarkImage     = InstallerResources.GetImage("Background");
			base.WatermarkAlignment = ContentAlignment.BottomLeft;
			base.WatermarkWidth     = WatermarkImage.Width; // 273;
		}
		
		private void FinishedPage_PageLoad(object sender, EventArgs e) {
			
			this.WizardForm.EnableBack   = false;
			this.WizardForm.EnableNext   = true;
			this.WizardForm.EnableCancel = false;
			
			if( InstallerResources.IsCustomized ) {
				
				String author = InstallerResources.CustomizedSettings.InstallerDeveloper;
				String webStr = InstallerResources.CustomizedSettings.InstallerWebsite;
				
				Uri uri;
				
				if( !String.IsNullOrEmpty( author ) && Uri.TryCreate( InstallerResources.CustomizedSettings.InstallerWebsite, UriKind.Absolute, out uri ) ) {
					
					__authorWebsiteLbl.Text    = author;
					__authorWebsiteLbl.Visible = true;
					
					__authorWebsite.Tag        = uri;
					__authorWebsite.Text       = uri.OriginalString;
					__authorWebsite.Visible    = true;
				}
				
			}
			
			if( InstallationInfo.InstallationAborted ) {
				
				this.__title.Text                = InstallerResources.GetString("F_aborted");
				this.__installationComplete.Text = InstallerResources.GetString("F_abortedDesc");
				
				this.WizardForm.NextText = InstallerResources.GetString("F_finishButton");
				this.WizardForm.NextClicked += new EventHandler(WizardForm_NextClicked_Quit);
			
			} else {
				
				switch( InstallationInfo.ProgramMode ) {
					case ProgramMode.InstallPackage:
						
						if( PackageInfo.RequiresRestart ) {
							this.__installationComplete.Text = InstallerResources.GetString("F_installationCompleteRestart");
						} else {
							this.__installationComplete.Text = InstallerResources.GetString("F_installationComplete");
						}
						
						break;
						
					case ProgramMode.UninstallPackage:
						
						this.__title.Text = InstallerResources.GetString("F_TitleUninstall");
						
						if( PackageInfo.RequiresRestart ) {
							this.__installationComplete.Text = InstallerResources.GetString("F_uninstallationCompleteRestart");
						} else {
							this.__installationComplete.Text = InstallerResources.GetString("F_uninstallationComplete");
						}
						
						break;
					
					case ProgramMode.InstallTools:
						
						this.__installationComplete.Text = InstallerResources.GetString("F_installTools");
						
						break;
				}
				
				/////////////////////////////////
				
				if( PackageInfo.RequiresRestart ) {
					
					this.WizardForm.NextText     = InstallerResources.GetString("F_restartButton");
					this.WizardForm.NextClicked += new EventHandler(WizardForm_NextClicked_Restart);
					
				} else {
					
					this.WizardForm.NextText = InstallerResources.GetString("F_finishButton");
					this.WizardForm.NextClicked += new EventHandler(WizardForm_NextClicked_Quit);
				}
				
			}
			
			if( InstallationInfo.FeedbackCanSend && InstallationInfo.FeedbackSend ) {
				
				SendFeedbackForm feedbackForm = new SendFeedbackForm();
				feedbackForm.ShowDialog(this);
			}
			
		}
		
		private void WizardForm_NextClicked_Restart(Object sender, EventArgs e) {
			
			// restart the computer system
			PackageUtility.InitRestart();
			
			// Show a modal window telling the user to wait as restarting can take a while?
			
			WizardForm.EnableNext = false;
			WizardForm.EnableBack = false;
			
			WaitForm wait = new WaitForm();
			wait.ShowDialog(this);
		}
		
		private void WizardForm_NextClicked_Quit(Object sender, EventArgs e) {
			
			Application.Exit();
		}
		
		private void Website_Click(Object sender, EventArgs e) {
			
			Uri uri = (sender as Control).Tag as Uri;
			if( uri == null ) return;
			
			System.Diagnostics.Process.Start( uri.ToString() );
		}
		
		public override BaseWizardPage PrevPage {
			get { return null; }
		}
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
	}
}
