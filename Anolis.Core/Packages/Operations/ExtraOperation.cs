using System;
using System.Xml;

namespace Anolis.Core.Packages {
	
	public class ExtraOperation : Operation {
		
		public ExtraOperation(XmlElement operationElement) : base(operationElement) {
		}
		
		public override void Execute() {
			
		}
		
		protected override String OperationName {
			get { return "Extra"; }
		}
		
	}
}
