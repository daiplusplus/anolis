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
				Action = ResourceDataAction.Update;
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
		
		public static ResourceData Create(ResourceLang lang, Byte[] rawData) {
			
			return Create(lang, rawData, ResourceDataAction.None);
			
		}
		
		public static ResourceData Create(ResourceLang lang, Byte[] rawData, ResourceDataAction action) {
			
			// get the type from lang
			ResourceType type = lang.Name.Type;
			
			ResourceData retval;
			
			if(type.Identifier.KnownType != Win32ResourceType.Custom) {
				
				switch(type.Identifier.KnownType) {
					case Win32ResourceType.Bitmap:
						
						retval = new Data.BitmapResourceData(lang, rawData);
						
						break;
					case Win32ResourceType.CursorAnimated:
					case Win32ResourceType.CursorDeviceDependent:
					case Win32ResourceType.CursorDeviceIndependent:
					case Win32ResourceType.IconAnimated:
					case Win32ResourceType.IconDeviceDependent:
					case Win32ResourceType.IconDeviceIndependent:
						
						retval = new Data.IconCursorResourceData(lang, rawData);
						
						break;
					case Win32ResourceType.Version:
						
						retval = new Data.VersionResourceData(lang, rawData);
						
						break;
					default:
						
						retval = new ResourceData(lang, rawData);
						break;
				}
			
			} else {
				
				String id = type.Identifier.StringId.ToUpperInvariant(); // this isn't going to be null
				
				switch(id) {
					case "PNG":
					case "JPEG":
					case "GIF": // should I use another ResourceData subclass if it's an animated GIF?
						
						retval = new Data.ImageResourceData(lang, rawData);
						
						break;
						
					case "AVI":
					case "MPEG":
					case "MP3":
					case "MP2":
					case "RIFF":
					case "WAV":
					case "WMV":
						
						retval = new Data.MultimediaResourceData(lang, rawData);
						
						break;
					
					default:
						
						retval = new ResourceData(lang, rawData);
						
						break;
				}
				
			}
			
			return retval;
			
		}
		
		/// <summary>Creates a ResourceData instance from a stream containing data convertible into a resource. For instance a stream containing a  *.bmp file's content can be converted into a BITMAP resource.</summary>
		public static ResourceData Read(Stream stream) {
			
			// reads the file, determines what kind of ResourceData it is and what subclass to use and return
			// do we need ResourceData subclasses?
			
			throw new NotImplementedException();
			
		}
		
		/// <summary>Creates a ResourceData instance from input data that can be converted into a resource. If the data is the actual bytes of a resource use the public constructor.</summary>
		public static ResourceData Read(Byte[] data) {
			
			if(data == null) throw new ArgumentNullException("data");
			
			using(MemoryStream stream = new MemoryStream(data)) {
				
				return Read( stream );
			}
			
		}
		
		/// <summary>Creates a ResourceData instance from a file containing data convertible into a resource. For instance a *.bmp can be converted into a BITMAP resource.</summary>
		public static ResourceData Read(String filename) {
			
			if(filename == null) throw new ArgumentNullException("filename");
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
			
			if(path == null) throw new ArgumentNullException("path");
			
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