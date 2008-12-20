using System;
using System.Drawing;

namespace Anolis.Core.Data {
	
	public class PngImageResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if(typeId.KnownType != Win32ResourceType.Custom) return Compatibility.No;
			
			if(typeId.StringId == "PNG") return Compatibility.Yes;
			
			return Compatibility.Maybe;
		}
		
		public override Compatibility HandlesExtension(string filenameExtension) {
			if(filenameExtension == "png") return Compatibility.Yes;
			return Compatibility.No;
		}
		
		public override ResourceData FromResource(ResourceLang lang, byte[] data) {
			
			PngImageResourceData rd;
			if( PngImageResourceData.TryCreate(null, data, out rd) ) return rd;
			
			return null;
			
		}
		
		public override ResourceData FromFile(System.IO.Stream stream, string extension) {
			Byte[] data = GetAllBytesFromStream(stream);
			return FromResource(null, data);
		}
		
		public override string Name {
			get { return "PNG Image"; }
		}
	}
	
	public sealed class PngImageResourceData : ImageResourceData {
		
		private PngImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] data, out PngImageResourceData typed) {
			
			typed = null;
			
			if(!HasPngSignature(data)) return false;
			
			Image image;
			
			if(!TryCreateImage(data, out image)) return false;
			
			typed = new PngImageResourceData(image, lang, data);
			
			return true;
			
		}
		
		public static Boolean HasPngSignature(Byte[] data) {
			
			if(data.Length < 8) return false;
			
			// PNG Signature:
			// 89 50 4E 47 0D 0A 1A 0A
			
			return
				data[0] == 0x89 &&
				data[1] == 0x50 && // P
				data[2] == 0x4E && // N
				data[3] == 0x47 && // G
				data[4] == 0x0D && // CR
				data[5] == 0x0A && // LF
				data[6] == 0x1A && // EOF
				data[7] == 0x0A;   // LF
			
		}
		
		public override String[] SaveFileFilter {
			get { return new String[] { "PNG Image (*.png)|*.png" }; }
		}
		
	}
}