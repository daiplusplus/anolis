using System;
using System.Windows.Forms;

using Anolis.Core.Utility;
using Anolis.Packages.Utility;

namespace Anolis.Installer.Pages {
	
	public partial class InstallationOptionsForm : BaseForm {
		
		public InstallationOptionsForm() {
			InitializeComponent();
			
			this.__ok.Click += new EventHandler(__ok_Click);
			
			this.__restore .CheckedChanged += new EventHandler(__restore_CheckedChanged);
			this.__lite    .CheckedChanged += new EventHandler(__lite_CheckedChanged);
			this.__feedback.CheckedChanged += new EventHandler(__feedback_CheckedChanged);
			
			this.Load           += new EventHandler(InstallationOptionsForm_Load);
			this.VisibleChanged += new EventHandler(InstallationOptionsForm_VisibleChanged);
			this.FormClosing    += new System.Windows.Forms.FormClosingEventHandler(InstallationOptionsForm_FormClosing);
		}
		
		private void InstallationOptionsForm_Load(Object sender, EventArgs e) {
			
			LoadUI();
		}
		
		private void InstallationOptionsForm_VisibleChanged(Object sender, EventArgs e) {
			
			if( Visible ) LoadUI();
		}
		
		private void InstallationOptionsForm_FormClosing(Object sender, FormClosingEventArgs e) {
			e.Cancel = true;
			Owner.BringToForeground();
			Hide();
		}
		
		private void __ok_Click(object sender, EventArgs e) {
			Close(); // which calls FormClosing
		}
		
		protected override String LocalizePrefix {
			get { return "C_F2"; }
		}
		
		protected override void Localize() {
			base.Localize();
			
			if( !SystemRestore.IsSystemRestoreAvailable() ) {
				
				__restoreDesc.Text = InstallerResources.GetString("C_F2_RestoreDescNA");
			}
			
		}
		
		private void __restore_CheckedChanged(object sender, EventArgs e) {
			
			PackageInfo.SystemRestore = __restore.Checked;
		}
		
		private void __lite_CheckedChanged(object sender, EventArgs e) {
			
			PackageInfo.LiteMode = __lite.Checked;
		}
		
		private void __feedback_CheckedChanged(object sender, EventArgs e) {
			
			InstallationInfo.FeedbackSend = __feedback.Checked;
		}
		
		private void LoadUI() {
			
			if( PackageInfo.I386Install ) {
				
				__restore.Enabled = false;
				__restore.Checked = false;
				
			} else {
				
				if( __restore.Enabled = __restoreDesc.Enabled = SystemRestore.IsSystemRestoreAvailable() ) {
					
					__restore.Checked  = PackageInfo.SystemRestore;
					
				} else {
					
					__restore.Checked  = false;
				}
				
			}
			
			__lite        .Checked = PackageInfo.LiteMode;
			__feedback    .Checked = InstallationInfo.FeedbackSend;
			
			__feedback    .Enabled = InstallationInfo.FeedbackCanSend;
			__feedbackDesc.Enabled = InstallationInfo.FeedbackCanSend;
			
			if( !__feedback.Enabled ) __feedback.Checked = false;
		}
		
	}
}
