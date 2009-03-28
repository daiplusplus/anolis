using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Anolis.Installer.Properties;

namespace Anolis.Installer.Pages {
	public partial class BaseInteriorPage : W3b.Wizards.Wizard97.InteriorPage {
		
		public BaseInteriorPage() {
			InitializeComponent();
			
			base.__banner.Paint += new PaintEventHandler(__banner_Paint);
		}
		
		private void __banner_Paint(object sender, PaintEventArgs e) {
			
			Image anole = Resources.Banner;
			
			e.Graphics.DrawImage( anole, __banner.Width - anole.Width, __banner.Height - anole.Height );
		}
	}
}
