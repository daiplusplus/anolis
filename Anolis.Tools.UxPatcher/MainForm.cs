using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Anolis.Tools.UxPatcher {
	
	public partial class MainForm : Form {
		
		private InstallationInfo _info;
		
		public MainForm() {
			InitializeComponent();
			
			Patcher.PatchEvent += new EventHandler(Patcher_PatchEvent);
			
			this.Load += new EventHandler(MainForm_Load);
			
			this.__patch.Click += new EventHandler(__patch_Click);
			this.__restart.Click += new EventHandler(__restart_Click);
			
			this.__bw.DoWork += new System.ComponentModel.DoWorkEventHandler(__bw_DoWork);
		}
		
		private void __bw_DoWork(Object sender, DoWorkEventArgs e) {
			
			Patcher.Patch( _info );
			
			this.Invoke(new MethodInvoker( delegate() {
				
				__restart.Enabled = true;
			}));
			
		}
		
		private void Patcher_PatchEvent(Object sender, EventArgs e) {
			
			this.Invoke(new MethodInvoker( delegate() {
				__status.Text = Patcher.Status;
			}));
		}
		
		private void __restart_Click(Object sender, EventArgs e) {
			
			PatchUtility.Restart();
		}
		
		private void __patch_Click(Object sender, EventArgs e) {
			
			__patch  .Enabled = false;
			
			_info.SystemRestore = __systemRestore.Checked;
			
			__bw.RunWorkerAsync();
		}
		
		private void MainForm_Load(Object sender, EventArgs e) {
			
			_info = PatchUtility.GetInstallationInfo();
			
			if( _info.Install == Install.NotSupported ) {
				
				MessageBox.Show(this, "Your operating system is not supported by this patcher", "UxTheme Patcher", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				this.Close();
				
			}
			
			// populate Patch tab
			
			__system.Text     = PatchUtility.GetSystemInfo();
			__patchMatch.Text = _info.ToString();
			
		}
		
		
	}
}
