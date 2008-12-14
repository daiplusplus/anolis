using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Anolis.Core;
using Anolis.Core.Data;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class IconCursorViewer : TypeViewer {
		
		public IconCursorViewer() {
			InitializeComponent();
		}
		
		public override void RenderResource(ResourceData resource) {
			
			// TODO: Classes and methods for working with resource types
			
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			
			if( data is DirectoryResourceData ) return TypeViewerCompatibility.Ideal;
			
			return TypeViewerCompatibility.None;
			
		}
		
		public override String ViewerName {
			get { return "Icon and Cursor Viewer"; }
		}
	}
}
