/* 
  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF 
  ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
  THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A 
  PARTICULAR PURPOSE. 
  
    This is sample code and is freely distributable. 
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Anolis.Core.Utility.Quantization {
	
	/// <summary>Struct that defines a 32 bpp colour</summary>
	/// <remarks>This struct is used to read data from a 32 bits per pixel image
	/// in memory, and is ordered in this manner as this is the way that
	/// the data is layed out in memory</remarks>
	[StructLayout(LayoutKind.Explicit)]
	public struct Color32 {
		/// <summary>Holds the blue component of the colour</summary>
		[FieldOffset(0)]
		public byte Blue;
		/// <summary>Holds the green component of the colour</summary>
		[FieldOffset(1)]
		public byte Green;
		/// <summary>Holds the red component of the colour</summary>
		[FieldOffset(2)]
		public byte Red;
		/// <summary>Holds the alpha component of the colour</summary>
		[FieldOffset(3)]
		public byte Alpha;
		
		/// <summary>Permits the color32 to be treated as an int32</summary>
		[FieldOffset(0)]
		public int ARGB;
		
		/// <summary>Return the color for this Color32 object</summary>
		public Color Color {
			get { return Color.FromArgb(Alpha, Red, Green, Blue); }
		}
	}
}
