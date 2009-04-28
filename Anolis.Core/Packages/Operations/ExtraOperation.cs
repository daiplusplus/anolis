using System;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;

namespace Anolis.Core.Packages {
	
	public abstract class ExtraOperation : Operation {
		
		protected ExtraOperation(ExtraType type, Package package, XmlElement operationElement) : base(package, operationElement) {
			
			ExtraType = type;
		}
		
		protected override String OperationName {
			get { return "Extra"; }
		}
		
		public String             Attribution { get; private set; }
		public ExtraType ExtraType   { get; private set; }
		
		public static ExtraOperation Create(Package package, XmlElement operationElement) {
			
			String typeStr = operationElement.GetAttribute("type");
			ExtraType type = (ExtraType)Enum.Parse( typeof(ExtraType), typeStr, true );
			
			switch(type) {
				case ExtraType.Wallpaper:
				case ExtraType.Cursor:
				case ExtraType.CursorScheme:
					break;
			}
			
			return null;
		}
	}
	
	public enum ExtraType {
		None,
		Wallpaper,
		Cursor,
		CursorScheme,
		VisualStyle,
		Screensaver,
		ProgramRun,
		ProgramZip,
		Custom
	}
	
	public class WallpaperExtraOperation : ExtraOperation {
		
		public WallpaperExtraOperation(Package package, XmlElement element) : base(ExtraType.Wallpaper, package, element) {
			Wallpapers = new Collection<String>();
		}
		
		public Collection<String> Wallpapers {
			get; private set;
		}
		
		public override void Execute() {
			
			// copy the files to C:\Windows\web\Wallpaper
			// if they already exist append a digit methinks
			
			foreach(String source in Wallpapers) {
				
				String dest = System.IO.Path.GetFileName( source );
				dest = PackageUtility.ResolvePath( dest, null );
				
				File.Copy( source, dest );
			}
			
			// set the bottommost as the current wallpaper
			// call SystemParametersInfo to set the current wallpaper apparently
			// and then call RedrawWindow to repaint the desktop
			
			
		}
		
		public override Boolean Merge(Operation operation) {
			
			WallpaperExtraOperation other = operation as WallpaperExtraOperation;
			if(other == null) return false;
			
			foreach(String s in other.Wallpapers) {
				
				if( !Wallpapers.Contains( s ) ) Wallpapers.Add( s );
			}
			
			return true;
		}
	}
	
}
