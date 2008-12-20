using System;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Core.Data {
	
	public class CursorDirectoryResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if( typeId.KnownType == Win32ResourceType.CursorDirectory ) return Compatibility.Yes;
			
			return Compatibility.No;
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			
			if( filenameExtension == "cur" ) return Compatibility.Yes;
			
			return Compatibility.No;
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			LastErrorMessage = "Not Implemented";
			return null;
		}
		
		public override ResourceData FromFile(System.IO.Stream stream, String extension) {
			LastErrorMessage = "Not Implemented";
			return null;
		}
		
		public override String Name {
			get { return "Cursor Directory"; }
		}
	}
	
	public sealed class CursorDirectoryResourceData : DirectoryResourceData {
		
		private CursorDirectoryResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] rawData, out ResourceData data) {
			
			data = null;
			return false;
			
			// TODO
			
		}
		
		public override String[] SaveFileFilter {
			get { return new String[] { "Cursor File (*.cur)|.cur" }; }
		}
		
	}
}
