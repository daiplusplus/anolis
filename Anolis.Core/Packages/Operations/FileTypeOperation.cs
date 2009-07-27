using System;
using System.Collections.Generic;
using System.Xml;

using Anolis.Core.Utility;
using System.IO;

using P = System.IO.Path;

namespace Anolis.Core.Packages.Operations {
	
	public class FileTypeOperation : Operation {
		
		private List<FileTypeSetting> _types;
		
		public FileTypeOperation(Group parent, XmlElement element) :  base(parent, element) {
			_types = new List<FileTypeSetting>();
			
			TypeExtension    = element.GetAttribute("typeExt");
			TypeFriendlyName = element.GetAttribute("friendlyName");
			TypeIcon         = element.GetAttribute("icon");
			
			FileTypeSetting setting = new FileTypeSetting() {
				TypeExt      = TypeExtension,
				FriendlyName = TypeFriendlyName,
				Icon         = TypeIcon
			};
			_types.Add( setting );
		}
		
		public String TypeFriendlyName { get; set; }
		public String TypeExtension    { get; set; }
		public String TypeIcon         { get; set; }
		
		public FileTypeOperation(Group parent) :  base(parent) {
			_types = new List<FileTypeSetting>();
			
		}
		
		public override String OperationName {
			get { return "File type"; }
		}
		
		public override void Execute() {
			
			FileAssociations assoc = FileAssociations.GetAssociations();
			
			Backup( assoc, Package.ExecutionInfo.BackupGroup );
			
			DirectoryInfo iconsDir = new DirectoryInfo( PackageUtility.ResolvePath( @"%windir%\Resources\Icons" ) );
			if( !iconsDir.Exists ) iconsDir.Create();
			
			foreach(FileTypeSetting setting in _types) {
				
				if(!setting.TypeExt.StartsWith(".", StringComparison.Ordinal)) {
					Package.Log.Add( LogSeverity.Warning, "Invalid extension, must start with '.': " + setting.TypeExt );
					continue;
				}
				
				FileType      targetType      = null;
				FileExtension targetExtension = null;
				
				// get the type for this extension
				foreach(FileExtension ext in assoc.AllExtensions) {
					if( String.Equals( ext.Extension, setting.TypeExt, StringComparison.OrdinalIgnoreCase ) ) {
						targetExtension = ext;
						break;
					}
				}
				
				if( targetExtension == null ) {
					// create type and extension
					
					Package.Log.Add( LogSeverity.Info, "Extension undefined: \"" + setting.TypeExt + "\". Creating FileType and FileExtension");
					
					targetType = assoc.CreateFileType( assoc.GetUnusedProgIdForExtension( setting.TypeExt ) );
					targetType.FriendlyName = setting.FriendlyName;
					
					targetExtension = assoc.CreateFileExtension( setting.TypeExt );
					targetExtension.FileType = targetType;
					
				} else {
					
					targetType = targetExtension.FileType;
					
					if( targetType == null ) {
						
						// create the type
						
						Package.Log.Add( LogSeverity.Info, "Extension defined : \"" + setting.TypeExt + "\", but FileType undefined. Creating FileType");
						
						targetType = assoc.CreateFileType( assoc.GetUnusedProgIdForExtension( setting.TypeExt ) );
						targetType.FriendlyName = setting.FriendlyName;
						
						targetExtension.FileType = targetType;
						
					}
				}
				
				////////////////////////
				// copy the icon files to %windir%\Resources\Icons
				
				FileInfo iconFile = Package.RootDirectory.GetFile( setting.Icon );
				
				String destinationFileName = P.Combine( iconsDir.FullName, iconFile.Name );
				
				iconFile.CopyTo( destinationFileName, true );
				
				targetType.DefaultIcon = destinationFileName;
				targetType.IsDirty     = true;
			}
			
			assoc.CommitChanges();
			
		}
		
		private void Backup(FileAssociations assoc, Group backupGroup) {
			
			if( backupGroup == null ) return;
			
			// NOTE: Delete the %windir%\resources\Icons directory?
			
			foreach(FileTypeSetting setting in _types) {
				
				FileExtension existingExtension = assoc.GetExtension( setting.TypeExt );
				if( existingExtension == null ) continue;
				
				FileType existingType = existingExtension.FileType;
				if( existingType == null ) continue;
				
				FileTypeOperation op = new FileTypeOperation(backupGroup);
				op.TypeExtension    = setting.TypeExt;
				op.TypeFriendlyName = setting.FriendlyName;
				op.TypeIcon         = existingType.DefaultIcon;
 				
 				backupGroup.Operations.Add( op );
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
			public String FriendlyName;
		}
		
	}
	
	
}
