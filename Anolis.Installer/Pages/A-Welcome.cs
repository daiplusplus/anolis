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
			
			this.__culture.DrawMode   = DrawMode.OwnerDrawFixed;
//			this.__culture.ItemHeight = TextRenderer.MeasureText("Lqpd", SystemFonts.DialogFont).Height + 3;
			this.__culture.DrawItem += new DrawItemEventHandler(__culture_DrawItem);
			
			this.__cultureAttrib.Click += new EventHandler(__cultureAttrib_Click);
			
			this.__culture.SelectedIndexChanged += new EventHandler(__culture_SelectedIndexChanged);
			
			InstallerResources.CurrentLanguageChanged += new EventHandler(InstallerResources_CurrentLanguageChanged);
			
			Localize();
		}
		
		protected override String LocalizePrefix {
			get { return "A"; }
		}
		
		private void __cultureAttrib_Click(object sender, EventArgs e) {
			
			Uri uri = __cultureAttrib.Tag as Uri;
			if( uri != null ) {
				
				System.Diagnostics.Process.Start( uri.ToString() );
			}
			
		}
		
		private void __culture_SelectedIndexChanged(object sender, EventArgs e) {
			
			InstallerResourceLanguage lang = __culture.SelectedItem as InstallerResourceLanguage;
			
			InstallerResources.CurrentLanguage = lang;
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
		
		private void InstallerResources_CurrentLanguageChanged(object sender, EventArgs e) {
			Localize();
		}
		
		private void __culture_DrawItem(object sender, DrawItemEventArgs e) {
			
			if( e.Index == -1 ) return;
			
			InstallerResourceLanguage[] langs = InstallerResources.GetAvailableLanguages();
			InstallerResourceLanguage lang = langs[ e.Index ];
			
			e.DrawBackground();
			
			Graphics  g = e.Graphics;
			Rectangle r = e.Bounds;
			
			Int32 x = r.X + 2;
			Int32 y = r.Y + (r.Height / 2) - (lang.Flag.Height / 2);
			
			g.DrawImageUnscaled( lang.Flag, x, y );
			
			Size ts = TextRenderer.MeasureText(g, lang.LanguageName, SystemFonts.IconTitleFont);
			
			Int32 tx = r.X + lang.Flag.Width + 3;
			Int32 ty = r.Y + (r.Height / 2) - (ts.Height / 2); // TODO: align by baseline, not by the rendered string size
			
// this code works, but it does make the list look funny
//			if( lang.RightToLeft ) {
//				tx = r.Width - ts.Width - 3;
//			}
			
			TextRenderer.DrawText( g, lang.LanguageName, SystemFonts.IconTitleFont, new Point( tx, ty ), e.ForeColor );
			
			e.DrawFocusRectangle();
			
		}
		
		protected override void Localize() {
			
			base.Localize();
			
			base.WatermarkImage     = InstallerResources.GetImage("Background");
			base.WatermarkAlignment = ContentAlignment.BottomLeft;
			base.WatermarkWidth     = WatermarkImage.Width; // 273;
			
			if( InstallerResources.IsCustomized ) {
				
				this.__title.Text = InstallerResources.GetString("A_Title_Cus"  , InstallerResources.CustomizedSettings.InstallerFullName );
				this.__inst1.Text = InstallerResources.GetString("A_Message_Cus", InstallerResources.CustomizedSettings.InstallerFullName );
				
			} else {
				
				this.__title.Text = InstallerResources.GetString("A_Title");
				this.__inst1.Text = InstallerResources.GetString("A_Message");
			}
			
		}
		
		private void WelcomePage_Load(object sender, EventArgs e) {
			
			WizardForm.EnableNext = true;
			
			///////////////////////
			
			__culture.Items.Clear();
			
			InstallerResourceLanguage[] languages = InstallerResources.GetAvailableLanguages();
			__culture.Items.AddRange( languages );
			
			__culture.SelectedItem = InstallerResources.CurrentLanguage;
			
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
