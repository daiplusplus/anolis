using System;
using System.Xml;

namespace Anolis.Core.Packages {
	
	public class FileOperation : Operation {
		
		public FileOperation(XmlElement operationElement) : base(operationElement) {
			
			
			
		}
		
		/// <summary>Gets the actual, working, path to the file (if it exists).</summary>
		public String ResolvedPath { get; private set; }
		
		//public FileCondition Condition { get; private set; }
		
		public override void Execute() {
			
			
			
		}
		
	}
}
