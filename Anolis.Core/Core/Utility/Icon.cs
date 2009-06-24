using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Drawing;

using Anolis.Core.Data;
using Anolis.Core.Native;
using System.IO;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Utility {
	
	public class IconGroup {
		
		private static readonly ResourceTypeIdentifier _iiTypeId = new ResourceTypeIdentifier( Win32ResourceType.IconImage );
		private static readonly ResourceTypeIdentifier _ciTypeId = new ResourceTypeIdentifier( Win32ResourceType.CursorImage );
		
		private List<IDirectoryMember> _images = new List<IDirectoryMember>();
		
		public IconType     Type          { get; private set; }
		
		public ResourceLang DirectoryLang { get; private set; } 
		
		public IconGroup(String fileName) {
			
			using(FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read)) {
				
				Load( fs );
			}
			
		}
		
		public IconGroup(Stream icoStream) {
			
			Load( icoStream );
			
		}
		
		private DirectoryMemberCollection _members;
		
		public DirectoryMemberCollection Members {
			get {
				if( _members == null ) _members = new DirectoryMemberCollection( _images );
				return _members;
			}
		}
		
		public IconGroup(ResourceLang directoryLang, Byte[] rawData) {
			
			Load( directoryLang, rawData );
		}
		
		private void Load(ResourceLang directoryLang, Byte[] rawData) {
			
			this.DirectoryLang = directoryLang;
			
			using(MemoryStream ms = new MemoryStream( rawData )) {
				
				BinaryReader rdr = new BinaryReader(ms);
				
				IconDirectory dir = new IconDirectory( rdr );
				if( dir.wType == 1 ) Type = IconType.Icon;
				if( dir.wType == 2 ) Type = IconType.Cursor;
				
				ResIconDirectoryEntry[] subImages = new ResIconDirectoryEntry[ dir.wCount ];
				for(int i=0;i<subImages.Length;i++) {
					
					subImages[i] = new ResIconDirectoryEntry( rdr );
				}
				
				//////////////////////////////////////////
				
				foreach(ResIconDirectoryEntry entry in subImages) {
					
					IconCursorImageResourceData rdata = GetRD( entry.wId );
					
					IconImage image = new IconImage( rdata.RawData, this, entry );
					image.ResourceDataTyped = rdata;
					
					_images.Add( image );
					
				}
				
			}
			
		}
		
		/// <summary>Associates all of the IconImages in this directory with the ResourceSource by creating IconCursorResourceData instances for each IconImage (with the specified Lang)</summary>
		public void BindToSource(ResourceSource source, UInt16 langId) {
			
			IconCursorImageResourceDataFactory factory = GetImageFactory();
			
			ResourceTypeIdentifier typeId = Type == IconType.Icon ? _iiTypeId : _ciTypeId;
			
			foreach(IconImage image in _images) {
				
				IconCursorImageResourceData rd = (IconCursorImageResourceData)factory.FromFileData(null, image.ImageData, Type == IconType.Icon);
				
				image.ResourceDataTyped = rd;
				
				source.Add( typeId, source.GetUnusedName(typeId), langId, rd );
			}
			
		}
		
		public void Merge(IconGroup additions) {
			
			foreach(IconImage image in additions._images) {
				
				this.AddUpdate( image );
			}
			
		}
		
		private IconCursorImageResourceDataFactory _imgFactory;
		
		private IconCursorImageResourceDataFactory GetImageFactory() {
			
			if( _imgFactory != null ) return _imgFactory;
			
			Win32ResourceType desired = Type == IconType.Icon ? Win32ResourceType.IconImage : Win32ResourceType.CursorImage;
			
			ResourceTypeIdentifier typeId = new ResourceTypeIdentifier( desired );
			
			ResourceDataFactory[] factories = ResourceDataFactory.GetFactoriesForType( typeId );
			
			foreach(ResourceDataFactory f in factories) {
				
				IconCursorImageResourceDataFactory icif = f as IconCursorImageResourceDataFactory;
				if(icif != null) return _imgFactory = icif;
				
			}
			
			return _imgFactory;
		}
		
		private void Load(Stream icoStream) {
			
			BinaryReader rdr = new BinaryReader( icoStream );
			
			IconDirectory dir = new IconDirectory( rdr );
			if( dir.wType == 1 ) Type = IconType.Icon;
			if( dir.wType == 2 ) Type = IconType.Cursor;
			
			FileIconDirectoryEntry[] subImages = new FileIconDirectoryEntry[ dir.wCount ];
			for(int i=0;i<subImages.Length;i++) {
				
				subImages[i] = new FileIconDirectoryEntry( rdr );
			}
			
			//////////////////////////////////////////
			
			foreach(FileIconDirectoryEntry entry in subImages) {
				
				rdr.BaseStream.Seek( entry.dwImageOffset, SeekOrigin.Begin );
				
				Byte[] imageData = rdr.ReadBytes( (int)entry.dwBytesInRes );
				
				IconImage image = new IconImage( imageData, this, entry );
				image.ResourceDataTyped = GetRD( imageData );
				
				_images.Add( image );
				
			}
			
		}
		
		private IconCursorImageResourceData GetRD(Byte[] data) {
			
			IconCursorImageResourceDataFactory factory = GetImageFactory();
			
			return factory.FromFileData( null, data, Type == IconType.Icon );
		}
		
		/// <summary>Saves as an ICO to the specified File</summary>
		public void Save(String fileName) {
			
			using(FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write)) {
				
				Save( fs );
			}
			
		}
		
		/// <summary>Saves as an ICO to the specified Stream</summary>
		public void Save(Stream stream) {
			
			BinaryWriter wtr = new BinaryWriter( stream );
			
			IconDirectory dir = new IconDirectory();
			dir.wReserved = 0;
			dir.wType     = (ushort)(Type == IconType.Icon ? 1u : 2u);
			dir.wCount    = (ushort)_images.Count;
			
			dir.Write( wtr );
			
			//////////////////////////////////
			
			_images.Sort();
			
			// Calculate offsets
			
			UInt32 offsetSoFar = (uint)( IconDirectory.Size + _images.Count * FileIconDirectoryEntry.Size );
			
			UInt32[] offsets = new UInt32[ _images.Count ];
			
			for(int i=0;i<_images.Count;i++) {
				
				offsets[i] = offsetSoFar;
				
				offsetSoFar += (uint)(_images[i] as IconImage).ImageData.Length;
				
				if(Type == IconType.Cursor) offsetSoFar -= 4;
			}
			
			//////////////////////////////////
			
			for(int i=0;i<_images.Count;i++) {
				
				IconImage image = _images[i] as IconImage;
				
				FileIconDirectoryEntry entry = new FileIconDirectoryEntry();
				entry.bWidth        = (byte)image.Size.Width;
				entry.bHeight       = (byte)image.Size.Height;
				
				entry.bColorCount   = image.ColorCount;
				entry.bReserved     = 0;
				
				if( Type == IconType.Icon ) {
					
					entry.wPlanes      = image.Planes;
					entry.wBitCount    = image.BitCount;
					entry.dwBytesInRes = (uint)image.ImageData.Length;
					
				} else if( Type == IconType.Cursor ) {
					
					entry.wXHotspot    = (ushort)image.Hotspot.X;
					entry.wYHotspot    = (ushort)image.Hotspot.Y;
					entry.dwBytesInRes = (uint)image.ImageData.Length - 4;
				}
				
				entry.dwImageOffset = offsets[i];
				
				entry.Write( wtr );
			}
			
			//////////////////////////////////
			
			for(int i=0;i<_images.Count;i++) {
				
				IconImage image = _images[i] as IconImage;
				
				if(	Type == IconType.Icon ) {
					
					wtr.Write( image.ImageData );
					
				} else if( Type == IconType.Cursor ) {
					
					// the first 4 bytes (2 words) of a cursor's image data are discarded
					
					wtr.Write( image.ImageData, 4, image.ImageData.Length - 4 );
					
				}
				
				
			}
			
		}
		
		/// <summary>Returns an IconImage in the collection that matches the width and color depth, otherwise returns null</summary>
		private IconImage FindMatch(IconImage image) {
			
			foreach(IconImage img in _images) if( img.CompareTo( image ) == 0 ) return img;
			
			return null;
			
		}
		
		public IconImage GetIconForSize(Size size) {
			
			// search for an identical or larger match (as larger results can be scaled down)
			
			// sorted by size, then color depth; so the first dimensions match will be of the highest color depth
			IconImage[] sortedIcons = new IconImage[_images.Count];
			
			_images.CopyTo( sortedIcons, 0 );
			Array.Sort<IconImage>(sortedIcons, delegate(IconImage x, IconImage y) {
				
				// compare y to x because we want it in descending order
				Int32 sizeComparison = y.Size.Width.CompareTo( x.Size.Width );
				return sizeComparison == 0 ? y.BitCount.CompareTo( x.BitCount ) : sizeComparison;
				
			});
			
			IconImage bestMatch = null;
			
			foreach(IconImage member in sortedIcons) {
				
				if( member.Size == size ) return member;
				
				if(bestMatch == null) bestMatch = member;
				
				if( member.Size.Width > size.Width ) {
					
					if(bestMatch == null) bestMatch = member;
					else {
						
						if( member.Size.Width < bestMatch.Size.Width ) bestMatch = member;
						
					}
				}
			}
			
			return bestMatch;
			
		}
		
