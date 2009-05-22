#define NativeDialogs

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Anolis.Core.Data;
using Anolis.Core.Utility;
using System.Collections.Generic;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class MenuDialogViewer : TypeViewer {
		
		private MenuDialogHelperForm _form;
		
		public MenuDialogViewer() {
			InitializeComponent();
			
			this.__tOpen.CheckedChanged += new EventHandler(__tOpen_CheckedChanged);
			this.__itemsTree.AfterSelect += new TreeViewEventHandler(__itemsTree_AfterSelect);
			this.VisibleChanged += new EventHandler(MenuDialogViewer_VisibleChanged);
		}
		
		private	void __itemsTree_AfterSelect(object sender, TreeViewEventArgs e) {
			
			TreeNode node = e.Node;
			
			__properties.SelectedObject = node.Tag as DialogMenuItem;
			
		}
		
		private void MenuDialogViewer_VisibleChanged(object sender, EventArgs e) {
			
			// this is fired when the Viewer is removed from ResourceDataView
			
			if(!this.Visible && _form != null ) {
				
//				_form.Visible = false;
			}
		}
		
		private void __tOpen_CheckedChanged(object sender, EventArgs e) {
			
//#if !NativeDialogs
			if(_form != null) _form.Visible = __tOpen.Checked;
//#endif
		}
		
		public override void RenderResource(ResourceData resource) {
			
			if(resource is DialogResourceData) RenderDialog( resource as DialogResourceData );
			
			if(resource is MenuResourceData)   RenderMenu( resource as MenuResourceData );
			
		}
		
		private void RenderDialog(DialogResourceData d) {
			
			
			
#if NativeDialogs
			RenderDialogNative(d);
#else
			_form = new MenuDialogHelperForm();
			_form.LoadDialog( d.Dialog );
			
			_form.Show(this);
#endif
			
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
			
			if( _form == null ) _form = new MenuDialogHelperForm();
			_form.LoadMenu( m.Menu );
			
			if( !_form.Visible ) _form.Show(this);
			
			//////////////////////////////////
			// Populate tree view
			
			__itemsTree.Nodes.Clear();
			
			PopulateMenuTreeView( m.Menu.Root, __itemsTree.Nodes );
			
			__itemsTree.ExpandAll();
			
		}
		
		private void PopulateMenuTreeView(DialogMenuItem item, TreeNodeCollection nodes) {
			
			foreach(DialogMenuItem child in item.Children) {
				
				TreeNode node = new TreeNode( child.Text );
				nodes.Add( node );
				node.Tag = child;
				
				PopulateMenuTreeView( child, node.Nodes );
				
			}
			
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			
			return (data is DialogResourceData || data is MenuResourceData) ? TypeViewerCompatibility.Ideal : TypeViewerCompatibility.None;
			
		}
		
		public override string ViewerName {
			get { return "Dialog and Menu Viewer"; }
		}
		
#region Native Dialogs
		
	#if NativeDialogs
		
		private void RenderDialogNative(Object o) {
			
			DialogResourceData d = o as DialogResourceData;
			
			IntPtr p = Marshal.AllocHGlobal( d.RawData.Length );
			Marshal.Copy( d.RawData, 0, p, d.RawData.Length );
			
			NativeMethods.DialogProc dialogProc = new NativeMethods.DialogProc( DialogProc );
			
			IntPtr hInstance = Marshal.GetHINSTANCE( typeof(MenuDialogHelperForm).Module );
			
			IntPtr hWndDialog = NativeMethods.CreateDialogIndirectParam( hInstance, p, this.Handle, dialogProc, IntPtr.Zero );
			
			Marshal.FreeHGlobal( p );
			
		}
		
		private Boolean DialogProc(IntPtr hWndDialog, UInt32 msg, UIntPtr wParam, IntPtr lParam) {
			switch(msg) {
				
				case 0x10: // WM_CLOSE
					
					NativeMethods.EndDialog( hWndDialog, IntPtr.Zero );
					
					return true;
				default:
					return false;
			}
		}
		
	#endif
		
#endregion
		
	}
}
