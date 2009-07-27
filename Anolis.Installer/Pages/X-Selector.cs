using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public partial class SelectorPage : BaseInteriorPage {
		
		public SelectorPage() {
			InitializeComponent();
		}
		
		protected override String LocalizePrefix {
			get { return "X"; }
		}
		
		public override BaseWizardPage NextPage {
			get { throw new NotImplementedException(); }
		}
		
		public override BaseWizardPage PrevPage {
			get { throw new NotImplementedException(); }
		}
	}
}
