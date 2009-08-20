using System;
using System.Diagnostics;
using System.Xml;

using Microsoft.Win32;

using P = System.IO.Path;

namespace Anolis.Core.Packages.Operations {
	
	public class RegistryExtraOperation : ExtraOperation {
		
		public RegistryExtraOperation(Group parent, XmlElement element) :  base(ExtraType.Registry, parent, element) {
		}
		
		public override void Execute() {
			
			foreach(ExtraFile regfile in Files) {
				
				ProcessStartInfo startInfo = new ProcessStartInfo("reg IMPORT", regfile.FileName );
				Process p = Process.Start( startInfo );
				p.WaitForExit(250);
				
			}
			
		}
		
		public void Backup(Group backupGroup) {
			
			// TODO, but I don't think this can be implemented
			
		}
		
	}
}
