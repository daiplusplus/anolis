using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Anolis.Packages;
using Anolis.Packages.Operations;
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
			this.__packageView.NodeMouseClick += new TreeNodeMouseClickEventHandler(__packageView_NodeMouseClick);
			
			this.__simple.Click += new EventHandler(__simple_Click);
			
			this.__cmInherit.Click += new EventHandler(__cmmItem_Click);
			this.__cmEnable .Click += new EventHandler(__cmmItem_Click);
			this.__cmDisable.Click += new EventHandler(__cmmItem_Click);
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
			
			switch(e.Action) {
				case TreeViewAction.ByKeyboard:
				case TreeViewAction.ByMouse:
				case TreeViewAction.Collapse:
				case TreeViewAction.Expand:
					break;
				case TreeViewAction.Unknown:
				default:
					return;
			}
			
			// this is explicitly setting .Enabled on an item
			
			PackageItem item = e.Node.Tag as PackageItem;
			if( item != null ) {
				item.Enabled = e.Node.Checked;
				e.Node.ToolTipText = GetToolTipText( item );
			}
			
			if( e.Action != TreeViewAction.Unknown ) {
				
				RefreshTreeView( e.Node );
			}
			
		}
		
		private void __cmmItem_Click(object sender, EventArgs e) {
			
			TreeNode node = __cm.Tag as TreeNode;
			if( node == null ) return;
			
			PackageItem item = node.Tag as PackageItem;
			
			if( sender == __cmEnable  ) item.Enabled = true;
			if( sender == __cmDisable ) item.Enabled = false;
			if( sender == __cmInherit ) item.Enabled = null;
			
			RefreshTreeView( node );
		}
		
		private void __packageView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
			
			if( e.Button != MouseButtons.Right ) return;
			if( e.Node   == null               ) return;
			
			__cm.Tag = e.Node;
			__cm.Show( __packageView, e.Location);
		}
		
		private void __simple_Click(object sender, EventArgs e) {
			InstallationInfo.UseSelector = true;
			WizardForm.LoadPage( Program.PageCE1Selector );
		}
		
#endregion
		
		private void ModifyPackagePage_Load(object sender, EventArgs e) {
			
			__simple.Enabled = __simple.Visible = PackageInfo.Package.Presets.Count > 0;
			
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
			
			TreeNode node = new TreeNode() {
				Checked     = item.IsEnabled,
				Tag         = item,
				Text        = item.ToString(),
				ToolTipText = GetToolTipText( item )
			};
			
			parent.Add( node );
			
			Group c = item as Group;
			if(c != null) {
				
				foreach(PackageItem set in c.Children)   PopulateTreeview(node.Nodes, set);
				foreach(Operation    op in c.Operations) PopulateTreeview(node.Nodes, op);
				
			}
			
			return node;
		}
		
		private void RefreshTreeView(TreeNode node) {
			
			PackageItem item = node.Tag as PackageItem;
			if( item != null ) {
				node.Checked     = item.IsEnabled;
				node.ToolTipText = GetToolTipText( item );
			} else {
				node.ToolTipText = G("NullItem");
			}
			
			foreach(TreeNode child in node.Nodes) {
				
				RefreshTreeView( child );
			}
			
		}
		
		private String G(String name) {
			
			return InstallerResources.GetString( LocalizePrefix + "_" + name );
		}
		
		private String GetToolTipText(PackageItem item) {
			
			String nameStr      = item.Name;
			String enabledStr   = item.IsEnabled       ? G("Enabled") : G("Disabled");
			String inheritedStr = item.Enabled == null ? G("Inherit") : G("Explicit");
			
			if( String.IsNullOrEmpty( nameStr ) ) {
				if( item is Group ) nameStr = G("UnnamedGroup");
				Operation op = item as Operation;
				if( op != null ) nameStr = G("Unnamed") + " " + op.OperationName;
			}
			
			return nameStr + " (" + enabledStr + ", " + inheritedStr + ")";
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
