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
	
	public class BootScreenExtraOperation : ExtraOperation {
		
		public BootScreenExtraOperation(Package package, XmlElement element) :  base(ExtraType.BootScreen, package, element) {
		}
		
		public override void Execute() {
			
			// the bootscreen will be the last file listed
			
			// http://technet.microsoft.com/en-gb/sysinternals/bb963892.aspx
			
			if( Files.Count == 0 ) return;
			
			String bootcfgExe = PackageUtility.ResolvePath(@"%windir%\system32\bootcfg.exe");
			if( !File.Exists( bootcfgExe ) ) {
				
				Package.Log.Add( new LogItem(LogSeverity.Error, "Could not find bootcfg.exe; BootScreenExtraOperation aborted") );
				return;
			}
			
			String file = Files[ Files.Count - 1 ];
			String dest = PackageUtility.ResolvePath(@"%windir%\Boot.bmp");
			
			File.Copy( file, dest );
			
			// HACK: hardcoding /ID 1 means that I'm assuming this XP distro is the first OS in the list
			ProcessStartInfo processInfo = new ProcessStartInfo(bootcfgExe, @"/RAW ""/NOGUIBOOT /BOOTLOGO"" /A /ID 1" );
			processInfo.CreateNoWindow = true;
			
			Process.Start( processInfo );
			
			
			
		}
	}
}
