/* 
  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF 
  ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
  THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A 
  PARTICULAR PURPOSE. 
  
    This is sample code and is freely distributable. 
*/
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.ObjectModel;

namespace Anolis.Core.Utility.Quantization {
	
	/// <summary>Quantize using an Octree</summary>
	public unsafe class OctreeQuantizer : Quantizer {
		
		/// <summary>Stores the tree</summary>
		private Octree _octree;
		
		/// <summary>Maximum allowed color depth</summary>
		private int    _maxColors;
		
		/// <summary>Construct the octree quantizer</summary>
		/// <remarks>The Octree quantizer is a two pass algorithm. The initial pass sets up the octree,
		/// the second pass quantizes a color based on the nodes in the tree</remarks>
		/// <param name="maxColors">The maximum number of colors to return</param>
		/// <param name="maxColorBits">The number of significant bits</param>
		public OctreeQuantizer(int maxColors, int maxColorBits) : base(false) {
			
			if(maxColors < 1 || maxColors > 255)
				throw new ArgumentOutOfRangeException("maxColors", maxColors, "The number of colors must be between 1 and 255 (inclusive)");
				
			if((maxColorBits < 1) | (maxColorBits > 8))
				throw new ArgumentOutOfRangeException("maxColorBits", maxColorBits, "maxColorBits must be between 1 and 8 (inclusive)");
			
			// Construct the octree
			_octree = new Octree(maxColorBits);
			
			_maxColors = maxColors;
		}
		
		/// <summary>Process the pixel in the first pass of the algorithm</summary>
		/// <param name="pixel">The pixel to quantize</param>
		/// <remarks>This function need only be overridden if your quantize algorithm needs two passes, such as an Octree quantizer.</remarks>
		protected override void InitialQuantizePixel(Color32* pixel) {
			// Add the color to the octree
			_octree.AddColor(pixel);
		}
		
		/// <summary>Override this to process the pixel in the second pass of the algorithm</summary>
		/// <param name="pixel">The pixel to quantize</param>
		/// <returns>The quantized value</returns>
		protected override byte QuantizePixel(Color32* pixel) {
			
			byte paletteIndex = (byte)_maxColors;	// The color at [_maxColors] is set to transparent
			
			// Get the palette index if this non-transparent
			if(pixel->Alpha > 0)
				paletteIndex = (byte)_octree.GetPaletteIndex(pixel);
			
			return paletteIndex;
		}
		
		/// <summary>Retrieve the palette for the quantized image</summary>
		/// <param name="original">Any old palette, this is overrwritten</param>
		/// <returns>The new color palette</returns>
		protected override ColorPalette GetPalette(ColorPalette palette) {
			
			// First off convert the octree to _maxColors colors
			Collection<Color> nPalette = _octree.Palletize(_maxColors - 1);
			
			// Then convert the palette based on those colors
			for(int index = 0;index < nPalette.Count;index++)
				palette.Entries[index] = (Color)nPalette[index];
			
			// Add the transparent color
			palette.Entries[_maxColors] = Color.FromArgb(0, 0, 0, 0);
			
			return palette;
		}
		
	}
}
