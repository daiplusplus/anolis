using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Schema;
using System.Text;
using System.IO;
using System.Net;

using Anolis.Core.Packages.Operations;
using Anolis.Core.Utility;

using N     = System.Globalization.NumberStyles;
using Cult  = System.Globalization.CultureInfo;
using Image = System.Drawing.Image;

using ProgressEventArgs = Anolis.Core.Packages.PackageProgressEventArgs;

namespace Anolis.Core.Packages {
	
	/// <summary>Represents a collection of resource sets</summary>
	public class Package : PackageBase {
		
		public Package(DirectoryInfo root) : base(null) {
			
			CreateState();
			
			RootDirectory = root;
			RootGroup     = new Group(this, null, (String[])null);
		}
		
		internal Package(DirectoryInfo root, XmlElement packageElement) : base(null, packageElement) {
			
			CreateState();
			
			RootDirectory = root;
			
			Version       = Single.Parse( packageElement.Attributes["version"].Value, N.AllowDecimalPoint | N.AllowLeadingWhite | N.AllowTrailingWhite, Cult.InvariantCulture );
			Attribution   = packageElement.Attributes["attribution"].Value;
			Website       = packageElement.GetAttribute("website")  .Length > 0 ? new Uri( packageElement.Attributes["website"]  .Value ) : null;
			UpdateUri     = packageElement.GetAttribute("updateUri").Length > 0 ? new Uri( packageElement.Attributes["updateUri"].Value ) : null;
			ConditionDesc = packageElement.GetAttribute("conditionDesc");
			
			//////////////////////////////////////
			// Release Notes
			
			String releaseNotesPath = packageElement.GetAttribute("releaseNotes");
			
			if( !String.IsNullOrEmpty( releaseNotesPath ) ) {
				
				String actualNotesLocation = Path.Combine( RootDirectory.FullName, releaseNotesPath );
				
				if( File.Exists( actualNotesLocation ) )
					ReleaseNotes = File.ReadAllText( actualNotesLocation );
				
			}
			
			//////////////////////////////////////
			// Children
			
			// there can only be one root group element as a child, but as Whitespace is being preserved there may be other nodes in the document
			
			foreach(XmlNode node in packageElement.ChildNodes) {
				
				XmlElement rootGroupElement = node as XmlElement;
				if( rootGroupElement == null ) continue;
				
				if( RootGroup != null ) throw new PackageException("<package> element must have one (and only one) <group> child element");
				
				RootGroup = new Group(this, null, rootGroupElement);
				
			}
			
		}
		
		private void CreateState() {
			PackageImages = new Dictionary<String,Image>();
			Log           = new Log();
		}
		
		private void EnsureState() {
			
			if( RootGroup     == null ) throw new PackageException("Package's root group is not defined");
			if( RootDirectory == null ) throw new PackageException("Package's root filesystem directory is not defined");
			
		}
		
		private String ConditionDesc { get; set; }
		
		public Single Version      { get; set; }
		public String Attribution  { get; set; }
		public Uri    Website      { get; set; }
		public Uri    UpdateUri    { get; set; }
		public String ReleaseNotes { get; set; }
		
		public Group         RootGroup     { get; private set; }
		public DirectoryInfo RootDirectory { get; private set; }
		
		internal Dictionary<String,System.Drawing.Image> PackageImages { get; private set; }
		
		//////////////////////////////
		
		public static Package FromDirectory(String packageDirectory) {
			
			return FromFile( Path.Combine( packageDirectory, "Package.xml" ) );
		}
		
