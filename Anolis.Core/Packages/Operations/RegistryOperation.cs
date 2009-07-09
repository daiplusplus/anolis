using System;
using System.Xml;
using Anolis.Core.Utility;
using Microsoft.Win32;

namespace Anolis.Core.Packages.Operations {
	
	public class RegistryOperation : Operation {
		
		public RegistryOperation(Package package, Group parent, XmlElement element) :  base(package, parent, element) {
			
			RegKey   = element.GetAttribute("key");
			RegName  = element.GetAttribute("vname");
			RegValue = element.GetAttribute("value");
			RegKind  = ParseType( element.GetAttribute("type") );
			
		}
		
		public RegistryOperation(Package package, Group parent) :  base(package, parent, (String)null) {
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
				
				Registry.SetValue( RegKey, RegName, RegValue, RegKind );
				
			} catch(ArgumentException ex) {
				
				Package.Log.Add( LogSeverity.Error, "Registry.SetValue ArgumentException: " + ex.Message );
				
			} catch(UnauthorizedAccessException aex) {
				
				Package.Log.Add( LogSeverity.Error, "Registry.SetValue UnauthorizedAccessException: " + aex.Message );
			}
			
		}
		
		private void Backup(Group backupGroup) {
			
			if( backupGroup == null ) return;
			
			// get the current value and write it
			
			Object v = Registry.GetValue( RegKey, RegName, null );
			
			RegistryOperation op = new RegistryOperation(backupGroup.Package, backupGroup);
			op.RegKey   = RegKey;
			op.RegName  = RegName;
			op.RegKind  = RegKind; // there is an NRE here apparently
			
			op.RegValue = v == null ? "" : v.ToString();
			
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
	}
}
