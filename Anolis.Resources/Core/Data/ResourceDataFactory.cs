using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

using Anolis.Core.Utility;
using Anolis.Core.Source;

namespace Anolis.Core.Data {
	
	public abstract class ResourceDataFactory : FactoryBase {
		
		/// <summary>Indicates the compatibility between the specified ResourceType and this factory's ResourceData.</summary>
		public abstract Compatibility HandlesType(ResourceTypeIdentifier typeId);
		
		/// <summary>Indicates the compatibility between the specified filename extension and this factory's ResourceData. It is assumed the extension is truthful.</summary>
		public abstract Compatibility HandlesExtension(String fileNameExtension);
		
		/// <summary>Returns null if unsuccessful. Wrap all exceptions in ResourceDataException.</summary>
		public abstract ResourceData FromResource(ResourceLang lang, Byte[] data);
		
		public abstract ResourceData FromFileToAdd   (Stream stream, String extension, UInt16 langId, ResourceSource currentSource);
		public abstract ResourceData FromFileToUpdate(Stream stream, String extension, ResourceLang currentLang);
		
		/// <summary>Gets the (human-readable) message as to why the previously loaded resource could not be loaded.</summary>
		public virtual String LastErrorMessage { get; protected set; }
		
		/// <summary>Gets the (human-readable) name of the data handled by this IResourceDataFactory.</summary>
		public abstract String Name { get; }
		
		/// <summary>Gets the filter to use in open-file dialogs. Return null if opening from files is not supported.</summary>
		public abstract String OpenFileFilter { get; }
		
		protected static Byte[] GetAllBytesFromStream(Stream stream) {
			
			MemoryStream ms = stream as MemoryStream;
			if(ms != null) return ms.ToArray();
			
			Byte[] data = new Byte[ stream.Length ];
			stream.Read( data, 0, data.Length );
			return data;
		}
		
		protected internal static Boolean IsExtension(String extension, params String[] extensions) {
			
			if( extension.StartsWith(".", StringComparison.Ordinal) ) extension = extension.Substring(1);
			
			foreach(String ext in extensions) {
				if( String.Equals( extension, ext, StringComparison.OrdinalIgnoreCase ) ) return true;
			}
			
			return false;
			
		}
		
#region Factory Selection and Loading
		
		private static Pair<ResourceDataFactory, String>[] _openFileFilters;
		
		public static Pair<ResourceDataFactory, String>[] GetOpenFileFilters() {
			
			if( _openFileFilters == null ) {
				
				ResourceDataFactoryCollection factories = GetFactories();
				
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
				
				foreach(ResourceDataFactory factory in GetFactories()) {
					
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
		public static ResourceDataFactory[] GetFactoriesForExtension(String fileNameExtension) {
			
			if( !_forExt.ContainsKey( fileNameExtension ) ) {
				
				List<ResourceDataFactory> yes = new List<ResourceDataFactory>();
				List<ResourceDataFactory> may = new List<ResourceDataFactory>();
				List<ResourceDataFactory> all = new List<ResourceDataFactory>();
				
				foreach(ResourceDataFactory factory in GetFactories()) {
					
					switch(factory.HandlesExtension(fileNameExtension)) {
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
				
				_forExt.Add(fileNameExtension, yes.ToArray());
			
			}
			
			return _forExt[fileNameExtension];
			
		}
		
		private static ResourceDataFactoryCollection _factories;
		
		internal static ResourceDataFactoryCollection GetFactories() {
			
			if( _factories == null ) {
				
				List<ResourceDataFactory> list = new List<ResourceDataFactory>();
				
				Prepopulate( list );
				
				LoadFactoriesFromAssemblies<ResourceDataFactory>(list);
				
				_factories = new ResourceDataFactoryCollection( list );
				
				foreach(FactoryBase factory in _factories) factory.RegisterOptions();
				
			}
			
			return _factories;
			
		}
		
		private static readonly Type _type = typeof(ResourceDataFactory);
		
		private static void Prepopulate(List<ResourceDataFactory> factories) {
			
			// Images
			factories.Add( new BmpImageResourceDataFactory() );
			factories.Add( new GifImageResourceDataFactory() );
			factories.Add( new JpegImageResourceDataFactory() );
			factories.Add( new PngImageResourceDataFactory() );
			factories.Add( new TiffImageResourceDataFactory() );
			
			factories.Add( new IconCursorImageResourceDataFactory() );
			factories.Add( new RiffMediaResourceDataFactory() );
			
			// Directories
			factories.Add( new IconDirectoryResourceDataFactory() );
			factories.Add( new CursorDirectoryResourceDataFactory() );
			
			// Windows
			factories.Add( new VersionResourceDataFactory() );
			factories.Add( new DialogResourceDataFactory() );
			factories.Add( new StringTableResourceDataFactory() );
			factories.Add( new MenuResourceDataFactory() );
			
			// The Rest
			factories.Add( new SgmlResourceDataFactory() );
			factories.Add( new UnknownResourceDataFactory() );
			
		}
		
		#endregion
		
	}
	
	public enum Compatibility {
		/// <summary>This is fully supported.</summary>
		Yes,
		/// <summary>This is supported because it supports everything in some kind of lowest-common-denominator capacity.</summary>
		All,
		/// <summary>This might be supported. It may throw an error or it may succeed.</summary>
		Maybe,
		/// <summary>This is explicitly not supported.</summary>
		No
	}
	
	public class ResourceDataFactoryCollection : ReadOnlyCollection<ResourceDataFactory> {
		public ResourceDataFactoryCollection(IList<ResourceDataFactory> list) : base(list) {
		}
	}
	

	
}
