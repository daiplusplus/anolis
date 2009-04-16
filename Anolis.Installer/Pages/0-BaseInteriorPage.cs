using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Anolis.Installer.Properties;

using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public class BaseInteriorPage : InteriorPage {
		
		private static Bitmap _bannerImage = GetBannerImage();
		
		private static Bitmap GetBannerImage() {
			return Resources.Banner;
		}
		
		public BaseInteriorPage() {
			
			BannerImage = _bannerImage;
			
		}
	}
}
