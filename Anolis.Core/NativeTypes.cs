using System;
using System.Runtime.InteropServices;

namespace Anolis.Core.NativeTypes {
	
	// word  = ushort = UInt16
	// dword = uint   = UInt32
	// long  = int    = Int32
	
#region Bitmaps
	
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct BitmapFileHeader { 
		public ushort bfType;
		public uint   bfSize;
		public ushort bfReserved1;
		public ushort bfReserved2;
		public uint   bfOffBits;
	}
	
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct BitmapInfo {
		public BitmapInfoHeader bmiHeader;
		/// <summary>This is an inline array</summary>
		public RgbQuad bmiColors;
	}
	
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct BitmapInfoHeader {
		public uint biSize;
		public int biWidth;
		public int biHeight;
		public ushort biPlanes;
		public ushort biBitCount;
		public uint biCompression;
		public uint biSizeImage;
		public int biXPelsPerMeter;
		public int biYPelsPerMeter;
		public uint biClrUsed;
		public uint biClrImportant;
	}
	
	internal enum BiCompression {
		BiRgb       = 0,
		BiRle8      = 1,
		BiRle4      = 2,
		BiBitFields = 3,
		BiJpeg      = 4,
		BiPng       = 5
	}
	
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	internal struct RgbQuad {
		public byte rgbBlue;
		public byte rgbGreen;
		public byte rgbRed;
		public byte rgbReserved;
	}
	
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	internal struct RgbTriple {
		public byte rgbBlue;
		public byte rgbGreen;
		public byte rgbRed;
	}
	
#endregion
	
#region Icons and Cursors
	
	/// <summary>Helps to defines the memory layout of a RT_GROUP_ICON resource. In particular its wId member indicates the RT_ICON resource for the directory entry.</summary>
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct MemoryIconDirEntry {
		public byte bWidth;
		public byte bHeight;
		public byte bColorCount;
		public byte bReserved;
		public ushort wPlanes;
		public ushort wBitCount;
		public uint dwBytesInRes;
		public ushort wId;
	}
	
	/// <summary>Defines the memory layout of a RT_GROUP_ICON Win32 resource.</summary>
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct MemoryIconDir {
		public ushort wReserved;
		public ushort wType;
		public ushort wCount;
		/// <summary>This is an inline array</summary>
		public MemoryIconDirEntry arEntries;
	}
	
	/// <summary>Defines the peristent format of an icon directory entry in a .ICO file.</summary>
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct FileIconDirEntry {
		public byte bWidth;
		public byte bHeight;
		public byte bColorCount;
		public byte bReserved;
		public ushort wPlanes;
		public ushort wBitCount;
		public uint dwBytesInRes;
		public uint dwImageOffset;
	}

#endregion
	
}