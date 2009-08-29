using System;
using System.Collections.Generic;
using System.Text;

using W3b.Wizards;
using System.Windows.Forms;

namespace Anolis.Installer {
	
	internal static class LocalizerHelper {
		
		public static void Localize(String localizePrefix, IWizardPage page) {
			
			String title = InstallerResources.GetString( localizePrefix + "_Title" );
			if(title != null) page.PageTitle = title;
			
			String subtitle = InstallerResources.GetString( localizePrefix + "_Subtitle" );
			if(subtitle != null) page.PageSubtitle = subtitle;
			
			////////////////////
			
			if( page.WizardForm != null ) {
				
				page.WizardForm.BackText   = InstallerResources.GetString("Wiz_Prev");
				page.WizardForm.NextText   = InstallerResources.GetString("Wiz_Next");
				page.WizardForm.CancelText = InstallerResources.GetString("Wiz_Cancel");
			}
			
			////////////////////
			
			Control control = page as Control;
			if( control != null ) {
				
				RecurseLocalizeControl( localizePrefix, control );
				
			}
			
		}
		
		public static List<String> nulls = new List<String>();
		
		public static void Localize(String localizePrefix, Form form) {
			
			form.Text = InstallerResources.GetString( localizePrefix + "_Title");
			
			form.RightToLeft = InstallerResources.CurrentLanguage.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
			
			RecurseLocalizeControl( localizePrefix, form );
			
		}
		
		private static void RecurseLocalizeControl(String localizePrefix, Control c) {
			
			String key = localizePrefix + '_' + c.Name.Replace("__", "");
			
			String text = InstallerResources.GetString( key );
			if( text != null ) c.Text = text;
			else nulls.Add( key + "\t" + c.GetType().Name );
			
			foreach(Control child in c.Controls)
				RecurseLocalizeControl( localizePrefix, child );
			
		}
		
	}
}
