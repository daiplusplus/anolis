using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

using Pesi = Anolis.Packages.PackageExecutionSettingsInfo;

namespace Anolis.Installer.Pages {
	
	public partial class SendFeedbackForm : BaseForm {
		
		public SendFeedbackForm() {
			
			InitializeComponent();
			
			Localize();
			
			this.Load += new EventHandler(SendFeedbackForm_Load);
			
			this.__bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(__bw_RunWorkerCompleted);
			this.__bw.DoWork             += new DoWorkEventHandler(__bw_DoWork);
		}
		
		protected override String LocalizePrefix {
			get { return "G2"; }
		}
		
		private void SendFeedbackForm_Load(object sender, EventArgs e) {
			
			__bw.RunWorkerAsync();
		}
		
		private void __bw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
			
			if( !InstallationInfo.FeedbackCanSend ) {
				e.Result = FeedbackResult.NoUriSpecified;
				return;
			}
			
			if( InstallationInfo.InstallationAborted ) {
				
				FeedbackSender.SendErrorReport( PackageInfo.Package.FeedbackUri );
			}
			
			Uri    uri     = PackageInfo.Package == null ? null : PackageInfo.Package.FeedbackUri;
			String message = InstallationInfo.FeedbackMessage;
			Pesi   pesi    = PackageInfo.Package == null ? null : PackageInfo.Package.ExecutionInfo;
			Log    log     = PackageInfo.Package == null ? null : PackageInfo.Package.Log;
			
			Boolean result = FeedbackSender.SendFeedback( uri, message, log, pesi );
			
			e.Result = result ? FeedbackResult.Success : FeedbackResult.Error;
			
		}
		
		private void __bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			
			String title   = InstallerResources.GetString("G2_ErrorTitle");
			String message = InstallerResources.GetString("G2_ErrorMessage");
			
			if( e.Error != null ) {
				
				// never query e.Result, it raises exceptions if there's an error
				message += " " + e.Error.Message +  " - " + e.Error.GetType().Name;
				
				MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				
			} else {
				
				if( e.Result == null || (FeedbackResult)e.Result == FeedbackResult.Error ) {
					
					// then it's an unknown error (spooky!)
					
					message += " " + InstallerResources.GetString("G2_UnknownError");
					
					MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					
				}
				
			}
			
			Close();
		}
		
		private enum FeedbackResult {
			Success,
			NoUriSpecified,
			Error
		}
		
	}
	
}
