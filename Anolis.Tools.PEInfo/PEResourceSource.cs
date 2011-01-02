using System;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;

namespace Anolis.Tools.PEInfo {
	
	
	
	public class PEResourceSource {
		
		private PEFile _pe;
		private FileStream _fs;
		
		public PEResourceSource(String fileName) {
			
			_fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			
			UInt64 imageLength = (UInt64)new FileInfo(fileName).Length;
			
			_pe = new PEFile( imageLength, _fs );
		}
		
		public void GetResources() {
			
			int resourceTableIdx = (int)CoffImageDirectoriesIndex.ResourceTable;
			
			CoffImageDataDirectory[] directories = GetDataDirectory();
			if( directories.Length <= resourceTableIdx ) throw new PEResourceException("The PE Image does not contain a resource directory.");
			
			CoffImageDataDirectory resourceDirectory = directories[ resourceTableIdx ];
			if( resourceDirectory.VirtualAddress == 0 || resourceDirectory.Size == 0 ) throw new PEResourceException("The PE Image does not contain any resource data.");
			
			//////////////////////////////////////////
			
			SectionTableEntry? sectionTemp;
			UInt32 fileOffset = _pe.RvaToFileOffset( resourceDirectory.VirtualAddress, out sectionTemp );
			
			if( fileOffset == 0 && sectionTemp == null ) throw new PEResourceException("The PE Image's resource directory does not exist within a section.");
			SectionTableEntry section = sectionTemp.Value;
			
			// Ensure the resource section is the same size (and address) as the resource directory entry says
			if( resourceDirectory.Size != section.VirtualSize || resourceDirectory.VirtualAddress != section.VirtualAddress ) throw new PEResourceException("The PE Image's declared resource directory does not coincide with the size and location of the .rsrc section.");
			
			// Apparently SizeOfRawData can be smaller than VirtualSize for .rsrc sections, according to the spec the rest should be filled with zeros
			// So I hope this doesn't affect the parsing of the actual data...
			
			////////////////////////////////////////////////////
			// Read the Resource Directories
			
			_fs.Seek( fileOffset, SeekOrigin.Begin );
			
			BinaryReader rdr = new BinaryReader( _fs );
			
			ResourceDirectoryTable rootTable = new ResourceDirectoryTable( _pe, rdr );
			
		}
		
		private CoffImageDataDirectory[] GetDataDirectory() {
			
			if( _pe.OptionalHeader32     != null ) return _pe.OptionalHeader32    .Value.DataDirectories;
			if( _pe.OptionalHeader32Plus != null ) return _pe.OptionalHeader32Plus.Value.DataDirectories;
			return null;
		}
		
	}
	
	/// <summary>Each resource directory table has the following format. This data structure should be considered the heading of a table because the table actually consists of directory entries.</summary>
	public class ResourceDirectoryTable {
		
		/// <summary>Resource flags. This field is reserved for future use. It is currently set to zero.</summary>
		public UInt32 Characteristics;
		
		/// <summary>The time that the resource data was created by the resource compiler.</summary>
		public UInt32 DateTimeStamp;
		
		/// <summary>The major version number, set by the user.</summary>
		public UInt16 VersionMajor;
		/// <summary>The minor version number, set by the user.</summary>
		public UInt16 VersionMinor;
		
		/// <summary>The number of directory entries immediately following the table that use strings to identify Type, Name, or Language entries (depending on the level of the table).</summary>
		public UInt16 NameCount;
		/// <summary>The number of directory entries immediately following the Name entries that use numeric IDs for Type, Name, or Language entries.</summary>
		public UInt16 IdCount;
		
		public ResourceDirectoryTable(PEFile pe, BinaryReader rdr) {
			
			Characteristics = rdr.ReadUInt32();
			DateTimeStamp   = rdr.ReadUInt32();
			VersionMajor    = rdr.ReadUInt16();
			VersionMinor    = rdr.ReadUInt16();
			NameCount       = rdr.ReadUInt16();
			IdCount         = rdr.ReadUInt16();
			
			EntriesByName = new ResourceDirectoryEntry[ NameCount ];
			EntriesById   = new ResourceDirectoryEntry[ IdCount   ];
			
			for(int i=0;i<NameCount;i++) {
				EntriesByName[i] = new ResourceDirectoryEntry( pe, true, rdr );
			}
			
			for(int i=0;i<IdCount;i++) {
				EntriesById[i] = new ResourceDirectoryEntry( pe, false, rdr );
			}
		}
		
