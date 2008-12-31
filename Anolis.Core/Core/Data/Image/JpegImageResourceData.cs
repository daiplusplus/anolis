using System;
using System.Drawing;

namespace Anolis.Core.Data {
	
	public class JpegImageResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if(typeId.KnownType == Win32ResourceType.Custom) {
				
				if( String.Equals(typeId.StringId, "JPEG", StringComparison.InvariantCultureIgnoreCase ) ||
					String.Equals(typeId.StringId, "JPG", StringComparison.InvariantCultureIgnoreCase ) )
					
					return Compatibility.Yes;
				
				return Compatibility.Maybe;
				
			}
			
			return Compatibility.No;
			
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			
			switch(filenameExtension) {
				case "jpeg":
				case "jpg":
				case "jfif":
					return Compatibility.Yes;
			}
			
			return Compatibility.No;
			
		}
		
		public override String OpenFileFilter {
			get { return "JPEG Image (*.jpg, *.jpeg, *.jfif)|*.jpg, *.jpeg, *.jfif"; }
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			JpegImageResourceData rd;
			
			if( JpegImageResourceData.TryCreate(lang, data, out rd ) ) return rd;
			
			return null;
			
		}
		
		public override ResourceData FromFile(System.IO.Stream stream, String extension, ResourceSource source) {
			
			Byte[] data = GetAllBytesFromStream(stream);
			
			return FromResource(null, data);
			
		}
		
		public override String Name {
			get { return "JPEG Image"; }
		}
	}
	
	public sealed class JpegImageResourceData : ImageResourceData {
		
		private JpegImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
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
		
	}
}