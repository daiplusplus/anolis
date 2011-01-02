using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Anolis.Tools.PEInfo {
	
	[StructLayout(LayoutKind.Sequential)]
	public struct DosHeader {
		
		// Note:
		// Paragraph =  16 bytes
		// Block     = 512 bytes
		// 1 Block  == 1 Page
		
		/// <summary>'MZ' magic number of the file.</summary>
		public UInt16 Magic; // must equal 0x4D 0x5A == 'MZ'
		/// <summary>Number of bytes in the last block of the program that are actually used. If this is zero then all 512 bytes of the block are used.</summary>
		public UInt16 LastBlockBytes;
		/// <summary>Number of blocks in the file that are part of the EXE file. If LastBlockBytes is non-zero, only that much of the last block is used.</summary>
		public UInt16 NofBlocks;
		/// <summary>Number of relocation entries stored after the header. May be zero.</summary>
		public UInt16 NofRelocationEntries;
		/// <summary>Number of paragraphs in the header. The program's data begins just after the header, and this field can be used to calculate the appropriate file offset. The header includes the relocation entries. Note that some OSs and/or programs may fail if the header is not a multiple of 512 bytes.</summary>
		public UInt16 NofParagraphsInHeader;
		/// <summary>Number of paragraphs of additional memory that the program will need. This is the equivalent of the BSS size in a Unix program. The program can't be loaded if there isn't at least this much memory available to it.</summary>
		public UInt16 NofParagraphsInMemoryRequired;
		/// <summary>Maximum number of paragraphs of additional memory. Normally, the OS reserves all the remaining conventional memory for your program, but you can limit it with this field.</summary>
		public UInt16 NofParagraphsInMemoryMaximum;
		/// <summary>Relative value of the stack segment. This value is added to the segment the program was loaded at, and the result is used to initialize the SS register.</summary>
		public UInt16 InitialSS;
		/// <summary>Initial value of the SP register.</summary>
		public UInt16 InitialSP;
		/// <summary>Word checksum. If set properly, the 16-bit sum of all words in the file should be zero. Usually, this isn't filled in.</summary>
		public UInt16 Checksum;
		/// <summary>Initial value of the IP register.</summary>
		public UInt16 InitialIP;
		/// <summary>Initial value of the CS register, relative to the segment the program was loaded at.</summary>
		public UInt16 InitialCS;
		/// <summary>Offset of the first relocation item in the file.</summary>
		public UInt16 RelocationTableOffset;
		/// <summary>Overlay number. Normally zero, meaning that it's the main program.</summary>
		public UInt16 OverlayNumber;
		
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public UInt16[] Reserved1;
		
		public UInt16 OemIdentifier;
		public UInt16 OemInformation;
		
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
		public UInt16[] Reserved2;
		
		/// <summary>File address of New Executable header (also used for PE). This is at offset 0x3C from the beginning of this struct.</summary>
		public UInt32 NewExeHeaderAddress;
		
		public DosRelocationTableEntry[] RelocationTable;
		
		public DosHeader(BinaryReader rdr) {
			
			Magic                         = rdr.ReadUInt16();
			
			if( Magic != 0x5A4D )
				throw new FormatException("The specified stream is not a DOS Executable.");
			
			LastBlockBytes                = rdr.ReadUInt16();
			NofBlocks                     = rdr.ReadUInt16();
			NofRelocationEntries          = rdr.ReadUInt16();
			NofParagraphsInHeader         = rdr.ReadUInt16();
			NofParagraphsInMemoryRequired = rdr.ReadUInt16();
			NofParagraphsInMemoryMaximum  = rdr.ReadUInt16();
			InitialSS                     = rdr.ReadUInt16();
			InitialSP                     = rdr.ReadUInt16();
			Checksum                      = rdr.ReadUInt16();
			InitialIP                     = rdr.ReadUInt16();
			InitialCS                     = rdr.ReadUInt16();
			RelocationTableOffset         = rdr.ReadUInt16();
			OverlayNumber                 = rdr.ReadUInt16();
			Reserved1                     = rdr.ReadUInt16Array( 4 );
			OemIdentifier                 = rdr.ReadUInt16();
			OemInformation                = rdr.ReadUInt16();
			Reserved2                     = rdr.ReadUInt16Array( 10 );
			NewExeHeaderAddress           = rdr.ReadUInt32();
			
			RelocationTable = new DosRelocationTableEntry[ NofRelocationEntries ];
			if( RelocationTable.Length > 0 ) {
				
				long currentPos = rdr.BaseStream.Position;
				rdr.BaseStream.Seek( RelocationTableOffset, SeekOrigin.Begin );
				
				for(int i=0;i<RelocationTable.Length;i++) {
					
					RelocationTable[i].Offset  = rdr.ReadUInt16();
					RelocationTable[i].Segment = rdr.ReadUInt16();
				}
				
				rdr.BaseStream.Seek( currentPos, SeekOrigin.Begin );
			}
		}
		
		public UInt16 HeaderSize {
			get { return (ushort)(NofParagraphsInHeader * (ushort)16u); }
		}
		
	}
	
	public struct DosRelocationTableEntry {
		public UInt16 Offset;
		public UInt16 Segment;
	}
	
	public class DosFile {
		
		public DosHeader DosHeader;
		
		public DosFile(String fileName) {
			
			using(FileStream fs = new FileStream( fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 0x400, FileOptions.None ) ) {
			
				BinaryReader rdr = new BinaryReader(fs);
				
				DosHeader = new DosHeader( rdr );
				
			}
		}
		
	}
	
}
