using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Diagnostics;

using Microsoft.Win32;

using P = System.IO.Path;


namespace Anolis.Core.Packages.Operations {
	
	public class CursorSchemeOperation : Operation {
		
		public CursorSchemeOperation(Package package, XmlElement element) :  base(package, element) {
			
			foreach(XmlNode node in element.ChildNodes) {
				
				if(node.NodeType != XmlNodeType.Element) continue;
				
				XmlElement child = node as XmlElement;
				
				
				
			}
			
		}
		
		protected override String OperationName {
			get { return "Cursor set"; }
		}
		
		public override Boolean Merge(Operation operation) {
			throw new NotImplementedException();
		}
		
		public override void Execute() {
			throw new NotImplementedException();
		}
		
		public enum CursorType {
			Arrow,
			Help,
			AppStarting,
			Wait,
			Hand,
			IBeam,
			No,
			NWPen,
			Crosshair,
			SizeAll,
			SizeNWSW,
			SizeNS,
			SizeNWSE,
			SizeWE,
			UpArrow
		}
		
	}
	
	
	public class FileTypeOperation : Operation {
		
		public FileTypeOperation(Package package, XmlElement element) :  base(package, element) {
		}
		
		protected override string OperationName {
			get { throw new NotImplementedException(); }
		}

		public override void Execute() {
			throw new NotImplementedException();
		}

		public override bool Merge(Operation operation) {
			throw new NotImplementedException();
		}
	}
	
	public class RegistryOperation : Operation {
		
		public RegistryOperation(Package package, XmlElement element) :  base(package, element) {
		}
		
		protected override string OperationName {
			get { throw new NotImplementedException(); }
		}

		public override void Execute() {
			throw new NotImplementedException();
		}

		public override bool Merge(Operation operation) {
			throw new NotImplementedException();
		}
	}
	
}
