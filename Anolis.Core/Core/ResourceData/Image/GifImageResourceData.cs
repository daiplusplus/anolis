using System;
using System.Drawing;

namespace Anolis.Core.Data {
	
	public sealed class GifImageResourceData : ImageResourceData {
		
		// There is no need to override the SaveAs method since the bytes are the same
		
		private GifImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] data, out ResourceData typed) {
			
			// XN Resource Editor checks if the first few bytes are a GIF signature
			
			typed = null;
			
			if(data.Length < 5) return false;
			
			String sig = System.Text.Encoding.ASCII.GetString(data, 0, 5);
			
			if(sig != "GIF87" && sig != "GIF89") return false;
			
			Image image;
			
			if(!TryCreateImage(data, out image)) return false;
			
			typed = new GifImageResourceData(image, lang, data);
			
			return true;
			
		}
		
		public override string FileFilter {
			get { return "GIF Image (*.gif)|*.gif"; }
		}
		
	}
	
}
