using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Anolis.Core.Data;
using Anolis.Core.Native;

namespace Anolis.Core.Utility {
	
	// The IconDirectoryResoruceData class was getting into a mess, so I moved the IconDir-handling code to this class
	// ... in much the same way I did with BmpResourceData and the Dib class
	
	/// <summary>Represents an Icon Directory as a Resource, used for reconstructing an IconDirectoryResourceData's RawData</summary>
	public class ResIconDir {
		
		
		private Byte[]  _rawData;
		private Boolean _updated = true;
		
		public ResIconDir(UInt16 lang, ResourceSource source) {
			
			_members = new List<IDirectoryMember>();
			
			Source = source;
			Lang   = lang;
		}
		
		private List<IDirectoryMember> _members;
		
		internal List<IDirectoryMember> Members { get { return _members; } } // this is used as a hack to expose members for DirectoryResourceData.Members, there's probably a better way around it
		
		public ResourceSource Source { get; private set; }
		public UInt16         Lang   { get; private set; }
		
		internal void UnderlyingAdd(IconDirectoryMember existingMember) {
			
			_members.Add( existingMember );
		}
		
		/// <summary>Adds or Updates (replaces) a member image. DO NOT USE to add an entry that already exists as part of a resource</summary>
		public void AddUpdateMemberImage(IconDirectoryMember newMember) {
			
			// it is important to maintain order: color depth ascending then size descending.
				// I've implemented this in IconDirectoryMember.CompareTo and called in GetRawData()
			
			
			// first off, check if it's an add or an update by seeing if the specified member already exists
			
			IconDirectoryMember orig = null;
			if( (orig = FindMember(newMember)) != null ) {
				
				// it already exists, so just update the associated ResourceLang
				ResourceLang lang = orig.ResourceData.Lang;
				lang.SwapData( newMember.ResourceData );
				_members.Add( newMember );
				
				_members.Remove( orig );
				
			} else {
				
				// it's new, so add it
				
				Source.Add( _iconImageTypeId, Source.GetUnusedName(_iconImageTypeId), Lang, newMember.ResourceData );
				_members.Add( newMember );
				
			}
			
			_updated = true;
		}
		
		public void RemoveMemberImage(IconDirectoryMember member) {
			
			if( _members.IndexOf( member) == -1 ) return;
			
			ResourceLang lang = member.ResourceData.Lang;
			
			_members.Remove( member );
			
			Source.Remove( lang );
			
		}
		
		private IconDirectoryMember FindMember(IconDirectoryMember newMember) {
			
			foreach(IconDirectoryMember member in _members) if( member.CompareTo( newMember ) == 0 ) return member;
			
			return null;
		}
		
		private static ResourceTypeIdentifier _iconImageTypeId = new ResourceTypeIdentifier( Win32ResourceType.IconImage );
		
		/// <summary>Recreates the byte array that is used for the IconDirectoryRD's RawData</summary>
		public Byte[] GetRawData() {
			
			if( !_updated ) return _rawData;
			
			_members.Sort(); // I assume this uses the IconDirectoryMember comparatoer
			
			Int32 sizeOfIconDirectory = Marshal.SizeOf(typeof(IconDirectory));
			Int32 sizeOfResIconDirEnt = Marshal.SizeOf(typeof(ResIconDirectoryEntry));
			
			IconDirectory dir = new IconDirectory();
			dir.wReserved = 0;
			dir.wType = 1; // 1 for icons, 0 for cursors
			dir.wCount = (ushort)_members.Count;
			
			Int32 sizeOfData =
				sizeOfIconDirectory +
				sizeOfResIconDirEnt * dir.wCount;
			
			IntPtr p = Marshal.AllocHGlobal( sizeOfData );
			Marshal.StructureToPtr(dir, p, true);
			
			IntPtr q = Inc(p, Marshal.SizeOf(typeof(IconDirectory)));
			
			foreach(IconDirectoryMember member in _members) {
				
				ResIconDirectoryEntry d = new ResIconDirectoryEntry() {
					bWidth       = GetDimension( member.Dimensions.Width ),
					bHeight      = GetDimension( member.Dimensions.Height ),
					bColorCount  = member.ColorCount,
					bReserved    = member.Reserved,
					wPlanes      = member.Planes,
					wBitCount    = member.BitCount,
					dwBytesInRes = member.Size,
					wId          = (ushort)member.ResourceData.Lang.Name.Identifier.NativeId
				};
				
				Marshal.StructureToPtr(d, q, true);
				q = Inc(q, sizeOfResIconDirEnt);
			}
			
			_rawData = new Byte[ sizeOfData ];
			Marshal.Copy(p, _rawData, 0, sizeOfData);
			
			Marshal.FreeHGlobal( p );
			
			return _rawData;
			
		}
		
