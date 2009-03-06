using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Anolis.Core.Packages;
using W3b.Wizards;

namespace Anolis.Installer.Pages {
	
	public partial class ModifyPackagePage : BaseInteriorPage {
		
		public ModifyPackagePage() {
			
			InitializeComponent();
			
			this.Load += new EventHandler(Extracting_Load);
			this.__packageView.AfterSelect += new TreeViewEventHandler(__packageView_AfterSelect);
			
		}
		
		private void __packageView_AfterSelect(object sender, TreeViewEventArgs e) {
			
			PackageItem item = e.Node.Tag as PackageItem;
			
			PopulatePackageItemInfo( item );
			
		}
		
		private void Extracting_Load(object sender, EventArgs e) {
			
			WizardForm.EnableNext = true;
			
			PopulateTreeview();
			
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageICSelectPackage; }
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageIFInstallationOptions; }
		}
		
		private void PopulateTreeview() {
			
			__packageView.Nodes.Clear();
			
			TreeNode root = PopulateTreeview( __packageView.Nodes, PackageInfo.Package);
			root.Expand();
			
		}
		
		private TreeNode PopulateTreeview(TreeNodeCollection parent, PackageItem item) {
			
			TreeNode node = new TreeNode( item.ToString() ) { Checked = item.Enabled, Tag = item };
			
			parent.Add( node );
			
			PackageItemContainer c = item as PackageItemContainer;
			if(c != null) {
				
				foreach(PackageItem set in c.Children)   PopulateTreeview(node.Nodes, set);
				foreach(Operation    op in c.Operations) PopulateTreeview(node.Nodes, op);
				
			}
			
			return node;
		}
		
		private void PopulatePackageItemInfo(PackageItem item) {
			
			if(item == null) {
				__infoPicture.Image = null;
				__infoLbl.Text      = null;
				return;
			}
			
			if(item.DescriptionImage == null) {
				
				__infoPicture.Visible = false;
				__infoLbl.Top         = __infoPicture.Top;
				
			} else {
				
				__infoPicture.Image   = GetPackageItemImage( item );
				__infoPicture.Visible = true;
				__infoLbl.Top         = __infoPicture.Bottom + 3;
			}
			
			__infoLbl.Text = item.Description == null ? Anolis.Installer.Properties.Resources.PageI_E_NoInfo : item.Description;
		}
		
		private Dictionary<String,Image> _packageItemImages = new Dictionary<String,Image>();
		
		private Image GetPackageItemImage(PackageItem item) {
			
			if( item.DescriptionImage == null ) return null;
			
			if( !_packageItemImages.ContainsKey(item.DescriptionImage) ) {
				
				try {
					Image i = Image.FromFile( System.IO.Path.Combine( PackageInfo.Package.RootDirectory.FullName, item.DescriptionImage ) );
					
					_packageItemImages.Add( item.DescriptionImage, i );
				} catch(OutOfMemoryException) {
					_packageItemImages.Add( item.DescriptionImage, null );
				} catch(System.IO.FileNotFoundException) {
					_packageItemImages.Add( item.DescriptionImage, null );
				}
				
			}
			
			return _packageItemImages[item.DescriptionImage];
			
		}
		
	}
}
