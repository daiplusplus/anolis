using System;
using System.Windows.Forms;

using Anolis.Core;

namespace Anolis.Resourcer.TypeViewers {
	
	public class TypeViewer : UserControl { // can't define as abstract thanks to the WinForms designer
		
		public virtual void RenderResource(ResourceData resource) {
			throw new NotImplementedException();
		}
		
		/// <summary>This Type Viewer can handle the specified resource data. Also if this viewer is the recommended viewer for that type.</summary>
		public virtual TypeViewerCompatibility CanHandleResource(ResourceData data) {
			throw new NotImplementedException();
		}
		
		public virtual String ViewerName { get { throw new NotImplementedException(); } }
		
	}
	
	public enum TypeViewerCompatibility {
		Ideal,
		Works,
		None
	}
	
}
