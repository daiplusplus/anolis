using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public class BaseExteriorPage : ExteriorPage {
		
		// These 3 methods are a straight copy from BaseInteriorPage, ensure they're kept in sync
		
		protected virtual void Localize() {
			
			String title = InstallerResources.GetString( LocalizePrefix + '_' + "Title" );
			if(title != null) PageTitle = title;
			
			String subtitle = InstallerResources.GetString( LocalizePrefix + '_' + "Subtitle" );
			if(subtitle != null) PageSubtitle = subtitle;
			
			RightToLeft = InstallerResources.CurrentLanguage.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
			
			foreach(Control c in this.Controls) {
				
				RecurseLocalizeControl( c );
			}
			
			////////////////////
			
			if( WizardForm != null ) {
				
				WizardForm.BackText   = InstallerResources.GetString("Wiz_Prev");
				WizardForm.NextText   = InstallerResources.GetString("Wiz_Next");
				WizardForm.CancelText = InstallerResources.GetString("Wiz_Cancel");
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
		
	}
}
