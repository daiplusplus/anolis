using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Anolis.Core;

using W3b.Wizards.WindowsForms;
using System.IO;
using Anolis.Core.Packages;

namespace Anolis.Installer.Pages {
	
	public partial class SelectBackupPage : Anolis.Installer.Pages.BaseInteriorPage {
		
		public SelectBackupPage() {
			
			InitializeComponent();
			
			this.__browse.Click += new EventHandler(__browse_Click);
			this.PageUnload += new EventHandler<W3b.Wizards.PageChangeEventArgs>(Page_Unload);
			this.PageLoad   += new EventHandler(Page_Load);
			
			Localize();
		}
		
		protected override String LocalizePrefix { get { return "E_A"; } }
		
		public override BaseWizardPage NextPage {
			get { return Program.PageCGInstalling; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageBMainAction; }
		}
		
		private Boolean PrepareToUninstall() {
			
			if( __dir.Text.Length == 0 ) return false;
			if( !Path.IsPathRooted( __dir.Text ) ) return false;
			
			DirectoryInfo dir = new DirectoryInfo( __dir.Text );
			if( !dir.Exists ) return false;
			
			FileInfo packageFile = dir.GetFile("package.xml");
			if( !packageFile.Exists ) return false;
			
			try {
				
				PackageInfo.SystemRestore = __sysRes.Checked;
				PackageInfo.Package       = Package.FromFile( packageFile.FullName );
				
			} catch(PackageException) {
				return false;
			}
			
			EvaluationInfo res = PackageInfo.Package.EvaluateCondition();
			return res.Success;
			
		}
		
#region UI Events
		
		private void __browse_Click(object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) == DialogResult.OK ) {
				
				__dir.Text = __fbd.SelectedPath;
			}
			
		}
		
		private void Page_Load(object sender, EventArgs e) {
			
			WizardForm.NextText = "Uninstall";
		}
		
		private void Page_Unload(object sender, W3b.Wizards.PageChangeEventArgs e) {
			
			if( e.PageToBeLoaded != NextPage ) {
				
				WizardForm.NextText = InstallerResources.GetString("Wiz_Next");
				return;
			}
			
			if( !PrepareToUninstall() ) {
				
				MessageBox.Show(this, "The specified directory does not contain a valid uninstallation package", "Anolis Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				
				e.Cancel = true;
				
			} else {
				
				WizardForm.NextText = InstallerResources.GetString("Wiz_Next");
				
			}
			
		}
		
#endregion
		
	}
}
