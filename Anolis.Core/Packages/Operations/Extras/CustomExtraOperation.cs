using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Diagnostics;

using System.CodeDom;

using Microsoft.Win32;
using Anolis.Core.Utility;
using P = System.IO.Path;

namespace Anolis.Core.Packages.Operations {
	
	public class CustomExtraOperation : ExtraOperation {
		
		public CustomExtraOperation(Package package, XmlElement element) :  base(ExtraType.Custom, package, element) {
		}

		protected override Boolean CanMerge { get { return false; } }
		
		public override void Execute() {
			
			Package.Log.Add( LogSeverity.Warning, "CustomExtraOperation not implemented. " + base.Files[0] + " not executed." );
			
		}
	}
}
