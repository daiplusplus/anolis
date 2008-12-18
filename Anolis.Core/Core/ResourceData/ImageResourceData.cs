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
		
		public override string FileFilter {
			get { return "GIF Image (*.gif)|*.gif"; }
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
		
		public override string FileFilter {
			get { return "JPEG Image (*.jpg)|*.jpg"; }
		}
		
	}
	
	public sealed class PngImageResourceData : ImageResourceData {
		
		private PngImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] data, out ResourceData typed) {
			
			typed = null;
			
			if(!LooksLikePng(data)) return false;
			
			Image image;
			
			if(!TryCreateImage(data, out image)) return false;
			
			typed = new PngImageResourceData(image, lang, data);
			
			return true;
			
		}
		
		internal static Boolean LooksLikePng(Byte[] data) {
			
			if(data.Length < 3) return false;
			
			String sig = System.Text.Encoding.ASCII.GetString(data, 1, 3); // ignore first byte
			
			return sig == "PNG";
			
		}
		
		public override string FileFilter {
			get { return "PNG Image (*.png)|*.png"; }
		}
		
	}
	
	public sealed class BmpImageResourceData : ImageResourceData {
		
		private FileDib _dib;
		
		// HACK: When I implement the IResourceDataFactory patterns I should simplify the constructor here
		
		private BmpImageResourceData(FileDib dib, Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
			_dib = dib;
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] rawData, out ResourceData typed) {
			
			// check if the data is of the right format before working with it
			
			FileDib dib = new FileDib( rawData );
			
			Bitmap bmp;
			
			if(!dib.TryToBitmap(out bmp)) {
				typed = null;
				return false;
			}
			
			typed = new BmpImageResourceData(dib, bmp, lang, rawData);
			return true;
			
		}
		
		
		public override void SaveAs(Stream stream) {
			
			// don't use Image.Save since we want to save it without any added .NET Image class nonsense, and preserve 32-bit BMPs
			
			Byte[] bitmapFileData = _dib.Data;
			
			stream.Write( bitmapFileData, 0, bitmapFileData.Length );
			
		}
		
		public override string FileFilter {
			get { return "BMP Image (*.bmp)|*.bmp"; }
		}
		
	}
	
	public sealed class IconImageResourceData : ImageResourceData {
		
		private IconImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
		}
		
		public Size Size { get; private set; }
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] rawData, out ResourceData typed) {
			
			// rawData is an ICONIMAGE structure OR a PNG image
			
			// if it's a PNG image it's easy enough:
			if( PngImageResourceData.LooksLikePng( rawData ) ) {
				
				Image image;
				if(!ImageResourceData.TryCreateImage(rawData, out image)) { typed = null; return false; }
				
				typed = new IconImageResourceData(image, lang, rawData) { Size = image.Size };
				return true;
				
			}
			
			// I can't load the data into the ICONIMAGE because .NET doesn't support C-style value arrays very well
			
/*
typdef struct {
	BITMAPINFOHEADER icHeader;      // DIB header
	RGBQUAD          icColors[1];   // Color table
	BYTE             icXOR[1];      // DIB bits for XOR mask
	BYTE             icAND[1];      // DIB bits for AND mask
}ICONIMAGE, *LPICONIMAGE;
*/
			
			// in the BitmapInfoHeader icHeader only the following fields are used:
			//	biSize, biWidth, biHeight, biPlanes, biBitCount, biSizeImage.
			// All other members must be 0. The biHeight member specifies the combined height of the XOR and AND masks.
			// The members of icHeader define the contents and sizes of the other elements of the ICONIMAGE structure in the
			//	same way that the BITMAPINFOHEADER structure defines a CF_DIB format DIB.
			
			Int32 sizeOfBitmapInfoHeader = Marshal.SizeOf(typeof(BitmapInfoHeader));
			
			IntPtr p = Marshal.AllocHGlobal( rawData.Length );
			Marshal.Copy( rawData, 0, p, rawData.Length );
			
			BitmapInfoHeader bih = (BitmapInfoHeader)Marshal.PtrToStructure( p, typeof(BitmapInfoHeader) );
			
			// the XOR part of the ICONIMAGE is the actual DIB bitmap and can be treated as such (just divide the biHeight by 2 first)
			// the AND part is the 'binary transparency' part of the icon. It is redundant in 32-bit DIBs.
			
			// I need to make a copy of the IconInfo data, modify the BitmapInfoHeader to fix the size and then factor in the AND mask
			
			bih.biHeight /= 2;
			
			Marshal.StructureToPtr(bih, p, false);
			
			Byte[] fixd = new Byte[rawData.Length];
			
			Marshal.Copy(p, fixd, 0, rawData.Length);
			Marshal.FreeHGlobal( p );
			
			// truncate the array so it's the right size
			
			// use biSizeImage as this value MUST be set
				// gah... wait a sec. Msmsgr's #1 icon doesn't have biSizeImage set!!!!
			// biSizeImage is equal to the calculated bmSizeBits in the Dib class
			
			// which means the byte array length must be equal to... TODO: find out, damnit
			
			
			Array.Resize<Byte>(ref fixd, (int)bih.biSizeImage );
			
			
			MemoryDib dib = new MemoryDib( fixd );
			
			typed = null;
			return false;
			
		}
		
		public override string FileFilter {
			get { return "BMP Image (*.bmp)|*.bmp"; }
		}
		
	}
	
}
