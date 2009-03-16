using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using W3b.Wizards;
namespace Anolis.Installer.Pages {
	
	public partial class MainActionPage : BaseInteriorPage {
		
		public MainActionPage() {
			InitializeComponent();
			
			this.PageUnload += new EventHandler<PageChangeEventArgs>(MainActionPage_PageUnload);
		}
		
		private void MainActionPage_PageUnload(object sender, PageChangeEventArgs e) {
			
			if(e.PageToBeLoaded == Program.PageAWelcome) {
				Program.ProgramMode = ProgramMode.None;
				return;
			}
			
			if( __optInstallRad.Checked ) {
				
				Program.ProgramMode = ProgramMode.InstallPackage;
				
			} else if( __optUndo.Checked ) {
				
				Program.ProgramMode = ProgramMode.UninstallPackage;
				
			} else if( __optCreateRad.Checked ) {
				
				Program.ProgramMode = ProgramMode.InstallTools;
				
			} else {
				
				Program.ProgramMode = ProgramMode.None;
			}
			
		}
		
		public override BaseWizardPage NextPage {
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
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageAWelcome; }
		}
		
	}
}
