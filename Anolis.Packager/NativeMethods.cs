using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System.Runtime.CompilerServices {
	
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class ExtensionAttribute : Attribute {
		
		public ExtensionAttribute() {
		}
		
	}
	
}

namespace Anolis.Packager {
	
	
	
	internal static class NativeMethods {
		
		[DllImport("User32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean SetForegroundWindow(IntPtr hWnd);
		
	}
	
	public static class Extensions {
		
		public static void BringToForeground(this Form form) {
			
			NativeMethods.SetForegroundWindow( form.Handle );
		}
		
	}
}
