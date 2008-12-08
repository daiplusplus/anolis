using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Security.Permissions;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core {
	
	// TODO: Boring Fx-compliance, IEquatable, and operator-overloading code goes here
	
	public partial class ResourceType : IEquatable<ResourceType> {
		
		public Boolean Equals(ResourceType other) {
			
			if( Object.ReferenceEquals( this, other ) ) return true;
			if( Object.ReferenceEquals( other, null)) return false;
			if( !Object.ReferenceEquals( this.Source, other.Source ) ) return false;
			
			return this.Identifier.Equals( other.Identifier );
		}
		
		public override Boolean Equals(object obj) {
			return Equals( (ResourceType)obj );
		}
		
		public static Boolean operator ==(Object x, Object y) {
			
			if(x == null ^ y == null) return false;
			if(x == null) return true; // since y == null
			
			
			
			return x.Equals(y);
			
		}
		
	}
	
	public partial class ResourceName : IEquatable<ResourceName> {
		
		public Boolean Equals(ResourceName other) {
			
			if( Object.ReferenceEquals( this, other ) ) return true;
			if( Object.ReferenceEquals( other, null)) return false;
			if( !Object.ReferenceEquals( this.Type, other.Type ) ) return false;
			
			return this.Identifier.Equals( other.Identifier );
		}
		
		public override Boolean Equals(object obj) {
			ResourceName other = obj as ResourceName;
			if(other == null) return false;
			return this.Equals( other );
		}
		
	}
	
	public partial class ResourceLang : IEquatable<ResourceLang> {
		
		public Boolean Equals(ResourceLang other) {
			
			if( Object.ReferenceEquals( this, other ) ) return true;
			if( Object.ReferenceEquals( other, null)) return false;
			if( !Object.ReferenceEquals( this.Name, other.Name ) ) return false;
			
			return LanguageId == other.LanguageId;
			
		}
		
		public override Boolean Equals(object obj) {
			ResourceLang other = obj as ResourceLang;
			if(other == null) return false;
			return this.Equals( other );
		}
		
	}
	
}