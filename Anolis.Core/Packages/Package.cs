using System;
using System.Drawing;
using System.Xml;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Anolis.Core.Packages {
	
	/// <summary>Represents all the packages included in a distribution (or are available to the executable)</summary>
	public class PackageContext {
		
	}
	
	/// <summary>Represents a collection of resource sets</summary>
	public class Package {
		
	}
	
	public class Set {
		
		
		
	}
	
	public class File {
		
		/// <summary>Gets the Path to the file as specified in the Package definition file.</summary>
		public String Path { get; private set; }
		
		/// <summary>Gets the actual, working, path to the file (if it exists).</summary>
		public String ResolvedPath { get; private set; }
		
	}
	
	public class ResourceOperation {
		
	}
	
	public class Extra {
		
	}
	
}
