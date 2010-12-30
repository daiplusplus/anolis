using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Core.Data {
	
	public class UnknownResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			return Compatibility.All;
		}
		
		public override Compatibility HandlesExtension(String fileNameExtension) {
			return Compatibility.All;
		}
		
		public override ResourceData FromResource(ResourceLang lang, byte[] data) {
			
			return new UnknownResourceData(lang, data);
		}
		
		private static ResourceData FromFile(Stream stream, String extension) {
			
			Byte[] data = GetAllBytesFromStream(stream);
			return new UnknownResourceData(extension, null, data);
		}
		
		public override ResourceData FromFileToAdd(Stream stream, String extension, UInt16 langId, ResourceSource currentSource) {
			return FromFile(stream, extension);
		}
		
		public override ResourceData FromFileToUpdate(Stream stream, string extension, ResourceLang currentLang) {
			return FromFile(stream, extension);
		}
		
		public override String OpenFileFilter {
			get { return CreateFileFilter("All Files", "*"); }
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
