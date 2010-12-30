using System;
using System.Xml;

using Anolis.Packages.Utility;

namespace Anolis.Packages.Operations {
	
	public class RegistryExtraOperation : ExtraOperation {
		
		public RegistryExtraOperation(Group parent, XmlElement element) :  base(ExtraType.Registry, parent, element) {
		}
		
		public RegistryExtraOperation(Group parent, String path) :  base(ExtraType.Registry, parent, path) {
		}
		
		public override void Execute() {
			
			foreach(ExtraFile regfile in Files) {
				
				PackageUtility.RegistryImport( regfile.FileName );
			}
			
		}
		
		public void Backup(Group backupGroup) {
			
			// TODO, but I don't think this can be implemented
			
		}
		
	}
}
