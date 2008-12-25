using System;
using System.Collections.Generic;

namespace Anolis.Core {
	
	public abstract partial class ResourceSource : IDisposable {
		
		protected ResourceSource(Boolean readOnly) {
			
			IsReadOnly = readOnly;
			
			InitResourceSourceCollections();
			
		}
		
		protected ResourceSource(Boolean readOnly, Boolean loadSource) {
			
			IsReadOnly = readOnly;
			
			InitResourceSourceCollections();
			
		}
		
		//////////////////////
		
		public Boolean IsReadOnly { get; private set; }
		
		/// <summary>Returns information about this ResourceSource in a Key/Value pair.</summary>
		public virtual ResourceSourceInfo SourceInfo { get { return null; } }
		
		//////////////////////
		
		/// <summary>Extracts the Resource Data for the specified Resource.</summary>
		public abstract ResourceData GetResourceData(ResourceLang lang);
		
		public virtual void Dispose() {}
		
		//////////////////////
		
		public static ResourceSource Open(String filename, Boolean readOnly) {
			
			// get the file type of the file to load
			// if PE executable (a dll, native exe, etc)
			
			String ext = System.IO.Path.GetExtension(filename).ToUpperInvariant();
			switch(ext) {
				case ".EXE":
				case ".DLL":
				case ".SCR":
				case ".CPL":
					return new PE.PEResourceSource(filename, readOnly);
			}
			
			throw new NotImplementedException("Anolis does not support files that aren't PE/COFF Executables as ResourceSources yet");
			
		}
		
		//////////////////////
		// ResourceSource State Mutators
		
		public abstract void CommitChanges(Boolean reload);
		
		/// <summary>Actually reloads all the data into the source.</summary>
		public abstract void Reload();
		
	}
	
	public class ResourceSourceInfo : Dictionary<String,String> {
		// TODO: Maybe later make a ReadOnlyKeyedCollection
		// see: http://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=29
	}
	
}
