using System;
using System.Runtime.InteropServices;

namespace Anolis.Core.Win32.NativeTypes {
	
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct BitmapInfoHeader {
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
	
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct RgbQuad {
		public byte rgbBlue;
		public byte rgbGreen;
		public byte rgbRed;
		public byte rgbReserved;
	}
	
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct BitmapInfo {
		public BitmapInfoHeader bmiHeader;
		/// <summary>This is an inline array</summary>
		public RgbQuad bmiColors;
	}
	
	/// <summary>Helps to defines the memory layout of a RT_GROUP_ICON resource. In particular its wId member indicates the RT_ICON resource for the directory entry.</summary>
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct IconDirectoryEntry {
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
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct IconDirectory {
		public ushort wReserved;
		public ushort wType;
		public ushort wCount;
		/// <summary>This is an inline array</summary>
		public IconDirectoryEntry[] arEntries;
	}
	
	/// <summary>Defines the peristent format of an icon directory entry in a .ICO file.</summary>
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct FileIconDirEntry {
		public byte bWidth;
		public byte bHeight;
		public byte bColorCount;
		public byte bReserved;
		public ushort wPlanes;
		public ushort wBitCount;
		public uint dwBytesInRes;
		public uint dwImageOffset;
	}
	
}
