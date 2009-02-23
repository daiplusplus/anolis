using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards;

namespace Anolis.Gui.Pages {
	
	public partial class WelcomePage : W3b.Wizards.Wizard97.ExteriorPage {
		
		public WelcomePage() {
			
			InitializeComponent();
			
			this.Load += new EventHandler(WelcomePage_Load);
		}
		
		private void WelcomePage_Load(object sender, EventArgs e) {
			
			WizardForm.EnableNext = true;
		}
		
		public override WizardPage NextPage {
			get { return Program.PageBMainAction; }
		}
		
		public override WizardPage PrevPage {
			get { return null; }
		}
		
	}
}
