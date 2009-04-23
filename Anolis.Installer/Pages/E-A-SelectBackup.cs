using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public partial class SelectBackupPage : Anolis.Installer.Pages.BaseInteriorPage {
		
		public SelectBackupPage() {
			InitializeComponent();
			
			Localize();
		}
		
		protected override String LocalizePrefix { get { return "E_A"; } }
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageBMainAction; }
		}
		
	}
}
