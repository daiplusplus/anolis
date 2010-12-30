using System;

using W3b.Wizards.WindowsForms;
using Anolis.Packages;


namespace Anolis.Installer.Pages {
	
	public partial class ReleaseNotesPage : BaseInteriorPage {
		
		public ReleaseNotesPage() {
		
			InitializeComponent();
			
			this.PageLoad += new EventHandler(ReleaseNotesPage_PageLoad);
			
			this.__packageRtf.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(LinkClicked);
			this.__installerRtf.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(LinkClicked);
		}
		
		private void LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e) {
			
			if( e.LinkText.StartsWith("http")) System.Diagnostics.Process.Start( e.LinkText );
		}
		
		private void ReleaseNotesPage_PageLoad(object sender, EventArgs e) {
			
			Package package = PackageInfo.Package;
			
			String packageNotes = package.ReleaseNotes;
			String installerNotes = InstallerResources.GetString("EULA");
			
			__packageRtf  .Rtf = packageNotes;
			__installerRtf.Rtf = installerNotes;
			
			__packageTab.Text = InstallerResources.GetString("C_D_packageNotes", PackageInfo.Package.Name);
			
			// TODO: Hide the package tab if it has no notes. This isn't a trivial operation, the tab has to be physically removed or re-added
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
			
			// the release notes should never have RTL set to true as it's often in English
			__installerRtf.RightToLeft = System.Windows.Forms.RightToLeft.No;
			__packageRtf.RightToLeft   = System.Windows.Forms.RightToLeft.No;
		}
		
		public override BaseWizardPage PrevPage {
			get {
				// TODO problem: if the package is not updatable then the UpdatePackage page loads the ReleaseNotes page during its Page_Load event handler
				return Program.PageCCUpdatePackage;
			}
		}
		
		public override BaseWizardPage NextPage {
			get {
				if( InstallationInfo.UseSelector.Value ) return Program.PageCE1Selector;
				return Program.PageCE2ModifyPackage;
			}
		}
		
	}
}
