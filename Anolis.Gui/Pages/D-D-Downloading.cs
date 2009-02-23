using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Anolis.Gui.Pages {
	public partial class DownloadingPage : Anolis.Gui.Pages.BaseInteriorPage {
		
		public DownloadingPage() {
			InitializeComponent();
		}
		
		public override W3b.Wizards.WizardPage NextPage {
			get { return null; }
		}
		
		public override W3b.Wizards.WizardPage PrevPage {
			get { return Program.PageDCDestination; }
		}
		
	}
}
