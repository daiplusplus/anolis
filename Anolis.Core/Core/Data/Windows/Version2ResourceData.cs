using System;
using System.Collections.Generic;
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
		
		private VersionResourceData(Byte[] rawData, ResourceLang lang) : base(lang, rawData) {
		}
		
		internal static VersionResourceData TryCreate(Byte[] data, ResourceLang lang) {
			
			using(MemoryStream s = new MemoryStream(data))
			using(BinaryReader rdr = new BinaryReader(s, Encoding.Unicode)) { // good thing VS_VERSION_INFO uses UTF16 throughout
				
					// new approach:
					// recursively build a tree of versionitem classes then handle them intelligently afterwards
				
				Mode mode = Mode.Root;
				
				VersionItem root = new VersionItem();
				root.Mode         = mode;
				root.wLength      = rdr.ReadUInt16();
				root.wValueLength = rdr.ReadUInt16();
				root.wType        = rdr.ReadUInt16();
				root.szKey        = GetKey(mode, rdr, out mode );
				root.padding      = Pad(rdr);
				
				UInt32 signature = rdr.ReadUInt32();
				if(signature != 0xFEEF04BD) throw new InvalidOperationException("lol wtf");
				rdr.ReadBytes( 48 ); // this is where FixedFileInfo goes (58 - 4)
				
				Byte[] padding2   = Pad(rdr);
				
				List<Object> children = new List<Object>();
				
				//                1   2   3   4                               5                     6
				Int32 bytesRead = 2 + 2 + 2 + ((root.szKey.Length * 2) + 2) + root.padding.Length + 4 + 48; _bytesRead = bytesRead;
				while(bytesRead < root.wLength) {
					
					VersionItem child = RecurseItem( GetNextMode(mode), rdr ); // no need to switch since the children will always be VersionItems
					children.Add( child );
					
					bytesRead += child.wLength;
					
				}
				
				root.children = children.ToArray();
				
				return null;
				
			}//using
			
		}
		
		private enum Mode {
			None,
			Root,
				StringFileInfoOrVarFileInfo,
				StringFileInfo,
					StringTable,
						String,
				VarFileInfo,
					Var
		}
		
		[System.Diagnostics.DebuggerStepThrough()]
		private static Byte[] Pad(BinaryReader rdr) {
			
			Int64 pos = rdr.BaseStream.Position;
			pos = (pos + 3) & ~3; // I've no idea how this works
			
			Int64 offset = pos - rdr.BaseStream.Position;
			
			return rdr.ReadBytes( (int)offset );
			
		}
		
		private static Int32 _bytesRead = 0;
		
		private static VersionItem RecurseItem(Mode mode, BinaryReader rdr) {
			
			Int64 initPos = rdr.BaseStream.Position;
			
			VersionItem item = new VersionItem();
			item.wLength      = rdr.ReadUInt16();
			item.wValueLength = rdr.ReadUInt16();
			item.wType        = rdr.ReadUInt16();
			item.szKey        = GetKey(mode, rdr, out item.Mode );
			item.padding      = Pad(rdr);
			
			mode = item.Mode;
			
			List<Object> children = new List<Object>();
			
			//                1   2   3   4                               5
			Int32 bytesRead = 2 + 2 + 2 + ((item.szKey.Length * 2) + 2) + item.padding.Length; _bytesRead += bytesRead;
			
			while(bytesRead < item.wLength) {
				
				switch(mode) {
					case Mode.String:
						
						Byte[] bytes = rdr.ReadBytes( item.wValueLength * 2 );
						String s = Encoding.Unicode.GetString( bytes.SubArray(0, bytes.Length - 2 ) ); // miss out the null terminator
						children.Add( s );
						
						bytesRead += item.wValueLength * 2;
						_bytesRead += item.wValueLength * 2;
						
						if( bytesRead != item.wLength ) throw new InvalidOperationException("More bytes left over?");
						
						break;
					case Mode.Var:
						
						Byte[] data = rdr.ReadBytes( item.wValueLength ); // wValueLength = size in bytes
						
						children.Add( data ); // I'll figure out what to do with the data later
						
						bytesRead += item.wValueLength;
						_bytesRead += item.wValueLength;
						
						if( bytesRead != item.wLength ) throw new InvalidOperationException("More bytes left over?");
						
						break;
					default:
						VersionItem child = RecurseItem( GetNextMode(mode), rdr );
						children.Add( child );
						
						bytesRead += child.wLength; // after here (this breakpoint being triggered when children.count == 8) the while() loop continues, it's as if bytesRead isn't set properly such that it thinks VarFileInfo is a member of the Strings in the StringTable rather than a new section entirely
						
						break;
				}
				
				// the reader was corrupted before entering the third String of the first StringTable of the StringFileInfo
				// so let's see if padding here helps
				
				Int32 padding = Pad( rdr ).Length;
				
				bytesRead += padding;
				_bytesRead += padding;
				
			}
			
			Int64 currentPos = rdr.BaseStream.Position;
			
			
			if(bytesRead > item.wLength) {
				// seek backwards so that it lines up
				Int64 goBack = bytesRead - item.wLength;
				rdr.BaseStream.Seek( -goBack, SeekOrigin.Current );
			}
			
			item.children = children.ToArray();
			
			return item;
			
		}
		
		[System.Diagnostics.DebuggerStepThrough()]
		private static Mode GetNextMode(Mode mode) {
			switch(mode) {
				case Mode.Root:
					return Mode.StringFileInfoOrVarFileInfo;
				case Mode.StringFileInfo:
					return Mode.StringTable;
				case Mode.StringTable:
					return Mode.String;
				case Mode.String:
					return Mode.None;
					
				case Mode.VarFileInfo:
					return Mode.Var;
				case Mode.Var:
					return Mode.None;
				default:
					throw new InvalidOperationException("GetNextMode, invalid mode");
			}
		}
		
