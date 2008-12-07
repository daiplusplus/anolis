using System;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Core {
	
	public abstract partial class ResourceSource : IDisposable {
		
		protected ResourceSource(Boolean readOnly) {
			
			IsReadOnly = readOnly;
			
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
			
			ResourceSource src = new PE.PESource(filename, readOnly);
			
			// TODO Loading ResourceSources
			
			throw new NotImplementedException();
			
		}
		
		//////////////////////
		// ResourceSource State Mutators
		
		public abstract void CommitChanges();
		
		public virtual void Rollback() {
			
			// TODO reset all the resourcedatas
			
		}
		
	}
}
