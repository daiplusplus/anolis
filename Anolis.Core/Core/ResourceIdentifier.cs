using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

using NumberStyles = System.Globalization.NumberStyles;
using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core {
	
	/// <summary>Utility class used to identify Resource Names. For ResourceTypes use ResourceTypeIdentifier.</summary>
	public class ResourceIdentifier : IEquatable<ResourceIdentifier>, IDisposable {
		
		public Int32? IntegerId { get; protected set; }
		public String StringId  { get; protected set; }
		
		public String FriendlyName { get; protected set; }
		public IntPtr NativeId     { get; protected set; }
		
		protected ResourceIdentifier() {
		}
		
		/// <summary>Constructs a new instance of ResourceIdentifier using the LPCTSTR identifier located at the specified pointer.</summary>
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
		public ResourceIdentifier(IntPtr id) {
			
			switch(GetIdentifierType(id)) {
				
				case IdentifierType.Integer:
					
					IntegerId = id.ToInt32();
					NativeId  = new IntPtr(IntegerId.Value); // seems odd, creating a value copy of it
					
					FriendlyName = IntegerId.Value.ToString(Cult.CurrentCulture);
					
					break;
					
				case IdentifierType.IntegerString:
					
					IntegerId = Int32.Parse( Marshal.PtrToStringAuto( id ).Substring(1), System.Globalization.NumberStyles.Integer, Cult.InvariantCulture);
					NativeId  = new IntPtr(IntegerId.Value);
					
					FriendlyName = IntegerId.Value.ToString(Cult.CurrentCulture);
					
					break;
					
				case IdentifierType.String:
					
					StringId = Marshal.PtrToStringAuto( id );
					NativeId = Marshal.StringToHGlobalAuto(StringId); // reallocating the string becuase you can't use the original. This is deallocated/freed in the Dispose method (called from the Destructor)
					
					FriendlyName = '"' + StringId + '"';
					
					break;
			}
			
		}
		
		/// <param name="id">Converts this String id into a marshaled NativeId HGlobalAuto.</param>
		public ResourceIdentifier(String id) {
			
			StringId = id;
			NativeId = Marshal.StringToHGlobalAuto(StringId);
			
			FriendlyName = '"' + StringId + '"';
			
		}
		
		public ResourceIdentifier(Int32 id) {
			
			IntegerId = id;
			NativeId  = new IntPtr(IntegerId.Value);
			
			FriendlyName = id.ToString(Cult.InvariantCulture);
			
		}
		
		/// <summary>Creates a ResourceTypeIdentifier instance from a string which may represent a Known Win32 Resource Type, a number, or a string id (in that order)</summary>
		/// <param name="shortStyle">Set to True to allow short-style identifiers like "icon" or "bmp" (for IconDirectory and Bitmap respectively) to be parsed as being Known Win32 types as opposed to their full names</param>
		public static ResourceIdentifier CreateFromString(String ambiguousString) {
			
			if(ambiguousString == null) throw new ArgumentNullException("ambiguousString");
			if(ambiguousString.Length == 0) throw new ArgumentOutOfRangeException("ambiguousString", "Length must be greater than 0");
			
			String ambiguousStringUpper = ambiguousString.ToUpperInvariant();
			
			// is the string a number?
			
			Int32 number;
			NumberStyles numStyle = ambiguousStringUpper.StartsWith("0x") ? NumberStyles.HexNumber : NumberStyles.Integer;
			
			if( Int32.TryParse( ambiguousStringUpper, numStyle, Cult.InvariantCulture, out number ) ) {
				
				return new ResourceIdentifier( new IntPtr( number ) );
			}
			
			// finally, interpret it as a string identifier
			// HACK: I'll forgoe '#' prefix support, it only applies to internal resources anyway
			// what about numbers that are meant to represent strings? I'll do if it anyone encounters a file that uses that system. I'd need a way to denote it, maybe with " symbols?
			
			return new ResourceIdentifier( ambiguousString );
			
		}
		
		
		~ResourceIdentifier() {
			Dispose();
		}
		
		public void Dispose() {
			if( StringId != null ) Marshal.FreeHGlobal( NativeId );
		}
		
		/// <summary>Implementation of the IS_INTRESOURCE macro defined in winuser.h</summary>
		protected static IdentifierType GetIdentifierType(IntPtr id) {
			
			if( (Int64)id > 0xFFFF ) {
				
				// it's a string, but is it a decimal-encoded string or a proper string?
				
				String ids = Marshal.PtrToStringAuto( id );
				
				return ids.StartsWith("#") ? IdentifierType.IntegerString : IdentifierType.String;
				
			} else {
				
				// it's an integer
				return IdentifierType.Integer;
			}
			
		}
		
		public override String ToString() { return FriendlyName; }
		
		protected enum IdentifierType {
			Integer,
			IntegerString,
			String
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
	
	public class ResourceTypeIdentifier : ResourceIdentifier {
		
		/// <param name="id">Converts this String id into a marshaled NativeId HGlobalAuto.</param>
		public ResourceTypeIdentifier(String id) : base(id) {
			
			KnownType = Win32ResourceType.Custom;
			
		}
		
		public ResourceTypeIdentifier(Win32ResourceType type) : base( (int)type ) {
			
			FriendlyName = GetTypeFriendlyName( IntegerId.Value );
			KnownType    = type;
			
		}
		
		public ResourceTypeIdentifier(IntPtr id) {
			
			switch(GetIdentifierType(id)) {
				
				case IdentifierType.Integer:
					
					IntegerId = id.ToInt32();
					NativeId  = new IntPtr(IntegerId.Value); // seems odd, creating a value copy of it
					
					FriendlyName = GetTypeFriendlyName( IntegerId.Value ); // lookup friendly name of integer ID
					KnownType    = GetKnownType( IntegerId.Value );
					
					break;
					
				case IdentifierType.IntegerString:
					
					IntegerId = Int32.Parse( Marshal.PtrToStringAuto( id ).Substring(1), System.Globalization.NumberStyles.Integer, Cult.InvariantCulture);
					NativeId  = new IntPtr(IntegerId.Value);
					
					FriendlyName = '#' + GetTypeFriendlyName( IntegerId.Value );
					KnownType    = GetKnownType( IntegerId.Value );
					
					break;
					
				case IdentifierType.String:
					
					StringId = Marshal.PtrToStringAuto( id );
					NativeId = Marshal.StringToHGlobalAuto(StringId); // reallocating the string becuase you can't use the original. This is deallocated/freed in the Dispose method (called from the Destructor)
					
					FriendlyName = '"' + StringId + '"'; // surround typename in quotes
					KnownType    = Win32ResourceType.Custom;
					
					break;
			}
			
			
		}
		
		/// <summary>Creates a ResourceTypeIdentifier instance from a string which may represent a Known Win32 Resource Type, a number, or a string id (in that order)</summary>
		/// <param name="shortStyle">Set to True to allow short-style identifiers like "icon" or "bmp" (for IconDirectory and Bitmap respectively) to be parsed as being Known Win32 types as opposed to their full names</param>
		public static ResourceTypeIdentifier CreateFromString(String ambiguousString, Boolean shortStyle) {
			
			if(ambiguousString == null) throw new ArgumentNullException("ambiguousString");
			if(ambiguousString.Length == 0) throw new ArgumentOutOfRangeException("ambiguousString", "Length must be greater than 0");
			
			String ambiguousStringUpper = ambiguousString.ToUpperInvariant();
			
			if( shortStyle && _resourceTypeShortNames.ContainsKey(ambiguousStringUpper) ) {
				
				return new ResourceTypeIdentifier( _resourceTypeShortNames[ ambiguousStringUpper ] );
			}
			
			// try the Win32ResourceTypes
			
			String[] winNames = Enum.GetNames( typeof(Win32ResourceType) );
			foreach(String name in winNames) {
				
				if( String.Equals( ambiguousStringUpper, name, StringComparison.OrdinalIgnoreCase ) ) {
					
					return new ResourceTypeIdentifier( (Win32ResourceType)Enum.Parse( typeof(Win32ResourceType), name, true ) );
					
				}
			}
			
			// is the string a number?
			
			Int32 number;
			NumberStyles numStyle = ambiguousStringUpper.StartsWith("0x") ? NumberStyles.HexNumber : NumberStyles.Integer;
			
			if( Int32.TryParse( ambiguousStringUpper, numStyle, Cult.InvariantCulture, out number ) ) {
				
				return new ResourceTypeIdentifier( new IntPtr( number ) );
			}
			
			// finally, interpret it as a string identifier
			// HACK: I'll forgoe '#' prefix support, it only applies to internal resources anyway
			// what about numbers that are meant to represent strings? I'll do if it anyone encounters a file that uses that system. I'd need a way to denote it, maybe with " symbols?
			
			return new ResourceTypeIdentifier( ambiguousString );
			
		}
		
		public Win32ResourceType KnownType { get; private set; }
		
#region Type Friendly Names
		
		private static Dictionary<Int32,String> _resourceTypeFriendlyNames;
		
		private static Dictionary<String,Win32ResourceType> _resourceTypeShortNames;
		
		// TODO: Maybe make these entries in a Resources set in this assembly so it can be localised? (irony aside, of course)
		
		static ResourceTypeIdentifier() {
			_resourceTypeFriendlyNames = new Dictionary<Int32,String>();
			_resourceTypeFriendlyNames.Add(0, "Unknown");
			_resourceTypeFriendlyNames.Add(1, "Cursor Image");
			_resourceTypeFriendlyNames.Add(2, "Bitmap");
			_resourceTypeFriendlyNames.Add(3, "Icon Image");
			_resourceTypeFriendlyNames.Add(4, "Menu");
			_resourceTypeFriendlyNames.Add(5, "Dialog");
			_resourceTypeFriendlyNames.Add(6, "String Table");
			_resourceTypeFriendlyNames.Add(7, "Font Directory");
			_resourceTypeFriendlyNames.Add(8, "Font");
			_resourceTypeFriendlyNames.Add(9, "Accelerator");
			_resourceTypeFriendlyNames.Add(10, "RC Data");
			_resourceTypeFriendlyNames.Add(11, "Message Table");
			_resourceTypeFriendlyNames.Add(12, "Cursor Directory");
			// 13 = unknown
			_resourceTypeFriendlyNames.Add(14, "Icon Directory");
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
			
			_resourceTypeShortNames = new Dictionary<String,Win32ResourceType>();
			_resourceTypeShortNames.Add("ICON"  , Win32ResourceType.IconDirectory);
			_resourceTypeShortNames.Add("CURSOR", Win32ResourceType.CursorDirectory);
			_resourceTypeShortNames.Add("BMP"   , Win32ResourceType.Bitmap);
		}
		
		public static String GetTypeFriendlyName(Int32 integerId) {
			
			return _resourceTypeFriendlyNames.ContainsKey( integerId ) ?
				_resourceTypeFriendlyNames[ integerId ] :
				_resourceTypeFriendlyNames[0] + " (" + integerId.ToString() + ")";
		}
		
		private static Win32ResourceType GetKnownType(Int32 integerId) {
			
			if( Enum.IsDefined(typeof(Win32ResourceType), integerId) ) {
				
				return (Win32ResourceType)integerId;
				
			} else {
				
				return Win32ResourceType.Unknown;
				
			}
			
		}
		
#endregion
		
	}
	
	public enum Win32ResourceType {
		/// <summary>1</summary>
		CursorImage             = 1,
		/// <summary>2</summary>
		Bitmap                  = 2,
		/// <summary>3</summary>
		IconImage               = 3,
		/// <summary>4</summary>
		Menu                    = 4,
		/// <summary>5</summary>
		Dialog                  = 5,
		/// <summary>6</summary>
		StringTable             = 6,
		/// <summary>7</summary>
		FontDirectory           = 7,
		/// <summary>8</summary>
		Font                    = 8,
		/// <summary>9</summary>
		Accelerator             = 9,
		/// <summary>10</summary>
		RCData                  = 10,
		/// <summary>11</summary>
		MessageTable            = 11,
		/// <summary>12</summary>
		CursorDirectory         = 12,
		// no 13
		/// <summary>14</summary>
		IconDirectory           = 14,
		// no 15
		/// <summary>16</summary>
		Version                 = 16,
		/// <summary>17</summary>
		DlgInclude              = 17,
		// no 18
		/// <summary>19</summary>
		PlugAndPlay             = 19,
		/// <summary>20</summary>
		Vxd                     = 20,
		/// <summary>21</summary>
		CursorAnimated          = 21,
		/// <summary>22</summary>
		IconAnimated            = 22,
		/// <summary>23</summary>
		Html                    = 23,
		/// <summary>24</summary>
		Manifest                = 24,
		/// <summary>-1 - ResourceType is not a known Win32 Resource Type. Refer to the StringId property for the type's name.</summary>
		Custom                  = -1,
		/// <summary>0 - ResourceType's type identifier refers to an unknown resource type that is not a string.</summary>
		Unknown                 =  0
	}
	
}
