using System;
using System.Collections.Generic;
using System.Xml;

using Anolis.Core.Utility;

namespace Anolis.Core.Packages.Operations {
	
	public class FileTypeOperation : Operation {
		
		private List<FileTypeSetting> _types;
		
		public FileTypeOperation(Package package, XmlElement element) :  base(package, element) {
			_types = new List<FileTypeSetting>();
			
			String type = element.GetAttribute("type");
			String icon = element.GetAttribute("path");
			
			FileTypeSetting set = new FileTypeSetting() { Type = type, Icon = icon };
			
		}
		
		protected override string OperationName {
			get { return "File type"; }
		}
		
		public override void Execute() {
			
			FileAssociations assoc = FileAssociations.GetAssoctiations();
			
			foreach(FileTypeSetting setting in _types) {
				
				FileType type = null;
				
				if( setting.Type.StartsWith(".") ) {
					// get the extension then get its type
					
					foreach(FileExtension ext in assoc.AllExtensions) {
						if( String.Equals( ext.Extension, setting.Type, StringComparison.OrdinalIgnoreCase ) ) {
							type = ext.FileType;
							break;
						}
					}
					
					if( type == null ) Package.Log.Add( LogSeverity.Warning, "Could not find Type for ext");
					
				}
				
			}
			
			assoc.CommitChanges();
			
		}
		
		public override bool Merge(Operation operation) {
			
			FileTypeOperation other = operation as FileTypeOperation;
			if(other == null) return false;
			
			_types.AddRange( other._types );
			
			return true;
		}
		
		private class FileTypeSetting {
			
			public String Type;
			public String Icon;
		}
		
	}
	
	
}
