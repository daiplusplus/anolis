using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using W3b.Wizards;
namespace Anolis.Gui.Pages {
	
	public partial class DestinationPage : Anolis.Gui.Pages.BaseInteriorPage {
		
		public DestinationPage() {
			InitializeComponent();
			
			this.Load += new EventHandler(DestinationPage_Load);
			this.__progGroup.CheckedChanged += new EventHandler(__progGroup_CheckedChanged);
		}
		
		private void __progGroup_CheckedChanged(object sender, EventArgs e) {
			
			__progGroupAll.Enabled = __progGroup.Checked;
			__progGroupMe .Enabled = __progGroup.Checked;			
		}
		
		private void DestinationPage_Load(object sender, EventArgs e) {
			
			__destPath.Text = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Anolis" );
			
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageDDDownloading; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageBMainAction; }
		}
		
	}
}
