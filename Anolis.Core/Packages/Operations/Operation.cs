using System;
using System.Collections.ObjectModel;
using System.Xml;

using Anolis.Core.Utility;
using P = System.IO.Path;
using System.Text;

namespace Anolis.Core.Packages.Operations {
	
	public abstract class Operation : PackageItem {
		
		protected Operation(Group parent, XmlElement operationElement) : base( parent.Package, parent, operationElement) {
		}
		
		protected Operation(Group parent) : base(parent.Package, parent) {
		}
		
		public abstract String OperationName { get; }
		
		public abstract void Execute();
		
		/// <summary>Provides an opportunity to reduce the number of operations by merging them together to be Executed in a single go. Return true if the provided operation was successfully merged into this operation (thus making it obsolete, it will then be removed from the flattened operation list). Return false to keep the old operation in the list.</summary>
		public abstract Boolean Merge(Operation operation);
		
		public virtual String Key { get { return OperationName; } }
		
		public virtual Boolean SupportsCDImage {
			get { return false; }
		}
		
		public virtual Boolean CustomEvaluation {
			get { return false; }
		}
		
		public override String ToString() {
			return OperationName;
		}
		
#region Static
		
		public static Operation FromElement(Group parent, XmlElement operationElement) {
			
			switch(operationElement.Name) {
				case "patch":
					return new ResPatchOperation(parent, operationElement);
				case "file":
					return new FileOperation(parent, operationElement);
				case "directory":
					return new DirectoryOperation(parent, operationElement);
				case "extra":
					return ExtraOperation.Create(parent, operationElement);
				case "cursorScheme":
					return new CursorSchemeOperation(parent, operationElement);
				case "systemParameter":
					return new SystemParameterOperation(parent, operationElement);
				case "fileType":
					return new FileTypeOperation(parent, operationElement);
				case "registry":
					return new RegistryOperation(parent, operationElement);
				case "program":
					return new ProgramOperation(parent, operationElement);
				case "uxtheme":
					return new UXThemeOperation(parent, operationElement);
				case "uninstallation":
					return new UninstallationOperation(parent, operationElement);
				case "clearIconCache":
					return new ClearIconCacheOperation(parent, operationElement);
				default:
					// TODO: Allow additional libraries or code-generation to specify their own stuff
					// Define types in the Package XML? http://www.codeproject.com/KB/dotnet/evaluator.aspx
					
					parent.Package.Log.Add(LogSeverity.Warning, "Unrecognised element: " + operationElement.Name);
					
					return null;
			}
			
		}
		
#endregion
		
	}
	
	public abstract class PathOperation : Operation {
		
		private String _path;
		
		protected PathOperation(Group parent, XmlElement operationElement) : base(parent, operationElement) {
			
			Path = operationElement.GetAttribute("path");
		}
		
		protected PathOperation(Group parent, String operationPath) : base(parent) {
			
			Path = operationPath;
		}
		
		public String Path {
			get { return _path; }
			set {
				
				if( value == null ) {
					_path = null;
				} else {
					_path = PackageUtility.ResolvePath( value, Package.RootDirectory.FullName );
				}
				
				
			}
		}
		
		public override String Key { get { return OperationName + Path; } }
		
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
