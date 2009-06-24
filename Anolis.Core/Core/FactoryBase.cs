using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

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
		
	}
}