#region Resource-mode Members
		
		private ResourceType _appropriateType;
		
		private ResourceType AppropriateType {
			get {
				
				if( _appropriateType == null ) {
					
					Win32ResourceType appropriate = (Type == IconType.Icon) ? Win32ResourceType.IconImage : Win32ResourceType.CursorImage;
					
					foreach(ResourceType type in DirectoryLang.Name.Type.Source.AllTypes) {
						
						if( type.Identifier.KnownType == appropriate ) {
							
							_appropriateType = type;
							break;
							
						}
					}
					
					if( _appropriateType == null ) throw new AnolisException("Couldn't find SubImage ResourceType");
				}
				
				return _appropriateType;
			}
		}
		
		private IconCursorImageResourceData GetRD(UInt16 wId) {
			
			ResourceName appropriateName = null;
			
			foreach(ResourceName name in AppropriateType.Names) {
				
				if( name.Identifier.IntegerId == wId ) {
					appropriateName = name;
					break;
				}
			}
			
			if( appropriateName == null ) throw new AnolisException("Couldn't find ResourceName for wId");
			
			//////////////////////////////////
			
			if( appropriateName.Langs.Count == 1 ) {
				
				return appropriateName.Langs[0].Data as IconCursorImageResourceData;
			}
			
			foreach(ResourceLang lang in appropriateName.Langs) {
				
				if( lang.LanguageId == this.DirectoryLang.LanguageId || lang.LanguageId == 0 ) {
					
					// PNG subimages will always be IconCursorImageRD instances and not PngImageRD
					return lang.Data as IconCursorImageResourceData;
				}
				
			}
			
			throw new AnolisException("Couldn't find matching ResourceLang for subImage");
		}
		
		public Byte[] GetResDirectoryData() {
			
			// recreate the Icon Directory resource data
			
			MemoryStream ms = new MemoryStream();
			
			BinaryWriter wtr = new BinaryWriter(ms);
			
			IconDirectory dir = new IconDirectory();
			dir.wReserved = 0;
			dir.wType     = (ushort)(Type == IconType.Icon ? 1u : 2u);
			dir.wCount    = (ushort)_images.Count;
			
			dir.Write( wtr );
			
			foreach(IconImage image in _images) {
				
				ResIconDirectoryEntry entry = new ResIconDirectoryEntry();
				entry.bWidth  = (byte)image.Size.Width;
				entry.bHeight = (byte)image.Size.Height;
				
				entry.bColorCount = image.ColorCount;
				entry.bReserved = 0;
				
				entry.wPlanes   = image.Planes;
				entry.wBitCount = image.BitCount;
				
				entry.dwBytesInRes = (uint)image.ImageData.Length;
				entry.wId          = (ushort)image.ResourceData.Lang.Name.Identifier.NativeId.ToInt32(); // da...?
				
				entry.Write( wtr );
			}
			
			return ms.ToArray();
		}
		
		private ResourceSource Source {
			get { return DirectoryLang.Name.Type.Source; }
		}
		
		public void AddUpdate(IconImage image) {
			
			IconImage original = FindMatch( image );
			if( original == null ) {
				
				ResourceTypeIdentifier typeId = AppropriateType.Identifier;
				
				Source.Add( typeId, Source.GetUnusedName( typeId ), DirectoryLang.LanguageId, image.ResourceDataTyped );
				
			} else {
				
				ResourceLang lang = original.ResourceData.Lang;
				lang.SwapData( image.ResourceData );
				
				_images.Remove( original );
				
			}
			
			_images.Add( image );
			_images.Sort();
			
		}
		
		public void Remove(IconImage image) {
			
			Source.Remove( image.ResourceData.Lang );
			
			_images.Remove( image );
			
		}
		
