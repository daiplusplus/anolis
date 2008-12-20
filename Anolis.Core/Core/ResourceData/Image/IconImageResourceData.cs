using System;
using System.Drawing;
using System.IO;

using System.Runtime.InteropServices;
using Anolis.Core.NativeTypes;
using Anolis.Core.Utility;

namespace Anolis.Core.Data {
	
	public sealed class IconImageResourceData : ImageResourceData {
		
		private IconImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
		}
		
		public Size Size { get; private set; }
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] rawData, out ResourceData typed) {
			
			// rawData is an ICONIMAGE structure OR a PNG image
			
			// if it's a PNG image it's easy enough:
			if( PngImageResourceData.HasPngSignature( rawData ) ) {
				
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
			
			// I propose subclassing Dib to create a Dib for icons that passes in an IconInfo to get hints as to how to process it
				// I disagree, it still needs to be able to process icons without their directories.
				// right, sorry, just get it so it gets the image size right. Shame we can't trust biSizeImage
			
			Array.Resize<Byte>(ref fixd, (int)bih.biSizeImage );
			
			
			MemoryDib dib = new MemoryDib( fixd );
			
			typed = null;
			return false;
			
		}
		
		public override String FileFilter {
			get { return "BMP Image (*.bmp)|*.bmp"; }
		}
		
	}
	
}
