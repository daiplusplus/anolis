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
			
			Package = PackageInfo.Package;
			
		}
		
		private Package Package { get; set; }
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageICSelectPackage; }
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageIFInstallationOptions; }
		}
		
		private void PopulateTreeview() {
			
			__packageView.Nodes.Clear();
			
			TreeNode root = new TreeNode("Package: " + Package.Name);
			
			__packageView.Nodes.Add( root );
			
			foreach(PackageItem item in Package.Children) {
				
				PopulateTreeview( root, item );
			}
			
		}
		
		private void PopulateTreeview(TreeNode parent, PackageItem item) {
			
			if( item is Set ) {
				
				TreeNode thisNode = new TreeNode( item.Name );
				thisNode.Checked = item.Enabled;
				
				parent.Nodes.Add( thisNode );
				
				foreach(PackageItem child in (item as Set).Children) {
					
					PopulateTreeview( thisNode, item );
				}
			}
			
		}
		
	}
}
