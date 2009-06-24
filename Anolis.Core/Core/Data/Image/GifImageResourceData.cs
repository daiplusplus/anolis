using System;
using System.IO;
using System.Drawing;

namespace Anolis.Core.Data {
	
	public class GifImageResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if(typeId.KnownType == Win32ResourceType.Unknown) return Compatibility.Maybe;
			if(typeId.KnownType == Win32ResourceType.Html)    return Compatibility.Maybe;
			
			if(typeId.KnownType != Win32ResourceType.Custom) return Compatibility.No;
			
			if(typeId.StringId == "GIF") return Compatibility.Yes;
			
			return Compatibility.Maybe;
			
		}
		
		public override Compatibility HandlesExtension(String fileNameExtension) {
			
			return ( IsExtension( fileNameExtension, "gif" ) ) ? Compatibility.Yes : Compatibility.No;
			
		}
		
		public override String OpenFileFilter {
			get { return CreateFileFilter("GifImage", "gif"); }
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			GifImageResourceData rd;
			
			if(GifImageResourceData.TryCreate(lang, data, out rd)) return rd;
			
			return null;
			
		}
		
		private static ResourceData FromFile(Stream stream, String extension) {
			
			Byte[] data = GetAllBytesFromStream(stream);
			
			GifImageResourceData rd;
			
			if(GifImageResourceData.TryCreate(null, data, out rd)) return rd;
			
			return null;
			
		}
		
		public override ResourceData FromFileToAdd(System.IO.Stream stream, string extension, ushort lang, ResourceSource currentSource) {
			return FromFile(stream, extension);
		}
		
		public override ResourceData FromFileToUpdate(System.IO.Stream stream, string extension, ResourceLang currentLang) {
			return FromFile(stream, extension);
		}
		
		public override string Name {
			get { return "GIF Image"; }
		}
	}
	
	public sealed class GifImageResourceData : ImageResourceData {
		
		// There is no need to override the SaveAs method since the bytes are the same
		
		private GifImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
			
			_image = image;
		}
		
		internal static Boolean TryCreate(ResourceLang lang, Byte[] data, out GifImageResourceData typed) {
			
			// XN Resource Editor checks if the first few bytes are a GIF signature
			
			typed = null;
			
			if(!HasGifSignature(data)) return false;
			
			Image image;
			
			if(!TryCreateImage(data, out image)) return false;
			
			typed = new GifImageResourceData(image, lang, data);
			
			return true;
			
		}
		
		public static Boolean HasGifSignature(Byte[] data) {
			
			// GIF files start with "GIF87" or "GIF89" in ASCII
			if(data.Length < 5) return false;
			return 
				data[0] == 0x47 &&  // G
				data[1] == 0x49 &&  // I
				data[2] == 0x46 &&  // F
				data[3] == 0x38 &&  // 8
				(data[4] == 0x37 || // 7
				 data[4] == 0x39);  // 9
		}
		
		protected override void SaveAs(System.IO.Stream stream, String extension) {
			
			if(extension == "gif") {
				
				stream.Write( this.RawData, 0, RawData.Length );
				
			} else {
				
				base.ConvertAndSaveImageAs(stream, extension);
				
			}
			
		}
		
		protected override String[] SupportedFilters {
			get { return new String[] {
				"GIF Image (*.gif)|*.gif",
				"Convert to Bitmap (*.exf)|*.exf",
				"Convert to EXIF (*.bmp)|*.bmp",
				"Convert to JPEG (*.jpg)|*.jpg",
				"Convert to PNG (*.png)|*.png",
			}; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier("GIF");
		}
		
		private Image _image;
		
		public override Image Image {
			get { return _image; }
		}
		
	}
	
}
