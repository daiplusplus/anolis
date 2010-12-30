using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

using Anolis.Packages;
using Anolis.Packages.Utility;

using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public partial class SelectPackagePage : BaseInteriorPage {
		
		public SelectPackagePage() {
			InitializeComponent();
			
			this.Load += new EventHandler(SelectPackage_Load);
			this.PageUnload += new EventHandler<W3b.Wizards.PageChangeEventArgs>(SelectPackagePage_PageUnload);
			
			this.__anopBrowse.Click += new EventHandler(__anopBrowse_Click);
			this.__packBrowse.Click += new EventHandler(__packBrowse_Click);
		}
		
		protected override String LocalizePrefix { get { return "C_A"; } }
		
		private void SelectPackage_Load(object sender, EventArgs e) {
			
			// Load the embedded list
			EmbeddedPackage[] embedded = PackageUtility.GetEmbeddedPackages();
			__embedList.Items.Clear();
			
			foreach(EmbeddedPackage package in embedded) {
				
				__embedList.Items.Add( package );
				
			}
		}
		
		private void SelectPackagePage_PageUnload(object sender, W3b.Wizards.PageChangeEventArgs e) {
						
			if( InstallerResources.IsCustomized && InstallerResources.CustomizedSettings.SimpleUI ) return;
			
			if( e.PageToBeLoaded == Program.PageBMainAction ) return;
			
			if( __embedRad.Checked ) {
				
				if( __embedList.SelectedItem == null ) {
					
					MessageBox.Show(this, InstallerResources.GetString("C_A_selectEmbeddedPackageFirst"), "Anolis Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					e.Cancel = true;
					return;
				}
				
				EmbeddedPackage package = __embedList.SelectedItem as EmbeddedPackage;
				
				Stream stream = PackageUtility.GetEmbeddedPackage( package );
				
				PackageInfo.Source     = PackageSource.Embedded;
				PackageInfo.SourcePath = package.Name;
				PackageInfo.Archive    = PackageArchive.FromStream( package.Name, PackageSubclass.LzmaTarball, stream );
			
			} else if( __packRad.Checked ) {
				
				if( !File.Exists( __packFilename.Text ) ) {
					
					String message = String.Format(CultureInfo.InvariantCulture, InstallerResources.GetString("C_A_notFileExists"), __anopFilename.Text );
					MessageBox.Show(this, message, "Anolis Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					e.Cancel = true;
					return;
				}
				
				String packageName = new DirectoryInfo( Path.GetDirectoryName( __packFilename.Text ) ).Name;
				
				PackageInfo.Source     = PackageSource.File;
				PackageInfo.SourcePath = __packFilename.Text;
			
			} else if( __anopRad.Checked ) {
				
				if( !File.Exists( __anopFilename.Text ) ) {
					
					String message = String.Format(CultureInfo.InvariantCulture, InstallerResources.GetString("C_A_notFileExists"), __anopFilename.Text );
					MessageBox.Show(this, message, "Anolis Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					e.Cancel = true;
					return;
				}
				
				String packageName = Path.GetFileNameWithoutExtension( __anopFilename.Text );
				
				Stream stream = File.OpenRead( __anopFilename.Text );
				
				PackageInfo.Source     = PackageSource.Archive;
				PackageInfo.SourcePath = __anopFilename.Text;
				PackageInfo.Archive    = PackageArchive.FromStream( packageName, PackageSubclass.LzmaTarball, stream );
			}
			
		}
		
		private void __anopBrowse_Click(Object sender, EventArgs e) {
			
			__ofd.Filter = "Anolis Package (*.anop)|*.anop|All files (*.*)|*.*";
			
			if( __ofd.ShowDialog(this) == DialogResult.OK ) {
				
				__anopRad.Checked = true;
				
				__anopFilename.Text = __ofd.FileName;
			}
			
		}
		
		private void __packBrowse_Click(object sender, EventArgs e) {
			
			__ofd.Filter = "Anolis Package XML (package.xml)|*.xml|All files (*.*)|*.*";
			
			if( __ofd.ShowDialog(this) == DialogResult.OK ) {
				
				__packRad.Checked = true;
				
				__packFilename.Text = __ofd.FileName;
			}
			
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageCBExtracting; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageBMainAction; }
		}
		
	}
}