		public static Package FromFile(String packageXmlFileName) {
			
			Collection<ValidationEventArgs> validationMessages = new Collection<ValidationEventArgs>();
			
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.Schemas.Add( PackageSchema );
			settings.ValidationEventHandler += new ValidationEventHandler( delegate(Object sender, ValidationEventArgs ve) {
				
				validationMessages.Add( ve );
				
			});
			
			settings.Schemas.Compile();
			
			settings.ValidationType = ValidationType.Schema;
			
			XmlReader rdr = XmlReader.Create( packageXmlFileName, settings );
			XmlDocument doc = new XmlDocument();
			doc.PreserveWhitespace = true;
			
			doc.Load( rdr );
			
			doc.Validate( new ValidationEventHandler( delegate(Object sender, ValidationEventArgs ve) {
				
				validationMessages.Add( ve );
				
			}) );
			
			if( validationMessages.Count > 0 ) {
				
				throw new PackageValidationException("Errors were thrown during validation of the Package XML File", validationMessages);
				
			}
			
			// walk the document
			
			XmlElement packageElement = doc.DocumentElement;
			
			return new Package( new DirectoryInfo( Path.GetDirectoryName(packageXmlFileName) ), packageElement );
			
			
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
		
		public Log Log { get; private set; }
		
		public PackageExecutionSettingsInfo ExecutionInfo { get; private set; }
		
		public Boolean IsBusy { get; private set; }
		
		//////////////////////////////
		
		protected void OnProgressEvent(ProgressEventArgs e) {
			
			if( ProgressEvent != null ) ProgressEvent(this, e);
		}
		
		public event EventHandler<ProgressEventArgs> ProgressEvent;
		
#region Execute
		
		public EvaluationInfo EvaluateCondition() {
			
			Dictionary<String,Double> symbols = BuildSymbols();
			
			EvaluationResult result = Evaluate( symbols );
			
			EvaluationInfo ret = new EvaluationInfo( result, result == EvaluationResult.False ? ConditionDesc : String.Empty );
			
			return ret;
		}
		
		public void Execute(PackageExecutionSettings settings) {
			
			///////////////////////////////////
			// Prepare
			
			EnsureState();
			
			if( IsBusy ) throw new InvalidOperationException("Currently executing a package.");
			
			Group backupGroup = null;
			if( settings.BackupDirectory != null ) {
				
				Package backupPackage = new Package( settings.BackupDirectory );
				backupPackage.Name = "Uninstallation Package";
				
				backupGroup = backupPackage.RootGroup;
			}
			
			ExecutionInfo = new PackageExecutionSettingsInfo(this, settings.ExecutionMode, settings.CreateSystemRestorePoint, backupGroup, settings.I386Directory );
			
			IsBusy = true;
			
			///////////////////////////////////
			// Flatten
			
			Log.Add( LogSeverity.Info, "Beginning package execution: " + this.RootGroup.Name );
			
			OnProgressEvent( new PackageProgressEventArgs( 0, "Flattening Package Tree" ) );
			
			List<Operation> operations = new List<Operation>();
			
			RootGroup.Flatten(operations);
			
			List<Operation> obsoleteOperations = new List<Operation>();
			
			Dictionary<String,Operation> uniques = new Dictionary<String,Operation>();
			foreach(Operation op in operations) {
				
				if( !op.Enabled ) {
					obsoleteOperations.Add( op );
					continue;
				}
				
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
			
			if( ExecutionInfo.ExecutionMode == PackageExecutionMode.Regular )
				PackageUtility.AllowProtectedRenames();
			
			///////////////////////////////////
			// System Restore, Part 1
			if( ExecutionInfo.ExecutionMode == PackageExecutionMode.Regular && ExecutionInfo.CreateSystemRestorePoint ) {
				
				OnProgressEvent( new PackageProgressEventArgs( -1, "Creating System Restore Point" ) );
				
				String pointName = "Installed Anolis Package \"" + this.RootGroup.Name + '"';
				
				PackageUtility.CreateSystemRestorePoint( pointName, PackageUtility.SystemRestoreType.ApplicationInstall, PackageUtility.SystemRestoreEventType.BeginSystemChange );
			}
			
			///////////////////////////////////
			// Install (Backup and Execute; backups are the responisiblity of each Operation)
			
			try {
				
				float i = 0, cnt = operations.Count;
				
				foreach(Operation op in operations) {
					
					OnProgressEvent( new PackageProgressEventArgs( (int)( 100 * i++ / cnt ), op.ToString() ) );
					
					if( !op.SupportsI386 && ExecutionInfo.ExecutionMode == PackageExecutionMode.I386 ) continue;
					
					try {
						
						op.Execute();
						
					} catch(Exception ex) {
						
						Log.Add( new LogItem( ex, op ) );
						continue;
					}
					
					Log.Add( LogSeverity.Info, "Done " + op.Name + ": " + op.Path );
					
				}//foreach
				
				PackageUtility.ClearIconCache();
				
				OnProgressEvent( new PackageProgressEventArgs( 100, "Complete" ) );
				
			} finally {
				
				///////////////////////////////////
				// System Restore, Part 2
				if( ExecutionInfo.ExecutionMode == PackageExecutionMode.Regular && ExecutionInfo.CreateSystemRestorePoint ) {
					
					OnProgressEvent( new PackageProgressEventArgs( -1, "Finishing System Restore Point" ) );
					
					String pointName = "Installed Anolis Package \"" + this.RootGroup.Name + '"';
					
					PackageUtility.CreateSystemRestorePoint( pointName, PackageUtility.SystemRestoreType.ApplicationInstall, PackageUtility.SystemRestoreEventType.EndSystemChange );
				}
				
				///////////////////////////////////
				// Backup, Part 2
				
				if( ExecutionInfo.BackupGroup != null ) {
					
					String backupFileName = Path.Combine( ExecutionInfo.BackupDirectory.FullName, "Package.xml" );
					
					ExecutionInfo.BackupPackage.Write( backupFileName );
					
				}
				
				///////////////////////////////////
				// Dump the log to disk
				
				if( Log.CountNonNominal > 0 )
					Log.Save( Path.Combine( this.RootDirectory.FullName, "Anolis.Installer.log" ) );
				
				IsBusy = false;
				
			}//try/finally
			
		}
		
#endregion
		
		
		/// <summary>A blocking method that returns details of the latest version, if it exists. Returns null under all failure conditions.</summary>
		public PackageUpdateInfo CheckForUpdates() {
			
			if( this.UpdateUri == null ) return null;
			
			WebClient client = new WebClient();
			
			String data;
			
			try {
				
				data = client.DownloadString( UpdateUri );
				
			} catch(WebException) {
				return null;
			}
			
			String[] lines = data.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
			
			// PackageUpdateInfo format specification:
			// line 1: package name, used for info purposes
			// line 2: version of the new package
			// line 3: URI of the package to download, can be zero-length
			// line 4: URI of a web page with information on the new package, such as how to get it if line 3 is empty
			// line 5 onwards: reserved and ignored
			
			if( lines.Length < 4 ) return null;
			
			String name = lines[0];
			Single version; Uri packageLocation, infoLocation;
			
			if( !Single.TryParse ( lines[1], out version ) ) return null;
			if( !Uri   .TryCreate( lines[2], UriKind.Absolute, out packageLocation ) ) return null;
			if( !Uri   .TryCreate( lines[3], UriKind.Absolute, out infoLocation    ) ) return null;
			
			PackageUpdateInfo info = new PackageUpdateInfo( name, version, packageLocation, infoLocation );
			return info;
			
		}
		
		public void Write(String fileName) {
			
			EnsureState();
			
			using(FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None)) {
				
				Write( fs );
			}
			
		}
		
		public void Write(Stream stream) {
			
			EnsureState();
			
			XmlDocument doc = new XmlDocument();
			XmlElement root = doc.CreateElement("package");
			
			                        PackageItem.AddAttribute(root, "id"         , Id );
			                        PackageItem.AddAttribute(root, "name"       , Name );
			if( Condition != null ) PackageItem.AddAttribute(root, "condition"  , Condition.ToString() );
			                        PackageItem.AddAttribute(root, "version"    , Version.ToString(Cult.InvariantCulture) );
			                        PackageItem.AddAttribute(root, "attribution", Attribution );
			if( Website   != null ) PackageItem.AddAttribute(root, "website"    , Website.ToString() );
			if( UpdateUri != null ) PackageItem.AddAttribute(root, "updateUri"  , UpdateUri.ToString());
			
			doc.AppendChild(root);
			
			RootGroup.Write( root );
			
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.ConformanceLevel = ConformanceLevel.Document;
			settings.Encoding         = Encoding.UTF8;
			settings.Indent           = true;
			settings.IndentChars      = "\t";
			
			XmlWriter wtr = XmlWriter.Create(stream, settings);
			
			doc.WriteTo( wtr );
			
			wtr.Flush();
			
		}
		
	}
	
