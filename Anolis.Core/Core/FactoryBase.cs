using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Anolis.Core.Data;
using Anolis.Core.Source;

namespace Anolis.Core {
	
	public abstract class FactoryBase {
		
		public static String CreateFileFilter(String description, params String[] extensions) {
			
			return Utility.Miscellaneous.CreateFileFilter(description, extensions);
		}
		
		protected internal static String[] AssemblyFileNames { get; set; }
		
		private static List<String> _assembliesLoaded = new List<String>();
		private static String       _thisAssemblyFilename = Assembly.GetExecutingAssembly().Location;
		
		protected static void LoadFactoriesFromAssemblies<T>(IList<T> list) where T : class {
			
			if( AssemblyFileNames == null ) return;
			
			foreach(String filename in AssemblyFileNames) {
				
				if( File.Exists( filename ) ) {
					
					LoadFactoriesFromAssembly( list, filename );
					
				}
				
			}
			
		}
		
		private static void LoadFactoriesFromAssembly<T>(IList<T> list, String assemblyFileName) where T : class {
			
			if(_assembliesLoaded.Contains(assemblyFileName.ToUpperInvariant())) return;
			
			_assembliesLoaded.Add( assemblyFileName.ToUpperInvariant() );
			
			if(!File.Exists(assemblyFileName)) return;
			if( String.Equals(assemblyFileName, _thisAssemblyFilename, StringComparison.OrdinalIgnoreCase) ) return;
			
			Assembly assembly;
			
			try {
				
				assembly = Assembly.LoadFile(assemblyFileName);
				
			} catch(FileLoadException) {
				return;
			} catch(BadImageFormatException) {
				return;
			}
			
			list.AddRange2<T>( Utility.Miscellaneous.InstantiateTypes<T>( assembly, typeof(T) ) );
			
		}
		
		/// <summary>Causes this factory to add its options to the FactoryOptions instance</summary>
		public virtual void RegisterOptions() {
		}
		
	}
	
	
	public class FactoryOptions : Dictionary<String,FactoryOptionValue> {
		
		private static FactoryOptions _instance;
		
		public static FactoryOptions Instance {
			get {
				if( _instance == null ) _instance = new FactoryOptions();
				return _instance;
			}
		}
		
		private FactoryOptions() {
		}
		
		/// <summary>Loads all recognised factories which causes them to add their options to the dictionary</summary>
		public static void Populate() {
			
			// ResourceData
			ResourceDataFactory.GetFactories();
			
			// ResourceSource
			ResourceSourceFactory.GetFactories();
			
			// TypeViewers can't be done from this assembly (obviously)
			// so it's Resourcer's responsibility
			
		}
		
		public new Object this[String key] {
			get {
				FactoryOptionValue value = base[key];
				return value.Value;
			}
			set {
				FactoryOptionValue val;
				if( TryGetValue(key, out val) ) val.Value = value;
				else throw new KeyNotFoundException();
			}
		}
		
		public void AddDefault(String key, Object defaultValue, Type type, String description, String category) {
			
			if( base.ContainsKey(key) ) return;
			
			Add( key, new FactoryOptionValue() { Value = defaultValue, ValueType = type, Description = description, Category = category } );
			
		}
		
	}
	
	public class FactoryOptionValue {
		
		public Object Value;
		public Type   ValueType;
		public String Description;
		public String Category;
		
	}
	
}
