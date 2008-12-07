using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using Anolis.Core;
using Anolis.Resourcer.TypeViewers;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class HtmlViewer : TypeViewer {
		
		public HtmlViewer() {
			InitializeComponent();
		}
		
		public override Boolean CanHandleResourceType(ResourceType type) {
			
			switch(type.Identifier.KnownType) {
				
				case KnownWin32ResourceType.Html: // HTML
				case KnownWin32ResourceType.Manifest : // Manifest
					return true;
				
			}
			
			if(type.Identifier.StringId == null) return false;
			
			switch(type.Identifier.StringId.ToUpperInvariant()) {
				
				case "HTML":
				case "XML":
				case "XHTML":
				case "SGML":
					return true;
			}
			
			return false;
			
		}
		
		public override void RenderResource(ResourceData resource) {
			
			Byte[] data = resource.RawData;
			
			MemoryStream stream = new MemoryStream(data); // not sure if I should use using() or not. Does the stream need to exist after its been read in?
			__browser.DocumentStream = stream;
			
		}
		
		public override string ViewerName {
			get { return "HTML Viewer"; }
		}
		
	}
}
