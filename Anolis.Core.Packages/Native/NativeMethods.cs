using System;
using System.IO;
using System.Runtime.InteropServices;

using Microsoft.Win32.SafeHandles;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Packages.Native {
	
	internal static class NativeMethods {
		
#region MoveFileEx
		
		[DllImport("kernel32.dll", SetLastError=true, CharSet=CharSet.Unicode, ThrowOnUnmappableChar=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean MoveFileEx(String lpExistingFileName, String lpNewFileName, MoveFileFlags dwFlags);
		
		
		
#endregion
		
#region System Parameters
		
		// SpiAction enum moved to SystemParemeterOperation
		
		
		
		[DllImport("user32.dll", CharSet=CharSet.Unicode, ThrowOnUnmappableChar=true, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean SystemParametersInfo(UInt32 uAction, UInt32 uParam, String pvParam, SpiUpdate fuWinIni);
		
		[DllImport("user32.dll", CharSet=CharSet.Unicode, ThrowOnUnmappableChar=true, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean SystemParametersInfo(UInt32 uAction, UInt32 uParam, IntPtr pvParam, SpiUpdate fuWinIni);
		
		public static Boolean SystemParametersInfo(SpiAction action, UInt32 uParam, String pvParam) {
			
			UInt32    uAction = (UInt32)action;
			SpiUpdate update  = SpiUpdate.SendWinIniChange | SpiUpdate.UpdateIniFile;
			
			return SystemParametersInfo(uAction, uParam, pvParam, update);
		}
		
		public static Boolean SystemParametersInfo(SpiAction action, UInt32 uParam, IntPtr pvParam) {
			
			UInt32    uAction = (UInt32)action;
			SpiUpdate update  = SpiUpdate.SendWinIniChange | SpiUpdate.UpdateIniFile;
			
			return SystemParametersInfo(uAction, uParam, pvParam, update);
		}
		
#endregion
		
#region Subverting WRP/WFP/SFP
		
		/// <summary>Calls an undocumented function in Windows XP that disables Windows File Protection for a particular file. This no-longer exists in Windows Vista or later.</summary>
		/// <returns>A HRESULT. 0 = S_OK; 1 = Error (file is not protected by WFP. Check with SfcIsFileProtected first)</returns>
		/// <param name="param1">Unknown. Set to 0.</param>
		/// <param name="filename">The path to the file to exclude from WFP.</param>
		/// <param name="param2">Unknown. Set to -1.</param>
		[DllImport("sfc_os.dll", EntryPoint="#5", CharSet=CharSet.Unicode, SetLastError=true, ThrowOnUnmappableChar=true)]
		public static extern WfpResult SetSfcFileException(Int32 param1, String fileName, Int32 param2);
		
		
		[DllImport("sfc.dll", SetLastError=true, ThrowOnUnmappableChar=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean SfcIsFileProtected(String filename);
		
#endregion
		
#region System Restore
		
		// See http://www.codeproject.com/KB/cs/sysrestore.aspx
		[DllImport("srclient.dll", EntryPoint="SRSetRestorePointW")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean SetSystemRestorePoint(ref RestorePointInfo pRestorePtSpec, out StateManagerStatus pSMgrStatus);
		
#endregion
		
#region Redraw Window
		
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawFlags flags); 
		
#endregion
		
#region GetLastError
		
		// Copy 'n' paste from Anolis.Core.NativeMethods
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern Int32 FormatMessage(FormatMessageFlags flags, IntPtr source, Int32 messageId, Int32 languageId, out String buffer, Int32 size, IntPtr arguments);
		
		public enum LanguageId : ushort {
			Neutral = 0x00,
			Invariant = 0x7f
		}
		
		public enum SubLanguageId : ushort {
			Neutral = 0x00,
			Default = 0x01,
			SystemDefault = 0x02
		}
		
		public static Int16 MakeLangId(LanguageId primaryLanguage, SubLanguageId subLanguage) {
			UInt16 pl = (UInt16)primaryLanguage, sl = (UInt16)subLanguage;
			return (Int16)( pl << (ushort)10 | sl );
		}
		
		public static Int32 GetLastError() {
			return Marshal.GetLastWin32Error();
		}
		
		public static String GetLastErrorString() {
			return GetErrorString( GetLastError() );
		}
		
		[Flags]
		public enum FormatMessageFlags {
			AllocateBuffer = 0x0100,
			IgnoreInserts  = 0x0200,
			FromString     = 0x0400,
			FromHModule    = 0x0800,
			FromSystem     = 0x1000,
			ArgumentArray  = 0x2000
		}
		
		private static String GetErrorString(Int32 errorCode) {
			
			String failed = "Unable to FormatMessage({0}), cause: {1}";
			
			String message;
			
			Int16 languageId = MakeLangId( LanguageId.Neutral, SubLanguageId.Neutral );
			
			FormatMessageFlags flags = FormatMessageFlags.AllocateBuffer | FormatMessageFlags.FromSystem;
			IntPtr zero = IntPtr.Zero;
			
			Int32 length = FormatMessage(flags, zero, errorCode, languageId, out message, 0, zero);
			
			if(
				length         == 0    ||
				message        == null ||
				message.Length != length) return String.Format(Cult.InvariantCulture, failed, errorCode, GetLastErrorString());
			
			message = message.Trim('\r', '\n');
			
			return message;
			
		}
		
#endregion
		
#region Privileges
		
		/// <summary>An Luid is a 64-bit value guaranteed to be unique only on the system on which it was generated. The uniqueness of a locally unique identifier (Luid) is guaranteed only until the system is restarted.</summary>
		[StructLayout(LayoutKind.Sequential, Pack=1)]
		internal struct Luid {
			/// <summary>The low order part of the 64 bit value.</summary>
			public int LowPart;
			/// <summary>The high order part of the 64 bit value.</summary>
			public int HighPart;
		}
		
		/// <summary>The LuidAndAttributes structure represents a locally unique identifier (Luid) and its attributes.</summary>
		[StructLayout(LayoutKind.Sequential, Pack=1)]
		internal struct LuidAndAttributes {
			/// <summary>Specifies an Luid value.</summary>
			public Luid Luid;
			/// <summary>Specifies attributes of the Luid. This value contains up to 32 one-bit flags. Its meaning is dependent on the definition and use of the Luid.</summary>
			public int Attributes;
		}
		
		/// <summary>The TokenPrivileges structure contains information about a set of privileges for an access token.</summary>
		[StructLayout(LayoutKind.Sequential, Pack=1)]
		internal struct TokenPrivileges {
			/// <summary>Specifies the number of entries in the Privileges array.</summary>
			public int PrivilegeCount ;
			/// <summary>Specifies an array of LuidAndAttributes structures. Each structure contains the Luid and attributes of a privilege.</summary>
			public LuidAndAttributes Privileges;
		}
		
		public static class SePrivileges {
			public const string AssignPrimaryToken     = "SeAssignPrimaryTokenPrivilege";
			public const string Audit                  = "SeAuditPrivilege";
			public const string Backup                 = "SeBackupPrivilege";
			public const string ChangeNotify           = "SeChangeNotifyPrivilege";
			public const string CreateGlobal           = "SeCreateGlobalPrivilege";
			public const string CreatePagefile         = "SeCreatePagefilePrivilege";
			public const string CreatePermanent        = "SeCreatePermanentPrivilege";
			public const string CreateSymbolicLink     = "SeCreateSymbolicLinkPrivilege";
			public const string CreateToken            = "SeCreateTokenPrivilege";
			public const string Debug                  = "SeDebugPrivilege";
			public const string EnableDelegation       = "SeEnableDelegationPrivilege";
			public const string Impersonate            = "SeImpersonatePrivilege";
			public const string IncreaseBasePriority   = "SeIncreaseBasePriorityPrivilege";
			public const string IncreaseQuotaPrivilege = "SeIncreaseQuotaPrivilege";
			public const string IncreaseWorkingSet     = "SeIncreaseWorkingSetPrivilege";
			public const string LoadDriver             = "SeLoadDriverPrivilege";
			public const string LockMemory             = "SeLockMemoryPrivilege";
			public const string MachineAccount         = "SeMachineAccountPrivilege";
			public const string ManageVolume           = "SeManageVolumePrivilege";
			public const string ProfileSingleProcess   = "SeProfileSingleProcessPrivilege";
			public const string Relabel                = "SeRelabelPrivilege";
			public const string RemoteShutdown         = "SeRemoteShutdownPrivilege";
			public const string Restore                = "SeRestorePrivilege";
			public const string Security               = "SeSecurityPrivilege";
			public const string Shutdown               = "SeShutdownPrivilege";
			public const string SyncAgent              = "SeSyncAgentPrivilege";
			public const string SystemEnvironment      = "SeSystemEnvironmentPrivilege";
			public const string SystemProfile          = "SeSystemProfilePrivilege";
			public const string SystemTime             = "SeSystemtimePrivilege";
			public const string TakeOwnership          = "SeTakeOwnershipPrivilege";
			public const string Tcb                    = "SeTcbPrivilege";
			public const string TimeZone               = "SeTimeZonePrivilege";
			public const string TrustedCredManAccess   = "SeTrustedCredManAccessPrivilege";
			public const string Undock                 = "SeUndockPrivilege";
			public const string UnsolicitedInput       = "SeUnsolicitedInputPrivilege";
		}
		
		/// <summary>The OpenProcessToken function opens the access token associated with a process.</summary>
		/// <param name="ProcessHandle">Handle to the process whose access token is opened.</param>
		/// <param name="DesiredAccess">Specifies an access mask that specifies the requested types of access to the access token. These requested access types are compared with the token's discretionary access-control list (DACL) to determine which accesses are granted or denied.</param>
		/// <param name="TokenHandle">Pointer to a handle identifying the newly-opened access token when the function returns.</param>
		/// <returns>If the function succeeds, the return value is nonzero.<br></br><br>If the function fails, the return value is zero. To get extended error information, call Marshal.GetLastWin32Error.</br></returns>
		[DllImport("advapi32.dll", CharSet=CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean OpenProcessToken(IntPtr processHandle, Int32 desiredAccess, ref IntPtr tokenHandle);
		
		/// <summary>The LookupPrivilegeValue function retrieves the locally unique identifier (Luid) used on a specified system to locally represent the specified privilege name.</summary>
		/// <param name="lpSystemName">Pointer to a null-terminated string specifying the name of the system on which the privilege name is looked up. If a null string is specified, the function attempts to find the privilege name on the local system.</param>
		/// <param name="lpName">Pointer to a null-terminated string that specifies the name of the privilege, as defined in the Winnt.h header file. For example, this parameter could specify the constant SE_SECURITY_NAME, or its corresponding string, "SeSecurityPrivilege".</param>
		/// <param name="lpLuid">Pointer to a variable that receives the locally unique identifier by which the privilege is known on the system, specified by the lpSystemName parameter.</param>
		/// <returns>If the function succeeds, the return value is nonzero.<br></br><br>If the function fails, the return value is zero. To get extended error information, call Marshal.GetLastWin32Error.</br></returns>
		[DllImport("advapi32.dll", CharSet=CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean LookupPrivilegeValue(String systemName, String name, ref Luid lpLuid);
		
		
		/// <summary>The AdjustTokenPrivileges function enables or disables privileges in the specified access token. Enabling or disabling privileges in an access token requires TOKEN_ADJUST_PRIVILEGES access.</summary>
		/// <param name="tokenHandle">Handle to the access token that contains the privileges to be modified. The handle must have TOKEN_ADJUST_PRIVILEGES access to the token. If the PreviousState parameter is not NULL, the handle must also have TOKEN_QUERY access.</param>
		/// <param name="disableAllPrivileges">Specifies whether the function disables all of the token's privileges. If this value is TRUE, the function disables all privileges and ignores the NewState parameter. If it is FALSE, the function modifies privileges based on the information pointed to by the NewState parameter.</param>
		/// <param name="mewState">Pointer to a TokenPrivileges structure that specifies an array of privileges and their attributes. If the DisableAllPrivileges parameter is FALSE, AdjustTokenPrivileges enables or disables these privileges for the token. If you set the SE_PRIVILEGE_ENABLED attribute for a privilege, the function enables that privilege; otherwise, it disables the privilege. If DisableAllPrivileges is TRUE, the function ignores this parameter.</param>
		/// <param name="bufferLength">Specifies the size, in bytes, of the buffer pointed to by the PreviousState parameter. This parameter can be zero if the PreviousState parameter is NULL.</param>
		/// <param name="previousState">Pointer to a buffer that the function fills with a TokenPrivileges structure that contains the previous state of any privileges that the function modifies. This parameter can be NULL.</param>
		/// <param name="returnLength">Pointer to a variable that receives the required size, in bytes, of the buffer pointed to by the PreviousState parameter. This parameter can be NULL if PreviousState is NULL.</param>
		/// <returns>If the function succeeds, the return value is nonzero. To determine whether the function adjusted all of the specified privileges, call Marshal.GetLastWin32Error.</returns>
		[DllImport( "advapi32.dll", CharSet=CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean AdjustTokenPrivileges(IntPtr tokenHandle, Int32 disableAllPrivileges, ref TokenPrivileges newState, Int32 bufferLength, ref TokenPrivileges previousState, ref Int32 returnLength);
		
		public static void EnableProcessToken(String sePrivilege) {
			
			IntPtr tokenHandle = IntPtr.Zero;
			Luid privilegeLuid = new Luid();
			TokenPrivileges newPrivileges = new TokenPrivileges();
			TokenPrivileges tokenPrivileges;
			
			const int TOKEN_ADJUST_PRIVILEGES = 0x20;
			const int TOKEN_QUERY             = 0x08;
			const int SE_PRIVILEGE_ENABLED    = 0x02;
			
			if(!OpenProcessToken(System.Diagnostics.Process.GetCurrentProcess().Handle, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref tokenHandle))
				throw new AnolisException("OpenProcessToken failed: " + GetLastErrorString() );
			
			if(!LookupPrivilegeValue(null, sePrivilege, ref privilegeLuid))
				throw new AnolisException("LookupPrivilegeValue failed: " +  GetLastErrorString() );
			
			tokenPrivileges.PrivilegeCount        = 1;
			tokenPrivileges.Privileges.Attributes = SE_PRIVILEGE_ENABLED;
			tokenPrivileges.Privileges.Luid       = privilegeLuid;
			
			int size = 4;
			if(!AdjustTokenPrivileges(tokenHandle, 0, ref tokenPrivileges, 4 + (12 * tokenPrivileges.PrivilegeCount), ref newPrivileges, ref size))
				throw new AnolisException("AdjustTokenPrivileges failed: " + GetLastErrorString() );
			
		}
		
#endregion
		
#region Shutdown
		
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean ExitWindowsEx(ExitWindows uFlags, ShutdownReason dwReason);
		
		[Flags]
		public enum ExitWindows : uint {
			// ONE of the following five:
			LogOff = 0x00,
			ShutDown = 0x01,
			Reboot = 0x02,
			PowerOff = 0x08,
			RestartApps = 0x40,
			// plus AT MOST ONE of the following two:
			Force = 0x04,
			ForceIfHung = 0x10,
		}
		
		[Flags]
		public enum ShutdownReason : uint {
			MajorApplication = 0x00040000,
			MajorHardware = 0x00010000,
			MajorLegacyApi = 0x00070000,
			MajorOperatingSystem = 0x00020000,
			MajorOther = 0x00000000,
			MajorPower = 0x00060000,
			MajorSoftware = 0x00030000,
			MajorSystem = 0x00050000,
			
			MinorBlueScreen = 0x0000000F,
			MinorCordUnplugged = 0x0000000b,
			MinorDisk = 0x00000007,
			MinorEnvironment = 0x0000000c,
			MinorHardwareDriver = 0x0000000d,
			MinorHotfix = 0x00000011,
			MinorHung = 0x00000005,
			MinorInstallation = 0x00000002,
			MinorMaintenance = 0x00000001,
			MinorMMC = 0x00000019,
			MinorNetworkConnectivity = 0x00000014,
			MinorNetworkCard = 0x00000009,
			MinorOther = 0x00000000,
			MinorOtherDriver = 0x0000000e,
			MinorPowerSupply = 0x0000000a,
			MinorProcessor = 0x00000008,
			MinorReconfig = 0x00000004,
			MinorSecurity = 0x00000013,
			MinorSecurityFix = 0x00000012,
			MinorSecurityFixUninstall = 0x00000018,
			MinorServicePack = 0x00000010,
			MinorServicePackUninstall = 0x00000016,
			MinorTermSrv = 0x00000020,
			MinorUnstable = 0x00000006,
			MinorUpgrade = 0x00000003,
			MinorWMI = 0x00000015,
			
			FlagUserDefined = 0x40000000,
			FlagPlanned = 0x80000000
		}
		
#endregion
		
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError=true)]
		public static extern uint GetShortPathName([MarshalAs(UnmanagedType.LPTStr)]String lpszLongPath, [MarshalAs(UnmanagedType.LPTStr)]System.Text.StringBuilder lpszShortPath, uint cchBuffer);
		
	}
}
