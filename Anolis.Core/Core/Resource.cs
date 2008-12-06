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
	
	public abstract class ResourceSource : IDisposable {
		
		public Boolean IsReadOnly { get; protected set; }
		
		public abstract ResourceTypeCollection GetResources();
		
		public abstract void AddResource(ResourceLang resource);
		
		public abstract void RemoveResource(ResourceLang resource);
		
		public abstract void CommitChanges();
		
		public abstract void Rollback();
		
		/// <summary>Extracts the Resource Data for the specified Resource.</summary>
		public abstract ResourceData GetResourceData(ResourceLang lang);
		
		public abstract void Dispose();
		
		//////////////////////
		
		public static ResourceSource Open(String filename, Boolean readOnly) {
			
			// get the file type of the file to load
			// if PE executable (a dll, native exe, etc)
			
			ResourceSource src = new PE.PESource(filename, readOnly);
			
			throw new NotImplementedException();
			
		}
		
	}
	
	public partial class ResourceType : IEquatable<ResourceType> {
		
		public ResourceIdentifier     Identifier { get; private set; }
		public ResourceNameCollection Names      { get; private set; }
		
		public ResourceSource         Source     { get; private set; }
		
		private List<ResourceName> _names;
		
		/// <summary>Constructs a Win32 resource type based on a Win32 resource type LPCTSTR.</summary>
		public ResourceType(IntPtr typePointer, ResourceSource source) {
			
			Identifier = new ResourceIdentifier(typePointer, true);
			Source     = source;
			
			_names     = new List<ResourceName>();
			Names      = new ResourceNameCollection(_names); // ResourceNameCollection is a read-only decorator of any List
			
		}
		
		public override string ToString() {
			return Identifier.FriendlyName;
		}
		
	}
	
	public partial class ResourceName : IEquatable<ResourceName> {
		
		public ResourceIdentifier     Identifier { get; private set; }
		public ResourceLangCollection Langs      { get; private set; }
		
		public ResourceType           Type       { get; private set; }
		
		private List<ResourceLang> _langs;
		
		public ResourceName(IntPtr namePointer, ResourceType type) {
			
			Identifier = new ResourceIdentifier(namePointer, false);
			Type       = type;
			
			_langs     = new List<ResourceLang>();
			Langs      = new ResourceLangCollection(_langs);
			
		}
		
		public override string ToString() {
			return Identifier.FriendlyName;
		}
		
	}
	
	public partial class ResourceLang : IEquatable<ResourceLang> {
		
		public UInt16 LanguageId { get; private set; }
		
		public ResourceName Name { get; private set; }
		
		private ResourceData _data;
		
		////////////////////////////////
		
		public ResourceLang(UInt16 languageId, ResourceName name) {
			
			LanguageId = languageId;
			Name       = name;
		}
		
		/// <summary>Constructs a ResourceLang with the ResourceData already loaded. For when adding resources to a PE rather than loading from.</summary>
		public ResourceLang(UInt16 languageId, ResourceName name, ResourceData data) : this(languageId, name) {
			
			_data = data;
		}
		
		public override string ToString() {
			return LanguageId.ToString(Cult.InvariantCulture);
		}
		
		/// <summary>Lazy-loads the ResourceData associated with this ResourceLang from the Resource Source if the resource data is not already loaded.</summary>
		public ResourceData GetData() {
			
			if(_data == null) {
				
				_data = this.Name.Type.Source.GetResourceData(this);
				
			}
			
			return _data;
		}
		
		
	}
	
	
	
	
}
