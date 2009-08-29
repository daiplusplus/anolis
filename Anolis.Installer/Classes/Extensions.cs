using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System.Runtime.CompilerServices {
	
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class ExtensionAttribute : Attribute {
		
		public ExtensionAttribute() {
		}
		
	}
	
}

namespace Anolis.Installer {
	
	public static class Extensions {
		
		public static void BringToForeground(this Form form) {
			
			NativeMethods.SetForegroundWindow( form.Handle );
		}
		
	}
	
	internal static class NativeMethods {
		
		[DllImport("User32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean SetForegroundWindow(IntPtr hWnd);
		
	}
	
	
}
