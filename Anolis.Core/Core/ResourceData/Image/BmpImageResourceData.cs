using System;
using System.Drawing;
using System.Text;

using Stream = System.IO.Stream;
using Anolis.Core.Utility;

namespace Anolis.Core.Data {
	
	public sealed class BmpImageResourceData : ImageResourceData {
		
		private FileDib _dib;
		
		// HACK: When I implement the IResourceDataFactory patterns I should simplify the constructor here
		
		private BmpImageResourceData(FileDib dib, Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
			_dib = dib;
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] rawData, out ResourceData typed) {
			
			// check if the data is of the right format before working with it
			
			FileDib dib = new FileDib( rawData );
			
			Bitmap bmp;
			
			if(!dib.TryToBitmap(out bmp)) {
				typed = null;
				return false;
			}
			
			typed = new BmpImageResourceData(dib, bmp, lang, rawData);
			return true;
			
		}
		
		
		public override void SaveAs(Stream stream) {
			
			// don't use Image.Save since we want to save it without any added .NET Image class nonsense, and preserve 32-bit BMPs
			
			Byte[] bitmapFileData = _dib.Data;
			
			stream.Write( bitmapFileData, 0, bitmapFileData.Length );
			
		}
		
		public override string FileFilter {
			get { return "BMP Image (*.bmp)|*.bmp"; }
		}
		
	}
	
}
