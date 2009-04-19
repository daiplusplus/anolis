using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

using Anolis.Core.Native;
using Anolis.Core.Data;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Utility {
	
	public static class ResIconDirHelper {
		
#region From Resource
		
		public static ResIconDir FromResource(ResourceLang lang, Byte[] rawBytes) {
			
			Int32 sizeOfIconDir = Marshal.SizeOf(typeof(IconDirectory));
			Int32 sizeOfDirEntr = Marshal.SizeOf(typeof(ResIconDirectoryEntry));
			
			// the data in here is an ICONDIR structure
			
			IntPtr p = Marshal.AllocHGlobal(rawBytes.Length);
			Marshal.Copy(rawBytes, 0, p, rawBytes.Length);
			
			// this could be vastly simplified by correctly marshaling the member array of IconDirectory
			// but that's a can of worms, I'd rather not
			IconDirectory dir = (IconDirectory)Marshal.PtrToStructure(p, typeof(IconDirectory));
			
			Marshal.FreeHGlobal( p );
			
			if(dir.wType != 1) throw new InvalidOperationException("Provided rawData was not that of an icon's");
			
			ResIconDirectoryEntry[] subImages = new ResIconDirectoryEntry[dir.wCount];
			
			for(int i=0;i<dir.wCount;i++) {
				
				Int32 byteOffset = sizeOfIconDir + sizeOfDirEntr * i;
				
				p = Marshal.AllocHGlobal( sizeOfDirEntr );
				Marshal.Copy(rawBytes, byteOffset, p, sizeOfDirEntr );
				
				ResIconDirectoryEntry img = (ResIconDirectoryEntry)Marshal.PtrToStructure(p, typeof(ResIconDirectoryEntry));
				
				subImages[i] = img;
			}
			
			ResIconDir retval = new ResIconDir(lang.LanguageId, lang.Name.Type.Source);
			
			// then we might be able to get the resourcedata for the subimages to include in the directory
			
			// find the Icon Image resource type
			ResourceType iconType = null;
			
			foreach(ResourceType type in lang.Name.Type.Source.AllTypes) {
				if(type.Identifier.KnownType == Win32ResourceType.IconImage) {
					iconType = type;
					break;
				}
			}
			
			if(iconType != null) {
				
				foreach(ResIconDirectoryEntry img in subImages) {
					
					IconCursorImageResourceData rd = GetDataFromWid(iconType, lang, img.wId);
					
					Size dimensions = new Size( img.bWidth == 0 ? 256 : img.bWidth, img.bHeight == 0 ? 256 : img.bHeight );
					
					retval.Members.Add( new IconDirectoryMember(rd, dimensions, img.bColorCount, img.bReserved, img.wPlanes, img.wBitCount, img.dwBytesInRes ) ); 
					
				}
				
			}
			
			return retval;
		}
		
		private static IconCursorImageResourceData GetDataFromWid(ResourceType type, ResourceLang thisLang, ushort wId) {
			
			ResourceName name = null;
			
			foreach(ResourceName nom in type.Names) {
				if(nom.Identifier.IntegerId == wId) { name = nom; break; }
			}
			
			if(name == null) throw new AnolisException("Directory Subimage name not found"); // this has never happened in the past btw
			
			ResourceLang lang = null;
			
			foreach(ResourceLang lan in name.Langs) {
				if(thisLang.LanguageId == 0) { // 0 matches any language, so go with the first
					lang = lan;
					break;
				}
				if(lan.LanguageId == thisLang.LanguageId) {
					lang = lan;
					break;
				} // because a ResourceName can have multiple languages associated with it. I'm after the ResourceLang's resource data related to this Directory's lang
			}
			
			if(lang == null) throw new AnolisException("Directory Subimage language not found"); // this has never happened in the past btw
			
			IconCursorImageResourceData data = lang.Data as IconCursorImageResourceData;
			
			return data;
			
		}
		
#endregion
		
#region From File
		
		public static void FromFile(Stream stream, UInt16 lang, ResourceSource source, ResIconDir resDir) {
			
			if(stream.Length < Marshal.SizeOf(typeof(IconDirectory))) return;
			
			BinaryReader rdr = new BinaryReader(stream);
			
			IconDirectory? tDir = ReadIcoHeader( rdr );
			if(tDir == null) return;
			
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
			
			for(int i=0;i<dir.wCount;i++) {
				
				FileIconDirectoryEntry img = subImages[i];
				
				stream.Seek(img.dwImageOffset, SeekOrigin.Begin);
				
				Byte[] data = new Byte[ img.dwBytesInRes ];
				
				stream.Read(data, 0, (int)img.dwBytesInRes);
				
				Size dimensions = new Size( img.bWidth == 0 ? 256 : img.bWidth, img.bHeight == 0 ? 256 : img.bHeight );
				
				IconCursorImageResourceData memberImageData = factory.FromResource(null, data) as IconCursorImageResourceData;
				IconDirectoryMember         member          = new IconDirectoryMember(memberImageData, dimensions, img.bColorCount, img.bReserved, img.wPlanes, img.wBitCount, img.dwBytesInRes);
				
				resDir.AddUpdateMemberImage( member );
			}
			
		}
		
		public static ResIconDir FromFile(Stream stream, UInt16 lang, ResourceSource source) {
			
			ResIconDir retval = new ResIconDir(lang, source);
			
			FromFile(stream, lang, source, retval);
			
			return retval;
			
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
		
		private static IconImageResourceDataFactory _factory;
		
		private static IconImageResourceDataFactory GetIconImageFactory() {
			
			if( _factory != null ) return _factory;
			
			ResourceTypeIdentifier typeId = new ResourceTypeIdentifier( new IntPtr( (int)Win32ResourceType.IconImage ) );
			
			ResourceDataFactory[] factories = ResourceDataFactory.GetFactoriesForType( typeId );
			
			foreach(ResourceDataFactory f in factories) {
				
				IconImageResourceDataFactory iif = f as IconImageResourceDataFactory;
				if(iif != null) return _factory = iif;
				
			}
			
			return _factory;
		}
		
#endregion
		
	}
}
