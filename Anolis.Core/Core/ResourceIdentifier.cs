using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core {
	
	/// <summary>Utility class used to identify Resource Types and Resource Names</summary>
	public class ResourceIdentifier : IEquatable<ResourceIdentifier> {
		
		public Int32? IntegerId { get; private set; }
		public String StringId  { get; private set; }
		
		public String FriendlyName { get; private set; }
		
		private Boolean _isType;
		
		/// <summary>Constructs a new instance of ResourceIdentifier using the LPCTSTR identifier located at the specified pointer.</summary>
		/// <param name="isType">True if this is an identifier for a ResourceType rather than a ResourceName</param>
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
		public ResourceIdentifier(IntPtr id, Boolean isType) {
			
			if( IsStrResource( id ) ) {
				
				StringId = Marshal.PtrToStringAuto( id );
				
				if(StringId.StartsWith("#", StringComparison.Ordinal)) { // if the string begins with '#' then the rest of it is a decimal number that is the integer id
					
					String deci = StringId.Substring(1);
					Int32 i;
					
					if( Int32.TryParse( deci, System.Globalization.NumberStyles.Integer, Cult.InvariantCulture, out i ) ) {
						
						IntegerId = i;
						StringId = null;
					}
					
				} else {
					
					StringId = '"' + StringId + '"';
					
				}
				
			} else {
				
				IntegerId = id.ToInt32();
				
			}
			
		}
		
		public ResourceIdentifier(Int32 integerId) { IntegerId = integerId; }
		public ResourceIdentifier(String stringId) { StringId  = stringId; }
		
		/// <summary>Implementation of the IS_INTRESOURCE macro defined in winuser.h</summary>
		private static Boolean IsStrResource(IntPtr id) {
			return (Int64)id > 0xFFFF;
		}
		
		public override String ToString() { return FriendlyName; }
		
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
		public IntPtr GetNativeId() {
			
			if( IntegerId == null ) {
				
				return Marshal.StringToHGlobalAuto( StringId );
				
			} else {
				return (IntPtr)IntegerId;
			}
			
		}
		
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
		
	}
	
}
