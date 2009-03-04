using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Xml;
using System.Xml.Schema;
using System.Text;
using System.IO;

using ProgressEventArgs = Anolis.Core.Packages.PackageProgressEventArgs;

namespace Anolis.Core.Packages {
	
	public abstract class PackageItem {
		
		internal PackageItem(String rootDir, XmlElement itemElement) {
			
			Id          = itemElement.GetAttribute("id");
			Name        = itemElement.GetAttribute("name");
			Description = itemElement.GetAttribute("desc");
			
			String descImg = itemElement.GetAttribute("descImg");
			if(descImg.Length > 0) {
				DescriptionImage = Image.FromFile( Path.Combine( rootDir, descImg ) );
			}
			
			String enabled = itemElement.GetAttribute("enabled");
			if(enabled.Length > 0) {
				Enabled = enabled != "false" && enabled != "0"; // with xs:boolean both "false" and "0" are valid values
			} else {
				Enabled = true;
			}
			
			
		}
		
		protected PackageItem(Package package, XmlElement itemElement) : this(package.RootDirectory.FullName, itemElement) {
		}
		
		public String  Id               { get; protected set; }
		public String  Name             { get; protected set; }
		public String  Description      { get; protected set; }
		public Image   DescriptionImage { get; protected set; }
		public Boolean Enabled          { get; set; }
		
	}
	
	/// <summary>Represents a collection of resource sets</summary>
	public class Package : PackageItem {
		
		internal Package(DirectoryInfo root, XmlElement packageElement) : base(root.FullName, packageElement) {
			
			RootDirectory = root;
			Children    = new SetCollection();
			
			Version     = Single.Parse( packageElement.Attributes["version"].Value );
			Attribution = packageElement.Attributes["attribution"].Value;
			Website     = new Uri( packageElement.Attributes["website"].Value );
			if(packageElement.Attributes["updateUri"] != null) UpdateUri = new Uri( packageElement.Attributes["updateUri"].Value );
			
			foreach(XmlElement setElement in packageElement.ChildNodes) {
				
				Set s = new Set(this, setElement );
				Children.Add( s );
				
			}
			
		}
		
		public Single Version     { get; private set; }
		public String Attribution { get; private set; }
		public Uri    Website     { get; private set; }
		public Uri    UpdateUri   { get; private set; }
		
		public SetCollection Children { get; private set; }
		
		public DirectoryInfo RootDirectory { get; private set; }
		
		//////////////////////////////
		
		public static Package FromDirectory(String packageDirectory) {
			
			return FromFile( Path.Combine( packageDirectory, "Package.xml" ) );
		}
		
		public static Package FromFile(String packageXmlFilename) {
			
			Collection<ValidationEventArgs> validationMessages = new Collection<ValidationEventArgs>();
			
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.Schemas.Add( PackageSchema );
			settings.ValidationEventHandler += new ValidationEventHandler( delegate(Object sender, ValidationEventArgs ve) {
				
				validationMessages.Add( ve );
				
			});
			
			settings.Schemas.Compile();
			
			settings.ValidationType = ValidationType.Schema;
			
			XmlReader rdr = XmlReader.Create( packageXmlFilename, settings );
			XmlDocument doc = new XmlDocument();
			
			doc.Load( rdr );
			
			doc.Validate( new ValidationEventHandler( delegate(Object sender, ValidationEventArgs ve) {
				
				validationMessages.Add( ve );
				
			}) );
			
			if( validationMessages.Count > 0 ) {
				
				throw new PackageValidationException("Errors were thrown during validation of the Package XML File", validationMessages);
				
			}
			
			// walk the document
			
			XmlElement packageElement = doc.DocumentElement;
			
			return new Package( new DirectoryInfo( Path.GetDirectoryName(packageXmlFilename) ), packageElement );
			
			
		}
		
		private static XmlSchema _packageSchema;
		
		public static XmlSchema PackageSchema {
			get {
				if(_packageSchema == null) {
					
					using(MemoryStream ms = new MemoryStream( Resources.PackageSchema )) {
						
						_packageSchema = XmlSchema.Read( ms, new ValidationEventHandler( delegate(Object sender, ValidationEventArgs ve) {
							
							throw new XmlSchemaException("Package Schema is invalid", ve.Exception );
							
						}) );
						
					}
				}
				return _packageSchema;
			}
		}
		
		//////////////////////////////
		
		/// <summary>Ensures all the source files referenced by the Package exist in the filesystem and any other checks</summary>
		/// <remarks>This method does not validate the package XML. That is done when the package is instantiated</remarks>
		public void Validate() {
			
			
			
		}
		
		
		/// <summary>Executes the operations contained within this package</summary>
		public void Execute() {
			
			foreach(Set set in Children) {
				
				set.Execute();
			}
			
		}
		
		protected void OnProgressEvent(ProgressEventArgs e) {
			
			if( ProgressEvent != null ) ProgressEvent(this, e);
		}
		
		public event EventHandler<ProgressEventArgs> ProgressEvent;
		
	}
	
	/// <summary>An arbitrary grouping of elements</summary>
	public class Set : PackageItem {
		
		public Set(Package package, XmlElement setElement) : base(package, setElement) {
			
			Children = new SetCollection();
			Operations = new OperationCollection();
			Mutex    = new SetCollection();
			// set Mutex members after all the siblings have been read in
			
			foreach(XmlElement e in setElement.ChildNodes) {
				
				switch(e.Name) {
					case "set":
						
						Set set = new Set(package, e );
						Children.Add( set );
						
						break;
					case "patch":
						
						PatchOperation patch = new PatchOperation(package, e );
						Operations.Add( patch );
						
						break;
						
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
		
		public SetCollection       Children   { get; private set; }
		public OperationCollection Operations { get; private set; }
		
		internal void Execute() {
			
			foreach(Set       set in Children)  set.Execute();
			
			foreach(Operation op  in Operations) op.Execute();
			
		}
		
	}
	
	public class SetCollection : Collection<Set> {
	}
	
	[Serializable]
	public class PackageException : AnolisException {
		
		public PackageException() {
		}
		
		public PackageException(String message) : base(message) {
		}
		
		public PackageException(String message, Exception inner) : base(message, inner) {
		}
		
		protected PackageException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) {
		}
		
	}
	
	[Serializable]
	public class PackageValidationException : PackageException {
		
		public PackageValidationException(String message, Collection<ValidationEventArgs> errors) : base(message) {
			
			ValidationErrors = errors;
		}
		
		protected PackageValidationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) {
		}
		
		public Collection<ValidationEventArgs> ValidationErrors {
			get; private set;
		}
		
	}
	
}
