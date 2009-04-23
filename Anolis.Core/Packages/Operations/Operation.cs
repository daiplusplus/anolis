using System;
using System.Collections.ObjectModel;
using System.Xml;

using P = System.IO.Path;
using System.Text;

namespace Anolis.Core.Packages {
	
	public class OperationCollection : Collection<Operation> {
	}
	
	public abstract class Operation : PackageItem {
		
		private String _path;
		
		protected Operation(Package package, XmlElement operationElement) : base(package, operationElement) {
			
			Path = operationElement.GetAttribute("path");
		}
		
		protected abstract String OperationName { get; }
		
		public String Path {
			get { return _path; }
			set {
				
				_path = PackageUtility.ResolvePath( value, Package.RootDirectory.FullName );
			}
		}
		
		public abstract void Execute();
		
		public abstract Boolean Merge(Operation operation);
		
		public virtual String Key { get { return OperationName + Path; } }
		
		public static Operation FromElement(Package package, XmlElement operationElement) {
			
			switch(operationElement.Name) {
				case "patch":
					return new PatchOperation(package, operationElement);
				case "file":
					return new FileOperation(package, operationElement);
				case "extra":
					return new ExtraOperation(package, operationElement);
				default:
					// TODO: Allow additional libraries or code-generation to specify their own stuff
					// Define types in the Package XML? http://www.codeproject.com/KB/dotnet/evaluator.aspx
					return null;
			}
			
		}
		
		public override String ToString() {
			return OperationName + ": " + System.IO.Path.GetFileName( Path );
		}
		
	}
}
