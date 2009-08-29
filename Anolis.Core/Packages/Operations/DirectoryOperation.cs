using System;
using System.Xml;
using System.IO;
using Anolis.Core.Utility;

using P = System.IO.Path;

namespace Anolis.Core.Packages.Operations {
	
	public class DirectoryOperation : PathOperation {
		
		public DirectoryOperation(Group parent, XmlElement element) : base(parent, element) {
			
			String opType = element.GetAttribute("operation").ToUpperInvariant();
			switch(opType) {
				case "COPY":
					Operation = FileOperationType.Copy; break;
				case "DELETE":
					Operation = FileOperationType.Delete; break;
				case "REPLACE":
					Operation = FileOperationType.Replace; break;
				default:
					Operation = FileOperationType.None; break;
			}
			
			SpecifiedPath = element.GetAttribute("path");
			SourceDirectory = PackageUtility.ResolvePath( element.GetAttribute("src"), Package.RootDirectory.FullName );
		}
		
		public DirectoryOperation(Group parent, String sourcePath, String destPath, FileOperationType operation) : base(parent, destPath) {
			
			SourceDirectory = sourcePath;
			
			Operation       = operation;
		}
		
		public String SpecifiedPath { get; set; }
		public String SourceDirectory { get; set; }
		
		public FileOperationType Operation { get; private set; }
		
		public override string OperationName {
			get { return "Directory"; }
		}
		
		public override void Execute() {
			
			if( Package.ExecutionInfo.ExecutionMode != PackageExecutionMode.Regular ) return;
			
			switch(Operation) {
				
				case FileOperationType.Delete:
					
					DirectoryInfo dirToDelete = new DirectoryInfo( Path );
					if( !dirToDelete.Exists ) {
						Package.Log.Add( LogSeverity.Error, "Could not find directory to delete: " + Path );
						return;
					}
					
					//Directory.Delete( Path, true );
					PackageUtility.AddPfroEntry( dirToDelete );
					
					break;
					
				case FileOperationType.Copy:
				case FileOperationType.Replace:
					
					String sourceDir = P.Combine( Package.RootDirectory.FullName, SourceDirectory );
					
					DirectoryInfo source = new DirectoryInfo( sourceDir );
					if( !source.Exists ) {
						Package.Log.Add( LogSeverity.Error, "Could not find source directory: " + sourceDir );
						return;
					}
					
					if( Directory.Exists( Path ) && Operation != FileOperationType.Replace ) {
						
						Package.Log.Add( LogSeverity.Error, "Will not overwrite: " + Path );
						return;
						
					}
					
					source.CopyTo( Path );
					
					break;
					
				case FileOperationType.None:
				default:
					return;
			}
			
		}
		
		public override Boolean Merge(Operation operation) {
			return false;
		}
		
		public override void Write(XmlElement parent) {
			
			CreateElement(parent, "directory",
				"operation", Operation.ToString(),
				"src"      , SourceDirectory,
				"path"     , Path
			);
			
		}
	}
}
