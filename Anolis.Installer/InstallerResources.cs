using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Reflection;

using Anolis.Core;
using System.IO;

namespace Anolis.Installer {
	
	/// <summary>Creates the right ResourceManager for the installer depending on whether this installer is created by the Distributor or not.</summary>
	internal static class InstallerResources {
		
		// the ".resources" bit is tacked on by the framework
		private const String _resourcesName    = @"Anolis.Installer.Resources.Wizard";
		private const String _resourcesCusName = @"Anolis.Installer.Merged.Wizard";
		
		/////////////////////////////////////////////////////////////
		
		private static Boolean? _isCustomized = null;
		
		public static Boolean IsCustomized {
			get {
				
				if( _isCustomized == null ) {
					
					String[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
					
					_isCustomized = names.IndexOf( _resourcesCusName + ".resources" ) >= 0;
					
				}
				
				return _isCustomized.Value;
				
			}
		}
		
		/////////////////////////////////////////////////////////////
		
		private static ResourceManager _resourceManager;
		
		public static ResourceManager ResourceManager {
			get {
				if( _resourceManager == null ) {
					
					_resourceManager = IsCustomized ?
						new ResourceManager(_resourcesCusName, Assembly.GetExecutingAssembly() ) :
						new ResourceManager(_resourcesName   , Assembly.GetExecutingAssembly() );
					
				}
				return _resourceManager;
			}
		}
		
		/////////////////////////////////////////////////////////////
		
		public static String GetString(String name) {
			
			return ResourceManager.GetString( name );
		}
		
		public static Object GetObject(String name) {
			
			return ResourceManager.GetObject( name );
		}
		
		public static Stream GetStream(String name) {
			
			return ResourceManager.GetStream( name );
		}
		
		public static System.Drawing.Image GetImage(String name) {
			
			return GetObject( name ) as System.Drawing.Image;
		}
		
		public static System.Drawing.Icon GetIcon(String name) {
			
			return GetObject( name ) as System.Drawing.Icon;
		}
		
	}
}
