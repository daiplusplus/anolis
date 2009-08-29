using System;
using System.IO;
using System.Reflection;
using System.Xml;

using Microsoft.Win32;

using P = System.IO.Path;
using Cult = System.Globalization.CultureInfo;


namespace Anolis.Core.Packages.Operations {
	
	/// <summary>Creates an entry in the Add/Remove programs list and finalises the Backup directory</summary>
	public class UninstallationOperation : Operation {
		
		public UninstallationOperation(Group parent, XmlElement operationElement) : base(parent, operationElement) {
			
			DisplayIcon = operationElement.GetAttribute("displayIcon");
			
			if( String.IsNullOrEmpty( operationElement.GetAttribute("hidden") ) ) Hidden = true;
		}
		
		public UninstallationOperation(Group parent) : base(parent) {
			Hidden = true;
		}
		
		public String DisplayIcon { get; set; }
		
		public override void Execute() {
			
			if( !Package.ExecutionInfo.MakeBackup ) return;
			
			//////////////////////////////////
			// Copy files to the Backup directory
			
			String dispIconFn = null;
			
			String uninstallerPath = P.Combine( Package.ExecutionInfo.BackupDirectory.FullName, "Uninstall.exe" );
			
			String thisInstallerPath = Assembly.GetExecutingAssembly().Location;
			File.Copy( thisInstallerPath, uninstallerPath, true );
			
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
			                              prodKey.SetValue("UninstallString"     , uninstallerPath + " /uninstall:package.xml", RegistryValueKind.String);
			
			if( Package.Website != null ) prodKey.SetValue("HelpLink"       , Package.Website.OriginalString   , RegistryValueKind.String);
			                              prodKey.SetValue("Publisher"      , Package.Attribution              , RegistryValueKind.String);
			                              prodKey.SetValue("InstallDate"    , DateTime.Now.ToString("yyyyMMdd"), RegistryValueKind.String);
			
			                              prodKey.SetValue("DisplayVersion" , Package.Version.ToString(), RegistryValueKind.String );
			                              prodKey.SetValue("Version"        , Package.Version.Major, RegistryValueKind.DWord); // I've no idea what the convention for the 'Version' value is
			                              prodKey.SetValue("VersionMajor"   , Package.Version.Major, RegistryValueKind.DWord);
			                              prodKey.SetValue("VersionMinor"   , Package.Version.Minor, RegistryValueKind.DWord);
			
			
			String fullKeyPath = uninstallKey.Name + '\\' + regKeyName;
			Backup( Package.ExecutionInfo.BackupGroup, fullKeyPath );
			
			prodKey.Close();
			uninstallKey.Close();
		}
		
		private void Backup(Group backupGroup, String regKeyPath) {
			
			if( backupGroup == null ) return;
			
			// basically, run reg.exe such that it deletes the uninstallation registry key
			
			ProgramOperation op = ProgramOperation.CreateRegistryOperation(backupGroup, "DELETE", regKeyPath);
			
			backupGroup.Operations.Add( op );
		}
		
		public override void Write(XmlElement parent) {
			
			CreateElement(parent, "uninstallation", "displayIcon", DisplayIcon);
		}
		
		public override String OperationName {
			get { return "Uninstaller"; }
		}
		
		public override Boolean Merge(Operation operation) {
			
			// there can only be one!
			throw new PackageException("There can only be one active UninstallationOperation in a package");
			
		}
	}
	
}
