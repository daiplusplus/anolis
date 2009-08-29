using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Resources;

using Anolis.Core;

namespace Anolis.Installer {
	
	public class InstallerResourceLanguage {
		
		private String _manifestStreamName;
		private Boolean _isGzipped;
		
		public InstallerResourceLanguage(String manifestStreamName) {
			
			_manifestStreamName = manifestStreamName;
			_isGzipped          = manifestStreamName.EndsWith(".gz");
		}
		
		private ResourceSet _set;
		
		public ResourceSet ResourceSet {
			get {
				
				if( _set == null ) {
					
					using(Stream manifestStream = GetResourceStream() ) {
						
						ResourceReader rdr = new ResourceReader( manifestStream );
						
						_set = new ResourceSet( rdr );
					}
				}
				
				return _set;
			}
		}
		
		private Stream GetResourceStream() {
			
			Stream rawManifestStream = Assembly.GetExecutingAssembly().GetManifestResourceStream( _manifestStreamName );
			
			if( _isGzipped ) {
				
				MemoryStream ms = new MemoryStream();
				
				using(GZipStream gz = new GZipStream( rawManifestStream, CompressionMode.Decompress, false )) {
					
					Byte[] buffer = new Byte[ 10240 ]; // 10KB
					Int32 read = 0;
					while( ( read = gz.Read( buffer, 0, 10240 ) ) > 0 ) {
						
						ms.Write( buffer, 0, read );
					}
					
				}
				ms.Seek(0, SeekOrigin.Begin);
				
				return ms;
			}
			
			return rawManifestStream;
			// GZipStream.Dispose closes the original stream
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
		
		private Boolean? _rightToLeft;
		
		public Boolean RightToLeft {
			get {
				if( _rightToLeft == null ) {
					String rtlStr = ResourceSet.GetString("Lang_RTL");
					if( rtlStr == "1" ) _rightToLeft = true;
					else                _rightToLeft = false;
				}
				return _rightToLeft.Value;
			}
		}
		
#endregion
		
		public override String ToString() {
			
			return LanguageName;
		}
		
	}
	
	public class InstallerCustomizer {
		
		internal InstallerCustomizer(String streamName) {
			
			using(Stream manifestStream = Assembly.GetExecutingAssembly().GetManifestResourceStream( streamName )) {
				
				ResourceReader rdr = new ResourceReader( manifestStream );
				
				_customizerSet = new ResourceSet( rdr );
			}
		}
		
		private ResourceSet _customizerSet;
		
		public ResourceSet ResourceSet {
			get { return _customizerSet; }
		}
		
		/// <summary>e.g. "xpize"</summary>
		public String InstallerName {
			get { return GetString("Installer_Name"); }
		}
		
		/// <summary>e.g. "xpize 5 Release 4"</summary>
		public String InstallerFullName {
			get { return GetString("Installer_NameFull"); }
		}
		
		public String InstallerDeveloper {
			get { return GetString("Installer_Developer"); }
		}
		
		public String InstallerWebsite {
			get { return GetString("Installer_Website"); }
		}
		
		public String InstallerCondition {
			get { return GetString("Installer_Condition"); }
		}
		
		public String InstallerConditionMessage {
			get { return GetString("Installer_ConditionMessage"); }
		}
		
		public Boolean SimpleUI {
			get {
				if( _customizerSet == null ) return false;
				return (Boolean)_customizerSet.GetObject("Option_SimpleUI");
			}
		}
		
		public Boolean HideI386 {
			get {
				if( _customizerSet == null ) return false;
				return (Boolean)_customizerSet.GetObject("Option_HideI386");
			}
		}
		
		public Boolean DisablePackageCheck {
			get {
				if( _customizerSet == null ) return false;
				return (Boolean)_customizerSet.GetObject("Option_DisablePackageCheck");
			}
		}
		
		public Boolean DisableUpdateCheck {
			get {
				if( _customizerSet == null ) return false;
				return (Boolean)_customizerSet.GetObject("Option_DisableUpdateCheck");
			}
		}
		
