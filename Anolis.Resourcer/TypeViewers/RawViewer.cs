using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Anolis.Core;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class RawViewer : TypeViewer {
		
		public RawViewer() {
			InitializeComponent();
		}
		
		public override void RenderResource(ResourceData resource) {
			
			Byte[] data = resource.RawData;
			
			Be.Windows.Forms.DynamicByteProvider bytesProv = new Be.Windows.Forms.DynamicByteProvider( data );
			
			__hex.ByteProvider = bytesProv;
			
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			return TypeViewerCompatibility.Works;
		}
		
		public override string ViewerName {
			get { return "Raw Binary Viewer"; }
		}
	}
}
