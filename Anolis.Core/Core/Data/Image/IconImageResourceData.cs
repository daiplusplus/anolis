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
		
		protected override String GetOpenFileFilter() {
			return null;
		}
		
		public override String OpenFileFilter {
			get { return null; }
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			IconCursorImageResourceData rd;
			
			if( IconCursorImageResourceData.TryCreate(true, lang, data, out rd) ) return rd;
			
			return null;
		}
		
		public override String Name {
			get { return "Icon Directory Member Image"; }
		}
		
		public override ResourceData FromFileToAdd(Stream stream, string extension, ushort lang, ResourceSource currentSource) {
			LastErrorMessage = "Not supported";
			return null;
		}
		
		public override ResourceData FromFileToUpdate(Stream stream, string extension, ResourceLang currentLang) {
			LastErrorMessage = "Not supported";
			return null;
		}
	}
	
	public class CursorImageResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if( typeId.KnownType == Win32ResourceType.CursorImage ) return Compatibility.Yes;
			if( typeId.KnownType == Win32ResourceType.Bitmap      ) return Compatibility.Maybe;
			
			return Compatibility.No;
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			
			//if( filenameExtension == "BMP" ) return Compatibility.Maybe;
			
			return Compatibility.No;
		}
		
		protected override String GetOpenFileFilter() {
			return null;
		}
		public override String OpenFileFilter {
			get { return null; }
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			IconCursorImageResourceData rd;
			
			if( IconCursorImageResourceData.TryCreate(false, lang, data, out rd) ) return rd;
			
			return null;
			
		}
		
		public override string Name {
			get { return "Cursor"; }
		}
		
		public override ResourceData FromFileToAdd(Stream stream, string extension, ushort lang, ResourceSource currentSource) {
			LastErrorMessage = "Not implemented";
			return null;
		}
		
		public override ResourceData FromFileToUpdate(Stream stream, string extension, ResourceLang currentLang) {
			LastErrorMessage = "Not implemented";
			return null;
		}
	}
	
	/// <summary>Magically represents both Icons and Cursors.</summary>
	public sealed class IconCursorImageResourceData : ImageResourceData {
		
		/// <summary>Unmanaged pointer to the memory holding the data that makes up the GDI icon pointed to by hIcon created by CreateIconFromResource</summary>
		private IntPtr _p;
		
		private IntPtr _hIcon;
		
		private IconCursorImageResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		private IconCursorImageResourceData(Image image, ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
			_image = image;
		}
		
		private void CreateIcon() {
			
			// "Almost" cheating; this uses Win32's icon function, but in a nice way that doesn't break my conceptual model
			
			// and to think I'd need to manually process the DIB information to extract and recreate Bitmaps
			
			// although I might want to do that in future if I wanted to display the AND and XOR masks separately
			
			_p = Marshal.AllocHGlobal( RawData.Length );
			Marshal.Copy( RawData, 0, _p, RawData.Length );
			
			_hIcon = NativeMethods.CreateIconFromResource( _p, (uint)RawData.Length, IsIcon);
			if(_hIcon == IntPtr.Zero) {
				String message = NativeMethods.GetLastErrorString();
				throw new ResourceDataException("CreateIconFromResourceEx Failed " + message);
			}
			
			// because the Icon is born out of unmanaged data I cannot free the handle here; do it in the Disposer
			
			_icon = System.Drawing.Icon.FromHandle( _hIcon );
			
		}
		
		protected override void Dispose(Boolean managed) {
			
			if(managed) {
				if( _image != null ) _image.Dispose();
				if( _icon  != null ) _icon.Dispose();
			}
			
			NativeMethods.DestroyIcon( _hIcon );
			if( _p != IntPtr.Zero ) Marshal.FreeHGlobal( _p );
			
			base.Dispose(managed);
		}
		
		private Icon _icon;
		
		public Icon Icon {
			get {
				if( _icon == null ) CreateIcon();
				return _icon;
			}
		}
		
		private Image _image;
		
		public override Image Image {
			get {
				if( _image == null ) {
					_image = Icon.ToBitmap();
				}
				return _image;
			}
		}
		
		public Boolean IsIcon { get; private set; }
		
		public static Boolean TryCreate(Boolean isIcon, ResourceLang lang, Byte[] rawData, out IconCursorImageResourceData typed) {
			
			// rawData is an ICONIMAGE structure OR a PNG image
			
			// if it's a PNG image it's easy enough:
			if( PngImageResourceData.HasPngSignature( rawData ) ) {
				
				Image image;
				if(!ImageResourceData.TryCreateImage(rawData, out image)) { typed = null; return false; }
				
				typed = new IconCursorImageResourceData(image, lang, rawData) { IsIcon = isIcon };
				return true;
				
			}
			
			typed = new IconCursorImageResourceData(lang, rawData) { IsIcon = isIcon };
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
			
			if( extension == "png" ) {
				
				// hmm, that was simple
				//Icon.Save( stream );
				Image.Save( stream, System.Drawing.Imaging.ImageFormat.Png );
				
			} else {
				
				base.ConvertAndSaveImageAs(stream, extension);
				
			}
			
		}
		
		protected override String[] SupportedFilters {
			get { return new String[] {
				"PNG Image (*.png)|*.png"
			}; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier( IsIcon ? Win32ResourceType.IconImage : Win32ResourceType.CursorImage );
		}
		
		private static Bitmap GetImage(Icon icon) {
			
			// Icon.ToBitmap() throws when dealing with certain icon image types, like the (seemingly perfectly valid) 2-color bitmaps in Apple QuickTime 7's QuickTime.cpl control panel
			// it should be possible to work-around it in future by creating the bitmap myself
			
			// but for now just use the old method, even if it can throw under certain circumstances
			
			// actually, I think the exception is only raised when debugging. I can never seem to repro this when using a non-attached Release version (I can open all of QuickTime.cpl's icon images perfectly fine)
			
			try {
				
				return icon.ToBitmap();
			
			} catch(ArgumentException) {
				// TODO: Create the bitmap manually
				throw;
				
			}
			
		}
		
	}
	
}
