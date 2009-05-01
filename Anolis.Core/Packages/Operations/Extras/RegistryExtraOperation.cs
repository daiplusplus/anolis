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
	
	public class RegistryExtraOperation : ExtraOperation {
		
		public RegistryExtraOperation(Package package, XmlElement element) :  base(ExtraType.Registry, package, element) {
		}
		
		public override void Execute() {
			
			
			
			
		}
	}
}
