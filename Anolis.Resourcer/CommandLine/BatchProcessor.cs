using System;
using System.Collections.Generic;
using System.Text;

using Anolis.Core;
using Anolis.Core.Data;
using Anolis.Core.Source;
using System.IO;

namespace Anolis.Resourcer.CommandLine {
	
	public class BatchProcess {
		
		private Report       _report;
		
		public BatchProcess() {
		}
		
		public BatchOptions Options { get; private set; }
		public Boolean      IsBusy  { get; private set; }
		
		public Int32  MajorProgressPercentage { get; private set; }
		public String MajorProgressMessage    { get; private set; }
		
		public Int32  MinorProgressPercentage { get; private set; }
		public String MinorProgressMessage    { get; private set; }
		
		public event EventHandler MajorProgressChanged;
		public event EventHandler MinorProgressChanged;
		
#region Meta and Utility
		
		private void OnMajorProgressChanged(Single i, Single total, String message) {
			
			Int32 percentage = (int)( 100 * i / total );
			
			MajorProgressPercentage = percentage;
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
			
			_report  = null;
			Options = options;
			
		}
		
		private FileInfo[] GetFiles() {
			
			List<FileInfo> files = new List<FileInfo>();
			
			String[] filters = Options.SourceFilter.Split(';');
			foreach(String fltr in filters) {
				
				// .GetFiles is recursive in itself, which saves trouble
				FileInfo[] f = Options.SourceDirectory.GetFiles( fltr, Options.SourceRecurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly );
				files.AddRange( f );
				
			}
			
			return files.ToArray();
		}
		
#endregion
		
		public void Process(BatchOptions options) {
			
			ResetAndEnsure(options);
			
			try {
				
				// get a list of files from the directory
				FileInfo[] files = GetFiles();
				for(int i=0;i<files.Length;i++) {
					
					FileInfo file = files[i];
					
					ProcessFile( file );
					
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
				
				// TODO: add it to the Report
				
				return;
			}
			
			// now process it
			Int32 i = 1;
			foreach(ResourceType type in source.AllTypes) {
				
				foreach(ResourceName name in type.Names) {
					
					foreach(ResourceLang lang in name.Langs) {
						
//						try {
							
							// such as it is, you need to load ALL the resource data to determine its type as ResourceType is unreliable
							ResourceData data = lang.Data;
							
							if( ShouldExportData( data ) ) {
								
								// one directory per file
								// get the path of the file relative to the root directory being searched
								String relativeName = file.FullName.Substring( Options.SourceDirectory.FullName.Length + 1 ); // +1 to skip the forward slash
								
								String directory = Path.Combine( Options.ExportDirectory.FullName, relativeName );
								
								if( !Directory.Exists( directory ) ) Directory.CreateDirectory( directory );
								
								String filename = Path.Combine( directory, Anolis.Core.Utility.Miscellaneous.FSSafeResPath( data.Lang.ResourcePath ) ) + data.RecommendedExtension;
								
								data.Save( filename );
									
							}
							
//						} catch(IOException io) {
							
//						} catch(AnolisException ax) {
							
//						}
						
						
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
			
			DirectoryResourceData ddata = data as DirectoryResourceData;
			if(ddata != null) return true;
			
			ImageResourceData idata = data as ImageResourceData;
			if(idata != null) return true;
			
			MediaResourceData mdata = data as MediaResourceData;
			if(mdata != null) return true;
			
			/////////////////////////////////
			// If non-visual, check with the options
			
			return Options.ExportNonVisual;
			
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
		public Boolean       ExportAllLangs  { get; set; }
		public Boolean       ExportIcons     { get; set; }
		
	}
	
	internal class Report {
		
	}
	
	internal class ReportFile {
		
	}
	
	internal class ReportResource {
		
	}
	
	
	
}
