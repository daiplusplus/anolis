using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Anolis.Core;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class IconCursorViewer : TypeViewer {
		
		public IconCursorViewer() {
			InitializeComponent();
		}
		
		public override void RenderResource(ResourceData resource) {
			
			// TODO: Classes and methods for working with resource types
			
		}
		
		public override Boolean CanHandleResourceType(ResourceType type) {
			
			switch(type.Identifier.KnownType) {
				
				case KnownWin32ResourceType.CursorAnimated:
				case KnownWin32ResourceType.CursorDeviceIndependent:
				case KnownWin32ResourceType.IconAnimated:
				case KnownWin32ResourceType.IconDeviceIndependent:
					
					return true;
				
			}
			
			return false;
			
		}
		
		public override String ViewerName {
			get { return "Icon and Cursor Viewer"; }
		}
	}
}
