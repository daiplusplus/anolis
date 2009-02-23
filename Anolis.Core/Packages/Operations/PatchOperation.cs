using System;
using System.Xml;

using Anolis.Core;
using Anolis.Core.Data;

namespace Anolis.Core.Packages {
	
	public class PatchOperation : Operation {
		
		public PatchOperation(XmlElement operationElement) : base(operationElement) {
			
			
			
		}
		
		public String File { get; private set; }
		
		public override void Execute() {
			
			// open the resource source in non-interactive mode
			ResourceSource src = ResourceSource.Open( File, false );
			
			// TODO: How is the DummyResourceData exposed?
			// well that's for deletes and 1:1 binary resources
			// but the point remains...
			
		}
		
	}
	
	internal class PatchResource {
		
		
		
		
	}
	
}
