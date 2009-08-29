using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Core.Data;

using StringDict = System.Collections.Generic.Dictionary<string,string>;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class VersionViewer : TypeViewer {
		
		public VersionViewer() {
			InitializeComponent();
			
			FontFamily monospaceFam = FontFamily.GenericMonospace;
			__value.Font = new Font(monospaceFam, this.Font.SizeInPoints, FontStyle.Regular);
			
			__versionItems.AfterSelect += new TreeViewEventHandler(__versionItems_AfterSelect);
		}
		
		private void __versionItems_AfterSelect(object sender, TreeViewEventArgs e) {
			
			VersionItem v = e.Node.Tag as VersionItem;
			if(v == null) return;
			
			if( v.Value is String) __value.Text = (String)v.Value;
			else if( v.Value is Byte[] ) {
				
				StringBuilder sb = new StringBuilder();
				foreach(Byte b in v.Value as Byte[]) {
					
					sb.Append( b.ToString("X2") );
					sb.Append(' ');
				}
				
				__value.Text = sb.ToString();
				
			} else if( v.Value is StringDict ) {
				
				StringBuilder sb = new StringBuilder();
				
				StringDict d = v.Value as StringDict;
				foreach(String key in d.Keys) {
					
					sb.Append( key );
					sb.Append(" - ");
					sb.Append( d[key] );
					sb.Append("\r\n");
				}
				
				String t = sb.ToString();
				if(t.Length > 1) t = t.Left( t.Length - 1 );
				
				__value.Text = t;
				
			} else if(v.Value != null) {
				
				__value.Text = v.Value.ToString();
			} else {
				__value.Text = "ValueLength: " + v.ValueLength.ToString();
			}
			
		}
		
		public override void RenderResource(ResourceData resource) {
			
			__versionItems.Nodes.Clear();
			
			VersionResourceData vd = resource as VersionResourceData;
			if(vd == null) return;
			
			TreeNode ffi = new TreeNode( "VS_FIXEDFILEINFO" );
			ffi.Tag = vd.VSVersionInfo;
			
			__versionItems.BeginUpdate();
			
			__versionItems.Nodes.Add( ffi );
			
			foreach(VersionItem child in vd.VSVersionInfo.Children) {
				
				AddNode( __versionItems.Nodes, child );
			}
			
			__versionItems.ExpandAll();
			
			__versionItems.EndUpdate();
			
		}
		
		private void AddNode(TreeNodeCollection parentNode, VersionItem item) {
			
			String text;
			
			String mode = item.Mode.ToString();
			if( String.Equals( item.Key, mode, StringComparison.OrdinalIgnoreCase ) ) {
				
				text = item.Key;
			} else {
				
				text = mode + " - " + item.Key;
			}
			
			if(item.Value is String) text += " - \"" + (String)item.Value + "\"";
			else                     text += " - Binary";
			
			TreeNode n = new TreeNode( text ) { Tag = item };
			
			parentNode.Add( n );
			
			foreach(VersionItem child in item.Children) {
				
				AddNode( n.Nodes, child );
			}
			
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			
			if( data is VersionResourceData ) return TypeViewerCompatibility.Ideal;
			
			return TypeViewerCompatibility.None;
			
		}
		
		public override String ViewerName { get { return "Version Information"; } }
		
	}
}
