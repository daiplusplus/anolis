using System;
using System.Collections.Generic;
using System.Text;
using Anolis.Core.Native;
using Anolis.Core.Utility;

namespace Anolis.Core.Packages.Operations {
	
	public class ClearIconCacheOperation : Operation {
		
		public ClearIconCacheOperation(Group parent, System.Xml.XmlElement element) : base(parent, element) {
		}
		
		public override string OperationName {
			get { return "Clear Icon Cache"; }
		}
		
		public override void Execute() {
			
			PackageUtility.ClearIconCache();
			
			if( !NativeMethods.SystemParametersInfo( SpiAction.SetIcons, 0, IntPtr.Zero ) ) {
				
				String message = NativeMethods.GetLastErrorString();
				
				Package.Log.Add( LogSeverity.Error, "SystemParametersInfo Failed: " + message );
			}
			
		}
		
		public override Boolean Merge(Operation operation) {
			return true;
		}
		
		public override void Write(System.Xml.XmlElement parent) {
			CreateElement(parent, "ClearIconCache");
		}
	}
}
