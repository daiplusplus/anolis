using System;
using System.Collections.Generic;
using Anolis.Core.Source;

using Anolis.Core.Data;

namespace Anolis.Core {
	
	public abstract partial class ResourceSource : IDisposable {
		
		protected ResourceSource(Boolean isReadOnly, ResourceSourceLoadMode mode) {
			
			IsReadOnly = isReadOnly;
			LoadMode   = mode;
			
			InitialiseEnumerables();
		}
		
		//////////////////////
		
		public Boolean                IsReadOnly { get; private set; }
		public ResourceSourceLoadMode LoadMode   { get; private set; }
		
		/// <summary>Returns information about this ResourceSource in a Key/Value pair.</summary>
		public virtual ResourceSourceInfo SourceInfo { get { return null; } }
		
		public abstract String Name { get; }
		
		//////////////////////
		
		/// <summary>Extracts the Resource Data for the specified Resource.</summary>
		public abstract ResourceData GetResourceData(ResourceLang lang);
		
		public virtual void Dispose() {}
		
		//////////////////////
		
		public static ResourceSource Open(String filename, Boolean readOnly, ResourceSourceLoadMode mode) {
			
			// get the file type of the file to load
			// if PE executable (a dll, native exe, etc)
			
//			String ext = System.IO.Path.GetExtension(filename).ToUpperInvariant();
//			switch(ext) {
//				case ".EXE":
//				case ".DLL":
//				case ".SCR":
//				case ".CPL":
					return new PEResourceSource(filename, readOnly, mode);
//			}
//			
//			throw new NotImplementedException("Anolis does not support files that aren't PE/COFF Executables as ResourceSources yet");
			
		}
		
		//////////////////////
		// ResourceSource State Mutators
		
		/// <summary>Commits all pending operations (like Adding, Updates, and Deletions) to the source. After completion all ResourceData instances will have an Action of None.</summary>
		/// <remarks>Unless Blind mode is specified during construction, the ResourceSource subclass may opt to fully reload itself after commiting changes.</remarks>
		public abstract void CommitChanges();
		
		/// <summary>Actually reloads all the data into the source.</summary>
		public abstract void Reload();
		
	}
	
	public class ResourceSourceInfo : Dictionary<String,String> {
		// TODO: Maybe later make a ReadOnlyKeyedCollection
		// see: http://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=29
	}
	
	public enum ResourceSourceLoadMode {
		/// <summary>The ResourceSource will not enumerate ResourceLangs nor load ResourceData (even if lazy-loaded)</summary>
		Blind          = 0,
		/// <summary>The ResourceSource will enumerate ResourceLangs when created, but will not load ResourceData (even if lazy-loaded)</summary>
		EnumerateOnly  = 1,
		/// <summary>The ResourceSource will enumerate ResourceLangs when created and will load ResourceData lazily</summary>
		LazyLoadData   = 2,
		/// <summary>The ResourceSource will enumerate ResourceLangs when created and will load all ResourceData preemptively</summary>
		PreemptiveLoad = 3
	}
	
}
