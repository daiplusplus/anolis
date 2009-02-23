using System;
using System.IO;
using System.Windows.Forms;
using Anolis.Core.Packages;

namespace Anolis.Gui.Pages {
	
	public partial class SelectPackagePage : BaseInteriorPage {
		
		public SelectPackagePage() {
			InitializeComponent();

			this.Load += new EventHandler(SelectPackage_Load);
			// TODO: An event handler for when the next page is loaded
			
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
		
		private void SelectPackage_UnloadNext(Object sender, EventArgs e) {
			
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
		
		public override W3b.Wizards.WizardPage NextPage {
			get { return Program.PageIDExtracting; }
		}
		
		public override W3b.Wizards.WizardPage PrevPage {
			get { return Program.PageBMainAction; }
		}
		
	}
}
