using System;
using System.Xml;

using Anolis.Core.Native;

using Cult = System.Globalization.CultureInfo;
using N = System.Globalization.NumberStyles;

namespace Anolis.Core.Packages.Operations {
	
	public class SystemParameterOperation : Operation {
		
		private NativeMethods.SpiAction _spiAction;
		private UInt32                  _uiParam;
		private UInt32                  _pvParam;
		
		public SystemParameterOperation(Package package, XmlElement operationElement) : base(package, operationElement) {
			
			String spiAction = operationElement.GetAttribute("spiAction");
			String uiParam   = operationElement.GetAttribute("uiParam");
			String pvParam   = operationElement.GetAttribute("pvParam");
			
			if( uiParam.Equals("TRUE" , StringComparison.OrdinalIgnoreCase) ) uiParam = "1";
			if( uiParam.Equals("FALSE", StringComparison.OrdinalIgnoreCase) ) uiParam = "0";
			
			_spiAction = (NativeMethods.SpiAction)Enum.Parse( typeof(NativeMethods.SpiAction), spiAction, true );
			_uiParam   = UInt32.Parse( uiParam, N.Integer | N.HexNumber, Cult.InvariantCulture );
			_pvParam   = UInt32.Parse( uiParam, N.Integer | N.HexNumber, Cult.InvariantCulture );
			
		}
		
		protected override String OperationName {
			get { return "SystemParameter"; }
		}
		
		public override Boolean Merge(Operation operation) {
			return false;
		}
		
		public override void Execute() {
			
			NativeMethods.SpiUpdate update = NativeMethods.SpiUpdate.SendWinIniChange | NativeMethods.SpiUpdate.UpdateIniFile;
			
			Boolean result = NativeMethods.SystemParametersInfo( _spiAction, _uiParam, new IntPtr( _pvParam ), update );
			
			if(!result) {
				String error = NativeMethods.GetLastErrorString();
				Package.Log.Add( LogSeverity.Error, "SystemParametersInfo failed: " + error );
			}
			
		}
	}
	
}
