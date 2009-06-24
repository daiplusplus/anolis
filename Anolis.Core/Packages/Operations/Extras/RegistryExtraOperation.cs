using System;
using System.Diagnostics;
using System.Xml;

using Microsoft.Win32;

using P = System.IO.Path;

namespace Anolis.Core.Packages.Operations {
	
	public class RegistryExtraOperation : ExtraOperation {
		
		public RegistryExtraOperation(Package package, Group parent, XmlElement element) :  base(ExtraType.Registry, package, parent, element) {
		}
		
		public override void Execute() {
			
			foreach(String regfile in Files) {
				
				ProcessStartInfo startInfo = new ProcessStartInfo("reg IMPORT", regfile);
				Process p = Process.Start( startInfo );
				p.WaitForExit(250);
				
			}
			
		}
		
		public override void Backup(Group backupGroup) {
			
			// TODO, but I don't think this can be implemented
			
		}
		
	}
}
