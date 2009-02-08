using System;
using System.Configuration;
using System.ComponentModel;

using Microsoft.Win32;

namespace Anolis.Resourcer.Settings {
	
	// This class allows you to handle specific events on the settings class:
	//  The SettingChanging event is raised before a setting's value is changed.
	//  The PropertyChanged event is raised after a setting's value is changed.
	//  The SettingsLoaded event is raised after the setting values are loaded.
	//  The SettingsSaving event is raised before the setting values are saved.
	
	//[SettingsProvider(typeof(Anolis.Resourcer.Settings.RegistrySettingsProvider))]
	internal sealed partial class Settings {
		
		public Settings() {
			
			
			
		}
		
		protected override void OnSettingsLoaded(object sender, SettingsLoadedEventArgs e) {
			base.OnSettingsLoaded(sender, e);
			
			Ensure();
		}
		
		protected override void OnSettingsSaving(object sender, CancelEventArgs e) {
			base.OnSettingsSaving(sender, e);
			
			Ensure();
		}
		
		/// <summary>Ensures the Settings values are valid, such as making negative numbers positive and removing excess items.</summary>
		public void Ensure() {
			
			
			////////////////////////////////
			// MRU
			if( MruList == null ) return;
			
			/////////////////////////////////
			// MRU Count
			if(this.MruCount < 0) this.MruCount = -this.MruCount;
			
			/////////////////////////////////
			// MRU
			while(this.MruList.Count > this.MruCount) {
				this.MruList.RemoveAt( this.MruCount );
			}
			
		}
		
		public TriState IsAssociatedWithFiles {
			get {
				
				FileAssociationManager mgr = new FileAssociationManager("Anolis Resourcer", System.Reflection.Assembly.GetEntryAssembly().Location );
				return mgr.IsAssociatedWithFiles( new String[] { "exefile", "dllfile", "ocxfile", "cplfile", "scrfile" } );
				
			}
		}
		
		public void AssociateWithFiles(Boolean isAssociated) {
			
			FileAssociationManager mgr = new FileAssociationManager("Anolis Resourcer", System.Reflection.Assembly.GetEntryAssembly().Location );
			mgr.AssociateWithFiles( new String[] { "exefile", "dllfile", "ocxfile", "cplfile", "scrfile" }, isAssociated );
			
		}
		
	}
	
	public enum TriState {
		True,
		Partial,
		False
	}
	
}
