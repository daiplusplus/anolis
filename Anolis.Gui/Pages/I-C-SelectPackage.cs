using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
			
			// Load the embedded list
			String[] embedded = PackageManager.GetEmbeddedPackages();
			__optPackagesList.Items.Clear();
			
			foreach(String name in embedded) {
				
				ListViewItem lvi = new ListViewItem( name );
				__optPackagesList.Items.Add( lvi );
				
			}
			
		}
		
		private void __optBrowseBrowse_Click(Object sender, EventArgs e) {
			
			if( __ofd.ShowDialog(this) == DialogResult.OK ) {
				
				__optBrowseFilename.Text = __ofd.FileName;
			}
			
		}
		
		public override W3b.Wizards.WizardPage NextPage {
			get { return Program.PageIDExtracting; }
		}
		
		public override W3b.Wizards.WizardPage PreviousPage {
			get { return Program.PageBMainAction; }
		}
		
	}
}
