using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;

using Anolis.Core;
using Anolis.Core.Data;
using Anolis.Core.Utility;

using P = System.IO.Path;

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Anolis.Core.Packages.Operations {
	
	public class PatchOperation : Operation {
		
		public PatchOperation(Package package, Group parent, XmlElement operationElement) : base(package, parent, operationElement) {
			
			ResourceSet   = new PatchResourceSet( base.Condition );
			_resourceSets = new List<PatchResourceSet>() { ResourceSet };
			
			Path     = operationElement.GetAttribute("path");
			I386Path = operationElement.GetAttribute("i386path");
			
			SaveTo   = operationElement.GetAttribute("saveTo");
			if(SaveTo.Length > 0)
				SaveTo = PackageUtility.ResolvePath( SaveTo );
			else
				SaveTo = null;
			
			foreach(XmlNode node in operationElement.ChildNodes) {
				
				if(node.NodeType != XmlNodeType.Element) continue;
				
				XmlElement child = node as XmlElement;
				
				if( child.Name != "res" ) continue;
				
				PatchResource res = new PatchResource() {
					Type = child.GetAttribute("type"),
					Name = child.GetAttribute("name"),
					Lang = child.GetAttribute("lang"),
					File = child.GetAttribute("src"),
					Add  = child.GetAttribute("add") == "true" || child.GetAttribute("add") == "1"
				};
				
				if( !res.File.StartsWith("comp:", StringComparison.Ordinal) )
					res.File = P.Combine( package.RootDirectory.FullName, res.File );
				
				ResourceSet.Resources.Add( res );
			}
			
		}
		
		public PatchOperation(Package package, Group parent, String path) : base(package, parent, path) {
			
			ResourceSet   = new PatchResourceSet( base.Condition );
			_resourceSets = new List<PatchResourceSet>() { ResourceSet };
		}
		
		public String  ConditionHash { get; private set; }
		public String  SaveTo        { get; set; }
		public String  I386Path      { get; set; }
		public PatchResourceSet ResourceSet { get; private set; }
		
		private List<PatchResourceSet> _resourceSets;
		
#region Execute
		
		public override void Execute() {
			
			if( Package.ExecutionInfo.ExecutionMode == PackageExecutionMode.Regular ) {
				
				if( Package.ExecutionInfo.MirrorX64 ) {
					
					// check to see if the equivalent file exists in 32-bit Program Files or SysWow64
					String sysWow64Path = PackageUtility.GetSysWow64File( Path );
					if( sysWow64Path != null ) {
						
						// NOTE: ExecuteRegular calls PatchFile which makes the backup entry
						// however the Condition will not be evaluated for the x86 version of the file
						// I probably should fix this...
						ExecuteRegular( Path );
					}
					
				}
				
				ExecuteRegular( Path );
				
			} else if( Package.ExecutionInfo.ExecutionMode == PackageExecutionMode.I386) {
				
				String workingPath, i386Path;
				
				I386Prepare( Path, out workingPath, out i386Path );
				
				if( workingPath == null ) return;
				
				PatchFile( workingPath );
				
				if( i386Path != null )
					I386Aftermath( workingPath, i386Path );
				
			}
			
		}
		
		private void ExecuteRegular(String path) {
			
			if( !File.Exists( path ) ) {
				
				Package.Log.Add( new LogItem(LogSeverity.Error, "Source file not found: " + path) );
				return;
			}
			
			// copy the file first
			
			String workOnThis;
			
			if( !String.IsNullOrEmpty( SaveTo ) ) {
				
				workOnThis = SaveTo;
				
			} else {
				
				workOnThis = path + ".anofp"; // "Anolis File Pending"
			}
			
			if(File.Exists( workOnThis )) Package.Log.Add( LogSeverity.Warning, "Overwritten *.anofp: " + workOnThis);
			File.Copy( path, workOnThis, true );
			
			PatchFile( workOnThis );
			
			// if it throws, this won't be encountered
			PackageUtility.AddPfroEntry( workOnThis, path );
			
			Package.ExecutionInfo.RequiresRestart = true;
		}
		
		/// <param name="path">The original path to the file as if it were in the local computer's system directories</param>
		/// <param name="workingFilePath">The path to the file to perform the patch operation on (located under the Temp directory)</param>
		/// <param name="i386FilePath">The path to the file in its I386 directory</param>
		private void I386Prepare(String path, out String workingFilePath, out String i386FilePath) {
			
			// if the I386 path is already provided, don't bother searching for it
			// otherwise, find the compressed file
			
			FileInfo compressedFile;
			
			String nom, ext;
			
			if( I386Path != null ) {
				
				compressedFile = Package.ExecutionInfo.I386Directory.GetFile( I386Path );
				
				nom = P.GetFileName( compressedFile.FullName );
				ext = P.GetExtension( compressedFile.FullName );
				
			} else {
				
				// get the filename, the path is not needed
				nom = P.GetFileNameWithoutExtension( path );
				ext = P.GetExtension( path );
				
				String compressedFilename = nom + ext.LeftFR(1) + '_';
				
				compressedFile = Package.ExecutionInfo.I386FindFile( compressedFilename );
				
			}
			
			String destTempDir =  P.Combine( P.GetTempPath(), @"Anolis\I386");
			if( !Directory.Exists( destTempDir ) ) Directory.CreateDirectory( destTempDir );
			
			if( compressedFile != null ) {
				
				// expand it
				
				String destFile = PackageUtility.GetUnusedFileName( P.Combine(destTempDir, nom + ext ) );
				
				// don't use the -r switch if specifying an output filename
				//String args = String.Format(CultureInfo.InvariantCulture, @"-r ""{0}"" ""{1}""", compressedFile.FullName, destFile);
				String args = String.Format(CultureInfo.InvariantCulture, @"""{0}"" ""{1}""", compressedFile.FullName, destFile);
				
				ProcessStartInfo procStart = new ProcessStartInfo("expand", args);
				procStart.WindowStyle = ProcessWindowStyle.Hidden;
				procStart.CreateNoWindow = true;
				
				Process p = Process.Start( procStart );
				
				if( !p.Start() ) {
					Package.Log.Add( LogSeverity.Error, "Couldn't start expand process");
					
					workingFilePath = null;
					i386FilePath    = null;
					
					return;
				}
				
				if( !p.WaitForExit( 5000 ) ) {
					Package.Log.Add( LogSeverity.Error, "expand took longer than 5000ms");
					
					workingFilePath = null;
					i386FilePath    = null;
					
					return;
				}
				
				workingFilePath = destFile;
				i386FilePath    = compressedFile.FullName;
				
				return;
			}
			
			// uncompressed file
			
			FileInfo uncompressedFile = Package.ExecutionInfo.I386FindFile( P.GetFileName( path ) );
			
			if( uncompressedFile != null ) {
				
				workingFilePath = uncompressedFile.FullName;
				i386FilePath    = null;
				return;
			}
			
			Package.Log.Add( LogSeverity.Warning, "Couldn't find file " + P.GetFileName( path ) + " in the I386 directory");
			
			workingFilePath = null;
			i386FilePath    = null;
			
		}
		
		private void I386Aftermath(String expandedPath, String originalPath) {
			
			// compress and overwrite
			
			String args = String.Format(CultureInfo.InvariantCulture, @"/D CompressionType=LZX /D CompressionMemory=21 ""{0}"" ""{1}""", expandedPath, originalPath);
			
			ProcessStartInfo procStart = new ProcessStartInfo("makecab", args);
			procStart.WindowStyle = ProcessWindowStyle.Hidden;
			procStart.CreateNoWindow = true;
			
			Process p = Process.Start( procStart );
			
			p.Start();
			// no need to wait for the result
			
		}
		
		private void PatchFile(String fileName) {
			
			Dictionary<String,Double> symbols = BuildSymbols( fileName );
			
			List<PatchResource> patchResources = new List<PatchResource>();
			foreach(PatchResourceSet set in _resourceSets) {
				
				try {
					Double result = set.Condition.Evaluate( symbols );
					
					if(result == 1) {
						
						// HACK: This just adds them together into a massive list. If the same name is mentioned it'll be overwritten several times
						// fortunately it isnt' very expensive as only the last "final" one counts, but could do with filtering at this stage maybe?
						
						patchResources.AddRange( set.Resources );
					} else {
						Package.Log.Add( LogSeverity.Info, "Expression evaluation zero: " + set.Condition.ExpressionString + ", did not process " + set.Resources.Count + " resources" );
					}
					
				} catch(ExpressionException ex) {
					
					Package.Log.Add( LogSeverity.Error, "Expression evaluation exception: " + ex.Message );
				}
			}
			
			try {
				
				// for now, use lazy-load under all circumstances. In future analyse the Resources list to see if it's necessary or not
				// but the performance impact is minimal and it's the safest option, so keep it as it is
				using(ResourceSource source = ResourceSource.Open(fileName, false, ResourceSourceLoadMode.LazyLoadData)) {
					
					List<String> tempFiles = new List<String>();
					
					foreach(PatchResource res in patchResources) {
						
						if( res.File.StartsWith("comp:") ) {
							
							CompositedImage comp = new CompositedImage( res.File, Package.RootDirectory );
							
							DirectoryInfo packageTempDirectory = new DirectoryInfo( P.Combine( Package.RootDirectory.FullName, "Temp" ) );
							if( !packageTempDirectory.Exists ) packageTempDirectory.Create();
							
							// I think not using the *.bmp extension messes up Bitmap import
							String tempFileName = PackageUtility.GetUnusedFileName( P.Combine( packageTempDirectory.FullName, P.GetFileName(Path) + res.Name ) + ".bmp" );
							
							comp.Save( tempFileName, System.Drawing.Imaging.ImageFormat.Bmp );
							
							res.File = tempFileName;
							tempFiles.Add( tempFileName );
						
						} else if( !File.Exists( res.File ) ) {
							Package.Log.Add( LogSeverity.Error, "Data File not found: " + res.File );
							continue;
						}
						
						ResourceTypeIdentifier typeId = ResourceTypeIdentifier.CreateFromString( res.Type, true );
						ResourceIdentifier     nameId = ResourceIdentifier.CreateFromString( res.Name );
						UInt16                 langId = String.IsNullOrEmpty( res.Lang ) ? UInt16.MaxValue : UInt16.Parse( res.Lang, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture );
						
						if(	langId == UInt16.MaxValue ) { // if the lang="" attribute was not specified
							
							ResourceName name = source.GetName(typeId, nameId);
							
							if(name == null) { // if the specified name does not exist
								
								if( res.Add ) {
									
									UInt16 sysLang = (UInt16)CultureInfo.InvariantCulture.LCID;
									
									ResourceData data = ResourceData.FromFileToAdd( res.File, sysLang, source );
									source.Add( typeId, nameId, sysLang, data );
									
								} else {
									
									Package.Log.Add( LogSeverity.Warning, "Name not found: " + source.Name + '\\' + typeId.ToString() + '\\' + nameId.FriendlyName );
								}
								
								
							} else {
								
								foreach(ResourceLang lang in name.Langs) {
									
									ResourceData data = ResourceData.FromFileToAdd( res.File, lang.LanguageId, source );
									lang.SwapData( data );
									
								}
								
							}
							
						} else { // if the lang="" attribute was specified
							
							ResourceLang lang = source.GetLang( typeId, nameId, langId );
							if(lang == null) {
								
								ResourceData data = ResourceData.FromFileToAdd( res.File, langId, source );
								source.Add( typeId, nameId, langId, data );
								
							} else {
								
								ResourceData data = ResourceData.FromFileToUpdate( res.File, lang );
								lang.SwapData( data );
							}
							
						}
						
					}//foreach
					
					// note that Win32ResourceSource now recomptues the PE checksum by itself
					source.CommitChanges();
					
					foreach(String tempFile in tempFiles) File.Delete( tempFile );
					
				}//using source
				
				if( Package.ExecutionInfo.BackupGroup != null ) {
					
					String hash = PackageUtility.GetMD5Hash( fileName );
					
					Backup( Package.ExecutionInfo.BackupGroup, Path, hash );
					
				}
				
			} catch(AnolisException aex) {
				
				Package.Log.Add( LogSeverity.Error, "Patch Exception: " + aex.Message );
				
				if( File.Exists( fileName ) ) File.Delete( fileName );
				
				throw;
			}
			
		}
		
#endregion
		
		private void Backup(Group backupGroup, String originalFileName, String patchedHash) {
			
			DirectoryInfo backupDir = backupGroup.Package.RootDirectory;
			
			String backupToThis = P.Combine( backupDir.FullName, P.GetFileName( originalFileName ) );
			backupToThis = PackageUtility.GetUnusedFileName( backupToThis );
			
			File.Copy( originalFileName, backupToThis );
			
			FileOperation op = new FileOperation(backupGroup.Package, backupGroup, backupToThis, originalFileName, FileOperationType.Copy);
			op.ConditionHash = patchedHash;
			backupGroup.Operations.Add( op );
			
		}
		
		public override void Write(XmlElement parent) {
			
			foreach(PatchResourceSet set in this._resourceSets) {
				
				XmlElement element = CreateElement(parent, "patch", "path", Path);
				if( set.Condition != null ) {
					XmlAttribute attrib = element.Attributes.GetNamedItem("condition") as XmlAttribute;
					if( attrib != null ) {
						attrib.Value = set.Condition.ExpressionString;
					} else {
						AddAttribute( element, "condition", set.Condition.ExpressionString );
					}
				}
				
				foreach(PatchResource res in set.Resources) {
					
					XmlElement re = CreateElement(element, "res");
					AddAttribute(re, "type", res.Type);
					AddAttribute(re, "name", res.Name);
					AddAttribute(re, "lang", res.Lang);
					AddAttribute(re, "src", res.File);
					if( res.Add ) AddAttribute(re, "add", "true");
					
				}
				
			}
			
			
			
		}
		
		private Boolean EvaluateCondition() {
			
			if( !String.IsNullOrEmpty( ConditionHash ) ) {
				
				String currentHash = PackageUtility.GetMD5Hash( Path );
				if( !String.Equals( ConditionHash, currentHash, StringComparison.OrdinalIgnoreCase ) ) {
					
					return false;
				}
				
			}
			
			if( Condition != null ) {
				
				Dictionary<String,Double> symbols = BuildSymbols( Path );
				
				EvaluationResult result = Evaluate( symbols );
				return result == EvaluationResult.True;
				
			}
			
			return true;
		}
		
		public override Boolean SupportsI386 {
			get { return true; }
		}
		
		public override String OperationName {
			get { return "Res patch"; }
		}
		
		public override Boolean Merge(Operation operation) {
			
			PatchOperation op = operation as PatchOperation;
			if( op == null ) return false;
			
			if( !String.Equals( Path, op.Path, StringComparison.OrdinalIgnoreCase ) ) return false;
			
			// add the incoming operation's sets to this own
			_resourceSets.AddRange( op._resourceSets );
			
			return true;
		}
		
	}
	
	public class PatchResourceSet {
		
		public PatchResourceSet(Expression expression) {
			
			Condition = expression;
			Resources  = new Collection<PatchResource>();
		}
		
		public Expression Condition { get; set; }
		
		public Collection<PatchResource> Resources { get; private set; }
		
	}
	
	public class PatchResource {
		
		public String Type { get; set; }
		
		public String Name { get; set; }
		
		public String Lang { get; set; }
		
		public String File { get; set; }
		
		/// <summary>True if the resource is to be added as opposed to updated.</summary>
		public Boolean Add { get; set; }
	}
	
}
