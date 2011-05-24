using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

using Anolis.Core.Data;

namespace Anolis.Core.Data {
	
	public class TiffImageResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if(typeId.KnownType != Win32ResourceType.Custom) return Compatibility.No;
			
			if( typeId.StringId.IndexOf("TIF", StringComparison.OrdinalIgnoreCase) > -1 ) return Compatibility.Yes;
			
			return Compatibility.Maybe;
		}
		
		public override Compatibility HandlesExtension(String fileNameExtension) {
			if( IsExtension( fileNameExtension, "TIF", "TIFF" ) ) return Compatibility.Yes;
			return Compatibility.No;
		}
		
		public override ResourceData FromResource(ResourceLang lang, byte[] data) {
			
			TiffImageResourceData rd;
			if( TiffImageResourceData.TryCreate( lang, data, out rd ) ) return rd;
			return null;
		}
		
		public override ResourceData FromFileToAdd(Stream stream, String extension, ushort langId, ResourceSource currentSource) {
			return FromFile(stream);
		}
		
		public override ResourceData FromFileToUpdate(Stream stream, String extension, ResourceLang currentLang) {
			return FromFile(stream);
		}
		
		private ResourceData FromFile(Stream stream) {
			return FromResource(null, GetAllBytesFromStream(stream));
		}
		
		public override String Name {
			get { return "TIFF Image"; }
		}
		
		public override String OpenFileFilter {
			get { return CreateFileFilter("TiffImage", "TIF"); }
		}
	}
	
	public class TiffImageResourceData : ImageResourceData {
		
		private TiffImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
			
			_image = image;
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] data, out TiffImageResourceData typed) {
			typed = null;
			
			if( !HasTiffSignature(data) ) return false;
			
			Image image;
			
			if( !TryCreateImage( data, out image ) ) return false;
			
			typed = new TiffImageResourceData(image, lang, data);
			
			return true;
		}
		
		public static Boolean HasTiffSignature(Byte[] data) {
			if( data.Length < 4) return false;
			
			// TIFF Little-Endian Signature
			// 49 49 2A 00
			
			// TIFF Big-Endian Signature:
			// 4D 4D 00 2A
			
			// Is it Little-Endian?
			bool isLittleEndian =
				data[0] == 0x49 && // I
				data[1] == 0x49 && // I
				data[2] == 0x2A && // *
				data[3] == 0x00;   // \0
			
			// Is it Big-Endian?
			bool isBigEndian =
				data[0] == 0x4D && // M
				data[1] == 0x4D && // M
				data[2] == 0x00 && // \0
				data[3] == 0x2A;   // *
			
			return isLittleEndian || isBigEndian;
		}
		
		private Image _image;
		
		public override Image Image {
			get { return _image; }
		}
		
		protected override String[] SupportedFilters {
			get { return new String[] {
				"TIFF Image (*.tif)|*.tif",
				"Convert to Bitmap (*.bmp)|*.bmp"
			}; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier("TIFF");
		}
		
		protected override void SaveAs(Stream stream, string extension) {
			if(extension == "tif") {
				
				stream.Write( this.RawData, 0, RawData.Length );
			} else {
				
				base.ConvertAndSaveImageAs( stream, extension );
			}
		}
	}
}
