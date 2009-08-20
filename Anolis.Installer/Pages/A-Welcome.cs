using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public partial class WelcomePage : BaseExteriorPage {
		
		public WelcomePage() {
			
			InitializeComponent();
			
			this.Load += new EventHandler(WelcomePage_Load);
			
			this.__cultureAttrib.Click += new EventHandler(__cultureAttrib_Click);
			this.__culture.SelectedIndexChanged += new EventHandler(__culture_SelectedIndexChanged);
		}
		
		protected override String LocalizePrefix {
			get { return "A"; }
		}
		
		protected override void Localize() {
			
			base.Localize();
			
			base.WatermarkImage     = InstallerResources.GetImage("Background");
			base.WatermarkAlignment = ContentAlignment.BottomLeft;
			base.WatermarkWidth     = WatermarkImage.Width; // 273;
			
			if( InstallerResources.IsCustomized ) {
				
				this.__title.Text = InstallerResources.GetString("A_Title_Cus", InstallerResources.CustomizedSettings.InstallerFullName );
				this.__notes.Text = InstallerResources.GetString("A_Notes_Cus", InstallerResources.CustomizedSettings.InstallerFullName );
			}
			
		}
		
		private void __cultureAttrib_Click(object sender, EventArgs e) {
			
			Uri uri = __cultureAttrib.Tag as Uri;
			if( uri != null ) {
				
				System.Diagnostics.Process.Start( uri.ToString() );
			}
			
		}
		
		private void __culture_SelectedIndexChanged(object sender, EventArgs e) {
			
			InstallerResourceLanguage lang = __culture.SelectedItem as InstallerResourceLanguage;
			__cultureAttrib.Text               = lang.Attribution;
			
			Uri uri;
			if( Uri.TryCreate( lang.AttributionUri, UriKind.Absolute, out uri ) ) {
				
				__cultureAttrib.LinkArea = new LinkArea(0, lang.Attribution.Length );
				__cultureAttrib.Tag       = uri;
			} else {
				
				__cultureAttrib.LinkArea  = new LinkArea(0, 0 );
				__cultureAttrib.Tag       = null;
			}
			
		}
		
		private void WelcomePage_Load(object sender, EventArgs e) {
			
			WizardForm.EnableNext = true;
			
			///////////////////////
			// Inform the user about the condition
			
			if( !PackageInfo.IgnoreCondition && !InstallationInfo.EvaluateInstallerCondition() ) {
				
				String message = InstallerResources.CustomizedSettings.InstallerConditionMessage;
				
				MessageBox.Show(this, message, "Anolis Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
			}
			
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageBMainAction; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return null; }
		}
		
	}
}
