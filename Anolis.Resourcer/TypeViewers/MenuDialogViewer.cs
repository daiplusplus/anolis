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
	
	public partial class MenuDialogViewer : TypeViewer {
		
		public MenuDialogViewer() {
			InitializeComponent();
		}
		
		public override void RenderResource(ResourceData resource) {
			
			
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			return TypeViewerCompatibility.None;
		}
		
		public override string ViewerName {
			get { return "Menu Viewer"; }
		}
		
	}
}
