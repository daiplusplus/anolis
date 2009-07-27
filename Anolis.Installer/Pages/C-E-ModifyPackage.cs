using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Anolis.Core.Packages;
using Anolis.Core.Packages.Operations;
using W3b.Wizards.WindowsForms;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Installer.Pages {
	
	public partial class ModifyPackagePage : BaseInteriorPage {
		
		public ModifyPackagePage() {
			
			InitializeComponent();
			
			// the TreeView class doesn't support Indeterminate state for checkboxes (bummer, I know)
			// you can use an unhealthy mixture of OwnerDraw and subclassing to almost implement it (see CodeProject)
			// but for now, leave it as-is
			
			this.PageLoad += new EventHandler(ModifyPackagePage_Load);
			this.__packageView.AfterSelect    += new TreeViewEventHandler(__packageView_AfterSelect);
			this.__packageView.BeforeCollapse += new TreeViewCancelEventHandler(__packageView_BeforeCollapse);
			this.__packageView.AfterCheck     += new TreeViewEventHandler(__packageView_AfterCheck);
			
			Localize();
		}
		
		protected override String LocalizePrefix { get { return "C_E"; } }
		
#region UI Events
		
		private void __packageView_BeforeCollapse(object sender, TreeViewCancelEventArgs e) {
			// prevent collapsing of the root node
			if( e.Node.Parent == null ) e.Cancel = true;
		}
		
		private void __packageView_AfterSelect(object sender, TreeViewEventArgs e) {
			
			PackageItem item = e.Node.Tag as PackageItem;
			
			PopulatePackageItemInfo( item );
			
		}
		
		private void __packageView_AfterCheck(object sender, TreeViewEventArgs e) {
			
			PackageItem item = e.Node.Tag as PackageItem;
			if( item != null ) {
				item.Enabled = e.Node.Checked;
			}
			
			if( e.Action != TreeViewAction.Unknown ) {
				
				RefreshTreeView( e.Node.Nodes );
			}
			
		}
		
#endregion
		
		private void ModifyPackagePage_Load(object sender, EventArgs e) {
			
			WizardForm.EnableNext = true;
			
			PopulateTreeview();
			
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageCDReleaseNotes; }
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageCFInstallOpts; }
		}
		
		private void PopulateTreeview() {
			
			__packageView.Nodes.Clear();
			
			TreeNode root = PopulateTreeview( __packageView.Nodes, PackageInfo.Package.RootGroup);
			root.Expand();
			
			PopulatePackageItemInfo( root.Tag as PackageItem );
			
		}
		
		private TreeNode PopulateTreeview(TreeNodeCollection parent, PackageItem item) {
			
			if( item.Hidden ) return null;
			
			TreeNode node = new TreeNode( item.ToString() ) { Checked = item.Enabled, Tag = item };
			
			parent.Add( node );
			
			Group c = item as Group;
			if(c != null) {
				
				foreach(PackageItem set in c.Children)   PopulateTreeview(node.Nodes, set);
				foreach(Operation    op in c.Operations) PopulateTreeview(node.Nodes, op);
				
			}
			
			return node;
		}
		
		private void RefreshTreeView(TreeNodeCollection nodes) {
			
			foreach(TreeNode child in nodes) {
				
				PackageItem item = child.Tag as PackageItem;
				if( item != null ) child.Checked = item.Enabled;
				
				RefreshTreeView( child.Nodes );
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
			
			if( String.IsNullOrEmpty( item.Description ) ) {
				
				PathOperation po = item as PathOperation;
				
				if(po != null) {
					
					__infoLbl.Text = po.Path;
				} else {
					
					__infoLbl.Text = String.Format(Cult.CurrentCulture, InstallerResources.GetString("C_E_noInfo"), item.Name);
				}
				
			} else {
				
				__infoLbl.Text = item.Description;
				
			}
			
		}
		
	}
}
