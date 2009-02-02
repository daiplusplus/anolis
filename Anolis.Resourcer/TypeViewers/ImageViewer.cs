using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using Anolis.Core;

using MemoryStream = System.IO.MemoryStream;
using Anolis.Core.Data;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class ImageViewer : TypeViewer {
		
		public ImageViewer() {
			InitializeComponent();
		}
		
		public override void RenderResource(ResourceData resource) {
			
			ImageResourceData ri = resource as ImageResourceData;
			if(ri == null) throw new ArgumentException("Provided Resource was not an ImageResourceData", "resource");
			
			__pv.Image = ri.Image;
			
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			
			if( data is ImageResourceData ) return TypeViewerCompatibility.Ideal;
			
			return TypeViewerCompatibility.None;
			
		}
		
		public override String ViewerName {
			get { return "Bitmap / Picture Viewer"; }
		}
		
		public override string ToString() {
			return ViewerName;
		}
		
	}
}
