using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace Anolis.Core.Packages {
	
	public class OperationCollection : Collection<Operation> {
	}
	
	public abstract class Operation : PackageItem {
		
		protected Operation(XmlElement operationElement) : base(operationElement) {
			
			Path = operationElement.GetAttribute("path");
			
		}
		
		protected abstract String OperationName { get; }
		
		public String Path { get; set; }
		
		public abstract void Execute();
		
		public static Operation FromElement(XmlElement operationElement) {
			
			switch(operationElement.Name) {
				case "patch":
					return new PatchOperation(operationElement);
				case "file":
					return new FileOperation(operationElement);
				case "extra":
					return new ExtraOperation(operationElement);
				default:
					// TODO: Allow additional libraries or code-generation to specify their own stuff
					// Define types in the Package XML? http://www.codeproject.com/KB/dotnet/evaluator.aspx
					return null;
			}
			
		}
		
		public override String ToString() {
			return OperationName + ": " + base.ToString();
		}
		
	}
}
