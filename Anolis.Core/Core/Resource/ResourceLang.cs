using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Anolis.Core.Data;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core {
	
	public sealed partial class ResourceLang {
		
		public UInt16 LanguageId { get; private set; }
		
		public ResourceName Name { get; private set; }
		
		private ResourceData _data;
		
		////////////////////////////////
		
		/// <summary>Constructs a ResourceLang with the ResourceData already loaded. For when adding resources to a PE rather than loading from.</summary>
		public ResourceLang(UInt16 languageId, ResourceName name, ResourceData data) {
			LanguageId = languageId;
			Name       = name;
			_data      = data;
		}
		
		public ResourceLang(UInt16 languageId, ResourceName name) : this(languageId, name, null) {
		}
		
		public override String ToString() {
			return LanguageId.ToString(Cult.InvariantCulture);
		}
		
#region ResourceData Operations
		
		/// <summary>Lazy-loads the ResourceData associated with this ResourceLang from the Resource Source if the resource data is not already loaded.</summary>
		public ResourceData Data {
			get {
				
				if(_data == null) {
					
					_data = this.Name.Type.Source.GetResourceData(this);
					
				}
				
				return _data;
			}
		}
		
		/// <summary>Indicates if .Data has been loaded already. Useful for consumer optimisation.</summary>
		public Boolean DataIsLoaded { get { return _data != null; } }
		
		public void SwapData(ResourceData data) {
			
			data.Action = ResourceDataAction.Update;
			
			data.Lang = this;
			
			_data = data;
			
		}
		
#endregion
		
	}
	
	public class ResourceLangCollection : ReadOnlyCollection<ResourceLang> {
		internal ResourceLangCollection(List<ResourceLang> list) : base(list) {}
	}
	
}
