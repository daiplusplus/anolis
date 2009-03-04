using System;
using System.Xml;

namespace Anolis.Core.Packages {
	
	public abstract class PackageItemContainer : PackageItem {
		
		public PackageItemContainer(XmlElement element) : base(element) {
			
			Children   = new SetCollection();
			Operations = new OperationCollection();
			
			foreach(XmlElement e in element.ChildNodes) {
				
				switch(e.Name) {
					case "set":
						
						Set set = new Set(e);
						Children.Add( set );
						
						break;
						
					case "patch":
						
						PatchOperation patch = new PatchOperation(e);
						Operations.Add( patch );
						break;
						
					case "file":
					case "filetype":
						break;
				}
				
			}
			
		}
		
		public SetCollection       Children   { get; private set; }
		public OperationCollection Operations { get; private set; }
		
		internal void Execute() {
			
			foreach(Set       set in Children)  set.Execute();
			
			foreach(Operation op  in Operations) op.Execute();
			
		}
		
	}
	
}
