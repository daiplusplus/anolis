using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core {
	
	/// <summary>Utility class used to identify Resource Types and Resource Names</summary>
	public class ResourceIdentifier : IEquatable<ResourceIdentifier>, IDisposable {
		
		public Int32? IntegerId { get; private set; }
		public String StringId  { get; private set; }
		
		public String FriendlyName { get; private set; }
		public IntPtr NativeId     { get; private set; }
		
		/// <summary>Constructs a new instance of ResourceIdentifier using the LPCTSTR identifier located at the specified pointer.</summary>
		/// <param name="isType">True if this is an identifier for a ResourceType rather than a ResourceName</param>
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
		public ResourceIdentifier(IntPtr id) {
			
			if( IsStrResource( id ) ) {
				
				StringId = Marshal.PtrToStringAuto( id );
				NativeId = Marshal.StringToHGlobalAuto(StringId); // need to reallocate it
				
				if(StringId.StartsWith("#", StringComparison.Ordinal)) { // if the string begins with '#' then the rest of it is a decimal number that is the integer id
					
					String deci = StringId.Substring(1);
					Int32 i;
					
					if( Int32.TryParse( deci, System.Globalization.NumberStyles.Integer, Cult.InvariantCulture, out i ) ) {
						
						IntegerId = i;
						NativeId  = new IntPtr(i);
						StringId  = null;
					}
					
				} else {
					
					FriendlyName = '"' + StringId + '"';
					
				}
				
			} else {
				
				IntegerId = id.ToInt32();
				NativeId  = new IntPtr(IntegerId.Value);
				FriendlyName = _resourceTypeFriendlyNames.ContainsKey( IntegerId.Value ) ? _resourceTypeFriendlyNames[ IntegerId.Value ] : _resourceTypeFriendlyNames[0] + " (" + id.ToString() + ")";
				
			}
			
		}
		
		~ResourceIdentifier() {
			Dispose();
		}
		
		public void Dispose() {
			if( StringId != null ) Marshal.FreeHGlobal( NativeId );
		}
		
		public ResourceIdentifier(Int32 integerId) { IntegerId = integerId; }
		public ResourceIdentifier(String stringId) { StringId  = stringId; }
		
		/// <summary>Implementation of the IS_INTRESOURCE macro defined in winuser.h</summary>
		private static Boolean IsStrResource(IntPtr id) {
			return (Int64)id > 0xFFFF;
		}
		
		public override String ToString() { return FriendlyName; }
		
#region Comparison
		
		public Boolean Equals(ResourceIdentifier other) {
			
			if( Object.ReferenceEquals( this, other ) ) return true;
			if( Object.ReferenceEquals( other, null)) return false;
			
			if( IntegerId != null && (IntegerId == other.IntegerId) ) return true;
			
			if( IntegerId == null && (other.IntegerId == null) ) {
				return StringId.Equals( other.StringId );
			}
			
			return false;
			
		}
		
		// TODO: Fx Compliance
		
#endregion
		
#region Friendly Names
		
		private static System.Collections.Generic.Dictionary<Int32,String> _resourceTypeFriendlyNames;
		
		static ResourceIdentifier() {
			_resourceTypeFriendlyNames = new System.Collections.Generic.Dictionary<Int32,String>();
			_resourceTypeFriendlyNames.Add(0, "Unknown");
			_resourceTypeFriendlyNames.Add(1, "Cursor (DD)");
			_resourceTypeFriendlyNames.Add(2, "Bitmap");
			_resourceTypeFriendlyNames.Add(3, "Icon (DD)");
			_resourceTypeFriendlyNames.Add(4, "Menu");
			_resourceTypeFriendlyNames.Add(5, "Dialog");
			_resourceTypeFriendlyNames.Add(6, "String Table");
			_resourceTypeFriendlyNames.Add(7, "Font Directory");
			_resourceTypeFriendlyNames.Add(8, "Font");
			_resourceTypeFriendlyNames.Add(9, "Accelerator");
			_resourceTypeFriendlyNames.Add(10, "RC Data");
			_resourceTypeFriendlyNames.Add(11, "Message Table");
			_resourceTypeFriendlyNames.Add(12, "Cursor (DI)");
			// 13 = unknown
			_resourceTypeFriendlyNames.Add(14, "Icon (DI)");
			// 15 = unknown
			_resourceTypeFriendlyNames.Add(16, "Version");
			_resourceTypeFriendlyNames.Add(17, "DLG Include");
			// 18 = unknown
			_resourceTypeFriendlyNames.Add(19, "Plug and Play");
			_resourceTypeFriendlyNames.Add(20, "VXD");
			_resourceTypeFriendlyNames.Add(21, "Cursor (Animated)");
			_resourceTypeFriendlyNames.Add(22, "Icon (Animated)");
			_resourceTypeFriendlyNames.Add(23, "HTML");
			_resourceTypeFriendlyNames.Add(24, "Manifest");
		}
		
#endregion
		
	}
	
	public enum KnownWin32ResourceType {
		CursorDeviceDependent   = 1,
		Bitmap                  = 2,
		IconDeviceDependent     = 3,
		Menu                    = 4,
		Dialog                  = 5,
		StringTable             = 6,
		FontDirectory           = 7,
		Font                    = 8,
		Accelerator             = 9,
		RCData                  = 10,
		MessageTable            = 11,
		CursorDeviceIndependent = 12,
		// no 13
		IconDeviceIndependent   = 14,
		// no 15
		Version                 = 16,
		DlgInclude              = 17,
		// no 18
		PlugAndPlay             = 19,
		Vxd                     = 20,
		CursorAnimated          = 21,
		IconAnimated            = 22,
		Html                    = 23,
		Manifest                = 24,
		Custom                  = -1, // See StringId
		Unknown                 =  0  // Unknown non-string ID resource type
	}
	
}
