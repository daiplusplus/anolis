using System;
using System.IO;
using System.Xml;

using Microsoft.Win32;

using P = System.IO.Path;

namespace Anolis.Core.Packages.Operations {
	
	/// <summary>Creates an entry in the Add/Remove programs list and finalises the Backup directory</summary>
	public class UninstallationOperation : Operation {
		
		public UninstallationOperation(Package package, Group group, XmlElement operationElement) : base(package, group, operationElement) {
			
			DisplayIcon = operationElement.GetAttribute("displayIcon");
		}
		
		public UninstallationOperation(Package package, Group parent) : base(package, parent, (String)null) {
			
			
		}
		
		public String DisplayIcon { get; set; }
		
		public override void Execute() {
			
			if( !Package.ExecutionInfo.MakeBackup ) return;
			
			//////////////////////////////////
			// Copy files to the Backup directory
			
			String dispIconFn = null;
			
			if( !String.IsNullOrEmpty( DisplayIcon ) ) {
				
				FileInfo iconSrc = Package.RootDirectory.GetFile( DisplayIcon );
				if( iconSrc.Exists ) {
					dispIconFn = PackageUtility.GetUnusedFileName( P.Combine( Package.ExecutionInfo.BackupDirectory.FullName, "DisplayIcon.ico" ) );
					iconSrc.CopyTo( dispIconFn );
				} else {
					Package.Log.Add( Anolis.Core.Utility.LogSeverity.Error, "Could not find DisplayIcon: " + DisplayIcon );
				}
			}
			
			//////////////////////////////////
			// Add the registry key
			RegistryKey uninstallKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", RegistryKeyPermissionCheck.ReadWriteSubTree);
			
			String regKeyName = "Anolis" + Package.ExecutionInfo.BackupDirectory.Name;
			
			RegistryKey prodKey = uninstallKey.CreateSubKey( regKeyName );
			                              prodKey.SetValue("DisplayName"         , Package.Name, RegistryValueKind.String);
			if( dispIconFn != null )      prodKey.SetValue("DisplayIcon"         , dispIconFn, RegistryValueKind.String);
			                              prodKey.SetValue("UninstallString"     , Package.ExecutionInfo.BackupDirectory + "Uninstall.exe", RegistryValueKind.String);
			
			if( Package.Website != null ) prodKey.SetValue("HelpLink"       , Package.Website.OriginalString   , RegistryValueKind.String);
			                              prodKey.SetValue("Publisher"      , Package.Attribution              , RegistryValueKind.String);
			                              prodKey.SetValue("InstallDate"    , DateTime.Now.ToString("yyyyMMdd"), RegistryValueKind.String);
			
			// TODO: Get the Package version and split up into Major and Minor DWORDs
//			                              prodKey.SetValue("Version"        , "", RegistryValueKind.DWord);
//			                              prodKey.SetValue("VersionMajor"   , "", RegistryValueKind.DWord);
//			                              prodKey.SetValue("VersionMinor"   , "", RegistryValueKind.DWord);
			
			prodKey.Close();
			uninstallKey.Close();
			
		}
		
		public override void Write(XmlElement parent) {
			
			CreateElement(parent, "uninstallation", "displayIcon", DisplayIcon);
		}
		
		public override String OperationName {
			get { return "File"; }
		}
		
		public override Boolean Merge(Operation operation) {
			
			// there can only be one Uninstallation operation
			
			throw new PackageException("There can only be one active UninstallationOperation in a package");
			
		}
	}
	
}
