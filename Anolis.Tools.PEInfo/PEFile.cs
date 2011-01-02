using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Anolis.Tools.PEInfo {
	
#region Coff File Header
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CoffFileHeader {
		
		/// <summary>The number that identifies the type of target machine.</summary>
		public Machine Machine;
		/// <summary>The number of sections. This indicates the size of the section table, which immediately follows the headers.</summary>
		public UInt16  NofSections;
		/// <summary>The low 32 bits of the number of seconds since 00:00 January 1, 1970 (a C run-time time_t value), that indicates when the file was created.</summary>
		public UInt32  DateTimeStamp;
		/// <summary>The file offset of the COFF symbol table, or zero if no COFF symbol table is present. This value should be zero for an image because COFF debugging information is deprecated.</summary>
		public UInt32  SymbolTableOffset;
		/// <summary>The file offset of the COFF symbol table, or zero if no COFF symbol table is present. This value should be zero for an image because COFF debugging information is deprecated.</summary>
		public UInt32  NofSymbols;
		/// <summary>The size of the optional header, which is required for executable files but not for object files. This value should be zero for an object file.</summary>
		public UInt16  SizeOfOptionalHeader;
		/// <summary>The flags that indicate the attributes of the file.</summary>
		public CoffFileCharacteristics Characteristics;
		
		public CoffFileHeader(BinaryReader rdr) {
			
			Machine              = (Machine)rdr.ReadUInt16();
			NofSections          = rdr.ReadUInt16();
			DateTimeStamp        = rdr.ReadUInt32();
			SymbolTableOffset    = rdr.ReadUInt32();
			NofSymbols           = rdr.ReadUInt32();
			SizeOfOptionalHeader = rdr.ReadUInt16();
			Characteristics      = (CoffFileCharacteristics)rdr.ReadUInt16();
		}
		
		public DateTime DTDateTimeStamp {
			get {
				DateTime dt = new DateTime( 1970, 1, 1 );
				return dt.AddSeconds( DateTimeStamp );
			}
		}
		
	}
	
	public enum Machine : ushort {
		
		/// <summary>The contents of this field are assumed to be applicable to any machine type.</summary>
		Unknown   = 0x0000,
		/// <summary>Matsushita AM33.</summary>
		AM33      = 0x01D3,
		/// <summary>x64.</summary>
		Amd64     = 0x8664,
		/// <summary>Alpha_AXP</summary>
		Alpha     = 0x0184,
		/// <summary>Alpha64 aka AXP64.</summary>
		Alpha64   = 0x0284,
		/// <summary>ARM little endian.</summary>
		Arm       = 0x01C0,
		/// <summary>ARMv7 (or higher) Thumb mode only.</summary>
		ArmV7     = 0x01C4,
		/// <summary>EFI byte code.</summary>
		Ebc       = 0x0EBC,
		/// <summary>Intel 386 or later processors and compatible processors.</summary>
		I386      = 0x014C,
		/// <summary>Intel Itanium processor family.</summary>
		IA64      = 0x0200,
		/// <summary>Mitsubishi M32R little endian.</summary>
		M32R      = 0x9041,
		/// <summary>MIPS16.</summary>
		Mips16    = 0x0266,
		/// <summary>MIPS with FPU.</summary>
		MipsFpu   = 0x0366,
		/// <summary>MIPS16 with FPU.</summary>
		MipsFpu16 = 0x0466,
		/// <summary>Power PC little endian.</summary>
		PowerPC   = 0x01F0,
		/// <summary>Power PC with floating point support.</summary>
		PowerPCFP = 0x01F1,
		/// <summary>MIPS little-endian.</summary>
		R3000     = 0x0162,
		/// <summary>MIPS little endian.</summary>
		R4000     = 0x0166,
		/// <summary>MIPS little-endian.</summary>
		R10000    = 0x0168,
		/// <summary>Hitachi SH3.</summary>
		SH3       = 0x01A2,
		/// <summary>Hitachi SH3 DSP.</summary>
		SH3Dsp    = 0x01A3,
		/// <summary>Hitachi SH3 E Little-endian</summary>
		SH3E      = 0x01A4,
		/// <summary>Hitachi SH4. Little-endian.</summary>
		SH4       = 0x01A6,
		/// <summary>Hitachi SH5.</summary>
		SH5       = 0x01A8,
		/// <summary>ARM or Thumb (“interworking”).</summary>
		Thumb     = 0x01C2,
		/// <summary>MIPS little-endian WCE v2.</summary>
		WceMipsV2 = 0x0169,
		/// <summary>Infineon TriCore.</summary>
		TriCore   = 0x0520,
		Cef       = 0x0CEF,
		Cee       = 0xC0EE
	}
	
	[Flags]
	public enum CoffFileCharacteristics : ushort {
		/// <summary></summary>
		None                   = 0x0000,
		/// <summary>Image only, Windows CE, and Windows NT® and later. This indicates that the file does not contain base relocations and must therefore be loaded at its preferred base address. If the base address is not available, the loader reports an error. The default behavior of the linker is to strip base relocations from executable (EXE) files.</summary>
		RelocsStripped         = 0x0001,
		/// <summary>Image only. This indicates that the image file is valid and can be run. If this flag is not set, it indicates a linker error.</summary>
		ExecutableImage        = 0x0002,
		/// <summary>COFF line numbers have been removed. This flag is deprecated and should be zero.</summary>
		LineNumbersStripped    = 0x0004,
		/// <summary>COFF symbol table entries for local symbols have been removed. This flag is deprecated and should be zero.</summary>
		LocalSymbolsStripped   = 0x0008,
		/// <summary>Obsolete. Aggressively trim working set. This flag is deprecated for Windows 2000 and later and must be zero.</summary>
		AggressiveWSTrim       = 0x0010,
		/// <summary>Application can handle > 2 GB addresses.</summary>
		LargeAddressAware      = 0x0020,
		/// <summary>This flag is reserved for future use.</summary>
		Reserved               = 0x0040,
		/// <summary>Little endian: the least significant bit (LSB) precedes the most significant bit (MSB) in memory. This flag is deprecated and should be zero.</summary>
		BytesReversedLow       = 0x0080,
		/// <summary>Machine is based on a 32-bit-word architecture.</summary>
		Machine32bit           = 0x0100,
		/// <summary>Debugging information is removed from the image file.</summary>
		DebugStripped          = 0x0200,
		/// <summary>If the image is on removable media, fully load it and copy it to the swap file.</summary>
		RemovableRunFromSwap   = 0x0400,
		/// <summary>If the image is on network media, fully load it and copy it to the swap file.</summary>
		NetRunFromSwap         = 0x0800,
		/// <summary>The image file is a system file, not a user program.</summary>
		System                 = 0x1000,
		/// <summary>The image file is a dynamic-link library (DLL). Such files are considered executable files for almost all purposes, although they cannot be directly run.</summary>
		Dll                    = 0x2000,
		/// <summary>The file should be run only on a uniprocessor machine.</summary>
		UniprocessorSystemOnly = 0x4000,
		/// <summary>Big endian: the MSB precedes the LSB in memory. This flag is deprecated and should be zero.</summary>
		BytesReversedHigh      = 0x8000
	}
	
#endregion
	
#region Coff Optional Header
	
	public enum PE32Magic : ushort {
		PE32     = 0x010B,
		RomImage = 0x0107,
		/// <summary>PE32+ images allow for a 64-bit address space while limiting the image size to 2 gigabytes. Other PE32+ modifications are addressed in their respective sections.</summary>
		PE32Plus = 0x020B
	}
	
	/// <summary>The name is misleading. The header is required for Image files (but optional and totally useless for Object files).</summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct CoffOptionalHeader32 {
		
		/// <summary>The optional header magic number determines whether an image is a PE32 or PE32+ executable.</summary>
		public PE32Magic            Magic;
		/// <summary>Fields that are defined for all implementations of COFF, including UNIX.</summary>
		public CoffStandardFields32 StandardFields;
		/// <summary>Additional fields to support specific features of Windows (for example, subsystems).</summary>
		public CoffWindowsFields32  WindowsFields;
		/// <summary>Address/size pairs for special tables that are found in the image file and are used by the operating system (for example, the import table and the export table).</summary>
		public CoffImageDataDirectory[] DataDirectories;
		
		public CoffOptionalHeader32(PE32Magic magic, BinaryReader rdr) {
			
			Magic = magic;
			
			StandardFields = new CoffStandardFields32( rdr );
			WindowsFields  = new CoffWindowsFields32( rdr );
			
			DataDirectories = new CoffImageDataDirectory[ WindowsFields.NumberOfRvaAndSizes ];
			
			for(int i=0;i<WindowsFields.NumberOfRvaAndSizes;i++) {
				
				DataDirectories[i].VirtualAddress = rdr.ReadUInt32();
				DataDirectories[i].Size           = rdr.ReadUInt32();
			}
		}
		
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CoffStandardFields32 { // must be 28 bytes long
		
		/// <summary>The linker major version number.</summary>
		public Byte   MajorLinkerVersion;
		/// <summary>The linker minor version number.</summary>
		public Byte   MinorLinkerVersion;
		/// <summary>The size of the code (text) section, or the sum of all code sections if there are multiple sections.</summary>
		public UInt32 SizeOfCode;
		/// <summary>The size of the initialized data section, or the sum of all such sections if there are multiple data sections.</summary>
		public UInt32 SizeOfInitializedData;
		/// <summary>The size of the uninitialized data section (BSS), or the sum of all such sections if there are multiple BSS sections.</summary>
		public UInt32 SizeOfUninitializedData;
		/// /// <summary>The address of the entry point relative to the image base when the executable file is loaded into memory. For program images, this is the starting address. For device drivers, this is the address of the initialization function. An entry point is optional for DLLs. When no entry point is present, this field must be zero.</summary>
		public UInt32 AddressOfEntryPoint;
		/// <summary>The address that is relative to the image base of the beginning-of-code section when it is loaded into memory.</summary>
		public UInt32 BaseOfCode;
		/// <summary>The address that is relative to the image base of the beginning-of-data section when it is loaded into memory.</summary>
		public UInt32 BaseOfData;
		
		public CoffStandardFields32(BinaryReader rdr) {
			
			MajorLinkerVersion      = rdr.ReadByte();
			MinorLinkerVersion      = rdr.ReadByte();
			SizeOfCode              = rdr.ReadUInt32();
			SizeOfInitializedData   = rdr.ReadUInt32();
			SizeOfUninitializedData = rdr.ReadUInt32();
			AddressOfEntryPoint     = rdr.ReadUInt32();
			BaseOfCode              = rdr.ReadUInt32();
			BaseOfData              = rdr.ReadUInt32();
		}
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CoffWindowsFields32 { // must be 68 bytes long
		
		/// <summary>The preferred address of the first byte of image when loaded into memory; must be a multiple of 64 K. The default for DLLs is 0x10000000. The default for Windows CE EXEs is 0x00010000. The default for Windows NT, Windows 2000, Windows XP, Windows 95, Windows 98, and Windows Me is 0x00400000.</summary>
		public UInt32 ImageBase;
		/// <summary>The alignment (in bytes) of sections when they are loaded into memory. It must be greater than or equal to FileAlignment. The default is the page size for the architecture.</summary>
		public UInt32 SectionAlignment;
		/// <summary>The alignment factor (in bytes) that is used to align the raw data of sections in the image file. The value should be a power of 2 between 512 and 64 K, inclusive. The default is 512. If the SectionAlignment is less than the architecture’s page size, then FileAlignment must match SectionAlignment.</summary>
		public UInt32 FileAlignment;
		/// <summary>The major version number of the required operating system.</summary>
		public UInt16 MajorOperatingSystemVersion;
		/// <summary>The minor version number of the required operating system.</summary>
		public UInt16 MinorOperatingSystemVersion;
		/// <summary>The major version number of the image.</summary>
		public UInt16 MajorImageVersion;
		/// <summary>The minor version number of the image.</summary>
		public UInt16 MinorImageVersion;
		/// <summary>The major version number of the subsystem.</summary>
		public UInt16 MajorSubsystemVersion;
		/// <summary>The minor version number of the subsystem.</summary>
		public UInt16 MinorSubsystemVersion;
		/// <summary>Reserved, must be zero.</summary>
		public UInt32 Win32VersionValue;
		/// <summary>The size (in bytes) of the image, including all headers, as the image is loaded in memory. It must be a multiple of SectionAlignment.</summary>
		public UInt32 SizeOfImage;
		/// <summary>The combined size of an MS DOS stub, PE header, and section headers rounded up to a multiple of FileAlignment.</summary>
		public UInt32 SizeOfHeaders;
		/// <summary>The image file checksum. The algorithm for computing the checksum is incorporated into IMAGHELP.DLL. The following are checked for validation at load time: all drivers, any DLL loaded at boot time, and any DLL that is loaded into a critical Windows process.</summary>
		public UInt32 CheckSum;
		/// <summary>The subsystem that is required to run this image. For more information, see “Windows PESubsystem” later in this specification.</summary>
		public PESubsystem Subsystem;
		public DllCharacteristics Characteristics;
		/// <summary>The size of the stack to reserve. Only SizeOfStackCommit is committed; the rest is made available one page at a time until the reserve size is reached.</summary>
		public UInt32 SizeOfStackReserve;
		/// <summary>The size of the stack to commit.</summary>
		public UInt32 SizeOfStackCommit;
		/// <summary>The size of the local heap space to reserve. Only SizeOfHeapCommit is committed; the rest is made available one page at a time until the reserve size is reached.</summary>
		public UInt32 SizeOfHeapReserve;
		/// <summary>The size of the local heap space to commit.</summary>
		public UInt32 SizeOfHeapCommit;
		/// <summary>Reserved, must be zero.</summary>
		public UInt32 LoaderFlags;
		/// <summary>The number of data-directory entries in the remainder of the optional header. Each describes a location and size.</summary>
		public UInt32 NumberOfRvaAndSizes;
		
		public CoffWindowsFields32(BinaryReader rdr) {
			
			ImageBase                   = rdr.ReadUInt32();
			SectionAlignment            = rdr.ReadUInt32();
			FileAlignment               = rdr.ReadUInt32();
			MajorOperatingSystemVersion = rdr.ReadUInt16();
			MinorOperatingSystemVersion = rdr.ReadUInt16();
			MajorImageVersion           = rdr.ReadUInt16();
			MinorImageVersion           = rdr.ReadUInt16();
			MajorSubsystemVersion       = rdr.ReadUInt16();
			MinorSubsystemVersion       = rdr.ReadUInt16();
			Win32VersionValue           = rdr.ReadUInt32();
			SizeOfImage                 = rdr.ReadUInt32();
			SizeOfHeaders               = rdr.ReadUInt32();
			CheckSum                    = rdr.ReadUInt32();
			Subsystem                   = (PESubsystem)rdr.ReadUInt16();
			Characteristics             = (DllCharacteristics)rdr.ReadUInt16();
			SizeOfStackReserve          = rdr.ReadUInt32();
			SizeOfStackCommit           = rdr.ReadUInt32();
			SizeOfHeapReserve           = rdr.ReadUInt32();
			SizeOfHeapCommit            = rdr.ReadUInt32();
			LoaderFlags                 = rdr.ReadUInt32();
			NumberOfRvaAndSizes         = rdr.ReadUInt32();
		}
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CoffOptionalHeader32Plus {
		
		/// <summary>The optional header magic number determines whether an image is a PE32 or PE32+ executable.</summary>
		public PE32Magic                Magic;
		/// <summary>Fields that are defined for all implementations of COFF, including UNIX.</summary>
		public CoffStandardFields32Plus StandardFields;
		/// <summary>Additional fields to support specific features of Windows (for example, subsystems).</summary>
		public CoffWindowsFields32Plus  WindowsFields;
		/// <summary>Address/size pairs for special tables that are found in the image file and are used by the operating system (for example, the import table and the export table).</summary>
		public CoffImageDataDirectory[] DataDirectories;
		
		public CoffOptionalHeader32Plus(PE32Magic magic, BinaryReader rdr) {
			
			Magic = magic;
			
			StandardFields = new CoffStandardFields32Plus( rdr );
			WindowsFields  = new CoffWindowsFields32Plus( rdr );
			
			DataDirectories = new CoffImageDataDirectory[ WindowsFields.NumberOfRvaAndSizes ];
			
			for(int i=0;i<WindowsFields.NumberOfRvaAndSizes;i++) {
				
				DataDirectories[i].VirtualAddress = rdr.ReadUInt32();
				DataDirectories[i].Size           = rdr.ReadUInt32();
			}
		}
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CoffStandardFields32Plus { // must be 24 bytes long
		
		/// <summary>The linker major version number.</summary>
		public Byte   MajorLinkerVersion;
		/// <summary>The linker minor version number.</summary>
		public Byte   MinorLinkerVersion;
		/// <summary>The size of the code (text) section, or the sum of all code sections if there are multiple sections.</summary>
		public UInt32 SizeOfCode;
		/// <summary>The size of the initialized data section, or the sum of all such sections if there are multiple data sections.</summary>
		public UInt32 SizeOfInitializedData;
		/// <summary>The size of the uninitialized data section (BSS), or the sum of all such sections if there are multiple BSS sections.</summary>
		public UInt32 SizeOfUninitializedData;
		/// /// <summary>The address of the entry point relative to the image base when the executable file is loaded into memory. For program images, this is the starting address. For device drivers, this is the address of the initialization function. An entry point is optional for DLLs. When no entry point is present, this field must be zero.</summary>
		public UInt32 AddressOfEntryPoint;
		/// <summary>The address that is relative to the image base of the beginning-of-code section when it is loaded into memory.</summary>
		public UInt32 BaseOfCode;
		
		public CoffStandardFields32Plus(BinaryReader rdr) {
			
			MajorLinkerVersion      = rdr.ReadByte();
			MinorLinkerVersion      = rdr.ReadByte();
			SizeOfCode              = rdr.ReadUInt32();
			SizeOfInitializedData   = rdr.ReadUInt32();
			SizeOfUninitializedData = rdr.ReadUInt32();
			AddressOfEntryPoint     = rdr.ReadUInt32();
			BaseOfCode              = rdr.ReadUInt32();
		}
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CoffWindowsFields32Plus { // must be 88 bytes long
		
		/// <summary>The preferred address of the first byte of image when loaded into memory; must be a multiple of 64 K. The default for DLLs is 0x10000000. The default for Windows CE EXEs is 0x00010000. The default for Windows NT, Windows 2000, Windows XP, Windows 95, Windows 98, and Windows Me is 0x00400000.</summary>
		public UInt64 ImageBase;
		/// <summary>The alignment (in bytes) of sections when they are loaded into memory. It must be greater than or equal to FileAlignment. The default is the page size for the architecture.</summary>
		public UInt32 SectionAlignment;
		/// <summary>The alignment factor (in bytes) that is used to align the raw data of sections in the image file. The value should be a power of 2 between 512 and 64 K, inclusive. The default is 512. If the SectionAlignment is less than the architecture’s page size, then FileAlignment must match SectionAlignment.</summary>
		public UInt32 FileAlignment;
		/// <summary>The major version number of the required operating system.</summary>
		public UInt16 MajorOperatingSystemVersion;
		/// <summary>The minor version number of the required operating system.</summary>
		public UInt16 MinorOperatingSystemVersion;
		/// <summary>The major version number of the image.</summary>
		public UInt16 MajorImageVersion;
		/// <summary>The minor version number of the image.</summary>
		public UInt16 MinorImageVersion;
		/// <summary>The major version number of the subsystem.</summary>
		public UInt16 MajorSubsystemVersion;
		/// <summary>The minor version number of the subsystem.</summary>
		public UInt16 MinorSubsystemVersion;
		/// <summary>Reserved, must be zero.</summary>
		public UInt32 Win32VersionValue;
		/// <summary>The size (in bytes) of the image, including all headers, as the image is loaded in memory. It must be a multiple of SectionAlignment.</summary>
		public UInt32 SizeOfImage;
		/// <summary>The combined size of an MS DOS stub, PE header, and section headers rounded up to a multiple of FileAlignment.</summary>
		public UInt32 SizeOfHeaders;
		/// <summary>The image file checksum. The algorithm for computing the checksum is incorporated into IMAGHELP.DLL. The following are checked for validation at load time: all drivers, any DLL loaded at boot time, and any DLL that is loaded into a critical Windows process.</summary>
		public UInt32 CheckSum;
		/// <summary>The subsystem that is required to run this image. For more information, see “Windows PESubsystem” later in this specification.</summary>
		public PESubsystem Subsystem;
		public DllCharacteristics Characteristics;
		/// <summary>The size of the stack to reserve. Only SizeOfStackCommit is committed; the rest is made available one page at a time until the reserve size is reached.</summary>
		public UInt64 SizeOfStackReserve;
		/// <summary>The size of the stack to commit.</summary>
		public UInt64 SizeOfStackCommit;
		/// <summary>The size of the local heap space to reserve. Only SizeOfHeapCommit is committed; the rest is made available one page at a time until the reserve size is reached.</summary>
		public UInt64 SizeOfHeapReserve;
		/// <summary>The size of the local heap space to commit.</summary>
		public UInt64 SizeOfHeapCommit;
		/// <summary>Reserved, must be zero.</summary>
		public LoaderFlags LoaderFlags;
		/// <summary>The number of data-directory entries in the remainder of the optional header. Each describes a location and size.</summary>
		public UInt32 NumberOfRvaAndSizes;
		
		public CoffWindowsFields32Plus(BinaryReader rdr) {
			
			ImageBase                   = rdr.ReadUInt64();
			SectionAlignment            = rdr.ReadUInt32();
			FileAlignment               = rdr.ReadUInt32();
			MajorOperatingSystemVersion = rdr.ReadUInt16();
			MinorOperatingSystemVersion = rdr.ReadUInt16();
			MajorImageVersion           = rdr.ReadUInt16();
			MinorImageVersion           = rdr.ReadUInt16();
			MajorSubsystemVersion       = rdr.ReadUInt16();
			MinorSubsystemVersion       = rdr.ReadUInt16();
			Win32VersionValue           = rdr.ReadUInt32();
			SizeOfImage                 = rdr.ReadUInt32();
			SizeOfHeaders               = rdr.ReadUInt32();
			CheckSum                    = rdr.ReadUInt32();
			Subsystem                   = (PESubsystem)rdr.ReadUInt16();
			Characteristics             = (DllCharacteristics)rdr.ReadUInt16();
			SizeOfStackReserve          = rdr.ReadUInt64();
			SizeOfStackCommit           = rdr.ReadUInt64();
			SizeOfHeapReserve           = rdr.ReadUInt64();
			SizeOfHeapCommit            = rdr.ReadUInt64();
			LoaderFlags                 = (LoaderFlags)rdr.ReadUInt32();
			NumberOfRvaAndSizes         = rdr.ReadUInt32();
		}
	}
	
	public enum CoffImageDirectoriesIndex {
		
		ExportTable           =  0,
		ImportTable           =  1,
		ResourceTable         =  2,
		ExceptionTable        =  3,
		/// <summary>Also known as the Security Directory.</summary>
		CertificateTable      =  4,
		BaseRelocationTable   =  5,
		Debug                 =  6,
		/// <summary>Architecture-specific data.</summary>
		Architecture          =  7,
		/// <summary>RVA of Global Pointer.</summary>
		GlobalPointer         =  8,
		TlsTable              =  9,
		LoadConfigTable       = 10,
		BoundImport           = 11,
		ImportAddressTable    = 12,
		DelayImportDescriptor = 13,
		/// <summary>Also known as the COM Runtime Descriptor, possibly before the CLR had a name?</summary>
		ClrRuntimeHeader      = 14,
		Reserved              = 15
		
	}
	
	/// <summary>Each data directory gives the address and size of a table or string that Windows uses. These data directory entries are all loaded into memory so that the system can use them at run time.</summary>
	public struct CoffImageDataDirectory {
		/// <summary>The RVA of the table. The RVA is the address of the table relative to the base address of the image when the table is loaded.</summary>
		public UInt32 VirtualAddress;
		/// <summary>The size, in bytes, of the table.</summary>
		public UInt32 Size;
	}
	
	/// <summary>Determine which Windows subsystem (if any) is required to run the image.</summary>
	public enum PESubsystem : ushort {
		/// <summary>An unknown subsystem.</summary>
		Unknown              = 0,
		/// <summary>Device drivers and native Windows processes.</summary>
		Native               = 1,
		/// <summary>The Windows graphical user interface (GUI) subsystem.</summary>
		WindowsGui           = 2,
		/// <summary>The Windows character subsystem.</summary>
		WindowsCui           = 3,
		/// <summary>The OS/2 character subsystem (Only applies to OS/2 v1.x images).</summary>
		OS2Cui               = 5,
		/// <summary>The Posix character subsystem.</summary>
		PosixCui             = 7,
		/// <summary>Windows CE.</summary>
		WindowsCEGui         = 9,
		/// <summary>An Extensible Firmware Interface (EFI) application.</summary>
		EfiApplication       = 10,
		/// <summary>An EFI driver with boot services.</summary>
		EfiBootServiceDriver = 11,
		/// <summary>An EFI driver with run-time services.</summary>
		EfiRuntimeDriver     = 12,
		/// <summary>An EFI ROM image.</summary>
		EfiRom               = 13,
		/// <summary>XBOX.</summary>
		Xbox                 = 14
	}
	
	[Flags]
	public enum DllCharacteristics : ushort {
		None                = 0x0000,
		
		/// <summary>Call DLL initialization function when the DLL is first loaded into the process' address space.</summary>
		CallOnDllLoad       = 0x0001,
		/// <summary>Call DLL initialization function when a thread terminates.</summary>
		CallOnThreadExit    = 0x0002,
		/// <summary>Call DLL initialization function when a thread starts up.</summary>
		CallOnThreadStartUp = 0x0004,
		/// <summary>Call DLL initialization function when the DLL exits.</summary>
		CallOnDllExit       = 0x0008,
		
		/// <summary>DLL can be relocated at load time.</summary>
		DynamicBase         = 0x0040,
		/// <summary>Code Integrity checks are enforced.</summary>
		ForceIntegrity      = 0x0080,
		/// <summary>Image is NX compatible.</summary>
		NXCompatible        = 0x0100,
		/// <summary>Isolation aware, but do not isolate the image.</summary>
		NoIsolation         = 0x0200,
		/// <summary>Does not use structured exception (SE) handling. No SE handler may be called in this image.</summary>
		NoSeh               = 0x0400,
		/// <summary>Do not bind the image.</summary>
		NoBind              = 0x0800,
		/// <summary>Reserved.</summary>
		Reserved            = 0x1000,
		/// <summary>A WDM driver.</summary>
		WdmDriver           = 0x2000,
		/// <summary>Terminal Server aware.</summary>
		TerminalServerAware = 0x8000
	}
	
	[Flags]
	public enum LoaderFlags : uint {
		/// <summary>No special loader behavior is defined.</summary>
		None           = 0x0,
		/// <summary>Invoke a breakpoint instruction before starting the process.</summary>
		BreakOnStart   = 0x1,
		/// <summary>Invoke a debugger on the process after it's been loaded.</summary>
		InvokeDebugger = 0x2
	}
	
#endregion
	
#region Sections
	
	public struct SectionTableEntry {
		
		/// <summary>An 8-byte, null-padded UTF-8 encoded string. If the string is exactly 8 characters long, there is no terminating null. For longer names, this field contains a slash (/) that is followed by an ASCII representation of a decimal number that is an offset into the string table. Executable images do not use a string table and do not support section names longer than 8 characters. Long names in object files are truncated if they are emitted to an executable file.</summary>
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		public Byte[] Name;
		/// <summary>The total size of the section when loaded into memory. If this value is greater than SizeOfRawData, the section is zero-padded. This field is valid only for executable images and should be set to zero for object files.</summary>
		public UInt32 VirtualSize;
		/// <summary>For executable images, the address of the first byte of the section relative to the image base when the section is loaded into memory. For object files, this field is the address of the first byte before relocation is applied; for simplicity, compilers should set this to zero. Otherwise, it is an arbitrary value that is subtracted from offsets during relocation.</summary>
		public UInt32 VirtualAddress;
		/// <summary>The size of the section (for object files) or the size of the initialized data on disk (for image files). For executable images, this must be a multiple of FileAlignment from the optional header. If this is less than VirtualSize, the remainder of the section is zero-filled. Because the SizeOfRawData field is rounded but the VirtualSize field is not, it is possible for SizeOfRawData to be greater than VirtualSize as well. When a section contains only uninitialized data, this field should be zero.</summary>
		public UInt32 SizeOfRawData;
		/// <summary>The file pointer to the first page of the section within the COFF file. For executable images, this must be a multiple of FileAlignment from the optional header. For object files, the value should be aligned on a 4 byte boundary for best performance. When a section contains only uninitialized data, this field should be zero.</summary>
		public UInt32 PointerToRawData;
		/// <summary>The file pointer to the beginning of relocation entries for the section. This is set to zero for executable images or if there are no relocations.</summary>
		public UInt32 PointerToRelocations;
		/// <summary>The file pointer to the beginning of line-number entries for the section. This is set to zero if there are no COFF line numbers. This value should be zero for an image because COFF debugging information is deprecated.</summary>
		public UInt32 PointerToLineNumbers;
		/// <summary>The number of relocation entries for the section. This is set to zero for executable images.</summary>
		public UInt16 NofRelocations;
		/// <summary>The number of line-number entries for the section. This value should be zero for an image because COFF debugging information is deprecated.</summary>
		public UInt16 NofLineNumbers;
		/// <summary>The flags that describe the characteristics of the section.</summary>
		public SectionCharacteristics Characteristics; 
		
		public SectionTableEntry(BinaryReader rdr) {
			
			Name                 = rdr.ReadBytes(8);
			VirtualSize          = rdr.ReadUInt32();
			VirtualAddress       = rdr.ReadUInt32();
			SizeOfRawData        = rdr.ReadUInt32();
			PointerToRawData     = rdr.ReadUInt32();
			PointerToRelocations = rdr.ReadUInt32();
			PointerToLineNumbers = rdr.ReadUInt32();
			NofRelocations       = rdr.ReadUInt16();
			NofLineNumbers       = rdr.ReadUInt16();
			Characteristics      = (SectionCharacteristics)rdr.ReadUInt32();
		}
		
		public String NameString {
			get {
				// for some reason, Array.IndexOf always returns -1 when working with byte[]
				int nullIdx = Name.Length;
				for(int i=Name.Length-1;i>=0;i--) if( Name[i] == 0 ) nullIdx = i;
				return Encoding.UTF8.GetString( Name, 0, nullIdx );
			} 
		}
		
	}
	
	[Flags]
	public enum SectionCharacteristics : uint {
		None = 0x00000000,
		
		/// <summary>The section should not be padded to the next boundary. This flag is obsolete and is replaced by IMAGE_SCN_ALIGN_1BYTES. This is valid only for object files.</summary>
		TypeNoPadding = 0x8,
		
		/// <summary>The section contains executable code.</summary>
		ContainsCode = 0x20,
		/// <summary>The section contains initialized data.</summary>
		ContainsInitializedData = 0x40,
		/// <summary>The section contains uninitialized data.</summary>
		ContainsUninitializedData = 0x80,
		
		/// <summary>Reserved for future use.</summary>
		LinkOther  = 0x0100,
		/// <summary>The section contains comments or other information. The .drectve section has this type. This is valid for object files only.</summary>
		LinkInfo   = 0x0200,
		/// <summary>The section will not become part of the image. This is valid only for object files.</summary>
		LinkRemove = 0x0800,
		/// <summary>The section contains COMDAT data. For more information, see section 5.5.6, “COMDAT Sections (Object Only).” This is valid only for object files.</summary>
		LinkComdat = 0x1000,
		
		/// <summary>The section contains data referenced through the global pointer (GP).</summary>
		GPRel = 0x8000,
		
		/// <summary>Reserved for future use.</summary>
		MemoryPurgable = 0x20000,
		/// <summary>For ARM machine types, the section contains Thumb code.  Reserved for future use with other machine types.</summary>
		Memory16Bit    = 0x20000,
		/// <summary>Reserved for future use.</summary>
		MemoryLocked   = 0x40000,
		/// <summary>Reserved for future use.</summary>
		MemoryPreload  = 0x80000,
		
		/// <summary>Align data on a 1-byte boundary. Valid only for object files.</summary>
		Align1Byte    = 0x00100000,
		/// <summary>Align data on a 2-byte boundary. Valid only for object files.</summary>
		Align2Bytes   = 0x00200000,
		/// <summary>Align data on a 4-byte boundary. Valid only for object files.</summary>
		Align4Bytes   = 0x00300000,
		/// <summary>Align data on a 8-byte boundary. Valid only for object files.</summary>
		Align8Bytes   = 0x00400000,
		/// <summary>Align data on a 16-byte boundary. Valid only for object files.</summary>
		Align16Byte   = 0x00500000,
		/// <summary>Align data on a 32-byte boundary. Valid only for object files.</summary>
		Align32Byte   = 0x00600000,
		/// <summary>Align data on a 64-byte boundary. Valid only for object files.</summary>
		Align64Byte   = 0x00700000,
		/// <summary>Align data on a 128-byte boundary. Valid only for object files.</summary>
		Align128Byte  = 0x00800000,
		/// <summary>Align data on a 256-byte boundary. Valid only for object files.</summary>
		Align256Byte  = 0x00900000,
		/// <summary>Align data on a 512-byte boundary. Valid only for object files.</summary>
		Align512Byte  = 0x00A00000,
		/// <summary>Align data on a 1024-byte boundary. Valid only for object files.</summary>
		Align1024Byte = 0x00B00000,
		/// <summary>Align data on a 2048-byte boundary. Valid only for object files.</summary>
		Align2048Byte = 0x00C00000,
		/// <summary>Align data on a 4096-byte boundary. Valid only for object files.</summary>
		Align4096Byte = 0x00D00000,
		/// <summary>Align data on a 8192-byte boundary. Valid only for object files.</summary>
		Align8192Byte = 0x00E00000,
		
		/// <summary>The section contains extended relocations.</summary>
		/// <remarks>IThis indicates that the count of relocations for the section exceeds the 16 bits that are reserved for it in the section header. If the bit is set and the NumberOfRelocations field in the section header is 0xffff, the actual relocation count is stored in the 32-bit VirtualAddress field of the first relocation. It is an error if this is set and there are fewer than 0xffff relocations in the section.</remarks>
		LinkNRelocOvfl    = 0x01000000,
		
		/// <summary>The section can be discarded as needed.</summary>
		MemoryDiscardable = 0x02000000,
		/// <summary>The section cannot be cached.</summary>
		MemoryNotCached   = 0x04000000,
		/// <summary>The section is not pageable.</summary>
		MemoryNotPaged    = 0x08000000,
		/// <summary>The section can be shared in memory.</summary>
		MemoryShared      = 0x10000000,
		/// <summary>The section can be executed as code.</summary>
		MemoryExecute     = 0x20000000,
		/// <summary>The section can be read.</summary>
		MemoryRead        = 0x40000000,
		/// <summary>The section can be written to.</summary>
		MemoryWrite       = 0x80000000,
	}
	
#endregion
	
	public class PEFile {
		
		public UInt64 ImageLength;
		
		public DosHeader DosHeader;
		public UInt32    PEOffset;
		public Byte[]    PESignature; // The PESignature is not part of the Standard COFF Header, despite appearing right before it.
		
		public CoffFileHeader            CoffFileHeader;
		
		public Byte[]                    OptionalHeader;
		public CoffOptionalHeader32?     OptionalHeader32;
		public CoffOptionalHeader32Plus? OptionalHeader32Plus;
		
		public SectionTableEntry[]       SectionTable;
		
		/////////////////////////////////
		
		public PEFile(String fileName) {
			
			ImageLength = (UInt64)new FileInfo( fileName ).Length;
			
			using(FileStream fs = new FileStream( fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 0x400, FileOptions.None ) ) {
				
				Load( fs );
			}
			
		}
		
		public PEFile(UInt64 length, Stream stream) {
			
			ImageLength = length;
			
			Load( stream );
		}
		
		private void Load(Stream stream) {
			
			BinaryReader rdr = new BinaryReader(stream);
			
			////////////////////////////////////
			// The DOS Stub and PE Offset marker
			
			DosHeader = new DosHeader( rdr );
			
			PEOffset = DosHeader.NewExeHeaderAddress; // this is at 0x3C
			
			rdr.BaseStream.Seek( PEOffset, SeekOrigin.Begin );
			
			////////////////////////////////////
			// The COFF File Header
			
			PESignature = rdr.ReadBytes(4);
			
			if( !IsPESignature( PESignature ) )
				throw new FormatException("The specified file does not contain a PE signature.");
			
			CoffFileHeader = new CoffFileHeader( rdr );
			
			long offsetEndOfCoffFileHeader = rdr.BaseStream.Position;
			
			////////////////////////////////////
			// The Optional Header
			
			PE32Magic magic = (PE32Magic)rdr.ReadUInt16();
			switch(magic) {
				case PE32Magic.PE32:
					
					// Ensure the Optional Header is big enough for the minimum amount of data
					// Standard fields: 28 bytes
					// Windows fields: 68 bytes
					// Total: 92 bytes
					if( CoffFileHeader.SizeOfOptionalHeader < 96 ) {
						String msg = String.Format("The specified file's declared Optional Header size of {0} bytes is smaller than the minimum of 96 required bytes.", CoffFileHeader.SizeOfOptionalHeader);
						throw new FormatException(msg);
					}
					
					OptionalHeader32 = new CoffOptionalHeader32( magic, rdr );
					
					break;
				case PE32Magic.PE32Plus:
					
					// Standard fields: 24 bytes
					// Windows fields: 88 bytes
					// Total: 112 bytes
					if( CoffFileHeader.SizeOfOptionalHeader < 112 ) {
						String msg = String.Format("The specified file's declared Optional Header size of {0} bytes is smaller than the minimum of 112 required bytes.", CoffFileHeader.SizeOfOptionalHeader);
						throw new FormatException(msg);
					}
					
					OptionalHeader32Plus = new CoffOptionalHeader32Plus( magic, rdr );
					
					break;
				case PE32Magic.RomImage:
				default:
					OptionalHeader = rdr.ReadBytes( CoffFileHeader.SizeOfOptionalHeader );
					break;
			}
			
			long offsetEndOfOptionalHeader = rdr.BaseStream.Position;
			
			// by now we're at the end of the Optional Header, but let's make sure
			
			if( offsetEndOfOptionalHeader != offsetEndOfCoffFileHeader + CoffFileHeader.SizeOfOptionalHeader ) {
				// error condition
			}
			
			///////////////////////////////
			// The Section Table
			
			SectionTable = new SectionTableEntry[ CoffFileHeader.NofSections ];
			for(int i=0;i<SectionTable.Length;i++) {
				
				SectionTable[i] = new SectionTableEntry( rdr );
			}
			
		}
		
		private static Boolean IsPESignature(Byte[] signature) {
			
			if( signature == null ) throw new ArgumentNullException("signature");
			if( signature.Length != 4 ) throw new ArgumentException("Signature must be 4 bytes long.");
			
			return
				signature[0] == 'P' &&
				signature[1] == 'E' &&
				signature[2] == 0x00 &&
				signature[3] == 0x00;
			
		}
		
		/// <summary>Converts a Relative Virtual Address to a location in the file.</summary>
		/// <param name="section">This is the section that contains the destination of the file pointer and virtual address. This can be null if the RVA points to a part of the image not within a section (e.g. part of the DOS/COFF/PE headers) or beyond the sections. This is a very rare occurence and might possibly be illegal.</param>
		/// <returns>An offset from the beginning of the image file.</returns>
		public UInt32 RvaToFileOffset(UInt32 rva, out SectionTableEntry? section) {
			
			// Virtual addresses are defined by the PE's Sections
			// So use that information to do the conversion
			
			// First off, get the SectionTableEntry that contains the RVA
			
			section = GetSectionEntry( rva );
			
			if( section == null ) {
				// if the RVA points to something outside of a section then it's a file offset, apparently
				// http://www.reverse-engineering.net/viewtopic.php?f=7&t=3312
				
				return rva;
				
			}
			
			if( ImageBase > UInt32.MaxValue )
				throw new NotSupportedException("PE File's ImageBase address is beyond 32 bits and is not currently supported.");
			
			/////////////////////////////////
			
			// NOTE: This method of calculating the file offset is the same as used in PELib, it seems the other templated approach I saw online (that uses ImageBase) is incorrect.
			
			UInt32 rvaFromSectionStart = rva - section.Value.VirtualAddress;
			UInt32 fileOffset          = section.Value.PointerToRawData + rvaFromSectionStart;
			
			return fileOffset;
		}
		
		/// <summary>Gets the SectionTableEntry that contains the specified RVA.</summary>
		private SectionTableEntry? GetSectionEntry(UInt32 rva) {
			
			for(int i=0;i<this.SectionTable.Length;i++) {
				
				SectionTableEntry sectionEntry = this.SectionTable[i];
				
				UInt32 maxSize = Math.Max( sectionEntry.VirtualSize, sectionEntry.SizeOfRawData ); // this also solves the Watcom Linker issue where VirtualSize == 0
				
				// Is the RVA within this section?
				if( rva >= sectionEntry.VirtualAddress && rva < sectionEntry.VirtualAddress + maxSize ) {
					
					return sectionEntry;
				}
				
			}
			
			return null;
		}
		
		private UInt64 ImageBase {
			get {
				if(      this.OptionalHeader32     != null ) return this.OptionalHeader32.Value.WindowsFields.ImageBase;
				else if( this.OptionalHeader32Plus != null ) return this.OptionalHeader32Plus.Value.WindowsFields.ImageBase;
				return 0;
			}
		}
		
	}
	
}
