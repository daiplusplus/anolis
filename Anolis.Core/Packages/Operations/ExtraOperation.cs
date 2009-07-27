using System;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;

using Microsoft.Win32;

using P = System.IO.Path;

namespace Anolis.Core.Packages.Operations {
	
	public abstract class ExtraOperation : PathOperation {
		
		protected ExtraOperation(ExtraType type, Group parent, XmlElement operationElement) : base(parent, operationElement) {
			
			String file = operationElement.GetAttribute("path");
			
			if( !String.IsNullOrEmpty( file ) ) file = PackageUtility.ResolvePath( file,parent.Package.RootDirectory.FullName );
			
			CreateState( type, file );
		}
		
		protected ExtraOperation(ExtraType type, Group parent, String path) : base(parent, path) {
			
			String file = null;
			if( path != null ) file = PackageUtility.ResolvePath( path, parent.Package.RootDirectory.FullName );
			
			CreateState( type, file );
		}
		
		private void CreateState(ExtraType type, String file) {
			
			Files = new Collection<String>();
			ExtraType = type;
			
			if( file != null ) {
				
				Files.Add( file );
			}
		}
		
		public override String OperationName {
			get { return ExtraType.ToString(); }
		}
		
		public String             Attribution { get; private set; }
		public ExtraType          ExtraType   { get; private set; }
		public Collection<String> Files       { get; private set; }
		
		public static ExtraOperation Create(Group parent, XmlElement operationElement) {
			
			String typeStr = operationElement.GetAttribute("type");
			ExtraType type = (ExtraType)Enum.Parse( typeof(ExtraType), typeStr, true );
			
			switch(type) {
				case ExtraType.Wallpaper:
					return new WallpaperExtraOperation(parent, operationElement);
				case ExtraType.BootScreen:
					return new BootScreenExtraOperation(parent, operationElement);
				case ExtraType.VisualStyle:
					return new VisualStyleExtraOperation(parent, operationElement);
				case ExtraType.Screensaver:
					return new ScreensaverExtraOperation(parent, operationElement);
				case ExtraType.Custom:
					return new CustomExtraOperation(parent, operationElement);
				case ExtraType.RunOnce:
					return new RunOnceExtraOperation(parent, operationElement);
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
			
			CreateElement(parent, "extra",
				"type"   , ExtraType.ToString(),
				"path"   , Path
			);
		}
		
		protected static void MakeRegOp(Group backupGroup, String keyPath, String valueName) {
			
			String v = (String)Registry.GetValue( keyPath, valueName, null );
			
			if( v == null ) return;
			
			RegistryOperation op = new RegistryOperation( backupGroup );
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
		Registry,
		Custom,
		RunOnce
	}
	
	
	
}
