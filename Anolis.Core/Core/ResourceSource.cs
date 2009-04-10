using System;
using System.Collections.Generic;
using Anolis.Core.Source;

using Anolis.Core.Data;

using Path = System.IO.Path;
using System.IO;

namespace Anolis.Core {
	
	public abstract class ResourceSourceFactory {
		
		public abstract ResourceSource Create(String filename, Boolean isReadOnly, ResourceSourceLoadMode mode);
		
	}
	
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
		
		public void Dispose() {
			
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		
		protected virtual void Dispose(Boolean managed) {
			
			if(managed) {
				
				foreach(ResourceLang lang in this.AllLoadedLangs) {
					
					lang.Data.Dispose();
				}
				
			}
			
		}
		
		//////////////////////
		
		public static ResourceSource Open(String filename, Boolean readOnly, ResourceSourceLoadMode mode) {
			
			// TODO: Adopt a Factory pattern for Resource Sources because different types can share extensions
			// e.g. Managed executes, New Executables, Portable Executables etc
			// and in fact, Managed and PE can both be read by Win32
			
			// get the file type of the file to load
			// if PE executable (a dll, native exe, etc)
			
			// what about the "Resource Template *.rct" format?
			// XN makes a reference to "DCT" as an alias extension for *.res files
			
			String ext = Path.GetExtension(filename).ToUpperInvariant();
			switch(ext) {
				case ".RES":
					return new ResResourceSource(filename, readOnly, mode);
//				case ".ICL":
//					return new NEResourceSource(filename, readOnly, mode);
//				case ".RC":
//					return new RCResourceSource(filename, readOnly, mode);
				default:
					return new Win32ResourceSource(filename, readOnly, mode);
			}
			
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
