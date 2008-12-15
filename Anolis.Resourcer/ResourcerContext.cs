using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using Anolis.Core;
using Anolis.Resourcer.TypeViewers;
using Anolis.Resourcer.Settings;

namespace Anolis.Resourcer {
	
	/// <summary>All the information for Resourcer in one place.</summary>
	internal sealed class ResourcerContext {
		
		private ResourceSource _source;
		
		private List<TypeViewer> _viewers;
		
		private Mru _mru;
		private Settings.Settings _settings;
		
		public ResourcerContext() {
			
			_settings = Anolis.Resourcer.Settings.Settings.Default;
			_settings.Upgrade();
			
			if( _settings.MruList == null ) _settings.MruList = new StringCollection();
			
			_viewers = new List<TypeViewer>();
			_mru     = new Mru( _settings.MruCount, _settings.MruList, StringComparison.InvariantCultureIgnoreCase );
			
		}
		
		/// <summary>Saves the ResourcerContext state to the Settings.</summary>
		public void Save() {
			
			_settings.MruList.AddRange( _mru.Items ); 
			_settings.MruCount = _mru.Capacity;
			
			_settings.Save();
			
		}
		
		public ResourceSource Source { get; set; }
		public Mru            Mru    { get { return _mru; } }
		
		///////////////////////
		
		
		
	}
}
