using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Anolis.Core.Source;
using Cult = System.Globalization.CultureInfo;
using Anolis.Core.Packages;
using System.Reflection;

namespace Anolis.Packager {
	
	public partial class DistributorForm : Form {
		
		private Assembly     _originalInstaller;
		private List<String> _addPackages;
		
		public DistributorForm() {
			InitializeComponent();
			
			_addPackages = new List<String>();
			
			this.__packAdd       .Click += new EventHandler(__packAdd_Click);
			this.__packOrigBrowse.Click += new EventHandler(__packOrigBrowse_Click);
			this.__packOrigLoad  .Click += new EventHandler(__packOrigLoad_Click);
			this.__create        .Click += new EventHandler(__create_Click);
		}
		
#region UI Events
		
		private void __packAdd_Click(object sender, EventArgs e) {
			
			InstallerAddPackage();
		}
		
		private void __packOrigLoad_Click(object sender, EventArgs e) {
			
			InstallerLoad();
		}
		
		private void __create_Click(object sender, EventArgs e) {
			
			InstallerCreate();
		}
		
		private void __packOrigBrowse_Click(object sender, EventArgs e) {
			
			InstallerLoadPrompt();
		}
		
#endregion
		
#region Work
		
		private void InstallerAddPackage() {
			
			if( __ofdAnop.ShowDialog(this) == DialogResult.OK ) {
				
				_addPackages.AddRange( __ofdAnop.FileNames );
				
				//update the UI
				
				foreach(String s in __ofdAnop.FileNames) {
					
					if( InstallerPackageListItemExists( s ) ) continue;
					
					FileInfo file = new FileInfo( s );
					
					ListViewItem item = new ListViewItem();
					item.Text = Path.GetFileName( s );
					item.Tag  = file;
					item.SubItems.Add( "Add" );
					item.SubItems.Add( (file.Length / 1024).ToString(Cult.InvariantCulture) + "KB" );
					
					__packList.Items.Add( item );
				}
				
			}
			
		}
		
		private Boolean InstallerPackageListItemExists(String fileName) {
			
			foreach(ListViewItem item in __packList.Items) {
				
				FileInfo file = item.Tag as FileInfo;
				if( file == null ) continue;
				
				if( String.Equals( file.FullName, fileName, StringComparison.OrdinalIgnoreCase ) ) return true;
				
			}
			
			return false;
			
		}
		
		private void InstallerLoadPrompt() {
			
			if( __ofdInstaller.ShowDialog(this) == DialogResult.OK ) {
				
				__packOrigPath.Text = __ofdInstaller.FileName;
				
				InstallerLoad();
			}
			
		}
		
		private void InstallerLoad() {
			
			String fileName = __packOrigPath.Text;
			
			if( !File.Exists( fileName ) ) {
				
				MessageBox.Show(this, "The specified file does not exist", "Anolis Packager", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				
				return;
			}
			
			_originalInstaller = Assembly.LoadFile( fileName );
			
			EmbeddedPackage[] embeddedPackages = PackageUtility.GetEmbeddedPackages( _originalInstaller );
			
			foreach(EmbeddedPackage package in embeddedPackages) {
				
				Int32 length;
				
				using(Stream fs = PackageUtility.GetEmbeddedPackage( package )) {
					length = (Int32)fs.Length;
				}
				
				ListViewItem item = new ListViewItem();
				item.Tag = package;
				item.Text = package.Name;
				item.SubItems.Add("Embedded");
				item.SubItems.Add( (length / 1024).ToString(Cult.InvariantCulture) + "KB" );
				
				__packList.Items.Add( item );
				
			}
			
		}
		
		private void InstallerCreate() {
			
			if( __sfd.ShowDialog(this) != DialogResult.OK ) return;
			
			DistributionCreator.CreateDistribution( __sfd.FileName, __packOrigPath.Text, _addPackages.ToArray() );
			
		}
		
#endregion
		
	}
}
