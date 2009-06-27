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
	
	public class ScreensaverExtraOperation : ExtraOperation {
		
		public ScreensaverExtraOperation(Package package, Group parent, XmlElement element) :  base(ExtraType.Screensaver, package, parent, element) {
		}
		
		public ScreensaverExtraOperation(Package package, Group parent, String path) :  base(ExtraType.Screensaver, package, parent, path) {
		}
		
		public override void Execute() {
			
			if( Files.Count == 0 ) return;
			
			Backup( Package.ExecutionInfo.BackupGroup );
			
			// copy all the screensaver files to the system directory; I can't see any other directories the Display panel searches
			
			String destDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.System);
			
			String lastSaver = null;
			
			foreach(String source in Files) {
				
				String dest = P.Combine( destDir, P.GetFileName( source ) );
				
				String moved = PackageUtility.ReplaceFile( dest );
				if(moved != null) Package.Log.Add( LogSeverity.Warning, "File renamed: " + dest + " -> " + moved );
				
				File.Copy( source, dest );
				
				lastSaver = dest;
			}
			
			if( lastSaver != null ) SetScreensaver( lastSaver );
			
		}
		
		public void Backup(Group backupGroup) {
			
			if( backupGroup == null ) return;
			
			// get the current screensaver
			
			String value  = (String)Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "SCRNSAVE.EXE"    , null);
			String active = (String)Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveActive", "1");
			
			if( value != null ) {
				
				RegistryOperation restoreOp1 = new RegistryOperation(backupGroup.Package, backupGroup);
				restoreOp1.RegKey   = @"HKEY_CURRENT_USER\Control Panel\Desktop";
				restoreOp1.RegName  = "SCRNSAVE.EXE";
				restoreOp1.RegValue = value;
				restoreOp1.RegKind  = RegistryValueKind.String;
				
				backupGroup.Operations.Add( restoreOp1 );
				
				RegistryOperation restoreOp2 = new RegistryOperation(backupGroup.Package, backupGroup);
				restoreOp2.RegKey   = @"HKEY_CURRENT_USER\Control Panel\Desktop";
				restoreOp2.RegName  = "ScreenSaveActive";
				restoreOp2.RegValue = active;
				restoreOp2.RegKind  = RegistryValueKind.String;
				
				backupGroup.Operations.Add( restoreOp2 );
			}
			
		}
		
		private void SetScreensaver(String screensaverFilename) {
			
			// if the saver is located under system32 you don't need the full path
			String name = P.GetFileName( screensaverFilename );
			
			RegistryKey desktopKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
			desktopKey.SetValue("ScreenSaveActive", "1" , RegistryValueKind.String);
			desktopKey.SetValue("SCRNSAVE.EXE"    , name, RegistryValueKind.String);
			desktopKey.Close();
			
			RegistryKey logonKey = Registry.Users.OpenSubKey(".DEFAULT").OpenSubKey(@"Control Panel\Desktop", true);
			logonKey.SetValue("ScreenSaveActive", "1" , RegistryValueKind.String);
			logonKey.SetValue("SCRNSAVE.EXE"    , name, RegistryValueKind.String);
			logonKey.Close();
			
		}
		
	}
}
