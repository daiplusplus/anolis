using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Anolis.Tools.PngToArgb {
	
	public class Program {
		
		public static void Main(String[] args) {
			
			String dir;
			
			if( args.Length >0 ) {
				dir = args[0];
			} else {
				Console.WriteLine("Path?");
				dir = Console.ReadLine();
			}
			
			ProcessDirectory( new DirectoryInfo( dir ) );
			
		}
		
		private static void ProcessDirectory(DirectoryInfo dir) {
			
			FileInfo[] pngFiles = dir.GetFiles("*.png");
			foreach(FileInfo pngFile in pngFiles) {
				
				Bitmap png = (Bitmap)Image.FromFile( pngFile.FullName );
				if( png.PixelFormat == PixelFormat.Format32bppArgb ) {
					
					png.Save( GetBmpFileName( pngFile.FullName ), ImageFormat.Bmp );
					
					Console.WriteLine("Done: " + pngFile.FullName);
				} else {
					Console.WriteLine("Skipped: " + pngFile.FullName);
				}
				
				
				
			}
			
			foreach(DirectoryInfo child in dir.GetDirectories()) {
				
				ProcessDirectory( child );
			}
			
		}
		
		private static String GetBmpFileName(String pngName) {
			
			return pngName.Substring(0, pngName.Length - 3) + "bmp";
		}
		
	}
	
}
