using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Anolis.Core.Utility;

namespace Anolis.Installer.Pages {
	
	public partial class InstallationOptionsForm : BaseForm {
		
		public InstallationOptionsForm() {
			InitializeComponent();
			
			this.__ok.Click += new EventHandler(__ok_Click);
			
			this.__restore.CheckedChanged += new EventHandler(__restore_CheckedChanged);
			this.__lite   .CheckedChanged += new EventHandler(__lite_CheckedChanged);
			
			this.Load += new EventHandler(InstallationOptionsForm_Load);
			
			Localize();
		}
		
		private void InstallationOptionsForm_Load(object sender, EventArgs e) {
			
			LoadUI();
		}
		
		private void __ok_Click(object sender, EventArgs e) {
			this.Close();
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
		
		private void LoadUI() {
			
			if( Anolis.Core.Utility.SystemRestore.IsSystemRestoreAvailable() ) {
				
				__restore.Enabled  = true;
				__restore.Checked  = PackageInfo.SystemRestore;
				
				__restoreDesc.Enabled = true;
				
			} else {
				
				__restore.Enabled     = false;
				__restore.Checked     = false;
				
				__restoreDesc.Enabled = false;
			}
			
			__lite.Checked = PackageInfo.LiteMode;
			
		}
		
	}
}
