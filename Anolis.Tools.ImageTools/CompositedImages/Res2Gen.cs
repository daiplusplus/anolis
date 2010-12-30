#define MAKE_IMAGES

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Anolis.Core.Utility;

namespace Anolis.Tools.CompositedImages {
	
	public class RunOnceProgram {
		
		public static void RunOnceMain(String[] args) {
			
			// This program generates a new Res.csv based on the old Res.csv and bitmap info
			
			DirectoryInfo preGenPath = new DirectoryInfo(@"D:\Users\David\My Documents\Visual Studio Projects\Anolis\_resources\xpize\_source\Pre-Generator Bitmaps\MCE\");
			
			List<ImageItem> images = new List<ImageItem>();
			
			using(StreamReader rdr = new StreamReader(@"D:\Users\David\My Documents\Visual Studio Projects\Anolis\_resources\xpize\_source\Generator\Res.csv")) {
				
				String line;
				while( (line = rdr.ReadLine()) != null ) {
					
					if( line.StartsWith("#") ) continue;
					
					String[] fields = line.Split(',');
					
					ImageItem img = new ImageItem();
					img.PEPath   = fields[0];
					img.ResName  = fields[1];
					img.SizeType = Program.GetSize( fields[2] );
					
					if( fields[3].Length > 0 ) {
						
						img.ForegroundImagePath = fields[3] + ".png";
					} else {
						
						img.ForegroundImagePath = Path.Combine( img.PEPath, img.ResName ) + ".png";
					}
					
					img.ForegroundCoords = new Point( Int32.Parse( fields[4] ), Int32.Parse( fields[5] ) );
					
					// now load the actual Bitmap from the pre-Generator directory
					
					String pathToOriginalBmp = Path.Combine( Path.Combine( preGenPath.FullName, img.PEPath ), img.ResName ) + ".bmp";
					using(Bitmap orig = Bitmap.FromFile( pathToOriginalBmp ) as Bitmap) {
					
						img.FinalDimensions = orig.Size;
					}
					
					images.Add( img );
				}
			}
			
			/////////////////////////////////////////////////////////////////////////
			
			using(StreamWriter wtr = new StreamWriter(@"D:\Users\David\My Documents\Visual Studio Projects\Anolis\_resources\xpize\_source\Generator\Res2.csv", false)) {
				
				foreach(ImageItem item in images) {
					
					String line =
						item.PEPath + "," +
						item.ResName + "," +
						item.SizeType.ToString() + "," +
						item.FinalDimensions.Width + "," +
						item.FinalDimensions.Height + "," +
						item.ForegroundImagePath + "," +
						item.ForegroundCoords.X + "," +
						item.ForegroundCoords.Y;
					
					wtr.WriteLine( line );
					
					Console.WriteLine( line );
				}
				
			}
			
		}
		
	}
	
	

	
}
