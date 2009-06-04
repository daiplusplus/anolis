using System;
using System.Collections.Generic;
using System.Text;

using Anolis.Core;
using Anolis.Core.Data;
using Anolis.Core.Source;
using System.IO;

using Anolis.Core.Utility;

namespace Anolis.Resourcer.CommandLine {
	
	public class BatchProcess {
		
		private BatchReport _report;
		
		public BatchProcess() {
			_report = new BatchReport();
			Log = new Log();
		}
		
		public BatchOptions Options { get; private set; }
		public Boolean      IsBusy  { get; private set; }
		public Boolean      Cancel  { get; set; }
		
		public Int32 FilesCount { get; private set; }
		public Int32 FilesDone  { get; private set; }
		
		public Int32  MajorProgressPercentage { get; private set; }
		public String MajorProgressMessage    { get; private set; }
		
		public Int32  MinorProgressPercentage { get; private set; }
		public String MinorProgressMessage    { get; private set; }
		
		public event EventHandler MajorProgressChanged;
		public event EventHandler MinorProgressChanged;
		
#region Error Logging
		
		public Log Log { get; private set; }
		
#endregion
		
#region Meta and Utility
		
		private void OnMajorProgressChanged(Single i, Single total, String message) {
			
			Int32 percentage = (int)( 100 * i / total );
			
			MajorProgressPercentage = i == -1 ? -1 : percentage;
			MajorProgressMessage    = message;
			
			if( MajorProgressChanged != null ) MajorProgressChanged( this, EventArgs.Empty );
		}
		
		private void OnMinorProgressChanged(Single i, Single total, String message) {
			
			Int32 percentage = (int)( 100 * i / total );
			
			MinorProgressPercentage = percentage;
			MinorProgressMessage    = message;
			
			if( MinorProgressChanged != null ) MinorProgressChanged( this, EventArgs.Empty );
		}
		
		private void ResetAndEnsure(BatchOptions options) {
			
			/////////////////////////////////
			// Ensure
			
			if( IsBusy ) {
				throw new InvalidOperationException("The BatchProcess is busy processing another operation.");
			}
			
			IsBusy = true;
			
			if( options == null ) throw new ArgumentNullException("options");
			
			/////////////////////////////////
			// Reset State
			
			Cancel  = false;
			_report = null;
			Options = options;
			
		}
		
		private FileInfo[] GetFiles() {
			
			List<FileInfo> files = new List<FileInfo>();
			
			String[] filters = Options.SourceFilter.Split(';');
			
			// I can't use DirectoryInfo.GetFiles because it blindly accesses directories and fails on Windows 7 when it tries to access C:\windows\temp
			
//			foreach(String fltr in filters) {
//				
//				// .GetFiles is recursive in itself, which saves trouble
//				FileInfo[] f = Options.SourceDirectory.GetFiles( fltr, Options.SourceRecurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly );
//				files.AddRange( f );
//				
//			}
			
			AddFiles( Options.SourceDirectory, filters, files );
			
			return files.ToArray();
		}
		
		private static void AddFiles(DirectoryInfo directory, String[] filterPatterns, List<FileInfo> files) {
			
			foreach(String filter in filterPatterns) {
				
				files.AddRange( directory.GetFiles( filter ) );
			}
			
			foreach(DirectoryInfo child in directory.GetDirectories()) {
				
				try {
					
					AddFiles( child, filterPatterns, files );
					
				} catch(IOException) { // TODO: Get some kind of centralised logging system up so I can report this
				} catch(UnauthorizedAccessException) {
				} catch(System.Security.SecurityException) {
				}
				
			}
			
		}
		
#endregion
		
		public void Process(BatchOptions options) {
			
			ResetAndEnsure(options);
			
			try {
				
				OnMajorProgressChanged( -1, -1, "Enumerating files");
				
				// get a list of files from the directory
				FileInfo[] files = GetFiles();
				FilesCount = files.Length;
				
				for(int i=0;i<files.Length;i++) {
					
					if( Cancel ) break;
					
					FileInfo file = files[i];
					
					ProcessFile( file );
					
					FilesDone = i;
					OnMajorProgressChanged(i + 1, files.Length, file.Name );
				}
				
			} finally {
				
				IsBusy = false;
			}
			
		}
		
