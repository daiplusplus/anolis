using System;
using System.Windows.Forms;

using Anolis.Core.Data;
using Anolis.Core.Utility;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class MenuDialogViewer : TypeViewer {
		
		private MenuDialogHelperForm _form;
		
		public MenuDialogViewer() {
			InitializeComponent();

			this.__tOpen.CheckedChanged += new EventHandler(__tOpen_CheckedChanged);
			this.VisibleChanged += new EventHandler(MenuDialogViewer_VisibleChanged);
		}
		
		private void MenuDialogViewer_VisibleChanged(object sender, EventArgs e) {
			
			// this is fired when the Viewer is removed from ResourceDataView
			
			if(!this.Visible && _form != null ) {
				
				_form.Visible = false;
			}
		}
		
		private void __tOpen_CheckedChanged(object sender, EventArgs e) {
			
			_form.Visible = __tOpen.Checked;
		}
		
		public override void RenderResource(ResourceData resource) {
			
			if( _form != null ) { // if Resourcer is reusing this Viewer to show another dialog
				_form.Visible = false; // maybe allow an option to have several dialogs open simultaneously?
			}
			
			if(resource is DialogResourceData) RenderDialog( resource as DialogResourceData );
			
			if(resource is MenuResourceData)   RenderMenu( resource as MenuResourceData );
			
		}
		
		private void RenderDialog(DialogResourceData d) {
			
			_form = new MenuDialogHelperForm();
			_form.LoadDialog( d.Dialog );
			
			_form.Show(this);
			
			/////////////////////
			
			__itemsTree.BeginUpdate();
			__itemsTree.Nodes.Clear();
			
			TreeNode root = new TreeNode( "Dialog - \"" + d.Dialog.Text + "\"" );
			__itemsTree.Nodes.Add( root );
			
			foreach(DialogControl c in d.Dialog.Controls) {
				
				TreeNode n = new TreeNode( c.ToString() );
				root.Nodes.Add( n );
				
			}
			
			root.ExpandAll();
			
			__itemsTree.EndUpdate();
			
		}
		
		private void RenderMenu(MenuResourceData m) {
			
			_form = new MenuDialogHelperForm();
			_form.LoadMenu( m.Menu );
			
			_form.Show(this);
			
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			
			return (data is DialogResourceData || data is MenuResourceData) ? TypeViewerCompatibility.Ideal : TypeViewerCompatibility.None;
			
		}
		
		public override string ViewerName {
			get { return "Dialog and Menu Viewer"; }
		}
		
	}
}
