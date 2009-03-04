using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace Anolis.Core.Packages {
	
	public class OperationCollection : Collection<Operation> {
	}
	
	public abstract class Operation : PackageItem {
		
		protected Operation(Package package, XmlElement operationElement) : base(package, operationElement) {
		}
		
		public abstract void Execute();
		
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
		
	}
	
	public class FileOperation : Operation {
		
		public FileOperation(Package package, XmlElement operationElement) : base(package, operationElement) {
		}
		
		/// <summary>Gets the Path to the file as specified in the Package definition file.</summary>
		public String Path { get; private set; }
		
		/// <summary>Gets the actual, working, path to the file (if it exists).</summary>
		public String ResolvedPath { get; private set; }
		
		//public FileCondition Condition { get; private set; }
		
		public override void Execute() {
			
			
			
		}
		
	}
	
	public class ExtraOperation : Operation {
		
		public ExtraOperation(Package package, XmlElement operationElement) : base(package, operationElement) {
		}
		
		public override void Execute() {
			
		}
		
	}
}
