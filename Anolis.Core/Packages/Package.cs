using System;
using System.Drawing;
using System.Xml;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Anolis.Core.Packages {
	
	public abstract class PackageItem {
		
		public Boolean Enabled     { get; set; }
		public String  Name        { get; protected set; }
		public String  Description { get; protected set; }
		
	}
	
	/// <summary>Represents all the packages included in a distribution (or are available to the executable)</summary>
	public class PackageContext {
		
		public void AddPackage(Package package) {
			
		}
		
	}
	
	/// <summary>Represents a collection of resource sets</summary>
	public class Package : PackageItem {
		
		public Boolean Enabled { get; set; }
		
	}
	
	public class Set : PackageItem {
		
		
		
	}
	
	public class File : PackageItem {
		
		/// <summary>Gets the Path to the file as specified in the Package definition file.</summary>
		public String Path { get; private set; }
		
		/// <summary>Gets the actual, working, path to the file (if it exists).</summary>
		public String ResolvedPath { get; private set; }
		
		public FileCondition Condition { get; private set; }
		
	}
	
	public class ResourceOperation : PackageItem {
		
	}
	
	public class Extra : PackageItem {
		
	}
	
	public class FileCondition {
		
		// Condition variables:
		// File version, Target processor architecture (non-.NET dlls/exes)
		// Don't work on file creation/modified/accessed dates of course
		
		// Processor architecture comes from the 'Machine' field in the COFF header inside a PE file
		// Refer to the PE/COFF file format specification
		
		// There is also the VERSION resource you can query too
		// I think, maybe, the COFF timestamp field is a good field too
		
		public FileCondition(String attributeValue) {
			
		}
		
		
		
	}
	
}
