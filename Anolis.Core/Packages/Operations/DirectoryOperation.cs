using System;
using System.Xml;
using System.IO;
using Anolis.Core.Utility;

using P = System.IO.Path;

namespace Anolis.Core.Packages.Operations {
	
	public class DirectoryOperation : PathOperation {
		
		public DirectoryOperation(Group parent, XmlElement element) : base(parent, element) {
			
			SourceDirectory = element.GetAttribute("src");
			Overwrite       = element.GetAttribute("overwrite") == "true" || element.GetAttribute("overwrite") == "1";
		}
		
		public DirectoryOperation(Group parent, String path) : base(parent, path) {
		}
		
		public String  SourceDirectory { get; set; }
		public Boolean Overwrite       { get; set; }
		
		public override string OperationName {
			get { return "Directory"; }
		}
		
		public override void Execute() {
			
			String sourceDir = P.Combine( Package.RootDirectory.FullName, SourceDirectory );
			
			DirectoryInfo source = new DirectoryInfo( sourceDir );
			if( !source.Exists ) {
				Package.Log.Add( LogSeverity.Error, "Could not find source directory: " + sourceDir );
				return;
			}
			
			if( Directory.Exists( Path ) ) {
				
				if( Overwrite ) {
					
					Package.Log.Add( LogSeverity.Warning, "Overwriting: " + Path );
				} else {
					
					Package.Log.Add( LogSeverity.Error, "Will not overwrite: " + Path );
					return;
				}
				
			}
			
			source.CopyTo( Path );
			
		}
		
		public override Boolean Merge(Operation operation) {
			return false;
		}
		
		public override void Write(XmlElement parent) {
			
			CreateElement(parent, "directory",
				"src"      , SourceDirectory,
				"overwrite", Overwrite ? "true" : "false"
			);
			
		}
	}
}
