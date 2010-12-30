using System;
using System.IO;
using System.Xml;

using Microsoft.Win32;
using Anolis.Packages.Utility;

using P = System.IO.Path;

namespace Anolis.Packages.Operations {
	
	public class VisualStyleExtraOperation : ExtraOperation {
		
		public VisualStyleExtraOperation(Group parent, XmlElement element) :  base(ExtraType.VisualStyle, parent, element) {
		}
		
		public VisualStyleExtraOperation(Group parent, String path) :  base(ExtraType.VisualStyle, parent, path) {
		}
		
		private static readonly String _themesDir = PackageUtility.ResolvePath(@"%windir%\Resources\Themes");
		
		public override Boolean SupportsCDImage {
			get { return true; }
		}
		
		public override void Execute() {
			
			if( Files.Count == 0 ) return;
			
			Boolean reg = Package.ExecutionInfo.ExecutionMode == PackageExecutionMode.Regular;
			
			Backup( Package.ExecutionInfo.BackupGroup, Package.ExecutionInfo.BackupDirectory );
			
			String lastMsstyle  = null;
			String lastSelected = null;
			
			foreach(ExtraFile file in Files) {
				
				if( reg ) {
					
					String installedPath;
					if( InstallStyleRegular( file.FileName, out installedPath ) ) {
						
						lastMsstyle = installedPath;
						if( file.IsSelected ) lastSelected = installedPath;
					}
						
					
				} else {
					
					InstallStyleCDImage( file.FileName );
				}
				
			}
			
			if( reg ) {
				if( lastSelected != null ) {
					
					MakeActive( lastSelected );
					
				} else if( lastMsstyle != null ) {
					
					MakeActive( lastMsstyle );
				}
			}
			
		}
		
		private void Backup(Group backupGroup, DirectoryInfo backupDir) {
			
			if( backupGroup == null ) return;
			
			try {
				
				BackupThemeConfig(backupGroup, backupDir, @"HKEY_CURRENT_USER");
				
				if( Package.ExecutionInfo.ApplyToDefault ) {
					
					BackupThemeConfig(backupGroup, backupDir, @"HKEY_USERS\.DEFAULT");
				}
				
			} catch(AnolisException aex) {
				
				Package.Log.Add( new LogItem(LogSeverity.Error, aex, "Error whilst attempting to save theme config") );
			}
			
			// The PFRO deletions for the msstyles directory are made in InstallStyleRegular
			
		}
		
		private static void BackupThemeConfig(Group backupGroup, DirectoryInfo backupDir, String userKey) {
			
			String[] files = new String[] {
				ExportKey(userKey + @"\Software\Microsoft\Windows\CurrentVersion\ThemeManager", backupDir),
				ExportKey(userKey + @"\Control Panel\Colors"                                  , backupDir),
				ExportKey(userKey + @"\Control Panel\Desktop\WindowMetrics"                   , backupDir)
			};
			
			foreach(String file in files) {
				
				RegistryExtraOperation rop = new RegistryExtraOperation(backupGroup, file);
				backupGroup.Operations.Add( rop );
			}
			
		}
		
		private static String ExportKey(String keyPath, DirectoryInfo backupDir) {
			
			String destFileName = PackageUtility.GetUnusedFileName( backupDir.GetFile("Theme.reg").FullName );
			
			PackageUtility.RegistryExport( keyPath, destFileName );
			
			return destFileName;
		}
		
		private Boolean InstallStyleRegular(String packageMsstylesPath, out String installedMsstylesPath) {
			
			// copy the entire directory to the Themes directory basically
			
			DirectoryInfo source = new DirectoryInfo( P.GetDirectoryName( packageMsstylesPath ) );
			
			if( !source.Exists ) {
				Package.Log.Add( LogSeverity.Error, "Source directory doesn't exist: " + source.FullName );
				installedMsstylesPath = null;
				return false;
			}
			
			String destDirectoryName = PackageUtility.GetUnusedDirectoryName( P.Combine( _themesDir, source.Name ) );
			
			source.CopyTo( destDirectoryName );
			
			// Backup Uninstall
			if( Package.ExecutionInfo.BackupGroup != null ) {
				
				DirectoryOperation dop = new DirectoryOperation( Package.ExecutionInfo.BackupGroup, null, destDirectoryName, FileOperationType.Delete );
				
				Package.ExecutionInfo.BackupGroup.Operations.Add( dop );
				
			}
			
			installedMsstylesPath = P.Combine( destDirectoryName, P.GetFileName( packageMsstylesPath ) );
			
			return true;
		}
		
		private void InstallStyleCDImage(String packageMsstylesPath) {
			
			DirectoryInfo source = new DirectoryInfo( P.GetDirectoryName( packageMsstylesPath ) );
			
			if( !source.Exists ) {
				Package.Log.Add( LogSeverity.Error, "Source directory doesn't exist: " + source.FullName );
				return;
			}
			
			DirectoryInfo winDir = Package.ExecutionInfo.CDImage.OemWindows;
			DirectoryInfo dest = winDir.GetDirectory(@"Resources\Themes");
			if( !dest.Exists ) dest.Create();
			
			String destDirectoryName = PackageUtility.GetUnusedDirectoryName( P.Combine( dest.FullName, source.Name ) );
			
			source.CopyTo( destDirectoryName );
			
		}
		
		private void MakeActive(String msstylesPath) {
			
			// there's an API out there to actually change the visual style of the current session, but I can't find it
			// it's also slow and shows that "Please Wait" screen to the user. I think it's better if the user restarts anyway, it makes it a nicer surprise
			
			RegistryKey[] managers = new RegistryKey[2];
			managers[0] = Registry.CurrentUser.OpenSubKey(         @"Software\Microsoft\Windows\CurrentVersion\ThemeManager", true);
			managers[1] = Registry.Users      .OpenSubKey(@".DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", true);
			
			foreach(RegistryKey themeManager in managers) {
				
				themeManager.SetValue("WCreatedUser", "1", RegistryValueKind.String);
				themeManager.SetValue("LoadedBefore", "0", RegistryValueKind.String);
				themeManager.SetValue("ThemeActive" , "1", RegistryValueKind.String);
				themeManager.SetValue("ColorName"   , "NormalColor", RegistryValueKind.String);
				themeManager.SetValue("SizeName"    , "NormalSize", RegistryValueKind.String);
				themeManager.SetValue("DllName"     , msstylesPath, RegistryValueKind.String);
				
				themeManager.Close();
				
			}
			
		}
		
	}
}
