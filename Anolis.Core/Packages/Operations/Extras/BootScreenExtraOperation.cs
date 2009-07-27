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
		
		public BootScreenExtraOperation(Group parent, XmlElement element) :  base(ExtraType.BootScreen, parent, element) {
		}
		
		public BootScreenExtraOperation(Group parent, String path) : base(ExtraType.BootScreen, parent, path) {
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
			
			////////////////////////////////////////////
			// Add the switches
			
			BootIni boot = BootIni.FromDefaultBootIni();
			if( boot == null ) {
				Package.Log.Add( LogSeverity.Error, "Could not load C:\\Boot.ini" );
				return;
			}
			
			boot.DefaultOS.Switches.AddSwitch( "/NOGUIBOOT" );
			boot.DefaultOS.Switches.AddSwitch( "/BOOTLOGO" );
			
			try {
				boot.Save();
			} catch(AnolisException ex) {
				Package.Log.Add( new LogItem(LogSeverity.Error, ex, "Couldn't save Boot.ini") );
			}
		}
		
		private void ExecuteBackup() {
			
			// just delete the custom boot.bmp and go back to the regular switches
			
			String bootBmp = PackageUtility.ResolvePath(@"%windir%\Boot.bmp");
			
			if( File.Exists(bootBmp) ) File.Delete( bootBmp );
			
			////////////////////////////////////////////
			// Remove the switches
			
			BootIni boot = BootIni.FromDefaultBootIni();
			if( boot == null ) {
				Package.Log.Add( LogSeverity.Error, "Could not load Boot.ini" );
				return;
			}
			
			boot.DefaultOS.Switches.Remove("/NOGUIBOOT");
			boot.DefaultOS.Switches.Remove( "/BOOTLOGO" );
			
			try {
				boot.Save();
			} catch(AnolisException ex) {
				Package.Log.Add( new LogItem(LogSeverity.Error, ex, "Couldn't save Boot.ini") );
			}
		}
		
		private void Backup(Group backupGroup) {
			
			if( backupGroup == null ) return;
			
			BootScreenExtraOperation op = new BootScreenExtraOperation(backupGroup, (String)null ); // null path, so it will be deleted when executed
			
			backupGroup.Operations.Add( op );
			
		}
		
	}
}
