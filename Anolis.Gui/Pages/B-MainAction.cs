using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Anolis.Gui.Pages {
	
	public partial class MainActionPage : BaseInteriorPage {
		
		public MainActionPage() {
			InitializeComponent();

			this.Load += new EventHandler(MainActionPage_Load);
		}
		
		private void MainActionPage_Load(object sender, EventArgs e) {
			
			
		}
		
		public override W3b.Wizards.WizardPage NextPage {
			get {
				
				if( __optInstallRad.Checked ) {
					
					return Program.PageICSelectPackage;
					
				} else if( __optUndo.Checked ) {
					
					return Program.PageUCSelectBackup;
					
				} else if( __optCreateRad.Checked ) {
					
					return Program.PageDCDestination;
					
				} else {
					
					return null;
				}
			}
		}
		
		public override W3b.Wizards.WizardPage PreviousPage {
			get { return Program.PageAWelcome; }
		}
		
	}
}
