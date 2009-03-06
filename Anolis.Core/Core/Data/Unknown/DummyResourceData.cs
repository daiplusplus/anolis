using System;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Core.Data {
	
	public class DummyResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			return Compatibility.All;
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			return Compatibility.All;
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			return new DummyResourceData(ResourceDataAction.None, data);
		}
		
		public override ResourceData FromFile(System.IO.Stream stream, String extension, ResourceSource source) {
			Byte[] data = GetAllBytesFromStream(stream);
			return FromResource(null, data);
		}
		
		public override String OpenFileFilter {
			get { return null; }
		}
		
		public override String Name {
			get { return "Placeholder Resource Data"; }
		}
	}
	
	public sealed class DummyResourceData : ResourceData {
		
		public DummyResourceData(ResourceDataAction action, ResourceData data) : this(action, null, data.RawData) {	
		}
		
		public DummyResourceData(ResourceDataAction action, Byte[] rawData) : this(action, null, rawData) {	
		}
		
		public DummyResourceData(ResourceDataAction action, ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
			
			base.Action = action;
		}
		
		protected override String[] SupportedFilters {
			get { return new String[0]; }
		}
		
		protected override void SaveAs(System.IO.Stream stream, string extension) {
			throw new NotSupportedException();
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			throw new NotSupportedException();
		}
		
	}
}