		public DateTime DateTime {
			get {
				DateTime dt = new DateTime(1970, 1, 1);
				return dt.AddSeconds( DateTimeStamp );
			}
		}
		
		public ResourceDirectoryEntry[] EntriesByName;
		public ResourceDirectoryEntry[] EntriesById;
		
	}
	
	/// <summary>The directory entries make up the rows of a table. Each resource directory entry has the following format. Whether the entry is a Name or ID entry is indicated by the resource directory table, which indicates how many Name and ID entries follow it (remember that all the Name entries precede all the ID entries for the table). All entries for the table are sorted in ascending order: the Name entries by case-insensitive string and the ID entries by numeric value.</summary>
	public class ResourceDirectoryEntry {
		
		/// <summary>If Named, the address of a string that gives the Type, Name, or Language ID entry, depending on level of table. Else, a 32-bit integer that identifies the Type, Name, or Language ID entry.</summary>
		public UInt32 Identifier;
		
		/// <summary>If the high bit is 0. Address of a Resource Data entry (a leaf). Else, the lower 31 bits are the address of another resource directory table (the next level down).</summary>
		public UInt32 ChildRva;
		
		public ResourceDirectoryTable Subdirectory;
		public ResourceDataEntry      LeafData;
		
		public ResourceDirectoryEntry(PEFile pe, Boolean isNamed, BinaryReader rdr) {
			
			IsNamed = isNamed;
			
			Identifier = rdr.ReadUInt32();
			ChildRva   = rdr.ReadUInt32();
			
			long currentPosition = rdr.BaseStream.Position;
			
			SectionTableEntry? section;
			UInt32 childOffset = pe.RvaToFileOffset( ChildRvaAddress, out section );
			rdr.BaseStream.Seek( childOffset, SeekOrigin.Begin );
			
			if( IsLeaf ) LeafData     = new ResourceDataEntry(rdr);
			else         Subdirectory = new ResourceDirectoryTable(pe, rdr);
			
			rdr.BaseStream.Seek( currentPosition, SeekOrigin.Begin );
		}
		
		public Boolean IsNamed { get; private set; }
		
		/// <summary>Returns true if the Next RVA is a DataEntry, otherwise use Subdirectory RVA because the next 'node' is a directory.</summary>
		public Boolean IsLeaf {
			get { return ((ChildRva >> 31) & 0x1) == 0x0; }
		}
		
		public UInt32 ChildRvaAddress {
			get { return ChildRva & 0x7FFFFFFF; }
		}
		
	}
	
	/// <summary>Each Resource Data entry describes an actual unit of raw data in the Resource Data area. A Resource Data entry has the following format.</summary>
	[StructLayout(LayoutKind.Sequential,Pack=1)]
	public struct ResourceDataEntry {
		
		/// <summary>The address of a unit of resource data in the Resource Data area.</summary>
		public UInt32 DataRva;
		/// <summary>The size, in bytes, of the resource data that is pointed to by the Data RVA field.</summary>
		public UInt32 Size;
		/// <summary>The code page that is used to decode code point values within the resource data. Typically, the code page would be the Unicode code page.</summary>
		public UInt32 Codepage;
		/// <summary>Reserved, must be 0.</summary>
		public UInt32 Reserved;
		
		public ResourceDataEntry(BinaryReader rdr) {
			
			DataRva  = rdr.ReadUInt32();
			Size     = rdr.ReadUInt32();
			Codepage = rdr.ReadUInt32();
			Reserved = rdr.ReadUInt32();
		}
	}
	
	/// <summary>The resource directory string area consists of Unicode strings, which are word-aligned. Each resource directory string has the following format.</summary>
	[StructLayout(LayoutKind.Sequential,Pack=1)]
	public struct ResourceDirectoryString {
		
		// Remember, in Windows 'Unicode' means WCHAR.
		// The spec doesn't say if 'Length' is in Bytes or Characters...
		
		/// <summary>The size of the string, not including length field itself.</summary>
		public UInt16 Length;
		/// <summary>The variable-length Unicode string data, word-aligned.</summary>
		public Byte[] StringBytes;
		
		public ResourceDirectoryString(BinaryReader rdr) {
			Length      = rdr.ReadUInt16();
			StringBytes = rdr.ReadBytes( Length );
		}
		
		public String String {
			get { return Encoding.Unicode.GetString( StringBytes ); }
		}
	}
	
	[Serializable]
	public class PEResourceException : Exception {
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public PEResourceException() { }
		public PEResourceException(string message) : base(message) { }
		public PEResourceException(string message, Exception inner) : base(message, inner) { }
		protected PEResourceException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
	
}
