using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using Anolis.Core.Packages;
using System.Collections;

namespace Anolis.Gui {
	
	public static class EmbeddedPackageManager {
		
		
#region Embedded Packages
		
		// To create binary .resources from stuff use ResourceWriter (this is what ResGen.exe does internally)
		// To merge the .resources with the assembly use the Assembly Linker al.exe; I don't know of any way to do this programatically
		
		public static String[] GetEmbeddedPackages() {
			
			return GetEmbeddedPackages( Assembly.GetExecutingAssembly() );
			
		}
		
		public static String[] GetEmbeddedPackages(Assembly inAssembly) {
			
			List<String> packages = new List<String>();
			
			String[] resourceNames = inAssembly.GetManifestResourceNames();
			foreach(String resName in resourceNames) {
				
				using(Stream stream = inAssembly.GetManifestResourceStream( resName )) {
					
					ResourceSet set = new ResourceSet( stream );
					foreach(DictionaryEntry e in set) {
						
						String k = e.Key as String;
						if( k == null ) continue;
						
						if( k.EndsWith("_anop") ) packages.Add( k.Substring(0, k.Length - 5) );
						
					}
					
				}
				
			}
			
			return packages.ToArray();
			
		}
		
		public static Stream GetEmbeddedPackage(Assembly assembly, String name) {
			
			throw new NotImplementedException();
			
		}
		
#endregion
		
	}
}
