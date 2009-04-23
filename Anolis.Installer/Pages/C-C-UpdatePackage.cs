using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards;
using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public partial class UpdatePackagePage : BaseInteriorPage {
		
		public UpdatePackagePage() {
			
			InitializeComponent();
			
			this.Load       += new EventHandler(InstallationOptionsPage_Load);
			this.PageLoad   += new EventHandler(InstallationOptionsPage_PageLoad);
			this.PageUnload += new EventHandler<PageChangeEventArgs>(InstallationOptionsPage_PageUnload);
			
			Localize();
		}
		
		protected override String LocalizePrefix { get { return "C_C"; } }
		
		private void InstallationOptionsPage_Load(object sender, EventArgs e) {
		}
		
		private void InstallationOptionsPage_PageLoad(object sender, EventArgs e) {
			
			
		}
		
		private void InstallationOptionsPage_PageUnload(object sender, PageChangeEventArgs e) {
			
			
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageCASelectPackage; }
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageCDModifyPackage; }
		}
		
	}
}
