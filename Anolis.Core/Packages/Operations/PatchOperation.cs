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
		
		private String _saveTo;
		
		public PatchOperation(Package package, Group parent, XmlElement operationElement) : base(package, parent, operationElement) {
			
			Resources = new Collection<PatchResource>();
			
			base.Path = operationElement.GetAttribute("path");
			
			_saveTo   = operationElement.GetAttribute("saveTo");
			if(_saveTo.Length > 0)
				_saveTo = PackageUtility.ResolvePath( _saveTo );
			
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
				
				res.File = P.Combine( package.RootDirectory.FullName, res.File );
				
				Resources.Add( res );
			}
			
		}
		
		public PatchOperation(Package package, Group parent, String path) : base(package, parent, path) {
			
			Resources = new Collection<PatchResource>();
			
		}
		
		public override void Execute() {
			
			if( Package.ExecutionMode == PackageExecutionMode.Regular ) {
				
				ExecuteRegular( Path );
				
			} else if( Package.ExecutionMode == PackageExecutionMode.I386) {
				
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
				
				Package.Log.Add( new LogItem(LogSeverity.Error, "Source File not found: " + path) );
				return;
			}
			
			// copy the file first
			
			String workOnThis;
			
			if( _saveTo != null ) {
				
				workOnThis = _saveTo;
				
			} else {
				
				workOnThis = path + ".anofp"; // "Anolis File Pending"
			}
			
			if(File.Exists( workOnThis )) Package.Log.Add( LogSeverity.Warning, "Overwritten *.anofp: " + workOnThis);
			File.Copy( path, workOnThis, true );
			
			PatchFile( workOnThis );
			
			// if it throws, this won't be encountered
			PackageUtility.AddPfroEntry( workOnThis, path );
			
		}
		
		private void I386Prepare(String path, out String workingFilePath, out String i386FilePath) {
			
			// get the filename, the path is not needed
			String nom = P.GetFileNameWithoutExtension( path );
			String ext = P.GetExtension( path );
			
			String compressedFilename = nom + ext.LeftFR(1) + '_';
			
			FileInfo compressedFile = Package.I386Info.FindFile( compressedFilename );
			
			String destTempDir =  P.Combine( P.GetTempPath(), "AnolisI386");
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
			
			FileInfo uncompressedFile = Package.I386Info.FindFile( P.GetFileName( path ) );
			
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
			
			try {
				
				// for now, use lazy-load under all circumstances. In future analyse the Resources list to see if it's necessary or not
				// but the performance impact is minimal and it's the safest option, so keep it as it is
				using(ResourceSource source = ResourceSource.Open(fileName, false, ResourceSourceLoadMode.LazyLoadData)) {
					
					foreach(PatchResource res in Resources) {
						
						if( !File.Exists( res.File ) ) {
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
									
									UInt16 sysLang = GetSystemLangId();
									
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
						
					}
					
					source.CommitChanges();
				}
				
			} catch(AnolisException aex) {
				
				Package.Log.Add( LogSeverity.Error, "Patch Exception: " + aex.Message );
				
				if( File.Exists( fileName ) ) File.Delete( fileName );
				
				throw;
			}
			
		}
		
		public override void Backup(Group backupGroup) {
			// TODO
		}
		
		public override void Write(XmlElement parent) {
			
			XmlElement element = CreateElement(parent, "patch", "path", Path);
			
			foreach(PatchResource res in Resources) {
				
				XmlElement re = CreateElement(element, "res");
				AddAttribute(re, "type", res.Type);
				AddAttribute(re, "name", res.Name);
				AddAttribute(re, "lang", res.Lang);
				AddAttribute(re, "src", res.File);
				if( res.Add ) AddAttribute(re, "add", "true");
				
			}
			
		}
		
		public override Boolean SupportsI386 {
			get { return true; }
		}
		
		private static UInt16 GetSystemLangId() {
			
			return PackageUtility.GetSystemInstallLanguage();
		}
		
		public override String OperationName {
			get { return "Res patch"; }
		}
		
		public override Boolean Merge(Operation operation) {
			
			// TODO check condition first
			return false;
		}
		
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
