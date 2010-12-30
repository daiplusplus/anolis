using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Core.Packages.Operations {
	
	public class ExceptionExtraOperation : ExtraOperation {
		
		public ExceptionExtraOperation(Package package, XmlElement element) : base(ExtraType.Exception, package, element) {
		}
		
		public override void Execute() {
			throw new DivideByZeroException("fa-lah-lah");
		}
		
	}
}
