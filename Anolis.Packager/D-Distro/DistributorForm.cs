using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

using Anolis.Core.Source;
using Cult = System.Globalization.CultureInfo;
using Anolis.Core.Packages;
using System.Drawing;


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
			
			this.__cusImagesBanner   .Click += new EventHandler(__cusImagesBanner_Click);
			this.__cusImagesWatermark.Click += new EventHandler(__cusImagesWatermark_Click);
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
		
		//////////////////////////////////////////////
		
		private void __cusImagesWatermark_Click(object sender, EventArgs e) {
			
			if( __ofdImage.ShowDialog(this) == DialogResult.OK ) {
				
				__cusImagesWatermark.Tag = __ofdImage.FileName;
				__cusImagesWatermark.Load( __ofdImage.FileName );
			}
			
		}
		
		private void __cusImagesBanner_Click(object sender, EventArgs e) {
			
			if( __ofdImage.ShowDialog(this) == DialogResult.OK ) {
				
				__cusImagesBanner.Tag = __ofdImage.FileName;
				__cusImagesBanner.Load( __ofdImage.FileName );
			}
			
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
			
			List<String> resources = new List<String>();
			resources.AddRange( _addPackages );
			
			String cus = InstallerCreateCustomizationResource();
			if( cus != null ) resources.Add( cus );
			
			DistributionCreator.CreateDistribution( __sfd.FileName, __packOrigPath.Text, resources.ToArray() );
			
		}
		
		private String InstallerCreateCustomizationResource() {
			
			if( _originalInstaller == null ) return null;
			
			String customizerFn = Path.Combine( Path.GetDirectoryName( _originalInstaller.Location ), "Anolis.Installer.Customizer.resources" );
			
			ResourceWriter wtr = new ResourceWriter(customizerFn);
			
			//////////////////////////////////////////
			
			if( __cusImagesWatermark.Image != null ) {
				
				String path = __cusImagesWatermark.Tag.ToString();
				Image image = Image.FromFile( path );
				wtr.AddResource("Background", image );
			}
			
			
			if( __cusImagesBanner.Image != null ) {
				
				String path = __cusImagesBanner.Tag.ToString();
				Image image = Image.FromFile( path );
				wtr.AddResource("Banner", image );
			}
			
			//////////////////////////////////////////
			
			if( __cusStringName.Text.Length > 0 )
				wtr.AddResource("Installer_Name", __cusStringName.Text );
			
			if( __cusStringNameFull.Text.Length > 0 )
				wtr.AddResource("Installer_NameFull", __cusStringNameFull.Text );
			
			if( __cusStringDeveloper.Text.Length > 0 )
				wtr.AddResource("Installer_Developer", __cusStringDeveloper.Text );
			
			if( __cusStringWebsite.Text.Length > 0 )
				wtr.AddResource("Installer_Website", __cusStringWebsite.Text );
			
			if( __cusStringCond.Text.Length > 0 )
				wtr.AddResource("Installer_Condition", __cusStringCond.Text );
			
			if( __cusStringCondMsg.Text.Length > 0 )
				wtr.AddResource("Installer_ConditionMessage", __cusStringCondMsg.Text );
			
			//////////////////////////////////////////
			
			wtr.AddResource("Option_SimpleUI"           , __cusOptSimple.Checked );
			wtr.AddResource("Option_HideI386"           , __cusOptsHideI386.Checked );
			wtr.AddResource("Option_DisablePackageCheck", __cusOptsCheckDisable.Checked );
			wtr.AddResource("Option_DisableUpdateCheck" , __cusOptsCheckDisable.Checked );
			
			//////////////////////////////////////////
			
			wtr.Close(); // Close() calls Generate() via Disposing(true)
			
			return customizerFn;
		}
		
#endregion
		
	}
}
