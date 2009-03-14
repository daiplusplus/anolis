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
				
				value = ResolveEnvironmentVariables( value );
				
				if( P.IsPathRooted( value ) ) _path = value;
				else {
					
					_path = P.Combine( Package.RootDirectory.FullName, value );
				}
				
			}
		}
		
		private static String ResolveEnvironmentVariables(String path) {
			
			if( path.IndexOf('%') == -1 ) return path;
			
			StringBuilder retval = new StringBuilder();
			StringBuilder currentEnvVar = new StringBuilder();
			
			Boolean inEnvVar = false;
			for(int i=0;i<path.Length;i++) {
				Char c = path[i];
				
				if(c == '%') inEnvVar = !inEnvVar;
				if(c == '%' && inEnvVar) {
					inEnvVar = path.IndexOf('%', i) > -1; // it doesn't count if there isn't another % in the string later on
					if(inEnvVar) continue; // no point logging the % character
				}
				if(c == '%' && !inEnvVar) {
					continue;
				}
				
				if(!inEnvVar) retval.Append( c );
				else {
					
					currentEnvVar.Append( c );
					
					if( path[i+1] == '%' ) { // if we're at the end of an envvar; // TODO: How do I avoid an indexoutofrange?
						// actually, I don't think this will happen since the check above means we won't be inEnvVar if there isn't another one in the string
						
						String envVar = currentEnvVar.ToString(); currentEnvVar.Length = 0;
						retval.Append( Environment.GetEnvironmentVariable( envVar ) );
						
					}
				}
			}
			
			return retval.ToString();
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
