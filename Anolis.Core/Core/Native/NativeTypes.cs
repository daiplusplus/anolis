using System;
using System.Runtime.InteropServices;
using System.IO;

namespace Anolis.Core.Native {
	
	// word  = ushort = UInt16
	// dword = uint   = UInt32
	// long  = int    = Int32

#region Bitmaps
	
	namespace Dib {
		
		/// <remarks>OS/2 DIB Header. bcBitCount can be one of 1, 4, 8, or 24</remarks>
		[StructLayout(LayoutKind.Sequential, Pack=2)]
		internal struct BitmapCoreHeader {
			public UInt32 bcSize;
			public UInt16 bcWidth;
			public UInt16 bcHeight;
			public UInt16 bcPlanes;
			public UInt16 bcBitCount;
			
			public BitmapCoreHeader(BinaryReader rdr) {
				bcSize     = rdr.ReadUInt32();
				bcWidth    = rdr.ReadUInt16();
				bcHeight   = rdr.ReadUInt16();
				bcPlanes   = rdr.ReadUInt16();
				bcBitCount = rdr.ReadUInt16();
			}
		}
		
		/// <remarks>Windows 3.x DIB Header</remarks>
		[StructLayout(LayoutKind.Sequential, Pack=2)]
		internal struct BitmapInfoHeader {
			
			public UInt32 biSize;
			public Int32  biWidth;
			public Int32  biHeight;
			public UInt16 biPlanes;
			public UInt16 biBitCount;
			public BiCompression biCompression;
			public UInt32 biSizeImage;
			public Int32  biXPelsPerMeter;
			public Int32  biYPelsPerMeter;
			public UInt32 biClrUsed;
			public UInt32 biClrImportant;
			
			public BitmapInfoHeader(BinaryReader rdr) {
				biSize          = rdr.ReadUInt32();
				biWidth         = rdr.ReadInt32();
				biHeight        = rdr.ReadInt32();
				biPlanes        = rdr.ReadUInt16();
				biBitCount      = rdr.ReadUInt16();
				biCompression   = (BiCompression)rdr.ReadUInt32();
				biSizeImage     = rdr.ReadUInt32();
				biXPelsPerMeter = rdr.ReadInt32();
				biYPelsPerMeter = rdr.ReadInt32();
				biClrUsed       = rdr.ReadUInt32();
				biClrImportant  = rdr.ReadUInt32();
			}
		}
		
		/// <remarks>Windows 95/ NT 4.0 DIB Header</remarks>
		[StructLayout(LayoutKind.Sequential,Pack=2)]
		internal struct BitmapV4Header {
			
			public UInt32 bV4Size;
			public Int32  bV4Width;
			public Int32  bV4Height;
			public UInt16 bV4Planes;
			public UInt16 bV4BitCount;
			public BiCompression bV4Compression;
			public UInt32 bV4SizeImage;
			public Int32  bV4XPelsPerMeter;
			public Int32  bV4YPelsPerMeter;
			public UInt32 bV4ClrUsed;
			public UInt32 bV4ClrImportant;
			public UInt32 bV4RedMask;
			public UInt32 bV4GreenMask;
			public UInt32 bV4BlueMask;
			public UInt32 bV4AlphaMask;
			public UInt32 bV4CSType;
			public CieXyzTriple bV4Endpoints;
			public UInt32 bV4GammaRed;
			public UInt32 bV4GammaGreen;
			public UInt32 bV4GammaBlue;
			
			public BitmapV4Header(BinaryReader rdr) {
				bV4Size          = rdr.ReadUInt32();
				bV4Width         = rdr.ReadInt32();
				bV4Height        = rdr.ReadInt32();
				bV4Planes        = rdr.ReadUInt16();
				bV4BitCount      = rdr.ReadUInt16();
				bV4Compression   = (BiCompression)rdr.ReadUInt32();
				bV4SizeImage     = rdr.ReadUInt32();
				bV4XPelsPerMeter = rdr.ReadInt32();
				bV4YPelsPerMeter = rdr.ReadInt32();
				bV4ClrUsed       = rdr.ReadUInt32();
				bV4ClrImportant  = rdr.ReadUInt32();
				bV4RedMask       = rdr.ReadUInt32();
				bV4GreenMask     = rdr.ReadUInt32();
				bV4BlueMask      = rdr.ReadUInt32();
				bV4AlphaMask     = rdr.ReadUInt32();
				bV4CSType        = rdr.ReadUInt32();
				bV4Endpoints     = new CieXyzTriple(rdr);
				bV4GammaRed      = rdr.ReadUInt32();
				bV4GammaGreen    = rdr.ReadUInt32();
				bV4GammaBlue     = rdr.ReadUInt32();
			}
		}
		
		/// <remarks>Windows 98 / 2000 DIB Header</remarks>
		[StructLayout(LayoutKind.Sequential,Pack=2)]
		internal struct BitmapV5Header {
			
