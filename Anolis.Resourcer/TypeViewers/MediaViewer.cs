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
	
	public partial class MediaViewer : TypeViewer {
		
		public MediaViewer() {
			
			InitializeComponent();
			
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			
			if(data is RiffMediaResourceData) return TypeViewerCompatibility.Ideal;
			
			return TypeViewerCompatibility.None;
			
		}
		
		public override void RenderResource(ResourceData resource) {
			
			RiffMediaResourceData riff = resource as RiffMediaResourceData;
			
			String filename = riff.SaveToTemporaryFile();
			
			__wmp.URL = filename;
			
		}
		
		public override string ViewerName {
			get { return "Windows Media Player"; }
		}
		
	}
}
