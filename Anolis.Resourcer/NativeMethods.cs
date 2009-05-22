using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Anolis.Resourcer {
	
	internal static class NativeMethods {
		
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
		
		public static Int32 MakeLong(Int16 lo, Int16 hi) {
			
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
		
	}
}
