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
				
				Initialise();
			}
		}
		
		public ResourceLang Lang { get; private set; }
		
		public ResourceDataAction Action { get; internal set; }
		
		////////////////////////////////////
		
		protected ResourceData(ResourceLang lang, Byte[] rawData) {
			
			Lang    = lang;
			_data   = rawData;
			Action  = ResourceDataAction.None; // action would be set by the ResourceSource using it and is none of our concern
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
		public static ResourceData FromFile(Stream stream, String extension) {
			
			// 'intelligent reading' of the file itself is too resource intensive. Better just to trust the extension.
			
			extension = extension.ToLowerInvariant();
			if(extension.StartsWith(".")) extension = extension.Substring(1);
			
			ResourceDataFactory[] factories = ResourceDataFactory.GetFactoriesForExtension( extension );
			
			// try the factories in order of compatibility.
			
			Int32 i = 0;
			ResourceData data = null;
			while(data == null) {
				
				if(i >= factories.Length) throw new Exception("Unable to locate factory for resource data.");
				
				data = factories[i++].FromFile( stream, extension );
				
			}
			
			return data;
			
			throw new NotImplementedException();
			
		}
		
		/// <summary>Creates a ResourceData instance from a file containing data convertible into a resource. For instance a *.bmp can be converted into a BITMAP resource.</summary>
		public static ResourceData FromFile(String filename) {
			
			if(filename == null) throw new ArgumentNullException("filename");
			if( !File.Exists(filename) ) throw new FileNotFoundException("The file to load the resource data from was not found", filename);
			
			using(Stream stream = File.OpenRead(filename)) {
				
				return FromFile( stream, Path.GetExtension(filename) );
			}
			
		}
		
		////////////////////////////////////
		
		/// <summary>Saves the raw data in the Resource to disk.</summary>
		public void Save(String path) {
			
			// TODO: Standardise the use of 'path', 'filename', and 'filepath' in the source code.
			// I'm of the opinion that 'path' must always be absolute and 'filename' can be relative or absolute, 'filepath' need not exist
			// Should standardise 'if file exists, overwrite?' behaviour too
			
			if(path == null) throw new ArgumentNullException("path");
			
			using(Stream stream = File.Create(path)) {
				
				Save(stream);
			}
			
		}
		
		public void Save(Stream stream) {
			
			stream.Write( this.RawData, 0, this.RawData.Length );
		}
		
		/// <summary>Saves the ResourceData to disk in a suitable file format. The base implementation is the same as Save. If the file exists it will be overwritten.</summary>
		public void SaveAs(String path) {
			
			if(path == null) throw new ArgumentNullException("path");
			
			using(Stream stream = File.Create(path)) {
				
				SaveAs(stream, Path.GetExtension(path) );
			}
			
		}
		
		public virtual void SaveAs(Stream stream, String extension) {
			
			Save(stream);
			
		}
		
		/// <summary>Gets the file extension and friendly name in .NET "File Filter" format associated with the data format contained within.</summary>
		/// <example>Binary Data File (*.bin)|*.bin</example>
		public abstract String[] SaveFileFilter { get; }
		
		/// <summary>Called when the RawData is set. A notification to subclasses to recreate any stateful data.</summary>
		protected virtual void Initialise() {
			
			
			
		}
		
	}

}