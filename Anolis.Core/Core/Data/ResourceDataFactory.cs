using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Anolis.Core.Utility;

namespace Anolis.Core.Data {
	
	public abstract class ResourceDataFactory {
		
		/// <summary>Indicates the compatibility between the specified ResourceType and this factory's ResourceData.</summary>
		public abstract Compatibility HandlesType(ResourceTypeIdentifier typeId);
		
		/// <summary>Indicates the compatibility between the specified filename extension and this factory's ResourceData. It is assumed the extension is truthful.</summary>
		public abstract Compatibility HandlesExtension(String filenameExtension);
		
		/// <summary>Returns null if unsuccessful.</summary>
		public abstract ResourceData FromResource(ResourceLang lang, Byte[] data);
		
		public abstract ResourceData FromFileToAdd   (Stream stream, String extension, UInt16 lang, ResourceSource currentSource);
		public abstract ResourceData FromFileToUpdate(Stream stream, String extension, ResourceLang currentLang);
		
		/// <summary>Gets the (human-readable) message as to why the previously loaded resource could not be loaded.</summary>
		public virtual String LastErrorMessage { get; protected set; }
		
		/// <summary>Gets the (human-readable) name of the data handled by this IResourceDataFactory.</summary>
		public abstract String Name { get; }
		
		/// <summary>Gets the filter to use in open-file dialogs. Return null if opening from files is not supported.</summary>
		public abstract String OpenFileFilter { get; }
		
		protected static Byte[] GetAllBytesFromStream(Stream stream) {
			
			if( stream is MemoryStream ) {
				return (stream as MemoryStream).ToArray();
			}
			
			Byte[] data = new Byte[ stream.Length ];
			stream.Read( data, 0, data.Length );
			return data;
		}
		
#region Factory Selection and Loading
		
		private static Pair<ResourceDataFactory, String>[] _openFileFilters;
		
		public static Pair<ResourceDataFactory, String>[] GetOpenFileFilters() {
			
			if( _openFileFilters == null ) {
				
				ResourceDataFactoryCollection factories = GetFactories(null);
				
				List<Pair<ResourceDataFactory, String>> filters = new List<Pair<ResourceDataFactory, String>>(factories.Count);
				
				for(int i=0;i<factories.Count;i++) {
					
					String filter = factories[i].OpenFileFilter;
					
					if(filter != null) filters.Add( new Pair<ResourceDataFactory,String>(factories[i], filter) );
				}
				
				_openFileFilters = filters.ToArray();
			}
			
			return _openFileFilters;
			
		}
		
		private static Dictionary<ResourceTypeIdentifier,ResourceDataFactory[]> _forType = new Dictionary<ResourceTypeIdentifier,ResourceDataFactory[]>();
		private static Dictionary<String                ,ResourceDataFactory[]> _forExt  = new Dictionary<String,ResourceDataFactory[]>();
		
		/// <summary>Gets all the factories that support a resource type in order of compatibility.</summary>
		public static ResourceDataFactory[] GetFactoriesForType(ResourceTypeIdentifier typeId) {
			
			if( !_forType.ContainsKey( typeId ) ) {
				
				List<ResourceDataFactory> yes = new List<ResourceDataFactory>();
				List<ResourceDataFactory> may = new List<ResourceDataFactory>();
				List<ResourceDataFactory> all = new List<ResourceDataFactory>();
				
				foreach(ResourceDataFactory factory in GetFactories(null)) {
					
					switch(factory.HandlesType(typeId)) {
						case Compatibility.Yes:
							yes.Add( factory ); break;
						case Compatibility.Maybe:
							may.Add( factory ); break;
						case Compatibility.All:
							all.Add( factory ); break;
					}
					
				}
				
				yes.AddRange( may );
				yes.AddRange( all );
				
				_forType.Add(typeId, yes.ToArray());
				
			}
			
			return _forType[typeId];
			
		}
		
		/// <summary>Gets all the factories that support a file type in order of compatibility.</summary>
		public static ResourceDataFactory[] GetFactoriesForExtension(String filenameExtension) {
			
			if( !_forExt.ContainsKey( filenameExtension ) ) {
				
				List<ResourceDataFactory> yes = new List<ResourceDataFactory>();
				List<ResourceDataFactory> may = new List<ResourceDataFactory>();
				List<ResourceDataFactory> all = new List<ResourceDataFactory>();
				
				foreach(ResourceDataFactory factory in GetFactories(null)) {
					
					switch(factory.HandlesExtension(filenameExtension)) {
						case Compatibility.Yes:
							yes.Add( factory ); break;
						case Compatibility.Maybe:
							may.Add( factory ); break;
						case Compatibility.All:
							all.Add( factory ); break;
					}
					
				}
				
				yes.AddRange( may );
				yes.AddRange( all );
				
				_forExt.Add(filenameExtension, yes.ToArray());
			
			}
			
			return _forExt[filenameExtension];
			
		}
		
