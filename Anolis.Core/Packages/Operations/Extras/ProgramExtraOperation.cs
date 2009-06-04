using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Diagnostics;

using Microsoft.Win32;
using Anolis.Core.Utility;

using P = System.IO.Path;

namespace Anolis.Core.Packages.Operations {
	
	public class ProgramExtraOperation : ExtraOperation {
		
		public ProgramExtraOperation(Package package, XmlElement element) :  base(ExtraType.Program, package, element) {
		}
		
		public override void Execute() {
			
			foreach(String file in Files) {
				
				// TODO: Separate arguments from filename
				String filename = file;
				String argument = "";
				
				ProcessStartInfo processInfo = new ProcessStartInfo(filename, argument);
				
				Process process = Process.Start( processInfo );
				process.WaitForExit(10 * 1000); // wait no more than 10 seconds
				
				if(!process.HasExited && !process.Responding) {
					// note that hung command-line programs will still be running
					process.Kill();
					Package.Log.Add( new LogItem(LogSeverity.Error, "Killed nonresponsive process: " + processInfo.FileName) );
				}
				
			}
			
		}
	}
}
