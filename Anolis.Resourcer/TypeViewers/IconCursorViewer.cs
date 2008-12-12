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
				
				case Win32ResourceType.CursorAnimated:
				case Win32ResourceType.CursorDeviceIndependent:
				case Win32ResourceType.IconAnimated:
				case Win32ResourceType.IconDeviceIndependent:
					
					return true;
				
			}
			
			return false;
			
		}
		
		public override String ViewerName {
			get { return "Icon and Cursor Viewer"; }
		}
	}
}
