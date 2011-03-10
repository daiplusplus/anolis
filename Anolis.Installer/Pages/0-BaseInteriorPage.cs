using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public /*abstract*/ class BaseInteriorPage : InteriorPage {
		
		private static Bitmap _bannerImage = GetBannerImage();
		
		private static Bitmap GetBannerImage() {
			return InstallerResources.GetImage("Banner") as Bitmap;
		}
		
		public BaseInteriorPage() {
			
			BannerImage = _bannerImage;
			
			InstallerResources.CurrentLanguageChanged += new EventHandler(InstallerResources_CurrentLanguageChanged);
		}
		
		private void InstallerResources_CurrentLanguageChanged(object sender, EventArgs e) {
			
			Localize();
		}
		
		protected virtual void Localize() {
			
			LocalizerHelper.Localize( LocalizePrefix, this );
		}
		
		protected virtual String LocalizePrefix {
			get { return String.Empty; }
		}
		
		private void InitializeComponent() {
			this.SuspendLayout();
			// 
			// BaseInteriorPage
			// 
			this.Name = "BaseInteriorPage";
			this.ResumeLayout(false);
		}
		
#if DEBUG
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return null; }
		}
		
#endif
		
	}
}
