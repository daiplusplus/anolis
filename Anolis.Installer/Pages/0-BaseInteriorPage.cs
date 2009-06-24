using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public class BaseInteriorPage : InteriorPage {
		
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
			
			String title = InstallerResources.GetString( LocalizePrefix + '_' + "Title" );
			if(title != null) PageTitle = title;
			
			String subtitle = InstallerResources.GetString( LocalizePrefix + '_' + "Subtitle" );
			if(subtitle != null) PageSubtitle = subtitle;
			
			foreach(Control c in this.Controls) {
				
				RecurseLocalizeControl( c );
			}
			
		}
		
		private void RecurseLocalizeControl(Control c) {
			
			String key = LocalizePrefix + '_' + c.Name.Replace("__", "");
			
			String text = InstallerResources.GetString( key );
			if(text != null) c.Text = text;
			
			foreach(Control child in c.Controls)
				RecurseLocalizeControl( child );
			
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
	}
}
