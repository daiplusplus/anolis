using System;
using System.Collections.Generic;

using Microsoft.Win32;

namespace Anolis.Resourcer.Settings {
	
	public class FileAssociationManager {
		
		
		public FileAssociationManager(String applicationName, String applicationPath) {
			ApplicationName = applicationName;
			ApplicationPath = applicationPath;
		}
		
		public String ApplicationName { get; set; }
		public String ApplicationPath { get; set; }
		
		public TriState IsAssociatedWithFiles(String[] classes) {
			
			Boolean[] states = new Boolean[classes.Length];
			
			for(int i=0;i<classes.Length;i++) {
				
				RegistryKey cls = Registry.ClassesRoot.OpenSubKey( classes[i] );
				RegistryKey shell = cls.OpenSubKey("shell");
				
				// validate all the subkeys and values are there
				
				RegistryKey openWith = shell.OpenSubKey( OpenWith );
				if(openWith == null) continue;
				
				RegistryKey comd = openWith.OpenSubKey("Command");
				if(comd == null) continue;
				
				String defaultVal = comd.GetValue(null) as String;
				if(defaultVal == null) continue;
				
				if( String.Equals( defaultVal, Command, StringComparison.OrdinalIgnoreCase) ) {
					states[i] = true;
				}
				
			}
			
			if( Array.TrueForAll<Boolean>( states, x => x == false ) ) return TriState.False;
			if( Array.TrueForAll<Boolean>( states, x => x == true ) ) return TriState.True;
			return TriState.Partial;
			
		}
		
		public void AssociateWithFiles(String[] classes, Boolean isAssociated) {
			
			if( isAssociated ) {
				
				for(int i=0;i<classes.Length;i++) {
					
					RegistryKey cls      = Registry.ClassesRoot.CreateSubKey( classes[i] );
					RegistryKey shell    = cls.CreateSubKey("shell");
					RegistryKey openWith = shell.CreateSubKey( OpenWith );
					RegistryKey command  = openWith.CreateSubKey( "Command" );
					command.SetValue(null, Command);
					
				}
				
			} else {
				
				for(int i=0;i<classes.Length;i++) {
					
					RegistryKey cls      = Registry.ClassesRoot.CreateSubKey( classes[i] );
					RegistryKey shell    = cls.CreateSubKey("shell");
					
					if( shell.OpenSubKey( OpenWith ) != null ) shell.DeleteSubKeyTree( OpenWith );
					
				}
				
			}
			
		}
		
		private String OpenWith {
			get { return "Open with " + ApplicationName; }
		}
		
		private String Command {
			get { return ApplicationPath + " %1"; }
		}
		
	}
}
