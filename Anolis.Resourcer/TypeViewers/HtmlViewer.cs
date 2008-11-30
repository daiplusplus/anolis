using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using Anolis.Core;
using Anolis.Core.Win32;
using Anolis.Resourcer.TypeViewers;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class HtmlViewer : TypeViewer {
		
		public HtmlViewer() {
			InitializeComponent();
		}
		
		public override Boolean CanHandleResourceType(Win32ResourceType type) {
			
			switch(type.TypeInt) {
				
				case 23: // HTML
				case 24: // Manifest
					return true;
				
			}
			
			if(type.TypeInt != -1) return false;
			
			String t = type.TypeStr.ToUpperInvariant(); // won't be null because TypeInt == -1
			
			switch(t) {
				
				case "HTML":
				case "XML":
				case "XHTML":
				case "SGML":
					return true;
			}
			
			return false;
			
		}
		
		public override void RenderResource(Win32ResourceLanguage resource) {
			
			Byte[] data = resource.GetData();
			
			MemoryStream stream = new MemoryStream(data); // not sure if I should use using() or not
			__browser.DocumentStream = stream;
			
		}
		
		public override string ViewerName {
			get { return "HTML Viewer"; }
		}
		
	}
}
