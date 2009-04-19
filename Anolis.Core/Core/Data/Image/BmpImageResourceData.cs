using System;
using System.Drawing;
using System.Text;

using Stream = System.IO.Stream;
using Anolis.Core.Utility;
using Anolis.Core.Source;

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
			
			return Compatibility.No;
			
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			
			// I was thinking of adding a Compatibility.ConvertTo, but that's what Maybe is for...
			
			switch(filenameExtension) {
				case "BMP":
				case "DIB":
				case "RLE":
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
		
		protected override String GetOpenFileFilter() {
			return CreateFileFilter("BmpImage", "bmp", "dib", "rle");
		}
		
		public override ResourceData FromFileToAdd(System.IO.Stream stream, string extension, ushort lang, ResourceSource currentSource) {
			return FromFile(stream, extension);
		}
		
		public override ResourceData FromFileToUpdate(System.IO.Stream stream, string extension, ResourceLang currentLang) {
			return FromFile(stream, extension);
		}
		
		private ResourceData FromFile(Stream stream, String extension) {
			
			if(extension == "bmp") {
				
				Byte[] fileData = GetAllBytesFromStream(stream);
				
				return FromResource(null, fileData);
				
			} else {
				
				throw new NotImplementedException();
				
			}
			
		}
		
		public override ResourceData FromResource(ResourceLang lang, byte[] data) {
			
			BmpImageResourceData rd;
			
			if( BmpImageResourceData.TryCreate(lang, data, out rd) ) return rd;
			
			return null;
			
		}
		
		public override String Name {
			get { return "Bitmap"; }
		}
		
	}
	
	public sealed class BmpImageResourceData : ImageResourceData {
		
		private Dib _dib;
		
		private BmpImageResourceData(Dib dib, ResourceLang lang, Byte[] rawData) : base(lang, dib.GetDibBytes() ) {
			_dib = dib;
		}
		
		internal static Boolean TryCreate(ResourceLang lang, Byte[] rawData, out BmpImageResourceData typed) {
			
			Dib dib = new Dib( rawData );
			
			typed = new BmpImageResourceData(dib, lang, rawData);
			
			return true;
		}
		
		protected override void SaveAs(Stream stream, String extension) {
			
			// don't use Image.Save since we want to save it without any added .NET Image class nonsense, and preserve the Dib headers and stuff
			
			if(extension == "bmp") {
				
				_dib.Save( stream );
				
			} else {
				
				base.ConvertAndSaveImageAs( stream, extension );
				
			}
			
		}
		
		private Image _image;
		
		public override Image Image {
			get {
				if( _image == null ) {
					_image = _dib.ToBitmap();
					if(_image == null) throw new ResourceDataException("Invalid DIB Bitmap Format");
				}
				return _image;
			}
		}
		
		protected override void Dispose(bool managed) {
			
			if(managed) {
				if( _image != null ) _image.Dispose();
			}
			
			base.Dispose(managed);
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
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier( Win32ResourceType.Bitmap );
		}
		
	}
	
}
