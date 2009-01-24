using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Anolis.Core.Native;

namespace Anolis.Core.Data {
	
	public class VersionResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if(typeId.KnownType == Win32ResourceType.Version)
				return Compatibility.Yes;
			else
				return Compatibility.No;
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			return Compatibility.No;
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			return VersionResourceData.TryCreate(data, lang);
			
		}
		
		public override ResourceData FromFile(System.IO.Stream stream, String extension, ResourceSource currentSource) {
			throw new NotSupportedException();
		}
		
		public override String Name {
			get { return "Version Info"; }
		}
		
		public override String OpenFileFilter {
			get { return null; }
		}
	}
	
	public class VersionResourceData : ResourceData {
		
		// represent the Version data in a tree like how it's stored (see: the "Children" members of various structures)
		// might be useful to display the data like that in the UI
		
		private VersionResourceData(Byte[] rawData, ResourceLang lang) : base(lang, rawData) {
		}
		
		internal static VersionResourceData TryCreate(Byte[] data, ResourceLang lang) {
			
			using(MemoryStream s = new MemoryStream(data))
			using(BinaryReader rdr = new BinaryReader(s)) {
				
				Int32 bitsSoFar = 0;
				
				VsVersionInfo vi;
				vi.wLength      = rdr.ReadUInt16();										bitsSoFar += 16;
				vi.wValueLength = rdr.ReadUInt16();										bitsSoFar += 16;
				vi.wType        = rdr.ReadUInt16();										bitsSoFar += 16;
				
				vi.szKey        = Encoding.Unicode.GetString(
									rdr.ReadBytes( "VS_VERSION_INFO".Length * 2 ) );	bitsSoFar += vi.szKey.Length * 8 * 2;
				
				// Padding1= to align on a 32-bit boundary
				Int32 sizeOfPadding1  = (bitsSoFar % 32) / 8;
				
				vi.Padding1     = rdr.ReadBytes(sizeOfPadding1);						bitsSoFar += sizeOfPadding1 * 8;
				
				vi.Value        = new VsFixedFileInfo( rdr );							bitsSoFar += vi.wValueLength * 8;
				
				Int32 sizeOfPadding2 = (bitsSoFar % 32) / 8;
				
				vi.Padding2 = rdr.ReadBytes(sizeOfPadding2);							bitsSoFar += sizeOfPadding2 * 8;
				
				if( vi.wLength > bitsSoFar / 8 ) { // then there's going to be children
					
					ReadChildren(rdr, bitsSoFar, vi.wLength * 8);
				}
				
				return null;
				
			}//using
			
		}
		
		
		
		private static void ReadChildren(BinaryReader rdr, Int32 bitsSoFar, Int32 maxBits) {
			
			// data is (zero or more StringFileInfo) and/or (zero or one VarFileInfo)
			
			// both VarFileInfo and StringFileInfo share the same structure
			// you can identify them by the szKey member
			
//struct VarFileInfo { 
//  WORD  wLength; 
//  WORD  wValueLength; 
//  WORD  wType; 
//  WCHAR szKey[]; 
//  WORD  Padding[]; 
//  Var   Children[]; 
//};

//struct StringFileInfo { 
//  WORD        wLength; 
//  WORD        wValueLength; 
//  WORD        wType; 
//  WCHAR       szKey[]; 
//  WORD        Padding[]; 
//  StringTable Children[]; 
//};
			
			while( bitsSoFar < maxBits ) {
				
				UInt16 wLength      = rdr.ReadUInt16();				bitsSoFar += 16;
				UInt16 wValueLength = rdr.ReadUInt16();				bitsSoFar += 16;
				UInt16 wType        = rdr.ReadUInt16();				bitsSoFar += 16;
				String c            = Encoding.Unicode.GetString(
				                      rdr.ReadBytes(2) );			bitsSoFar += 2;
				String szKey;
				UInt16[] padding;
				
				if(c == "V") {
					szKey = c + Encoding.Unicode.GetString( rdr.ReadBytes( ("VarFileInfo".Length - 1) * 2) );
					
				} else if(c == "S") {
					szKey = c + Encoding.Unicode.GetString( rdr.ReadBytes( ("StringFileInfo".Length - 1) * 2) );
					
				}
				
			}
			
		}
		
		protected override void SaveAs(System.IO.Stream stream, String extension) {
			throw new NotSupportedException();
		}
		
		protected override String[] SupportedFilters {
			get { return new String[0]; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier( Win32ResourceType.Version );
		}
		
	}
	
}
