using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Core.Data {
	
	public class DialogResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			return (typeId.KnownType == Win32ResourceType.Dialog) ? Compatibility.Yes : Compatibility.No;
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			return Compatibility.No;
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			return DialogResourceData.TryCreate(lang, data);
		}
		
		public override String Name {
			get { return "Dialog Box"; }
		}
		
		public override String OpenFileFilter {
			get { return null; }
		}
		
		public override ResourceData FromFileToAdd(Stream stream, string extension, ushort lang, ResourceSource currentSource) {
			throw new NotSupportedException();
		}
		
		public override ResourceData FromFileToUpdate(Stream stream, string extension, ResourceLang currentLang) {
			throw new NotSupportedException();
		}
	}
	
	public class DialogResourceData : ResourceData {
		
		private DialogResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
			
		}
		
		internal static DialogResourceData TryCreate(ResourceLang lang, Byte[] rawData) {
			
			
			return null;
			
		}
		
		protected override void SaveAs(System.IO.Stream stream, String extension) {
			throw new NotImplementedException();
		}
		
		protected override String[] SupportedFilters {
			get { return new String[] {}; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier(Win32ResourceType.Dialog);
		}
	}
	
	
}
