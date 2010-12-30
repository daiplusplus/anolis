using System;

using Microsoft.Win32;

using N    = System.Globalization.NumberStyles;
using Cult = System.Globalization.CultureInfo;

namespace Anolis.Tools.TweakUI {
	
	public static class ShellSettings {
		
		private static readonly IntPtr _hwndDesktop = new IntPtr( 0 );
		
		private static RegistryKey _windowMetrics;
		
		static ShellSettings() {
			
			_windowMetrics = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop\WindowMetrics");
		}
		
		private static Int32 P(Object o) {
			String s = o.ToString();
			
			return Int32.Parse( s, N.Integer, Cult.InvariantCulture );
		}
		
		private static Single TwipsPerPixelX {
			get {
				IntPtr desktopDC = NativeMethods.GetDC( _hwndDesktop );
				Int32 ppiX = NativeMethods.GetDeviceCaps( desktopDC, DeviceCap.LogicalPpiX );
				
				NativeMethods.ReleaseDC( _hwndDesktop, desktopDC );
				
				return 1440f / (Single)ppiX;
			}
		}
		
		private static Single TwipsPerPixelY {
			get {
				IntPtr desktopDC = NativeMethods.GetDC( _hwndDesktop );
				Int32 ppiX = NativeMethods.GetDeviceCaps( desktopDC, DeviceCap.LogicalPpiX );
				
				NativeMethods.ReleaseDC( _hwndDesktop, desktopDC );
				
				return 1440f / (Single)ppiX;
			}
		}
		
		public static Int32 IconLargeSize {
			get {
				return P( _windowMetrics.GetValue("Shell Icon Size", -1) ); 
			}
			set {
				_windowMetrics.SetValue("Shell Icon Size", value.ToString(Cult.InvariantCulture), RegistryValueKind.String);
			}
		}
		
		public static Int32 IconSmallSize {
			get {
				return P( _windowMetrics.GetValue("Shell Small Icon Size", -1) ); 
			}
			set {
				_windowMetrics.SetValue("Shell Small Icon Size", value.ToString(Cult.InvariantCulture), RegistryValueKind.String);
			}
		}
		
		public static Int32 IconSpacingHoriz {
			get {
				
				Int32 v = P( _windowMetrics.GetValue("IconSpacing", -1) );
				
				// v is specified in negative twips
				// yet dividing this by twips-per-pixel doesn't give you the pixel value
				// hmmm
				
				
				return v;
			}
			set {
				
			}
		}
		
//		public static Boolean IconTitleTransparentBG {
//			get {
//				
//			}
//			set {
//				
//			}
//		}
		
		public static Boolean IconTitleWrap {
			get {
				return (String)_windowMetrics.GetValue("IconTitleWrap") == "1";
			}
			set {
				_windowMetrics.SetValue("IconTitleWrap", value ? "1" : "0");
			}
		}
		
		public static void CommitChanges() {
			
			_windowMetrics.Close();
			_windowMetrics = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop\WindowMetrics");
		}
		
	}
}
