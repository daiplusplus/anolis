using System;
using System.Collections.Generic;
using System.Text;

using Anolis.Core.Data;

namespace Anolis.Core {
	
	internal static class ResourceDataFactory {
		
		/// <summary>Identifies a ResourceData's type based from a hint, then falling back on the intrinsic data; and returns the data as a subclass of ResourceData.</summary>
		public static ResourceData GetResourceData(ResourceLang lang, ResourceHint hint, Byte[] rawData) {
			
			// an alternative (and for performance reasons), if Hinting doesn't work it falls back to UnknownResourceData and it's up to the user or consumer to attempt to recast it as a specific ResourceData subclass
			// If it does brute-force, there should be some presumption of type based on the ResourceType rather than brute-force all members of that ResourceType
			
			if(hint != ResourceHint.Unknown) {
				
				ResourceData data;
				
				if( (data = Heuristic(lang, hint, rawData)) != null ) return data;
				
			} // endif hint is known defined type
			
			return BruteForce(lang, rawData);
			
		}
		
		/// <summary>Tests the ResourceData type as specified by the hint. If it works it returns a ResourceData. If it fails it returns null.</summary>
		private static ResourceData Heuristic(ResourceLang lang, ResourceHint hint, Byte[] rawData) {
			
			// try the hint's suggestion, if it doesn't fall-back on the tried 'n' true brute-force approach
			
			ResourceData rd;
			
			switch(hint) {
				
#region Image
				
				case ResourceHint.Bitmap:
					
					if( BmpImageResourceData.TryCreate(lang, rawData, out rd ) ) return rd;
					break;
					
				case ResourceHint.Gif:
					
					if( GifImageResourceData.TryCreate(lang, rawData, out rd ) ) return rd;
					break;
					
				case ResourceHint.Jpeg:
					
					if( JpegImageResourceData.TryCreate(lang, rawData, out rd ) ) return rd;
					break;
					
				case ResourceHint.Png:
					
					if( PngImageResourceData.TryCreate(lang, rawData, out rd ) ) return rd;
					break;
					
#endregion
				
#region Directory
				
				case ResourceHint.CursorDirectory:
					
					if( CursorDirectoryResourceData.TryCreate(lang, rawData, out rd) ) return rd;
					
					break;
					
				case ResourceHint.IconDirectory:
					
					if( IconDirectoryResourceData.TryCreate(lang, rawData, out rd) ) return rd;
					
					break;
				
#endregion
					
/*					case Win32ResourceType.CursorImage:
				case Win32ResourceType.IconImage:
					
					retval = new IconCursorImageResourceData(lang, rawData);
					break;
				
//					case Win32ResourceType.CursorAnimated: // I have zero documentation on these 'animated' types
//					case Win32ResourceType.IconAnimated:
				case Win32ResourceType.CursorDirectory:
				case Win32ResourceType.IconDirectory:
					
					retval = new IconCursorDirectoryResourceData(lang, rawData);
					break;
					
				case Win32ResourceType.Manifest:
					
					retval = new XmlHtmlResourceData(lang, rawData);
					break;
					
				case Win32ResourceType.Version:
					
					retval = new VersionResourceData(lang, rawData);
					break;
					*/
			}
			
			return null;
			
		}
		
		private static ResourceData BruteForce(ResourceLang lang, Byte[] rawData) {
			
			// fail for now
			
			return UnknownResourceData.TryCreate(lang, rawData);
			
		}
		
	}
	
	internal enum ResourceHint {
		
		/// <summary>The ResourceType is neither a known IntegerId or StringId.</summary>
		Unknown                 = 0,
		
		// Supported IntegerIds
		CursorImage             = 1,
		Bitmap                  = 2,
		IconImage               = 3,
		Menu                    = 4,
		Dialog                  = 5,
		StringTable             = 6,
		FontDirectory           = 7,
		Font                    = 8,
		Accelerator             = 9,
		RCData                  = 10,
		MessageTable            = 11,
		CursorDirectory         = 12,
		IconDirectory           = 14,
		Version                 = 16,
		DlgInclude              = 17,
		PlugAndPlay             = 19,
		Vxd                     = 20,
		CursorAnimated          = 21,
		IconAnimated            = 22,
		Html                    = 23,
		Manifest                = 24,
		
		// Supported StringIds
		
		Png,
		Gif,
		Jpeg
	}
	
	public delegate Boolean TryCreate(ResourceLang lang, Byte[] rawData, out ResourceData data);
	
}

