using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards.WindowsForms;
using Anolis.Core.Packages;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Installer.Pages {
	
	public partial class ReleaseNotesPage : BaseInteriorPage {
		
		public ReleaseNotesPage() {
		
			InitializeComponent();
			
			this.PageLoad += new EventHandler(ReleaseNotesPage_PageLoad);
		}
		
		private void ReleaseNotesPage_PageLoad(object sender, EventArgs e) {
			
			Package package = PackageInfo.Package;
			
			String packageNotes = package.ReleaseNotes;
			String installerNotes = InstallerResources.GetString("EULA");
			
			__packageRtf  .Rtf = packageNotes;
			__installerRtf.Rtf = installerNotes;
			
		}
		
		protected override String LocalizePrefix {
			get { return "C_D"; }
		}
		
		protected override void Localize() {
			base.Localize();
			
			String packageName = PackageInfo.Package == null ? "" : PackageInfo.Package.Name;
			
			this.__packageTab  .Text = String.Format(Cult.InvariantCulture, InstallerResources.GetString("C_D_packageNotes"), packageName);
			this.__installerTab.Text = InstallerResources.GetString("C_D_installerNotes");
			
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageCCUpdatePackage; }
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageCEModifyPackage; }
		}
		
	}
}
