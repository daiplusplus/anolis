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
	
	public class RunOnceExtraOperation : ExtraOperation {
		
		public RunOnceExtraOperation(Group parent, XmlElement element) : base(ExtraType.RunOnce, parent, element ) {
		}
		
		public RunOnceExtraOperation(Group parent, String path) : base(ExtraType.RunOnce, parent, path ) {
		}
		
		public override void Execute() {
			
			RegistryKey runOnceKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce", RegistryKeyPermissionCheck.ReadWriteSubTree);
			
			Int32 i=1;
			
			foreach(String path in Files) {
				
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
