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
			
			////////////////////////////////////
			// Backup
			Backup( Package.ExecutionInfo.BackupGroup );
			
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
		
		private void Backup(Group backupGroup) {
			
			if( backupGroup == null ) return;
			
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
		
	}
}
