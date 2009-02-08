using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Anolis.Gui.Pages {
	
	public partial class SelectPackagePage : BaseInteriorPage {
		
		public SelectPackagePage() {
			InitializeComponent();

			this.Load += new EventHandler(SelectPackage_Load);
			
			this.__optBrowseBrowse.Click += new EventHandler(__optBrowseBrowse_Click);
			
		}
		
		private void SelectPackage_Load(object sender, EventArgs e) {
			
		}
		
		private void __optBrowseBrowse_Click(Object sender, EventArgs e) {
			
			if( __ofd.ShowDialog(this) == DialogResult.OK ) {
				
				__optBrowseFilename.Text = __ofd.FileName;
			}
			
		}
		
		public override W3b.Wizards.WizardPage NextPage {
			get { return Program.PageDExtracting; }
		}
		
		public override W3b.Wizards.WizardPage PreviousPage {
			get { return Program.PageBMainAction; }
		}
		
	}
}
