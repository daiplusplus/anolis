using System;
using System.IO;

using W3b.TarLzma;

namespace Anolis.Core.Packages {
	
	public class LzmaTarPackageArchive : PackageArchive {
		
		public override Package GetPackage() {
			
			if( State != PackageArchiveState.Extracted ) throw new InvalidOperationException("Cannot open the package when it hasn't been extracted");
			
			String packageXmlFilename = Path.Combine( _rootDirectory.FullName, "Package.xml" ); 
			if( !System.IO.File.Exists( packageXmlFilename ) ) throw new FileNotFoundException("Package does not contain a definition file", packageXmlFilename);
			
			return PackageReader.ReadPackage( packageXmlFilename );
			
		}
	}
}
