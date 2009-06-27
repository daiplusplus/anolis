using System;
using System.IO;
using System.Xml;

using P = System.IO.Path;
using Anolis.Core.Utility;

namespace Anolis.Core.Packages.Operations {
	
	public class FileOperation : Operation {
		
		public FileOperation(Package package, Group group, XmlElement operationElement) : base(package, group, operationElement) {
			
			String opType = operationElement.GetAttribute("operation").ToUpperInvariant();
			switch(opType) {
				case "COPY":
					Operation = FileOperationType.Copy; break;
				case "DELETE":
					Operation = FileOperationType.Delete; break;
				default:
					Operation = FileOperationType.None; break;
			}
			
			SpecifiedPath = operationElement.GetAttribute("path");
			ConditionHash = operationElement.GetAttribute("conditionHash");
			
			// the .Path setter resolves environment variables and sets the root to the package directory if non-rooted
			Path       = SpecifiedPath;
			SourceFile = PackageUtility.ResolvePath( operationElement.GetAttribute("src"), Package.RootDirectory.FullName );
		}
		
		public FileOperation(Package package, Group parent, String sourcePath, String destPath, FileOperationType operation) : base(package, parent, destPath) {
			
			SourceFile = sourcePath;
			
			Operation = operation;
		}
		
		public String SourceFile    { get; set; }
		public String SpecifiedPath { get; set; }
		public String ConditionHash { get; set; }
		
		public FileOperationType Operation { get; private set; }
		
		public override void Execute() {
			
			Backup( Package.ExecutionInfo.BackupGroup );
			
			// the Package class sets AllowProtectedRenames already
			
			if( !String.IsNullOrEmpty( ConditionHash ) && File.Exists( Path ) ) {
				
				String hash = PackageUtility.GetMD5Hash( Path );
				if( !String.Equals( hash, ConditionHash, StringComparison.OrdinalIgnoreCase ) ) {
					
					Package.Log.Add( LogSeverity.Info, "Hash didn't match: " + Path );
					return;
				}
				
			}
			
			switch( Operation ) {
				case FileOperationType.Copy:
					
					PackageUtility.AddPfroEntry( SourceFile, Path );
					
					break;
				case FileOperationType.Delete:
					
					PackageUtility.AddPfroEntry( Path, null );
					
					break;
			}
			
		}
		
		private void Backup(Group backupGroup) {
			
			// if there exists a file at the destination RIGHT NOW, then back it up
				// and the restore operation will be to copy it from its backed-up location to the current location
			
			// otherwise, during restoration it should delete any file that is currently there (because that file would be the file installed by this operation originally)
			
			if( File.Exists( Path ) ) {
				
				// backup the file
				String backupDir = P.Combine( backupGroup.Package.RootDirectory.FullName, "Files" );
				
				String backupFn  = P.Combine( backupDir, P.GetFileName( Path ) );
				
				backupFn = PackageUtility.GetUnusedFileName( backupFn );
				
				File.Copy( Path, backupFn, true );
				
				// make an operation for it
				
				FileOperation op = new FileOperation(backupGroup.Package, backupGroup, backupFn, SpecifiedPath, FileOperationType.Copy);
				
				backupGroup.Operations.Add( op );
				
			} else {
				
				FileOperation del = new FileOperation(backupGroup.Package, backupGroup, "", SpecifiedPath, FileOperationType.Delete);
				
				backupGroup.Operations.Add( del );
				
			}
			
		}
		
		public override void Write(XmlElement parent) {
			
			CreateElement(parent, "file", "operation", Operation.ToString(), "src", SourceFile);
		}
		
		public override String OperationName {
			get { return "File"; }
		}
		
		public override Boolean Merge(Operation operation) {
			
			// TODO check if the Path and Condition is the same
			return false;
		}
	}
	
	public enum FileOperationType {
		None,
		Copy,
		Delete
	}
}
