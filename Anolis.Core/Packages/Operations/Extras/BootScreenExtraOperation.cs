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
	
	public class BootScreenExtraOperation : ExtraOperation {
		
		public BootScreenExtraOperation(Package package, Group parent, XmlElement element) :  base(ExtraType.BootScreen, package, parent, element) {
		}
		
		public BootScreenExtraOperation(Package package, Group parent, String path) : base(ExtraType.BootScreen, package, parent, path) {
		}
		
		public override void Execute() {
			
			// the bootscreen will be the last file listed
			
			// http://technet.microsoft.com/en-gb/sysinternals/bb963892.aspx
			
			if( Files.Count == 0 ) return;
			
			String file = Files[ Files.Count - 1 ];
			
			if( String.IsNullOrEmpty( file ) ) {
				ExecuteBackup();
				return;
			}
			
			String dest = PackageUtility.ResolvePath(@"%windir%\Boot.bmp");
			
			String moved = PackageUtility.ReplaceFile( dest );
			if(moved != null) Package.Log.Add( LogSeverity.Warning, "File renamed: " + dest + " -> " + moved );
			
			File.Copy( file, dest );
			
			ExecuteBootcfg(Package,  @"/RAW ""/NOGUIBOOT /BOOTLOGO"" /A /ID 1");
		}
		
		private void ExecuteBackup() {
			
			// just delete the custom boot.bmp and go back to the regular switches
			
			String bootBmp = PackageUtility.ResolvePath(@"%windir%\Boot.bmp");
			
			if( File.Exists(bootBmp) ) File.Delete( bootBmp );
			
			ExecuteBootcfg(Package, @"/raw ""/NOexecute=optin /fastdetect"" /ID 1"); // I assume it's okay to set these default switches, but I can see it breaking... gah
			
		}
		
		public override void Backup(Group backupGroup) {
			
			BootScreenExtraOperation op = new BootScreenExtraOperation(backupGroup.Package, backupGroup, ""); // empty path, so it will be deleted when executed
			
			backupGroup.Operations.Add( op );
			
		}
		
		private static void ExecuteBootcfg(Package package, String args) {
			
			String bootcfgExe = PackageUtility.ResolvePath(@"%windir%\system32\bootcfg.exe");
			if( !File.Exists( bootcfgExe ) ) {
				
				package.Log.Add(LogSeverity.Error, "Could not find bootcfg.exe; BootScreenExtraOperation aborted");
				return;
			}
			
			// HACK: hardcoding /ID 1 means that I'm assuming this XP distro is the first OS in the list
			// a better solution would be to manually parse and process boot.ini; do that in future
			ProcessStartInfo processInfo = new ProcessStartInfo(bootcfgExe, args );
			processInfo.CreateNoWindow = true;
			
			Process.Start( processInfo );
			
			// TODO: In future, use the BootIni class described below
			
		}
		
		private class BootIni {
			
			private List<String>  _lines;
			private List<OSEntry> _oses;
			
			private OSEntry _default;
			
			public BootIni(String bootIniPath) {
				
				_lines = new List<String>();
				_oses  = new List<OSEntry>();
				
				using(FileStream fs = new FileStream(bootIniPath, FileMode.Open, FileAccess.Read))
				using(StreamReader rdr = new StreamReader(fs)) {
					
					Boolean inOS = false;
					
					String line;
					while( (line = rdr.ReadLine()) != null ) {
						
						_lines.Add( line );
						
						if( line == "[operating systems]" ) inOS = true;
						
						else if( inOS ) {
							
							OSEntry os = new OSEntry( line );
							
							_oses.Add( os );
							
						}
						
					}
					
				}
				
				// get the default line
				foreach(String line in _lines) {
					if( line.StartsWith("default=", StringComparison.OrdinalIgnoreCase) ) {
						
						// get the default OS
						String partitionPath = line.Substring("default=".Length);
						foreach(OSEntry entry in _oses) {
							if( entry.PartitionPath == partitionPath ) {
								_default = entry;
								break;
							}
						}
						
						break;
						
					}
				}
				
				if( _default == null ) throw new AnolisException("Couldn't find default OS");
				
			}
			
			private class OSEntry {
				
				public String   PartitionPath;
				public String   OSName;
				public List<String> Switches;
				
				public OSEntry(String line) {
					
					String allSwitches;
					
					using(StringReader rdr = new StringReader(line)) {
						
						State state = State.InPartitionPath;
						
						StringBuilder sb = new StringBuilder();
						
						Int32 nc;
						while( (nc = rdr.Read()) != -1 ) {
							
							Char c = (Char)nc;
							
							sb.Append( c );
							
							if( state == State.InPartitionPath && c == '=' ) {
								
								PartitionPath = sb.ToString().LeftFR(1);
								sb.Length = 0;
								continue;
							}
							
							if(c == '"' ) {
								
								if( state == State.InPartitionPath ) {
									
									state = State.InName;
									
								} else if( state == State.InName ) {
									
									state = State.InSwitches;
									
									OSName = sb.ToString().LeftFR(1);
									
									sb.Length = 0;
									continue;
								}
								
							}
							
						}
						
						allSwitches = sb.ToString();
						
					}//using
					
					using(StringReader rdr = new StringReader(allSwitches)) {
						
						StringBuilder ss = new StringBuilder();
						
						Boolean inString = false;
						
						Int32 nc;
						while( (nc = rdr.Read()) != -1 ) {
							
							Char c = (Char)nc;
							
							ss.Append( c );
							
							if(c == '"' ) {
								inString = !inString;
							}
							
							if(c == ' ' && !inString) {
								
								Switches.Add( ss.ToString() );
								ss.Length = 0;
							}
							
						}
						
						if( ss.Length > 0 ) Switches.Add( ss.ToString() );
						
					}
					
				}//cntr
				
				public override String ToString() {
					
					String str = PartitionPath + "=\"" + OSName + "\" ";
					
					foreach(String sw in Switches) {
						
						str += sw + " ";
					}
					
					return str;
					
				}
				
				private enum State {
					InPartitionPath,
					InName,
					InSwitches
				}
				
			}//class
			
		}//class
		
	}
}
