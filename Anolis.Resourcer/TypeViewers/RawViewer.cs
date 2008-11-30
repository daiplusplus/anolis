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
	
	public partial class RawViewer : TypeViewer {
		
		public RawViewer() {
			InitializeComponent();
		}
		
		public override void RenderResource(Win32ResourceLanguage resource) {
			
			Byte[] data = resource.GetData();
			
			Be.Windows.Forms.DynamicByteProvider bytesProv = new Be.Windows.Forms.DynamicByteProvider( data );
			
			__hex.ByteProvider = bytesProv;
			
			///////////////////////
			
			__statusSize.Text = String.Format("Size: {0} bytes", data.Length);
			__statusType.Text = String.Format("Type: {0}", resource.ParentName.ParentType.FriendlyName);
			
		}
		
		public override bool CanHandleResourceType(Win32ResourceType type) {
			return true; // this can handle anything since it shows the raw bytes
		}
		
		public override string ViewerName {
			get { return "Raw Binary Viewer"; }
		}
	}
}
