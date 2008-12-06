using System;
using System.IO;

namespace Anolis.Core {
	
	/// <summary>ResourceData CONTAINS the resource data. It is lazy-loaded by ResourceLang (i.e. when the resource data is requested the data is extracted from the source and an instance of ResourceData is constructed with that data</summary>
	public class ResourceData {
		
		private Byte[] _data;
		
		/// <summary>Returns the raw bytes of the resource's data.</summary>
		public Byte[] RawData {
			get {
				return _data;
			}
			set {
				_data = value;
				IsDirty = true;
			}
		}
		
		internal Boolean IsDirty { get; private set; }
		
		public ResourceLang Lang { get; private set; }
		
		////////////////////////////////////
		
		/// <summary>Creates a ResourceData instance from the resource's actual data. No conversion is done.</summary>
		public ResourceData(ResourceLang lang, Byte[] data) {
			
			Lang    = lang;
			RawData = data;
		}
		
		/// <summary>Creates a ResourceData instance from a stream containing data convertible into a resource. For instance a stream containing a  *.bmp file's content can be converted into a BITMAP resource.</summary>
		public static ResourceData Read(Stream stream) {
			
			// reads the file, determines what kind of ResourceData it is and what subclass to use and return
			
			throw new NotImplementedException();
			
		}
		
		public static ResourceData Read(Byte[] data) {
			
			if(data == null) throw new ArgumentNullException("The byte array 'data' cannot be null");
			
			using(MemoryStream stream = new MemoryStream(data)) {
				
				return Read( stream );
			}
			
		}
		
		/// <summary>Creates a ResourceData instance from a file containing data convertible into a resource. For instance a *.bmp can be converted into a BITMAP resource.</summary>
		public static ResourceData Read(String filename) {
			
			if(filename == null) throw new ArgumentNullException("The string 'filename' cannot be null");
			if( !File.Exists(filename) ) throw new FileNotFoundException("The file to load the resource data from was not found", filename);
			
			using(Stream stream = File.OpenRead(filename)) {
				
				return Read( stream );
			}
			
		}
		
		////////////////////////////////////
		
		/// <summary>Saves the raw data in the Resource to disk.</summary>
		public void Save(String path) {
			
			// TODO: Standardise the use of 'path', 'filename', and 'filepath' in the source code.
			// I'm of the opinion that 'path' must always be absolute and 'filename' can be relative or absolute, 'filepath' need not exist
			// Should standardise 'if file exists, overwrite?' behaviour too
			
			if(path == null) throw new ArgumentNullException("The string 'filename' cannot be null");
			
			using(Stream stream = File.Create(path)) {
				
				SaveAs(stream);
			}
			
		}
		
		public void Save(Stream stream) {
			
			stream.Write( this.RawData, 0, this.RawData.Length );
		}
		
		/// <summary>Saves the ResourceData to disk in a suitable file format. The base implementation is the same as Save. If the file exists it will be overwritten.</summary>
		public void SaveAs(String path) {
			
			Save(path);
		}
		
		public virtual void SaveAs(Stream stream) {
			
			Save(stream);
		}
		
	}

}