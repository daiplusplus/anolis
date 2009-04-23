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
			
			this.Load += new EventHandler(InstallationOptionsPage_Load);
			this.PageLoad   += new EventHandler(InstallationOptionsPage_PageLoad);
			this.PageUnload += new EventHandler<PageChangeEventArgs>(InstallationOptionsPage_PageUnload);

			__bw.DoWork += new DoWorkEventHandler(__bw_DoWork);
			
			Localize();
		}
		
		protected override String LocalizePrefix { get { return "C_E"; } }
		
		private void __bw_DoWork(object sender, DoWorkEventArgs e) {
			
			
			
		}
		
		private void InstallationOptionsPage_Load(object sender, EventArgs e) {
			
			this.WizardForm.NextClicked += new EventHandler(WizardForm_NextClicked);
		}
		
		private void WizardForm_NextClicked(object sender, EventArgs e) {
			
			PackageInfo.SystemRestore = __sysRes.Checked;
		}
		
		private String _oldNextText;
		
		private void InstallationOptionsPage_PageLoad(object sender, EventArgs e) {
			
			_oldNextText = WizardForm.NextText;
			
			WizardForm.NextText = InstallerResources.GetString("C_D_installButton");
		}
		
		private void InstallationOptionsPage_PageUnload(object sender, PageChangeEventArgs e) {
			
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
