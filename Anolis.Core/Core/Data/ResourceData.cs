using System;
using System.Collections.Generic;
using System.IO;

namespace Anolis.Core.Data {
	
	/// <summary>ResourceData contains the resource data (rather than ResourceLang). It is lazy-loaded by ResourceLang (i.e. when the resource data is requested the data is extracted from the source and an instance of ResourceData is constructed with that data</summary>
	public abstract class ResourceData : IDisposable {
		
		private Byte[] _data;
		
		/// <summary>Returns the raw bytes of the resource's data.</summary>
		public Byte[] RawData {
			get {
				return _data;
			}
//			set {
//				_data = value;
//				if(Lang != null) Lang.Action = ResourceDataAction.Update;
//				
//				Reinitialize();
//			}
		}
//		/// <summary>Called when the RawData is set. A notification to subclasses to recreate any stateful data.</summary>
//		protected virtual void Reinitialize() {
//		}
		
		public ResourceLang Lang { get; internal set; }
		
		
		protected internal delegate void RemoveFunction(ResourceLang lang);
		
		/// <summary>Called when this ResourceData is being removed from the ResourceSource. ResourceDatas that have dependencies on this ResourceData must be appropriately dealt with (e.g. removed) when this is called.</summary>
		/// <param name="underlyingDelete">If true then the ResourceData is just being removed the collection of resources. If false it is being deleted from the ResourceSource</param>
		/// <param name="removeFunction">A delegate to call in order to remove any dependent ResourceData instances</param>
		protected internal virtual void OnRemove(Boolean underlyingDelete, RemoveFunction deleteFunction) {
		}
		
#region Constructor / Destructor
		
		protected ResourceData(ResourceLang lang, Byte[] rawData) {
			
			Lang    = lang;
			_data   = rawData;
			
			Tag = new ResourceDataTagDictionary();
		}
		
		~ResourceData() {
			
			Dispose(false);
		}
		
		public void Dispose() {
			
			Dispose(true);
			
			GC.SuppressFinalize(this);
		}
		
		protected virtual void Dispose(Boolean managed) {
		}
		
#endregion
		
#region Operation Assistance
		
		//////////////////////////////////////////////////////
		// Save File Filters
		
		/// <summary>Returns an array of supported File Filters. Return an empty array (never null) if it does not support custom saving. The preferred extension must be at position 1</summary>
		protected abstract String[] SupportedFilters { get; }
		
		public String[] SaveFileFilters {
			
			get {
				
				String[] supported = SupportedFilters;
				Array.Resize<String>(ref supported, supported.Length + 1);
				supported[supported.Length - 1] = "Raw Resource Data (*.bin)|*.bin";
				
				return supported;
			}
		}
		
		
		//////////////////////////////////////////////////////
		// Recommended Type ID
		
		/// <summary>Returns the TypeIdentifier of the best-match/recommended ResourceType for this ResourceData.</summary>
		protected abstract ResourceTypeIdentifier GetRecommendedTypeId();
		
		private ResourceTypeIdentifier _recommendedTypeId;
		
		public ResourceTypeIdentifier RecommendedTypeId {
			get {
				
				if( _recommendedTypeId == null ) _recommendedTypeId = GetRecommendedTypeId();
				
				return _recommendedTypeId;
			}
		}
		
		//////////////////////////////////////////////////////
		// Save Extensions
		
		public String RecommendedExtension {
			get {
				
				String filter = SaveFileFilters[0]; // there will always be at least 1, no need to NRE or bounds check
				
				String extension = filter.Substring( filter.LastIndexOf('.') );
				
				return extension;
			}
		}
		
		//////////////////////////////////////////////////////
		// Tags
		
		/// <summary>Depository for ResourceData/Lang metadata. Known keys include 'sourceFile' - the path to the file where the resource data originated from</summary>
		public ResourceDataTagDictionary Tag {
			get; private set;
		}
		
#endregion
		
#region Save
		
		/// <summary>
		///		<para>Saves the Resource Data to disk. The extension is inferred from the path and the file format selected that way. Ensure the extension is supported by querying SaveFileFilters first.</para>
		///		<para>If the file exists it will be overwritten.</para>
		///		<para>Use the .bin extension to save the raw resource data regardless of ResourceData subclass.</para>
		///	</summary>
		public void Save(String fileName) {
			
			if(fileName == null) throw new ArgumentNullException("filename");
			
			using(Stream stream = File.Open( fileName, FileMode.Create, FileAccess.Write, FileShare.None )) {
				
				Save(stream, Path.GetExtension(fileName) );
			}
			
		}
		
