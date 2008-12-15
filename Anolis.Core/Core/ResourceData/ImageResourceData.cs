using System;
using System.Drawing;
using System.IO;

using System.Runtime.InteropServices;
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
			get { return "GIF Image (*.gif)|.gif"; }
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
			get { return "JPEG Image (*.jpg)|.jpg"; }
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
		
		public override string FileFilter {
			get { return "PNG Image (*.png)|.png"; }
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
			
			typed = BmpImageResourceData.Create(lang, data);
			
			return typed != null;
			
		}
		
		private static BmpImageResourceData Create(ResourceLang lang, Byte[] rawData) {
			
			Image image;
			
			Byte[] bitmapFileData = HasBitmapFileHeader(rawData) ? rawData : CreateBitmapFileData(rawData);
			
			if(TryCreateImage(bitmapFileData, out image)) {
				
				BmpImageResourceData rd = new BmpImageResourceData(image, lang, rawData);
				
				return rd;
				
			}
			
			return null;
		}
		
		private static Boolean HasBitmapFileHeader(Byte[] rawData) {
			
			if(rawData.Length > 2) {
				return rawData[0] == 'B' && rawData[1] == 'M';
			}
			return false;
			
		}
		
		private static Byte[] CreateBitmapFileData(Byte[] bitmapData) {
			
			IntPtr p = Marshal.AllocHGlobal( bitmapData.Length );
			Marshal.Copy( bitmapData, 0, p, bitmapData.Length );
			
			BitmapInfo bitmapInfo = (BitmapInfo)Marshal.PtrToStructure(p, typeof(BitmapInfo) ); // Beats the need for unsafe code
			
			Marshal.FreeHGlobal( p );
			
			///////////////////////////////////
			// Calculate the DIB Size
			
			BitmapInfoHeader bih = bitmapInfo.bmiHeader;
			
			uint colorTableSize = GetPaletteSize( bih );
			uint bmfhSize       = (uint)Marshal.SizeOf(typeof(BitmapFileHeader));
			
			uint dibSize        = bih.biSize + colorTableSize;
			
			switch((BiCompression)bitmapInfo.bmiHeader.biCompression) {
				case BiCompression.BiRle4:
				case BiCompression.BiRle8:
				case BiCompression.BiJpeg:
				case BiCompression.BiPng:
					// Compressed bitmap, so you cannot calculate the size of the pixeldata
					// So trust the biSizeImage
					
					if( bih.biSizeImage <= 0 ) throw new NotSupportedException();
					
					dibSize += bih.biSizeImage;
					
					break;
					
				case BiCompression.BiBitFields:
				case BiCompression.BiRgb:
					// it's not an RLE or compressed so the size can be calculated
					
					uint bmBitsSize = WidthBytes( (uint)bih.biWidth * bih.biBitCount ) * (uint)Math.Abs( bih.biHeight );
					
					dibSize += bmBitsSize;
					
//					System.Diagnostics.Debug.Assert( bmBitsSize == bih.biSizeImage );
					// commented out because there might be a bitmap that has an incorrect biSizeImage set
					// the MFC version overwrites this to magically fix any bitmaps passing through. Hmmm.
					
					// VS's bitmapFileHeader.bfSize is always magically right, even though I'm doing a faithful re-implementation of the MFC algo and I'm always off by 2 bytes.
					// so here goes nothing:
					
					// Maybe if I get that internship next year on the VS team I can take a look at the source and identify the cause:
					dibSize += 2;
					
					// EDIT: Compare against bitmapData.Length
					if(dibSize != bitmapData.Length) throw new InvalidDataException("DIB Image Size was not of the expected value.");
					
					break;
			}
			
			// Now calculate the file size
			
			BitmapFileHeader bmfh;
			bmfh.bfType      = 19778; // "BM"
			bmfh.bfSize      = dibSize + bmfhSize;
			bmfh.bfReserved1 = 0;
			bmfh.bfReserved2 = 0;
			bmfh.bfOffBits   = bmfhSize + bih.biSize + colorTableSize;
			
			
			// Now turn it into a stream containing a Bitmap recognised by Image
			
			// I'll figure out 32-bit compat later on, but I hear it is possible and not too hard
			
			Byte[] bitmapFileData = new Byte[ bitmapData.Length + bmfhSize ];
			
			p = Marshal.AllocHGlobal( (int)bmfhSize );
			Marshal.StructureToPtr(bmfh, p, true);
			
			Marshal.Copy(p, bitmapFileData, 0, (int)bmfhSize);
			
			Marshal.FreeHGlobal(p);
			
			Array.Copy(bitmapData, 0, bitmapFileData, 14, bitmapData.Length);
			
			return bitmapFileData;
			
		}
		
		public override void SaveAs(Stream stream) {
			
			// don't use Image.Save since we want to save it without any added .NET Image class nonsense, and preserve 32-bit BMPs
			
			Byte[] bitmapFileData = CreateBitmapFileData( RawData );
			
			stream.Write( bitmapFileData, 0, bitmapFileData.Length );
			
		}
		
		public override string FileFilter {
			get { return "BMP Image (*.bmp)|.bmp"; }
		}
		