		private void ProcessFile(FileInfo file) {
			
			ResourceSource source;
			Int32 nofNames = 0;
			
			try {
				
				source = ResourceSource.Open( file.FullName, true, ResourceSourceLoadMode.LazyLoadData );
				
				// quickly get a count of all the names
				foreach(ResourceType type in source.AllTypes) foreach(ResourceName name in type.Names) nofNames++;
			
			} catch(AnolisException aex) {
				
				Log.Add( LogSeverity.Error, "Couldn't open " + file.FullName + ", " + aex.Message );
				
				return;
			}
			
			// now process it
			Int32 i = 1;
			foreach(ResourceType type in source.AllTypes) {
				
				foreach(ResourceName name in type.Names) {
					
					foreach(ResourceLang lang in name.Langs) {
						
						try {
							
							// such as it is, you need to load ALL the resource data to determine its type as ResourceType is unreliable
							ResourceData data = lang.Data;
							
							if( ShouldExportData( data ) ) {
								
								// one directory per file
								// get the path of the file relative to the root directory being searched
								String relativeName = file.FullName.Substring( Options.SourceDirectory.FullName.Length );
								if( relativeName.StartsWith("\\") ) relativeName = relativeName.Substring( 1 );
								
								String directory = Path.Combine( Options.ExportDirectory.FullName, relativeName );
								
								if( !Directory.Exists( directory ) ) Directory.CreateDirectory( directory );
								
								String filename = Path.Combine( directory, Anolis.Core.Utility.Miscellaneous.FSSafeResPath( data.Lang.ResourcePath ) ) + data.RecommendedExtension;
								
								data.Save( filename );
								
							}
							
						} catch(Exception ex) {
							
							Log.Add( LogSeverity.Error, "Couldn't save " + file.FullName + lang.ResourcePath + ", Exception: " + ex.Message );
							
						}
						
						
					}//lang
					
					OnMinorProgressChanged( i++, nofNames, name.Identifier.FriendlyName );
					
				}//name
				
			}//type
		}
		
		private Boolean ShouldExportData(ResourceData data) {
			
			/////////////////////////////////
			// Special check for icon subimages
			
			IconCursorImageResourceData icdata = data as IconCursorImageResourceData;
			if(icdata != null && !Options.ExportIcons) return false;
			
			/////////////////////////////////
			// All visual resources should be exported regardless
			
			if(data is DirectoryResourceData) return true;
			if(data is ImageResourceData)     return true;
			if(data is MediaResourceData)     return true;
			
			/////////////////////////////////
			// If non-visual, check with the options
			
			if( !Options.ExportNonVisual) return false;
			
			/////////////////////////////////
			// Certain commonplace resources might not be exported
			
			if(data is VersionResourceData)   return Options.ExportNonVisual && Options.ExportCommonRes;
			if(data is SgmlResourceData && data.Lang.Name.Type.Identifier.KnownType == Win32ResourceType.Manifest)
				return Options.ExportNonVisual && Options.ExportCommonRes;
			
			/////////////////////////////////
			// Then check size
			
			if( Options.ExportNonVisualSize == -1 ) return true;
			
			return data.RawData.Length >= Options.ExportNonVisualSize;
			
		}
		
	}
	
	public class BatchOptions {
		
		public DirectoryInfo SourceDirectory { get; set; }
		public String        SourceFilter    { get; set; }
		public Boolean       SourceRecurse   { get; set; }
		
		public Boolean       ReportCreate    { get; set; }
		public FileInfo      ReportFile      { get; set; }
		
		public DirectoryInfo ExportDirectory { get; set; }
		public Boolean       ExportNonVisual { get; set; }
		public Boolean       ExportCommonRes { get; set; }
		public Boolean       ExportIcons     { get; set; }
		public Int32         ExportNonVisualSize { get; set; } // size in bytes
		
	}
	
	
	
}
