using System;
using System.Xml;
using Microsoft.Win32;

namespace Anolis.Core.Packages.Operations {
	
	public class RegistryOperation : Operation {
		
		private String            _key;
		private String            _name;
		private String            _value;
		private RegistryValueKind _type;
		
		public RegistryOperation(Package package, XmlElement element) :  base(package, element) {
			
			_key   = element.GetAttribute("key");
			_name  = element.GetAttribute("vname");
			_value = element.GetAttribute("value");
			_type  = ParseType( element.GetAttribute("type") );
			
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
		
		protected override string OperationName {
			get { return "Registry"; }
		}
		
		public override void Execute() {
			
			try {
				
				Registry.SetValue( _key, _name, _value, _type );
				
			} catch(ArgumentException ex) {
				
				Package.Log.Add( LogSeverity.Error, "Registry.SetValue ArgumentException: " + ex.Message );
				
			} catch(UnauthorizedAccessException aex) {
				
				Package.Log.Add( LogSeverity.Error, "Registry.SetValue UnauthorizedAccessException: " + aex.Message );
			}
			
		}
		
		public override bool Merge(Operation operation) {
			return false;
		}
	}
}
