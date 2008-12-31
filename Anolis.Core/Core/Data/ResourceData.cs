using System;
using System.IO;

using Anolis.Core.Data;

namespace Anolis.Core {
	
	/// <summary>ResourceData contains the resource data (rather than ResourceLang). It is lazy-loaded by ResourceLang (i.e. when the resource data is requested the data is extracted from the source and an instance of ResourceData is constructed with that data</summary>
	public abstract class ResourceData {
		
		private Byte[] _data;
		
		/// <summary>Returns the raw bytes of the resource's data.</summary>
		public Byte[] RawData {
			get {
				return _data;
			}
			set {
				_data = value;
				Action = ResourceDataAction.Update;
				
				Reinitialise();
			}
		}
		
		public ResourceLang Lang { get; internal set; }
		
		public ResourceDataAction Action { get; internal set; }
		
		////////////////////////////////////
		
		protected ResourceData(ResourceLang lang, Byte[] rawData) {
			
			Lang    = lang;
			_data   = rawData;
			Action  = ResourceDataAction.None; // action would be set by the ResourceSource using it and is none of our concern
		}
		
		protected static ResourceDataException ME(Exception ex) {
			return new ResourceDataException(ex);
		}
		
		protected static ResourceDataException MEEx(String extension) {
			return new ResourceDataException( new ArgumentException("Unsupported extension \"" + extension + '"', "extension") );
		}
		
		public static ResourceData FromResource(ResourceLang lang, Byte[] rawData) {
			
			ResourceTypeIdentifier typeId = lang.Name.Type.Identifier;
			
			// get a list of suitable factories
			
			ResourceDataFactory[] factories = ResourceDataFactory.GetFactoriesForType( typeId );
			
			// try the factories in order of compatibility.
			
			Int32 i = 0;
			ResourceData data = null;
			while(data == null) {
				
				if(i >= factories.Length) throw new Exception("Unable to locate factory for resource data.");
				
				data = factories[i++].FromResource(lang, rawData);
				
			}
			
			return data;
			
		}
		
		/// <summary>Creates a ResourceData instance from a stream containing data convertible into a resource. For instance a stream containing a  *.bmp file's content can be converted into a BITMAP resource.</summary>
		public static ResourceData FromFile(Stream stream, String extension, ResourceSource source) {
			
			// 'intelligent reading' of the file itself is too resource intensive. Better just to trust the extension.
			
			extension = extension.ToLowerInvariant();
			if(extension.StartsWith(".")) extension = extension.Substring(1);
			
			ResourceDataFactory[] factories = ResourceDataFactory.GetFactoriesForExtension( extension );
			
			// try the factories in order of compatibility.
			
			Int32 i = 0;
			ResourceData data = null;
			while(data == null) {
				
				if(i >= factories.Length) throw new Exception("Unable to locate factory for resource data.");
				
				data = factories[i++].FromFile( stream, extension, source );
				
			}
			
			return data;
			
			throw new NotImplementedException();
			
		}
		
		/// <summary>Creates a ResourceData instance from a file containing data convertible into a resource. For instance a *.bmp can be converted into a BITMAP resource.</summary>
		public static ResourceData FromFile(String filename, ResourceSource source) {
			
			if(filename == null) throw new ArgumentNullException("filename");
			if( !File.Exists(filename) ) throw new FileNotFoundException("The file to load the resource data from was not found", filename);
			
			using(Stream stream = File.OpenRead(filename)) {
				
				return FromFile( stream, Path.GetExtension(filename), source );
			}
			
		}
		
		////////////////////////////////////
		
		/// <summary>
		///		<para>Saves the Resource Data to disk. The extension is inferred from the path and the file format selected that way. Ensure the extension is supported by querying SaveFileFilters first.</para>
		///		<para>If the file exists it will be overwritten.</para>
		///		<para>Use the .bin extension to save the raw resource data regardless of ResourceData subclass.</para>
		///	</summary>
		public void Save(String path) {
			
			// TODO: Standardise the use of 'path', 'filename', and 'filepath' in the source code.
			// I'm of the opinion that 'path' must always be absolute and 'filename' can be relative or absolute, 'filepath' need not exist
			// Should standardise 'if file exists, overwrite?' behaviour too
			
			if(path == null) throw new ArgumentNullException("path");
			
			using(Stream stream = File.Create(path)) {
				
				Save(stream, Path.GetExtension(path) );
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
		
		public String[] SaveFileFilters {
			
			get {
				
				String[] supported = SupportedFilters;
				Array.Resize<String>(ref supported, supported.Length + 1);
				supported[supported.Length - 1] = "Raw Resource Data (*.bin)|*.bin";
				
				return supported;
			}
		}
		
		/// <summary>Returns an array of supported File Filters. Return an empty array (never null) if it does not support custom saving.</summary>
		protected abstract String[] SupportedFilters { get; }
		
		/// <summary>Returns the TypeIdentifier of the best-match/recommended ResourceType for this ResourceData.</summary>
		protected abstract ResourceTypeIdentifier GetRecommendedTypeId();
		
		private ResourceTypeIdentifier _recommendedTypeId;
		
		public ResourceTypeIdentifier RecommendedTypeId {
			get {
				
				if( _recommendedTypeId == null ) _recommendedTypeId = GetRecommendedTypeId();
				
				return _recommendedTypeId;
			}
		}
		
		/// <summary>Called when the RawData is set. A notification to subclasses to recreate any stateful data.</summary>
		protected virtual void Reinitialise() {
		}
		
	}

}