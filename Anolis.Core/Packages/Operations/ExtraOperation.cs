using System;
using System.Xml;

namespace Anolis.Core.Packages {
	
	public class ExtraOperation : Operation {
		
		public ExtraOperation(Package package, XmlElement operationElement) : base(package, operationElement) {
		}
		
		public override void Execute() {
			
		}
		
		protected override String OperationName {
			get { return "Extra"; }
		}
		
		public override Boolean Merge(Operation operation) {
			return false;
		}
	}
}
