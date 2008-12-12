using System;
using System.Collections;
using System.Collections.Generic;

using Anolis.Core;
using Anolis.Resourcer.TypeViewers;

namespace Anolis.Resourcer {
	
	/// <summary>All the information for Resourcer in one place.</summary>
	public sealed class ResourcerContext {
		
		private ResourceSource _source;
		
		private List<TypeViewer> _viewers;
		private Dictionary<ResourceType, List<TypeViewer>> _viewersForType;
		
		public ResourcerContext() {
			
			_viewers        = new List<TypeViewer>();
			_viewersForType = new Dictionary<ResourceType,List<TypeViewer>>();
			
		}
		
		private void ResetState() {
			
			
			
		}
		
		public ResourceSource Source {
			get { return _source; }
			set {
				
				ResetState();
				
				_source = value;
			}
		}
		
		///////////////////////
		
		
		
	}
}
