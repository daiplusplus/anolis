using System;
using IEnumerator = System.Collections.IEnumerator;
using IEnumerable = System.Collections.IEnumerable;
using System.Collections.Generic;

using Anolis.Core.Data;

namespace Anolis.Core {
	
	/// <summary>The stateful set of resources within a ResourceSource.</summary>
	public abstract partial class ResourceSource {
		
		private void InitialiseEnumerables() {
			
			_types         = new List<ResourceType>();
			
			AllTypes       = new ResourceTypeCollection(_types);
			AllLangs       = new ResourceLangEnumerable( AllTypes, null );
			AllActiveLangs = new ResourceLangEnumerable( AllTypes, l => l.Action != ResourceDataAction.None );
			AllLoadedLangs = new ResourceLangEnumerable( AllTypes, l => l.DataIsLoaded );
		}
		
		private List<ResourceType> _types;
		
		public Boolean HasUnsavedChanges {
			get {
				
				if( IsReadOnly ) return false;
				
				IEnumerator<ResourceLang> e = AllActiveLangs.GetEnumerator();
				return e.MoveNext(); // if there is just one ResourceLang with a non-None action then this will return true
				
			}
		}
		
		//////////////////////////////////
		
		protected ResourceType UnderlyingFind(Predicate<ResourceType> match) {
			
			return _types.Find(match);
		}
		
		protected ResourceName UnderlyingFind(ResourceType type, Predicate<ResourceName> match) {
			
			return type.UnderlyingNames.Find( match );
		}
		
		//////////////////////////////////
		
#region Resource Tree Mutators
		
		protected void UnderlyingClear() {
			
			foreach(ResourceType type in _types) {
				
				foreach(ResourceName name in type.UnderlyingNames) {
					
					name.Identifier.Dispose();
					
					name.UnderlyingLangs.Clear();
					
				}
				
				type.Identifier.Dispose();
				
				type.UnderlyingNames.Clear();
			}
			
			_types.Clear();
		}
		
	#region Add
		
		/// <summary>Adds the specified ResourceData to this ResourceSource instance. If the specified ResourceType or ResourceName or ResourceLang does not exist they will be created. If the specified ResourceLang does exist an exception will be thrown (as you're meant to use Update to replace existing resources)</summary>
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
			if(LoadMode > 0) foreach(ResourceLang lon in name.Langs) if(lon.LanguageId == langId) { lang = lon; break; }
			if(lang != null) throw new AnolisException("The specified ResourceLang already exists");
			
			lang = new ResourceLang(langId, name, data);
			lang.Action = ResourceDataAction.Add;
			
			name.UnderlyingLangs.Add( lang );
			
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
		
	#endregion
	
	#region Update
		
		/// <summary>Provides an alternative way to update a ResourceLang's data, originally designed for use in Blind mode.</summary>
		public ResourceLang Update(ResourceTypeIdentifier typeId, ResourceIdentifier nameId, UInt16 langId, ResourceData newData) {
			
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
			if(LoadMode > 0) foreach(ResourceLang lon in name.Langs) if(lon.LanguageId == langId) { lang = lon; break; }
			
			if(lang == null)
				lang = new ResourceLang(langId, name, newData);
			
			lang.Action = ResourceDataAction.Update;
			
			name.UnderlyingLangs.Add( lang );
			
			return lang;
		}
		
	#endregion
	
	#region Cancel
		
		public void Cancel(ResourceLang lang) {
			
			switch(lang.Action) {
				case ResourceDataAction.Add:
					
					UnderlyingRemove( lang.Data );
					break;
					
				case ResourceDataAction.Delete:
					
					lang.Action = ResourceDataAction.None;
					break;
					
				case ResourceDataAction.None:
					break;
					
				case ResourceDataAction.Update:
					
					lang.Rollback();
					break;
			}
		}
		
	#endregion
	
	#region Remove
		
		public ResourceLang Remove(ResourceTypeIdentifier typeId, ResourceIdentifier nameId, UInt16 langId) {
			
			if(LoadMode > 0) throw new InvalidOperationException("This Remove overload can only be used in Blind mode");
			
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
			if(lang != null && lang.Action != ResourceDataAction.Delete) throw new AnolisException("The specified ResourceLang already exists with an Action other than to Delete");
			
			lang = new ResourceLang(langId, name);
			lang.Action = ResourceDataAction.Delete;
			
			name.UnderlyingLangs.Add( lang );
			
			return lang;
		}
		
		public void Remove(ResourceLang lang) {
			
			EnsureReadOnly();
			
			switch(lang.Action) {
				
				case ResourceDataAction.Add:
					
					lang.Data.OnRemove( true, new ResourceData.Remove( Remove ) );
					
					Cancel( lang );
					break;
					
				case ResourceDataAction.Update:
				case ResourceDataAction.Delete:
				case ResourceDataAction.None:
					
					lang.Action = ResourceDataAction.Delete;
					
					lang.Data.OnRemove( false, new ResourceData.Remove( Remove ) );
					
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
		
	#endregion
		
#endregion
		
		/// <summary>Returns an unused integer ResourceIdentifier for a ResourceName that is not currently being used.</summary>
		public virtual ResourceIdentifier GetUnusedName(ResourceTypeIdentifier typeId) {
			
			// get the type then enumerate through all the integer Ids.
			ResourceType type = this.AllTypes[ typeId ];
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
		
		public virtual ResourceName GetName(ResourceTypeIdentifier typeId, ResourceIdentifier nameId) {
			
			if(LoadMode == 0) throw new ArgumentException("The specified ResourceSource has not enumerated its resources");
			
			foreach(ResourceType type in AllTypes) {
				
				if( type.Identifier.Equals(typeId) ) {
					
					foreach(ResourceName name in type.Names) {
						
						if(name.Identifier.Equals(nameId) ) {
							
							return name;
						}
					}
					
				}//if
				
			}//foreach
			
			return null;
			
		}
		
		public virtual ResourceLang GetLang(ResourceTypeIdentifier typeId, ResourceIdentifier nameId, UInt16 langId) {
			
			ResourceName name = GetName(typeId, nameId);
			if(name == null) return null;
			
			foreach(ResourceLang lang in name.Langs) {
				
				if(lang.LanguageId == langId) return lang;
			}
			
			return null;
		}
		
		//////////////////////////////////
		
		private void EnsureReadOnly() {
			if( IsReadOnly ) throw new InvalidOperationException("This ResourceSet is marked as read-only.");
		}
		
		//////////////////////////////////
		
#region Enumerables
		
		public ResourceTypeCollection AllTypes       { get; private set; }
		
		public ResourceLangEnumerable AllLangs       { get; internal set; }
		
		public ResourceLangEnumerable AllActiveLangs { get; internal set; }
		
		public ResourceLangEnumerable AllLoadedLangs { get; internal set; }
		
#endregion
		
	}
}
