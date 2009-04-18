﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace Anolis.Core {
	
	public class FactoryBase {
		
		public static String CreateFileFilter(String description, params String[] extensions) {
			
			return Utility.Miscellaneous.CreateFileFilter(description, extensions);
		}
		
		protected internal static String[] AssemblyFilenames { get; set; }
		
		private static List<String> _assembliesLoaded = new List<String>();
		private static String       _thisAssemblyFilename = Assembly.GetExecutingAssembly().Location.ToLowerInvariant();
		
		protected static void LoadFactoriesFromAssemblies<T>(List<T> list) where T : class {
			
			if( AssemblyFilenames == null ) return;
			
			foreach(String filename in AssemblyFilenames) {
				
				if( File.Exists( filename ) ) {
					
					LoadFactoriesFromAssembly( list, filename );
					
				}
				
			}
			
		}
		
		private static void LoadFactoriesFromAssembly<T>(List<T> list, String assemblyFilename) where T : class {
			
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
			
			list.AddRange( Utility.Miscellaneous.InstantiateTypes<T>( assembly, typeof(T) ) );
			
		}
		
	}
}
