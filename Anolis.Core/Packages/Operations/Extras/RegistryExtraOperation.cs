using System;
using System.Diagnostics;
using System.Xml;

using Microsoft.Win32;

using P = System.IO.Path;

namespace Anolis.Core.Packages.Operations {
	
	public class RegistryExtraOperation : ExtraOperation {
		
		public RegistryExtraOperation(Package package, XmlElement element) :  base(ExtraType.Registry, package, element) {
		}
		
		public override void Execute() {
			
			foreach(String regfile in Files) {
				
				ProcessStartInfo startInfo = new ProcessStartInfo("regedit", regfile);
				Process p = Process.Start( startInfo );
				p.WaitForExit(250);
				
			}
			
		}
	}
}
