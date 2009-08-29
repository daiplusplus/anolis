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
			RootGroup     = new Group(this, null);
		}
		
		internal Package(DirectoryInfo root, XmlElement packageElement) : base(null, packageElement) {
			
			CreateState();
			
			RootDirectory = root;
			
			Version       = new Version( packageElement.Attributes["version"].Value );
			Attribution   = packageElement.GetAttribute("attribution");
			Website       = packageElement.GetAttribute("website")    .Length > 0 ? new Uri( packageElement.Attributes["website"]    .Value ) : null;
			UpdateUri     = packageElement.GetAttribute("updateUri")  .Length > 0 ? new Uri( packageElement.Attributes["updateUri"]  .Value ) : null;
			FeedbackUri   = packageElement.GetAttribute("feedbackUri").Length > 0 ? new Uri( packageElement.Attributes["feedbackUri"].Value ) : null;
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
			
			// there can only be one root <group> element as a child, but as Whitespace is being preserved there may be other nodes in the document
			// then there's alsot he <presets> element
			
			XmlElement presetsElement = null;
			
			foreach(XmlNode node in packageElement.ChildNodes) {
				
				XmlElement rootElement = node as XmlElement;
				if( rootElement == null ) continue;
				
				if( rootElement.Name == "presets" ) {
					
					if( presetsElement != null ) throw new PackageValidationException("<package> element can have at most one <presets> child element");
					presetsElement = rootElement;
					
				} else if( rootElement.Name == "group" ) {
					
					if( RootGroup != null ) throw new PackageValidationException("<package> element must have one (and only one) <group> child element");
					RootGroup = new Group(this, null, rootElement);
					
				}	
			}
			
			if( presetsElement != null ) {
				BuildPresets( presetsElement );
			}
			
		}
		
		private void CreateState() {
			PackageImages = new Dictionary<String,Image>();
			Log           = new Log();
			Presets       = new PresetCollection();
		}
		
		private void EnsureState() {
			
			if( Version       == null ) throw new PackageException("Package's version is not defined");
			if( RootGroup     == null ) throw new PackageException("Package's root group is not defined");
			if( RootDirectory == null ) throw new PackageException("Package's root filesystem directory is not defined");
			
		}
		
		public String ConditionDesc { get; set; }
		
		public Version Version      { get; set; }
		public String  Attribution  { get; set; }
		public Uri     Website      { get; set; }
		public Uri     UpdateUri    { get; set; }
		public Uri     FeedbackUri  { get; set; }
		public String  ReleaseNotes { get; set; }
		
		public Group         RootGroup     { get; private set; }
		public DirectoryInfo RootDirectory { get; private set; }
		
		public PresetCollection Presets    { get; private set; }
		
		internal Dictionary<String,System.Drawing.Image> PackageImages { get; private set; }
		
		//////////////////////////////
		
		public static Package FromFile(String packageXmlFileName) {
			
			Collection<ValidationEventArgs> validationMessages = new Collection<ValidationEventArgs>();
			
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.Schemas.Add( PackageSchema );
			settings.ValidationEventHandler += new ValidationEventHandler( delegate(Object sender, ValidationEventArgs ve) {
				
				validationMessages.Add( ve );
				
			});
			
			settings.Schemas.Compile();
			
			settings.ValidationType = ValidationType.Schema;
			
			XmlDocument doc = new XmlDocument();
			doc.PreserveWhitespace = true;
			
			using(XmlReader rdr = XmlReader.Create( packageXmlFileName, settings )) {
				
				try {
					
					doc.Load( rdr );
					
				} catch(XmlException xex) {
					
					String message = String.Format(Cult.InvariantCulture, "XML Parsing Error: \"{0}\" on line {1} position {2}", xex.Message, xex.LineNumber, xex.LinePosition);
					
					throw new PackageValidationException(message, xex);
				}
				
			}
			
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
		
		/// <summary>Enables, Disables, and sets IsSelected as appropriate on child elements to match the specified preset</summary>
		public void ApplySelections(Preset preset) {
			
			List<Group>                     opsB;
			List<ResPatchOperation>         opsR;
			List<WallpaperExtraOperation>   opsW;
			List<VisualStyleExtraOperation> opsV;
			
			Preset.GetPackageItems(RootGroup, out opsB, out opsR, out opsW, out opsV);
			
			foreach(Group group in opsB)
				group.Enabled = group == preset.Group;
			
			foreach(ResPatchOperation opR in opsR)
				opR.Enabled = opR == preset.WelcomeScreen;
			
			foreach(WallpaperExtraOperation opW in opsW)
				opW.Selected = opW == preset.Wallpaper;
			
			foreach(VisualStyleExtraOperation opV in opsV)
				opV.Selected = opV == preset.VisualStyle;
			
		}
		
		//////////////////////////////
		
		protected void OnProgressEvent(ProgressEventArgs e) {
			
			if( ProgressEvent != null ) ProgressEvent(this, e);
		}
		
		public event EventHandler<ProgressEventArgs> ProgressEvent;
		
#region Execute
		
		public override EvaluationResult Evaluate() {
			
			return EvaluateCondition( GetSymbols() );
		}
		
		public void Execute(PackageExecutionSettings settings) {
			
			///////////////////////////////////
			// Prepare
			
			EnsureState();
			
			if( IsBusy ) throw new InvalidOperationException("Cannot execute another package whilst executing a package.");
			IsBusy = true;
			
			if( settings.LiteMode ) {
				FactoryOptions.Populate();
				FactoryOptions.Instance[ Anolis.Core.Data.DirectoryResourceDataFactory.IconSizeLimit ] = 128;
			}
			
			///////////////////////////////////
			// Create Backup Details
			
			Group backupGroup = null;
			if( settings.BackupDirectory != null ) {
				
				if( settings.BackupDirectory.Exists ) {
					
					settings.BackupDirectory = new DirectoryInfo( PackageUtility.GetUnusedDirectoryName( settings.BackupDirectory.FullName ) );
				}
					
				settings.BackupDirectory.Create();
				settings.BackupDirectory.Refresh();
				
				Package backupPackage = new Package( settings.BackupDirectory );
				backupPackage.Version = this.Version;
				backupPackage.Name = this.Name + " Uninstallation Package";
				backupPackage.Attribution = "Anolis Installer";
				
				backupGroup = backupPackage.RootGroup;
			}
			
			ExecutionInfo = new PackageExecutionSettingsInfo(this, settings.ExecutionMode, settings.CreateSystemRestorePoint, settings.LiteMode, backupGroup, settings.I386Directory );
			
			///////////////////////////////////
			// Flatten
			
			Log.Add( LogSeverity.Info, "Beginning package execution: " + this.Name + ", with mode " + ExecutionInfo.ExecutionMode.ToString() );
			
			OnProgressEvent( new PackageProgressEventArgs( 0, "Flattening Package Tree" ) );
			
			List<Operation> operations = new List<Operation>();
			
			RootGroup.Flatten(operations);
			
			List<Operation> obsoleteOperations = new List<Operation>();
			
			Dictionary<String,Operation> uniques = new Dictionary<String,Operation>();
			foreach(Operation op in operations) {
				
				if( !op.IsEnabled ) {
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
			
			Int64 restorePointSequenceNumber = -2;
			
			///////////////////////////////////
			// System Restore, Part 1
			if( ExecutionInfo.ExecutionMode == PackageExecutionMode.Regular && ExecutionInfo.CreateSystemRestorePoint ) {
				
				if( SystemRestore.IsSystemRestoreAvailable() ) {
					
					OnProgressEvent( new PackageProgressEventArgs( -1, "Creating System Restore Point" ) );
					
					String pointName = "Installed Anolis Package \"" + this.Name + '"';
					
					restorePointSequenceNumber = SystemRestore.CreateRestorePoint( pointName, SystemRestoreType.ApplicationInstall );
					
					if( restorePointSequenceNumber < 0 ) Log.Add( LogSeverity.Error, "Failed to create System Restore point" ); 
					
				} else {
					
					Log.Add( LogSeverity.Error, "System Restore not supported" ); 
				}
				
				
			}
			
			///////////////////////////////////
			// Install (Backup and Execute; backups are the responisiblity of each Operation)
			
			try {
				
				float i = 0, cnt = operations.Count;
				
				foreach(Operation op in operations) {
					
					OnProgressEvent( new PackageProgressEventArgs( (int)( 100 * i++ / cnt ), op.ToString() ) );
					
					if( !op.SupportsCDImage && ExecutionInfo.ExecutionMode == PackageExecutionMode.CDImage ) continue;
					
					try {
						
						if( op.CustomEvaluation ) {
							
							op.Execute();
							
						} else {
							
							EvaluationResult result = op.Evaluate();
							
							switch(result) {
								case EvaluationResult.False:
									Log.Add( LogSeverity.Info, "Evaluation False - " + op.Key );
									break;
								case EvaluationResult.FalseParent:
									Log.Add( LogSeverity.Info, "Evaluation ParentFalse - " + op.Key );
									break;
								case EvaluationResult.Error:
									Log.Add( LogSeverity.Error, "Evaluation Error - " + op.Key );
									break;
								case EvaluationResult.True:
									op.Execute();
									break;
							}
							
						}
						
					} catch(Exception ex) {
						
						Log.Add( new LogItem(LogSeverity.Error, ex, op.Name + " failed: \"" + ex.Message + "\"") );
						continue;
					}
					
#if !DEBUG
					// don't add "Info - Done {op}" in debug mode because it's too verbose and clutters up the logfile
					PathOperation pathOp = op as PathOperation;
					if( pathOp != null ) {
						Log.Add( LogSeverity.Info, "Done " + op.Name + ": " + pathOp.Path );
					} else {
						Log.Add( LogSeverity.Info, "Done " + op.Name );
					}
#endif
					
				}//foreach
				
				OnProgressEvent( new PackageProgressEventArgs( 100, "Complete" ) );
				
			} finally {
				
				///////////////////////////////////
				// System Restore, Part 2
				if( restorePointSequenceNumber >= 0 ) {
					
					OnProgressEvent( new PackageProgressEventArgs( -1, "Finishing System Restore Point" ) );
					
					SystemRestore.EndRestorePoint( restorePointSequenceNumber );
				}
				
				///////////////////////////////////
				// Backup, Part 2
				
				if( ExecutionInfo.BackupGroup != null ) {
					
					String backupFileName = Path.Combine( ExecutionInfo.BackupDirectory.FullName, "Package.xml" );
					
					ExecutionInfo.BackupPackage.Write( backupFileName );
					
				}
				
				///////////////////////////////////
				// Dump the log to disk
				
				Log.Save( Path.Combine( this.RootDirectory.FullName, "Anolis.Installer.log" ) );
				
				IsBusy = false;
				
			}//try/finally
			
		}
		
		public void DeleteFiles() {
			
			if( IsBusy ) throw new InvalidOperationException("Cannot delete files whilst the package is executing");
			
			FileInfo logFile = RootDirectory.GetFile("Anolis.Installer.log");
			String logFileDest = RootDirectory.Parent.GetFile( logFile.Name ).FullName;
			if( logFile.Exists ) {
				
				if( File.Exists( logFileDest ) ) File.Delete( logFileDest );
				
				logFile.MoveTo( logFileDest );
			}
			
			if( !RootDirectory.Exists ) return;
			
			Exception ex = null;
			
			try {
				
				RootDirectory.Delete(true);
				
			} catch(IOException iox) {
				ex = iox;
			} catch(UnauthorizedAccessException uax) {
				ex = uax;
			} catch(System.Security.SecurityException sex) {
				ex = sex;
			}
			
			if( ex != null ) {
				// the obvious cases have been taken care of. This exception could happen because:
				//   * the user has a file open
				//   * its trying to delete Uninstall.exe if this was an uninstallation package (!!!)
				// so just append it to the log
				
				if( File.Exists( logFileDest ) ) {
					using(StreamWriter wtr = new StreamWriter(logFileDest, true)) {
						
						LogItem item = new LogItem(LogSeverity.Error, ex, "PackageDeleteFiles Exception");
						item.Write( wtr );
					}
				}
				
			}//if
			
		}//void
		
#endregion
		
		/// <summary>A blocking (synchronous) method that returns details of the latest version, if it exists. Returns null under all failure conditions.</summary>
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
			Version version; Uri packageLocation = null, infoLocation = null;
			
			if( !Extensions.VersionTryParse( lines[1], out version ) ) return null;
			
			Uri.TryCreate( lines[2], UriKind.Absolute, out packageLocation );
			Uri.TryCreate( lines[3], UriKind.Absolute, out infoLocation );
			
			PackageUpdateInfo info = new PackageUpdateInfo( name, version, packageLocation, infoLocation );
			return info;
			
		}
		
		private void BuildPresets(XmlElement presetsElement) {
			
			foreach(XmlNode child in presetsElement.ChildNodes) {
				XmlElement childElement = child as XmlElement;
				if( childElement == null ) continue;
				
				Preset preset = new Preset( RootGroup, childElement );
				Presets.Add( preset );
				
			}
			
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
			
			                          PackageItem.AddAttribute(root, "id"         , Id                    );
			                          PackageItem.AddAttribute(root, "name"       , Name                  );
			if( Condition   != null ) PackageItem.AddAttribute(root, "condition"  , Condition.ToString()  );
			                          PackageItem.AddAttribute(root, "version"    , Version.ToString()    );
			                          PackageItem.AddAttribute(root, "attribution", Attribution           );
			if( Website     != null ) PackageItem.AddAttribute(root, "website"    , Website.ToString()    );
			if( UpdateUri   != null ) PackageItem.AddAttribute(root, "updateUri"  , UpdateUri.ToString()  );
			if( FeedbackUri != null ) PackageItem.AddAttribute(root, "feedbackUri", FeedbackUri.ToString());
			
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
		
		public PackageUpdateInfo(String name, Version version, Uri packageLocation, Uri infoLocation) {
			
			Name                = name;
			Version             = version;
			PackageLocation     = packageLocation;
			InformationLocation = infoLocation;
		}
		
		public String  Name                { get; private set; }
		public Version Version             { get; private set; }
		public Uri     PackageLocation     { get; private set; }
		public Uri     InformationLocation { get; private set; }
		
	}
	
}
