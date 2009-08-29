using System;
using System.IO;

using Anolis.Core.Data;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class SgmlViewer : TypeViewer {
		
		public SgmlViewer() {
			InitializeComponent();
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			
			if( data is SgmlResourceData ) return TypeViewerCompatibility.Works;
			
			return TypeViewerCompatibility.None;
		}
		
		public override void RenderResource(ResourceData resource) {
			
			Byte[] data = resource.RawData;
			
			MemoryStream stream = new MemoryStream(data); // not sure if I should use using() or not. Does the stream need to exist after its been read in?
			__browser.DocumentStream = stream;
			
		}
		
		public override string ViewerName {
			get { return "SGML (HTML/XML) Viewer"; }
		}
		
	}
}
