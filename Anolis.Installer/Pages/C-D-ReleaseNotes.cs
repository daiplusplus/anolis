using System;

using W3b.Wizards.WindowsForms;
using Anolis.Core.Packages;


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
			
			__packageTab.Text = InstallerResources.GetString("C_D_packageNotes", PackageInfo.Package.Name);
		}
		
		protected override String LocalizePrefix {
			get { return "C_D"; }
		}
		
		protected override void Localize() {
			base.Localize();
			
			if( InstallerResources.IsCustomized ) {
				
				PageSubtitle = InstallerResources.GetString("C_D_Subtitle_Cus", InstallerResources.CustomizedSettings.InstallerName);
			}
			
			__installerTab.Text = InstallerResources.GetString("C_D_installerNotes");
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageCCUpdatePackage; }
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageCEModifyPackage; }
		}
		
	}
}