		private String GetString(String key) {
			if( _customizerSet == null ) return null;
			return _customizerSet.GetString(key);
		}
		
	}
	
	internal static class InstallerResources {
		
		private static String              _customizerName;
		private static InstallerCustomizer _customizer;
		
#region Language Management
		
		private static InstallerResourceLanguage _english;
		
		private static InstallerResourceLanguage[] _availableLanguages;
		
		static InstallerResources() {
			
			InstallerResourceLanguage[] langs = GetAvailableLanguages();
			
			CultureInfo current = CultureInfo.CurrentCulture.Parent;
			
			foreach(InstallerResourceLanguage lang in langs) {
				if( lang.LcidName == "en" ) {
					_english = lang;
				}
				if( current.Name == lang.LcidName ) {
					_currentLanguage = lang;
				}
				if( _english != null && _currentLanguage != null ) break;
			}
			
			if( _english == null ) throw new AnolisException("Couldn't load fallback language.");
			
			if( _currentLanguage == null ) _currentLanguage = _english;
			
			/////////////////////////////////////////////
			
			if( _customizerName != null ) {
				// _customizerName is set by GetAvailableLanguages as a side-effect
				_customizer = new InstallerCustomizer( _customizerName );
			}
			
		}
		
		/// <summary>Gets an array of all the Installer resource languages in this installer.</summary>
		public static InstallerResourceLanguage[] GetAvailableLanguages() {
			
			if( _availableLanguages == null ) {
				
				Assembly thisAssembly = Assembly.GetExecutingAssembly();
				String[] names = thisAssembly.GetManifestResourceNames();
				
				List<InstallerResourceLanguage> langs = new List<InstallerResourceLanguage>();
				
				foreach(String name in names) {
					
					if( name.StartsWith("Anolis.Installer.Resources", StringComparison.Ordinal) ) {
						
						InstallerResourceLanguage lang = new InstallerResourceLanguage( name );
						langs.Add( lang );
						
					} else if ( name.StartsWith("Anolis.Installer.Customizer", StringComparison.Ordinal) ) {
						
						_customizerName = name;
					}
					
				}
				
				_availableLanguages = langs.ToArray();
				
				//Array.Sort( _availableLanguages, (x,y) => x.LcidName.CompareTo( y.LcidName ) );
				Array.Sort( _availableLanguages, (x,y) => x.LanguageName.CompareTo( y.LanguageName ) );
				
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
		
		public static void ForceLocalize() {
			CurrentLanguageChanged(null, EventArgs.Empty);
		}
		
		public static Boolean IsCustomized {
			get {
				return _customizer != null;
			}
		}
		
		public static InstallerCustomizer CustomizedSettings {
			get { return _customizer; }
		}
		
#endregion
		
		/////////////////////////////////////////////////////////////
		
		public static String GetString(String name, params Object[] formatArgs) {
			
			String s = GetString(name);
			if( String.IsNullOrEmpty( s ) ) return s;
			
			return String.Format( CultureInfo.CurrentCulture, s, formatArgs );
		}
		
		private static List<String> nulls = new List<String>();
		
		public static String GetString(String name) {
			
			String ret;
			
			if( _customizer != null ) {
				ret = _customizer.ResourceSet.GetString( name );
				if( ret != null ) return ret;
			}
			
			ret = CurrentLanguage.ResourceSet.GetString( name );
			if( ret != null ) return ret;
			
			ret = _english.ResourceSet.GetString( name );
			if( ret == null || ret.Length == 0 )
				nulls.Add( name );
			
			return ret;
		}
		
		public static Object GetObject(String name) {
			
			Object ret;
			
			if( _customizer != null ) {
				ret = _customizer.ResourceSet.GetObject( name );
				if( ret != null ) return ret;
			}
			
			ret = CurrentLanguage.ResourceSet.GetObject( name );
			if( ret != null ) return ret;
			
			return _english.ResourceSet.GetObject( name );
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
