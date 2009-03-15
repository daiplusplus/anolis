using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Schema;
using System.Text;
using System.IO;

using N = System.Globalization.NumberStyles;

using ProgressEventArgs = Anolis.Core.Packages.PackageProgressEventArgs;
using System.Collections.Generic;

namespace Anolis.Core.Packages {
	
	/// <summary>Represents a collection of resource sets</summary>
	public class Package {
		
		internal Package(DirectoryInfo root, XmlElement packageElement) {
			
			RootDirectory = root;
			
			Version     = Single.Parse( packageElement.Attributes["version"].Value, N.AllowDecimalPoint | N.AllowLeadingWhite | System.Globalization.NumberStyles.AllowTrailingWhite, System.Globalization.CultureInfo.InvariantCulture );
			Attribution = packageElement.Attributes["attribution"].Value;
			Website     = new Uri( packageElement.Attributes["website"].Value );
			if(packageElement.Attributes["updateUri"] != null) UpdateUri = new Uri( packageElement.Attributes["updateUri"].Value );
			
			PackageImages = new Dictionary<String,System.Drawing.Image>();
			Log           = new Collection<LogItem>();
			
			// Load it up
			RootSet       = new Set(this, packageElement);
		}
		
		public Single Version     { get; private set; }
		public String Attribution { get; private set; }
		public Uri    Website     { get; private set; }
		public Uri    UpdateUri   { get; private set; }
		
		public DirectoryInfo RootDirectory { get; private set; }
		
		internal Dictionary<String,System.Drawing.Image> PackageImages { get; private set; }
		
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
		
		public Set                 RootSet { get; private set; }
		
		public Collection<LogItem> Log     { get; private set; }
		
		//////////////////////////////
		
		protected void OnProgressEvent(ProgressEventArgs e) {
			
			if( ProgressEvent != null ) ProgressEvent(this, e);
		}
		
		public event EventHandler<ProgressEventArgs> ProgressEvent;
		
		
		
		public void Execute() {
			
			///////////////////////////////////
			// Flatten
			
			OnProgressEvent( new PackageProgressEventArgs( 0, "Flattening Package Tree" ) );
			
			List<Operation> operations = new List<Operation>();
			
			RootSet.Flatten(operations);
			
			List<Operation> obsoleteOperations = new List<Operation>();
			
			Dictionary<String,Operation> uniques = new Dictionary<String,Operation>();
			foreach(Operation op in operations) {
				
				Operation originalOperation;
				if( uniques.TryGetValue( op.Key, out originalOperation ) ) {
					
					if( originalOperation.Merge( op ) )
						obsoleteOperations.Add( op );
					
				} else {
					
					uniques.Add( op.Key, op );
				}
				
			}
			
			operations.RemoveAll( op => obsoleteOperations.Contains( op ) );
			
			///////////////////////////////////
			// Prepare
			
			PackageUtility.AllowProtectedRenames();
			
			///////////////////////////////////
			// Install
			
			float i = 0, cnt = operations.Count;
			
			foreach(Operation op in operations) {
				
				OnProgressEvent( new PackageProgressEventArgs( (int)( 100 * i++ / cnt ), op.ToString() ) );
				
				op.Execute();
				
			}
			
			OnProgressEvent( new PackageProgressEventArgs( 100, "Complete" ) );
			
		}
		
		
	}
	
	public class LogItem {
		
		public LogItem(LogSeverity severity, String message) {
			
			Severity = severity;
			Message  = message;
		}
		
		public LogSeverity Severity { get; private set; }
		public String      Message  { get; private set; }
	}
	
	public enum LogSeverity {
		Fatal,
		Error,
		Warning,
		Info
	}
	
}
