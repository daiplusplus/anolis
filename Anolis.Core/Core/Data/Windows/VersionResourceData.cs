using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Anolis.Core.Native;

using Cult = System.Globalization.CultureInfo;
using Mode = Anolis.Core.Data.VersionItemMode;

namespace Anolis.Core.Data {
	
	public class VersionResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if(typeId.KnownType == Win32ResourceType.Version)
				return Compatibility.Yes;
			else
				return Compatibility.No;
		}
		
		public override Compatibility HandlesExtension(String fileNameExtension) {
			return Compatibility.No;
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			return VersionResourceData.TryCreate(data, lang);
			
		}
		
		public override ResourceData FromFileToAdd(System.IO.Stream stream, String extension, UInt16 langId, ResourceSource currentSource) {
			throw new NotSupportedException();
		}
		
		public override ResourceData FromFileToUpdate(System.IO.Stream stream, string extension, ResourceLang currentLang) {
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
		
		private VersionResourceData(VersionItem root, Byte[] rawData, ResourceLang lang) : base(lang, rawData) {
			
			VSVersionInfo = root;
			
		}
		
#region Construction
		
		// VS_VERSION parsing assisted by looking at the source of the Vestris ResourceLib ( http://www.codeproject.com/KB/library/ResourceLib.aspx )
		// it helped with figuring out how to processing padding; my recursive approach diffthers from his though
		
		internal static VersionResourceData TryCreate(Byte[] data, ResourceLang lang) {
			
			using(MemoryStream s = new MemoryStream(data))
			using(BinaryReader rdr = new BinaryReader(s, Encoding.Unicode)) { // good thing VS_VERSION_INFO uses UTF16 throughout
				
				try {
					
					VersionItem root = RecurseItem(Mode.Root, rdr);
					
					return new VersionResourceData(root, data, lang );
					
				} catch(AnolisException) {
					// TODO: how should this be logged?
					return null;
				}
				
			}//using
			
		}
		
		private static VersionItem RecurseItem(VersionItemMode mode, BinaryReader rdr) {
			
			Int64 initPos = rdr.BaseStream.Position;
			
			VersionItem item = new VersionItem(mode);
			item.Length      = rdr.ReadUInt16();
			item.ValueLength = rdr.ReadUInt16();
			item.Type        = rdr.ReadUInt16();
			item.Key         = GetKey(mode, rdr, out item._mode );
			
			rdr.Align4();
			
			mode = item.Mode;
			
			List<VersionItem> children = new List<VersionItem>();
			
			while(rdr.BaseStream.Position < initPos + item.Length) {
				
				switch(mode) {
					case Mode.Root:
						
						if(item.Value == null) {
							
							Byte[] ffiBytes = rdr.ReadBytes( item.ValueLength ); // this is where FixedFileInfo goes						
							/*Byte[] padding2 = */ rdr.Align4();
							
							if(ffiBytes.Length >= 52) { // 52 == 0x34
								
								VSFixedFileInfo ffi = new VSFixedFileInfo( ffiBytes );
								
								if(ffi.dwSignature != 0xFEEF04BD) throw new InvalidOperationException("Unrecognised VS_VERSIONINFO Signature");
								
								Dictionary<String,String> ffiDict = FfiToDict( ffi );
								
								item.Value = ffiDict;
								
							} else {
								throw new InvalidOperationException("Unexpected VS_FIXEDFILEINFO length");
							}
						}
						
						goto default;
						
					case Mode.String:
						
						Byte[] bytes = rdr.ReadBytes( item.ValueLength * 2 );
						String s = Encoding.Unicode.GetString( bytes.SubArray(0, bytes.Length - 2 ) ); // miss out the null terminator
						item.Value = s;
						
						break;
					case Mode.Var:
						
						Byte[] data = rdr.ReadBytes( item.ValueLength ); // wValueLength = size in bytes
						item.Value = data;
						
						// data is a DWORD array indicating the language and code page combinations supported by this file.
						// The low-order word of each DWORD must contain a Microsoft language identifier, and the high-order word must contain the IBM code page number.
						// Either high-order or low-order word can be zero, indicating that the file is language or code page independent.
						// ms-help://MS.MSDNQTR.v90.en/winui/winui/windowsuserinterface/resources/versioninformation/versioninformationreference/versioninformationstructures/var.htm
						
						break;
					default:
						VersionItem child = RecurseItem( GetNextMode(mode), rdr );
						children.Add( child );
						
						break;
				}
				
				// the reader was corrupted before entering the third String of the first StringTable of the StringFileInfo
				// so let's see if padding here helps
				
				rdr.Align4();
				
			}
			
			rdr.Align4();
			
			item.Children = children.ToArray();
			
			return item;
			
		}
		
		private static Dictionary<String,String> FfiToDict(VSFixedFileInfo ffi) {
			
			Dictionary<String,String> d = new Dictionary<String,String>();
			
			d.Add("Signature"        , GetUInt32String( ffi.dwSignature     ) );
			d.Add("StrucVersion"     , GetUInt32String( ffi.dwStrucVersion  ) );
			d.Add("FileVersionMS"    , GetUInt32String( ffi.dwFileVersionMS ) );
			d.Add("FileVersionLS"    , GetUInt32String( ffi.dwFileVersionLS ) );
			d.Add("FileVersion"      , null );
			d.Add("ProductVersionMS" , GetUInt32String( ffi.dwProductVersionMS ) );
			d.Add("ProductVersionLS" , GetUInt32String( ffi.dwProductVersionLS ) );
			d.Add("ProductVersion"   , null );
			
			d.Add("FileFlagsMask"    , GetUInt32String( ffi.dwFileFlagsMask ) );
			d.Add("FileFlags"        , ffi.dwFileFlags.ToString() );
			d.Add("FileOS"           , ffi.dwFileOS.ToString() );
			d.Add("FileType"         , ffi.dwFileType.ToString() );
			d.Add("FileSubType"      , GetUInt32String( ffi.dwFileSubType ) );
			d.Add("FileSubTypeDriver", ffi.dwFileSubTypeDriver.ToString() );
			d.Add("FileSubTypeFont"  , ffi.dwFileSubTypeFont.ToString() );
			d.Add("FileDateMS"       , GetUInt32String( ffi.dwFileDateMS ) );
			d.Add("FileDateLS"       , GetUInt32String( ffi.dwFileDateLS ) );
			d.Add("FileDate"         , null );
			
			return d;
		}
		
		private static String GetUInt32String(UInt32 u) {
			
			return u.ToString(Cult.InvariantCulture) + " - " + u.ToString("X", Cult.InvariantCulture);
		}
		
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
					throw new ResourceDataException("GetNextMode, invalid mode");
			}
		}
		
		private static String GetKey(Mode mode, BinaryReader rdr, out Mode newMode) {
			
			newMode = mode;
			
			// these strings are null-terminated
			String retval = rdr.ReadSZString();
			
			if(mode == Mode.StringFileInfoOrVarFileInfo) {
				if     (retval == "StringFileInfo") newMode = Mode.StringFileInfo;
				else if(retval == "VarFileInfo"   ) newMode = Mode.VarFileInfo;
				else throw new ResourceDataException("Invalid Key Name");
			}
			
			return retval;
			
		}
		
