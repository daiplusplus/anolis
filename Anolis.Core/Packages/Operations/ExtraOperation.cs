using System;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;

using Microsoft.Win32;

using P = System.IO.Path;

namespace Anolis.Core.Packages.Operations {
	
	public abstract class ExtraOperation : Operation {
		
		protected ExtraOperation(ExtraType type, Package package, Group parent, XmlElement operationElement) : base(package, parent, operationElement) {
			
			String file = operationElement.GetAttribute("path");
			
			file = PackageUtility.ResolvePath( file, package.RootDirectory.FullName );
			
			CreateState( type, file );
		}
		
		protected ExtraOperation(ExtraType type, Package package, Group parent, String path) : base(package, parent, path) {
			
			String file = PackageUtility.ResolvePath( path, package.RootDirectory.FullName );
			
			CreateState( type, file );
		}
		
		private void CreateState(ExtraType type, String file) {
			
			Files = new Collection<String>();
			ExtraType = type;
			
			Files.Add( file );
		}
		
		public override String OperationName {
			get { return ExtraType.ToString(); }
		}
		
		public String             Attribution { get; private set; }
		public ExtraType          ExtraType   { get; private set; }
		public Collection<String> Files       { get; private set; }
		
		public static ExtraOperation Create(Package package, Group parent, XmlElement operationElement) {
			
			String typeStr = operationElement.GetAttribute("type");
			ExtraType type = (ExtraType)Enum.Parse( typeof(ExtraType), typeStr, true );
			
			switch(type) {
				case ExtraType.Wallpaper:
					return new WallpaperExtraOperation(package, parent, operationElement);
				case ExtraType.BootScreen:
					return new BootScreenExtraOperation(package, parent, operationElement);
				case ExtraType.VisualStyle:
					return new VisualStyleExtraOperation(package, parent, operationElement);
				case ExtraType.Screensaver:
					return new ScreensaverExtraOperation(package, parent, operationElement);
				case ExtraType.Program:
					return new ProgramExtraOperation(package, parent, operationElement);
				case ExtraType.Custom:
					return new CustomExtraOperation(package, parent, operationElement);
				default:
					return null;
			}
		}
		
		public override String Key {
			get { return ExtraType.ToString(); }
		}
		
		protected virtual Boolean CanMerge { get { return true; } }
		
		public override Boolean Merge(Operation operation) {
			
			if( !CanMerge ) return false;
			
			ExtraOperation other = operation as ExtraOperation;
			if(other == null) return false;
			if(other.ExtraType != this.ExtraType) return false;
			
			foreach(String file in other.Files) {
				
				if( !this.Files.Contains( file ) ) this.Files.Add( file );
				
			}
			
			return true;
			
		}
		
		public override void Write(XmlElement parent) {
			
			CreateElement(parent, "extra", "type", ExtraType.ToString(), "path", Path );
		}
		
		protected static void MakeRegOp(Group backupGroup, String keyPath, String valueName) {
			
			String v = (String)Registry.GetValue( keyPath, valueName, null );
			
			RegistryOperation op = new RegistryOperation( backupGroup.Package, backupGroup );
			op.RegKey   = keyPath;
			op.RegName  = valueName;
			op.RegValue = v;
			op.RegKind  = RegistryValueKind.String;
			
			backupGroup.Operations.Add( op );
			
		}
		
	}
	
	public enum ExtraType {
		None,
		Wallpaper,
		BootScreen,
		VisualStyle,
		Screensaver,
		Program,
		Registry,
		Custom
	}
	
	
	
}
