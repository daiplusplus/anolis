using System;
using System.Collections.Generic;
using System.Text;
using Anolis.Core;
using System.Windows.Forms;

namespace Anolis.Resourcer.TypeViewers {
	
	public class TypeViewer : UserControl { // can't define as abstract thanks to the WinForms designer
		
		public virtual void RenderResource(Win32ResourceLanguage resource) {
			throw new NotImplementedException();
		}
		
		/// <summary>This Type Viewer can handle the specified type. Also if this viewer is the recommended viewer for that type.</summary>
		public virtual Boolean CanHandleResourceType(Win32ResourceType type) {
			throw new NotImplementedException();
		}
		
		public virtual String ViewerName { get { throw new NotImplementedException(); } }
		
	}
	
}