		private static ResourceDataFactoryCollection _factories;
		
		private static ResourceDataFactoryCollection GetFactories(FactoryLocation[] locations) {
			
			if( _factories == null ) {
				
				if(locations == null) locations = new FactoryLocation[] { new FactoryLocation( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ), FactoryLocationType.Directory ) };
				
				List<ResourceDataFactory> list = new List<ResourceDataFactory>();
				
				Prepopulate( list );
				
				foreach(FactoryLocation loc in locations) {
					
					switch(loc.Type) {
//						case FactoryLocationType.Directory:
//							
//							// recursivly search for all DLLs
//							if(Directory.Exists(loc.Path)) RecurseDirectory(list, new DirectoryInfo(loc.Path));
//							
//							break;
						case FactoryLocationType.Filename:
							
							LoadFactoriesFromAssembly(list, loc.Path);
							
							break;
						case FactoryLocationType.Wildcard:
							
							throw new NotSupportedException();
//							
//							String path = loc.Path.Substring(0, 
//							
//							break;
					}
					
				}
				
				_factories = new ResourceDataFactoryCollection( list );
				
			}
			
			return _factories;
			
		}
		
		private static readonly Type _type = typeof(ResourceDataFactory);
		
		private static void RecurseDirectory(List<ResourceDataFactory> list, DirectoryInfo directory) {
			
			foreach(FileInfo file in directory.GetFiles("*.dll")) {
				
				LoadFactoriesFromAssembly(list, file.FullName);
				
			}
			
			foreach(DirectoryInfo dir in directory.GetDirectories()) {
				RecurseDirectory(list, dir );
			}
			
		}
		
		private static List<String> _assembliesLoaded = new List<String>();
		private static String       _thisAssemblyFilename = Assembly.GetExecutingAssembly().Location.ToLowerInvariant();
		
		private static void LoadFactoriesFromAssembly(List<ResourceDataFactory> list, String assemblyFilename) {
			
			if(_assembliesLoaded.Contains(assemblyFilename.ToLowerInvariant())) return;
			
			_assembliesLoaded.Add( assemblyFilename.ToLowerInvariant() );
			
			if(!File.Exists(assemblyFilename)) return;
			if( String.Equals(assemblyFilename, _thisAssemblyFilename, StringComparison.OrdinalIgnoreCase) ) return;
			
			Assembly assembly;
			
			try {
				
				assembly = Assembly.LoadFile(assemblyFilename);
				
			} catch(FileLoadException) {
				return;
			} catch(BadImageFormatException) {
				return;
			}
			
			Type[] types = assembly.GetTypes();
			
			foreach(Type t in types) {
				
				if( t.IsSubclassOf( _type ) ) {
					
					ResourceDataFactory f = Activator.CreateInstance( t ) as ResourceDataFactory;
					list.Add( f );
					
				}
				
			}
			
		}
		
		private static void Prepopulate(List<ResourceDataFactory> factories) {
			
			// Images
			factories.Add( new BmpImageResourceDataFactory() );
			factories.Add( new GifImageResourceDataFactory() );
			factories.Add( new JpegImageResourceDataFactory() );
			factories.Add( new PngImageResourceDataFactory() );
			
			factories.Add( new IconImageResourceDataFactory() );
			factories.Add( new CursorImageResourceDataFactory() );
			
			factories.Add( new RiffMediaResourceDataFactory() );
			
			// Directories
			factories.Add( new IconDirectoryResourceDataFactory() );
//			factories.Add( new CursorDirectoryResourceDataFactory() );
			
			// Windows
			factories.Add( new VersionResourceDataFactory() );
			factories.Add( new DialogResourceDataFactory() );
			factories.Add( new StringTableResourceDataFactory() );
			factories.Add( new MenuResourceDataFactory() );
			
			// The Rest
			factories.Add( new SgmlResourceDataFactory() );
			factories.Add( new UnknownResourceDataFactory() );
			
		}
		
	}
	
#endregion
	
	public class FactoryLocation {
		
		public String              Path { get; private set; }
		public FactoryLocationType Type { get; private set; }
		
		public FactoryLocation(String path, FactoryLocationType type) {
			Path = path;
			Type = type;
		}
		
	}
	
	public enum FactoryLocationType {
		Filename,
		Directory,
		Wildcard
	}
	
	public enum Compatibility {
		/// <summary>The ResourceData supports this type.</summary>
		Yes,
		/// <summary>The ResourceData supports all types. It will be de-prioritised.</summary>
		All,
		/// <summary>The ResourceData supports this type under certain conditions.</summary>
		Maybe,
		/// <summary>The ResourceData will never support this type no matter what the conditions.</summary>
		No
	}
	
	public class ResourceDataFactoryCollection : ReadOnlyCollection<ResourceDataFactory> {
		public ResourceDataFactoryCollection(IList<ResourceDataFactory> list) : base(list) {
		}
	}
	

	
}
