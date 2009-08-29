using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;

using Cult = System.Globalization.CultureInfo;
using Anolis.Core.Utility;

namespace Anolis.Packager {
	
	public class PackageOptimizer {
		
		private String                _fileName;
		private XmlDocument           _doc;
		private List<CompositedImage> _compImages;
		
		public PackageOptimizer(String fileName) {
			
			_fileName   = fileName;
			
			_doc        = new XmlDocument();
			_compImages = new List<CompositedImage>();
		}
		
		public String XmlFileName {
			get { return _fileName; }
		}
		
		public List<String> LoadAndValidate() {
			
			// copypasted from Package.Load
			
			List<String> ret = new List<String>();
			
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.Schemas.Add( Anolis.Core.Packages.Package.PackageSchema );
			settings.ValidationEventHandler += new ValidationEventHandler( delegate(Object sender, ValidationEventArgs ve) {
				
				String line = ve.Message;
				if( ve.Exception != null ) line += " - " + ve.Exception.Message;
				
				ret.Add( line );
				
			});
			
			settings.Schemas.Compile();
			
			settings.ValidationType = ValidationType.Schema;
			
			_doc = new XmlDocument();
			_doc.PreserveWhitespace = true;
			
			using(XmlReader rdr = XmlReader.Create( _fileName, settings )) {
				
				try {
					
					_doc.Load( rdr );
					
				} catch(XmlException xex) {
					
					String message = String.Format(Cult.InvariantCulture, "XML Parsing Error: \"{0}\" on line {1} position {2}", xex.Message, xex.LineNumber, xex.LinePosition);
					
					ret.Add( message );
					
					return ret;
				}
				
			}
			
			_doc.Validate( new ValidationEventHandler( delegate(Object sender, ValidationEventArgs ve) {
				
				
				String line = ve.Message;
				if( ve.Exception != null ) line += " - " + ve.Exception.Message;
				
				ret.Add( line );
				
			}) );
			
			return ret;
			
			// then check for human errors, like the same name being set more than once in the same respatch resource group
			
			
			
		}
		
		public void GetFiles(out String[] missingFiles, out String[] unreferencedFiles) {
			
			// hunt the directory the XML file is in for files that aren't referenced by the XML file
			
			// the most efficient way to find the result of list subtraction is:
			//	foreach(element in unsortedList) {
			//		if( sortedList.BinarySearch( element ) < 0 ) result.Add( element )
			//	}
			
			String[] referencedFiles = GetReferencedFiles(); // unsorted, subset
			String[] physicalFilesAr = GetPhysicalFiles();   // sorted, superset
			
			Array.Sort( physicalFilesAr, new CaseInsensitiveComparer() );
			
			List<String> physicalFiles = new List<String>( physicalFilesAr );
			
			List<String> missing = new List<String>();
			
			// eh... inefficient way FTW
			foreach(String file in referencedFiles) {
				
				Int32 idx = physicalFiles.BinarySearch( file, new CaseInsensitiveComparer() );
				
				if( idx < 0 ) {
					// the referenced file is not found
					missing.Add( file );
				} else {
					physicalFiles.RemoveAt( idx );
				}
				
			}
			
			missingFiles      = missing.ToArray();
			unreferencedFiles = physicalFiles.ToArray();
			
		}
		
		private class CaseInsensitiveComparer : IComparer<String> {
			
			public Int32 Compare(string x, string y) {
				return String.Compare( x, y, StringComparison.OrdinalIgnoreCase );
			}
		}
		
		private String[] GetPhysicalFiles() {
			
			DirectoryInfo root = new DirectoryInfo( Path.GetDirectoryName( _fileName ) );
			
			List<String> allFiles = new List<String>();
			ProcessDirectory( root.FullName.Length, root, allFiles );
			
			return allFiles.ToArray();
		}
		
		private static void ProcessDirectory(Int32 rootLength, DirectoryInfo dir, List<String> files) {
			
			String[] theFiles = Directory.GetFiles( dir.FullName );
			for(int i=0;i<theFiles.Length;i++) theFiles[i] = theFiles[i].Substring( rootLength + 1 ).ToLowerInvariant(); // +1 for the '\'
			
			Array.Sort( theFiles );
			files.AddRange( theFiles );
			
			DirectoryInfo[] theDirs = dir.GetDirectories();
			Array.Sort( theDirs, (dx, dy) => dx.FullName.CompareTo( dy.FullName ) );
			
			foreach(DirectoryInfo child in theDirs) {
				
				ProcessDirectory( rootLength, child, files );
			}
			
		}
		
		private String[] GetReferencedFiles() {
			
			List<String> files = new List<String>();
			
			String root = Path.GetDirectoryName( _fileName );
			
			ProcessElement( new DirectoryInfo( root ), _doc.DocumentElement, files );
			
			return files.ToArray();
		}
		
		internal static readonly String[] FileAttributes = new String[] {
			"releaseNotes",
			"src",
			"icon",
			"displayIcon",
			"path",
			"descImg",
			"previewImg"
		};
		
		private void ProcessElement(DirectoryInfo root, XmlElement element, List<String> files) {
			
			// ideally HashSet<String> should be used, but it's a 3.5 class
			
			foreach(String attributeName in FileAttributes) {
				
				if( attributeName == "path" && element.Name == "file" ) continue;
				
				String attributeValue = element.GetAttribute(attributeName);
				if( attributeValue.Length == 0 ) continue;
					
				attributeValue = attributeValue.ToLowerInvariant();
				
				if( attributeValue.StartsWith("comp:", StringComparison.OrdinalIgnoreCase) ) {
					
					// get the paths of the images
					
					try {
						
						CompositedImage img = new CompositedImage( attributeValue, root );
						foreach(Layer layer in img.Layers) {
							
							String fn = layer.ImageFileName.Substring( root.FullName.Length + 1 ).ToLowerInvariant();
							
							if( !files.Contains( fn ) ) {
								
								files.Add( fn );
							}
						}
						_compImages.Add( img );
						
					} catch(FileNotFoundException fex) {
						
						files.Add( fex.FileName );
					}
					
				} else {
					
					if( !files.Contains(attributeValue) && !attributeValue.Contains("%") ) {
						
						files.Add( attributeValue );
					}
					
				}
				
			}
			
			List<XmlNode> commentsToRemove = new List<XmlNode>();
			
			foreach(XmlNode child in element.ChildNodes) {
				
				if(child is XmlComment) {
					
					commentsToRemove.Add( child );
					
				} else {
					
					XmlElement childElement = child as XmlElement;
					if(childElement != null)
						ProcessElement( root, childElement, files );
					
				}
			}
			
			foreach(XmlNode comment in commentsToRemove) element.RemoveChild( comment );
			
		}
		
		public List<CompositedImage> CompositedImages {
			get {
				return _compImages;
			}
		}
		
		private DuplicateFinder _finder;
		
		public DuplicateFinder GetDuplicateFilesFinder() {
			
			if( _finder == null ) {
				_finder = new DuplicateFinder( new DirectoryInfo( Path.GetDirectoryName( _fileName ) ), null );
			}
			
			return _finder;
			
		}
		
		public XmlDocument Document {
			get { return _doc; }
		}
		
	}
	
	
	
	
}
