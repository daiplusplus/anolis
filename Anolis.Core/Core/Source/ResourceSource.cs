using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Anolis.Core.Source;

using Anolis.Core.Data;

using Path = System.IO.Path;
using System.IO;

namespace Anolis.Core {
	
	namespace Source {
		
		public abstract class ResourceSourceFactory : FactoryBase {
			
#region Static
			
			private static ReadOnlyCollection<ResourceSourceFactory> _factoriesRo;
			private static List<ResourceSourceFactory> _factories;
			
			public static ReadOnlyCollection<ResourceSourceFactory> ListFactories() {
				
				if( _factories == null ) {
					
					// Prepopulate
					_factories = new List<ResourceSourceFactory>();
					_factories.Add( new Win32ResourceSourceFactory() );
					_factories.Add( new ResResourceSourceFactory() );
					
					LoadFactoriesFromAssemblies<ResourceSourceFactory>( _factories );
					
					_factoriesRo = new ReadOnlyCollection<ResourceSourceFactory>( _factories );
				}
				
				return _factoriesRo;
			}
			
			public static ResourceSourceFactory GetFactoryForExtension(String extension) {
				
				// return the last match, since that will be the most specific
				
				ResourceSourceFactory yesMatch   = null;
				ResourceSourceFactory maybeMatch = null;
				ResourceSourceFactory allMatch   = null;
				
				IList<ResourceSourceFactory> factories = ListFactories();
				foreach(ResourceSourceFactory factory in factories) {
					
					Compatibility compat = factory.HandlesExtension( extension );
					
					switch(compat) {
						case Compatibility.Yes:
							yesMatch = factory;
							break;
						case Compatibility.Maybe:
							maybeMatch = factory;
							break;
						case Compatibility.All:
							allMatch = factory;
							break;
					}
					
				}
				
				if(yesMatch != null ) return yesMatch;
				if( maybeMatch != null ) return maybeMatch;
				return allMatch;
				
			}
				
#endregion
			
#region Instance
			
			public abstract Compatibility HandlesExtension(String extension);
			
			public abstract ResourceSource Create(String fileName, Boolean isReadOnly, ResourceSourceLoadMode mode);
			
			public virtual ResourceSource CreateNew(String fileName, Boolean isReadOnly, ResourceSourceLoadMode mode) {
				
				CreateNew( fileName );
				
				return Create( fileName, isReadOnly, mode );
			}
			
			protected abstract void CreateNew(String fileName);
			
			public abstract String OpenFileFilter {
				get;
			}
			
			public abstract String NewFileFilter {
				get;
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
		
		public static ResourceSource Open(String fileName, Boolean readOnly, ResourceSourceLoadMode mode) {
			
			String ext = Path.GetExtension(fileName).ToUpperInvariant();
			if(ext.StartsWith(".", StringComparison.Ordinal)) ext = ext.Substring(1);
			
			ResourceSourceFactory factory = ResourceSourceFactory.GetFactoryForExtension( ext );
			
			return factory.Create( fileName, readOnly, mode );
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
