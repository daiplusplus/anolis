using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Windows.Forms;

using Anolis.Core;
using Anolis.Resourcer.TypeViewers;
using Anolis.Resourcer.Settings;

using Cult = System.Globalization.CultureInfo;
using Anolis.Core.Data;
using Anolis.Core.Utility;

namespace Anolis.Resourcer {
	
	/// <summary>All the information for Resourcer in one place.</summary>
	/// <remarks>The hosting UI should ask the user for confirmation before entering methods here. So this class separates user actions with system actions.</remarks>
	internal sealed class ResourcerContext {
		
		
		
		public ResourcerContext() {
			
			
			
		}
		
		/// <summary>Saves the ResourcerContext state to the Settings.</summary>

		
		public String         CurrentPath   { get; private set; }
		public ResourceSource CurrentSource { get; private set; }
		public ResourceLang   CurrentLang   { get; set; }
		public ResourceData   CurrentData   { get; set; }
		
		public Mru            Mru    { get { return _mru; } }
		
		public Settings.Settings Settings { get { return _settings; } }
		
		public event EventHandler CurrentSourceChanged;
		public event EventHandler CurrentDataChanged;
		
		///////////////////////
		
		private void OnCurrentSourceChanged() {
			if(CurrentSourceChanged != null) CurrentSourceChanged(this, EventArgs.Empty);
		}
		
		private void OnCurrentDataChanged() {
			if(CurrentDataChanged != null) CurrentDataChanged(this, EventArgs.Empty);
		}
		

		

		
		
	}
}
