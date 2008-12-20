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
		
		public override ResourceData FromFile(System.IO.Stream stream, String extension) {
			Byte[] data = GetAllBytesFromStream(stream);
			return FromResource(null, data);
		}
		
		public override string Name {
			get { return "Generic Binary Resource"; }
		}
	}
	
	public sealed class UnknownResourceData : ResourceData {
		
		public UnknownResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		public override String[] SaveFileFilter {
			get { return new String[] { "Binary Data File (*.bin)|*.bin" }; }
		}
		
	}
}
