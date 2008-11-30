using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Anolis.Core;
using Anolis.Core.Win32;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class IconCursorViewer : TypeViewer {
		
		public IconCursorViewer() {
			InitializeComponent();
		}
		
		public override void RenderResource(Win32ResourceLanguage resource) {
			
			// TODO: Classes and methods for working with resource types
			
		}
		
		public override Boolean CanHandleResourceType(Win32ResourceType type) {
			
			if( Enum.IsDefined( typeof(KnownWin32ResourceType), type.TypeInt ) ) { // check if type.TypeInt is a member of KnownWin32ResourceTypes
				
				KnownWin32ResourceType k = (KnownWin32ResourceType)type.TypeInt;
				
				return
					k == KnownWin32ResourceType.CursorAnimated ||
					k == KnownWin32ResourceType.CursorDeviceIndependent ||
					k == KnownWin32ResourceType.IconAnimated ||
					k == KnownWin32ResourceType.IconDeviceIndependent;
				
			}
			
			return false;
			
		}
		
		public override String ViewerName {
			get { return "Icon and Cursor Viewer"; }
		}
	}
}
