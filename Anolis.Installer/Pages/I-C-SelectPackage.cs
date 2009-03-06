using System;
using System.IO;
using System.Windows.Forms;
using Anolis.Core.Packages;
using W3b.Wizards;
namespace Anolis.Installer.Pages {
	
	public partial class SelectPackagePage : BaseInteriorPage {
		
		public SelectPackagePage() {
			InitializeComponent();
			
			this.Load += new EventHandler(SelectPackage_Load);
			this.PageUnload += new EventHandler<W3b.Wizards.PageChangeEventArgs>(SelectPackagePage_PageUnload);
			
			this.__anopBrowse.Click += new EventHandler(__anopBrowse_Click);
			this.__packBrowse.Click += new EventHandler(__packBrowse_Click);
			
		}
		
		private void SelectPackage_Load(object sender, EventArgs e) {
			
			// Load the embedded list
			String[] embedded = EmbeddedPackageManager.GetEmbeddedPackages();
			__embedList.Items.Clear();
			
			foreach(String name in embedded) {
				
				__embedList.Items.Add( name );
				
			}
			
		}
		
		private void SelectPackagePage_PageUnload(object sender, W3b.Wizards.PageChangeEventArgs e) {
			
			// TODO: Error messages etc
			// UnloadEventArgs should have a .Cancel property which will be set to true if the user's info isn't valid
			
			if( __embedRad.Checked ) {
				
				String packageName = __embedList.SelectedItem as String;
				
				Stream stream = EmbeddedPackageManager.GetEmbeddedPackage( GetType().Assembly, packageName );
				
				PackageInfo.Source     = PackageSource.Embedded;
				PackageInfo.SourcePath = packageName;
				PackageInfo.Archive = PackageArchive.FromStream( packageName, PackageSubclass.LzmaTarball, stream );
			
			} else if( __packRad.Checked ) {
				
				if( !File.Exists( __packFilename.Text ) ) {
					MessageBox.Show(this, "The file \"" + __packFilename.Text + "\" does not exist", "Anolis", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					e.Cancel = true;
					return;
				}
				
				String packageName = new DirectoryInfo( Path.GetDirectoryName( __packFilename.Text ) ).Name;
				
				PackageInfo.Source     = PackageSource.File;
				PackageInfo.SourcePath = __packFilename.Text;
			
			} else if( __anopRad.Checked ) {
				
				if( !File.Exists( __anopFilename.Text ) ) {
					MessageBox.Show(this, "The file \"" + __anopFilename.Text + "\" does not exist", "Anolis", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
			get { return Program.PageIDExtracting; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageBMainAction; }
		}
		
	}
}
