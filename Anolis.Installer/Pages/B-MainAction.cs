using System;
using System.Drawing;

using Anolis.Packages;
using Anolis.Packages.Utility;

using W3b.Wizards;
using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public partial class MainActionPage : BaseInteriorPage {
		
		public MainActionPage() {
			InitializeComponent();
			
			this.PageUnload += new EventHandler<PageChangeEventArgs>(MainActionPage_PageUnload);
			
			this.Load += new EventHandler(MainActionPage_Load);
		}
		
		protected override String LocalizePrefix { get { return "B"; } }
		
		protected override void Localize() {
			base.Localize();
			
			if( InstallerResources.IsCustomized ) {
				
				__installRad    .Text = InstallerResources.GetString("B_installRad_Cus"    , InstallerResources.CustomizedSettings.InstallerFullName);
				__installBlurb  .Text = InstallerResources.GetString("B_installBlurb_Cus"  , InstallerResources.CustomizedSettings.InstallerName);
				__uninstallRad  .Text = InstallerResources.GetString("B_uninstallRad_Cus"  , InstallerResources.CustomizedSettings.InstallerFullName);
				__toolsBlurb    .Text = InstallerResources.GetString("B_toolsBlurb_Cus"    , InstallerResources.CustomizedSettings.InstallerName);
			}
			
		}
		
		private void MainActionPage_Load(object sender, EventArgs e) {
			
			__toolsRad    .Font = new Font( __toolsRad.Font, FontStyle.Bold );
			__installRad  .Font = new Font( __toolsRad.Font, FontStyle.Bold );
			__uninstallRad.Font = new Font( __toolsRad.Font, FontStyle.Bold );
		}
		
		private void MainActionPage_PageUnload(object sender, PageChangeEventArgs e) {
			
			if(e.PageToBeLoaded == Program.PageAWelcome) {
				InstallationInfo.ProgramMode = ProgramMode.None;
				return;
			}
			
			if( __installRad.Checked ) {
				
				InstallationInfo.ProgramMode = ProgramMode.InstallPackage;
				
			} else if( __uninstallRad.Checked ) {
				
				InstallationInfo.ProgramMode = ProgramMode.UninstallPackage;
				
			} else if( __toolsRad.Checked ) {
				
				InstallationInfo.ProgramMode = ProgramMode.InstallTools;
				
			} else {
				
				InstallationInfo.ProgramMode = ProgramMode.None;
			}
			
		}
		
		public override BaseWizardPage NextPage {
			get {
				
				if( __installRad.Checked ) {
					
					if( InstallerResources.IsCustomized && InstallerResources.CustomizedSettings.SimpleUI ) {
						
						SetDefaultPackage();
						
						return Program.PageCBExtracting;
					}
					
					return Program.PageCASelectPackage;
					
				} else if( __uninstallRad.Checked ) {
					
					return Program.PageEASelectBackup;
					
				} else if( __toolsRad.Checked ) {
					
					return Program.PageDADestination;
					
				} else {
					
					return null;
				}
			}
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageAWelcome; }
		}
		
		private void SetDefaultPackage() {
			
			EmbeddedPackage[] embedded = PackageUtility.GetEmbeddedPackages();
			
			// skip this page and load the first embedded package
			
			if( embedded.Length > 0 ) {
				
				EmbeddedPackage package = embedded[0] as EmbeddedPackage;
				
				System.IO.Stream stream = PackageUtility.GetEmbeddedPackage( package );
				
				PackageInfo.Source     = PackageSource.Embedded;
				PackageInfo.SourcePath = package.Name;
				PackageInfo.Archive    = PackageArchive.FromStream( package.Name, PackageSubclass.LzmaTarball, stream );
			}
			
		}
		
	}
}
