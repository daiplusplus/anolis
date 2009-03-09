using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Anolis.Core {
	
	public sealed partial class ResourceName {
		
		public ResourceIdentifier     Identifier { get; private set; }
		public ResourceLangCollection Langs      { get; private set; }
		
		public ResourceType           Type       { get; private set; }
		
		private List<ResourceLang> _langs;
		
		internal ResourceName(IntPtr namePointer, ResourceType type) {
			
			Identifier = new ResourceIdentifier(namePointer);
			Type       = type;
			
			_langs     = new List<ResourceLang>();
			Langs      = new ResourceLangCollection(_langs);
			
		}
		
		public override string ToString() {
			return Identifier.FriendlyName;
		}
		
		internal List<ResourceLang> UnderlyingLangs { get { return _langs; } }
		
	}
	
	public class ResourceNameCollection : ReadOnlyCollection<ResourceName> {
		internal ResourceNameCollection(List<ResourceName> list) : base(list) {}
		
		public ResourceName this[ResourceIdentifier nameId] {
			get {
				foreach(ResourceName name in this) if(name.Identifier.Equals(nameId)) return name;
				return null;
			}
		}
	}
	
}
