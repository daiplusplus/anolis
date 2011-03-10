using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Anolis.Tools.PEInfo {
	
	/// <summary>Or more properly known as 'Segmented EXE Header'.</summary>
	public struct NEHeader {
		
		/// <summary>Signature word. Must be "NE" or 0x4E45</summary>
		public UInt16  Magic; // must equal 0x4E 0x45
		/// <summary>Version number of the linker.</summary>
		public Byte    LinkerVersion;
		/// <summary>Revision number of the linker.</summary>
		public Byte    LinkerRevision;
		/// <summary>Entry Table file offset, relative to the beginning of the segmented EXE header.</summary>
		public UInt16  EntryTableOffset;
		/// <summary>Number of bytes in the entry table.</summary>
		public UInt16  EntryTableSize;
		/// <summary>32-bit CRC of entire contents of file. These words are taken as 00 during the calculation.</summary>
		public UInt32  Crc32;
		/// <summary>Flag word.</summary>
		public NEFlags Flags;
		/// <summary>Segment number of automatic data segment. This value is set to zero if SINGLEDATA and MULTIPLEDATA flag bits are clear, NOAUTODATA is indicated in the flags word. A Segment number is an index into the module's segment table. The first entry in the segment table is segment number 1.</summary>
		public UInt16  AutoDataSegmentNumber;
		/// <summary>Initial size, in bytes, of dynamic heap added to the data segment. This value is zero if no initial local heap is allocated.</summary>
		public UInt16  InitialHeap;
		/// <summary>Initial size, in bytes, of stack added to the data segment. This value is zero to indicate no initial stack allocation, or when SS is not equal to DS.</summary>
		public UInt16  InitialStack;
		
		public UInt16  InitialIP;
		public UInt16  InitialCS;
		public UInt16  InitialSP;
		public UInt16  InitialSS;
		
		/// <summary>Number of entries in the Segment Table.</summary>
		public UInt16  SegmentTableCount;
		/// <summary>Number of entries in the Module Reference Table.</summary>
		public UInt16  ModuleTableCount;
		/// <summary>Number of bytes in the Non-Resident Name Table.</summary>
		public UInt16  NonResidentNameTableSize;
		
		/// <summary>Segment Table file offset, relative to the beginning of the segmented EXE header.</summary>
		public UInt16  SegmentTableOffset;
		/// <summary>Resource Table file offset, relative to the beginning of the segmented EXE header.</summary>
		public UInt16  ResourceTableOffset;
		/// <summary>Resident Name Table file offset, relative to the beginning of the segmented EXE header.</summary>
		public UInt16  ResidentNameTableOffset;
		/// <summary>Module Reference Table file offset, relative to the beginning of the segmented EXE header.</summary>
		public UInt16  ModuleReferenceTableOffset;
		/// <summary>Imported Names Table file offset, relative to the beginning of the segmented EXE header.</summary>
		public UInt16  ImportTableOffset;
		/// <summary>Non-Resident Name Table offset, relative to the beginning of the file.</summary>
		public UInt32  NonResidentNameTableOffset;
		
		/// <summary>Number of movable entries in the Entry Table.</summary>
		public UInt16  NofEntryTableMovableEntries; // blargh
		/// <summary>Logical sector alignment shift count, log(base 2) of the segment sector size (default 9). Also phrased as "Segment alignment shift count".</summary>
		public UInt16  Alignment;
		/// <summary>Number of resource entries. Also phrased as "Count of resource segments".</summary>
		public UInt16  NofResourceEntries;
		/// <summary>Executable type, used by loader.</summary>
		public NEExecutableType ExecutableType;
		
		// The following are documented in WinNT.h and not the Win3.x Specifications
		// TODO: Do they say the header is smaller? What follows the header if it has less fields?
		
		/// <summary>Other .EXE flags</summary>
		public Byte    OtherFlags;
		/// <summary>Fast Load area offset. Also documented as "Offset to return thunks" in WinNT.h.</summary>
		public UInt16  FastLoadOffset;
		/// <summary>Size of the Fast Load area. Also documented as "Offset to segment ref. bytes" in WinNT.h.</summary>
		public UInt16  FastLoadSize;
		/// <summary>Minimum code swap area size. Also documented as "Reserved" in the Windows 3.x documentation.</summary>
		public UInt16  SwapArea;
		
		/// <summary>Expected Windows minor version number.</summary>
		public Byte    WindowsRevision;
		/// <summary>Expected Windows major version number.</summary>
		public Byte    WindowsVersion;
		
		public NEHeader(BinaryReader rdr) {
			
			Magic                 = rdr.ReadUInt16();
			
			if( Magic != 0x454E )
				throw new FormatException("The specified stream is not an NE Executable.");
				
			
			LinkerVersion         = rdr.ReadByte();
			LinkerRevision        = rdr.ReadByte();
			EntryTableOffset      = rdr.ReadUInt16();
			EntryTableSize        = rdr.ReadUInt16();
			Crc32                 = rdr.ReadUInt32();
			Flags                 = (NEFlags)rdr.ReadUInt16();
			AutoDataSegmentNumber = rdr.ReadUInt16();
			InitialHeap           = rdr.ReadUInt16();
			InitialStack          = rdr.ReadUInt16();
			
			InitialIP = rdr.ReadUInt16();
			InitialCS = rdr.ReadUInt16();
			InitialSP = rdr.ReadUInt16();
			InitialSS = rdr.ReadUInt16();
			
			SegmentTableCount        = rdr.ReadUInt16();
			ModuleTableCount         = rdr.ReadUInt16();
			NonResidentNameTableSize = rdr.ReadUInt16();
			
			SegmentTableOffset         = rdr.ReadUInt16();
			ResourceTableOffset        = rdr.ReadUInt16();
			ResidentNameTableOffset    = rdr.ReadUInt16();
			ModuleReferenceTableOffset = rdr.ReadUInt16();
			ImportTableOffset          = rdr.ReadUInt16();
			NonResidentNameTableOffset = rdr.ReadUInt32();
			
			NofEntryTableMovableEntries = rdr.ReadUInt16();
			Alignment                   = rdr.ReadUInt16();
			NofResourceEntries          = rdr.ReadUInt16();
			ExecutableType              = (NEExecutableType)rdr.ReadByte();
			
			OtherFlags      = rdr.ReadByte();
			FastLoadOffset  = rdr.ReadUInt16();
			FastLoadSize    = rdr.ReadUInt16();
			SwapArea        = rdr.ReadUInt16();
			WindowsRevision = rdr.ReadByte();
			WindowsVersion  = rdr.ReadByte();
		}
		
	}
	
	[Flags]
	public enum NEFlags : ushort {
		NoAutodata    = 0x0000,
		/// <summary>Shared automatic data segment.</summary>
		SingleData    = 0x0001,
		/// <summary>Instanced automatic data segment.</summary>
		MultipleData  = 0x0002,
		/// <remarks>Gleaned from eXeScope.</remarks>
		ProtectMode   = 0x0008,
		/// <summary>Errors detected at link time, module will not load.</summary>
		LinkerError   = 0x2000,
		/// <summary>
		///		<para>The SS:SP information is invalid, CS:IP points to an initialization procedure that is called with AX equal to the module handle. This initialization procedure must perform a far return to the caller, with AX not equal to zero to indicate success, or AX equal to zero to indicate failure to initialize. DS is set to the library's data segment if the SINGLEDATA flag is set. Otherwise, DS is set to the caller's data segment.</para>
		///		<para>A program or DLL can only contain dynamic links to executable files that have this library module flag set. One program cannot dynamic-link to another program.</para>
		/// </summary>
		LibraryModule = 0x8000
	}
	
	public enum NEExecutableType : byte {
		Unknown = 0x0,
		Windows = 0x2
	}
	
	public struct NESegmentTableEntry {
		
		/// <summary>Logical-sector offset (n byte) to the contents of the segment data, relative to the beginning of the file. Zero means no file data.</summary>
		public UInt16        Offset;
		/// <summary>Length of the segment in the file, in bytes. Zero means 64K.</summary>
		public UInt16        Size;
		/// <summary>The segment's contents.</summary>
		public NESegmentType Type;
		/// <summary>Minimum allocation size of the segment, in bytes. Total size of the segment. Zero means 64K.</summary>
		public UInt16        MinimumAllocation;
	}
	
	[Flags]
	public enum NESegmentType : ushort {
		None      = 0x0000,
		
		/// <summary>Segment contains Data. Otherwise segment contains Code</summary>
		Data      = 0x0001,
		
		IsLoaded  = 0x0002,
		
		/// <summary>Segment is Movable. Otherwise segment is Fixed.</summary>
		Moveable  = 0x0010,
		/// <summary>Segment is Pure or Shareable. Otherwise segment is Impure or Nonshareable.</summary>
		Pure      = 0x0020,
		/// <summary>Segment is Preloadable. Otherwise segment is LoadOnCall.</summary>
		Preload   = 0x0040,
		
		/// <summary>Segment is ExecuteOnly if 0x80 and this is a code segment (0x0).</summary>
		ExecuteOnly = 0x0080,
		/// <summary>Segment is ReadOnly if 0x80 and this is a data segment (0x1).</summary>
		ReadOnly    = 0x0081,
		
		/// <summary>Set if segment has relocation records.</summary>
		RelocInfo = 0x0100,
		/// <summary>Discard priority.</summary>
		Discardable = 0xF000
	}
	
	public struct NEResourceTable {
		
		public UInt16                     AlignmentShiftCount;
		public NEResourceTableTypeEntry[] TypeEntries;
		public NEResourceTableString[]    Strings;
		
		public NEResourceTable(BinaryReader rdr) {
			
			AlignmentShiftCount = rdr.ReadUInt16();
			
			List<NEResourceTableTypeEntry> types = new List<NEResourceTableTypeEntry>();
			while(true) {
				
				NEResourceTableTypeEntry type = new NEResourceTableTypeEntry( rdr );
				
				if( type.TypeId == 0 ) break;
				
				types.Add( type );
			}
			
			TypeEntries = types.ToArray();
			
			Strings = null; // TODO
			
		}
	}
	
	public struct NEResourceTableTypeEntry {
		
		public UInt16 TypeId;
		public UInt16 ResourceCount;
		public UInt32 Reserved;
		
		public NEResourceTableEntry[] Resources;
		
		public NEResourceTableTypeEntry(BinaryReader rdr) {
			TypeId        = rdr.ReadUInt16();
			ResourceCount = rdr.ReadUInt16();
			Reserved      = rdr.ReadUInt32();
			
			Resources = new NEResourceTableEntry[ ResourceCount ];
			for(int i=0;i<Resources.Length;i++) {
				Resources[i] = new NEResourceTableEntry( rdr );
			}
		}
		
	}
	
	public class NEResourceTableEntry {
		
		public UInt16          Offset;
		public UInt16          Length;
		public NEResourceFlags Flags;
		public UInt16          Id;
		public UInt32          Reserved;
		
		public NEResourceTableEntry(BinaryReader rdr) {
			
			Offset   = rdr.ReadUInt16();
			Length   = rdr.ReadUInt16();
			Flags    = (NEResourceFlags)rdr.ReadUInt16();
			Id       = rdr.ReadUInt16();
			Reserved = rdr.ReadUInt16();
		}
		
	}
	
	[Flags]
	public enum NEResourceFlags : ushort {
		None     = 0x0000,
		Moveable = 0x0010,
		Pure     = 0x0020,
		Preload  = 0x0040
	}
	
	public struct NEResourceTableString {
		public Byte   Length;
		public Byte[] Chars;
		
		public NEResourceTableString(BinaryReader rdr) {
			
			Length = rdr.ReadByte();
			Chars  = rdr.ReadBytes( Length );
		}
		
		public String String {
			get {
				return Encoding.ASCII.GetString( Chars, 0, Length );
			}
		}
		
	}
	
	public class NEFile {
		
		public DosHeader DosHeader;
		public UInt16    NEOffset;
		public NEHeader  NEHeader;
		
		public NESegmentTableEntry[] SegmentTable;
		public NEResourceTable ResourceTable;
		
		public NEFile(String fileName) {
			
			using(FileStream fs = new FileStream( fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 0x400, FileOptions.None ) ) {
				
				BinaryReader rdr = new BinaryReader(fs);
				
				///////////////////////////////
				// DOS Header
				
				DosHeader = new DosHeader( rdr );
				
				// The NE documentation states that if the UInt16 at 0x18 in the DOS header is 0x40, then the UInt16 at 0x3C contains the byte offset (from the beginning of the file) to the segmented header (i.e. 'NE')
				
				///////////////////////////////
				// NE Offset
				
				if( DosHeader.RelocationTableOffset != 0x40 )
					throw new FormatException("The specified file does not contain an NE segment pointer.");
				
				rdr.BaseStream.Seek( 0x3C, SeekOrigin.Begin );
				
				NEOffset = rdr.ReadUInt16();
				
				rdr.BaseStream.Seek( NEOffset, SeekOrigin.Begin );
				
				///////////////////////////////
				// NE Header
				
				NEHeader = new NEHeader( rdr );
				
				///////////////////////////////
				// NE Segment Table
				
long bytesWasted1 = ( NEOffset + NEHeader.SegmentTableOffset ) - rdr.BaseStream.Position;
				
				rdr.BaseStream.Seek( NEOffset + NEHeader.SegmentTableOffset, SeekOrigin.Begin );
				
				SegmentTable = new NESegmentTableEntry[ NEHeader.SegmentTableCount ];
				for(int i=0;i<NEHeader.SegmentTableCount;i++) {
					
					SegmentTable[i].Offset            = rdr.ReadUInt16();
					SegmentTable[i].Size              = rdr.ReadUInt16();
					SegmentTable[i].Type              = (NESegmentType)rdr.ReadUInt16();
					SegmentTable[i].MinimumAllocation = rdr.ReadUInt16();
				}
				
				///////////////////////////////
				// Resource Table
				
long bytesWasted2 = ( NEOffset + NEHeader.ResourceTableOffset ) - rdr.BaseStream.Position;
				
				rdr.BaseStream.Seek( NEOffset + NEHeader.ResourceTableOffset, SeekOrigin.Begin );
				
				ResourceTable = new NEResourceTable( rdr );
				
				///////////////////////////////
				// Resident Name Table
				
				///////////////////////////////
				// Module-Reference Table
				
				///////////////////////////////
				// Imported-Name Table
				
				///////////////////////////////
				// Entry Table
				
				///////////////////////////////
				// Non-Resident Name Table
				
				///////////////////////////////
				// Per-Segment Data
				
			}
			
		}
		
	}
}
