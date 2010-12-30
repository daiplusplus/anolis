using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace Anolis.Core.Source {
	
	public abstract class ManagedResourceSource {
		
		public abstract ManagedResourceInfo[] GetResourceInfo();
		
		public abstract Stream GetResourceStream(ManagedResourceInfo resource);
		
		public virtual Stream GetResourceStream(String name) {
			
			ManagedResourceInfo[] infos = GetResourceInfo();
			foreach(ManagedResourceInfo info in infos) {
				
				if( info.Name == name ) return GetResourceStream( info );
			}
			
			return null;
		}
		
		public virtual void SaveResourceStream(String streamName, String destinationFileName) {
			
			Stream stream = GetResourceStream(streamName);
			if( stream == null ) throw new AnolisException("Specified stream not found");
			
			using(FileStream fs = new FileStream(destinationFileName, FileMode.Create, FileAccess.Write, FileShare.None)) {
				
				Byte[] buffer = new Byte[4096];
				while( stream.Read( buffer, 0, buffer.Length) == buffer.Length ) {
					fs.Write(buffer);
				}
				
			}
			
		}
		
	}
	
	public class ManagedResourceInfo {
		
		public ManagedResourceInfo(ManagedResourceSource source, String name, ResourceLocation location) {
			
			Source   = source;
			Name     = name;
			Location = location;
		}
		
		public ManagedResourceSource Source { get; private set; }
		
		public String                Name   { get; private set; }
		
		public ResourceLocation      Location { get; private set; }
		
		
		
	}
}
