using System;
using System.Windows.Forms;
using Anolis.Core.Packages;
using W3b.Wizards;
namespace Anolis.Gui.Pages {
	
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
			
			PopulateTreeview( __packageView.Nodes, null, PackageInfo.Package.Children);
			
		}
		
		private void PopulateTreeview(TreeNodeCollection parent, OperationCollection ops, SetCollection children) {
			
			if( ops != null )
			foreach(Operation op in ops) {
				
				TreeNode node = new TreeNode( op.Name ) { Checked = op.Enabled, Tag = op };
				parent.Add( node );
				
			}
			
			foreach(PackageItem item in children) {
				
				TreeNode node = new TreeNode( item.Name ) { Checked = item.Enabled, Tag = item };
				
				parent.Add( node );
				
				if( item is Set ) {
					
					PopulateTreeview( node.Nodes, (item as Set).Operations, (item as Set).Children );
				}
				
			}
			
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
				
				__infoPicture.Image   = item.DescriptionImage;
				__infoPicture.Visible = true;
				__infoLbl.Top         = __infoPicture.Bottom + 3;
			}
			
			__infoLbl.Text = item.Description == null ? Anolis.Gui.Properties.Resources.PageI_E_NoInfo : item.Description;
		}
		
	}
}
