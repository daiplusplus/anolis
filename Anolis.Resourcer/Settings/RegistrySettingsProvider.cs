using System;
using System.Collections;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.Specialized;
using Microsoft.Win32;

namespace Anolis.Resourcer.Settings {
	
	/// <summary>Mutated version of the RegistrySettingsProvider Sample in MSDN2008.</summary>
	public sealed class RegistrySettingsProvider : SettingsProvider { //, IApplicationSettingsProvider {
		
		public RegistrySettingsProvider() {
			
		}
		
		private String _subkeyPath;
		
		public override String ApplicationName {
			get { return Application.ProductName; }
			set { }
		}
		
		public String SubKeyPath {
			get { return _subkeyPath; }
			set { _subkeyPath = value; } // TODO: Perform validation
		}
		
		public override String Name {
			get {
				return base.Name;
			}
		}
		
		public override void Initialize(String name, NameValueCollection col) {
			
			base.Initialize(this.ApplicationName, col);
			
			_subkeyPath = @"Software\" +  Application.CompanyName + '\\' + Application.ProductName + '\\' + Application.ProductVersion;
		}
		
		// SetPropertyValue is invoked when ApplicationSettingsBase.Save is called
		// ASB makes sure to pass each provider only the values marked for that provider -
		// though in this case, since the entire settings class was marked with a SettingsProvider
		// attribute, all settings in that class map to this provider
		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection propvals) {
			
			// Iterate through the settings to be stored
			// Only IsDirty=true properties should be included in propvals
			foreach (SettingsPropertyValue propval in propvals) {
				
				// NOTE: this provider allows setting to both user- and application-scoped
				// settings. The default provider for ApplicationSettingsBase - 
				// LocalFileSettingsProvider - is read-only for application-scoped setting. This 
				// is an example of a policy that a provider may need to enforce for implementation,
				// security or other reasons.
				
//				if(propval.IsDirty) {
				
				GetRegKey( propval.Property ).SetValue( propval.Name, propval.SerializedValue );
				
				
				
//				}
				
			}
		}
		
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection props) {
			
			// Create new collection of values
			SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();
			
			// Iterate through the settings to be retrieved
			foreach (SettingsProperty setting in props) {
				
				SettingsPropertyValue value = new SettingsPropertyValue(setting);
				value.IsDirty = false;
				value.SerializedValue = GetRegKey(setting).GetValue(setting.Name);
				
				values.Add(value);
			}
			
			return values;
		}
		
		/// <summary>Helper method: fetches correct registry subkey. HKLM is used for settings marked as application-scoped. HKLU is used for settings marked as user-scoped.</summary>
		private RegistryKey GetRegKey(SettingsProperty prop) {
			
			RegistryKey regKey;
			
			if (IsUserScoped(prop)) {
				regKey = Registry.CurrentUser;
			} else {
				regKey = Registry.LocalMachine;
			}
			
			regKey = regKey.CreateSubKey( SubKeyPath );
			
			return regKey;
			
		}
		
		private bool IsUserScoped(SettingsProperty prop) {
			
			Boolean hasUserScope = prop.Attributes[ typeof(UserScopedSettingAttribute)        ] is UserScopedSettingAttribute;
			Boolean hasAppScope  = prop.Attributes[ typeof(ApplicationScopedSettingAttribute) ] is ApplicationScopedSettingAttribute;
			
			if( hasUserScope &&  hasAppScope) throw new ConfigurationErrorsException("SettingsProperty has both UserScope and ApplicationScope.");
			if(!hasUserScope && !hasAppScope) throw new ConfigurationErrorsException("SettingsProperty has neither UserScope nor ApplicationScope.");
			
			return hasUserScope;
		}
/*	
#region IApplicationSettingsProvider Members
		
		public SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property) {
			throw new NotImplementedException();
		}
		
		public void Reset(SettingsContext context) {
			throw new NotImplementedException();
		}
		
		public void Upgrade(SettingsContext context, SettingsPropertyCollection properties) {
			throw new NotImplementedException();
		}
		
#endregion
*/
	}
}
