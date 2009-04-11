using System;
using System.Runtime.InteropServices;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Native {
	
	internal static class NativeMethods {
		
#region Enum Resources
		
		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern Boolean EnumResourceTypes(IntPtr moduleHandle, EnumResTypeProc callback, IntPtr userParam);
		
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate Boolean EnumResTypeProc(IntPtr moduleHandle, IntPtr type, IntPtr userParam);
		
		
		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern Boolean EnumResourceNames(IntPtr moduleHandle, IntPtr type, EnumResNameProc callback, IntPtr userParam);
		
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate Boolean EnumResNameProc(IntPtr moduleHandle, IntPtr type, IntPtr name, IntPtr userParam);
		
		
		
		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern Boolean EnumResourceLanguages(IntPtr moduleHandle, IntPtr type, IntPtr name, EnumResLangProc callback, IntPtr userParam);
		
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate Boolean EnumResLangProc(IntPtr moduleHandle, IntPtr type, IntPtr name, UInt16 lang, IntPtr userParam);
		
#endregion

#region Enum Resources Ex (NT6 and greater)
		
		[Flags]
		public enum MuiResourceFlags {
			/// <summary>RESOURCE_ENUM_LN (0x0001)</summary>
			EnumLn        = 0x01,
			/// <summary>RESOURCE_ENUM_MUI (0x0002)</summary>
			EnumMui       = 0x02,
			/// <summary>RESOURCE_ENUM_MUI_SYSTEM (0x0004)</summary>
			EnumMuiSystem = 0x04,
			/// <summary>RESOURCE_UPDATE_LN (0x0010)</summary>
			UpdateLn      = 0x10,
			/// <summary>RESOURCE_UPDATE_MUI (0x0020)</summary>
			UpdateMui     = 0x20
		}
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean EnumResourceTypesEx(IntPtr hModule, EnumResTypeProc callback, IntPtr userParam, MuiResourceFlags flags, UInt16 langId);
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean EnumResourceNamesEx(IntPtr hModule, IntPtr type, EnumResNameProc callback, IntPtr userParam, MuiResourceFlags flags, UInt16 langId);
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean EnumResourceLanguagesEx(IntPtr hModule, IntPtr type, IntPtr name, EnumResLangProc callback, IntPtr userParam, MuiResourceFlags flags, UInt16 langId);
		
#endregion
		
#region Resource Accessing
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern IntPtr FindResourceEx(IntPtr moduleHandle, IntPtr type, IntPtr name, UInt16 language);
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern IntPtr LoadResource(IntPtr moduleHandle, IntPtr resInfo);
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern IntPtr LockResource(IntPtr resourceData);
		
		[DllImport("Kernel32.dll", EntryPoint="SizeofResource", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern Int32 SizeOfResource(IntPtr moduleHandle, IntPtr resInfo);
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean FreeResource(IntPtr resourceHandle);
		
#endregion
		
#region Resource Updating
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern IntPtr BeginUpdateResource(String filename, [param: MarshalAs(UnmanagedType.Bool)] Boolean deleteAllExistingResources);
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern Boolean UpdateResource(IntPtr updateHandle, IntPtr type, IntPtr name, UInt16 lang, IntPtr data, Int32 length);
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean EndUpdateResource(IntPtr updateHandle, [param: MarshalAs(UnmanagedType.Bool)] Boolean discardChanges);
		
#endregion
		
#region Specific Resource Types
		
		/*
		 * HICON CreateIconFromResourceEx(
	PBYTE pbIconBits,
    DWORD cbIconBits,
    BOOL fIcon,
    DWORD dwVersion,
    int cxDesired,
    int cyDesired,
    UINT uFlags
);*/
		[DllImport("User32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern IntPtr CreateIconFromResourceEx(IntPtr iconData, UInt32 sizeOfIconData, Boolean iconOrCursor, UInt32 version, Int32 desiredWidth, Int32 desiredHeight, IconFlags flags);
		
		public static IntPtr CreateIconFromResource(IntPtr iconData, UInt32 sizeOfIconData, Boolean iconOrCursor) {
			
			IntPtr retval = CreateIconFromResourceEx(iconData, sizeOfIconData, iconOrCursor, (uint)0x00030000, 0, 0, IconFlags.DefaultColor);
			
			if(retval == IntPtr.Zero )
				throw new Exception("CreateIconFromResourceEx failed: " + GetLastErrorString() );
			
			return retval;
			
		}
		
		/// <remarks>This is actually a subset of LoadImageFlags; but all the members of this enumeration are documentated to work with CreateIconFromResourceEx.</remarks>
		[Flags]
		public enum IconFlags {
			DefaultColor = 0x0000,
			DefaultSize  = 0x0040,
			Monochrome   = 0x0001,
			Shared       = 0x8000
		}
		
		// I don't recommend using these functions because they don't allow you to discriminate on language
		
		[DllImport("User32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern IntPtr LoadImage(IntPtr moduleHandle, IntPtr name, LoadImageType type, Int32 desiredWidth, Int32 desiredHeight, LoadImageFlags flags);
		
		public enum LoadImageType : uint {
			Bitmap = 0,
			Cursor = 2,
			Icon   = 1,
			EnhancedMetafile = 3
		}
		
		[Flags]
		public enum LoadImageFlags : uint {
			/// <summary>The default flag; it does nothing. All it means is "not LR_MONOCHROME".</summary>
			DefaultColor = 0,
			/// <summary>Loads the image in black and white.</summary>
			Monochrome   = 1,
			/// <summary></summary>
			Color        = 2,
			/// <summary></summary>
			CopyReturnOrg = 4,
			/// <summary></summary>
			CopyDeleteOrg = 8,
			/// <summary>Loads the stand-alone image from the file specified by lpszName (icon, cursor, or bitmap file).</summary>
			LoadFromFile = 0x10,
			/// <summary>Retrieves the color value of the first pixel in the image and replaces the corresponding entry in the color table with the default window color (COLOR_WINDOW). All pixels in the image that use that entry become the default window color. This value applies only to images that have corresponding color tables. Do not use this option if you are loading a bitmap with a color depth greater than 8bpp.</summary>
			LoadTransparent = 0x120,
			/// <summary>Uses the width or height specified by the system metric values for cursors or icons, if the cxDesired or cyDesired values are set to zero. If this flag is not specified and cxDesired and cyDesired are set to zero, the function uses the actual resource size. If the resource contains multiple images, the function uses the size of the first image.</summary>
			DefaultSize = 0x40,
			/// <summary></summary>
			VgaColor = 0x80,
			/// <summary>Searches the color table for the image and replaces the following shades of gray with the corresponding 3-D color. Do not use this option if you are loading a bitmap with a color depth greater than 8bpp.</summary>
			LoadMap3DColors = 0x1000,
			/// <summary>When the uType parameter specifies IMAGE_BITMAP, causes the function to return a DIB section bitmap rather than a compatible bitmap. This flag is useful for loading a bitmap without mapping it to the colors of the display device.</summary>
			CreateDibSection = 0x2000,
			/// <summary></summary>
			CopyFromResource = 0x4000,
			/// <summary>Shares the image handle if the image is loaded multiple times. If LR_SHARED is not set, a second call to LoadImage for the same resource will load the image again and return a different handle. When you use this flag, the system will destroy the resource when it is no longer needed. Do not use LR_SHARED for images that have non-standard sizes, that may change after loading, or that are loaded from a file. When loading a system icon or cursor, you must use LR_SHARED or the function will fail to load the resource. This function finds the first image in the cache with the requested resource name, regardless of the size requested.</summary>
			Shared = 0x8000
		}
		
		[DllImport("Gdi32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean DeleteObject(IntPtr objectHandle);
		
		[DllImport("User32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean DestroyCursor(IntPtr cursorHandle);
		
		[DllImport("User32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean DestroyIcon(IntPtr iconHandle);
		
#endregion
		
#region x64 Detection
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern void GetSystemInfo(out SystemInfo systemInfo);
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern void GetNativeSystemInfo(out SystemInfo systemInfo);
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean IsWow64Process(IntPtr hProcess, out Boolean isWow64Process);
		
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
			public const string ASSIGNPRIMARYTOKEN = "SeAssignPrimaryTokenPrivilege";
			public const string AUDIT = "SeAuditPrivilege";
			public const string BACKUP = "SeBackupPrivilege";
			public const string CHANGE_NOTIFY = "SeChangeNotifyPrivilege";
			public const string CREATE_GLOBAL = "SeCreateGlobalPrivilege";
			public const string CREATE_PAGEFILE = "SeCreatePagefilePrivilege";
			public const string CREATE_PERMANENT = "SeCreatePermanentPrivilege";
			public const string CREATE_SYMBOLIC_LINK = "SeCreateSymbolicLinkPrivilege";
			public const string CREATE_TOKEN = "SeCreateTokenPrivilege";
			public const string DEBUG = "SeDebugPrivilege";
			public const string ENABLE_DELEGATION = "SeEnableDelegationPrivilege";
			public const string IMPERSONATE = "SeImpersonatePrivilege";
			public const string INC_BAPRIORITY = "SeIncreaseBasePriorityPrivilege";
			public const string INCREAQUOTA = "SeIncreaseQuotaPrivilege";
			public const string INC_WORKING_SET = "SeIncreaseWorkingSetPrivilege";
			public const string LOAD_DRIVER = "SeLoadDriverPrivilege";
			public const string LOCK_MEMORY = "SeLockMemoryPrivilege";
			public const string MACHINE_ACCOUNT = "SeMachineAccountPrivilege";
			public const string MANAGE_VOLUME = "SeManageVolumePrivilege";
			public const string PROF_SINGLE_PROCESS = "SeProfileSingleProcessPrivilege";
			public const string RELABEL = "SeRelabelPrivilege";
			public const string REMOTE_SHUTDOWN = "SeRemoteShutdownPrivilege";
			public const string RESTORE = "SeRestorePrivilege";
			public const string SECURITY = "SeSecurityPrivilege";
			public const string SHUTDOWN = "SeShutdownPrivilege";
			public const string SYNC_AGENT = "SeSyncAgentPrivilege";
			public const string SYSTEM_ENVIRONMENT = "SeSystemEnvironmentPrivilege";
			public const string SYSTEM_PROFILE = "SeSystemProfilePrivilege";
			public const string SYSTEMTIME = "SeSystemtimePrivilege";
			public const string TAKE_OWNERSHIP = "SeTakeOwnershipPrivilege";
			public const string TCB = "SeTcbPrivilege";
			public const string TIME_ZONE = "SeTimeZonePrivilege";
			public const string TRUSTED_CREDMAN_ACCESS = "SeTrustedCredManAccessPrivilege";
			public const string UNDOCK = "SeUndockPrivilege";
			public const string UNSOLICITED_INPUT = "SeUnsolicitedInputPrivilege";
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
		
#region Loading Libraries
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern IntPtr GetModuleHandle(String modulePath);
		
		[Flags]
		public enum LoadLibraryFlags {
			/// <summary>If the module is a DLL the system does not call DllMain for process and thread initialization and termination. Also the system does not load additional modules referenced by the DLL.</summary>
			DontResolveDllReferences  = 0x001,
			/// <summary>Maps the file as if it were a data file. Nothing is done to execute or prepare to execute the mapped file.</summary>
			LoadLibraryAsDatafile     = 0x002,
			LoadWithAlteredSearchPath = 0x008,
			/// <summary>The system does not perform automatic trust comparisons on the DLL or its dependents.</summary>
			LoadIgnoreCodeAuthzLevel  = 0x010
		}
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern IntPtr LoadLibrary(String modulePath);
		
		/// <param name="fileHandle">Unused. Must be set to null (IntPtr.Zero).</param>
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern IntPtr LoadLibraryEx(String modulePath, IntPtr fileHandle, LoadLibraryFlags flags);
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean FreeLibrary(IntPtr moduleHandle);
		
#endregion
		
#region Errors
		
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
		
#region MoveFileEx
		
		[DllImport("kernel32.dll", SetLastError=true, CharSet=CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean MoveFileEx(String lpExistingFileName, String lpNewFileName, MoveFileFlags dwFlags);
		
		[Flags]
		public enum MoveFileFlags {
			ReplaceExisting    = 0x00000001,
			CopyAllowed        = 0x00000002,
			DelayUntilReboot   = 0x00000004,
			WriteThrough       = 0x00000008,
			CreateHardlink     = 0x00000010,
			FailIfNotTrackable = 0x00000020
		}
		
#endregion
		
#region System Restore
		
		// I'll do it the proper way later, but for now it's easier just to use WMI
/*		
		public struct RestorePointInfo {
		
		}
		
		public struct StateManagerStatus {
		
		}
		
		[DllImport("SrClient.dll", SetLastError=true, CharSet=CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean SRSetRestorePoint(RestorePointInfo restorePointSpec, out StateManagerStatus managerStatus); */
		
#endregion
		
	}
	
}
