using System;
using System.Xml;
using Anolis.Core.Utility;

using Anolis.Core.Native;

using Cult = System.Globalization.CultureInfo;
using N    = System.Globalization.NumberStyles;

namespace Anolis.Core.Packages.Operations {
	
	public class SystemParameterOperation : Operation {
		
		public SystemParameterOperation(Package package, Group parent, XmlElement operationElement) : base(package, parent, operationElement) {
			
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
			
			Backup( Package.ExecutionInfo.BackupGroup );
			
			NativeMethods.SpiUpdate update = NativeMethods.SpiUpdate.SendWinIniChange | NativeMethods.SpiUpdate.UpdateIniFile;
			
			Boolean result = NativeMethods.SystemParametersInfo( (UInt32)SpiAction, UIParam, new IntPtr( PVParam ), update );
			
			if(!result) {
				String error = NativeMethods.GetLastErrorString();
				Package.Log.Add( LogSeverity.Error, "SystemParametersInfo failed: " + error );
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
		
	}
	public enum SpiAction : uint {
		// Accessibility
		SetFocusBorderWidth  = 0x200F, // pvParam = value
		SetFocusBorderHeight = 0x2011, // pvParam = value
		SetMouseSonar        = 0x101D, // pvParam = TRUE | FALSE
		
		// Desktop
		SetCursors           = 0x0057, // 
		SetDesktopWallpaper  = 0x0014, // pvParam = pSz wallpaper BMP image (JPEG in Vista and later)
		SetDropShadow        = 0x1025, // pvParam = TRUE | FALSE
		SetFlatMenu          = 0x1023, // pvParam = TRUE | FALSE
		SetFontSmoothing     = 0x004B, // uiParam = TRUE | FALSE
		SetFontSmoothingType = 0x200B, // pvParam = 1 for Standard Antialiasing, 2 for ClearType
		
		// Icons
		IconHorizontalSpacing = 0x000D, // uiParam = value
		IconVerticalSpacing   = 0x0018, // uiParam = value
		SetIcons              = 0x0058, //
		SetIconTitleWrap      = 0x001A, // uiParam = TRUE | FALSE
		
		// Mouse
		SetMouseTrails        = 0x005D, // uiParam = 0 or 1 to disable, or n > 1 for n cursors
		
		// Menus
		SetMenuDropAlignment  = 0x001C, // uiParam = TRUE for right, FALSE for left
		SetMenuFade           = 0x1013, // pvParam = TRUE | FALSE
		SetMenuShowDelay      = 0x006B, // uiParam = time in miliseconds
		
		// UI Effects
		SetUIEffects          = 0x103F, // pvParam = TRUE | FALSE to enable/disable all UI effects at once
		// I'd define all of them here, but there's little point
		
		// Windows
		SetActiveWindowTracking  = 0x1001, // pvParam = TRUE | FALSE
		SetActiveWndTrkZOrder    = 0x100D, // pvParam = TRUE | FALSE
		SetActiveWndTrkTimeout   = 0x2003, // pvParam = time in miliseconds
		SetBorder                = 0x0006, // uiParam = multiplier width of sizing border
		SetCaretWidth            = 0x2007, // pvParam = width in pixels
		SetDragFullWindows       = 0x0025, // uiParam = TRUE | FALSE
		SetForegroundFlashCount  = 0x2005, // pvParam = # times to flash
		SetForegroundLockTimeout = 0x2001  // pvParam = timeout in miliseconds
	}
	
}
