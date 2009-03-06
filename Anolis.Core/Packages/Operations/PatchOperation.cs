using System;
using System.IO;
using System.Xml;

using Anolis.Core;
using Anolis.Core.Data;

namespace Anolis.Core.Packages {
	
	public class PatchOperation : Operation {
		
		public PatchOperation(XmlElement operationElement) : base(operationElement) {
			
		}
		
		public override void Execute() {
			
			if( !File.Exists( Path ) ) return; // TODO: Log it?
			
			// determine if it's necessary to open it blindly or not
			// i.e. if the lang attribute is is set on all the res elements
			
			// open the resource source in blind mode for performance
			ResourceSource src = ResourceSource.Open( Path, false, true );
			
			// TODO: How is the DummyResourceData exposed?
			// well that's for deletes and 1:1 binary resources
			// but the point remains...
			
		}
		
		protected override String OperationName {
			get { return "Res patch"; }
		}
		
	}
	
	internal class PatchResource {
		
		
		
		
	}
	
}
