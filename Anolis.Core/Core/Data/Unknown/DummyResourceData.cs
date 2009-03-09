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
	
	/// <summary>Represents an resource operation when the ResourceSource is opened in blind mode.</summary>
	/// <remarks>DummyResourceData can exist on itself with no data to denote a resource-deletion operation. For resource inserts and updates you need to wrap an existing ResourceData with the DummyResource data and set the appropriate action.</remarks>
	public sealed class DummyResourceData : ResourceData {
		
		public static DummyResourceData CreateToDelete() {
			
			return new DummyResourceData(ResourceDataAction.Delete, null, null);
		}
		
		public static DummyResourceData CreateToAdd(ResourceData newData) {
			
			EnsureCompatible( newData );
			
			return new DummyResourceData(ResourceDataAction.Add, null, newData.RawData);
		}
		
		public static DummyResourceData CreateToUpdate(ResourceData updatedData) {
			
			EnsureCompatible( updatedData );
			
			return new DummyResourceData(ResourceDataAction.Update, null, updatedData.RawData);
		}
		
		private static void EnsureCompatible(ResourceData data) {
			
			if( !IsResourceDataCompatible(data ) ) throw new ArgumentException("The provided ResourceData instance is incompatible with DummyResourceData");
		}
		
		public static Boolean IsResourceDataCompatible(ResourceData data) {
			
			return !( data is DirectoryResourceData );
		}
		
		private DummyResourceData(ResourceDataAction action, ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
			
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
