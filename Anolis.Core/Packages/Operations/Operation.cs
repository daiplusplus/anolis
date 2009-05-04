using System;
using System.Collections.ObjectModel;
using System.Xml;

using P = System.IO.Path;
using System.Text;

namespace Anolis.Core.Packages.Operations {
	
	public class OperationCollection : Collection<Operation> {
		
		private Group _parent;
		
		public OperationCollection(Group parent) {
			_parent = parent;
		}
		
		protected override void InsertItem(int index, Operation item) {
			base.InsertItem(index, item);
			
			if( !_parent.Enabled ) item.Enabled = false;
		}
		
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
		
		/// <summary>Provides an opportunity to reduce the number of operations by merging them together to be Executed in a single go. Return true if the provided operation was successfully merged into this operation (thus making it obsolete, it will then be removed from the flattened operation list). Return false to keep the old operation in the list.</summary>
		public abstract Boolean Merge(Operation operation);
		
		public virtual String Key { get { return OperationName + Path; } }
		
		public static Operation FromElement(Package package, XmlElement operationElement) {
			
			switch(operationElement.Name) {
				case "patch":
					return new PatchOperation(package, operationElement);
				case "file":
					return new FileOperation(package, operationElement);
				case "extra":
					return ExtraOperation.Create(package, operationElement);
				case "cursorScheme":
					return new CursorSchemeOperation(package, operationElement);
				case "systemParameter":
					return new SystemParameterOperation(package, operationElement);
				case "filetype":
					return new FileTypeOperation(package, operationElement);
				case "registry":
					return new RegistryOperation(package, operationElement);
				default:
					// TODO: Allow additional libraries or code-generation to specify their own stuff
					// Define types in the Package XML? http://www.codeproject.com/KB/dotnet/evaluator.aspx
					
					package.Log.Add(LogSeverity.Warning, "Unrecognised element: " + operationElement.Name);
					
					return null;
			}
			
		}
		
		public override String ToString() {
			return OperationName + ": " + System.IO.Path.GetFileName( Path );
		}
		
	}
}
