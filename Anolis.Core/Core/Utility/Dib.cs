using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

using InvalidDataException = System.IO.InvalidDataException;

using Anolis.Core.Native;
using Anolis.Core.Native.Dib;

namespace Anolis.Core.Utility {
	
	// it's worth pointing out that the .NET Image class does not support 32-bit Bitmaps. It needs to be converted into a PNG image before it can be displayed
	// ...which is a bit of a pain. I should google for a solution
	
	// TODO: Do something about different BitmapHeader versions, there's: CORE, info, v4, and v5. Core is the black sheep, but v4 and v5 need to be passed through when saving
	
	/// <summary>Encapuslates an immutable Device-Independent Bitmap that lacks a BitmapFileHeader.</summary>
	internal class MemoryDib {
		
		protected static readonly Int32 sizeOfBitmapFileHeader = Marshal.SizeOf(typeof(BitmapFileHeader));
		protected static readonly Int32 sizeOfBitmapInfoHeader = Marshal.SizeOf(typeof(BitmapInfoHeader));
		
		/// <summary>Creates a MemoryDib from an existing DIB's bytes. If the data contains a FileHeader it will be removed.</summary>
		public MemoryDib(Byte[] data) {
			_data = data;
			
			if( HasFileHeader ) {
				RemoveFileHeader();
			}
		}
		
		protected MemoryDib() { }
		
		/////////////////////////////////////////////
		// Data
		/////////////////////////////////////////////
		
		protected Byte[] _data;
		
		public Byte[] Data { get{ return _data; } }
		
		protected void ResizeData(Int32 newSize, Boolean front) {
			
			if(newSize == _data.Length) return;
			
			if(!front){
				
				Array.Resize<Byte>(ref _data, newSize);
				
			} else {
				
				if(newSize > _data.Length) {
					
					Int32 lengthDiff = newSize - _data.Length;
					
					Byte[] n = new Byte[ newSize ];
					Array.Copy(_data, 0, n, lengthDiff, _data.Length); 
					
					_data = n;
					
				} else {
					
					Int32 lengthDiff = _data.Length - newSize;
					
					Byte[] n = new Byte[ newSize ];
					Array.Copy(_data, lengthDiff, n, 0, n.Length);
					
					_data = n; 
					
				}
				
			}
			
		}
		
		/////////////////////////////////////////////
		// Utility functions for working with Headers for calculating size, etc...
		/////////////////////////////////////////////
		
		
		
		/////////////////////////////////////////////
		// Major Actions
		/////////////////////////////////////////////
		
		protected Boolean HasFileHeader {
			get {
				// TODO: Maybe do a more thorough check by casting Data to a BitmapFileInfoHeader
				
				if( _data.Length < 2 ) throw new InvalidOperationException("Dib contains insufficient data.");
				
				String start = System.Text.Encoding.ASCII.GetString(_data, 0, 2);
				
				return start == "BM";
			}
		}
		
		private void RemoveFileHeader() {
			
			ResizeData( _data.Length - sizeOfBitmapFileHeader, true );
			
		}
		
		private BitmapInfoHeader? _infoHeader;
		
		public BitmapInfoHeader InfoHeader {
			get {
				
				if(_infoHeader == null) {
					
					Int32 offset = HasFileHeader ? sizeOfBitmapFileHeader : 0;
					
					IntPtr p = Marshal.AllocHGlobal( sizeOfBitmapInfoHeader );
					Marshal.Copy( _data, offset, p, sizeOfBitmapInfoHeader );
					
					_infoHeader = (BitmapInfoHeader)Marshal.PtrToStructure(p, typeof(BitmapInfoHeader));
					
					Marshal.FreeHGlobal( p );
					
				}
				
				return _infoHeader.Value;
				
			}
		}
		
		public FileDib ToFileDib() {
			
			return new FileDib(_data);
		}
		
	}
	
	internal class FileDib : MemoryDib {
		
		/// <summary>Creates a FileDib from an existing DIB's bytes. If the data does not contain a FileHeader it will be added.</summary>
		public FileDib(Byte[] data) {
			
			_data = data;
			
			if( !HasFileHeader ) {
				AddFileHeader();
			}
			
		}
		
		private void AddFileHeader() {
			
			BitmapFileHeader bfh = CreateFileHeader();
			
			ResizeData( _data.Length + sizeOfBitmapFileHeader, true );
			
			IntPtr p = Marshal.AllocHGlobal( sizeOfBitmapFileHeader );
			Marshal.StructureToPtr( bfh, p, true );
			Marshal.Copy(p, _data, 0, sizeOfBitmapFileHeader );
			Marshal.FreeHGlobal( p );
			
		}
		
		
		
		private BitmapFileHeader? _fileHeader;
		
		public BitmapFileHeader FileHeader {
			get {
				
				// It can be assumed it has a FileHeader
				
				if( _fileHeader == null ) {
					
					IntPtr p = Marshal.AllocHGlobal( sizeOfBitmapFileHeader );
					Marshal.Copy( _data, 0, p, sizeOfBitmapFileHeader );
					
					_fileHeader = (BitmapFileHeader)Marshal.PtrToStructure(p, typeof(BitmapFileHeader));
					
					Marshal.FreeHGlobal( p );
					
				}
				
				return _fileHeader.Value;
			}
		}
		
		public Bitmap ToBitmap() {
			
			// TODO: Correct 32bit RGBA data if it doesn't work
			
			MemoryStream stream = new MemoryStream(_data); // Should MemoryStreams be in a using() for Bitmap creation? MSDN says the Stream needs to exist for the life of the Bitmap
			Bitmap image = Image.FromStream( stream, true, true ) as Bitmap;
			
			return image;
			
		}
		