#region Bitmap Header Work
		// Function identifiers variations on the MFC ones. Remind me to tidy them up later
		
		/// <summary>WidthBytes performs DWORD-aligning of DIB scanlines.  The "bits" parameter is the bit count for the scanline (biWidth * biBitCount), and this macro returns the number of DWORD-aligned bytes needed to hold those bits.</summary>
		private static uint WidthBytes(uint bits) {
			return (bits + 31) / 32 * 4;
		}
		
		private static uint GetPaletteSize(BitmapInfoHeader bmih) {
			
			uint nofColors = DibNumColors(bmih);
			
			if( IsBitmapInfoHeader(bmih) ) {
				
				if((BiCompression)bmih.biCompression == BiCompression.BiBitFields) {
					
					// C Original:
					// return (sizeof(DWORD) * 3) + (DIBNumColors (lpbih) * sizeof (RGBQUAD)); 
					// return (4 * 3) + (nofColors * 4)
					return 12 + nofColors * (uint)Marshal.SizeOf(typeof(RgbQuad));
					
				} else {
					return nofColors * (uint)Marshal.SizeOf(typeof(RgbQuad));
				}
				
			} else {
				
				return nofColors * (uint)Marshal.SizeOf(typeof(RgbTriple));
				
			}
			
		}
		
		/// <summary>Confirms it's a BitmapInfoHeader rather than BitmapCoreHeader (which hails from ye olde OS/2 days and is largely obsolete).</summary>
		private static Boolean IsBitmapInfoHeader(BitmapInfoHeader bmih) {
			return bmih.biSize >= 40;
		}
		
		private static ushort DibNumColors(BitmapInfoHeader bmih) {
			
			ushort bitCount;
			
			/*  If this is a Windows-style DIB, the number of colors in the color table can be less than the number of bits per pixel
				allows for (i.e. lpbi->biClrUsed can be set to some value). If this is the case, return the appropriate value. */
			if( IsBitmapInfoHeader(bmih) ) {
				
				uint colorsUsed = bmih.biClrUsed;
				if( colorsUsed != 0 ) return (ushort)bmih.biClrUsed;
				
				bitCount = bmih.biBitCount;
				
			} else {
				
				// Here it casts the BitmapInfoHeader as BitmapCoreHeader
				// Which I'm not going to do...
				throw new NotImplementedException();
				
			}
			
			/* Calculate the number of colors in the color table based on the number of bits per pixel for the DIB. */
			
			switch(bitCount) {
				case 1:
					return 2;
				case 4:
					return 16;
				case 8:
					return 256;
				default:
					// alternatively do a left bitshift, but .NET makes this hard with unsigned types so nvm
					return 0;
			}
			
		}
		
#endregion
		
	}
	
}
