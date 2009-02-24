using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using W3b.Wizards;
namespace Anolis.Gui.Pages {
	public partial class DownloadingPage : Anolis.Gui.Pages.BaseInteriorPage {
		
		public DownloadingPage() {
			InitializeComponent();
		}
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageDCDestination; }
		}
		
	}
}
