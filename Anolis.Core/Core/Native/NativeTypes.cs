using System;
using System.Runtime.InteropServices;

namespace Anolis.Core.Native {
	
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
	
	/// <summary>Defines the memory layout of a RT_GROUP_ICON Win32 resource or *.ico file.</summary>
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct IconDirectory {
		public ushort wReserved;
		public ushort wType;
		public ushort wCount;
//		/// <summary>This is an inline array</summary>
//		[MarshalAs(UnmanagedType.LPArray, SizeParamIndex=2)]
//		public ResourceIconDirectoryMember arEntries;
	}
	
	/// <summary>Helps to defines the memory layout of a RT_GROUP_ICON resource. In particular its wId member indicates the RT_ICON resource for the directory entry.</summary>
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct ResIconDirectoryEntry {
		public byte bWidth;
		public byte bHeight;
		public byte bColorCount;
		public byte bReserved;
		public ushort wPlanes;
		public ushort wBitCount;
		public uint dwBytesInRes;
		public ushort wId;
	}
	
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct IconImage {
		BitmapInfoHeader icHeader;      // DIB header
		RgbQuad[]        icColors;   // Color table
		Byte[]           icXOR;      // DIB bits for XOR mask
		Byte[]           icAND;      // DIB bits for AND mask
	}
	
	/// <summary>Defines the peristent format of an icon directory entry in a .ICO file.</summary>
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct FileIconDirectoryEntry {
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
	
#region Version
	
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct VsVersionInfo {
		public UInt16 wLength;
		public UInt16 wValueLength;
		public UInt16 wType;
		public String szKey;
		public Byte[] Padding1;
		public VsFixedFileInfo Value;
		public Byte[] Padding2;
		public UInt16[] Children;
	}
	
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct VsFixedFileInfo {
		public UInt32 dwSignature;
		public UInt32 dwStrucVersion;
		public UInt32 dwFileVersionMS;
		public UInt32 dwFileVersionLS;
		public UInt32 dwProductVersionMS; 
		public UInt32 dwProductVersionLS; 
		public UInt32 dwFileFlagsMask; 
		public VsFixedFileFlags dwFileFlags; 
		public VsFixedFileOS    dwFileOS; 
		public VsFixedFileType  dwFileType; 
		public UInt32 dwFileSubtype; 
		public UInt32 dwFileDateMS; 
		public UInt32 dwFileDateLS;
	}
	
	// TODO: Get the definitions of these enum members
	// see winver.h
	
	[Flags]
	internal enum VsFixedFileFlags : uint {
		Debug,
		InfoInferred,
		Patched,
		Prerelease,
		PrivateBuild,
		SpecialBuild
	}
	
	[Flags]
	internal enum VsFixedFileOS : uint {
		Dos,
		NT,
		Win16,
		Win32,
		OS216,
		OS232,
		PM16,
		PM32,
		Unknown
	}
	
	internal enum VsFixedFileType : uint {
		Unknown,
		Application,
		Dll,
		Driver,
		Font,
		Vxd,
		StaticLib
	}
	
	internal enum VsFixedFileSubType : uint {
		Unknown,
		// Driver specific
		
		DriverComm,
		DriverPrinter,
		DriverKeyboard,
		DriverLanguage,
		DriverDisplay,
		DriverMouse,
		DriverNetwork,
		DriverSystem,
		DriverInstallable,
		DriverSound,
		DriverVersionedPrinter,
		
		// Font-specific
		FontRaster,
		FontVector,
		FontTrueType
	}
	
	internal struct StringFileInfo {
		public UInt16 wLength;
		public UInt16 wValueLength;
		public UInt16 wType;
		public String szKey; // always contains "StringFileInfo"
		public UInt16[] Padding;
		public StringTable[] Children;
	}
	
		internal struct StringTable {
			public UInt16 wLength;
			public UInt16 wValueLength;
			public UInt16 wType;
			public String szKey; // 8-digit hex number stored as a unicode string of the MS lang id
			public UInt16[] Padding;
			public StringTableEntry[] Children;
		}
		
		internal struct StringTableEntry {
			public UInt16 wLength;
			public UInt16 wValueLength;
			public UInt16 wType;
			public String szKey;
			public UInt16[] Padding;
			public String Value; // represented by an array of WORD
		}
	
	internal struct VarFileInfo {
		public UInt16 wLength;
		public UInt16 wValueLength;
		public UInt16 wType;
		public String szKey; // always contains "VarFileInfo"
		public UInt16[] Padding;
		public Var[] Children;
	}
	
		internal struct Var {
			public UInt16 wLength;
			public UInt16 wValueLength;
			public UInt16 wType;
			public String szKey; // always contains "Translation"
			public UInt16[] Padding;
			public UInt32[] Value;
		}
	
#endregion
	
#region System Info
	
	internal struct SystemInfo {
		
		public ProcessorArchitecture wProcessorArchitecture;
		public UInt16 wReserved;
		public UInt32 dwPageSize;
		public IntPtr lpMinimumApplicationAddress;
		public IntPtr lpMaximumApplicationAddress;
		public UInt32 dwActiveProcessorMask;
		public UInt32 dwNumberOfProcessors;
		public ProcessorType dwProcessorType;
		public UInt32 dwAllocationGranularity;
		public UInt16 wProcessorLevel;
		public UInt16 wProcessorRevision;
		
	}
	
	internal enum ProcessorArchitecture : ushort { // WORD
		Amd64   = 9,
		IA32    = 6,
		IA64    = 0,
		Unknown = 0xFFFF
	}
	
	internal enum ProcessorType : uint { // DWORD
		i386    = 386,
		i486    = 486,
		Pentium = 586,
		IA64    = 2200,
		X8664   = 8664
	}
	
#endregion
	
}