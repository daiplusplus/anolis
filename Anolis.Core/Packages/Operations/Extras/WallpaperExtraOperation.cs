using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using System.IO;
using System.Collections.ObjectModel;
using System.Text;

using Anolis.Core.Native;

using P = System.IO.Path;

using Rdf = Anolis.Core.Native.NativeMethods.RedrawFlags;

namespace Anolis.Core.Packages.Operations {
	
	public class WallpaperExtraOperation : ExtraOperation {
		
		public WallpaperExtraOperation(Package package, XmlElement element) : base(ExtraType.Wallpaper, package, element) {
		}
		
		public override void Execute() {
			
			if( Files.Count == 0 ) return;
			
			// copy (don't move) the files to C:\Windows\web\Wallpaper
			// if they already exist append a digit methinks
			
			// don't move because you might be installing from a HDD-based package. The files will be deleted when the package completes anyway
			
			String wallpaperdir = PackageUtility.ResolvePath(@"%windir%\Web\Wallpaper");
			
			String lastWallpaper = null;
			
			foreach(String source in Files) {
				
				String dest = P.Combine( wallpaperdir, P.GetFileName( source ) );
				
				File.Copy( source, dest );
				
				lastWallpaper = dest;
			}
			
			// set the bottommost as the current wallpaper
			// call SystemParametersInfo to set the current wallpaper apparently
			// and then call RedrawWindow to repaint the desktop
			
			if( lastWallpaper != null )
				SetWallpaper(ref lastWallpaper);
			
		}
		
		private void SetWallpaper(ref String wallpaperFilename) {
			
			// it needs to be a bitmap image on Windows XP and earlier
			// but on Vista it can also be a JPEG
			
			Bitmap bitmap = Image.FromFile( wallpaperFilename ) as Bitmap;
			if( bitmap == null ) return;
			
			Boolean isBmp = bitmap.RawFormat.Guid == ImageFormat.Bmp.Guid;
			Boolean isJpg = bitmap.RawFormat.Guid == ImageFormat.Jpeg.Guid;
			Boolean isVis = Environment.OSVersion.Version.Major >= 6;
			
			if( (!isBmp && !isVis) || (!isBmp && !isJpg && isVis) ) {
				// so it needs to be a bitmap
				
				String name = P.GetFileName( wallpaperFilename );
				while( File.Exists( name + ".bmp" ) ) {
					name += "_";
				}
				
				wallpaperFilename = name + ".bmp";
				
				bitmap.Save( wallpaperFilename, ImageFormat.Bmp );
				
			}
			
			bitmap.Dispose();
			
			// set the wallpaper
			NativeMethods.SystemParametersInfo(NativeMethods.SpiAction.SetDesktopWallpaper, 0, wallpaperFilename, NativeMethods.SpiUpdate.SendWinIniChange | NativeMethods.SpiUpdate.UpdateIniFile);
			
			// refresh the desktop
			Rdf flags = Rdf.Invalidate | Rdf.Erase | Rdf.AllChildren | Rdf.UpdateNow;
			NativeMethods.RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, flags );
			
		}
		
	}
	
}
