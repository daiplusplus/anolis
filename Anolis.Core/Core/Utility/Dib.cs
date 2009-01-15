using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

using InvalidDataException = System.IO.InvalidDataException;

using Anolis.Core.Native;
using Anolis.Core.Native.Dib;

namespace Anolis.Core.Utility {
	
	/// <summary>One DIB class to rule them all.</summary>
	/// <remarks>Supports Device Independent Bitmaps (DIB) using the BitmapCore, BitmapInfo, BitmapV4, and BitmapV5 formats.</remarks>
	public class Dib {
		
		private BitmapFileHeader _fileHeader;
		private BitmapV5Header   _infoHeader;
		
		/// <summary>DIB Data, sans FileHeader</summary>
		private Byte[]           _dibData;
		
		public Dib(Byte[] data) {
			
			_dibData = data;
			
			MemoryStream ms = new MemoryStream( data );
			
			Initialise( ms );
			
		}
		
		public Dib(Stream stream, Int32 length) {
			
			if(!(stream.CanSeek && stream.CanRead)) throw new NotSupportedException("Stream must support seeking and reading.");
			
			Int64 initPos = stream.Position;
			
			Initialise(stream);
			
			stream.Seek(initPos, SeekOrigin.Begin);
			
			if( stream.Read( _dibData, 0, length ) != length ) throw new Exception("Unexpected stream length.");
			
		}
		
		/// <summary>Reads header information from a stream.</summary>
		private void Initialise(Stream stream) {
			
			BinaryReader rdr = new BinaryReader(stream);
			
			// see if it has a BitmapFileHeader at the beginning
			Int32 first = rdr.PeekChar();
			if(first == 'B') {
				
				_fileHeader = new BitmapFileHeader(rdr);
				
			}
			
			// determine the Dib type (the BitmapFileHeader will have advanced the reader)
			// maybe do some validation on the structs?
			
			switch(rdr.PeekChar()) {
				case 12:
					Class       = DibClass.Core;
					break;
				case 40:
					Class       = DibClass.V3;
					break;
				case 108:
					Class       = DibClass.V4;
					break;
				case 124:
					Class       = DibClass.V5;
					break;
				default:
					throw new NotSupportedException("Unrecognised BitmapInfoHeader dwSize");
			}
			
			_infoHeader = FillBitmapHeader(rdr, Class);
			
			if( _fileHeader.bfType == 0 ) { // if no fileheader was read earlier
				// create the FileHeader
				
				_fileHeader = CreateFileHeader();
				
			} else {
				
				// resize the data array to remove the fileheader
				
				Int32 sizeOfBitmapFileHeader = Marshal.SizeOf(typeof(BitmapFileHeader));
				
				Byte[] newData = new Byte[ _dibData.Length - sizeOfBitmapFileHeader ];
				
				Array.Copy( _dibData, sizeOfBitmapFileHeader, newData, 0, newData.Length );
				
				_dibData = newData;
				
			}
			
		}
		
		public DibClass Class { get; private set; }
		
		public Bitmap ToBitmap() {
			
			MemoryStream stream = new MemoryStream();
			Save(stream);
			
			return Bitmap.FromStream(stream) as Bitmap;
			
		}
		
		public void Save(Stream stream) {
			
			if(!stream.CanWrite) throw new ArgumentException("Cannot write to stream");
			
			// write the _fileHeader
			
			BinaryWriter wtr = new BinaryWriter(stream);
			
			_fileHeader.Write(wtr);
			
			// write the data
			
			stream.Write(_dibData, 0, _dibData.Length);
			
		}
		
#region Header Member Calculation
		
		private BitmapFileHeader CreateFileHeader() {
			
			UInt32 bmfhSize       = (uint)Marshal.SizeOf(typeof(BitmapFileHeader));
			
			UInt32 dibSize = _infoHeader.bV5Size + GetColorTableSize();
			
			switch( _infoHeader.bV5Compression ) {
				
				case BiCompression.BiRle4:
				case BiCompression.BiRle8:
				case BiCompression.BiJpeg:
				case BiCompression.BiPng:
					// Compressed bitmap, so you cannot calculate the size of the pixeldata
					// So trust the biSizeImage
					
					if( _infoHeader.bV5SizeImage <= 0 ) throw new NotSupportedException("The bitmap's size is not defined.");
					
					dibSize += _infoHeader.bV5SizeImage; // this is a V3 header field, but since Core doesn't use compression set it doesn't apply
					
					break;
					
				case BiCompression.BiBitFields:
				case BiCompression.BiRgb:
					// it's not an RLE or compressed so the size can be calculated
					
					uint bmBitsSize = WidthBytes( (uint)_infoHeader.bV5Width * _infoHeader.bV5BitCount ) * (uint)Math.Abs( _infoHeader.bV5Height );
					
					dibSize += bmBitsSize;
					
//					System.Diagnostics.Debug.Assert( bmBitsSize == bih.biSizeImage );
					// commented out because there might be a bitmap that has an incorrect biSizeImage set
					// the MFC version overwrites this to magically fix any bitmaps passing through. Hmmm.
					
					// VS's bitmapFileHeader.bfSize is always magically right, even though I'm doing a faithful re-implementation of the MFC algo and I'm always off by 2 bytes.
					// so here goes nothing:
					
					// Maybe if I get that internship next year on the VS team I can take a look at the source and identify the cause:
					if( dibSize + 2 == _dibData.Length) dibSize += 2;
					
					// EDIT: Compare against bitmapData.Length
//					if(dibSize != _data.Length) throw new InvalidDataException("DIB Image Size was not of the expected value.");
					
					break;
				
				default:
					
					throw new NotSupportedException("The bitmap uses an unknown biCompression value.");
			}
			
			// Now actually create the FileHeader and calculate the file size
			
			BitmapFileHeader bmfh;
			bmfh.bfType      = 19778; // "BM"
			bmfh.bfSize      = bmfhSize + dibSize;
			bmfh.bfReserved1 = 0;
			bmfh.bfReserved2 = 0;
			bmfh.bfOffBits   = bmfhSize + _infoHeader.bV5Size + GetColorTableSize();
			
			return bmfh;
			
		}
		
