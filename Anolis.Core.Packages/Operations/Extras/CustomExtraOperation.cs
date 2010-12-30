using System;
using System.Xml;

using Anolis.Packages.Utility;

namespace Anolis.Packages.Operations {
	
	public class CustomExtraOperation : ExtraOperation {
		
		public CustomExtraOperation(Group parent, XmlElement element) :  base(ExtraType.Custom, parent, element) {
		}
		
		public CustomExtraOperation(Group parent, String codeFilePath) : base(ExtraType.Custom, parent, codeFilePath) {
		}

		protected override Boolean CanMerge { get { return false; } }
		
		public override void Execute() {
			
			Package.Log.Add( LogSeverity.Warning, "CustomExtraOperation not implemented. " + base.Files[0] + " not executed." );
			
		}
		
	}
}
