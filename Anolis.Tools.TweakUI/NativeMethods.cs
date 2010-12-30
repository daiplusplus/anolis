using System;
using System.Runtime.InteropServices;

namespace Anolis.Tools.TweakUI {
	
	internal static class NativeMethods {
		
		[DllImport("user32.dll")]
		public static extern IntPtr GetDC(IntPtr hWnd);
		
		[DllImport("user32.dll")]
		public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
		
		[DllImport("gdi32.dll")]
		public static extern Int32 GetDeviceCaps(IntPtr hDC, DeviceCap cap);
		
	}
	
	public enum DeviceCap {
		/// <summary>Device driver version</summary>
		DriverVersion = 0,
		/// <summary>Device classification</summary>
		Technology = 2,
		/// <summary>Horizontal size in millimeters</summary>
		HorizontalSize = 4,
		/// <summary>Vertical size in millimeters</summary>
		VerticalSize = 6,
		/// <summary>Horizontal width in pixels</summary>
		HorizontalResolution = 8,
		/// <summary>Vertical height in pixels</summary>
		VerticalResolution = 10,
		/// <summary>Number of bits per pixel</summary>
		BitsPerPixel = 12,
		/// <summary>Number of planes</summary>
		NofPlanes = 14,
		/// <summary>Number of brushes the device has</summary>
		NofBrushes = 16,
		/// <summary>Number of pens the device has</summary>
		NofPens = 18,
		/// <summary>Number of markers the device has</summary>
		NofMarkers = 20,
		/// <summary>Number of fonts the device has</summary>
		NofFonts = 22,
		/// <summary>Number of colors the device supports</summary>
		NofColors = 24,
		/// <summary>Size required for device descriptor</summary>
		SizeOfDescriptor = 26,
		/// <summary>Curve capabilities</summary>
		CapsCurves = 28,
		/// <summary>Line capabilities</summary>
		CapsLines = 30,
		/// <summary>Polygonal capabilities</summary>
		CapsPolygons= 32,
		/// <summary>Text capabilities</summary>
		CapsText = 34,
		/// <summary>Clipping capabilities</summary>
		CapsClip = 36,
		/// <summary>Bitblt capabilities</summary>
		CapsRaster = 38,
		/// <summary>Shading and Blending caps</summary>
		CapsShadeBlend= 45,
		
		/// <summary>Length of the X leg</summary>
		AspectX = 40,
		/// <summary>Length of the Y leg</summary>
		AspectY = 42,
		/// <summary>Length of the hypotenuse</summary>
		AspectXY = 44,
		
		/// <summary>Logical pixels inch in X</summary>
		LogicalPpiX = 88,
		/// <summary>Logical pixels inch in Y</summary>
		LogicalPpiY = 90,
		/// <summary>Number of entries in physical palette</summary>
		PhysicalPaletteSize = 104,
		/// <summary>Number of reserved entries in palette</summary>
		PhysicalPaletteReserved = 106,
		/// <summary>Actual color resolution</summary>
		ColorResolution = 108,
		
		// Printing related DeviceCaps. These replace the appropriate Escapes
		
		/// <summary>Physical Width in device units</summary>
		PhysicalWidth = 110,
		/// <summary>Physical Height in device units</summary>
		PhysicalHeight = 111,
		/// <summary>Physical Printable Area x margin</summary>
		PhysicalOffsetX = 112,
		/// <summary>Physical Printable Area y margin</summary>
		PhysicalOffsetY = 113,
		/// <summary>Scaling factor x</summary>
		ScaleFactorX = 114,
		/// <summary>Scaling factor y</summary>
		ScaleFactorY = 115,
		/// <summary>Current vertical refresh rate of the display device (for displays only) in Hz</summary>
		VerticalRefresh = 116,
		/// <summary>Horizontal width of entire desktop in pixels</summary>
		DesktopSizeX = 117,
		/// <summary>Vertical height of entire desktop in pixels</summary>
		DesktopSizeY = 118,
		/// <summary>Preferred blt alignment</summary>
		BltAlignment = 119
    }
    
}
