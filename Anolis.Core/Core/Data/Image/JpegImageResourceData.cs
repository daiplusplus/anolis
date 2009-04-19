using System;
using System.Drawing;

namespace Anolis.Core.Data {
	
	public class JpegImageResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if(typeId.KnownType == Win32ResourceType.Unknown) return Compatibility.Maybe;
			if(typeId.KnownType != Win32ResourceType.Custom) return Compatibility.No;
			
			if(typeId.StringId == "JPEG") return Compatibility.Yes;
			if(typeId.StringId == "JPG") return Compatibility.Yes;
			if(typeId.StringId == "JPEGFILE") return Compatibility.Yes;
			
			return Compatibility.Maybe;
			
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			
			switch(filenameExtension) {
				case "JPEG":
				case "JPG":
				case "JFIF":
					return Compatibility.Yes;
			}
			
			return Compatibility.No;
			
		}
		
		protected override String GetOpenFileFilter() {
			return CreateFileFilter("JpegImage", "jpg", "jpeg", "jfif");
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			JpegImageResourceData rd;
			
			if( JpegImageResourceData.TryCreate(lang, data, out rd ) ) return rd;
			
			return null;
			
		}
		
		private ResourceData FromFile(System.IO.Stream stream, String extension) {
			
			Byte[] data = GetAllBytesFromStream(stream);
			
			return FromResource(null, data);
			
		}
		
		public override ResourceData FromFileToAdd(System.IO.Stream stream, string extension, ushort lang, ResourceSource currentSource) {
			return FromFile(stream, extension);
		}
		
		public override ResourceData FromFileToUpdate(System.IO.Stream stream, string extension, ResourceLang currentLang) {
			return FromFile(stream, extension);
		}
		
		public override String Name {
			get { return "JPEG Image"; }
		}
	}
	
	public sealed class JpegImageResourceData : ImageResourceData {
		
		private JpegImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
			
			_image = image;
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] data, out JpegImageResourceData typed) {
			
			typed = null;
			
			if( !HasJpegSignature(data) ) return false;
			
			Image image;
			
			if(!TryCreateImage(data, out image)) return false;
			
			typed = new JpegImageResourceData(image, lang, data);
			
			return true;
			
		}
		
		public static Boolean HasJpegSignature(Byte[] data) {
			
			// HACK: All of the JPEGs I've encountered so far have these bytes
			// the XN Editor uses it too, but also calls FindJpegSegment which isn't a method I can reproduce
			
			// something to do with a magic 'E0' byte located in the stream
			
			if(data.Length < 2) return false;
			
			return data[0] == 0xFF && data[1] == 0xD8;
			
		}
		
		protected override void SaveAs(System.IO.Stream stream, String extension) {
			
			if(extension == "jpg") {
				
				stream.Write( this.RawData, 0, RawData.Length );
				
			} else {
				
				base.ConvertAndSaveImageAs(stream, extension);
				
			}
			
		}
		
		protected override String[] SupportedFilters {
			get { return new String[] {
				"JPEG Image (*.jpg)|*.jpg",
				"Convert to Bitmap (*.bmp)|*.bmp",
				"Convert to EXIF (*.exf)|*.exf",
				"Convert to GIF (*.gif)|*.gif",
				"Convert to PNG (*.png)|*.png",
			}; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier("JPEG");
		}
		
		private Image _image;
		
		public override Image Image {
			get { return _image; }
		}
		
	}
}