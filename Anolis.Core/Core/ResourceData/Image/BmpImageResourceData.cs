using System;
using System.Drawing;
using System.Text;

using Stream = System.IO.Stream;
using Anolis.Core.Utility;

namespace Anolis.Core.Data {
	
	// might it be an idea to support FileBmpImageResourceData?
	// XN Resource Editor does with this annotation: "\program files\Microsoft Office\office\1033\outlibr.dll"
	// so it's worth checking out
	
	public class BmpImageResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if( typeId.KnownType == Win32ResourceType.Bitmap ) return Compatibility.Yes;
			if( typeId.KnownType == Win32ResourceType.IconImage ) return Compatibility.Maybe;
			if( typeId.KnownType == Win32ResourceType.CursorImage ) return Compatibility.Maybe;
			
			if( typeId.KnownType != Win32ResourceType.Custom ) return Compatibility.No;
			
			return Compatibility.Maybe;
			
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			
			// I was thinking of adding a Compatibility.ConvertTo, but that's what Maybe is for...
			
			switch(filenameExtension) {
				case "bmp":
				case "dib":
					return Compatibility.Yes;
/*				case "jpg":
				case "jpeg":
				case "png":
				case "gif":
				case "exif":
					return Compatibility.Maybe; */
			}
			
			return Compatibility.No;
			
		}
		
		public override String OpenFileFilter {
			get { return "Bitmap Image (*.bmp)|*.bmp"; }
		}
		
		public override ResourceData FromFile(Stream stream, String extension, ResourceSource source) {
			
			if(extension == "bmp") {
				
				Byte[] fileData = GetAllBytesFromStream(stream);
				
				return FromResource(null, fileData);
				
			} else {
				
				throw new NotImplementedException();
				
			}
			
		}
		
		public override ResourceData FromResource(ResourceLang lang, byte[] data) {
			
			BmpImageResourceData rd;
			
			if( BmpImageResourceData.TryCreate(null, data, out rd) ) return rd;
			
			return null;
			
		}
		
		public override String Name {
			get { return "Bitmap"; }
		}
	}
	
	public sealed class BmpImageResourceData : ImageResourceData {
		
		private FileDib _dib;
		
		private BmpImageResourceData(FileDib dib, Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
			_dib = dib;
		}
		
		internal static Boolean TryCreate(ResourceLang lang, Byte[] rawData, out BmpImageResourceData typed) {
			
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
		
		protected override void SaveAs(Stream stream, String extension) {
			
			// don't use Image.Save since we want to save it without any added .NET Image class nonsense, and preserve 32-bit BMPs
			
			if(extension == "bmp") {
				
				Byte[] bitmapFileData = _dib.Data;
				
				stream.Write( bitmapFileData, 0, bitmapFileData.Length );
				
			} else {
				
				base.ConvertAndSaveImageAs( stream, extension );
				
			}
			
		}
		
		protected override String[] SupportedFilters {
			get { return new String[] {
				"BMP Image (*.bmp)|*.bmp",
				"Convert to EXIF (*.exf)|*.exf",
				"Convert to GIF (*.gif)|*.gif",
				"Convert to JPEG (*.jpg)|*.jpg",
				"Convert to PNG (*.png)|*.png",
			}; }
		}
		
	}
	
}
