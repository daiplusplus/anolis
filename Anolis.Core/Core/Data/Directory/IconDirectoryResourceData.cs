using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

using Anolis.Core.Native;
using Anolis.Core.Utility;

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
		
		protected override String GetOpenFileFilter() {
			return Anolis.Core.Utility.Miscellaneous.CreateFileFilter("IconDirectory", "ico");
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			ResIconDir dir = ResIconDirHelper.FromResource(lang, data);
			
			return new IconDirectoryResourceData(dir, lang);
		}
		
		public override string Name {
			get { return "Icon Directory"; }
		}
		
		public override ResourceData FromFileToAdd(Stream stream, String extension, UInt16 lang, ResourceSource currentSource) {
			
			if(extension != "ico") throw new ArgumentException("ico is the only supported extension");
			
			ResIconDir dir = ResIconDirHelper.FromFile(stream, lang, currentSource);
			
			return new IconDirectoryResourceData(dir, null);
		}
		
		public override ResourceData FromFileToUpdate(Stream stream, String extension, ResourceLang currentLang) {
			
			if(extension != "ico") throw new ArgumentException("ico is the only supported extension");
			
			IconDirectoryResourceData originalData = currentLang.Data as IconDirectoryResourceData;
			if(originalData == null) throw new ResourceDataException("Unexpected original data subclass");
			
			ResIconDir original = originalData.IconDirectory;
			
			// Loads the icons in the stream into 'original'
			ResIconDirHelper.FromFile(stream, currentLang.LanguageId, currentLang.Name.Type.Source, original);
			
			return new IconDirectoryResourceData(original, null);
		}
	}
	
	public sealed class IconDirectoryMember : IDirectoryMember, IComparable<IconDirectoryMember>, IEquatable<IconDirectoryMember> {
		
		internal IconDirectoryMember(IconCursorImageResourceData data, Size dimensions, Byte colorCount, Byte reserved, UInt16 planes, UInt16 bitCount, UInt32 size) {
			
			ResourceData = data;
			
			Dimensions   = dimensions;
			ColorCount   = colorCount;
			Reserved     = reserved;
			Planes       = planes;
			BitCount     = bitCount;
			Size         = size;
			
			if( BitCount == 0) { // this regularly happens, so let's fix it
				BitCount = (UInt16)Math.Log( ColorCount, 2);
			}
			
			Description = String.Format(
				Cult.InvariantCulture,
				"{0}x{1} {2}-bit",
				dimensions.Width,
				dimensions.Height,
				BitCount
			);
			
		}
		
		public String       Description  { get; private set; }
		public ResourceData ResourceData { get; private set; }
		
		public Size         Dimensions   { get; private set; }
		public Byte         ColorCount   { get; private set; }
		public Byte         Reserved     { get; private set; }
		public UInt16       Planes       { get; private set; }
		public UInt16       BitCount     { get; private set; }
		public UInt32       Size         { get; private set; }
		
		public override String ToString() {
			return Description;
		}
		
#region Comparable
		
		public Int32 CompareTo(IconDirectoryMember other) {
			
			// sort by color depth first, then size
			
			Int32 color = BitCount.CompareTo( other.BitCount );
			
			if(color != 0) return color;
			
			return -Dimensions.Width.CompareTo( other.Dimensions.Width );
			
		}
		
		Int32 IComparable<IconDirectoryMember>.CompareTo(IconDirectoryMember other) {
			
			return CompareTo( other );
		}
		
		public Int32 CompareTo(IDirectoryMember other) {
			
			IconDirectoryMember other2 = other as IconDirectoryMember;
			if(other2 == null) return -1;
			
			return CompareTo(other2);
		}
		
#endregion
		
		
#region Equatable
		
		public Boolean Equals(IDirectoryMember other) {
			return Equals(other as IconDirectoryMember);
		}
		
		public Boolean Equals(IconDirectoryMember other) {
			
			if(other == null) return false;
			
			return Dimensions.Equals( other.Dimensions ) &&
			       BitCount.Equals( other.BitCount ) &&
			       ColorCount.Equals( other.ColorCount );
			
		}
		
#endregion
		
	}
	
	public sealed class IconDirectoryResourceData : DirectoryResourceData {
		
		internal IconDirectoryResourceData(ResIconDir directory, ResourceLang lang) : base(lang, directory.GetRawData() ) {
			
			if(directory == null) throw new ArgumentNullException("directory");
			
			IconDirectory = directory;
			
			_members = new DirectoryMemberCollection( directory.Members );
		}
		
		public ResIconDir IconDirectory { get; private set; }
		
		protected override String[] SupportedFilters {
			get { return new String[] { "Icon File (*.ico)|*.ico" }; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier( Win32ResourceType.IconDirectory );
		}
		
		protected override void SaveAs(Stream stream, String extension) {
			
			if(extension != "ico") throw new ArgumentException("ico is the only supported extension");
			
			IconDirectory.Save(stream);
		}
		
		private DirectoryMemberCollection _members;
		
		public override DirectoryMemberCollection Members {
			get { return _members; }
		}
		
	}
}