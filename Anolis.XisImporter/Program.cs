using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Anolis.XisImporter {
	
	public class Program {
		
		public static void Main(String[] args) {
			
			// args[0] is the nsi script containing details on each file
			// args[1] is the folder containing the files
			
			Section[] sections = NsiReader.ReadFile( @"D:\Users\David\My Documents\Visual Studio Projects\Anolis\_resources\xpize\_nsiScripts\InstallerSystemFilesFull.nsi" );
			
		}
		
		private static void ProcessFileDirectory(DirectoryInfo dir) {
			
			// open the file's reshacker script
			
			
			
		}
		
	}
	
	public class Section {
		
		public String Name;
		public String Id;
		
		public List<String> Commands;
		public List<Section> Children;
		
		public Section Parent;
		
		public Section() {
			Commands = new List<string>();
			Children = new List<Section>();
		}
		
		public override string ToString() {
			return "Section: " + Name + " - " + Id;
		}
	}
	
	public class SectionGroup : Section {
		
		public override string ToString() {
			return "SectionGroup: " + Name + " - " + Id;
		}
	}
	
	public static class NsiReader {
		
		public static Section[] ReadFile(String filename) {
			
			FileStream stream = File.Open(filename, FileMode.Open);
			
			StreamReader rdr = new StreamReader(stream);
			
			List<Section> sections = new List<Section>();
			
			Section currentSec = null;
			
			Boolean inMultilineComment = false;
			
			String line;
			while( (line = rdr.ReadLine()) != null ) {
				
				line = line.Trim();
				
				if( line.StartsWith("/*") )
					inMultilineComment = true;
				if( line.EndsWith("*/") ) {
					inMultilineComment = false;
					continue; // don't want to parse this line as it's commented out
				}
				
				if( inMultilineComment ) continue;
				
				if( line.StartsWith("#") ) continue; // it's a comment-line
				if( line.Length < 2 ) continue; // empty line
				
				if( line.StartsWith("SectionGroup ") ) {
					
					SectionGroup g = new SectionGroup();
					g.Name = line.Substring( line.IndexOf('"'), line.LastIndexOf('"') - line.IndexOf('"') );
					g.Id   = line.Substring( line.LastIndexOf('"') + 1 );
					
					if(currentSec == null) {
						currentSec = g;
						sections.Add( g );
						
					} else {
						(currentSec as SectionGroup).Children.Add( g );
						g.Parent = currentSec;
						currentSec = g;
					}
					
				} else 
				
				if( line.StartsWith("SectionGroupEnd") ) {
					
					currentSec = currentSec.Parent;
					
				} else 
				
				if( line.StartsWith("Section ") ) {
					
					Section s = new Section();
					s.Name = line.Substring( line.IndexOf('"'), line.LastIndexOf('"') - line.IndexOf('"') );
					s.Id   = line.Substring( line.LastIndexOf('"') + 1 );
					
					(currentSec as SectionGroup).Children.Add( s );
					s.Parent = currentSec;
					currentSec = s;
					
				} else
				
				if( line.StartsWith("SectionEnd") ) {
					
					currentSec = currentSec.Parent;
					
				} else {
					
					currentSec.Commands.Add( line );
					
				}
				
			}
			
			return sections.ToArray();
			
		}
		
	}
	
	
}
