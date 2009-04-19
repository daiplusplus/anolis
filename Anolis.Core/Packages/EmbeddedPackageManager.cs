using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using Anolis.Core.Packages;
using System.Collections;

namespace Anolis.Core.Packages {
	
	public class EmbeddedPackage {
		
		internal EmbeddedPackage(String name, Assembly assembly, String manifestResourceName) {
			
			Name                 = name;
			
			Assembly             = assembly;
			ManifestResourceName = manifestResourceName;
		}
		
		public String Name { get; private set; }
		
		internal Assembly Assembly             { get; set; }
		internal String   ManifestResourceName { get; set; }
		
		public override String ToString() {
			return Name;
		}
	}
	
	public static class EmbeddedPackageManager {
		
		// To create binary .resources from stuff use ResourceWriter (this is what ResGen.exe does internally)
		// To merge the .resources with the assembly use the Assembly Linker al.exe; I don't know of any way to do this programatically
		
		public static EmbeddedPackage[] GetEmbeddedPackages() {
			
			return GetEmbeddedPackages( Assembly.GetEntryAssembly() );
			
		}
		
		public static EmbeddedPackage[] GetEmbeddedPackages(Assembly inAssembly) {
			
			List<EmbeddedPackage> packages = new List<EmbeddedPackage>();
			
			String[] resourceNames = inAssembly.GetManifestResourceNames();
			foreach(String resName in resourceNames) {
				
				using(Stream stream = inAssembly.GetManifestResourceStream( resName )) {
					
					ResourceSet set = new ResourceSet( stream );
					foreach(DictionaryEntry e in set) {
						
						String k = e.Key as String;
						if( k == null ) continue;
						
						if( k.EndsWith("_anop", StringComparison.Ordinal) ) k = k.Substring(0, k.Length - 5);
						
						packages.Add( new EmbeddedPackage( k, inAssembly, resName ) );
					}
					
				}
				
			}
			
			return packages.ToArray();
			
		}
		
		public static Stream GetEmbeddedPackage(EmbeddedPackage package) {
			
			using(Stream stream = package.Assembly.GetManifestResourceStream( package.ManifestResourceName ) ) {
				
				ResourceSet set = new ResourceSet( stream );
				foreach(DictionaryEntry e in set) {
					
					String k = e.Key as String;
					if( k == null ) continue;
					
					if( k.EndsWith("_anop", StringComparison.Ordinal) ) {
						 k = k.Substring(0, k.Length - 5 );
						 
						 if( k == package.Name ) {
							
							return e.Value as Stream;
						 }
						 
					}
					
					
				}
				
			}
			
			throw new AnolisException("Unable to load embedded package.");
			
		}
		
	}
}
