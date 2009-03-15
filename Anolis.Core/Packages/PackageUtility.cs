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
		
		public static void InitShutdown() {
			
			IntPtr processHandle = NativeMethods.GetCurrentProcess();
			
			NativeMethods.TokenDesiredAccess access = 
				NativeMethods.TokenDesiredAccess.TOKEN_ADJUST_PRIVILEGES |
				NativeMethods.TokenDesiredAccess.TOKEN_QUERY;
			
			IntPtr processToken;
			
			if(!NativeMethods.OpenProcessToken( processHandle, access, out processToken)) {
				
				throw new AnolisException("OpenProcessToken Failed: " + NativeMethods.GetLastErrorString() );
			}
			
			NativeMethods.Luid luid;
			
			if(!NativeMethods.LookupPrivilegeValue( null, NativeMethods.SePrivileges.SHUTDOWN, out luid)) {
				
				throw new AnolisException("LookupPrivilegeValue Failed: " + NativeMethods.GetLastErrorString() );
			}
			
			NativeMethods.TokenPrivileges tk = new NativeMethods.TokenPrivileges();
			tk.PrivilegeCount = 1;
			tk.Privileges = new NativeMethods.LuidAndAttributes[1];
			tk.Privileges[0].Attributes = (uint)NativeMethods.Privileges.Enabled;
			
			if(!NativeMethods.AdjustTokenPrivileges( processToken, false, ref tk, 0, IntPtr.Zero, IntPtr.Zero)) {
				
				throw new AnolisException("AdjustTokenPrivileges Failed: " + NativeMethods.GetLastErrorString() );
			}
			
			if(!NativeMethods.ExitWindowsEx(NativeMethods.ExitWindows.ShutDown, 0)) {
				
				throw new AnolisException("ExitWindowsEx Failed: " + NativeMethods.GetLastErrorString() );
			}
			
		}
		
	}
}
