using System;
using System.Xml;

namespace Anolis.Core.Packages {
	
	public class FileOperation : Operation {
		
		public FileOperation(Package package, XmlElement operationElement) : base(package, operationElement) {
			
			
			
		}
		
		/// <summary>Gets the actual, working, path to the file (if it exists).</summary>
		public String ResolvedPath { get; private set; }
		
		public override void Execute() {
			
			
			
		}
		
		protected override string OperationName {
			get { return "File"; }
		}
		
		public override Boolean Merge(Operation operation) {
			
			// check if the Condition is the same
			return false;
		}
	}
}