	public class PackageUpdateInfo {
		
		public PackageUpdateInfo(String name, Single version, Uri packageLocation, Uri infoLocation) {
			
			Name                = name;
			Version             = version;
			PackageLocation     = packageLocation;
			InformationLocation = infoLocation;
		}
		
		public String  Name                { get; private set; }
		public Single  Version             { get; private set; }
		public Uri     PackageLocation     { get; private set; }
		public Uri     InformationLocation { get; private set; }
		
	}
	
	public class PackageExecutionSettings {
		
		public PackageExecutionMode ExecutionMode            { get; set; }
		
		public DirectoryInfo        BackupDirectory          { get; set; }
		
		public Boolean              CreateSystemRestorePoint { get; set; }
		
		public DirectoryInfo        I386Directory            { get; set; }
		
	}
	
	/// <summary>A read-only version of PackageExecutionSettings for passing to operations and consumers of Package</summary>
	public class PackageExecutionSettingsInfo {
		
		internal PackageExecutionSettingsInfo(Package package, PackageExecutionMode mode, Boolean createSysRes, Group backupGroup, DirectoryInfo i386Directory) {
			
			ExecutionMode            = mode;
			CreateSystemRestorePoint = createSysRes;
			BackupGroup              = backupGroup;
			
			if( i386Directory != null ) {
				
				I386Directory = i386Directory;
				_i386Files    = I386Directory.GetFiles();
				
				Array.Sort( _i386Files, (x,y) => String.Compare( x.Name, y.Name, StringComparison.OrdinalIgnoreCase ) );
			}
		}
		