		/// <summary>WidthBytes performs DWORD-aligning of DIB scanlines.  The "bits" parameter is the bit count for the scanline (biWidth * biBitCount), and this macro returns the number of DWORD-aligned bytes needed to hold those bits.</summary>
		protected static uint WidthBytes(uint bits) {
			return (bits + 31) / 32 * 4;
		}
		
		private static BitmapV5Header FillBitmapHeader(BinaryReader rdr, DibClass cls) {
			
			BitmapV5Header v5 = new BitmapV5Header();
			v5.bV5Compression = BiCompression.BiRgb; // I'm assuming that BitmapCore bitmaps use RGB encoding without compression
			
			Boolean isCore = cls == DibClass.Core;
			
			v5.bV5Size          = rdr.ReadUInt32();
			v5.bV5Width         = isCore ? rdr.ReadUInt16() : rdr.ReadInt32();
			v5.bV5Height        = isCore ? rdr.ReadUInt16() : rdr.ReadInt32();
			v5.bV5Planes        = rdr.ReadUInt16();
			v5.bV5BitCount      = rdr.ReadUInt16();
			
			if(cls == DibClass.Core) return v5;
			
			v5.bV5Compression   = (BiCompression)rdr.ReadUInt32();
			v5.bV5SizeImage     = rdr.ReadUInt32();
			v5.bV5XPelsPerMeter = rdr.ReadInt32();
			v5.bV5YPelsPerMeter = rdr.ReadInt32();
			v5.bV5ClrUsed       = rdr.ReadUInt32();
			v5.bV5ClrImportant  = rdr.ReadUInt32();
			
			if(cls == DibClass.V3) return v5;
			
			v5.bV5RedMask       = rdr.ReadUInt32();
			v5.bV5GreenMask     = rdr.ReadUInt32();
			v5.bV5BlueMask      = rdr.ReadUInt32();
			v5.bV5AlphaMask     = rdr.ReadUInt32();
			v5.bV5CSType        = rdr.ReadUInt32();
			v5.bV5Endpoints     = new CieXyzTriple(rdr);
			v5.bV5GammaRed      = rdr.ReadUInt32();
			v5.bV5GammaGreen    = rdr.ReadUInt32();
			v5.bV5GammaBlue     = rdr.ReadUInt32();
			
			if(cls == DibClass.V4) return v5;
			
			v5.bV5Intent        = rdr.ReadUInt32();
			v5.bV5ProfileData   = rdr.ReadUInt32();
			v5.bV5ProfileSize   = rdr.ReadUInt32();
			v5.bV5Reserved      = rdr.ReadUInt32();
			
			return v5;
			
		}
		
		private UInt32 GetColorTableSize() {
			
			UInt16 nofColors = GetNumColors();
			
			if( Class == DibClass.Core ) {
				
				return nofColors * (uint)Marshal.SizeOf(typeof(RgbTriple));
				
			} else {
				
				return GetColorTableSize( nofColors, _infoHeader.bV5Compression );
				
			}
			
		}
		
		private static UInt32 GetColorTableSize(UInt16 nofColors, BiCompression compression) {
			
			if(compression == BiCompression.BiBitFields) {
				
				// C Original:
				// return (sizeof(DWORD) * 3) + (DIBNumColors (lpbih) * sizeof (RGBQUAD)); 
				// return (4 * 3) + (nofColors * 4)
				return 12 + nofColors * (uint)Marshal.SizeOf(typeof(RgbQuad));
				
			} else { // what about RLE, PNG, and JPEG compression?
				
				return nofColors * (uint)Marshal.SizeOf(typeof(RgbQuad));
				
			}
			
		}
		
		
		private UInt16 GetNumColors() {
			
			if( Class == DibClass.Core ) {
				
				return GetNumColors(0, _infoHeader.bV5BitCount);
				
			} else {
				
				return GetNumColors(_infoHeader.bV5ClrUsed, _infoHeader.bV5BitCount);
				
			}
			
		}
		
		private static UInt16 GetNumColors(UInt32 clrUsed, UInt32 bitCount) {
			
			// the number of colors in the color table can be less than the number of bits per pixel allows for (i.e. lpbi->biClrUsed can be set to some value). If this is the case, return the appropriate value.
			if( clrUsed != 0 ) return (UInt16)clrUsed;
			
			switch(bitCount) {
				case 1:  return 2;
				case 4:  return 16;
				case 8:  return 256;
				default: return 0;
			}
			
		}
		
#endregion
		
	}
	
	public enum DibClass {
		/// <summary>Uses an OS/2 BitmapCoreHeader</summary>
		Core,
		/// <summary>Uses a Windows 3.x / NT3.5 BitmapInfoHeader</summary>
		V3,
		/// <summary>Uses a Windows 95 / NT4 BitmapV4Header</summary>
		V4,
		/// <summary>Uses a Windows 98 / 2000 BitmapV4Header</summary>
		V5
	}
	
}
