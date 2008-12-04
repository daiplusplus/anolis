using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Security.Permissions;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core {
	
	public class ResourceTypeCollection : ReadOnlyCollection<ResourceType> {
		public ResourceTypeCollection(List<ResourceType> list) : base(list) {}
	}
	
	public class ResourceNameCollection : ReadOnlyCollection<ResourceName> {
		public ResourceNameCollection(List<ResourceName> list) : base(list) {}
	}
	
	public class ResourceLangCollection : ReadOnlyCollection<ResourceLang> {
		public ResourceLangCollection(List<ResourceLang> list) : base(list) {}
	}
	
	public interface IResourceSource : IDisposable {
		
		ResourceTypeCollection GetResources();
		
		void AddResource(Resource resource);
		
		void RemoveResource(Resource resource);
		
		void CommitChanges();
		
		void Rollback();
		
	}
	
	public class ResourceType : IEquatable<Win32ResourceType> {
		
		public ResourceIdentifier     Identifier { get; private set; }
		public ResourceNameCollection Names      { get; private set; }
		
		public IResourceSource        Source     { get; private set; }
		
		/// <summary>Constructs a Win32 resource type based on a Win32 resource type LPCTSTR.</summary>
		internal ResourceType(IntPtr typePointer, IResourceSource source) {
			
			Identifier = new ResourceIdentifier(typePointer, true);
			// TODO: init Names
			Source     = source;
			
		}
		
		public override string ToString() {
			return Identifier.FriendlyName;
		}
		
		public Boolean Equals(ResourceType other) {
			
			if( Object.ReferenceEquals( this, other ) ) return true;
			if( Object.ReferenceEquals( other, null)) return false;
			if( !Object.ReferenceEquals( this.Source, other.Source ) ) return false;
			
			return this.Identifier.Equals( other.Identifier );
		}
		
		public override Boolean Equals(object obj) {
			return Equals( (ResourceType)obj );
		}
		
		// TODO: Fx Compliance
		
	}
	
	public class ResourceName : IEquatable<ResourceName> {
		
		public ResourceIdentifier     Identifier { get; private set; }
		public ResourceLangCollection Langs      { get; private set; }
		
		public ResourceType           Type       { get; private set; }
		
		internal ResourceName(IntPtr namePointer, ResourceType type) {
			
			Identifier = new ResourceIdentifier(namePointer, false);
			// TODO: init Langs
			Type       = type;
			
		}
		
		public override string ToString() {
			return Identifier.FriendlyName;
		}
		
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
	
	public class ResourceLang : IEquatable<ResourceLang> {
		
		[CLSCompliant(false)]
		public UInt16 LanguageId { get; private set; }
		
		public ResourceName Name { get; private set; }
		
		internal ResourceLang(UInt16 languageId, ResourceName name) {
			
			LanguageId = languageId;
			Name       = name;
		}
		
		public override string ToString() {
			return LanguageId.ToString(Cult.InvariantCulture);
		}
		
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
		
		// TODO: Arrange Resource data loading
		
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
		public Byte[] GetData() {
			
			return Name.Type.Source.GetResourceData( this );
			
		}
		
	}
	
	
	
	
}
