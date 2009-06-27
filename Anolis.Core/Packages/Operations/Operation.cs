using System;
using System.Collections.ObjectModel;
using System.Xml;

using Anolis.Core.Utility;
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
		
		protected Operation(Package package, Group parent, XmlElement operationElement) : base(package, parent, operationElement) {
			
			Path = operationElement.GetAttribute("path");
		}
		
		protected Operation(Package package, Group parent, String operationPath) : base(package, parent) {
			
			Path = operationPath;
		}
		
		public abstract String OperationName { get; }
		
		/// <summary>Whether the item will be executed or not.</summary>
		public Boolean IsEnabled {
			get {
				return ParentGroup.Enabled ? Enabled : false;
			}
		}
		
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
		
		public static Operation FromElement(Package package, Group parent, XmlElement operationElement) {
			
			switch(operationElement.Name) {
				case "patch":
					return new PatchOperation(package, parent, operationElement);
				case "file":
					return new FileOperation(package, parent, operationElement);
				case "extra":
					return ExtraOperation.Create(package, parent, operationElement);
				case "cursorScheme":
					return new CursorSchemeOperation(package, parent, operationElement);
				case "systemParameter":
					return new SystemParameterOperation(package, parent, operationElement);
				case "fileType":
					return new FileTypeOperation(package, parent, operationElement);
				case "registry":
					return new RegistryOperation(package, parent, operationElement);
				case "uxtheme":
					return new UXThemeOperation(package, parent, operationElement);
				default:
					// TODO: Allow additional libraries or code-generation to specify their own stuff
					// Define types in the Package XML? http://www.codeproject.com/KB/dotnet/evaluator.aspx
					
					package.Log.Add(LogSeverity.Warning, "Unrecognised element: " + operationElement.Name);
					
					return null;
			}
			
		}
		
		public virtual Boolean SupportsI386 {
			get { return false; }
		}
		
		public override String ToString() {
			return OperationName + ": " + System.IO.Path.GetFileName( Path );
		}
		
	}
	
	public enum I386Type {
		None,
		Compressed,
		Uncompressed
	}
	
}
