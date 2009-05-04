using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

using Anolis.Core.Native;
using Anolis.Core.Utility;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Data {
	
	public class CursorDirectoryResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if(typeId.KnownType == Win32ResourceType.CursorDirectory) return Compatibility.Yes;
			
			return Compatibility.No;
			
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			
			if( IsExtension( filenameExtension, "cur" ) ) return Compatibility.Yes;
			
			return Compatibility.No;
			
		}
		
		protected override String GetOpenFileFilter() {
			return CreateFileFilter("CursorDirectory", "cur");
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			ResIconDir dir = ResIconDirHelper.FromResource(lang, data);
			
			return new CursorDirectoryResourceData(dir, lang);
		}
		
		public override string Name {
			get { return "Cursor Directory"; }
		}
		
		public override ResourceData FromFileToAdd(Stream stream, String extension, UInt16 lang, ResourceSource currentSource) {
			
			if( !IsExtension( extension, "cur" ) ) throw new ArgumentException("cur is the only supported extension");
			
			ResIconDir dir = ResIconDirHelper.FromFile( false, stream, lang, currentSource);
			
			return new CursorDirectoryResourceData(dir, null);
		}
		
		public override ResourceData FromFileToUpdate(Stream stream, String extension, ResourceLang currentLang) {
			
			if( !IsExtension( extension, "ico" ) ) throw new ArgumentException("cur is the only supported extension");
			
			CursorDirectoryResourceData originalData = currentLang.Data as CursorDirectoryResourceData;
			if(originalData == null) throw new ResourceDataException("Unexpected original data subclass");
			
			ResIconDir original = originalData.IconDirectory;
			
			// Loads the icons in the stream into 'original'
			ResIconDirHelper.FromFile(stream, currentLang.LanguageId, currentLang.Name.Type.Source, original);
			
			return new CursorDirectoryResourceData(original, null);
		}
	}
	
	public sealed class CursorDirectoryResourceData : DirectoryResourceData {
		
		internal CursorDirectoryResourceData(ResIconDir directory, ResourceLang lang) : base(lang, directory.GetRawData() ) {
			
			if(directory == null) throw new ArgumentNullException("directory");
			
			IconDirectory = directory;
			
			_members = new DirectoryMemberCollection( directory.Members );
		}
		
		public ResIconDir IconDirectory { get; private set; }
		
		protected override String[] SupportedFilters {
			get { return new String[] { "Cursor File (*.cur)|*.cur" }; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier( Win32ResourceType.CursorDirectory );
		}
		
		protected override void SaveAs(Stream stream, String extension) {
			
			if(extension != "cur") throw new ArgumentException("cur is the only supported extension");
			
			IconDirectory.Save(stream);
		}
		
		private DirectoryMemberCollection _members;
		
		public override DirectoryMemberCollection Members {
			get { return _members; }
		}
		
	}
}