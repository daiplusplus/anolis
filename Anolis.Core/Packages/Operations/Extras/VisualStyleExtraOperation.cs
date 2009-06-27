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
		
		public VisualStyleExtraOperation(Package package, Group parent, XmlElement element) :  base(ExtraType.VisualStyle, package, parent, element) {
		}
		
		public VisualStyleExtraOperation(Package package, Group parent, String path) :  base(ExtraType.VisualStyle, package, parent, path) {
		}
		
		private static readonly String _themesDir = PackageUtility.ResolvePath(@"%windir%\Resources\Themes");
		
		public override void Execute() {
			
			if( Files.Count == 0 ) return;
			
			Backup( Package.ExecutionInfo.BackupGroup );
			
			String lastMsstyles = null;
			
			foreach(String styleDir in Files) {
				
				lastMsstyles = InstallStyle( styleDir );
				
			}
			
			MakeActive( lastMsstyles );
			
		}
		
		private void Backup(Group backupGroup) {
			
			if( backupGroup == null ) return;
			
			String keyPath = @"Software\Microsoft\Windows\CurrentVersion\ThemeManager";
			
			MakeRegOp(backupGroup, keyPath, "WCreatedUser");
			MakeRegOp(backupGroup, keyPath, "LoadedBefore");
			MakeRegOp(backupGroup, keyPath, "ThemeActive");
			MakeRegOp(backupGroup, keyPath, "ColorName");
			MakeRegOp(backupGroup, keyPath, "SizeName");
			MakeRegOp(backupGroup, keyPath, "DllName");
			
		}
		
		private String InstallStyle(String packageMsstylesPath) {
			
			// copy the entire directory to the Themes directory basically
			DirectoryInfo source = new DirectoryInfo( P.GetDirectoryName( packageMsstylesPath ) );
			String dest = P.Combine( _themesDir, source.Name );
			
			source.CopyTo( dest );
			
			return P.Combine( dest, P.GetFileName( packageMsstylesPath ) );
		}
		
		private void MakeActive(String msstylesPath) {
			
			// there's an API out there to actually change the visual style of the current session, but I can't find it
			// it's also slow and shows that "Please Wait" screen to the user. I think it's better if the user restarts anyway, it makes it a nicer surprise
			
			// TODO: Also set the .DEFAULT visual style?
			
			RegistryKey themeManager = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\ThemeManager", true);
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
