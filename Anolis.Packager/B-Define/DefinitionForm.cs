using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

using Anolis.Core.Packages;
using Anolis.Core.Packages.Operations;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Packager {
	
	public partial class DefinitionForm : Form {
		
		private Package  _package;
		private FileInfo _packageFile;
		private Boolean  _isDirty;
		
		public DefinitionForm() {
			
			InitializeComponent();
			
			this.__tPackNew .Click += new EventHandler(__tPackNew_Click);
			this.__tPackOpen.Click += new EventHandler(__tPackOpen_Click);
			this.__tPackSave.Click += new EventHandler(__tPackSave_Click);

			this.__tToolsResourcer.Click += new EventHandler(__tToolsResourcer_Click);
			this.__tToolsCondition.Click += new EventHandler(__tToolsCondition_Click);
			
			this.__tree.BeforeCollapse += new TreeViewCancelEventHandler(__tree_BeforeCollapse);
			this.__tree.AfterSelect += new TreeViewEventHandler(__tree_AfterSelect);
			
			if( File.Exists("Anolis.Resourcer.exe") ) __tToolsResourcer.Enabled = true;
			
			
		}
		
#region UI Events
		
	#region Main Toolbar
		
		private void __tPackNew_Click(object sender, EventArgs e) {
			
		}
		
		private void __tPackOpen_Click(object sender, EventArgs e) {
			
			PackageLoadPrompt();
		}
		
		private void __tPackSave_Click(object sender, EventArgs e) {
			
			PackageSave();
		}
		
		private void __tToolsResourcer_Click(object sender, EventArgs e) {
			
			ToolsResourcer();
		}
		
		private void __tToolsCondition_Click(object sender, EventArgs e) {
			
			ToolsCondition();
		}
		
	#endregion
		
	#region Tree View
		
		private void __tree_BeforeCollapse(object sender, TreeViewCancelEventArgs e) {
			
			// prevent collapse of root node
			if( e.Node.Parent == null ) e.Cancel = true;
			
		}
		
		private void __tree_AfterSelect(object sender, TreeViewEventArgs e) {
			
			PackageItem item = e.Node.Tag as PackageItem;
			
			if( item != null ) PackageItemShow( item );
			
		}
		
	#endregion
		
#endregion
		
#region Work
		
	#region Package and Package UI-related Operations
		
		private void PackageLoadPrompt() {
			
			if( !PackageUnloadPrompt() ) return;
			
			if( __ofd.ShowDialog(this) == DialogResult.OK ) {
				
				PackageLoad( __ofd.FileName );
				
			}
			
		}
		
		private void PackageLoad(String xmlFileName) {
			
			try {
				
				_package = Package.FromFile( xmlFileName );
				
			} catch(PackageValidationException pve) {
				
				String message = "encountered when attempting to load the package from the file. You will need to manually correct these errors before continuing";
				
				if( pve.ValidationErrors.Count == 1 ) {
					message = "1 error was " + message;
				} else {
					message = pve.ValidationErrors.Count.ToString(Cult.CurrentCulture) + " errors were " + message;
				}
				
				MessageBox.Show(this, message, "Anolis Packager", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				
				return;
				
			}
			
			PackageUIPopulate();
			
		}
		
		/// <summary>If a package is currently open with unsaved changes it prompts the user to save, continue or cancel. If cancelled this method returns false, otherwise true.</summary>
		private Boolean PackageUnloadPrompt() {
			
			if( _package != null && _isDirty ) {
				
				DialogResult result = MessageBox.Show(this, "Save changes to package?", "Anolis Packager", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
				if( result == DialogResult.Cancel ) return false;
				
				if( result == DialogResult.Yes && _isDirty ) PackageSave();
				
			}
			
			return true;
		}
		
		private Boolean PackageSaveSfd() {
			
			if( __sfd.ShowDialog(this) == DialogResult.OK ) {
				
				_packageFile = new FileInfo( __sfd.FileName );
				
				return true;
				
			} else return false;
			
		}
		
		private void PackageSave() {
			
			if( _package == null ) return;
			
			if( _packageFile == null ) {
				
				if(!PackageSaveSfd()) return;
				
			}
			
			_package.Write( _packageFile.FullName );
			
			_isDirty = false;
			
		}
		
		private void PackageUIPopulate() {
			
			__tPackSave.Enabled = true;
			
			PackageUIPopulateTree();
			
		}
		
		private void PackageUIPopulateTree() {
			
			__tree.BeginUpdate();
			__tree.Nodes.Clear();
			
			TreeNode root = new TreeNode() { Text = "Package" };
			__tree.Nodes.Add( root );
			
			PackageUIPopulateTreeRec( root, _package.RootGroup );
			
			__tree.ExpandAll();
			
			__tree.EndUpdate();
			
		}
		
		private void PackageUIPopulateTreeRec(TreeNode parent, Group group) {
			
			TreeNode node = new TreeNode();
			node.Text = "Group: " + group.Id + " - " + group.Name;
			node.Tag  = group;
			
			parent.Nodes.Add( node );
			
			foreach(Operation op in group.Operations) {
				
				TreeNode opNode = new TreeNode();
				opNode.Text = op.OperationName + " - " + op.Id + " - " + op.Name;
				opNode.Tag  = op;
				
				node.Nodes.Add( opNode );
				
			}
			
			foreach(Group child in group.Children) {
				
				PackageUIPopulateTreeRec( node, child );
				
			}
			
		}
		
	#endregion
		
		private void PackageItemShow(PackageItem item) {
			
			__properties.SelectedObject = item;
		}
		
		private void ToolsResourcer() {
			
			if( File.Exists("Anolis.Resourcer.exe") ) {
				
				System.Diagnostics.Process.Start("Anolis.Resourcer.exe");
				
			}
			
		}
		
		private void ToolsCondition() {
			
			ConditionEditor ed = new ConditionEditor();
			ed.Show(this);
			
		}
		
#endregion
		
		
	}
}
