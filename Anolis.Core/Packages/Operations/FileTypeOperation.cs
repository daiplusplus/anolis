using System;
using System.Collections.Generic;
using System.Xml;

using Anolis.Core.Utility;
using System.IO;

using P = System.IO.Path;

namespace Anolis.Core.Packages.Operations {
	
	public class FileTypeOperation : Operation {
		
		private List<FileTypeSetting> _types;
		
		public FileTypeOperation(Package package, Group parent, XmlElement element) :  base(package, parent, element) {
			_types = new List<FileTypeSetting>();
			
			String type = element.GetAttribute("typeExt");
			String icon = element.GetAttribute("icon");
			
			FileTypeSetting set = new FileTypeSetting() { TypeExt = type, Icon = icon };
			
		}
		
		public override String OperationName {
			get { return "File type"; }
		}
		
		public override void Execute() {
			
			DirectoryInfo iconsDir = new DirectoryInfo( PackageUtility.ResolvePath( @"%windir%\Resources\Icons" ) );
			if( !iconsDir.Exists ) iconsDir.Create();
			
			FileAssociations assoc = FileAssociations.GetAssoctiations();
			
			foreach(FileTypeSetting setting in _types) {
				
				if(!setting.TypeExt.StartsWith(".", StringComparison.Ordinal)) {
					Package.Log.Add( LogSeverity.Warning, "Invalid extension, must start with '.': " + setting.TypeExt );
					continue;
				}
				
				FileType type = null;
				
				// get the type for this extension
				foreach(FileExtension ext in assoc.AllExtensions) {
					if( String.Equals( ext.Extension, setting.TypeExt, StringComparison.OrdinalIgnoreCase ) ) {
						type = ext.FileType;
						break;
					}
				}
				
				if( type == null ) {
					Package.Log.Add( LogSeverity.Warning, "Could not find Type for ext \"" + setting.TypeExt + '"');
					continue;
				}
				
				////////////////////////
				
				// copy the icon files to %windir%\Resources\Icons
				
				FileInfo iconFile = Package.RootDirectory.GetFile( setting.Icon );
				
				iconFile.CopyTo( P.Combine( iconsDir.FullName, iconFile.Name ), true );
				
				type.DefaultIcon = iconFile.FullName;
			}
			
			assoc.CommitChanges();
			
		}
		
		private void Backup(Group backupGroup) {
			
			if( backupGroup == null ) return;
			
			foreach(FileTypeSetting setting in _types) {
				
				
				
			}
			
		}
		
		public override void Write(XmlElement parent) {
			
			foreach(FileTypeSetting setting in _types) {
				
				// TODO: this will fail if there's more than one _type since CreateElement will add the same Id, Name, Desc, and DescImgPath
				CreateElement(parent, "filetype", "typeExt", setting.TypeExt, "icon", setting.Icon);
			}
			
		}
		
		public override bool Merge(Operation operation) {
			
			FileTypeOperation other = operation as FileTypeOperation;
			if(other == null) return false;
			
			_types.AddRange( other._types );
			
			return true;
		}
		
		private class FileTypeSetting {
			
			public String TypeExt;
			public String Icon;
		}
		
	}
	
	
}
