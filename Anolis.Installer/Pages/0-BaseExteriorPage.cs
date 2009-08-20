using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public class BaseExteriorPage : ExteriorPage {
		
		// Ensure this class is kept in sync with BaseInternalBase
		
		protected BaseExteriorPage() {
			
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
		
	}
}
