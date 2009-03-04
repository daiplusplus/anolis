using System;
using System.Xml;

using Anolis.Core;
using Anolis.Core.Data;

namespace Anolis.Core.Packages {
	
	public class PatchOperation : Operation {
		
		public PatchOperation(Package package, XmlElement operationElement) : base(package, operationElement) {
			
			Name = System.IO.Path.GetFileName( operationElement.GetAttribute("path") );
			
		}
		
		public String File { get; private set; }
		
		public override void Execute() {
			
			// determine if it's necessary to open it interactively or non-interactively
			// i.e. if the lang attribute is is set on all the res elements
			
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
