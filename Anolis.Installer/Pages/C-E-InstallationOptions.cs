using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards;
using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public partial class InstallationOptionsPage : BaseInteriorPage {
		
		public InstallationOptionsPage() {
			
			InitializeComponent();
			
			this.Load       += new EventHandler(InstallationOptionsPage_Load);
			this.PageLoad   += new EventHandler(InstallationOptionsPage_PageLoad);
			this.PageUnload += new EventHandler<PageChangeEventArgs>(InstallationOptionsPage_PageUnload);
			
			this.__i386  .CheckedChanged += new EventHandler(__i386_CheckedChanged);
			this.__backup.CheckedChanged += new EventHandler(__backup_CheckedChanged);
			this.__sysRes.CheckedChanged += new EventHandler(__sysRes_CheckedChanged);

			this.__i386Browse.Click += new EventHandler(__i386Browse_Click);
			
			this.__bw.DoWork += new DoWorkEventHandler(__bw_DoWork);
			
			Localize();
		}
		
		private void __sysRes_CheckedChanged(object sender, EventArgs e) {
			
			__sysResDesc.Enabled = __sysRes.Checked;
			
		}
		
		private void __i386_CheckedChanged(object sender, EventArgs e) {
			
			__i386Browse.Enabled = __i386.Checked;
			__i386Lbl   .Enabled = __i386.Checked;
			__i386Path  .Enabled = __i386.Checked;
			
			__backup    .Enabled = __comp.Checked;
			
			if( __backup.Checked ) {
				
				__backupBrowse.Enabled = __backup.Checked;
				__backupDesc  .Enabled = __backup.Checked;
				__backupPath  .Enabled = __backup.Checked;
			} else {
				__backupBrowse.Enabled = false;
				__backupDesc  .Enabled = false;
				__backupPath  .Enabled = false;
			}
			
			__sysRes.Enabled = __comp.Checked;
			if( __sysRes.Checked ) {
				__sysResDesc.Enabled =  __comp.Checked;
			} else {
				__sysResDesc.Enabled = false;
			}
		}
		
		private void __backup_CheckedChanged(object sender, EventArgs e) {
			
			__backupBrowse.Enabled = __backup.Checked;
			__backupDesc  .Enabled = __backup.Checked;
			__backupPath  .Enabled = __backup.Checked;
			
		}
		
		private void __i386Browse_Click(object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) == DialogResult.OK ) {
				
				__i386Path.Text = __fbd.SelectedPath;
			}
			
		}
		
		protected override String LocalizePrefix { get { return "C_E"; } }
		
		private void __bw_DoWork(object sender, DoWorkEventArgs e) {
			
			
			
		}
		
		private void InstallationOptionsPage_Load(object sender, EventArgs e) {
			
			this.WizardForm.NextClicked += new EventHandler(WizardForm_NextClicked);
		}
		
		private void WizardForm_NextClicked(object sender, EventArgs e) {
			
			PackageInfo.SystemRestore = __sysRes.Checked;
			PackageInfo.BackupPath    = __backup.Checked ? __backupPath.Text : null;
			
			PackageInfo.I386Install = __i386.Checked;
			if( __i386.Checked ) {
				PackageInfo.I386Directory = new System.IO.DirectoryInfo( __i386Path.Text );
			}
			
		}
		
		private String _oldNextText;
		
		private void InstallationOptionsPage_PageLoad(object sender, EventArgs e) {
			
			_oldNextText = WizardForm.NextText;
			
			WizardForm.NextText = InstallerResources.GetString("C_E_installButton");
		}
		
		private void InstallationOptionsPage_PageUnload(object sender, PageChangeEventArgs e) {
			
			if( __i386.Checked && __i386Path.Text.Length == 0 ) {
				e.Cancel = true;
				return;
			}
			
			WizardForm.NextText = _oldNextText;
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageCDModifyPackage; }
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageCFInstalling; }
		}
		
	}
}
