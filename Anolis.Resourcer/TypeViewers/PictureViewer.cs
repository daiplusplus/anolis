using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Core.Win32;

using MemoryStream = System.IO.MemoryStream;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class PictureViewer : TypeViewer {
		
		public PictureViewer() {
			InitializeComponent();
		}
		
		public override void RenderResource(Win32ResourceLanguage resource) {
			
			Byte[] data = resource.GetData();
			
			MemoryStream stream = new MemoryStream( data );
			
			Image image = Image.FromStream( stream, true, true );
			
			__pv.Image = image;
			
		}
		
		public override Boolean CanHandleResourceType(Win32ResourceType type) {
			
			if(type.TypeInt == -1) {
				
				String typeStr = type.TypeStr.ToUpperInvariant();
				switch(typeStr) {
					case "PNG":
					case "GIF":
					case "JPEG":
					case "JPG":
						return true;
				}
				
			} else if( Enum.IsDefined( typeof(KnownWin32ResourceType), type.TypeInt ) ) { // check if type.TypeInt is a member of KnownWin32ResourceTypes
				
				// I'm having problems with Resource Bitmaps. Sys.Drawing.Image can't load them so nvm for now.
//				KnownWin32ResourceTypes typeEnum = (KnownWin32ResourceTypes)type.TypeInt;
//				switch(typeEnum) {
//					case KnownWin32ResourceTypes.Bitmap:
//						return true;
//					// NOTE: What about icons?
//					// No, you'll need a dedicated icon and cursor handler
//				}
				return false;
				
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
