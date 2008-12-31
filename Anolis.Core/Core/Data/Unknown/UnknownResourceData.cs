using System;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Core.Data {
	
	public class UnknownResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			return Compatibility.All;
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			return Compatibility.All;
		}
		
		public override ResourceData FromResource(ResourceLang lang, byte[] data) {
			
			return new UnknownResourceData(lang, data);
		}
		
		public override ResourceData FromFile(System.IO.Stream stream, String extension, ResourceSource source) {
			
			Byte[] data = GetAllBytesFromStream(stream);
			return new UnknownResourceData(extension, null, data);
		}
		
		public override String OpenFileFilter {
			get { return "All Files (*.*)|*.*"; }
		}
		
		public override string Name {
			get { return "Generic Binary Resource"; }
		}
	}
	
	public sealed class UnknownResourceData : ResourceData {
		
		private String _extension;
		
		public UnknownResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		public UnknownResourceData(String extension, ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
			_extension = extension;
		}
		
		protected override String[] SupportedFilters {
			get { return new String[0]; }
		}
		
		protected override void SaveAs(System.IO.Stream stream, String extension) {
			throw new NotSupportedException();
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			if(_extension != null)
				return new ResourceTypeIdentifier( _extension );
			else
				return new ResourceTypeIdentifier("Binary");
		}
		
	}
}
