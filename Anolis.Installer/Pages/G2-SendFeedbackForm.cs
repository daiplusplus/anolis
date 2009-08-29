using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

using Log  = Anolis.Core.Utility.Log;
using Pesi = Anolis.Core.Packages.PackageExecutionSettingsInfo;

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
			
			FeedbackResult result;
			
			if( e.Result == null ) result = FeedbackResult.Error;
			else                   result = (FeedbackResult)e.Result;
			
			if( result == FeedbackResult.Error ) {
				
				String reason = InstallerResources.GetString("G2_UnknownError");
				if( e.Error != null ) reason = e.Error.Message +  " - " + e.Error.GetType().Name;
				
				String title   = InstallerResources.GetString("G2_ErrorTitle");
				String message = InstallerResources.GetString("G2_ErrorMessage");
				
				MessageBox.Show(this, message + " " + reason, title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
