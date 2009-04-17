using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Anolis.Core.Source;

namespace Anolis.Core {
	
	public sealed partial class ResourceType {
		
		public ResourceTypeIdentifier Identifier { get; private set; }
		public ResourceNameCollection Names      { get; private set; }
		
		public ResourceSource         Source     { get; private set; }
		
		private List<ResourceName> _names;
		
		internal ResourceType(ResourceTypeIdentifier typeId, ResourceSource source) {
			
			Identifier = typeId;
			Source     = source;
			
			_names     = new List<ResourceName>();
			Names      = new ResourceNameCollection(_names); // ResourceNameCollection is a read-only decorator of any List
			
		}
		
		/// <summary>Constructs a Win32 resource type based on a Win32 resource type LPCTSTR.</summary>
		internal ResourceType(IntPtr typePointer, ResourceSource source) {
			
			Identifier = new ResourceTypeIdentifier(typePointer);
			Source     = source;
			
			_names     = new List<ResourceName>();
			Names      = new ResourceNameCollection(_names); // ResourceNameCollection is a read-only decorator of any List
			
		}
		
		public override string ToString() {
			return Identifier.FriendlyName;
		}
		
		internal List<ResourceName> UnderlyingNames { get { return _names; } }
		
	}
	
	public class ResourceTypeCollection : ReadOnlyCollection<ResourceType> {
		
		internal ResourceTypeCollection(List<ResourceType> list) : base(list) {}
		
		public ResourceType this[ResourceTypeIdentifier typeId] {
			get {
				foreach(ResourceType type in this) if(type.Identifier.Equals(typeId)) return type;
				return null;
			}
		}
	}
	
}
