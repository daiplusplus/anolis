using System;
using System.Xml;

using Anolis.Core.Utility;

namespace Anolis.Core.Packages.Operations {
	
	public class FileTypeOperation : Operation {
		
		public FileTypeOperation(Package package, XmlElement element) :  base(package, element) {
		}
		
		protected override string OperationName {
			get { return "File type"; }
		}
		
		public override void Execute() {
			// TODO
			//throw new NotImplementedException();
		}
		
		public override bool Merge(Operation operation) {
			return false;
		}
	}
}