#endregion
		
	}
	
	public enum IconType {
		Unknown,
		Icon   = 1,
		Cursor = 2
	}
	
	public class IconImage : IDirectoryMember, IComparable<IconImage> {
		
		public Byte[]       ImageData    { get; internal set; }
		public IconGroup    ParentIcon   { get; internal set; }
		
		public String       Description  { get; private set; }
		public IconCursorImageResourceData ResourceDataTyped { get; internal set; }
		public ResourceData ResourceData { get { return ResourceDataTyped; } }
		
		// Info
		public Size   Size        { get; internal set; }
		public Point  Hotspot     { get; internal set; }
		
		public Byte   ColorCount  { get; internal set; }
		public UInt16 BitCount    { get; internal set; }
		public UInt16 Planes      { get; internal set; }
		
		internal IconImage(Byte[] data, IconGroup parentIcon, FileIconDirectoryEntry entry) {
			
			ImageData  = data;
			ParentIcon = parentIcon;
			
			Size       = new Size ( entry.bWidth   , entry.bHeight );
			Hotspot    = new Point( entry.wXHotspot, entry.wYHotspot );
			
			ColorCount = entry.bColorCount;
			BitCount   = entry.wBitCount;
			Planes     = entry.wPlanes;
			
			Validate();
		}
		
		internal IconImage(Byte[] data, IconGroup parentIcon, ResIconDirectoryEntry entry) {
			
			if(data.Length < 4) throw new ArgumentException("IconImage data must be at least 4 bytes long", "data");
			
			// in resources, the hotspot is the first 2 words of the data
			UInt16 hotspotX = (ushort)(data[0] | data[1] << 8);
			UInt16 hotspotY = (ushort)(data[2] | data[3] << 8);
			
			ImageData  = data;
			ParentIcon = parentIcon;
			
			Size       = new Size ( entry.bWidth, entry.bHeight );
			Hotspot    = new Point( hotspotX    , hotspotY );
			
			ColorCount = entry.bColorCount;
			BitCount   = entry.wBitCount;
			Planes     = entry.wPlanes;
			
			Validate();
		}
		
		private void Validate() {
			
			if     ( Size.Width  == 0 && Size.Height == 0                 ) Size = new Size( 256, 256 );
			else if( Size.Width  == 0                                     ) Size = new Size( 256, Size.Height );
			else if( Size.Height == 0 && ParentIcon.Type == IconType.Icon ) Size = new Size( Size.Width, 256 );
			
			if( ParentIcon.Type == IconType.Icon && BitCount == 0) { // this regularly happens, so let's fix it
				BitCount = (UInt16)Math.Log( ColorCount, 2);
			}
			
			Description = String.Format(
				Cult.InvariantCulture,
				"{0}x{1} {2}-bit",
				Size.Width,
				Size.Height,
				BitCount
			);
			
		}
		
		public override String ToString() {
			return Description;
		}
		
#region Comparable
		
		public Int32 CompareTo(IconImage other) {
			
			if( ParentIcon.Type == IconType.Icon ) {
				
				// sort by color depth first, then size (smallest first)
				
				Int32 color = BitCount.CompareTo( other.BitCount );
				
				if(color != 0) return color;
				
				return -Size.Width.CompareTo( other.Size.Width );
				
			} else {
				
				// sort by size first (largest first), then color depth
				
				Int32 size = Size.Width.CompareTo( other.Size.Width );
				
				if(size != 0) return size;
				
				return BitCount.CompareTo( other.BitCount );;
				
			}
			
		}
		
		Int32 IComparable<IconImage>.CompareTo(IconImage other) {
			
			return CompareTo( other );
		}
		
		public Int32 CompareTo(IDirectoryMember other) {
			
			IconImage other2 = other as IconImage;
			if(other2 == null) return -1;
			
			return CompareTo(other2);
		}
		
#endregion
		
#region Equatable
		
		public Boolean Equals(IDirectoryMember other) {
			return Equals(other as IconImage);
		}
		
		public Boolean Equals(IconImage other) {
			
			if(other == null) return false;
			
			return Size.Equals( other.Size ) &&
			       BitCount.Equals( other.BitCount ) &&
			       ColorCount.Equals( other.ColorCount );
			
		}
		
#endregion
		
	}
	
}
