using System;
using System.Xml;

namespace Anolis.Core.Packages {
	
	public class ExtraOperation : Operation {
		
		public ExtraOperation(Package package, XmlElement operationElement) : base(package, operationElement) {
		}
		
		public override void Execute() {
			
		}
		
		protected override String OperationName {
			get { return "Extra"; }
		}
		
		public override Boolean Merge(Operation operation) {
			
			// when merged the item will be installed
			// but only the last item will be made active
			
			return false;
		}
	}
	
	public enum ExtraOperationType {
		None,
		Wallpaper,
		Cursor,
		CursorScheme,
		VisualStyle,
		Screensaver,
		ProgramRun,
		ProgramZip,
		Custom
	}
}
