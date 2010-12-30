using System;
using System.IO;
using System.Xml;

using Microsoft.Win32;

using P = System.IO.Path;

namespace Anolis.Packages.Operations {
	
	public class RunOnceExtraOperation : ExtraOperation {
		
		public RunOnceExtraOperation(Group parent, XmlElement element) : base(ExtraType.RunOnce, parent, element ) {
		}
		
		public RunOnceExtraOperation(Group parent, String path) : base(ExtraType.RunOnce, parent, path ) {
		}
		
		public override void Execute() {
			
			RegistryKey runOnceKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce", RegistryKeyPermissionCheck.ReadWriteSubTree);
			
			Int32 i=1;
			
			foreach(ExtraFile file in Files) {
				String path = file.FileName;
				
				String vName = "AnolisRO" + (i++).ToStringInvariant();
				
				// copy the file to a temp directory
				String destFn = P.Combine( P.GetTempPath(), P.GetFileName( path ) );
				
				File.Copy( path, destFn, true );
				
				runOnceKey.SetValue( vName, destFn, RegistryValueKind.String );
			}
			
			runOnceKey.Close();
			
		}
		
		public override String OperationName {
			get { return "Run Once"; }
		}
		
	}
}
