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
				Children.Add( s );
				
			}
			
		}
		
		public Single Version     { get; private set; }
		public String Attribution { get; private set; }
		public Uri    Website     { get; private set; }
		public Uri    UpdateUri   { get; private set; }
		
		public SetCollection Children { get; private set; }
		
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
			
			return new Package( packageElement );
			
			
		}
		
		public static Package FromFile(String packageXmlFilename, Object useObsolete) {
			
			using(FileStream fs = new FileStream(packageXmlFilename, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				
				XmlValidatingReader vrdr = new XmlValidatingReader( new XmlTextReader( packageXmlFilename ) );
				vrdr.ValidationType = ValidationType.Schema;
				vrdr.Schemas.Add( PackageSchema );
				
				Collection<ValidationEventArgs> validationMessages = new Collection<ValidationEventArgs>();
				
				vrdr.ValidationEventHandler += new ValidationEventHandler(delegate(Object sender, ValidationEventArgs ve) {
					
					validationMessages.Add( ve );
					
				});
				
				XmlDocument doc = new XmlDocument();
				doc.Load( vrdr );
				
				XmlElement packageElement = doc.DocumentElement;
				
				return new Package( packageElement );
				
			}
			
			
		}
		
		private static XmlSchema _packageSchema;
		
		public static XmlSchema PackageSchema {
			get {
				if(_packageSchema == null) {
					
					using(FileStream fs = new FileStream( @"D:\Users\David\My Documents\Visual Studio Projects\Anolis\Anolis.Core\Packages\Xml\PackageSchema.xsd", FileMode.Open)) {
						
						_packageSchema = XmlSchema.Read( fs, null );
						
					}
					
					_packageSchema.Compile( new ValidationEventHandler( delegate(Object sender, ValidationEventArgs ve) {
						
						throw new XmlSchemaException("Package Schema is invalid", ve.Exception );
						
					}));
					
//					using(MemoryStream ms = new MemoryStream( Resources.PackageSchema )) {
//						
//						_packageSchema = XmlSchema.Read( ms, null );
//						
//						_packageSchema = XmlSchema.Read( ms, new ValidationEventHandler( delegate(Object sender, ValidationEventArgs ve) {
//							
//							throw new XmlSchemaException("Package Schema is invalid", ve.Exception );
//							
//						}) );
//						
//					}
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
