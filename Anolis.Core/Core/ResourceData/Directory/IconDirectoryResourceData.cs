using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

using Anolis.Core.NativeTypes;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Data {
	
	public class IconDirectoryResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if(typeId.KnownType == Win32ResourceType.IconDirectory) return Compatibility.Yes;
			
			return Compatibility.No;
			
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			
			if(filenameExtension == "ico") return Compatibility.Yes;
			
			return Compatibility.No;
			
		}
		
		public override String OpenFileFilter {
			get { return "Icon File (*.ico)|*.ico"; }
		}
		
		public override ResourceData FromResource(ResourceLang lang, byte[] data) {
			
			IconDirectoryResourceData rd;
			if( IconDirectoryResourceData.TryCreateFromRes(lang, data, out rd) ) return rd;
			
			return null;
			
		}
		
		public override ResourceData FromFile(Stream stream, String extension, ResourceSource source) {
			
			IconDirectoryResourceData rd;
			if( IconDirectoryResourceData.TryCreateFromFile(stream, extension, source, out rd) ) return rd;
			
			return null;
			
		}
		
		public override string Name {
			get { return "Icon Directory"; }
		}
	}
	
	public sealed class IconDirectoryMember : IDirectoryMember {
		
		internal IconDirectoryMember(String description, IconCursorImageResourceData data) {
			
			Description  = description;
			ResourceData = data;
		}
		
		public String       Description  { get; private set; }
		public ResourceData ResourceData { get; private set; }
	}
	
	public sealed class IconDirectoryResourceData : DirectoryResourceData {
		
		private IconDirectoryResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		public static Boolean TryCreateFromRes(ResourceLang lang, Byte[] rawData, out IconDirectoryResourceData typed) {
			
			Int32 sizeOfIconDir = Marshal.SizeOf(typeof(IconDirectory));
			Int32 sizeOfDirEntr = Marshal.SizeOf(typeof(ResIconDirectoryEntry));
			
			// the data in here is an ICONDIR structure
			
			IntPtr p = Marshal.AllocHGlobal(rawData.Length);
			Marshal.Copy(rawData, 0, p, rawData.Length);
			
			// this could be vastly simplified by correctly marshaling the member array of IconDirectory
			// but that's a can of worms, I'd rather not
			IconDirectory dir = (IconDirectory)Marshal.PtrToStructure(p, typeof(IconDirectory));
			
			Marshal.FreeHGlobal( p );
			
			if(dir.wType != 1) throw new InvalidOperationException("Provided rawData was not that of an icon's");
			
			ResIconDirectoryEntry[] subImages = new ResIconDirectoryEntry[dir.wCount];
			
			for(int i=0;i<dir.wCount;i++) {
				
				Int32 byteOffset = sizeOfIconDir + sizeOfDirEntr * i;
				
				p = Marshal.AllocHGlobal( sizeOfDirEntr );
				Marshal.Copy( rawData, byteOffset, p, sizeOfDirEntr );
				
				ResIconDirectoryEntry img = (ResIconDirectoryEntry)Marshal.PtrToStructure(p, typeof(ResIconDirectoryEntry));
				
				subImages[i] = img;
			}
			
			IconDirectoryResourceData retval = new IconDirectoryResourceData(lang, rawData);
		
			// then we might be able to get the resourcedata for the subimages to include in the directory
			
			// find the Icon Image resource type
			ResourceType iconType = null;
			
			foreach(ResourceType type in lang.Name.Type.Source.Types) {
				if(type.Identifier.KnownType == Win32ResourceType.IconImage) {
					iconType = type;
					break;
				}
			}
			
			if(iconType != null) {
				
				foreach(ResIconDirectoryEntry img in subImages) {
					
					IconCursorImageResourceData rd = GetDataFromWid(iconType, lang, img.wId);
					
					String description = String.Format(
						Cult.InvariantCulture,
						"{0}x{1} {2}-bit",
						img.bWidth == 0 ? 256 : img.bWidth,
						img.bHeight == 0 ? 256 : img.bHeight,
						img.wBitCount
					);
					
					retval.UnderlyingMembers.Add( new IconDirectoryMember(description, rd) );
					
				}
				
			}
			
			typed = retval;
			return true;
			
		}
		
		private static IconCursorImageResourceData GetDataFromWid(ResourceType type, ResourceLang thisLang, ushort wId) {
			
			ResourceName name = null;
			
			foreach(ResourceName nom in type.Names) {
				if(nom.Identifier.IntegerId == wId) { name = nom; break; }
			}
			
			if(name == null) return null;
			
			ResourceLang lang = null;
			
			foreach(ResourceLang lan in name.Langs) {
				if(lan.LanguageId == thisLang.LanguageId) { lang = lan; break; } // because a ResourceName can have multiple languages associated with it. I'm after the ResourceLang's resource data related to this Directory's lang
			}
			
			IconCursorImageResourceData data = lang.Data as IconCursorImageResourceData;
			
			return data;
			
		}
		
		public static Boolean TryCreateFromFile(Stream stream, String extension, ResourceSource source, out IconDirectoryResourceData typed) {
			
			typed = null;
			
			if(extension != "ico") throw MEEx(extension);
			
			if(stream.Length < Marshal.SizeOf(typeof(IconDirectory))) return false;
			
			BinaryReader rdr = new BinaryReader(stream);
			
			IconDirectory? tDir = ReadIcoHeader( rdr );
			if(tDir == null) return false;
			
			IconDirectory dir = tDir.Value;
			
			///////////////////////////////
			
			// rdr is now at the beginning of the array of FileIconDirectoryMembers
			
			FileIconDirectoryEntry[] subImages = new FileIconDirectoryEntry[dir.wCount];
			
			for(int i=0;i<dir.wCount;i++) {
				
				subImages[i] = ReadFileDirMember(rdr);
			}
			
			/////////////////////////////
			
			// now for image data itself
			
			IconImageResourceDataFactory factory = GetIconImageFactory();
			
			IconCursorImageResourceData[] images = new IconCursorImageResourceData[ dir.wCount ];
			String[]                      descs  = new String[ dir.wCount ];
			
			for(int i=0;i<dir.wCount;i++) {
				
				FileIconDirectoryEntry img = subImages[i];
				
				stream.Seek(img.dwImageOffset, SeekOrigin.Begin);
				
				Byte[] data = new Byte[ img.dwBytesInRes ];
				
				stream.Read(data, 0, (int)img.dwBytesInRes);
				
				images[i] = factory.FromResource(null, data) as IconCursorImageResourceData;
				
				String description = String.Format(
					Cult.InvariantCulture,
					"{0}x{1} {2}-bit",
					img.bWidth == 0 ? 256 : img.bWidth,
					img.bHeight == 0 ? 256 : img.bHeight,
					img.wBitCount
				);
				
				descs[i] = description;
				
			}
			
			Byte[] reconstructed = ReconstructRawData(dir, subImages, images, source);
			
			IconDirectoryResourceData retval = new IconDirectoryResourceData(null, reconstructed);
			
			for(int i=0;i<images.Length;i++) {
				
				retval.UnderlyingMembers.Add( new IconDirectoryMember( descs[i], images[i] ) );
			}
			
			
			/////////////////////////////
			
			typed = retval;
			
			return true;
			
		}
		
		/// <summary>Checks the ICO Header is okay and returns the number of images.</summary>
		private static IconDirectory? ReadIcoHeader(BinaryReader rdr) {
			
			IconDirectory dir;
			
			dir.wReserved = rdr.ReadUInt16();
			dir.wType     = rdr.ReadUInt16();
			dir.wCount    = rdr.ReadUInt16();
			
			if( dir.wReserved != 0 ) return null;
			if( dir.wType     != 1 ) return null;
			
			return dir;
			
		}
		
		private static FileIconDirectoryEntry ReadFileDirMember(BinaryReader rdr) {
			
			FileIconDirectoryEntry file;
			
			file.bWidth        = rdr.ReadByte();
			file.bHeight       = rdr.ReadByte();
			file.bColorCount   = rdr.ReadByte();
			file.bReserved     = rdr.ReadByte();
			
			file.wPlanes       = rdr.ReadUInt16();
			file.wBitCount     = rdr.ReadUInt16();
			
			file.dwBytesInRes  = rdr.ReadUInt32();
			file.dwImageOffset = rdr.ReadUInt32();
			
			return file;
			
		}
		
		private static IconImageResourceDataFactory GetIconImageFactory() {
			
			IconImageResourceDataFactory factory = null;
			
			ResourceTypeIdentifier typeId = new ResourceTypeIdentifier( new IntPtr( (int)Win32ResourceType.IconImage ) );
			
			ResourceDataFactory[] factories = ResourceDataFactory.GetFactoriesForType( typeId );
			
			foreach(ResourceDataFactory f in factories) {
				
				if(f is IconImageResourceDataFactory) {
					
					factory = f as IconImageResourceDataFactory;
					break;
				}
			}
			
			if(factory == null) throw ME( new ApplicationException("Unable to locate IconImageResourceDataFactory") );
			
			return factory;
		}
		
		private static Byte[] ReconstructRawData(IconDirectory dir, FileIconDirectoryEntry[] ents, IconCursorImageResourceData[] images, ResourceSource source) {
			
			Int32 sizeOfIconDirectory = Marshal.SizeOf(typeof(IconDirectory));
			Int32 sizeOfResIconDirEnt = Marshal.SizeOf(typeof(ResIconDirectoryEntry));
			
			Int32 sizeOfData =
				sizeOfIconDirectory +
				sizeOfResIconDirEnt * dir.wCount;
			
			IntPtr p = Marshal.AllocHGlobal( sizeOfData );
			Marshal.StructureToPtr(dir, p, true);
			
			IntPtr q = Inc(p, Marshal.SizeOf(typeof(IconDirectory)));
			
			for(int i=0;i<ents.Length;i++) {
				
				FileIconDirectoryEntry e = ents[i];
				
				ResourceTypeIdentifier iconImageTypeId = new ResourceTypeIdentifier( new IntPtr( (int)Win32ResourceType.IconImage ) );
				ResourceIdentifier nameId = source.GetUnusedName(iconImageTypeId);
				source.Add(iconImageTypeId, nameId, 1033, images[i]);
				
				ResIconDirectoryEntry d = new ResIconDirectoryEntry() {
					bWidth       = e.bWidth,
					bHeight      = e.bHeight,
					bColorCount  = e.bColorCount,
					bReserved    = e.bReserved,
					wPlanes      = e.wPlanes,
					wBitCount    = e.wPlanes,
					dwBytesInRes = e.dwBytesInRes,
					wId          = (ushort)images[i].Lang.Name.Identifier.NativeId
				};
				
				Marshal.StructureToPtr(d, q, true);
				q = Inc(q, sizeOfResIconDirEnt);
			}
			
			Byte[] data = new Byte[ sizeOfData ];
			Marshal.Copy(p, data, 0, sizeOfData);
			
			Marshal.FreeHGlobal( p );
			
			return data;
			
			// Major problem:
			// RawData of a directory contains the ResourceNames of the entries in the ResourceSource
			// seeming as this source does not exist, what should go in their place?
			
			// idea:
			// when a ResourceData is created from a file it is passed the current ResourceSource which it can 'preliminarily add' the datas to
			//	a UI will need to be presented to prompt the user for details if not in Simple Mode. A UI is presented anyway, so this works.
			
			// this way the IconCursorImageResourceData has been added to the ResourceSource (with the Action.Add property) and this method can
			//	just query ResourceName. Simple (sort-of)
			
		}
		
		private static IntPtr Inc(IntPtr p, Int32 amount) {
			
			if( IntPtr.Size == 4 ) {
				
				return new IntPtr( p.ToInt32() + amount );
				
			} else if(IntPtr.Size == 8) {
				
				return new IntPtr( p.ToInt64() + amount );
				
			} else {
				throw new Exception("Unsupported IntPtr Size");
			}
			
		}
		
		protected override String[] SupportedFilters {
			get { return new String[] { "Icon File (*.ico)|*.ico" }; }
		}
		
		protected override void SaveAs(Stream stream, String extension) {
			
			if(extension != "ico") throw MEEx(extension);
			
			BinaryWriter wtr = new BinaryWriter(stream);
			
			
			
		}
		
	}
}