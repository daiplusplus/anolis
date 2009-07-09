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
		
		public ProgramExtraOperation(Package package, Group parent, XmlElement element) :  base(ExtraType.Program, package, parent, element) {
		}
		
		public override void Execute() {
			
			foreach(String file in Files) {
				
				String fileName, argument;
				
				GetFileName( file, out fileName, out argument );
				
				ProcessStartInfo startInfo = new ProcessStartInfo(fileName, argument);
				startInfo.CreateNoWindow = true;
				
				Process process = Process.Start( startInfo );
				process.WaitForExit(15 * 1000); // wait no more than 15 seconds
				
				if(!process.HasExited && !process.Responding) {
					// note that hung command-line programs will still be running
					process.Kill();
					Package.Log.Add( new LogItem(LogSeverity.Error, "Killed nonresponsive process: " + startInfo.FileName) );
				}
				
			}
			
		}
		
		private static void GetFileName(String path, out String fileName, out String args) {
			
			if( !path.StartsWith("\"") ) {
				fileName = path;
				args     = String.Empty;
				return;
			}
			
			Int32 lastQuote = path.LastIndexOf('"');
			if( lastQuote == -1 ) {
				
				fileName = path.Substring(1);
				args     = String.Empty;
				return;
			}
			
			fileName = path.Substring( 1, lastQuote );
			args     = path.Substring( lastQuote );
			
		}
		
	}
}