			public UInt32 bV5Size;
			public Int32  bV5Width;
			public Int32  bV5Height;
			public UInt16 bV5Planes;
			public UInt16 bV5BitCount;
			public BiCompression bV5Compression;
			public UInt32 bV5SizeImage;
			public Int32  bV5XPelsPerMeter;
			public Int32  bV5YPelsPerMeter;
			public UInt32 bV5ClrUsed;
			public UInt32 bV5ClrImportant;
			public UInt32 bV5RedMask;
			public UInt32 bV5GreenMask;
			public UInt32 bV5BlueMask;
			public UInt32 bV5AlphaMask;
			public UInt32 bV5CSType;
			public CieXyzTriple bV5Endpoints;
			public UInt32 bV5GammaRed;
			public UInt32 bV5GammaGreen;
			public UInt32 bV5GammaBlue;
			public UInt32 bV5Intent;
			public UInt32 bV5ProfileData;
			public UInt32 bV5ProfileSize;
			public UInt32 bV5Reserved;
			
			public BitmapV5Header(BinaryReader rdr) {
				bV5Size          = rdr.ReadUInt32();
				bV5Width         = rdr.ReadInt32();
				bV5Height        = rdr.ReadInt32();
				bV5Planes        = rdr.ReadUInt16();
				bV5BitCount      = rdr.ReadUInt16();
				bV5Compression   = (BiCompression)rdr.ReadUInt32();
				bV5SizeImage     = rdr.ReadUInt32();
				bV5XPelsPerMeter = rdr.ReadInt32();
				bV5YPelsPerMeter = rdr.ReadInt32();
				bV5ClrUsed       = rdr.ReadUInt32();
				bV5ClrImportant  = rdr.ReadUInt32();
				bV5RedMask       = rdr.ReadUInt32();
				bV5GreenMask     = rdr.ReadUInt32();
				bV5BlueMask      = rdr.ReadUInt32();
				bV5AlphaMask     = rdr.ReadUInt32();
				bV5CSType        = rdr.ReadUInt32();
				bV5Endpoints     = new CieXyzTriple(rdr);
				bV5GammaRed      = rdr.ReadUInt32();
				bV5GammaGreen    = rdr.ReadUInt32();
				bV5GammaBlue     = rdr.ReadUInt32();
				bV5Intent        = rdr.ReadUInt32();
				bV5ProfileData   = rdr.ReadUInt32();
				bV5ProfileSize   = rdr.ReadUInt32();
				bV5Reserved      = rdr.ReadUInt32();
			}
			
		}
		
		[StructLayout(LayoutKind.Sequential,Pack=2)]
		internal struct CieXyzTriple {
			public CieXyz cieXyzRed;
			public CieXyz cieXyzGreen;
			public CieXyz cieXyzBlue;
			
			public CieXyzTriple(BinaryReader rdr) {
				cieXyzRed   = new CieXyz(rdr);
				cieXyzGreen = new CieXyz(rdr);
				cieXyzBlue  = new CieXyz(rdr);
			}
		}
		
		[StructLayout(LayoutKind.Sequential,Pack=2)]
		internal struct CieXyz {
			// WinGdi.h:
			// typedef long            FXPT2DOT30, FAR *LPFXPT2DOT30;
			//   FXPT2DOT30 ciexyzX;
			public Int32 cieXyzX;
			public Int32 cieXyzY;
			public Int32 cieXyzZ;
			
			public CieXyz(BinaryReader rdr) {
				cieXyzX = rdr.ReadInt32();
				cieXyzY = rdr.ReadInt32();
				cieXyzZ = rdr.ReadInt32();
			}
		}
		
		[StructLayout(LayoutKind.Sequential, Pack=2)]
		internal struct BitmapFileHeader { 
			public UInt16 bfType;
			public UInt32 bfSize;
			public UInt16 bfReserved1;
			public UInt16 bfReserved2;
			public UInt32 bfOffBits;
			
			public BitmapFileHeader(BinaryReader rdr) {
				
				bfType      = rdr.ReadUInt16();
				bfSize      = rdr.ReadUInt32();
				bfReserved1 = rdr.ReadUInt16();
				bfReserved2 = rdr.ReadUInt16();
				bfOffBits   = rdr.ReadUInt32();
			}
			
			public void Write(BinaryWriter wtr) {
				wtr.Write( bfType );
				wtr.Write( bfSize );
				wtr.Write( bfReserved1 );
				wtr.Write( bfReserved2 );
				wtr.Write( bfOffBits );
			}
			
		}
		
		internal enum BiCompression : uint {
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
	
//	[StructLayout(LayoutKind.Sequential, Pack=2)]
//	internal struct IconImage {
//		BitmapInfoHeader icHeader;      // DIB header
//		RgbQuad[]        icColors;   // Color table
//		Byte[]           icXOR;      // DIB bits for XOR mask
//		Byte[]           icAND;      // DIB bits for AND mask
//	}
	
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