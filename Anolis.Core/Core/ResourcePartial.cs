using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Security.Permissions;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core {
	
#if RESOURCE_IEQUATABLE
	
	// TODO: Boring Fx-compliance, IEquatable, and operator-overloading code goes here
	
	public partial class ResourceType : IEquatable<ResourceType> {
		
		public static Boolean Equals(ResourceType x, ResourceType y) {
			
			if( Object.ReferenceEquals(x, y) ) return true; // Object.ReferenceEquals( null, null ) btw
			
//			if( !Object.ReferenceEquals( x.Source, y.Source ) ) return false;
			
			return ResourceTypeIdentifier.Equals(x.Identifier, y.Identifier);
			
		}
		
		public new static Boolean Equals(Object x, Object y) {
			
			ResourceType xr = x as ResourceType;
			ResourceType yr = y as ResourceType;
			
			return Equals(xr, yr);
			
		}
		
		public Boolean Equals(ResourceType other) {
			
			return ResourceType.Equals( this, other );
		}
		
		public override Boolean Equals(object obj) {
			return ResourceType.Equals( this, obj as ResourceType );
		}
		
		public static Boolean operator ==(ResourceType x, Object y) {
			return ResourceType.Equals( x, y as ResourceType );
		}
		
		public static Boolean operator !=(ResourceType x, Object y) {
			return !ResourceType.Equals( x, y as ResourceType );
		}
		
		public static Boolean operator ==(ResourceType x, ResourceType y) {
			return ResourceType.Equals( x, y );
		}
		
		public static Boolean operator !=(ResourceType x, ResourceType y) {
			return !ResourceType.Equals( x, y );
		}
		
		public override Int32 GetHashCode() {
			
			return this.Identifier.GetHashCode() ^ this.Names.GetHashCode();
		}
		
	}
	
	public partial class ResourceName : IEquatable<ResourceName> {
		
		public static Boolean Equals(ResourceName x, ResourceName y) {
			
			if( Object.ReferenceEquals(x, y) ) return true; // Object.ReferenceEquals( null, null ) btw
			
//			if( !Object.ReferenceEquals( x.Type, y.Type ) ) return false;
			
			return ResourceIdentifier.Equals(x.Identifier, y.Identifier);
			
		}
		
		public new static Boolean Equals(Object x, Object y) {
			
			ResourceName xr = x as ResourceName;
			ResourceName yr = y as ResourceName;
			
			return Equals(xr, yr);
			
		}
		
		public Boolean Equals(ResourceName other) {
			
			return ResourceName.Equals( this, other );
		}
		
		public override Boolean Equals(object obj) {
			return ResourceName.Equals( this, obj as ResourceName );
		}
		
		public static Boolean operator ==(ResourceName x, Object y) {
			return ResourceName.Equals( x, y as ResourceName );
		}
		
		public static Boolean operator !=(ResourceName x, Object y) {
			return !ResourceName.Equals( x, y as ResourceName );
		}
		
		public static Boolean operator ==(ResourceName x, ResourceName y) {
			return ResourceName.Equals( x, y );
		}
		
		public static Boolean operator !=(ResourceName x, ResourceName y) {
			return !ResourceName.Equals( x, y );
		}
		
		public override int GetHashCode() {
			
			
			
		}
		
	}
	
	public partial class ResourceLang : IEquatable<ResourceLang> {
		
		public static Boolean Equals(ResourceLang x, ResourceLang y) {
			
			if( Object.ReferenceEquals(x, y) ) return true; // Object.ReferenceEquals( null, null ) btw
			
//			if( !Object.ReferenceEquals( x.Name, y.Name ) ) return false;
			
			return x.LanguageId == y.LanguageId;
			
		}
		
		public new static Boolean Equals(Object x, Object y) {
			
			ResourceLang xr = x as ResourceLang;
			ResourceLang yr = y as ResourceLang;
			
			return Equals(xr, yr);
			
		}
		
		public Boolean Equals(ResourceLang other) {
			
			return ResourceName.Equals( this, other );
		}
		
		public override Boolean Equals(object obj) {
			return ResourceName.Equals( this, obj as ResourceLang );
		}
		
		public static Boolean operator ==(ResourceLang x, Object y) {
			return ResourceLang.Equals( x, y as ResourceLang );
		}
		
		public static Boolean operator !=(ResourceLang x, Object y) {
			return !ResourceLang.Equals( x, y as ResourceLang );
		}
		
		public static Boolean operator ==(ResourceLang x, ResourceLang y) {
			return ResourceLang.Equals( x, y );
		}
		
		public static Boolean operator !=(ResourceLang x, ResourceLang y) {
			return !ResourceLang.Equals( x, y );
		}
		
	}
	
#endif
	
}