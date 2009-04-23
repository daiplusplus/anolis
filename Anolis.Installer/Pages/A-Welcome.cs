using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public partial class WelcomePage : ExteriorPage {
		
		public WelcomePage() {
			
			InitializeComponent();
			
			this.Load += new EventHandler(WelcomePage_Load);
			
			Localize();
		}
		
		private void Localize() {
			
			base.WatermarkImage     = InstallerResources.GetImage("Background");
			base.WatermarkAlignment = ContentAlignment.BottomLeft;
			base.WatermarkWidth     = WatermarkImage.Width; // 273;
			
			this.__title.Text = InstallerResources.GetString("A_Title");
			this.__inst1.Text = InstallerResources.GetString("A_Message");
		}
		
		private void WelcomePage_Load(object sender, EventArgs e) {
			
			WizardForm.EnableNext = true;
			
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageBMainAction; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return null; }
		}
		
	}
}
