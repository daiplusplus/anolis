using System;
using System.IO;
using System.Windows.Forms;
using Anolis.Core.Packages;
using W3b.Wizards;
namespace Anolis.Gui.Pages {
	
	public partial class SelectPackagePage : BaseInteriorPage {
		
		public SelectPackagePage() {
			InitializeComponent();
			
			this.Load += new EventHandler(SelectPackage_Load);
			this.PageUnload += new EventHandler<W3b.Wizards.PageChangeEventArgs>(SelectPackagePage_PageUnload);
			
			this.__optBrowseBrowse.Click += new EventHandler(__optBrowseBrowse_Click);
			
		}
		
		private void SelectPackage_Load(object sender, EventArgs e) {
			
			// Load the embedded list
			String[] embedded = PackageManager.GetEmbeddedPackages();
			__optPackagesList.Items.Clear();
			
			foreach(String name in embedded) {
				
				__optPackagesList.Items.Add( name );
				
			}
			
		}
		
		private void SelectPackagePage_PageUnload(object sender, W3b.Wizards.PageChangeEventArgs e) {
			
			// TODO: Error messages etc
			// UnloadEventArgs should have a .Cancel property which will be set to true if the user's info isn't valid
			
			if( __optPackages.Checked ) {
				
				String packageName = __optPackagesList.SelectedItem as String;
				
				Stream stream = PackageManager.GetEmbeddedPackage( GetType().Assembly, packageName );
				
				PackageInfo.Archive = PackageArchive.FromStream( packageName, PackageSubclass.LzmaTarball, stream );
				
			} else if( __optBrowse.Checked ) {
				
				String packageName = Path.GetFileNameWithoutExtension( __optBrowseFilename.Text );
				
				Stream stream = File.OpenRead( __optBrowseFilename.Text );
				
				PackageInfo.Archive = PackageArchive.FromStream( packageName, PackageSubclass.LzmaTarball, stream );
				
			}
			
		}
		
		private void __optBrowseBrowse_Click(Object sender, EventArgs e) {
			
			if( __ofd.ShowDialog(this) == DialogResult.OK ) {
				
				__optBrowseFilename.Text = __ofd.FileName;
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
