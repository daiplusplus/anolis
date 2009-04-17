using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Anolis.Core.Source;

using Anolis.Core.Data;

using Path = System.IO.Path;
using System.IO;

namespace Anolis.Core {
	
	namespace Source {
		
		public abstract class ResourceSourceFactory {
			
#region Static
			
			private static ReadOnlyCollection<ResourceSourceFactory> _factoriesRo;
			private static List<ResourceSourceFactory> _factories;
			
			public static ReadOnlyCollection<ResourceSourceFactory> ListFactories() {
				
				if( _factories == null ) {
					_factories = new List<ResourceSourceFactory>();
					_factories.Add( new Win32ResourceSourceFactory() );
					_factories.Add( new ResResourceSourceFactory() );
					
					_factoriesRo = new ReadOnlyCollection<ResourceSourceFactory>( _factories );
				}
				
				return _factoriesRo;
			}
			
			public static ResourceSourceFactory GetFactoryForExtension(String extension) {
				
				// just return the first match
				
				ResourceSourceFactory maybeMatch = null;
				ResourceSourceFactory allMatch   = null;
				
				IList<ResourceSourceFactory> factories = ListFactories();
				foreach(ResourceSourceFactory factory in factories) {
					
					Compatibility compat = factory.HandlesExtension( extension );
					
					switch(compat) {
						case Compatibility.Yes:
							return factory;
						case Compatibility.Maybe:
							if( maybeMatch != null ) maybeMatch = factory;
							break;
						case Compatibility.All:
							if( allMatch != null ) allMatch = factory;
							break;
					}
					
				}
				
				if( maybeMatch != null ) return maybeMatch;
				return allMatch;
				
			}
				
#endregion
			
#region Instance
			
			public abstract Compatibility HandlesExtension(String extension);
			
			public abstract ResourceSource Create(String filename, Boolean isReadOnly, ResourceSourceLoadMode mode);
			
			protected abstract String GetOpenFileFilter();
			
			private String _off;
			
			public virtual String OpenFileFilter {
				get {
					if( _off == null ) _off = GetOpenFileFilter();
					return _off;
				}
			}
			
#endregion
		}
		
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
			
			String ext = Path.GetExtension(filename).ToUpperInvariant();
			if(ext.StartsWith(".")) ext = ext.Substring(1);
			
			ResourceSourceFactory factory = ResourceSourceFactory.GetFactoryForExtension( ext );
			
			return factory.Create( filename, readOnly, mode );
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
