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
	
	public class VisualStyleExtraOperation : ExtraOperation {
		
		public VisualStyleExtraOperation(Package package, XmlElement element) :  base(ExtraType.VisualStyle, package, element) {
		}
		
		public override void Execute() {
			throw new NotImplementedException();
		}
	}
	
	public class BootScreenExtraOperation : ExtraOperation {
		
		public BootScreenExtraOperation(Package package, XmlElement element) :  base(ExtraType.BootScreen, package, element) {
		}
		
		public override void Execute() {
			
			// the bootscreen will be the last file listed
			
			// http://technet.microsoft.com/en-gb/sysinternals/bb963892.aspx
			
			if( Files.Count == 0 ) return;
			
			String bootcfgExe = PackageUtility.ResolvePath(@"%windir%\system32\bootcfg.exe", null);
			if( !File.Exists( bootcfgExe ) ) {
				
				Package.Log.Add( new LogItem(LogSeverity.Error, "Could not find bootcfg.exe; BootScreenExtraOperation aborted") );
				return;
			}
			
			String file = Files[ Files.Count - 1 ];
			String dest = PackageUtility.ResolvePath(@"%windir%\Boot.bmp", null);
			
			File.Copy( file, dest );
			
			Process.Start( bootcfgExe, @"/RAW ""/NOGUIBOOT /BOOTLOGO"" /A /ID 1" );
			// HACK: hardcoding /ID 1 means that I'm assuming this XP distro is the first OS in the list
			
			
		}
	}
	
	public class CursorSetExtraOperation : ExtraOperation {
		
		public CursorSetExtraOperation(Package package, XmlElement element) :  base(ExtraType.CursorScheme, package, element) {
		}
		
		public override void Execute() {
			throw new NotImplementedException();
		}
	}
	
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
				
				File.Copy( source, dest );
				
				lastSaver = dest;
			}
			
			if( lastSaver != null ) SetScreensaver( lastSaver );
			
		}
		
		private void SetScreensaver(String screensaverFilename) {
			
			RegistryKey desktopKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
			
			desktopKey.SetValue("SCRNSAVE.EXE", screensaverFilename, RegistryValueKind.String);
			
			desktopKey.Close();
			
		}
		
	}
	
	public class CustomExtraOperation : ExtraOperation {
		
		public CustomExtraOperation(Package package, XmlElement element) :  base(ExtraType.Custom, package, element) {
		}
		
		public override void Execute() {
			throw new NotImplementedException();
		}
	}
	
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
	
	public class FileTypeOperation : Operation {
		
		public FileTypeOperation(Package package, XmlElement element) :  base(package, element) {
		}
		
		protected override string OperationName {
			get { throw new NotImplementedException(); }
		}

		public override void Execute() {
			throw new NotImplementedException();
		}

		public override bool Merge(Operation operation) {
			throw new NotImplementedException();
		}
	}
	
	public class RegistryOperation : Operation {
		
		public RegistryOperation(Package package, XmlElement element) :  base(package, element) {
		}
		
		protected override string OperationName {
			get { throw new NotImplementedException(); }
		}

		public override void Execute() {
			throw new NotImplementedException();
		}

		public override bool Merge(Operation operation) {
			throw new NotImplementedException();
		}
	}
	
}
