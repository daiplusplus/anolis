#define BLACKBOX

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Anolis.Packager.Importer {
	
	public static class XisImporter {
		
		private static String _nsiFilesDirectory;
		
		public static void Convert(String nsiFilePath, String nsiFilesDirectory, String outputPath) {
			
			_nsiFilesDirectory = nsiFilesDirectory;
			
			lock(_nsiFilesDirectory) {
				
				// args[0] is the nsi script containing details on each file
				// args[1] is the folder containing the files
				
				Section[] sections = NsiReader.ReadFile( nsiFilePath );
				
				foreach(Section s in sections) {
					
					ProcessSection( s );
				}
				
				// now... write out to Xml
				
				XmlDocument doc = new XmlDocument();
				
				XmlElement root = doc.CreateElement("package");
				root.SetAttribute("name", "xpize");
				
				doc.AppendChild( root );
				
				foreach(Section s in sections) {
					
					XmlProcessSection(doc, root, s);
					
				}
				
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.IndentChars = "\t";
				settings.Indent = true;
				settings.NewLineHandling = NewLineHandling.Replace;
				
				XmlWriter wtr = XmlWriter.Create( outputPath, settings );
				
				doc.Save( wtr );
				
			}
			
		}
		
		private static void XmlProcessSection(XmlDocument doc, XmlElement parent, Section s) {
			
			
			
			if(s.Children.Count == 0 && s.FilesToModify.Count > 0) {
				
				foreach( ModifyFile f in s.FilesToModify) {
					
					XmlElement patch = doc.CreateElement("patch");
					patch.SetAttribute("filename", f.CompleteFilename); 
					
					foreach(Operation op in f.Operations) {
						
						XmlElement res = doc.CreateElement("res");
						res.SetAttribute("type", op.ResourceType);
						res.SetAttribute("name", op.ResourceName);
						if(op.ResourceLang != null && op.ResourceLang.Length > 0) res.SetAttribute("lang", op.ResourceLang);
						res.SetAttribute("src", op.Filename);
						
						patch.AppendChild( res );
						
					}
					
					parent.AppendChild( patch );
					
				}
				
				
			} else {
				
				XmlElement set = doc.CreateElement("set");
				set.SetAttribute("id", s.Id);
				parent.AppendChild( set );
				
				foreach(Section c in s.Children) {
					
					XmlProcessSection(doc, set, c);
					
				}
				
			}
			
		}
		
		private static void ProcessSection(Section s) {
			
			foreach(Section child in s.Children) {
				
				ProcessSection(child);
				
			}
			
			foreach(String command in s.Commands) {
				
				if( command.StartsWith("!insertmacro InstallFile") ) {
					
					String[] parts = SplitIntoPartsTakingStringsIntoAccount(command, ' ');
					if(parts.Length != 5) throw new Exception("invalid number of parts");
					
					ModifyFile f = new ModifyFile();
					f.Filename = parts[3].Trim('"');
					f.FileLocation = parts[4].Trim('"');
					
					f.CompleteFilename = GetCompleteFilename( f.FileLocation, f.Filename );
					
					String dirContainingResHackerBatchStr = GetDirContainingResHackerBatch( f.CompleteFilename );
					
					DirectoryInfo dirContainingResHackerBatch = new DirectoryInfo( dirContainingResHackerBatchStr );
					
					ProcessFile( dirContainingResHackerBatch, f );
					
					s.FilesToModify.Add( f );
					
				}
				
			}
			
		}
		
		private static String[] SplitIntoPartsTakingStringsIntoAccount(String s, Char splitOn) {
			
			List<String> ses = new List<String>();
			
			Boolean inString = false;
			
			String current = "";
			
			for(int i=0;i<s.Length;i++) {
				
				if( s[i] == '"' ) inString = !inString;
				
				if( s[i] == splitOn && !inString ) {
					ses.Add( current );
					current = "";
				} else {
					
					current += s[i];
				}
				
				if( i == s.Length - 1 ) ses.Add( current );
				
			}
			
			return ses.ToArray();
			
		}
		
		private static String GetCompleteFilename(String location, String filename) {
			
			if( location.StartsWith("$WINDIR", StringComparison.OrdinalIgnoreCase) )
				return Path.Combine( @"%windir%" + location.Substring( 7 ), filename );
			
			if( location.StartsWith("$SYSDIR", StringComparison.OrdinalIgnoreCase) )
				return Path.Combine( @"%windir%\system32" + location.Substring( 7 ), filename );
			
			if( location.StartsWith("$PROGRAMFILES", StringComparison.OrdinalIgnoreCase) )
				return Path.Combine( @"%ProgramFiles%" + location.Substring( 13 ), filename );
				
			if( location.StartsWith("$COMMONFILES", StringComparison.OrdinalIgnoreCase) )
				return Path.Combine( @"%CommonProgramFiles%" + location.Substring( 12 ), filename );
			
			return Path.Combine( location, filename );
			
		}
		
		private static String GetDirContainingResHackerBatch(String completeFilename) {
			
			String retval = null;
			
			if( completeFilename.StartsWith(@"%windir%\system32") ) {
				
				String fn = completeFilename.Substring( 18 );
				if(fn.StartsWith("\\")) fn = fn.Substring(1);
				
				retval = Path.Combine( _nsiFilesDirectory + @"Windows\system32", fn );
				
			} else if( completeFilename.StartsWith(@"%windir%") ) {
				
				String fn = completeFilename.Substring( 8 );
				if(fn.StartsWith("\\")) fn = fn.Substring(1);
				
				retval = Path.Combine( _nsiFilesDirectory + @"Windows", fn );
				
			} else if( completeFilename.StartsWith(@"%ProgramFiles%") ) {
				
				String fn = completeFilename.Substring( 15 );
				if(fn.StartsWith("\\")) fn = fn.Substring(1);
				
				retval = Path.Combine( _nsiFilesDirectory + @"Program Files", fn );
				
			} else if( completeFilename.StartsWith(@"%CommonProgramFiles%") ) {
				
				String fn = completeFilename.Substring( 20 );
				if(fn.StartsWith("\\")) fn = fn.Substring(1);
				
				retval = Path.Combine( _nsiFilesDirectory + @"Program Files\Common Files", fn );
			
			}
			
			return retval;
			
		}
		
		private static void ProcessFile(DirectoryInfo folder, ModifyFile f) {
			
			// open the file's reshacker script
			
			String fn = Path.Combine( folder.FullName, folder.Name + ".txt" );
			
			if( !File.Exists( fn ) ) throw new FileNotFoundException("", fn);
			
			StreamReader rdr = new StreamReader( fn );
			String line;
			while( (line = rdr.ReadLine()) != null ) {
				
				line = line.Trim();
				
				if( !line.StartsWith("-") ) continue;
				
				// insert a comma after the command
				line = line.Substring(0, line.IndexOf(' ')) + ", " + line.Substring( line.IndexOf(' ') );
				
				String[] parts = line.Split(',');
				if(parts.Length != 5 && parts.Length != 4) throw new Exception("invalid number of parts");
				
				String currentDirRelativeToLoc = folder.FullName.Replace( _nsiFilesDirectory, "" );
				
				Operation o = new Operation();
				o.Op               = parts[0].Trim('"', ' ');
				o.Filename         = currentDirRelativeToLoc + parts[1].Trim('"', ' ').Replace("Resources\\" + folder.Name, "");
				o.ResourceType     = parts[2].Trim('"', ' ');
				if(parts.Length >= 4) o.ResourceName = parts[3].Trim('"', ' ');
				if(parts.Length == 5) o.ResourceLang = parts[4].Trim('"', ' ');
				
				f.Operations.Add( o );
			}
			
		}
		
	}
	
	public class ModifyFile {
		
		public String Filename;
		public String FileLocation;
		
		public String CompleteFilename;
		
		public List<Operation> Operations = new List<Operation>();
		
	}
	
	public class Operation {
		
		public String Op;
		
		public String Filename;
		
		public String ResourceType;
		public String ResourceName;
		public String ResourceLang;
		
	}
	
	public class Section {
		
		public String Name;
		public String Id;
		
		public List<String> Commands = new List<string>();
		public List<Section> Children = new List<Section>();
		
		public List<ModifyFile> FilesToModify = new List<ModifyFile>();
		
		public Section Parent;
		
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
					g.Name = line.Substring( line.IndexOf('"') + 1, line.LastIndexOf('"') - line.IndexOf('"') - 1 );
					g.Id   = line.Substring( line.LastIndexOf('"') + 2 );
					
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
					s.Name = line.Substring( line.IndexOf('"') + 1, line.LastIndexOf('"') - line.IndexOf('"') - 1 );
					s.Id   = line.Substring( line.LastIndexOf('"') + 2 );
					
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
