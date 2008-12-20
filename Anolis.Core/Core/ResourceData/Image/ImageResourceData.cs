using System;
using System.Drawing;
using System.IO;

using System.Runtime.InteropServices;
using Anolis.Core.NativeTypes;
using Anolis.Core.Utility;

namespace Anolis.Core.Data {
	
	public abstract class ImageResourceData : ResourceData {
		
		public virtual Image Image { get; private set; }
		
		protected ImageResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		protected ImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
			
			Image = image;
		}
		
		protected static Boolean TryCreateImage(Byte[] data, out Image image) {
			
			image = null;
			
			try {
				
				MemoryStream stream = new MemoryStream(data); // Should MemoryStreams be in a using() for Bitmap creation? MSDN says the Stream needs to exist for the life of the Bitmap
				image = Image.FromStream( stream, true, true );
				
				return true;
				
			} catch(ArgumentException) {
				return false;
			} catch(ExternalException) {
				return false;
			}
			
		}
		
		// TODO: Maybe override SaveAs and manage saving to GIF, JPEG, PNG, etc...?
		
	}
	
}
