using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

using Anolis.Core.Native;
using Anolis.Core.Utility;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Data {
	
	public class IconDirectoryResourceDataFactory : DirectoryResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if(typeId.KnownType == Win32ResourceType.IconDirectory) return Compatibility.Yes;
			
			return Compatibility.No;
			
		}
		
		public override Compatibility HandlesExtension(String fileNameExtension) {
			
			if( IsExtension( fileNameExtension, "ICO" ) ) return Compatibility.Yes;
			
			return Compatibility.No;
			
		}
		
		public override String OpenFileFilter {
			get { return CreateFileFilter("IconDirectory", "ICO"); }
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			IconGroup group = new IconGroup(lang, data);
			
			return new IconDirectoryResourceData(group, lang);
		}
		
		public override string Name {
			get { return "Icon Directory"; }
		}
		
		public override ResourceData FromFileToAdd(Stream stream, String extension, UInt16 langId, ResourceSource currentSource) {
			
			if( !IsExtension( extension, "ICO" ) ) throw new ArgumentException("ico is the only supported extension");
			
			IconGroup group = new IconGroup( stream );
			group.BindToSource( currentSource, langId );
			
			return new IconDirectoryResourceData(group, null);
		}
		
		public override ResourceData FromFileToUpdate(Stream stream, String extension, ResourceLang currentLang) {
			
			if( !IsExtension( extension, "ICO" ) ) throw new ArgumentException("ico is the only supported extension");
			
			IconDirectoryResourceData originalData = currentLang.Data as IconDirectoryResourceData;
			if(originalData == null) throw new ResourceDataException("Unexpected original data subclass");
			
			IconGroup newGroup = new IconGroup(stream);
			
			IconGroup canonicalGroup = originalData.IconGroup;
			
			canonicalGroup.Merge( newGroup, GetMergeOptions() );
			
			return new IconDirectoryResourceData( canonicalGroup, null );
		}
	}
	
	public sealed class IconDirectoryResourceData : DirectoryResourceData {
		
		internal IconDirectoryResourceData(IconGroup icon, ResourceLang lang) : base(lang, icon.GetResDirectoryData() ) {
			
			IconGroup = icon;
		}
		
		public IconGroup IconGroup { get; private set; }
		
		protected override String[] SupportedFilters {
			get { return new String[] { "Icon File (*.ico)|*.ico" }; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier( Win32ResourceType.IconDirectory );
		}
		
		protected override void SaveAs(Stream stream, String extension) {
			
			if( !IsExtension(extension, "ICO") ) throw new ArgumentException("ico is the only supported extension");
			
			IconGroup.Save(stream);
		}
		
		public override DirectoryMemberCollection Members {
			get { return IconGroup.Members; }
		}
		
	}
}