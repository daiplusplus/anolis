using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using Anolis.Core;

using MemoryStream = System.IO.MemoryStream;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class PictureViewer : TypeViewer {
		
		public PictureViewer() {
			InitializeComponent();
		}
		
		public override void RenderResource(ResourceData resource) {
			
			Byte[] data = resource.RawData;
			
			MemoryStream stream = new MemoryStream( data );
			
			// check if the resource is a BITMAP so it can add the right headers to the stream before making an Image from it
			// hmmm, how do I give it a ResourceType though?
			
			Image image = Image.FromStream( stream, true, true );
			
			__pv.Image = image;
			
		}
		
		public override Boolean CanHandleResourceType(ResourceType type) {
			
			if(type.Identifier.KnownType == Win32ResourceType.Bitmap) return true;
			
			if(type.Identifier.StringId == null) return false;
			
			switch(type.Identifier.StringId.ToUpperInvariant()) {
				case "PNG":
				case "GIF":
				case "JPEG":
				case "JPG":
					return true;
			}
			
			return false;
			
		}
		
		public override String ViewerName {
			get { return "Bitmap / Picture Viewer"; }
		}
		
		public override string ToString() {
			return ViewerName;
		}
		
	}
}
