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
	
	public partial class VersionViewer : TypeViewer {
		
		public VersionViewer() {
			InitializeComponent();
		}
		
		// TODO: Implement one of those Tree Grids for the display of version information
		
		public override void RenderResource(ResourceData resource) {
			
			// NOOP for now
			
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			
			if( data is VersionResourceData ) return TypeViewerCompatibility.Ideal;
			
			return TypeViewerCompatibility.None;
			
		}
		
		public override String ViewerName { get { return "Version Information"; } }
		
		
	}
}
