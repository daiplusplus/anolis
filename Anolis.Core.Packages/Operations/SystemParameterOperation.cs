using System;
using System.Xml;

using Anolis.Packages.Native;
using Anolis.Packages.Utility;

using Cult = System.Globalization.CultureInfo;
using N    = System.Globalization.NumberStyles;


namespace Anolis.Packages.Operations {
	
	public class SystemParameterOperation : Operation {
		
		public SystemParameterOperation(Group parent, XmlElement operationElement) : base(parent, operationElement) {
			
			String spiAction = operationElement.GetAttribute("spiAction");
			String uiParam   = operationElement.GetAttribute("uiParam");
			String pvParam   = operationElement.GetAttribute("pvParam");
			
			SpiAction = (SpiAction)Enum.Parse( typeof(SpiAction), spiAction, true );
			
			if( uiParam.Length > 0 ) {
				
				if     ( uiParam.Equals("TRUE" , StringComparison.OrdinalIgnoreCase) ) UIParam = 1;
				else if( uiParam.Equals("FALSE", StringComparison.OrdinalIgnoreCase) ) UIParam = 0;
				else {
					
					UInt32 uiParamN;
					if( UInt32.TryParse( uiParam, N.Integer, Cult.InvariantCulture, out uiParamN ) ) UIParam = uiParamN;
				}
			}
			
			////////////////////////////////
			
			if( pvParam.Length > 0 ) {
				
				if     ( pvParam.Equals("TRUE" , StringComparison.OrdinalIgnoreCase) ) PVParam = 1;
				else if( pvParam.Equals("FALSE", StringComparison.OrdinalIgnoreCase) ) PVParam = 0;
				else {
					
					UInt32 pvParamN;
					if( UInt32.TryParse( pvParam, N.Integer, Cult.InvariantCulture, out pvParamN ) ) PVParam = pvParamN;
				}
			}
			
		}
		
		public SystemParameterOperation(Group parent) : base(parent) {
			
		}
		
		public SpiAction SpiAction { get; set; }
		public UInt32    UIParam   { get; set; }
		public UInt32    PVParam   { get; set; }
		
		public override String OperationName {
			get { return "SystemParameter"; }
		}
		
		public override Boolean Merge(Operation operation) {
			return false;
		}
		
		public override void Execute() {
			
			if( SpiAction == SpiAction.None ) return;
			
			Backup( Package.ExecutionInfo.BackupGroup );
			
			Boolean result = NativeMethods.SystemParametersInfo( SpiAction, UIParam, new IntPtr( PVParam ) );
			
			if(!result) {
				String error = NativeMethods.GetLastErrorString();
				
				String args = SpiAction.ToString() + "," + UIParam.ToStringInvariant() + "," + PVParam.ToStringInvariant();
				
				Package.Log.Add( LogSeverity.Error, "SystemParametersInfo(" + args + ") failed: " + error );
			}
			
		}
		
		private void Backup(Group backupGroup) {
			
			if( backupGroup == null ) return;
			
			//NativeMethods.SystemParametersInfo(NativeMethods.SpiAction.
			
		}
		
		public override void Write(XmlElement parent) {
			
			CreateElement(parent, "systemParameter",
				
				"spiAction", SpiAction.ToString(),
				"uiParam"  , UIParam.ToStringInvariant(),
				"pvParam"  , PVParam.ToStringInvariant()
			);
			
		}
		
		public override String ToString() {
			return "System Parameter: " + SpiAction.ToString();
		}
		
		/// <summary>Returns the inverse 'Get' SpiAction for the specified 'Set' SpiAction. Returns 'None' if the specified action is already a Getter or has no Getter partner.</summary>
		private static SpiAction GetGetterAction(SpiAction action) {
			
			switch(action) {
				case SpiAction.SetFocusBorderWidth:
					return SpiAction.GetFocusBorderWidth;
					
				case SpiAction.SetFocusBorderHeight:
					return SpiAction.GetFocusBorderHeight;
					
				case SpiAction.SetMouseSonar:
					return SpiAction.GetMouseSonar;
				
				
				case SpiAction.SetDesktopWallpaper:
					return SpiAction.GetDesktopWallpaper;
				case SpiAction.SetDropShadow:
					return SpiAction.GetDropShadow;
				case SpiAction.SetFlatMenu:
					return SpiAction.GetFlatMenu;
				
				case SpiAction.SetFontSmoothing:
					return SpiAction.GetFontSmoothing;
				case SpiAction.SetFontSmoothingType:
					return SpiAction.GetFontSmoothingType;
				
				default:
					return SpiAction.None;
			}
		}
		
	}
	

	
}
