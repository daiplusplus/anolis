using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

using Anolis.Core.Native;
using Anolis.Core.Utility;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Data {
	
	public class CursorDirectoryResourceDataFactory : DirectoryResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if(typeId.KnownType == Win32ResourceType.CursorDirectory) return Compatibility.Yes;
			
			return Compatibility.No;
			
		}
		
		public override Compatibility HandlesExtension(String fileNameExtension) {
			
			if( IsExtension( fileNameExtension, "CUR" ) ) return Compatibility.Yes;
			
			return Compatibility.No;
			
		}
		
		public override String OpenFileFilter {
			get { return CreateFileFilter("CursorDirectory", "cur"); }
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			IconGroup group = new IconGroup(lang, data);
			
			return new CursorDirectoryResourceData(group, lang);
		}
		
		public override string Name {
			get { return "Cursor Directory"; }
		}
		
		public override ResourceData FromFileToAdd(Stream stream, String extension, UInt16 langId, ResourceSource currentSource) {
			
			if( !IsExtension( extension, "CUR" ) ) throw new ArgumentException("cur is the only supported extension");
			
			IconGroup group = new IconGroup(stream);
			group.BindToSource( currentSource, langId );
			
			return new CursorDirectoryResourceData(group, null);
		}
		
		public override ResourceData FromFileToUpdate(Stream stream, String extension, ResourceLang currentLang) {
			
			if( !IsExtension( extension, "CUR" ) ) throw new ArgumentException("cur is the only supported extension");
			
			CursorDirectoryResourceData originalData = currentLang.Data as CursorDirectoryResourceData;
			if(originalData == null) throw new ResourceDataException("Unexpected original data subclass");
			
			IconGroup newGroup = new IconGroup(stream);
			
			IconGroup canonicalGroup = originalData.IconGroup;
			
			canonicalGroup.Merge( newGroup, GetMergeOptions() );
			
			return new CursorDirectoryResourceData(canonicalGroup, null);
		}
	}
	
	public sealed class CursorDirectoryResourceData : DirectoryResourceData {
		
		internal CursorDirectoryResourceData(IconGroup group, ResourceLang lang) : base(lang, group.GetResDirectoryData() ) {
			
			if(group == null) throw new ArgumentNullException("group");
			
			IconGroup = group;
			
		}
		
		public IconGroup IconGroup { get; private set; }
		
		protected override String[] SupportedFilters {
			get { return new String[] { "Cursor File (*.cur)|*.cur" }; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier( Win32ResourceType.CursorDirectory );
		}
		
		protected override void SaveAs(Stream stream, String extension) {
			
			if( !IsExtension( extension, "CUR") ) throw new ArgumentException("cur is the only supported extension");
			
			IconGroup.Save(stream);
		}
		
		public override DirectoryMemberCollection Members {
			get { return IconGroup.Members; }
		}
		
	}
}