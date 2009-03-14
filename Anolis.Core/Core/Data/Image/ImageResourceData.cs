using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using System.Runtime.InteropServices;
using Anolis.Core.Native;
using Anolis.Core.Utility;

namespace Anolis.Core.Data {
	
	public abstract class ImageResourceData : ResourceData {
		
		public virtual Image Image { get; private set; }
		
		protected ImageResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		protected ImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
			
			Image = image;
			
		}
		
		protected static Boolean TryCreateImage(Byte[] data, out Image image) {
			
			image = null;
			
			try {
				
				MemoryStream stream = new MemoryStream(data); // Should MemoryStreams be in a using() for Bitmap creation? MSDN says the Stream needs to exist for the life of the Bitmap
				image = Image.FromStream( stream, true, true );
				
				return true;
				
			} catch(ArgumentException) {
				return false;
			} catch(ExternalException) {
				return false;
			}
			
		}
		
		/// <summary>Saves the underlying Image to the specified Stream using the specified Image format at 100% quality (where possible).</summary>
		protected void ConvertAndSaveImageAs(Stream saveTo, String extension) {
			
			ImageResourceDataSaveFormat format;
			switch(extension) {
				case "jpg": format = ImageResourceDataSaveFormat.Jpeg; break;
				case "exf": format = ImageResourceDataSaveFormat.Exif; break;
				case "gif": format = ImageResourceDataSaveFormat.Gif; break;
				case "png": format = ImageResourceDataSaveFormat.Png; break;
				case "bmp": format = ImageResourceDataSaveFormat.Bmp; break;
				default:
					throw new NotSupportedException("Unrecognised image file format extension \"" + extension + '"');
			}
			
			ConvertAndSaveImageAs(saveTo, format);
			
		}
		
		/// <summary>Saves the underlying Image to the specified Stream using the specified Image format at 100% quality (where possible).</summary>
		protected void ConvertAndSaveImageAs(Stream saveTo, ImageResourceDataSaveFormat format) {
			
			EncoderParameters paras = new EncoderParameters(1);
			paras.Param[0] = new EncoderParameter(Encoder.Quality, 100);
			
			switch(format) {
				case ImageResourceDataSaveFormat.Jpeg:
					
					Image.Save(saveTo, GetEncoder(ImageFormat.Jpeg), paras);
					break;
				
				case ImageResourceDataSaveFormat.Exif:
					
					Image.Save(saveTo, GetEncoder(ImageFormat.Exif), paras);
					break;
					
				case ImageResourceDataSaveFormat.Gif:
					
					Image.Save(saveTo, GetEncoder(ImageFormat.Gif), paras);
					break;
					
				case ImageResourceDataSaveFormat.Png:
				
					Image.Save(saveTo, GetEncoder(ImageFormat.Png), paras);
					break;
					
				case ImageResourceDataSaveFormat.Bmp:
					
					Image.Save(saveTo, GetEncoder(ImageFormat.Bmp), paras);
					break;
					
			}
			
		}
		
		private static ImageCodecInfo GetEncoder(ImageFormat format) {
			
			foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
				if (codec.FormatID == format.Guid)
					return codec;
			
			return null;
		}
		
		protected override void Dispose(Boolean managed) {
			
			if(managed) {
				this.Image.Dispose();
			}
			
			base.Dispose(managed);
		}
		
	}
	
	public enum ImageResourceDataSaveFormat {
		Jpeg,
		Exif,
		Gif,
		Png,
		Bmp
	}
	
}
