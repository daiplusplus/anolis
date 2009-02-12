using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Xml;
using System.Xml.Schema;
using System.Text;
using System.IO;

namespace Anolis.Core.Packages {
	
	public abstract class PackageItem {
		
		protected PackageItem(XmlElement itemElement) {
			
			Id          = itemElement.Attributes["id"].Value;
			Name        = itemElement.Attributes["name"].Value;
			Description = itemElement.Attributes["desc"].Value;
			
		}
		
		public Boolean Enabled     { get; set; }
		public String  Id          { get; protected set; }
		public String  Name        { get; protected set; }
		public String  Description { get; protected set; }
		
	}
	
	/// <summary>Represents a collection of resource sets</summary>
	public class Package : PackageItem {
		
		internal Package(XmlElement packageElement) : base(packageElement) {
			
			Version     = Single.Parse( packageElement.Attributes["version"].Value );
			Attribution = packageElement.Attributes["attribution"].Value;
			Website     = new Uri( packageElement.Attributes["website"].Value );
			UpdateUri   = new Uri( packageElement.Attributes["updateUri"].Value );
			
			foreach(XmlElement setElement in packageElement.ChildNodes) {
				
				Set s = new Set( setElement );
				Sets.Add( s );
				
			}
			
		}
		
		public Single Version     { get; private set; }
		public String Attribution { get; private set; }
		public Uri    Website     { get; private set; }
		public Uri    UpdateUri  { get; private set; }
		
		public SetCollection Sets { get; private set; }
		
	}
	
	/// <summary>An arbitrary grouping of elements</summary>
	public class Set : PackageItem {
		
		public Set(XmlElement setElement) : base(setElement) {
			
			Mutex = new SetCollection();
			// set Mutex members after all the siblings have been read in
			
			foreach(XmlElement e in setElement.ChildNodes) {
				
				switch(e.Name) {
					case "set":
						
						Set set = new Set( e );
						Children.Add( set );
						
						break;
					case "patch":
					case "file":
					case "filetype":
						break;
				}
				
			}
			
			String mutex = setElement.Attributes["mutex"] != null ? setElement.Attributes["mutex"].Value : String.Empty;
			if(mutex.Length > 0) {
				
				MutexIds = mutex.Split(' ');
				
			}
			
		}
		
		internal String[] MutexIds    { get; private set; }
		
		public SetCollection Mutex    { get; private set; }
		
		public SetCollection Children { get; private set; }
		
	}
	
	public class File {
		
		/// <summary>Gets the Path to the file as specified in the Package definition file.</summary>
		public String Path { get; private set; }
		
		/// <summary>Gets the actual, working, path to the file (if it exists).</summary>
		public String ResolvedPath { get; private set; }
		
		//public FileCondition Condition { get; private set; }
		
	}
	
	public class SetCollection : Collection<Set> {
	}
	
	internal static class PackageReader {
		
		public static Package ReadPackage(String filename) {
			
			XmlDocument doc = new XmlDocument();
			doc.Load( filename );
			
			doc.Validate( new ValidationEventHandler( Validation_Event ) );
			
			// walk the document
			
			XmlElement packageElement = doc.DocumentElement;
			
			return new Package( packageElement );
			
		}
		
		private static void Validation_Event(Object sender, ValidationEventArgs e) {
			
			
			
		}
		
	}
	
}