#endregion
		
		protected override void SaveAs(System.IO.Stream stream, String extension) {
			throw new NotSupportedException();
		}
		
		protected override String[] SupportedFilters {
			get { return new String[0]; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier( Win32ResourceType.Version );
		}
		
#region Actual Data
		
		public VersionItem VSVersionInfo { get; private set; }
		
		public Dictionary<String,String> GetStringTable() {
			
			VersionItem item = GetFirstItem( VSVersionInfo, Mode.StringTable );
			
			if( item == null ) return null;
			
			Dictionary<String,String> ret = new Dictionary<String,String>();
			
			foreach(VersionItem str in item.Children) {
				
				ret.Add( str.Key, str.Value == null ? String.Empty : str.Value.ToString() );
			}
			
			return ret;
		}
		
		public Dictionary<String,String> GetFixedFileInfo() {
			
			VersionItem root = VSVersionInfo;
			
			Dictionary<String,String> ffi = root.Value as Dictionary<String,String>;
			
			Dictionary<String,String> ret = new Dictionary<String,String>( ffi ); // this copies contents
			return ret;
		}
		
		private VersionItem GetFirstItem(VersionItem parent, Mode mode) {
			
			if( parent.Mode == mode ) return parent;
			
			foreach(VersionItem item in parent.Children) {
				
				if( item.Mode == mode ) return item;
				
				VersionItem match = GetFirstItem( item, mode );
				if( match != null ) return match;
				
			}
			
			return null;
		}
		
#endregion
		
	}
	
	public class VersionItem {
		
		internal VersionItemMode _mode;
		
		internal VersionItem(VersionItemMode mode) {
			_mode = mode;
		}
		
		public VersionItemMode Mode   { get { return _mode; } }
		
		public UInt16 Length          { get; set; }
		public UInt16 ValueLength     { get; set; }
		public UInt16 Type            { get; set; }
		public String Key             { get; set; }
		public VersionItem[] Children { get; set; }
		public Object Value           { get; set; }
		
		public override String ToString() {
			
			String ret = Mode.ToString() + " - " + Key;
			
			if(Value != null) ret += " : " + Value.ToString();
			
			return ret;
			
		}
		
	}
	
	public enum VersionItemMode {
		None,
		Root,
			StringFileInfoOrVarFileInfo,
			StringFileInfo,
				StringTable,
					String,
			VarFileInfo,
				Var
	}
	
}
