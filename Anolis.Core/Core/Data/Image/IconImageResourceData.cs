using System;
using System.Drawing;
using System.IO;

using System.Runtime.InteropServices;
using Anolis.Core.Native;
using Anolis.Core.Utility;

namespace Anolis.Core.Data {
	
	public class IconImageResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if( typeId.KnownType == Win32ResourceType.IconImage ) return Compatibility.Yes;
			if( typeId.KnownType == Win32ResourceType.Bitmap    ) return Compatibility.Maybe;
			
			return Compatibility.No;
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			// Temporarily: disallow modification of subimages in icons
			/*
			if( filenameExtension == "png" ) return Compatibility.Maybe;
			if( filenameExtension == "bmp" ) return Compatibility.Maybe;
			*/
			return Compatibility.No;
		}
		
		public override String OpenFileFilter {
			get { return null; }
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			IconCursorImageResourceData rd;
			String message;
			
			if( IconCursorImageResourceData.TryCreate(true, lang, data, out message, out rd) ) return rd;
			
			LastErrorMessage = message;
			
			return null;
			
		}
		
		public override ResourceData FromFile(Stream stream, String extension, ResourceSource source) {
			
			LastErrorMessage = "Not implemented";
			return null;
			
		}
		
		public override String Name {
			get { return "Icon Sub-Image"; }
		}
	}
	
	public class CursorImageResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if( typeId.KnownType == Win32ResourceType.CursorImage ) return Compatibility.Yes;
			if( typeId.KnownType == Win32ResourceType.Bitmap      ) return Compatibility.Maybe;
			
			return Compatibility.No;
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			
			if( filenameExtension == "bmp" ) return Compatibility.Maybe;
			
			return Compatibility.No;
		}
		
		public override String OpenFileFilter {
			get { return null; }
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			IconCursorImageResourceData rd;
			String message;
			
			if( IconCursorImageResourceData.TryCreate(false, lang, data, out message, out rd) ) return rd;
			
			LastErrorMessage = message;
			
			return null;
			
		}
		
		public override ResourceData FromFile(Stream stream, String extension, ResourceSource source) {
			
			throw new NotImplementedException();
			
		}
		
		public override string Name {
			get { return "Cursor"; }
		}
	}
	
	/// <summary>Magically represents both Icons and Cursors.</summary>
	public sealed class IconCursorImageResourceData : ImageResourceData {
		
		private IntPtr _unmanagedMemory;
		
		private IconCursorImageResourceData(IntPtr unmanagedPointer, Icon icon, ResourceLang lang, Byte[] rawData) : base(icon.ToBitmap(), lang, rawData) {
			
			_unmanagedMemory = unmanagedPointer;
			
			Icon = icon;
			Size = icon.Size;
		}
		
		private IconCursorImageResourceData(IntPtr unmanagedPointer, Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
			
			_unmanagedMemory = unmanagedPointer;
			
			// Icon is null
			Size = image.Size;
		}
		
		~IconCursorImageResourceData() {
			
			Marshal.FreeHGlobal( _unmanagedMemory );
			
		}
		
		public Size Size { get; private set; }
		
		public Icon Icon { get; private set; }
		
		public Boolean IsIcon { get; private set; }
		
		public static Boolean TryCreate(Boolean isIcon, ResourceLang lang, Byte[] rawData, out String message, out IconCursorImageResourceData typed) {
			
			message = null;
			
			// rawData is an ICONIMAGE structure OR a PNG image
			
			// if it's a PNG image it's easy enough:
			if( PngImageResourceData.HasPngSignature( rawData ) ) {
				
				Image image;
				if(!ImageResourceData.TryCreateImage(rawData, out image)) { typed = null; return false; }
				
				typed = new IconCursorImageResourceData(IntPtr.Zero, image, lang, rawData) { Size = image.Size, IsIcon = isIcon };
				return true;
				
			}
			
			// "Almost" cheating; this uses Win32's icon function, but in a nice way that doesn't break my conceptual model
			
			// and to think I'd need to manually process the DIB information to extract and recreate Bitmaps
			
			// although I might want to do that in future if I wanted to display the AND and XOR masks separately
			
			IntPtr p = Marshal.AllocHGlobal( rawData.Length );
			Marshal.Copy( rawData, 0, p, rawData.Length );
			
			IntPtr hIcon = NativeMethods.CreateIconFromResource(p, (uint)rawData.Length, isIcon);
			if(hIcon == IntPtr.Zero) {
				message = NativeMethods.GetLastErrorString();
				typed = null;
				return false;
			}
			
			Icon icon = Icon.FromHandle( hIcon );
			
			// because the Icon is born out of unmanaged data I cannot free the handle here; do it in the finaliser
			
			typed = new IconCursorImageResourceData(p, icon, lang, rawData) { IsIcon = isIcon };
			return true;
			
		}
		
/*		public Bitmap XorMask {
			
		}
		
		public Bitmap AndMask {
			
		}*/
		
/*		private static Boolean OldLoadCode(ResourceLang lang, Byte[] rawData, out ResourceData typed) {
			
			
			// I can't load the data into the ICONIMAGE because .NET doesn't support C-style value arrays very well
			
/*
typdef struct {
	BITMAPINFOHEADER icHeader;      // DIB header
	RGBQUAD          icColors[1];   // Color table
	BYTE             icXOR[1];      // DIB bits for XOR mask
	BYTE             icAND[1];      // DIB bits for AND mask
}ICONIMAGE, *LPICONIMAGE;
* /
			
			// in the BitmapInfoHeader icHeader only the following fields are used:
			//	biSize, biWidth, biHeight, biPlanes, biBitCount, biSizeImage.
			// All other members must be 0. The biHeight member specifies the combined height of the XOR and AND masks.
			// The members of icHeader define the contents and sizes of the other elements of the ICONIMAGE structure in the
			//	same way that the BITMAPINFOHEADER structure defines a CF_DIB format DIB.
			
			Int32 sizeOfBitmapInfoHeader = Marshal.SizeOf(typeof(BitmapInfoHeader));
			
			IntPtr p = Marshal.AllocHGlobal( rawData.Length );
			Marshal.Copy( rawData, 0, p, rawData.Length );
			
			BitmapInfoHeader bih = (BitmapInfoHeader)Marshal.PtrToStructure( p, typeof(BitmapInfoHeader) );
			
			// the XOR part of the ICONIMAGE is the actual DIB bitmap and can be treated as such (just divide the biHeight by 2 first)
			// the AND part is the 'binary transparency' part of the icon. It is redundant in 32-bit DIBs.
			
			// I need to make a copy of the IconInfo data, modify the BitmapInfoHeader to fix the size and then factor in the AND mask
			
			bih.biHeight /= 2;
			
			Marshal.StructureToPtr(bih, p, false);
			
			Byte[] fixd = new Byte[rawData.Length];
			
			Marshal.Copy(p, fixd, 0, rawData.Length);
			Marshal.FreeHGlobal( p );
			
			// truncate the array so it's the right size
			
			// use biSizeImage as this value MUST be set
				// gah... wait a sec. Msmsgr's #1 icon doesn't have biSizeImage set!!!!
			// biSizeImage is equal to the calculated bmSizeBits in the Dib class
			
			// which means the byte array length must be equal to... TODO: find out, damnit
			
			// I propose subclassing Dib to create a Dib for icons that passes in an IconInfo to get hints as to how to process it
				// I disagree, it still needs to be able to process icons without their directories.
				// right, sorry, just get it so it gets the image size right. Shame we can't trust biSizeImage
			
			Array.Resize<Byte>(ref fixd, (int)bih.biSizeImage );
			
			
			MemoryDib dib = new MemoryDib( fixd );
			
			typed = null;
			return false;
			
		}*/
		
		protected override void SaveAs(Stream stream, String extension) {
			
			if( extension != "bmp" ) {
				
				base.ConvertAndSaveImageAs(stream, extension);
				
			} else {
				
				// hmm, that was simple
				Icon.Save( stream );
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
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier( IsIcon ? Win32ResourceType.IconImage : Win32ResourceType.CursorImage );
		}
		
	}
	
}
