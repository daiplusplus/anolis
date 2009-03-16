using System;
using System.Management;
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
		
		public static void InitRestart() {
			
			NativeMethods.EnableProcessToken( NativeMethods.SePrivileges.SHUTDOWN );
			
			NativeMethods.ExitWindowsEx(NativeMethods.ExitWindows.Reboot, 0);
			
		}
		
		public static Boolean CreateSystemRestorePoint(String name, SystemRestoreType type) {
			
			// use WMI for now rather than the native API
			
			ManagementScope scope = new ManagementScope(@"\\localhost\root\default");
			ManagementPath  path  = new ManagementPath("SystemRestore");
			
			ObjectGetOptions getOpts = new ObjectGetOptions();
			
			ManagementClass clas = new ManagementClass(scope, path, getOpts);
			
			ManagementBaseObject inParams = clas.GetMethodParameters("CreateRestorePoint");
			inParams["Description"] = name;
			inParams["RestorePointType"] = (int)type;
			inParams["EventType"] = 100;
			
			ManagementBaseObject outParams = clas.InvokeMethod("CreateRestorePoint", inParams, null);
			Object ret = outParams.Properties["ReturnValue"];
			
			if(ret is Int32) {
				
				return (int)ret == 0;
			} else {
				
				return false;
			}
		}
		
		public enum SystemRestoreType {
			ApplicationInstall   = 0,
			ApplicationUninstall = 1,
			DeviceDriverInstall  = 10,
			ModifySettings       = 12,
			CancelledOperation   = 13
		}
		
		public enum SystemRestoreEventType {
			BeginSystemChange       = 100,
			EndSystemChange         = 101,
			BeginNestedSystemChange = 102,
			EndNestedSystemChange   = 103
		}
		
	}
}
