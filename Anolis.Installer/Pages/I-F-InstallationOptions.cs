using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards;

namespace Anolis.Installer.Pages {
	
	public partial class InstallationOptionsPage : BaseInteriorPage {
		
		public InstallationOptionsPage() {
			InitializeComponent();
			
			this.Load += new EventHandler(Extracting_Load);
			
		}
		
		private void Extracting_Load(object sender, EventArgs e) {
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageIEModifyPackage; }
		}
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
	}
}
