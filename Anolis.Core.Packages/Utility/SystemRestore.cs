using System;
using System.Collections.Generic;
using System.Text;

using Anolis.Core;
using Anolis.Packages.Native;

namespace Anolis.Packages.Utility {
	
	public static class SystemRestore {
		
		public static Boolean IsSystemRestoreAvailable() {
			
			if( !IsSystemRestoreAvailableByOS() ) return false;
			
			return IsSystemRestoreAvailableByFiles();
			
		}
		
		private static Boolean IsSystemRestoreAvailableByOS() {
			
			Version ver = Environment.OSVersion.Version;
			
			if( Environment.OSVersion.Platform == PlatformID.Win32NT ) {
				
				// Supported in Vista and later
				if( ver.Major >= 6 ) return true;
				
				// Supported in XP and later, but not 2000
				if( ver.Major == 5 && ver.Minor >= 1 ) {
					
					return true;
				}
				
			} else if( Environment.OSVersion.Platform == PlatformID.Win32Windows ) {
				
				// Also supported in *spit!* WindowsME
				return ver.Major == 4 && ver.Minor == 90;
				
			}
			
			return false;
		}
		
		private static Boolean IsSystemRestoreAvailableByFiles() {
			
			Version ver = Environment.OSVersion.Version;
			if( ver.Major <= 6 ) {
				
				// stupid nLite users often remove the system restore DLL files
				
				String srClientPath = System.Environment.ExpandEnvironmentVariables( @"%windir%\system32\srclient.dll" );
				
				return System.IO.File.Exists( srClientPath );
				
			}
			
			return true;
			
		}
		
		private static Boolean _isCreating;
		
		public static Boolean IsCreatingRestorePoint {
			get { return _isCreating; }
		}
		
		/// <returns>The sequence number</returns>
		public static Int64 CreateRestorePoint(String description, SystemRestoreType type) {
			
			if( !IsSystemRestoreAvailable() ) throw new NotSupportedException("System Restore is not supported");
			
			if( description == null      ) throw new ArgumentNullException("description");
			if( description.Length > 256 ) throw new ArgumentException("description cannot exceed 256 characters");
			
			_isCreating = true;
			
			RestorePointInfo   point  = new RestorePointInfo();
			StateManagerStatus status = new StateManagerStatus();
			
			point.dwEventType      = (Int32)SystemRestoreEventType.BeginSystemChange;
			point.dwRestorePtType  = (Int32)type;
			point.llSequenceNumber = 0;
			point.szDescription    = description;
			
			NativeMethods.SetSystemRestorePoint(ref point, out status);
			
			if( status.nStatus != 0 ) {
				// throw?
			}
			
			return status.llSequenceNumber;
		}
		
		public static void EndRestorePoint(Int64 sequenceNumber) {
			
			if( !IsSystemRestoreAvailable() ) throw new NotSupportedException("System Restore is not supported");
			
			RestorePointInfo   point  = new RestorePointInfo();
			StateManagerStatus status = new StateManagerStatus();
			
			point.dwEventType      = (Int32)SystemRestoreEventType.EndSystemChange;
			point.llSequenceNumber = sequenceNumber;
			
			NativeMethods.SetSystemRestorePoint(ref point, out status);
			
			if( status.nStatus != 0 ) {
				// throw?
			}
			
			_isCreating = false;
			
		}
		
		public static void CancelRestorePoint(Int64 sequenceNumber) {
			
			if( !IsSystemRestoreAvailable() ) throw new NotSupportedException("System Restore is not supported");
			
			RestorePointInfo   point  = new RestorePointInfo();
			StateManagerStatus status = new StateManagerStatus();
			
			point.dwEventType      = (Int32)SystemRestoreEventType.EndSystemChange;
			point.dwRestorePtType  = (Int32)SystemRestoreType.ApplicationCancelled;
			point.llSequenceNumber = sequenceNumber;
			
			NativeMethods.SetSystemRestorePoint(ref point, out status);
			
			if( status.nStatus != 0 ) {
				// throw?
			}
			
		}
		
	}

	[Serializable]
	public class SystemRestoreException : AnolisException {
		
		public SystemRestoreException() {
		}
		
		public SystemRestoreException(String message) : base(message) {
		}
		
		public SystemRestoreException(String message, Exception inner) : base(message, inner) {
		}
		
		protected SystemRestoreException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) {
		}
	}
	
	public enum SystemRestoreType {
		/// <summary>Installing a new application</summary>
		ApplicationInstall   = 0,
		/// <summary>An application has been uninstalled</summary>
		ApplicationUninstall = 1,
		/// <summary>An application has had features added or removed</summary>
		ApplicationModify    = 12,
		/// <summary>An application needs to delete the restore point it created</summary>
		ApplicationCancelled = 13,
		
		SystemRestore        = 6,
		SystemCheckpoint     = 7,
		
		DeviceDriverInstall  = 10,
		FirstRun             = 11,
		BackupRecovery       = 14
	}
	
	public enum SystemRestoreEventType {
		BeginSystemChange       = 100,
		EndSystemChange         = 101,
		BeginNestedSystemChange = 102,
		EndNestedSystemChange   = 103
	}
	
}
