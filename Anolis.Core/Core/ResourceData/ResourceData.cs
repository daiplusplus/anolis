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
		
		public static ResourceData Create(ResourceLang lang, Byte[] rawData) {
			
			Win32ResourceType wType = lang.Name.Type.Identifier.KnownType;
			
			ResourceHint hint;
			
			if(wType == Win32ResourceType.Custom) {
				
				String id = lang.Name.Type.Identifier.StringId.ToUpperInvariant(); // this isn't going to be null
				
				switch(id) {
					case "PNG" :  hint = ResourceHint.Png;  break;
					case "JPEG":  hint = ResourceHint.Jpeg; break;
					case "GIF" :  hint = ResourceHint.Gif;  break;
					case "AVI" :
					case "MPEG":
					case "MP3" :
					case "MP2" :
					case "RIFF":
					case "WAV" :
					case "WMV" :
					case "HTML":
					case "XML" :
					case "XSLT":
					case "SGML":
						hint = ResourceHint.Unknown; // TODO: Make hints for these file formats once I get round to it
						break;
					default:
						hint = ResourceHint.Unknown;
						break;
				}
				
			} else if(wType == Win32ResourceType.Unknown) {
				
				hint = ResourceHint.Unknown;
				
			} else {
				
				hint = (ResourceHint)wType;
			}
			
			ResourceData retval = ResourceDataFactory.GetResourceData(lang, hint, rawData);
			
			return retval;
			
		}
		
		/// <summary>Creates a ResourceData instance from a stream containing data convertible into a resource. For instance a stream containing a  *.bmp file's content can be converted into a BITMAP resource.</summary>
		public static ResourceData Read(Stream stream) {
			
			// reads the file, determines what kind of ResourceData it is and what subclass to use and return
			
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
				
				SaveAs(stream);
			}
			
		}
		
		public virtual void SaveAs(Stream stream) {
			
			Save(stream);
		}
		
		/// <summary>Gets the file extension and friendly name in .NET "File Filter" format associated with the data format contained within.</summary>
		/// <example>Binary Data File (*.bin)|*.bin</example>
		public abstract String FileFilter { get; }
		
		/// <summary>Called when the RawData is set. A notification to subclasses to recreate any stateful data.</summary>
		protected virtual void Initialise() {
			
			
			
		}
		
	}

}