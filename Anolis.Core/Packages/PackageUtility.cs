using System;
using Microsoft.Win32;

using Anolis.Core.Native;
using MoveFileFlags = Anolis.Core.Native.NativeMethods.MoveFileFlags;

namespace Anolis.Core.Packages {
	
	public static class PackageUtility {
		
		public static void AllowProtectedRenames() {
			
			RegistryKey sessionManager = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager", true);
				
			if( (int)sessionManager.GetValue("AllowProtectedRenames", 0) == 0 ) {
				
				sessionManager.SetValue("AllowProtectedRenames", 1, RegistryValueKind.DWord);
			}
			
			sessionManager.Close();
			
		}
		
		public static void AddPfroEntry(String fromFilename, String toFilename) {
			
			if( !NativeMethods.MoveFileEx( fromFilename, toFilename, MoveFileFlags.DelayUntilReboot | MoveFileFlags.ReplaceExisting ) ) {
				
				throw new PackageException("MoveFileEx failed: " + NativeMethods.GetLastErrorString() );
			}
			
		}
		
	}
}
