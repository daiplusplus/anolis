using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using W3b.Wizards;
namespace Anolis.Gui.Pages {
	
	public partial class SelectBackupPage : Anolis.Gui.Pages.BaseInteriorPage {
		
		public SelectBackupPage() {
			InitializeComponent();
		}
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageBMainAction; }
		}
		
	}
}