		public Package Package { get; private set; }
		
		public PackageExecutionMode ExecutionMode { get; private set; }
		
		public Boolean CreateSystemRestorePoint   { get; private set; }
		
		public Group BackupGroup { get; private set; }
		
		public Package BackupPackage {
			get { return BackupGroup == null ? null : BackupGroup.Package; }
		}
		
		public DirectoryInfo BackupDirectory {
			get { return BackupGroup == null ? null : BackupPackage.RootDirectory; }
		}
		
		public Boolean MakeBackup {
			get { return BackupGroup != null; }
		}
		
#region I386
		
		private static readonly FileSearchComparer _comp = new FileSearchComparer();
		
		private FileInfo[] _i386Files;
		
		public DirectoryInfo I386Directory { get; private set; }
		
		public FileInfo I386FindFile(String fileName) {
			
			Int32 idx = Array.BinarySearch( _i386Files, fileName, _comp );
			
			if( idx < 0 ) return null;
			
			return _i386Files[idx];
			
		}
		
		private class FileSearchComparer : IComparer<Object> {
			
			public Int32 Compare(object x, object y) {
				
				FileInfo fx = x as FileInfo;
				String   sy = y as String;
				
				return String.Compare( fx.Name, sy, StringComparison.OrdinalIgnoreCase );
				
			}
		}
		
#endregion
		
	}
	
	public enum PackageExecutionMode {
		Regular,
		I386
	}
	
	public class EvaluationInfo {
		
		public EvaluationInfo(EvaluationResult result, String message) {
			Result  = result;
			Message = message;
		}
		
		public EvaluationResult Result {
			get; private set;
		}
		
		public String Message {
			get; private set;
		}
		
		public Boolean Success {
			get { return Result == EvaluationResult.True; }
		}
		
	}
	
}