		private static Byte GetDimension(Int32 dimension) {
			Int32 retval = dimension == 256 ? 0 : dimension;
			return (Byte)retval;
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
		
		///////////////////////////////////
		
		public IconDirectoryMember GetIconForSize(Size size) {
			
			// search for an identical or larger match (larger results can be scaled down)
			
			// sorted by size, then color depth; so the first dimensions match will be of the highest color depth
			IconDirectoryMember[] sortedIcons = new IconDirectoryMember[_members.Count];
			
			_members.CopyTo( sortedIcons, 0 );
			Array.Sort<IconDirectoryMember>(sortedIcons, delegate(IconDirectoryMember x, IconDirectoryMember y) {
				
				// compare y to x because we want it in descending order
				Int32 sizeComparison = y.Dimensions.Width.CompareTo( x.Dimensions.Width );
				return sizeComparison == 0 ? y.BitCount.CompareTo( x.BitCount ) : sizeComparison;
				
			});
			
			IconDirectoryMember bestMatch = null;
			
			foreach(IconDirectoryMember member in sortedIcons) {
				
				if( member.Dimensions == size ) return member;
				
				if(bestMatch == null) bestMatch = member;
				
				if( member.Dimensions.Width > size.Width ) {
					
					if(bestMatch == null) bestMatch = member;
					else {
						
						if( member.Dimensions.Width < bestMatch.Dimensions.Width ) bestMatch = member;
						
					}
				}
			}
			
			return bestMatch;
			
		}
		
		///////////////////////////////////
		
		public void Save(Stream stream) {
			
			BinaryWriter wtr = new BinaryWriter(stream);
			
			Int64 offsetFrom = stream.Position;
			
			// Write IconHeader ( IconDirectory )
			
			wtr.Write( (UInt16)0 ); // wReserved
			wtr.Write( (UInt16)1 ); // wType
			wtr.Write( (UInt16)this._members.Count ); // wCount
			
			// Write out the array of directory entries, calculating the offsets first
			
			UInt32[] offsets = new UInt32[ _members.Count ];
			
			UInt32 offsetFromEndOfDirectory =
				(UInt32)stream.Position + (UInt32)( _members.Count * Marshal.SizeOf(typeof(FileIconDirectoryEntry)) );
			
			for(int i=0;i<_members.Count;i++) {
				
				IconDirectoryMember m = _members[i] as IconDirectoryMember;
				
				// Offset is from the start of the file
				offsets[i] = offsetFromEndOfDirectory;
				
				offsetFromEndOfDirectory += m.Size;
				
			}
			
			for(int i=0;i<_members.Count;i++) {
				
				IconDirectoryMember member = _members[i] as IconDirectoryMember;
				
				wtr.Write( (Byte)member.Dimensions.Width );
				wtr.Write( (Byte)member.Dimensions.Height );
				wtr.Write( member.ColorCount );
				wtr.Write( member.Reserved );
				wtr.Write( member.Planes );
				wtr.Write( member.BitCount );
				wtr.Write( member.Size );
				wtr.Write( offsets[i] );
				
			}
			
			// Write out the actual images
			
			for(int i=0;i<_members.Count;i++) {
				
				IconDirectoryMember m = _members[i] as IconDirectoryMember;
				
				wtr.Write( m.ResourceData.RawData );
				
			}
			
			// and we're done
			
		}
		
	}
}
