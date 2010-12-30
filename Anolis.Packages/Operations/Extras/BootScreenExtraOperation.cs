using System;
using System.IO;
using System.Xml;

using Anolis.Packages.Utility;

namespace Anolis.Packages.Operations {
	
	public class BootScreenExtraOperation : ExtraOperation {
		
		public BootScreenExtraOperation(Group parent, XmlElement element) :  base(ExtraType.BootScreen, parent, element) {
		}
		
		public BootScreenExtraOperation(Group parent, String path) : base(ExtraType.BootScreen, parent, path) {
		}
		
		public override void Execute() {
			
			if( Files.Count == 0 ) return;
			
			////////////////////////////////////
			// Backup
			Backup( Package.ExecutionInfo.BackupGroup );
			
			String lastFile = null, lastSelectedFile = null;
			
			foreach(ExtraFile file in Files) {
				
				lastFile = file.FileName;
				if( file.IsSelected ) lastSelectedFile = file.FileName;
			}
			
			if( lastSelectedFile != null ) {
				
				InstallBootscreen( lastSelectedFile );
				
			} else {
				
				InstallBootscreen( lastFile );
			}
			
			
		}
		
		private void InstallBootscreen(String fileName) {
			
			if( String.IsNullOrEmpty( fileName ) ) {
				ExecuteBackup();
				return;
			}
			
			String dest = PackageUtility.ResolvePath(@"%windir%\Boot.bmp");
			
			String moved = PackageUtility.ReplaceFile( dest );
			if(moved != null) Package.Log.Add( LogSeverity.Warning, "File renamed: " + dest + " -> " + moved );
			
			File.Copy( fileName, dest );
			
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
