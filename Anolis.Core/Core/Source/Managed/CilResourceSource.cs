using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.IO;

namespace Anolis.Core.Source {
	
	public class CilResourceSource : ManagedResourceSource {
		
		// CLR Resources don't follow the same model as Win32 resources, so a special class is made for those
		
		// maybe in future consider support for Jar/Java Archives and their resources? Wouldn't that be amusing
		// JARs are just PKZip archives with raw files for resources, which is very similar to how CIL works, hmm
		// Java's might be more complicated, and they already have a few Resource Editors for it
		// leave it for now...
		
		private FileInfo _file;
		private Assembly _assembly;
		
		private CilResourceSource(Assembly assembly) {
			
			_assembly = assembly;
			_file     = new FileInfo( assembly.Location );
		}
		
		public CilResourceSource(String fileName) {
			
			if(fileName == null) throw new ArgumentNullException("fileName");
			
			_file = new FileInfo( fileName );
			
			if( !_file.Exists ) throw new FileNotFoundException("The specified file was not found", fileName);
			
			try {
				
				_assembly = Assembly.LoadFile( _file.FullName );
				
			} catch(BadImageFormatException bex) {
				
				throw new AnolisException("Failed to load CLR assembly", bex);
				
			} catch(FileLoadException fex) {
				
				throw new AnolisException("Failed to load CLR assembly", fex);
			}
			
		}
		
		public static CilResourceSource OpenEntryAssembly() {
			
			return new CilResourceSource( Assembly.GetEntryAssembly() );
		}
		
		public override ManagedResourceInfo[] GetResourceInfo() {
			
			String[] names = _assembly.GetManifestResourceNames();
			
			List<ManagedResourceInfo> res = new List<ManagedResourceInfo>();
			
			foreach(String name in names) {
				
				ManifestResourceInfo manInfo = _assembly.GetManifestResourceInfo(name);
				
				ManagedResourceInfo info = new ManagedResourceInfo( this, name, manInfo.ResourceLocation );
				
				res.Add( info );
			}
			
			return res.ToArray();
		}
		
		public override Stream GetResourceStream(ManagedResourceInfo resource) {
			
			if(resource.Source != this) throw new ArgumentException("The specified ManagedResourceInfo did not originate from this ResourceSource", "resource");
			
			return _assembly.GetManifestResourceStream( resource.Name );
		}
		
		
		
	}
}
