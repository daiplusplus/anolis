using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

using InvalidDataException = System.IO.InvalidDataException;

using Anolis.Core.NativeTypes;

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
		
		/// <summary>WidthBytes performs DWORD-aligning of DIB scanlines.  The "bits" parameter is the bit count for the scanline (biWidth * biBitCount), and this macro returns the number of DWORD-aligned bytes needed to hold those bits.</summary>
		protected static uint WidthBytes(uint bits) {
			return (bits + 31) / 32 * 4;
		}
		
		protected uint GetPaletteSize() {
			
			BitmapInfoHeader bmih = InfoHeader;
			
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
		protected static Boolean IsBitmapInfoHeader(BitmapInfoHeader bmih) {
			return bmih.biSize >= 40;
		}
		
		protected static ushort DibNumColors(BitmapInfoHeader bmih) {
			
			ushort bitCount;
			
			/*  If this is a Windows-style DIB, the number of colors in the color table can be less than the number of bits per pixel
				allows for (i.e. lpbi->biClrUsed can be set to some value). If this is the case, return the appropriate value. */
			if( IsBitmapInfoHeader(bmih) ) {
				
				if( bmih.biClrUsed != 0 ) return (ushort)bmih.biClrUsed;
				
				bitCount = bmih.biBitCount;
				
			} else {
				
				// Here MFC casts the BitmapInfoHeader as BitmapCoreHeader... Which I'm not going to do...
				throw new NotImplementedException();
			}
			
			/* Calculate the number of colors in the color table based on the number of bits per pixel for the DIB. */
			
			// alternatively do a left bitshift, but .NET makes this hard with unsigned types so nvm
			switch(bitCount) {
				case 1:  return 2;
				case 4:  return 16;
				case 8:  return 256;
				default: return 0;
			}
			
		}
		
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
		
		private BitmapFileHeader CreateFileHeader() {
			
			BitmapInfoHeader bih = this.InfoHeader;
			
			uint colorTableSize = GetPaletteSize();
			uint bmfhSize       = (uint)sizeOfBitmapFileHeader;
			
			uint dibSize        = bih.biSize + colorTableSize;
			
			switch((BiCompression)bih.biCompression) {
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
					if( dibSize + 2 == _data.Length) dibSize += 2;
					
					// EDIT: Compare against bitmapData.Length
//					if(dibSize != _data.Length) throw new InvalidDataException("DIB Image Size was not of the expected value.");
					
					break;
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
	
}
