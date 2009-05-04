using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Diagnostics;

using Microsoft.Win32;

using P = System.IO.Path;

namespace Anolis.Core.Packages.Operations {
	
	public class ScreensaverExtraOperation : ExtraOperation {
		
		public ScreensaverExtraOperation(Package package, XmlElement element) :  base(ExtraType.Screensaver, package, element) {
		}
		
		public override void Execute() {
			
			if( Files.Count == 0 ) return;
			
			// copy all the screensaver files to the system directory; I can't see any other directories the Display panel searches
			
			String destDir = Environment.GetFolderPath(Environment.SpecialFolder.System);
			
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