		public Boolean TryToBitmap(out Bitmap bitmap) {
			
			bitmap = null;
			
			try {
				
				bitmap = ToBitmap();
				return true;
				
			} catch(ArgumentException) {
				return false;
			} catch(ExternalException) {
				return false;
			}
			
		}
		
	}
	
	
	/// <summary>One DIB class to rule them all.</summary>
	public class Dib {
		
		private BitmapFileHeader _fileHeader;
		private BitmapV5Header   _infoHeader;
		
		private Byte[]           _dataOriginal;
		
		public Dib(Byte[] data) {
			
			_dataOriginal = data;
			
			MemoryStream ms = new MemoryStream( data );
			
			Initialise( ms );
			
		}
		
		public Dib(Stream stream) {
			
			Initialise(stream);
			
			stream.Seek(0, SeekOrigin.Begin);
			
			stream.Read( _dataOriginal, 0, (Int32)stream.Length );
			
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
				
			}
			
		}
		
		public DibClass Class { get; private set; }
		
#region Header Member Calculation
		
		private BitmapFileHeader CreateFileHeader() {
			
			Int32 width, height;
			UInt16 bitCount;
			
			UInt32 colorTableSize, dibSize;
			BiCompression compression;
			
			switch(Class) {
				case DibClass.Core:
					
					colorTableSize = ((BitmapCoreHeader)_infoHeader).GetColorTableSize();
					dibSize        = ((BitmapCoreHeader)_infoHeader).bcSize + colorTableSize;
					compression    = BiCompression.BcCore;
					
					width          = ((BitmapCoreHeader)_infoHeader).bcWidth;
					height         = ((BitmapCoreHeader)_infoHeader).bcHeight;
					bitCount       = ((BitmapCoreHeader)_infoHeader).bcBitCount;
					break;
				case DibClass.V3:
					colorTableSize = ((BitmapInfoHeader)_infoHeader).GetColorTableSize();
					dibSize        = ((BitmapInfoHeader)_infoHeader).biSize + colorTableSize;
					compression    = ((BitmapInfoHeader)_infoHeader).biCompression;
					
					width          = ((BitmapInfoHeader)_infoHeader).biWidth;
					height         = ((BitmapInfoHeader)_infoHeader).biHeight;
					bitCount       = ((BitmapInfoHeader)_infoHeader).biBitCount;
					break;
				case DibClass.V4:
					colorTableSize = ((BitmapV4Header)_infoHeader).GetColorTableSize();
					dibSize        = ((BitmapV4Header)_infoHeader).bV4Size + colorTableSize;
					compression    = ((BitmapV4Header)_infoHeader).bV4Compression;
					
					width          = ((BitmapV4Header)_infoHeader).bV4Width;
					height         = ((BitmapV4Header)_infoHeader).bV4Height;
					bitCount       = ((BitmapV4Header)_infoHeader).bV4BitCount;
					break;
				case DibClass.V5:
					colorTableSize = ((BitmapV5Header)_infoHeader).GetColorTableSize();
					dibSize        = ((BitmapV5Header)_infoHeader).bV5Size + colorTableSize;
					compression    = ((BitmapV5Header)_infoHeader).bV5Compression;
					
					width          = ((BitmapV5Header)_infoHeader).bV5Width;
					height         = ((BitmapV5Header)_infoHeader).bV5Height;
					bitCount       = ((BitmapV5Header)_infoHeader).bV5BitCount;
					break;
			}
			
			uint bmfhSize       = (uint)Marshal.SizeOf(typeof(BitmapFileHeader));
			
			switch(compression) {
				case BiCompression.BiRle4:
				case BiCompression.BiRle8:
				case BiCompression.BiJpeg:
				case BiCompression.BiPng:
					// Compressed bitmap, so you cannot calculate the size of the pixeldata
					// So trust the biSizeImage
					
					if( bih.biSizeImage <= 0 ) throw new NotSupportedException("The bitmap's size is not defined.");
					
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
					if( dibSize + 2 == _data.Length) dibSize += 2;
					
					// EDIT: Compare against bitmapData.Length
//					if(dibSize != _data.Length) throw new InvalidDataException("DIB Image Size was not of the expected value.");
					
					break;
				
				case BiCompression.BcCore:
					
					
				
				default:
					
					throw new NotSupportedException("The bitmap uses an unknown biCompression value.");
			}
			
			// Now actually create the FileHeader and calculate the file size
			
			BitmapFileHeader bmfh;
			bmfh.bfType      = 19778; // "BM"
			bmfh.bfSize      = dibSize + bmfhSize;
			bmfh.bfReserved1 = 0;
			bmfh.bfReserved2 = 0;
			bmfh.bfOffBits   = bmfhSize + bih.biSize + colorTableSize;
			
			return bmfh;
			
		}
		
		/// <summary>WidthBytes performs DWORD-aligning of DIB scanlines.  The "bits" parameter is the bit count for the scanline (biWidth * biBitCount), and this macro returns the number of DWORD-aligned bytes needed to hold those bits.</summary>
		protected static uint WidthBytes(uint bits) {
			return (bits + 31) / 32 * 4;
		}
		
		private static BitmapV5Header FillBitmapHeader(BinaryReader rdr, DibClass cls) {
			
			BitmapV5Header v5 = new BitmapV5Header();
			
			v5.bV5Size          = rdr.ReadUInt32();
			v5.bV5Width         = rdr.ReadInt32();
			v5.bV5Height        = rdr.ReadInt32();
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