//		[System.Diagnostics.DebuggerStepThrough()]
		private static String GetKey(Mode mode, BinaryReader rdr, out Mode newMode) {
			
			newMode = mode;
			
			// wait a sec, are these strings null-terminated?
			
			StringBuilder sb = new StringBuilder();
			Char c;
			while( (c = rdr.ReadChar()) != 0 ) {
				sb.Append( c );
			}
			
			String retval = sb.ToString();
			
			if(mode == Mode.StringFileInfoOrVarFileInfo) {
				if     (retval == "StringFileInfo") newMode = Mode.StringFileInfo;
				else if(retval == "VarFileInfo"   ) newMode = Mode.VarFileInfo;
			}
			
			return retval;
			
			switch(mode) {
				case Mode.StringFileInfoOrVarFileInfo:
					
					String peek = Encoding.Unicode.GetString( rdr.ReadBytes(2) );
					if(peek == "S") {
						
						newMode = Mode.StringFileInfo;
						return peek + Encoding.Unicode.GetString( rdr.ReadBytes("tringFileInfo".Length * 2) );
						
					} else if(peek == "V") {
						
						newMode = Mode.VarFileInfo;
						return peek + Encoding.Unicode.GetString( rdr.ReadBytes("arFileInfo".Length * 2) );
						
					} else {
						throw new InvalidOperationException("Unexpected StringFileOrVarInfo szKey Value '" + peek + "'");
					}
					
					break;
				case Mode.StringFileInfo:
					
					throw new InvalidOperationException("This situation should never be encountered: StringFileInfo");
					
				case Mode.StringTable:
					
					// it's an 8-digit hex number stored as a UTF-16 string
					return Encoding.Unicode.GetString( rdr.ReadBytes( 8 * 2) );
					
				case Mode.String:
					
				case Mode.VarFileInfo:
					
					throw new InvalidOperationException("This situation should never be encountered: VarFileInfo");
					
				case Mode.Var:
			}
			
		}
		
		private class VersionItem {
			
			public Mode   Mode;
			
			public UInt16 wLength;
			public UInt16 wValueLength;
			public UInt16 wType;
			public String szKey;
			public Byte[] padding;
			public Object[] children; // children are always VersionItem[] except in case of String (when it's UInt16[]) or Var (DWORD)
			
			public override String ToString() {
				
				return Mode.ToString() + " - " + szKey;
				
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
