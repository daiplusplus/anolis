using System;
using System.Collections.Generic;

namespace Anolis.Core {
	
	/// <summary>The stateful set of resources within a ResourceSource.</summary>
	public abstract partial class ResourceSource : IEnumerable<ResourceData> {
		
		/*
		 * Okay, so....
		 * 
		 * ResourceCollection's methods: Add, Remove, etc... don't remove objects from the underlying collections, but just MARK them for that action when ResourceSource.CommitChanges is called
		 * 
		 * The ResourceTypes collections and the rest are exposed via ReadOnlyCollections
		 * 
		 * so far, so good
		 * 
		 * Adding is performed simply:
		 *		public void Add(ResourceIdentifier type, ResourceIdentifier name, uint16 lang, ResourceData data);
		 *		
		 *	and:
		 *		public void Add(ResourceType type, ResourceName name, uint16 lang, ResourceData data); // assuming type, and name already exist in the source
		 * 
		 * this class also has its own enumerator which enumerates all ResourceDatas that have had their data loaded
		 * 
		 * Subclasses of ResourceSource need to be able to add items to the list without them being marked as 'new'
		 *	maybe make THIS the new ResourceSource abstract class? that way they can be protected members
		 * 
		 */
		
		private List<ResourceType> _types;
		
		public ResourceTypeCollection Types { get; private set; }
		
		//////////////////////////////////
		
		public void Add(ResourceType type, ResourceName name, UInt16 lang, ResourceData data) {
			
			EnsureReadOnly();
			
		}
		
		public void Add(ResourceType type, ResourceIdentifier name, UInt16 lang, ResourceData data) {
			
			EnsureReadOnly();
			
		}
		
		public void Add(ResourceIdentifier type, ResourceIdentifier name, UInt16 lang, ResourceData data) {
			
			EnsureReadOnly();
			
		}
		
		/// <summary>Adds the specified ResourceType to the ResourceSource structure without invalidating the structure.</summary>
		protected void UnderlyingAdd(ResourceType type) {
			
			_types.Add( type );
			
		}
		
		/// <summary>Adds the specified ResourceName to the specified ResourceType without invalidating the structure. The ResourceType must already exist in this structure.</summary>
		protected void UnderlyingAdd(ResourceType type, ResourceName name) {
			
			
			
		}
		
		/// <summary>Adds the specified ResourceLang to the specified ResourceName without invalidating the structure. The ResourceName must already exist in this structure.</summary>
		protected void UnderlyingAdd(ResourceName name, ResourceLang lang) {
			
			
			
		}
		
		//////////////////////////////////
		
		protected ResourceType UnderlyingFind(Predicate<ResourceType> match) {
			
			return _types.Find(match);
		}
		
		protected ResourceName UnderlyingFind(ResourceType type, Predicate<ResourceName> match) {
			
			return type.UnderlyingNames.Find( match );
		}
		
		//////////////////////////////////
		
		public void Remove(ResourceData data) {
			
			EnsureReadOnly();
			
		}
		
		protected void UnderlyingRemove(ResourceData data) {
			
			
			
		}
		
		//////////////////////////////////
		
		public IEnumerator<ResourceData> GetEnumerator() {
			
			return new ResourceEnumerator(this.Types, true);
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			
			return GetEnumerator();
		}
		
		//////////////////////////////////
		
		private void EnsureReadOnly() {
			if( IsReadOnly ) throw new InvalidOperationException("This ResourceSet is marked as read-only.");
		}
		
	}
}
