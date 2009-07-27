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
using N = System.Globalization.NumberStyles;
using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Packages.Operations {
	
	public class ProgramOperation : PathOperation {
		
		public ProgramOperation(Group parent, XmlElement element) :  base(parent, element ) {
			
			Arguments = element.GetAttribute("arguments");
			
			String timeoutStr = element.GetAttribute("timeout");
			
			Int32 timeout;
			if( !Int32.TryParse( timeoutStr, N.Integer, Cult.InvariantCulture, out timeout ) ) {
				
				timeout = 15 * 1000;  // wait no more than 15 seconds by default
			}
			Timeout = timeout;
			
		}
		
		public ProgramOperation(Group parent, String path) : base(parent, path) {
		}
		
		public override void Execute() {
			
			ProcessStartInfo startInfo = new ProcessStartInfo(Path, Arguments);
			startInfo.CreateNoWindow = true;
			
			Process process = Process.Start( startInfo );
			
			if( Timeout == 0 ) return;
			
			process.WaitForExit( Math.Abs( Timeout ) );
			
			if( process.HasExited ) return;
			
			if( !process.Responding && Timeout < 0) {
				// note that hung command-line programs will still be running
				process.Kill();
				Package.Log.Add( new LogItem(LogSeverity.Error, "Killed nonresponsive process: " + startInfo.FileName) );
			}
			
		}
		
		public Int32  Timeout   { get; set; }
		public String Arguments { get; set; }
		
		public override Boolean Merge(Operation operation) {
			return false;
		}
		
		public override void Write(XmlElement parent) {
			
			CreateElement(parent, "program",
				"path"     , Path,
				"arguments", Arguments,
				"timeout"  , Timeout.ToString(Cult.InvariantCulture)
			);
		}
		
		public override String OperationName {
			get { return "Program"; }
		}
		
/*		private static void GetFileName(String path, out String fileName, out String args) {
			
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
			
		} */
		
	}
}
