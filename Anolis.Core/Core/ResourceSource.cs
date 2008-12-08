using System;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Core {
	
	public abstract partial class ResourceSource : IDisposable {
		
		protected ResourceSource(Boolean readOnly) {
			
			IsReadOnly = readOnly;
			
			InitResourceSourceCollections();
			
		}
		
		//////////////////////
		
		public Boolean IsReadOnly { get; private set; }
		
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
					return new PE.PESource(filename, readOnly);
			}
			
			throw new NotImplementedException("Anolis does not support files that aren't PE/COFF Executables as ResourceSources yet");
			
		}
		
		//////////////////////
		// ResourceSource State Mutators
		
		public abstract void CommitChanges();
		
		public virtual void Rollback() {
			
			// TODO reset all the resourcedatas
			
		}
		
	}
}
