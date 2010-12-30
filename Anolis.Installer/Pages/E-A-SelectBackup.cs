using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Packages;

using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public partial class SelectBackupPage : Anolis.Installer.Pages.BaseInteriorPage {
		
		public SelectBackupPage() {
			
			InitializeComponent();
			
			this.__browse.Click += new EventHandler(__browse_Click);
			this.PageUnload += new EventHandler<W3b.Wizards.PageChangeEventArgs>(Page_Unload);
			this.PageLoad   += new EventHandler(Page_Load);
		}
		
		protected override String LocalizePrefix { get { return "E_A"; } }
		
		protected override void Localize() {
			base.Localize();
			
			// problem: the feedback form will be shown regardless of whether or not the uninstallation package has a feedback URI set
			
			if( InstallerResources.IsCustomized ) {
				
				__feedbackLbl.Text = InstallerResources.GetString("E_A_feedbackLbl_Cus", InstallerResources.CustomizedSettings.InstallerName, InstallerResources.CustomizedSettings.InstallerDeveloper);
			}
			
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageCGInstalling; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageBMainAction; }
		}
		
		private Boolean PrepareToUninstall() {
			
			InstallationInfo.FeedbackMessage = __feedback.Text;
			
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
			
			return PackageInfo.Package.Evaluate() == EvaluationResult.True;
		}
		
#region UI Events
		
		private void __browse_Click(object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) == DialogResult.OK ) {
				
				__dir.Text = __fbd.SelectedPath;
			}
			
		}
		
		private void Page_Load(object sender, EventArgs e) {
			
			InstallationInfo.ProgramMode = ProgramMode.UninstallPackage;
			
			WizardForm.NextText = InstallerResources.GetString("E_A_uninstallButton");
			
			if( InstallationInfo.UninstallPackage != null ) {
				
				WizardForm.EnableBack = false;
				
				// as the control is not visible, CreateControl will not be called, so it won't double-load the stuff
				__culture.Visible                = true;
				__uninstallationLanguage.Visible = true;
				
				__dir.Text = InstallationInfo.UninstallPackage.Directory.FullName;
			}
			
		}
		
		private void Page_Unload(object sender, W3b.Wizards.PageChangeEventArgs e) {
			
			if( e.PageToBeLoaded != NextPage ) {
				
				WizardForm.NextText = InstallerResources.GetString("Wiz_Next");
				return;
			}
			
			if( !PrepareToUninstall() ) {
				
				MessageBox.Show(this, InstallerResources.GetString("E_A_notValidDirectory"), "Anolis Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				
				e.Cancel = true;
				
			} else {
				
				WizardForm.NextText = InstallerResources.GetString("Wiz_Next");
				
			}
			
		}
		
#endregion
		
	}
}
