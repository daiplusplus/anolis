using System;
using System.IO;

namespace Anolis.Core {
	
	// TODO: More implementation details. Should Resource-Data be lazy-loaded? And if so, should ResourceLang be the loader or ResourceData load it when GetRawData is called?
	
	public class ResourceData {
		
		/// <summary>Returns the raw bytes of the resource's data.</summary>
		public abstract Byte[] GetRawData();
		
		/// <summary>Sets the raw bytes of the resource's data. Implementations may throw an exception if the data is not in the correct format, but this is not guaranteed.</summary>
		public abstract void   SetRawData(Byte[] data);
		
		////////////////////////////////////
		
		public static ResourceData Read(Stream stream);
			
			// reads the file, determines what kind of ResourceData it is and what subclass to use
		
		public static ResourceData Read(Byte[] data) {
			
			if(data == null) throw new ArgumentNullException("The byte array 'data' cannot be null");
			
			using(MemoryStream stream = new MemoryStream(data)) {
				
				return Read( stream );
			}
			
		}
		
		public static ResourceData Read(String filename) {
			
			if(filename == null) throw new ArgumentNullException("The string 'filename' cannot be null");
			if( !File.Exists(filename) ) throw new FileNotFoundException("The file to load the resource data from was not found", filename);
			
			using(Stream stream = File.OpenRead(filename)) {
				
				return Read( stream );
			}
			
		}
		
		////////////////////////////////////
		
		
		public abstract void Save(String filename);
		
		public abstract void Save(Stream stream);
		
		
	}

}