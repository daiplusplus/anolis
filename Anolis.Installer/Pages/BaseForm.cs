using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace Anolis.Installer.Pages {
	
	public class BaseForm : Form {
		
		public BaseForm() {
			
			InstallerResources.CurrentLanguageChanged += new EventHandler(InstallerResources_CurrentLanguageChanged);
			
			// don't call Localize from this constructor because controls won't have been intiated by the child class
			// it is the concrete implementation's responsibility to call Localize
		}
		
		private void InstallerResources_CurrentLanguageChanged(Object sender, EventArgs e) {
			
			Localize();
		}
		
		protected virtual void Localize() {
			
			LocalizerHelper.Localize(LocalizePrefix, this);
		}
		
		protected virtual String LocalizePrefix {
			get { throw new NotImplementedException(); }
		}
		
	}
}
