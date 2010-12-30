using System;
using System.Xml;

using Anolis.Packages.Native;
using Anolis.Packages.Utility;

namespace Anolis.Packages.Operations {
	
	public class ClearIconCacheOperation : Operation {
		
		public ClearIconCacheOperation(Group parent, XmlElement element) : base(parent, element) {
		}
		
		public ClearIconCacheOperation(Group parent) : base(parent) {
		}
		
		public override string OperationName {
			get { return "Clear Icon Cache"; }
		}
		
		public override void Execute() {
			
			Backup( Package.ExecutionInfo.BackupGroup );
			
			PackageUtility.ClearIconCache();
			
			Boolean result = NativeMethods.SystemParametersInfo( SpiAction.SetIcons, 0, IntPtr.Zero );
			
			if(!result) {
				String error = NativeMethods.GetLastErrorString();
				
				String args = SpiAction.SetIcons.ToString() + "," + 0.ToStringInvariant() + "," + 0.ToStringInvariant();
				
				Package.Log.Add( LogSeverity.Error, "SystemParametersInfo(" + args + ") failed: " + error );
			}
			
		}
		
		private void Backup(Group backupGroup) {
			
			if( backupGroup == null ) return;
			
			ClearIconCacheOperation op = new ClearIconCacheOperation(backupGroup);
			backupGroup.Operations.Add( op );
			
		}
		
		public override Boolean Merge(Operation operation) {
			return true;
		}
		
		public override void Write(XmlElement parent) {
			CreateElement(parent, "clearIconCache");
		}
		
	}
}
