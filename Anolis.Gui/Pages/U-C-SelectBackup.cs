using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Anolis.Gui.Pages {
	
	public partial class SelectBackupPage : Anolis.Gui.Pages.BaseInteriorPage {
		
		public SelectBackupPage() {
			InitializeComponent();
		}
		
		public override W3b.Wizards.WizardPage NextPage {
			get { return null; }
		}
		
		public override W3b.Wizards.WizardPage PrevPage {
			get { return Program.PageBMainAction; }
		}
		
	}
}
