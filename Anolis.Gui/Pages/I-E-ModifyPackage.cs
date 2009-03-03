using System;
using System.Windows.Forms;
using Anolis.Core.Packages;
using W3b.Wizards;
namespace Anolis.Gui.Pages {
	
	public partial class ModifyPackagePage : BaseInteriorPage {
		
		public ModifyPackagePage() {
			
			InitializeComponent();
			
			this.Load += new EventHandler(Extracting_Load);
			
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
		
	}
}
