using System;
using System.IO;
using System.Xml;
using System.Diagnostics;

using Anolis.Packages.Utility;

using N = System.Globalization.NumberStyles;
using Cult = System.Globalization.CultureInfo;

namespace Anolis.Packages.Operations {
	
	public class ProgramOperation : PathOperation {
		
		public ProgramOperation(Group parent, XmlElement element) :  base(parent, element ) {
			
			Arguments = element.GetAttribute("arguments");
			
			String timeoutStr = element.GetAttribute("timeout");
			
			Int32 timeout;
			if( !Int32.TryParse( timeoutStr, N.Integer, Cult.InvariantCulture, out timeout ) ) {
				
				timeout = 15 * 1000;  // wait no more than 15 seconds by default
			}
			Timeout = timeout;
			
			Uninstall     = element.GetAttribute("uninstall");
			UninstallArgs = element.GetAttribute("uninstallArgs");
			
		}
		
		public ProgramOperation(Group parent, String path) : base(parent, path) {
		}
		
		public override void Execute() {
			
			Backup( Package.ExecutionInfo.BackupGroup );
			
			if(!File.Exists( Path ) ) {
				Package.Log.Add( LogSeverity.Error, "ProgramOperation: Couldn't find: " + Path );
				return;
			}
			
			ProcessStartInfo startInfo = new ProcessStartInfo(Path, Arguments);
			startInfo.WindowStyle    = ProcessWindowStyle.Hidden;
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
		
		private void Backup(Group backupGroup) {
			
			if( backupGroup == null ) return;
			
			ProgramOperation opPF = new ProgramOperation(backupGroup, Uninstall);
			opPF.Arguments = UninstallArgs;
			backupGroup.Operations.Add( opPF );
			
			if( Anolis.Core.Utility.Environment.IsX64  ) {
				
				String x86UninstString = null;
				
				if( Uninstall.StartsWith("%programfiles%", StringComparison.OrdinalIgnoreCase) ) {
					
					x86UninstString = "%programfiles(x86)%" + Uninstall.Substring("%programfiles%".Length);
					
				} else if( Uninstall.StartsWith("%commonprogramfiles%", StringComparison.OrdinalIgnoreCase) ) {
					
					x86UninstString = "%commonprogramfiles(x86)%" + Uninstall.Substring("%commonprogramfiles%".Length);
				}
				
				
				if( x86UninstString != null ) {
					
					ProgramOperation opPFx86 = new ProgramOperation(backupGroup, x86UninstString);
					opPFx86.Arguments = UninstallArgs;
					backupGroup.Operations.Add( opPFx86 );
				}
				
			}
			
			
		}
		
		public Int32  Timeout   { get; set; }
		public String Arguments { get; set; }
		public String Uninstall { get; set; }
		public String UninstallArgs { get; set; }
		
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
		
		public static ProgramOperation CreateRegistryOperation(Group parentGroup, String regOperation, String keyPath) {
			
			ProgramOperation op = new ProgramOperation(parentGroup, @"%windir%\system32\reg.exe");
			op.Arguments = regOperation + " \"" + keyPath + "\" /F";
			return op;
			
		}
		
	}
}
