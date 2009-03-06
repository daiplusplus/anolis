using System;
using System.Collections.Generic;

using Anolis.Core.Data;

namespace Anolis.Core {
	
	/// <summary>The stateful set of resources within a ResourceSource.</summary>
	public abstract partial class ResourceSource : IEnumerable<ResourceData> {
		
		private void InitResourceSourceCollections() {
			
			_types = new List<ResourceType>();
			Types  = new ResourceTypeCollection(_types);
			
		}
		
		private List<ResourceType> _types;
		
		public ResourceTypeCollection Types { get; private set; }
		
		public Boolean HasUnsavedChanges {
			get {
				
				if( IsReadOnly ) return false;
				
				foreach(ResourceData data in this) {
					if(data.Action != ResourceDataAction.None) return true;
				}
				
				return false;
			}
		}
		
		//////////////////////////////////
		
		public ResourceLang Add(ResourceTypeIdentifier typeId, ResourceIdentifier nameId, UInt16 langId, ResourceData data) {
			
			EnsureReadOnly();
			
			ResourceType type =  _types.Find( t => t.Identifier.Equals( typeId ));
			if(type == null) {
				// add it
				type = new ResourceType(typeId.NativeId, this);
				
				UnderlyingAdd( type );
			}
			
			ResourceName name = null;
			foreach(ResourceName nom in type.Names) if(nom.Identifier.Equals(nameId)) { name = nom; break; }
			if(name == null) {
				
				name = new ResourceName(nameId.NativeId, type);
				
				type.UnderlyingNames.Add( name );
			}
			
			ResourceLang lang = null;
			foreach(ResourceLang lon in name.Langs) if(lon.LanguageId == langId) { lang = lon; break; }
			if(lang == null) {
				
				lang = new ResourceLang(langId, name, data);
				
				name.UnderlyingLangs.Add( lang );
			}
			
			data.Lang = lang;
			
			if(IsBlind) {
				
				if(data.Action == ResourceDataAction.None)
					data.Action = ResourceDataAction.Add;
				
			} else {
				
				data.Action = ResourceDataAction.Add; // when in blind (i.e. non-interactive) mode allow use of the Add method to add ResourceData instances that act as Update or Delete instructions
				
			}
			
			return lang;
			
		}
		
		/// <summary>Adds the specified ResourceType to the ResourceSource structure without invalidating the structure.</summary>
		protected void UnderlyingAdd(ResourceType type) {
			
			_types.Add( type );
			
		}
		
		/// <summary>Adds the specified ResourceName to the specified ResourceType without invalidating the structure. The ResourceType must already exist in this structure.</summary>
		protected void UnderlyingAdd(ResourceType type, ResourceName name) {
			
			type.UnderlyingNames.Add( name );
			
		}
		
		/// <summary>Adds the specified ResourceLang to the specified ResourceName without invalidating the structure. The ResourceName must already exist in this structure.</summary>
		protected void UnderlyingAdd(ResourceName name, ResourceLang lang) {
			
			name.UnderlyingLangs.Add( lang );
			
		}
		
		//////////////////////////////////
		
		protected ResourceType UnderlyingFind(Predicate<ResourceType> match) {
			
			return _types.Find(match);
		}
		
		protected ResourceName UnderlyingFind(ResourceType type, Predicate<ResourceName> match) {
			
			return type.UnderlyingNames.Find( match );
		}
		
		//////////////////////////////////
		
		public void Cancel(ResourceData data) {
			
			switch(data.Action) {
				case ResourceDataAction.Add:
					
					UnderlyingRemove( data );
					break;
					
				case ResourceDataAction.Delete:
					
					data.Action = ResourceDataAction.None;
					break;
					
				case ResourceDataAction.None:
					break;
					
				case ResourceDataAction.Update:
					
					// TODO: Reload the original ResourceData
					throw new NotImplementedException();
					
//					break;
			}
		}
		
		public void Remove(ResourceData data) {
			
			EnsureReadOnly();
			
			switch(data.Action) {
				case ResourceDataAction.Add:
					
					data.OnRemove( true, new ResourceData.Remove( Remove ) );
					
					Cancel( data );
					break;
					
				case ResourceDataAction.Update:
				case ResourceDataAction.Delete:
				case ResourceDataAction.None:
					
					data.Action = ResourceDataAction.Delete;
					
					data.OnRemove( false, new ResourceData.Remove( Remove ) );
					
					break;
			}
			
		}
		
		protected void UnderlyingRemove(ResourceData data) {
			
			if(data.Lang.Name.Type.Source != this) throw new ArgumentException("Specified ResourceData is not a member of this Source");
			
			ResourceType type = data.Lang.Name.Type;
			ResourceName name = data.Lang.Name;
			ResourceLang lang = data.Lang;
			
			name.UnderlyingLangs.Remove( lang );
			
			if(name.UnderlyingLangs.Count == 0) { // if name has no langs, remove the name
				type.UnderlyingNames.Remove( name );
			}
			
			if(type.UnderlyingNames.Count == 0) { // if type has no names, remove the type
				_types.Remove( type );
			}
			
		}
		
		//////////////////////////////////
		
		public IEnumerator<ResourceData> GetEnumerator() {
			
			return new ResourceEnumerator(this.Types, true);
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			
			return GetEnumerator();
		}
		
		//////////////////////////////////
				
		/// <summary>Returns an unused integer ResourceIdentifier for a ResourceName that is not currently being used.</summary>
		public virtual ResourceIdentifier GetUnusedName(ResourceTypeIdentifier typeId) {
			
			// get the type then enumerate through all the integer Ids.
			ResourceType type = this.Types[ typeId ];
			if(type == null) {
				// then just use "1"
				return new ResourceIdentifier(1);
			}
			
			Int32 biggestIntId = -1;
			foreach(ResourceName name in type.Names) {
				
				ResourceIdentifier id = name.Identifier;
				
				if(id.IntegerId != null) {
					
					if(id.IntegerId > biggestIntId) biggestIntId = id.IntegerId.Value;
				}
			}
			if(biggestIntId == -1) biggestIntId = 0;
			
			return new ResourceIdentifier( biggestIntId + 1 );
			
		}
		
		/// <summary>Returns True if the specified ResourceIdentifier for a ResourceName is currently in use.</summary>
		public virtual Boolean IsNameInUse(ResourceType type, ResourceIdentifier nameId) {
			
			foreach(ResourceName name in type.Names) if(name.Equals( nameId )) return false;
			
			return true;
			
		}
		
		//////////////////////////////////
		
		private void EnsureReadOnly() {
			if( IsReadOnly ) throw new InvalidOperationException("This ResourceSet is marked as read-only.");
		}
		
	}
}
