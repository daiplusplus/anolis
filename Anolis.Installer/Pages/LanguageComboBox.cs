using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace Anolis.Installer.Pages {
	
	public class LanguageComboBox : ComboBox {
		
		public LanguageComboBox() : base() {
			
			DrawMode      = DrawMode.OwnerDrawFixed;
			DropDownStyle = ComboBoxStyle.DropDownList;
		}
		
		private Boolean _isPopulated;
		
		protected override void OnCreateControl() {
			base.OnCreateControl();
			
			if( DesignMode ) return;
			
			if( !_isPopulated ) {
				
				Items.Clear();
			
				InstallerResourceLanguage[] languages = InstallerResources.GetAvailableLanguages();
				Items.AddRange( languages );
				SelectedItem = InstallerResources.CurrentLanguage;
				
				_isPopulated = true;
			}
			
		}
		
		protected override void OnDrawItem(DrawItemEventArgs e) {
			
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
		
		protected override void OnSelectedIndexChanged(EventArgs e) {
			base.OnSelectedIndexChanged(e);
			
			InstallerResourceLanguage lang = SelectedItem as InstallerResourceLanguage;
			
			InstallerResources.CurrentLanguage = lang;
			
		}
		
	}
}
