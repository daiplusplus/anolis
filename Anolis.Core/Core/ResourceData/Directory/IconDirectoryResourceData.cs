using System;
using System.Drawing;
using System.Runtime.InteropServices;

using Anolis.Core.NativeTypes;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Data {
	
	public sealed class IconDirectoryMember : IDirectoryMember {
		
		internal IconDirectoryMember(String description, IconImageResourceData data) {
			
			Description  = description;
			ResourceData = data;
		}
		
		public String       Description  { get; private set; }
		public ResourceData ResourceData { get; private set; }
	}
	
	public sealed class IconDirectoryResourceData : DirectoryResourceData {
		
		private IconDirectoryResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] rawData, out ResourceData typed) {
			
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
			
			if(lang != null) {
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
						
						IconImageResourceData rd = GetDataFromWid(iconType, lang, img.wId);
						
						UInt32 nofColors = (img.bColorCount == 0) ? (UInt32)Math.Pow(2, img.wBitCount) : img.bColorCount;
						
						String description = String.Format(Cult.InvariantCulture, "{0} x {1} ({2} colors) #{3}; {4} bytes", img.bWidth, img.bHeight, nofColors, img.wId, img.dwBytesInRes);
						
						retval.UnderlyingMembers.Add( new IconDirectoryMember(description, rd) );
						
					}
					
				}
				
			} else {
				// what happens here? I dunno, lol
			}
			
			typed = retval;
			return true;
			
		}
		
		private static IconImageResourceData GetDataFromWid(ResourceType type, ResourceLang thisLang, ushort wId) {
			
			ResourceName name = null;
			
			foreach(ResourceName nom in type.Names) {
				if(nom.Identifier.IntegerId == wId) { name = nom; break; }
			}
			
			if(name == null) return null;
			
			ResourceLang lang = null;
			
			foreach(ResourceLang lan in name.Langs) {
				if(lan.LanguageId == thisLang.LanguageId) { lang = lan; break; } // because a ResourceName can have multiple languages associated with it. I'm after the ResourceLang's resource data related to this Directory's lang
			}
			
			IconImageResourceData data = lang.Data as IconImageResourceData;
			
			return data;
			
		}
		
		public override string FileFilter {
			get { return "Icon File (*.ico)|*.ico"; }
		}
		
	}
}