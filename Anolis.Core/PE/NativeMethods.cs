using System;
using System.Runtime.InteropServices;

namespace Anolis.Core.PE {
	
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
		public static extern IntPtr CreateIconFromResourceEx(IntPtr iconData, UInt32 sizeOfIconData, Boolean iconOrCursor, UInt32 version, Int16 desiredWidth, Int16 desiredHeight, IconFlags flags);
		
		public static IntPtr CreateIconFromResource(IntPtr iconData, UInt32 sizeOfIconData, Boolean iconOrCursor) {
			
			IntPtr retval = CreateIconFromResourceEx(iconData, sizeOfIconData, iconOrCursor, (uint)0x00030000, 0, 0, IconFlags.DefaultColor);
			
			if(retval == IntPtr.Zero )
				throw new Exception("CreateIconFromResourceEx failed: " + GetLastErrorString() );
			
			return retval;
			
		}
		
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
		public static extern Boolean DeleteObject(IntPtr objectHandle);
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern Boolean DestroyCursor(IntPtr cursorHandle);
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern Boolean DestroyIcon(IntPtr iconHandle);
		
#endregion
		
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
		public static extern IntPtr FreeLibrary(IntPtr moduleHandle);
		
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
		
		
		public static String GetErrorString(Int32 errorCode) {
			
			String message;
			
			Int16 languageId = MakeLangId( LanguageId.Neutral, SubLanguageId.Neutral );
			
			FormatMessageFlags flags = FormatMessageFlags.AllocateBuffer | FormatMessageFlags.FromSystem;
			IntPtr zero = IntPtr.Zero;
			
			Int32 length = FormatMessage(flags, zero, errorCode, languageId, out message, 0, zero);
			if(message.Length != length) return null; // TODO: Throw exception
			
			message = message.Trim('\r', '\n');
			
			return message;
			
		}
		
	}
	
}
