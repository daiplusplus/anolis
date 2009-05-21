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
			
			this.__culture.DrawMode = DrawMode.OwnerDrawFixed;
			this.__culture.DrawItem += new DrawItemEventHandler(__culture_DrawItem);
			
			Localize();
		}
		
		private void __culture_DrawItem(object sender, DrawItemEventArgs e) {
			
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
			
			// TODO: Figure out a way to support localisation
			
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageBMainAction; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return null; }
		}
		
	}
}
