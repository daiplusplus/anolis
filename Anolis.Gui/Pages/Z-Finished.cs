using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards;

namespace Anolis.Installer.Pages {
	
	public partial class FinishedPage : W3b.Wizards.Wizard97.ExteriorPage {
		
		public FinishedPage() {
			
			this.WizardForm.EnablePrev = false;
			
			this.WizardForm.NextText = "Reboot";
			this.WizardForm.NextClicked += new EventHandler(WizardForm_NextClicked);
		}
		
		private void WizardForm_NextClicked(object sender, EventArgs e) {
			
			// shutdown the system
			// note that for shutdown to work you need to call AdjustTokenPrivileges to grant yourself the right to shut the system down
			// I think a wrapper method, maybe in my Environment class would be useful
			// there are 3 main shutdown functions: ExitWindows[Ex], InitiateSystemShutdown[Ex], and InitiateShutdown[Ex]
			// only ExitWindows works from Windows 95. InitiateSystemShutdown is NT onwards, and InitiateShutdown is NT6 onwards
			
		}
		
		public override BaseWizardPage PrevPage {
			get { return null; }
		}
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
	}
}
