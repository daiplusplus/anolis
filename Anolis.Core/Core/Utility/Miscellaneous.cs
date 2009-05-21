using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using Anolis.Core.Source;

namespace Anolis.Core.Utility {
	
	public static class Miscellaneous {
		
		public static String CreateFileFilter(String description, params String[] extensions) {
			
			StringBuilder sb = new StringBuilder();
			
			sb.Append( description );
			sb.Append( " (" );
			
			for(int i=0;i<extensions.Length;i++) {
				String ext = extensions[i];
				
				sb.Append("*.");
				sb.Append( ext );
				if(i < extensions.Length - 1) sb.Append(';');
				
			}
			
			sb.Append(")|");
			
			for(int i=0;i<extensions.Length;i++) {
				String ext = extensions[i];
				
				sb.Append("*.");
				sb.Append( ext );
				if(i < extensions.Length - 1) sb.Append(';');
				
			}
			
			String retval = sb.ToString();
			
			return retval;
			
		}
		
		public static String TrimPath(String path, Int32 maxLength) {
			
			Char[] chars = new Char[] { '/', '\\' };
			
			if(path.Length <= maxLength) return path;
			
			String truncated = path;
			while(truncated.Length > maxLength) {
				// take stuff out from the middle
				// so starting from the middle search for the first slash remove it to the next slash on
				Int32 midSlashIdx = truncated.LastIndexOfAny( chars, truncated.Length / 2 );
				if( midSlashIdx == -1 ) return truncated;
				
				Int32 nextSlashForwardIdx = truncated.IndexOfAny( chars, midSlashIdx + 4 );
				
				truncated = truncated.Substring(0, midSlashIdx) + @"\.." + truncated.Substring( nextSlashForwardIdx );
			}
			
			return truncated;
		}
		
		public static String RemoveIllegalFilenameChars(String s, Char? replaceWith) {
			
			Char[] illegals = System.IO.Path.GetInvalidFileNameChars();
			Char[] str      = s.ToCharArray();
			
			if(replaceWith == null) {
				
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				
				for(int i=0;i<s.Length;i++) {
					
					Boolean isIllegal = false;
					for(int j=0;j<illegals.Length;j++) {
						if(str[i] == illegals[j]) {
							isIllegal = true;
							break;
						}
					}
					
					if(!isIllegal) sb.Append( str[i] );
					
				}
				
				return sb.ToString();
				
			} else {
				
				for(int i=0;i<s.Length;i++) {
					
					Boolean isIllegal = false;
					for(int j=0;j<illegals.Length;j++) {
						if(str[i] == illegals[j]) {
							isIllegal = true;
							break;
						}
					}
					
					if(isIllegal) str[i] = replaceWith.Value;
					
				}
				
				return new String( str );
				
			}
			
		}
		
		public static String FSSafeResPath(String path) {
			
			path = path.Replace('\\', '-');
			
			return RemoveIllegalFilenameChars( path, '_' );
		}
		
#region Extensibility
		
		/// <summary>Tells the factories where they can find additional assemblies. This only takes effect if the factories haven't already enumerated types</summary>
		public static void SetAssemblyFilenames(String[] assemblies) {
			
			FactoryBase.AssemblyFilenames = assemblies;
		}
		
		public static Boolean IsAssembly(String filename) {
			
			try {
				
				AssemblyName name = AssemblyName.GetAssemblyName( filename );
				
				return true;
			
			} catch( ArgumentException ) {
				
				return false;
				
			} catch( BadImageFormatException ) {
				
				return false;
			}
			
		}
		
		/// <summary>Searches the specified assembly for objects derived (or implementing) superType then instantiates and returns them.</summary>
		public static T[] InstantiateTypes<T>(Assembly assembly, Type superType) where T : class {
			
			Type[] types = assembly.GetTypes();
			
			List<T> retval = new List<T>();
			
			if( superType.IsInterface ) {
				
				foreach(Type t in types) {
					
					if( t.IsClass && !t.IsAbstract ) continue;
					
					if( t.GetInterface( superType.FullName ) == null ) continue;
					
					// if it implements the interface and is constructable
					ConstructorInfo constructor = t.GetConstructor( new Type[] { } );
					if(constructor != null) retval.Add( Activator.CreateInstance( t ) as T );
					
				}
				
			} else if( superType.IsClass ) {
				
				foreach(Type t in types) {
					
					if( t.IsClass && !t.IsAbstract ) continue;
					
					if( t.IsSubclassOf( superType ) ) {
						
						// if it derives from superType and is constructable
						ConstructorInfo constructor = t.GetConstructor( new Type[] { } );
						if(constructor != null) retval.Add( Activator.CreateInstance( t ) as T );
						
					}
					
				}
				
			}
			
			return retval.ToArray();
			
		}
		
#endregion
		
	}
}
