using System;
using System.Xml;
using Microsoft.Win32;

using Anolis.Core.Utility;
using Anolis.Packages.Utility;

namespace Anolis.Packages.Operations {
	
	public class RegistryOperation : Operation {
		
		public RegistryOperation(Group parent, XmlElement element) :  base(parent, element) {
			
			RegKey   = element.GetAttribute("key");
			RegName  = element.GetAttribute("vname");
			RegValue = element.GetAttribute("value");
			RegKind  = ParseType( element.GetAttribute("type") );
			
		}
		
		public RegistryOperation(Group parent) :  base(parent) {
		}
		
		private static RegistryValueKind ParseType(String type) {
			
			type = type.ToUpperInvariant();
			switch(type) {
				case "REG_SZ"       : return RegistryValueKind.String;
				case "REG_EXPAND_SZ": return RegistryValueKind.ExpandString;
				case "REG_BINARY"   : return RegistryValueKind.Binary;
				case "REG_DWORD"    : return RegistryValueKind.DWord;
				case "REG_MULTI_SZ" : return RegistryValueKind.MultiString;
				case "REG_QWORD"    : return RegistryValueKind.QWord;
				default:
					return RegistryValueKind.Unknown;
			}
			
		}
		
		private static String KindToString(RegistryValueKind type) {
			
			switch(type) {
				case RegistryValueKind.String:
					return "REG_SZ";
				case RegistryValueKind.ExpandString:
					return "REG_EXPAND_SZ";
				case RegistryValueKind.Binary:
					return "REG_BINARY";
				case RegistryValueKind.DWord:
					return "REG_DWORD";
				case RegistryValueKind.MultiString:
					return "REG_MULTI_SZ";
				case RegistryValueKind.QWord:
					return "REG_QWORD";
				case RegistryValueKind.Unknown:
				default:
					return "Unknown";
			}
			
		}
		
		public override String OperationName {
			get { return "Registry"; }
		}
		
		public String            RegKey   { get; set; }
		public String            RegName  { get; set; }
		public String            RegValue { get; set; }
		public RegistryValueKind RegKind  { get; set; }
		
		public override void Execute() {
			
			Backup( Package.ExecutionInfo.BackupGroup );
			
			try {
				
				if( RegValue == "###ANOLISREMOVE####" ) {
					
					RegistryKey key = GetRegistryKey( RegKey, true );
					if( key != null ) {
						
						key.DeleteValue( RegName );
					}
					key.Close();
					
				} else {
					
					Registry.SetValue( RegKey, RegName, RegValue, RegKind );
				}
				
				
			} catch(ArgumentException ex) {
				
				Package.Log.Add( LogSeverity.Error, "Registry.SetValue ArgumentException: " + ex.Message );
				
			} catch(UnauthorizedAccessException aex) {
				
				Package.Log.Add( LogSeverity.Error, "Registry.SetValue UnauthorizedAccessException: " + aex.Message );
			}
			
		}
		
		private static RegistryKey GetRegistryKey(String keyName, Boolean writable) {
			
			RegistryKey hive = null;
			
			// get the right hive
			String hiveName = keyName.Substring(0, keyName.IndexOf('\\')).ToUpperInvariant();
			switch(hiveName) {
				case "HKLM":
				case "HKEY_LOCAL_MACHINE":
					hive = Registry.LocalMachine;
					break;
				case "HKCU":
				case "HKEY_CURRENT_USER":
					hive = Registry.CurrentUser;
					break;
				case "HKCR":
				case "HKEY_CLASSES_ROOT":
					hive = Registry.ClassesRoot;
					break;
				case "HKU":
				case "HKEY_USERS":
					hive = Registry.Users;
					break;
				case "HKCC":
				case "HKEY_CURRENT_CONFIG":
					hive = Registry.CurrentConfig;
					break;
			}
			
			if( hive == null ) return null;
			
			////////////////////////////////////////////////////
			
			String relatveKeyName = keyName.Substring( hiveName.Length + 1 ); // +1 to skip the first backslash
			
			return hive.OpenSubKey( relatveKeyName, writable );
			
		}
		
		private void Backup(Group backupGroup) {
			
			if( backupGroup == null ) return;
			
			// get the current value and write it
			
			Object v = Registry.GetValue( RegKey, RegName, null );
			// TODO: In future, get the value's kind before writing, don't use the currently specified RegKind attribute
			
			RegistryOperation op = new RegistryOperation(backupGroup);
			op.RegKey   = RegKey;
			op.RegName  = RegName;
			op.RegKind  = RegKind; // there is an NRE here apparently
			
			op.RegValue = v == null ? "###ANOLISREMOVE####" : v.ToString();
			
			backupGroup.Operations.Add( op );
		}
		
		public override void Write(XmlElement parent) {
			
			XmlElement element = CreateElement(parent, "registry",
				"key"  , RegKey,
				"vname", RegName,
				"value", RegValue,
				"type" , KindToString( RegKind )
			);
			
		}
		
		public override bool Merge(Operation operation) {
			return false;
		}
		
		public override String ToString() {
			return OperationName + ": " + this.RegKey + "\\" + this.RegName;
		}
		
	}
}
