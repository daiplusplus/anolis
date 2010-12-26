using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Anolis.Resourcer {
	
	internal static class NativeMethods {
		
		/////////////////////////////////////////////
		// Animation Control
		
		[DllImport("user32.dll", CharSet=CharSet.Unicode, SetLastError=true, ThrowOnUnmappableChar=true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 message, UIntPtr wParam, IntPtr lParam);
		
		[DllImport("user32.dll", CharSet=CharSet.Unicode, SetLastError=true, ThrowOnUnmappableChar=true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 message, UIntPtr wParam, String lParam);
		
		[DllImport("comctl32.dll", CharSet=CharSet.Unicode, SetLastError=true, ThrowOnUnmappableChar=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean InitCommonControlsEx(InitCommonControlsExInfo icc);
		
		[StructLayout(LayoutKind.Sequential, Pack=1)]
		internal class InitCommonControlsExInfo {
			
			public UInt32 dwSize;
			public UInt32 dwIcc;
			
			public InitCommonControlsExInfo() {
				
				dwSize = 8;
			}
			
			public InitCommonControlsExInfo(UInt32 icc) : this() {
				dwIcc = icc;
			}
			
		}
		
		public static Boolean IsVistaOrLater {
			get {
				return Environment.OSVersion.Version.Major > 6;
			}
		}
		
		public static Int32 MakeInt32(Int16 lo, Int16 hi) {
			
			return ((lo & 0xffff) | (((short) hi) << 0x10));
		}
		
		/////////////////////////////////////////////
		// Dialogs
		
		[DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true, ThrowOnUnmappableChar=true)]
		public static extern IntPtr CreateDialogIndirectParam(IntPtr instance, IntPtr template, IntPtr parentWindow, DialogProc callback, IntPtr lParam);
		
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate Boolean DialogProc(IntPtr hWndDialog, UInt32 msg, UIntPtr wParam, IntPtr lParam);
		
		[DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true, ThrowOnUnmappableChar=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean EndDialog(IntPtr hDlg, IntPtr result);
		
		/////////////////////////////////////////////
		// Properties
		
		public static void ShowProperties(string fileName) {
			
			ShellExecuteInfo info = new ShellExecuteInfo();
			info.cbSize = Marshal.SizeOf(info);
			info.lpFile = fileName;
			info.nShow  = (int)ShowWindowType.Show;
			info.fMask  = 0xC; // SeeMaskInvokeIdList = 0xc
			info.lpVerb = "properties";
			
			ShellExecuteEx(ref info);
		}
		
		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		public static extern bool ShellExecuteEx(ref ShellExecuteInfo lpExecInfo);
		
	    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct ShellExecuteInfo {
			public int cbSize;
			public uint fMask;
			public IntPtr hwnd;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpVerb;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpFile;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpParameters;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpDirectory;
			public int nShow;
			public IntPtr hInstApp;
			public IntPtr lpIDList;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpClass;
			public IntPtr hkeyClass;
			public uint dwHotKey;
			public IntPtr hIcon;
			public IntPtr hProcess;
		}
		
		public enum ShowWindowType : int {
			Hide            = 0,
			ShowNormal      = 1,
			Normal          = 1,
			ShowMinimized   = 2,
			ShowMaximized   = 3,
			Maximize        = 3,
			ShowNoActivate  = 4,
			Show            = 5,
			Minimize        = 6,
			ShowMinNoActive = 7,
			ShowNa          = 8,
			Restore         = 9,
			ShowDefault     = 10,
			ForceMinimize   = 11,
			Max             = 11
		}
		
	}
}
