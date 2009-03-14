using System;
using System.Xml;

namespace Anolis.Core.Packages {
	
	public class NotImplementedOperation : Operation {
		
		public NotImplementedOperation(Package package, XmlElement operationElement) : base(package, operationElement) {
		}
		
		protected override String OperationName {
			get { return "Not Implemented"; }
		}
		
		public override void Execute() {
		}
		
		public override Boolean Merge(Operation operation) {
			return false;
		}
	}
}
