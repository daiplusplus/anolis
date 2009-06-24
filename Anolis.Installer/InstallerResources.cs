using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

using Anolis.Core;

namespace Anolis.Installer {
	
	public class InstallerResourceLanguage {
		
		private String _manifestStreamName;
		
		public InstallerResourceLanguage(String manifestStreamName) {
			
			_manifestStreamName = manifestStreamName;
		}
		
		private ResourceSet _set;
		
		public ResourceSet ResourceSet {
			get {
				
				if( _set == null ) {
					
					using(Stream manifestStream = Assembly.GetExecutingAssembly().GetManifestResourceStream( _manifestStreamName )) {
						
						ResourceReader rdr = new ResourceReader( manifestStream );
						
						_set = new ResourceSet( rdr );
						
					}
				}
				
				return _set;
			}
		}
		
#region Nice Properties
		
		public String LanguageName {
			get { return ResourceSet.GetString("Lang_Name"); }
		}
		
		public String LcidName {
			get { return ResourceSet.GetString("Lang_Lcid"); }
		}
		
		private CultureInfo _culture;
		
		public CultureInfo Culture {
			get {
				if( _culture == null ) _culture = new CultureInfo( LcidName );
				return _culture;
			}
		}
		
		private Image _flag;
		
		public Image Flag {
			get {
				if( _flag == null ) _flag = ResourceSet.GetObject("Lang_Flag") as Image;
				return _flag;
			}
		}
		
		public String Attribution {
			get { return ResourceSet.GetString("Lang_Attribution"); }
		}
		
		public String AttributionUri {
			get { return ResourceSet.GetString("Lang_AttributionUri"); }
		}
		
#endregion
		
		public override String ToString() {
			
			return LanguageName;
		}
		
	}
	
	internal static class InstallerResources {
		
		static InstallerResources() {
			
			InstallerResourceLanguage[] langs = GetAvailableLanguages();
			
			CultureInfo current = CultureInfo.CurrentCulture.Parent;
			
			foreach(InstallerResourceLanguage lang in langs) {
				if( lang.LcidName == "en" ) {
					_fallback = lang;
				}
				if( current.Name == lang.LcidName ) {
					_currentLanguage = lang;
				}
				if( _fallback != null && _currentLanguage != null ) break;
			}
			
			if( _fallback == null ) throw new AnolisException("Couldn't load fallback language.");
			
			if( _currentLanguage == null ) _currentLanguage = _fallback;
			
		}
		
#region Language Management
		
		private static InstallerResourceLanguage _fallback;
		
		private static InstallerResourceLanguage[] _availableLanguages;
		
		/// <summary>Gets an array of all the Installer resource languages in this installer.</summary>
		public static InstallerResourceLanguage[] GetAvailableLanguages() {
			
			if( _availableLanguages == null ) {
				
				Assembly thisAssembly = Assembly.GetExecutingAssembly();
				String[] names = thisAssembly.GetManifestResourceNames();
				
				List<InstallerResourceLanguage> langs = new List<InstallerResourceLanguage>();
				
				foreach(String name in names) {
					
					if( name.StartsWith("Anolis.Installer.Resources") ) {
						
						InstallerResourceLanguage lang = new InstallerResourceLanguage( name );
						langs.Add( lang );
					}
					
				}
				
				_availableLanguages = langs.ToArray();
				
			}
			
			return _availableLanguages;
			
		}
		
		private static InstallerResourceLanguage _currentLanguage;
		
		/// <summary>Gets or Sets the current language for the installer</summary>
		public static InstallerResourceLanguage CurrentLanguage {
			get { return _currentLanguage; }
			set {
				
				if( value != _currentLanguage ) {
					
					_currentLanguage = value;
					
					OnCurrentLanguageChanged();
				}
				
			}
		}
		
		private static void OnCurrentLanguageChanged() {
			
			if( CurrentLanguageChanged != null ) CurrentLanguageChanged(null, EventArgs.Empty);
		}
		
		public static event EventHandler CurrentLanguageChanged;
		
#endregion
		
		/////////////////////////////////////////////////////////////
		
		public static String GetString(String name) {
			
			String ret = CurrentLanguage.ResourceSet.GetString( name );
			
			if( ret == null ) // ResourceSet uses Hashtable, which returns null if the key isn't found
				ret = _fallback.ResourceSet.GetString( name );
			
			return ret;
		}
		
		public static Object GetObject(String name) {
			
			Object ret = CurrentLanguage.ResourceSet.GetObject( name );
			
			if( ret == null )
				ret = _fallback.ResourceSet.GetObject( name );
			
			return ret;
		}
		
		public static Image GetImage(String name) {
			
			return GetObject( name ) as Image;
		}
		
		public static Icon GetIcon(String name) {
			
			return GetObject( name ) as Icon;
		}
		
		public static CultureInfo Culture {
			get { return CurrentLanguage.Culture; }
		}
		
	}
}
