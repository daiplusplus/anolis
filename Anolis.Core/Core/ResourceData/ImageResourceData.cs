using System;
using System.Drawing;
using System.IO;

using Anolis.Core.NativeTypes;

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
			}
			
		}
		
	}
	
	public sealed class GifImageResourceData : ImageResourceData {
		
		// There is no need to override the SaveAs method since the bytes are the same
		
		private GifImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] data, out ResourceData typed) {
			
			// XN Resource Editor checks if the first few bytes are a GIF signature
			
			typed = null;
			
			if(data.Length < 5) return false;
			
			String sig = System.Text.Encoding.ASCII.GetString(data, 0, 5);
			
			if(sig != "GIF87" && sig != "GIF89") return false;
			
			Image image;
			
			if(!TryCreateImage(data, out image)) return false;
			
			typed = new GifImageResourceData(image, lang, data);
			
			return true;
			
		}
		
	}
	
	public sealed class JpegImageResourceData : ImageResourceData {
		
		private JpegImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] data, out ResourceData typed) {
			
			// XN Resource Editor does a little bit of raw byte voodoo to assess if a stream is a JPEG/JFIF or not
			// I'll implement it in a later version unless it's a massive resource drain
			
			typed = null;
			
			Image image;
			
			if(!TryCreateImage(data, out image)) return false;
			
			typed = new JpegImageResourceData(image, lang, data);
			
			return true;
			
		}
		
	}
	
	public sealed class PngImageResourceData : ImageResourceData {
		
		private PngImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] data, out ResourceData typed) {
			
			typed = null;
			
			if(data.Length < 3) return false;
			
			String sig = System.Text.Encoding.ASCII.GetString(data, 1, 3); // ignore first byte
			
			if(sig != "PNG") return false;
			
			Image image;
			
			if(!TryCreateImage(data, out image)) return false;
			
			typed = new PngImageResourceData(image, lang, data);
			
			return true;
			
		}
		
	}
	
	public sealed class BmpImageResourceData : ImageResourceData {
		
		private BmpImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] data, out ResourceData typed) {
			
			// check if the data is of the right format before working with it
			
			// it's worth pointing out that the .NET Image class does not support 32-bit Bitmaps. It needs to be converted into a PNG image before it can be displayed
			// ...which is a bit of a pain. I should google for a solution
			
			// don't forget the various header file formats
			
			
			
			typed = null;
			
			return false;
			
		}
		
		private static BmpImageResourceData Create(ResourceLang lang, Byte[] rawData) {
			
			Byte[] data = rawData;
			
			// determine if the data has the BMP header or not
			// if not, recreate it and add it
			
			// else { data = new Byte[ data.Length + sizeof(BitmapHeader) ]
			
			
			// assuming there is no BitmapFileHeader
			
			
			
		}
		
		public override void SaveAs(Stream stream) {
			
			// don't use Image.Save since we want to save it without any added .NET Image class nonsense, and preserve 32-bit BMPs
			
			
			
		}
		
	}
	
}
