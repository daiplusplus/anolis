using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Anolis.Gui.Pages {
	
	public partial class DestinationPage : Anolis.Gui.Pages.BaseInteriorPage {
		
		public DestinationPage() {
			InitializeComponent();

			this.Load += new EventHandler(DestinationPage_Load);
		}
		
		private void DestinationPage_Load(object sender, EventArgs e) {
			
			__destPath.Text = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Anolis" );
			
		}
		
		public override W3b.Wizards.WizardPage NextPage {
			get { return Program.PageDDDownloading; }
		}
		
		public override W3b.Wizards.WizardPage PreviousPage {
			get { return Program.PageBMainAction; }
		}
		
	}
}
