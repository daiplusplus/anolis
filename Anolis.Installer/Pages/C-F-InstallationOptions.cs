using System;
using System.IO;
using System.Windows.Forms;

using Anolis.Core.Utility;
using Anolis.Packages.Utility;

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
			
			this.__backupBrowse.Click += new EventHandler(__backupBrowse_Click);
			this.__i386Browse  .Click += new EventHandler(__i386Browse_Click);
			
			this.__advanced    .Click += new EventHandler(__advanced_Click);
		}
		
		protected override String LocalizePrefix { get { return "C_F"; } }
		
		protected override void Localize() {
			base.Localize();
			
			if( InstallerResources.IsCustomized ) {
				
				InstallerCustomizer cus = InstallerResources.CustomizedSettings;
				if( cus.HideI386 ) {
					
					__i386      .Visible = false;
					__i386Browse.Visible = false;
					__i386Lbl   .Visible = false;
					__i386Path  .Visible = false;
					
					__comp.Checked = true;
				}
				
			}
			
		}
		
		private void __backupBrowse_Click(object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) == DialogResult.OK ) {
				
				__backupPath.Text = __fbd.SelectedPath;
			}
			
		}
		
		private void __i386_CheckedChanged(object sender, EventArgs e) {
			
			PackageInfo.I386Install = __i386.Checked;
			
			// this code can be optimised for space, but using literals makes it easier to read
			////////////////////////////////////
			
			if( __i386.Checked ) {
				
				__i386Browse.Enabled = true;
				__i386Lbl   .Enabled = true;
				__i386Path  .Enabled = true;
				
			} else {
				
				__i386Browse.Enabled = false;
				__i386Lbl   .Enabled = false;
				__i386Path  .Enabled = false;
				
			}
			
			////////////////////////////////////
			
			if( __comp.Checked ) {
				
				__backup.Enabled = true;
				
			} else {
				
				__backup.Enabled = false;
			}
			
			////////////////////////////////////
			
			if( __backup.Enabled && __backup.Checked ) {
				
				__backupBrowse.Enabled = true;
				__backupDesc  .Enabled = true;
				__backupPath  .Enabled = true;
				
			} else {
				
				__backupBrowse.Enabled = false;
				__backupDesc  .Enabled = false;
				__backupPath  .Enabled = false;
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
		
		private void InstallationOptionsPage_Load(object sender, EventArgs e) {
			
			String backupDir = @"%programfiles%\Anolis\Installer\Backup " + Miscellaneous.RemoveIllegalFileNameChars( PackageInfo.Package.Name, null ) + "-" + DateTime.Now.ToString("yyyy-MM-dd hh-mm");
			
			this.__backupPath.Text = PackageUtility.ResolvePath( backupDir );
			
			if( !PackageInfo.IgnoreCondition ) {
				
				if( InstallationInfo.FailedCondition || !InstallationInfo.EvaluateInstallerCondition() ) {
					
					__i386.Checked = true;
					__comp.Enabled = false;
				}
			}
			
		}
		
		private void __advanced_Click(object sender, EventArgs e) {
			
			Program.PageCFInstallOptForm.ShowDialog(this);
		}
		
		private String _oldNextText;
		
		private void InstallationOptionsPage_PageLoad(object sender, EventArgs e) {
			
			_oldNextText = WizardForm.NextText;
			
			WizardForm.NextText = InstallerResources.GetString("C_F_installButton");
		}
		
		private void InstallationOptionsPage_PageUnload(object sender, PageChangeEventArgs e) {
			
			if( e.PageToBeLoaded == Program.PageCGInstalling ) {
				
				if( __i386.Checked ) {
					
					String i386Path = __i386Path.Text;
					if( !ValidateCDImagePath( i386Path ) ) {
						e.Cancel = true;
						
						MessageBox.Show(this, InstallerResources.GetString("C_F_pathInvalidI386"), "Anolis Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
						
						return;
					}
					
				}
				
				if( __backup.Checked ) {
					
					String backupPath = __backupPath.Text;
					if( backupPath.Length == 0 || !Path.IsPathRooted( backupPath ) ) {
						e.Cancel = true;
						
						MessageBox.Show(this, InstallerResources.GetString("C_F_pathInvalidBackup") , "Anolis Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
						
						return;
					}
					
					DirectoryInfo dir = new DirectoryInfo( backupPath );
					
					if( dir.Exists && !dir.IsEmpty() ) {
						
						e.Cancel = true;
						
						MessageBox.Show(this, InstallerResources.GetString("C_F_pathExistsBackup"), "Anolis Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
						
						return;
					}
				}
				
				PackageInfo.BackupPath    = __backup.Checked ? __backupPath.Text : null;
				
				PackageInfo.I386Install   = __i386.Checked;
				if( __i386.Checked ) {
					PackageInfo.I386Directory = new System.IO.DirectoryInfo( __i386Path.Text );
				}
				
			}
			
			WizardForm.NextText = _oldNextText;
		}
		
		private static Boolean ValidateCDImagePath(String path) {
			
			Boolean pathIsInvalid = path.Length == 0 || !Path.IsPathRooted( path ) || !Directory.Exists( path );
			if( pathIsInvalid ) return false;
			
			// check it contains at least an I386 or AMD64 dir and isn't readonly
			
			DirectoryInfo dir = new DirectoryInfo( path );
			
			if( (dir.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly ) return false;
			
			Boolean i386  = dir.GetDirectory("I386").Exists;
			Boolean amd64 = dir.GetDirectory("AMD64").Exists;
			
			return i386 || amd64;
		}
		
		public override BaseWizardPage PrevPage {
			get {
				if( InstallationInfo.UseSelector.Value ) return Program.PageCE1Selector;
				return Program.PageCE2ModifyPackage;
			}
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageCGInstalling; }
		}
		
	}
}
