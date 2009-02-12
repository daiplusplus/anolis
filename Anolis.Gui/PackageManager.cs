using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using Anolis.Core.Packages;
using System.Collections;

namespace Anolis.Gui {
	
	public static class PackageManager {
		
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
						
						if( k.EndsWith("_anop") ) packages.Add( k.Substring(0, k.LastIndexOf("_anop") ) );
						
					}
					
				}
				
			}
			
			return packages.ToArray();
			
		}
		
		public static Stream GetEmbeddedPackage(Assembly inAssembly, String name) {
			
			return inAssembly.GetManifestResourceStream(name);
			
		}
		
	}
}
