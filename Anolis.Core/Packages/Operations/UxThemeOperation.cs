using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;

using P = System.IO.Path;
using Env = Anolis.Core.Utility.Environment;
using W3b.TarLzma;

using Anolis.Core.Utility;
using Anolis.Core.Utility.BinaryPatch;

namespace Anolis.Core.Packages.Operations {
	
	public class UXThemeOperation : PatchOperation {
		
		public UXThemeOperation(Group parent, XmlElement element) : base(parent, element) {
			
			Path = @"%windir%\System32\uxtheme.dll";
		}
		
		public UXThemeOperation(Group parent) : base(parent, (String)null ) {
			
			Path = @"%windir%\System32\uxtheme.dll";
		}
		
		public override Boolean Merge(Operation operation) {
			throw new PackageException("There can only be one active UXThemeOperation in a package");
		}
		
		public override String OperationName {
			get { return "UxTheme"; }
		}
		
		public override Boolean SupportsCDImage {
			get { return true; }
		}
		
		protected override Boolean PatchFile(String fileName) {
			
			PatchFinder finder = UXThemePatchFinderFactory.Create( fileName );
			Patch patch = finder.GetPatchStatus();
			
			if( patch.CanPatch ) {
				
				patch.ApplyPatch();
				
				Miscellaneous.CorrectPEChecksum( fileName );
				
				return true;
				
			} else {
				
				File.Delete( fileName );
				
				String reason = "";
				foreach(PatchEntry entry in patch.Entries) reason += entry.Status + "; ";
				
				Package.Log.Add( LogSeverity.Warning, "Did not UxTheme Patch: " +  fileName + " because \"" + reason + "\", deleted working file" );
				
				return false;
			}
			
		}
		
		public override void Write(XmlElement parent) {
			CreateElement( parent, "uxtheme" );
		}
		
	}
}
