using System;
using System.Xml;

namespace Anolis.Core.Packages {
	
	public class FileOperation : Operation {
		
		public FileOperation(Package package, XmlElement operationElement) : base(package, operationElement) {
			
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
			
			// the .Path setter resolves environment variables and sets the root to the package directory if non-rooted
			Path       = SpecifiedPath;
			SourceFile = PackageUtility.ResolvePath( operationElement.GetAttribute("src"), Package.RootDirectory.FullName );
		}
		
		public String SourceFile    { get; private set; }
		public String SpecifiedPath { get; private set; }
		
		public FileOperationType Operation { get; private set; }
		
		public override void Execute() {
			
			// the Package class sets AllowProtectedRenames already
			
			switch( Operation ) {
				case FileOperationType.Copy:
					
					PackageUtility.AddPfroEntry( SourceFile, Path );
					
					break;
				case FileOperationType.Delete:
					
					PackageUtility.AddPfroEntry( Path, null );
					
					break;
			}
			
		}
		
		protected override String OperationName {
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