		public void Save(Stream stream, String extension) {
			
			if( extension[0] == '.' ) extension = extension.Substring(1);
			
			if(extension == "bin") {
				
				stream.Write( this.RawData, 0, this.RawData.Length );
				
			} else if(SupportedFilters.Length > 0) {
				
				SaveAs(stream, extension);
			}
		}
		
		protected abstract void SaveAs(Stream stream, String extension);
		
#endregion
		
#region Static
		
		public static ResourceData FromResource(ResourceLang lang, Byte[] rawData) {
			
			ResourceTypeIdentifier typeId = lang.Name.Type.Identifier;
			
			// get a list of suitable factories
			
			ResourceDataFactory[] factories = ResourceDataFactory.GetFactoriesForType( typeId );
			
			// try the factories in order of compatibility.
			
			Int32 i = 0;
			ResourceData data = null;
			while(data == null) {
				
				if(i >= factories.Length) throw new AnolisException("Unable to locate factory for resource data.");
				
				try {
					data = factories[i++].FromResource(lang, rawData);
				} catch(ResourceDataException) {
				}
			}
			
			return data;
			
		}
		
		/// <summary>Creates a ResourceData instance from a file containing data convertible into a resource ready to add to the specified ResourceSource (but doesn't actually add it). For instance a *.bmp can be converted into a BITMAP resource.</summary>
		public static ResourceData FromFileToAdd(String fileName, UInt16 langId, ResourceSource source) {
			
			if(fileName == null) throw new ArgumentNullException("fileName");
			if( !File.Exists(fileName) ) throw new FileNotFoundException("The file to load the resource data from was not found", fileName);
			
			using(FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read)) {
				
				return FromFileToAdd( stream, Path.GetExtension(fileName), langId, source );
			}
			
		}
		
		/// <summary>Creates a ResourceData instance from a stream containing data convertible into a resource ready to add to the specified ResourceSource (but doesn't actually add it). For instance a stream containing a  *.bmp file's content can be converted into a BITMAP resource.</summary>
		public static ResourceData FromFileToAdd(Stream stream, String extension, UInt16 langId, ResourceSource source) {
			
			// 'intelligent reading' of the file itself is too resource intensive. Better just to trust the extension.
			
			extension = extension.ToLowerInvariant();
			if(extension.StartsWith(".", StringComparison.OrdinalIgnoreCase)) extension = extension.Substring(1);
			
			ResourceDataFactory[] factories = ResourceDataFactory.GetFactoriesForExtension( extension );
			
			// try the factories in order of compatibility.
			
			Int32 i = 0;
			ResourceData data = null;
			while(data == null) {
				
				if(i >= factories.Length) throw new ResourceDataException("Unable to locate factory for resource data.");
				
				data = factories[i++].FromFileToAdd( stream, extension, langId, source );
				
			}
			
			return data;
			
		}
		
		/// <summary>Creates a ResourceData instance from a file containing data convertible into a resource ready to replace the specified ResourceLang (but doesn't actually replace it). For instance a *.bmp can be converted into a BITMAP resource.</summary>
		public static ResourceData FromFileToUpdate(String fileName, ResourceLang lang) {
			
			if(fileName == null) throw new ArgumentNullException("filename");
			if( !File.Exists(fileName) ) throw new FileNotFoundException("The file to load the resource data from was not found", fileName);
			
			using(Stream stream = File.OpenRead(fileName)) {
				
				return FromFileToUpdate( stream, Path.GetExtension(fileName), lang );
			}
			
		}
		
		/// <summary>Creates a ResourceData instance from a stream containing data convertible into a resource ready to replace the specified ResourceLang (but doesn't actually replace it). For instance a stream containing a  *.bmp file's content can be converted into a BITMAP resource.</summary>
		public static ResourceData FromFileToUpdate(Stream stream, String extension, ResourceLang lang) {
			
			// 'intelligent reading' of the file itself is too resource intensive. Better just to trust the extension.
			
			extension = extension.ToUpperInvariant();
			if(extension.StartsWith(".", StringComparison.OrdinalIgnoreCase)) extension = extension.Substring(1);
			
			ResourceDataFactory[] factories = ResourceDataFactory.GetFactoriesForExtension( extension );
			
			// try the factories in order of compatibility.
			
			Int32 i = 0;
			ResourceData data = null;
			while(data == null) {
				
				if(i >= factories.Length) throw new ResourceDataException("Unable to locate factory for resource data.");
				
				data = factories[i++].FromFileToUpdate(stream, extension, lang);
				
			}
			
			return data;
			
		}
		
#endregion
		
		
	}
	
	public enum ResourceDataAction {
		None,
		Add,
		Delete,
		Update
	}
	
	public class ResourceDataTagDictionary : Dictionary<String,Object> {
		internal ResourceDataTagDictionary() {
		}
	}

}