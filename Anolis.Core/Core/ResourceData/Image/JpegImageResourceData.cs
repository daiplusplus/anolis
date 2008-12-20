using System;
using System.Drawing;

namespace Anolis.Core.Data {
	
	public sealed class JpegImageResourceData : ImageResourceData {
		
		private JpegImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] data, out ResourceData typed) {
			
			// XN Resource Editor does a little bit of raw byte voodoo to assess if a stream is a JPEG/JFIF or not
			// I'll implement it in a later version unless it's a massive resource drain
			
			typed = null;
			
			Image image;
			
			if(!TryCreateImage(data, out image)) return false;
			
			typed = new JpegImageResourceData(image, lang, data);
			
			return true;
			
		}
		
		public override String[] SaveFileFilter {
			get { return new String[] { "JPEG Image (*.jpg)|*.jpg" }; }
		}
		
	}
}