using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Anolis.Packages.Native {
	
	[Flags]
	internal enum MoveFileFlags {
		ReplaceExisting    = 0x00000001,
		CopyAllowed        = 0x00000002,
		DelayUntilReboot   = 0x00000004,
		WriteThrough       = 0x00000008,
		CreateHardlink     = 0x00000010,
		FailIfNotTrackable = 0x00000020
	}
	
	[Flags]
	internal enum SpiUpdate : int {
		UpdateIniFile    = 0x01,
		SendWinIniChange = 0x02,
	}
	
	[Flags]
	internal enum RedrawFlags : uint {
		Invalidate      = 0x01,
		InternalPaint   = 0x02,
		Erase           = 0x04,
		
		Validate        = 0x08,
		NoInternalPaint = 0x10,
		NoErase         = 0x20,
		
		NoChildren      = 0x40,
		AllChildren     = 0x80,
		
		UpdateNow       = 0x100,
		EraseNow        = 0x200,
		
		Frame           = 0x400,
		NoFrame         = 0x800
	}
	
#region BinPatch
	
	internal enum WfpResult {
		Success = 0,
		Error   = 1
	}
	
	/// <summary>IMAGE_FILE_MACHINE</summary>
	internal enum MachineType : ushort {
		Unknown    = 0,
		I386       = 0x014c,  // Intel 386.
		R3000      = 0x0162,  // MIPS little-endian, 0x160 big-endian
		R4000      = 0x0166,  // MIPS little-endian
		R10000     = 0x0168,  // MIPS little-endian
		WCEMIPSV2  = 0x0169,  // MIPS little-endian WCE v2
		Alpha      = 0x0184,  // Alpha_AXP
		SH3        = 0x01a2,  // SH3 little-endian
		SH3Dsp     = 0x01a3,
		SH3E       = 0x01a4,  // SH3E little-endian
		SH4        = 0x01a6,  // SH4 little-endian
		SH5        = 0x01a8,  // SH5
		Arm        = 0x01c0,  // ARM Little-Endian
		THUMB      = 0x01c2,
		AM33       = 0x01d3,
		PowerPC    = 0x01F0,  // IBM PowerPC Little-Endian
		PowerPCFP  = 0x01f1,
		IA64       = 0x0200,  // Intel 64
		Mips16     = 0x0266,  // MIPS
		Alpha64    = 0x0284,  // ALPHA64
		MipsFPU    = 0x0366,  // MIPS
		MipsFPU16  = 0x0466,  // MIPS
		Tricore    = 0x0520,  // Infineon
		CEF        = 0x0CEF,
		EBC        = 0x0EBC,  // EFI Byte Code
		Amd64      = 0x8664,  // AMD64 (K8)
		M32R       = 0x9041,  // M32R little-endian
		CEE        = 0xC0EE
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct DosHeader {
		public       UInt16 e_magic;                     // Magic number
		public       UInt16 e_cblp;                      // Bytes on last page of file
		public       UInt16 e_cp;                        // Pages in file
		public       UInt16 e_crlc;                      // Relocations
		public       UInt16 e_cparhdr;                   // Size of header in paragraphs
		public       UInt16 e_minalloc;                  // Minimum extra paragraphs needed
		public       UInt16 e_maxalloc;                  // Maximum extra paragraphs needed
		public       UInt16 e_ss;                        // Initial (relative) SS value
		public       UInt16 e_sp;                        // Initial SP value
		public       UInt16 e_csum;                      // Checksum
		public       UInt16 e_ip;                        // Initial IP value
		public       UInt16 e_cs;                        // Initial (relative) CS value
		public       UInt16 e_lfarlc;                    // File address of relocation table
		public       UInt16 e_ovno;                      // Overlay number
		public fixed UInt16 e_res[4];                    // Reserved UInt16s
		public       UInt16 e_oemid;                     // OEM identifier (for e_oeminfo)
		public       UInt16 e_oeminfo;                   // OEM information; e_oemid specific
		public fixed UInt16 e_res2[10];                  // Reserved UInt16s
		public       UInt32 e_lfanew;                    // File address of new exe header
		
		public static UInt16 DosMagic = 0x5A4D; // "MZ";
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal struct NTHeader {
		public UInt32            Signature;
		public NTImageFileHeader FileHeader;
		public NTHeaders32       OptionalHeader;
		
		public static UInt32 NTSignature = 0x00004550; // "PE00";
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal struct NTImageFileHeader {
		public MachineType Machine;
		public UInt16 NumberOfSections;
		public UInt32 TimeDateStamp;
		public UInt32 PointerToSymbolTable;
		public UInt32 NumberOfSymbols;
		public UInt16 SizeOfOptionalHeader;
		public UInt16 Characteristics;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct NTHeaders32 {
		// Standard fields.
		public UInt16  Magic;
		public Byte    MajorLinkerVersion;
		public Byte    MinorLinkerVersion;
		public UInt32  SizeOfCode;
		public UInt32  SizeOfInitializedData;
		public UInt32  SizeOfUninitializedData;
		public UInt32  AddressOfEntryPoint;
		public UInt32  BaseOfCode;
		public UInt32  BaseOfData;
		// NT additional fields.
		public UInt32  ImageBase;
		public UInt32  SectionAlignment;
		public UInt32  FileAlignment;
		public UInt16  MajorOperatingSystemVersion;
		public UInt16  MinorOperatingSystemVersion;
		public UInt16  MajorImageVersion;
		public UInt16  MinorImageVersion;
		public UInt16  MajorSubsystemVersion;
		public UInt16  MinorSubsystemVersion;
		public UInt32  Win32VersionValue;
		public UInt32  SizeOfImage;
		public UInt32  SizeOfHeaders;
		public UInt32  CheckSum;
		public UInt16  Subsystem;
		public UInt16  DllCharacteristics;
		public UInt32  SizeOfStackReserve;
		public UInt32  SizeOfStackCommit;
		public UInt32  SizeOfHeapReserve;
		public UInt32  SizeOfHeapCommit;
		public UInt32  LoaderFlags;
		public UInt32  NumberOfRvaAndSizes;
//		public fixed ImageDataDirectory DataDirectory[16];
		public fixed Byte DataDirectory[ 16 * 8 ]; // 8 == sizeof(ImageDataDirectory)
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct NTHeaders64 {
		// Standard fields.
		public UInt16  Magic;
		public Byte    MajorLinkerVersion;
		public Byte    MinorLinkerVersion;
		public UInt32  SizeOfCode;
		public UInt32  SizeOfInitializedData;
		public UInt32  SizeOfUninitializedData;
		public UInt32  AddressOfEntryPoint;
		public UInt32  BaseOfCode;
		public UInt64  ImageBase;
		public UInt32  SectionAlignment;
		public UInt32  FileAlignment;
		public UInt16  MajorOperatingSystemVersion;
		public UInt16  MinorOperatingSystemVersion;
		public UInt16  MajorImageVersion;
		public UInt16  MinorImageVersion;
		public UInt16  MajorSubsystemVersion;
		public UInt16  MinorSubsystemVersion;
		public UInt32  Win32VersionValue;
		public UInt32  SizeOfImage;
		public UInt32  SizeOfHeaders;
		public UInt32  CheckSum;
		public UInt16  Subsystem;
		public UInt16  DllCharacteristics;
		public UInt64  SizeOfStackReserve;
		public UInt64  SizeOfStackCommit;
		public UInt64  SizeOfHeapReserve;
		public UInt64  SizeOfHeapCommit;
		public UInt32  LoaderFlags;
		public UInt32  NumberOfRvaAndSizes;
//		public fixed ImageDataDirectory DataDirectory[16];
		public fixed Byte DataDirectory[ 16 * 8 ]; // 8 == sizeof(ImageDataDirectory)
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal struct ImageDataDirectory {
		public UInt32 VirtualAddress;
		public UInt32 Size;
	}
	
#endregion

#region System Restore

	/// <summary>Contains information used by the SRSetRestorePoint function</summary>
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
	internal struct RestorePointInfo {
		public Int32 dwEventType; // The type of event
		public Int32 dwRestorePtType; // The type of restore point
		public Int64 llSequenceNumber; // The sequence number of the restore point
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256 + 1)]
		public String szDescription; // The description to be displayed so the user can easily identify a restore point
	}
	
	/// <summary>Contains status information used by the SRSetRestorePoint function</summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct StateManagerStatus {
		public Int32 nStatus; // The status code
		public Int64 llSequenceNumber; // The sequence number of the restore point
	}
	
#endregion
	
	public enum SpiAction : uint {
		
		None                 = 0,
		
		// Accessibility
		SetFocusBorderWidth  = 0x200F, // pvParam = value
		SetFocusBorderHeight = 0x2011, // pvParam = value
		SetMouseSonar        = 0x101D, // pvParam = TRUE | FALSE
		
		// Desktop
		SetCursors           = 0x0057, // 
		SetDesktopWallpaper  = 0x0014, // pvParam = pSz wallpaper BMP image (JPEG in Vista and later)
		SetDropShadow        = 0x1025, // pvParam = TRUE | FALSE
		SetFlatMenu          = 0x1023, // pvParam = TRUE | FALSE
		
		SetFontSmoothing            = 0x004B, // uiParam = TRUE | FALSE
		SetFontSmoothingType        = 0x200B, // pvParam = 1 for Standard Antialiasing, 2 for ClearType
		SetFontSmoothingContrast    = 0x200D, // pvParam = 1000 to 2200, default is 1400
		SetFontSmoothingOrientation = 0x2013, // pvParam = 0 for BGR, 1 for RGB
		
		// Icons
		IconHorizontalSpacing = 0x000D, // uiParam = value, pvParam = pointer to an int that gets the value
		IconVerticalSpacing   = 0x0018, // uiParam = value, pvParam = pointer to an int that gets the value
		SetIcons              = 0x0058, //
		SetIconTitleWrap      = 0x001A, // uiParam = TRUE | FALSE
		
		// Mouse
		SetMouseTrails        = 0x005D, // uiParam = 0 or 1 to disable, or n > 1 for n cursors
		
		// Menus
		SetMenuDropAlignment  = 0x001C, // uiParam = TRUE for right, FALSE for left
		SetMenuFade           = 0x1013, // pvParam = TRUE | FALSE
		SetMenuShowDelay      = 0x006B, // uiParam = time in miliseconds
		
		// UI Effects
		SetUIEffects          = 0x103F, // pvParam = TRUE | FALSE to enable/disable all UI effects at once
		// I'd define all of them here, but there's little point
		
		// Windows
		SetActiveWindowTracking  = 0x1001, // pvParam = TRUE | FALSE
		SetActiveWndTrkZOrder    = 0x100D, // pvParam = TRUE | FALSE
		SetActiveWndTrkTimeout   = 0x2003, // pvParam = time in miliseconds
		SetBorder                = 0x0006, // uiParam = multiplier width of sizing border
		SetCaretWidth            = 0x2007, // pvParam = width in pixels
		SetDragFullWindows       = 0x0025, // uiParam = TRUE | FALSE
		SetForegroundFlashCount  = 0x2005, // pvParam = # times to flash
		SetForegroundLockTimeout = 0x2001,  // pvParam = timeout in miliseconds
		
		
		// Get
		GetFocusBorderWidth  = 0x200E, // pvParam = Pointer to a UINT that receives the value
		GetFocusBorderHeight = 0x2010, // pvParam = Pointer to a UINT that receives the value
		GetMouseSonar        = 0x101C, // pvParam = Pointer to a BOOL
		
		GetDesktopWallpaper  = 0x0073, // pvParam = Pointer to a string buffer, uiParam = Size of the pvParam buffer
		GetDropShadow        = 0x1024, // pvParam = Pointer to a BOOL
		GetFlatMenu          = 0x1022, // pvParam = Pointer to a BOOL
		
		GetFontSmoothing            = 0x004A, // pvParam = Pointer to a BOOL
		GetFontSmoothingType        = 0x200A, // pvParam = Pointer to a UINT
		GetFontSmoothingContrast    = 0x200C, // pvParam = Pointer to a UINT
		GetFontSmoothingOrientation = 0x2012, // pvParam = Pointer to a UINT
		
		GetMouseTrails           = 0x005E, // pvParam = Pointer to an integer that received the value. If it's <= 1 then it's disabled. >= 2 is enabled with n many trails
		GetMenuDropAlignment     = 0x001B, // pvParam = Pointer to a BOOL (TRUE = right, FALSE = left)
		GetMenuFade              = 0x1012, // pvParam = Pointer to a BOOL
		GetMenuShowDelay         = 0x006A, // pvParam = Pointer to a DWORD, which is the length of the delay
		
		GetUIEffects             = 0x103E, // pvParam = Pointer to a BOOL
		
		GetActiveWindowTracking  = 0x1000, // pvParam = Pointer to a BOOL
		GetBorder                = 0x0005, // pvParam = Pointer to an integer
		GetCaretWidth            = 0x2006, // pvParam = Pointer to a DWORD
		GetDragFullWindows       = 0x0026, // pvParam = Pointer to a BOOL
		GetForegroundFlashCount  = 0x2004, // pvParam = Pointer to a DWORD
		GetForegroundLockTimeout = 0x2000  // pvParam = Pointer to a DWORD
	}
	
}
