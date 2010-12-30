using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace Anolis.Packages.Utility {
	
	// Reference:
	//	File Types
	//		http://msdn.microsoft.com/en-us/library/cc144148(VS.85).aspx
	//	ProgIDs
	//		http://msdn.microsoft.com/en-us/library/bb776872(VS.85).aspx
	
	// CLASSES_ROOT contains entries for each extension (beginning with a '.') as a key
	//	each key's default value is the name of its associated class's ProgID
	
	// each extension key has the following values
	// (Default Value) = associated progid
	// PerceivedType   = 
	// ContentType     = MIME content type
	
	// and the following subkeys
	// OpenWithProgIds = alternative progids for this extension. Shown in the "Open With" dialog bog
	// OpenWithList    = application keys, I need to read up on this
	
	
	// create an extension by writing to HKLM\Software\Classes rather than CLASSES_ROOT which is a "virtual" key
	// you can also write to HKCU\Software\Classes for types for the current user only
	
	// you need to call SHChangeNotify is an extension is changed so Explorer and other things can update themselves
	
	// the EditFlags DWORD / FTA_ flags act as a kind of policy restrictions for what the Folder Options dialog can do
	
	// for an application to appear in the Open With window for any type it appears under CLASSES_ROOT\Applications
	
	// don't forget the persistenthandler to allow text searching
	
	public class FileAssociations {
		
		private FileAssociations() {
		}
		
		private List<FileType>      _allTypes;
		private List<FileExtension> _allExts;
		
		public ReadOnlyCollection<FileType>      AllTypes      { get; private set; }
		public ReadOnlyCollection<FileExtension> AllExtensions { get; private set; }
		
		public void CommitChanges() {
			
			foreach(FileType type in AllTypes) {
				type.Save();
			}
			
			foreach(FileExtension ext in AllExtensions) {
				ext.Save();
			}
			
		}
		
		public FileExtension GetExtension(String extension) {
			
			if( !extension.StartsWith(".", StringComparison.OrdinalIgnoreCase) ) return null;
			
//			Int32 idx = Array.BinarySearch( AllExtensions, extension, _extComp );
//			if( idx < 0 ) return null;
//			
//			FileExtension result = AllExtensions[idx];
			
			FileExtension result = _allExts.Find( x => x.Extension.Equals( extension ) );
			
			return result;
		}
		
		private static readonly ExtensionSearchComparer _extComp = new ExtensionSearchComparer();
		
		private class ExtensionSearchComparer : IComparer<Object> {
			
			public Int32 Compare(object x, object y) {
				
				FileExtension fx = x as FileExtension;
				String        sy = y as String;
				
				return String.Compare( fx.Extension, sy, StringComparison.OrdinalIgnoreCase );
			}
		}
		
		public static FileAssociations GetAssociations() {
			
			FileAssociations assocs = new FileAssociations();
			
			///////////////////////////////////////
			
			String[] subkeyNames = Registry.ClassesRoot.GetSubKeyNames();
			
			List<FileExtension> allExtensions = new List<FileExtension>();
			Dictionary<String, List<FileExtension>> progIds = new Dictionary<String,List<FileExtension>>();
			
			foreach(String keyName in subkeyNames) {
				if( !keyName.StartsWith(".", StringComparison.Ordinal) ) continue;
				
				RegistryKey key = Registry.ClassesRoot.OpenSubKey( keyName );
				
				FileExtension ext = new FileExtension(assocs, key);
				
				allExtensions.Add( ext );
				
				if(ext.ProgId != null) {
					
					if( !progIds.ContainsKey( ext.ProgId ) )
						progIds.Add( ext.ProgId, new List<FileExtension>() );
				
					progIds[ ext.ProgId ].Add( ext );
					
				}
				
			}
			
			///////////////////////////////////////
			
			List<FileType> allTypes = new List<FileType>();
			
			foreach(String keyName in progIds.Keys) {
				
				RegistryKey key = Registry.ClassesRoot.OpenSubKey( keyName );
				if(key == null) continue; // then the extension uses its progid as its display name and that's it
				
				FileType type = new FileType( assocs, key );
				
				type.Extensions.AddRange2( progIds[keyName] );
				foreach(FileExtension ext in type.Extensions) ext.FileType = type;
				
				allTypes.Add( type );
			}
			
			assocs._allExts  = allExtensions;
			assocs._allTypes = allTypes;
			
			assocs.AllExtensions = new ReadOnlyCollection<FileExtension>( assocs._allExts );
			assocs.AllTypes      = new ReadOnlyCollection<FileType>     ( assocs._allTypes );
			
//			Array.Sort( assocs.AllExtensions );
//			Array.Sort( assocs.AllTypes );
			
			return assocs;
		}
		
#region File Types
		
		public String GetUnusedProgIdForExtension(String extension) {
			
			if( extension.StartsWith(".", StringComparison.OrdinalIgnoreCase) ) extension = extension.Substring(1);
			
			String origProgId = extension + "file";
			String progId = origProgId;
			
			Int32 i=1;
			FileType existingType = GetFileType( progId );
			while( existingType != null ) {
				
				progId = origProgId + (i++).ToStringInvariant();
				
				existingType = GetFileType( progId );
			}
			
			return progId;
		}
		
		public FileType GetFileType(String progId) {
			
			foreach(FileType type in AllTypes) {
				if( String.Equals( progId, type.ProgId, StringComparison.OrdinalIgnoreCase ) ) return type;
			}
			
			return null;
		}
		
		public FileType CreateFileType(String progId) {
			
			FileType type = new FileType( this, progId );
			
			_allTypes.Add( type );
			
			return type;
		}
#endregion
		
#region File Extensions
		
		public FileExtension GetFileExtension(String extension) {
			
			foreach(FileExtension ext in AllExtensions) {
				if( String.Equals( extension, ext.Extension, StringComparison.OrdinalIgnoreCase ) ) return ext;
			}
			
			return null;
		}
		
		public FileExtension CreateFileExtension(String extension) {
			
			FileExtension ext = new FileExtension(this, extension);
			
			_allExts.Add( ext );
			
			return ext;
		}
		
#endregion
		
	}
	
	public abstract class FileBase {
		
		protected FileBase(FileAssociations assocs) {
			
			ParentAssocs = assocs;
		}
		
		public FileAssociations ParentAssocs { get; private set; }
		
		public Boolean IsDirty { get; set; }
		
		public Boolean DeleteOnCommit { get; set; }
		
		protected internal abstract void Save();
		
	}
	
	public class FileType : FileBase, IComparable<FileType> {
		
		private FileType(FileAssociations assocs) : base(assocs) {
			
			Extensions   = new Collection<FileExtension>();
			ShellVerbs   = new Dictionary<String,String>();
		}
		
		internal FileType(FileAssociations assocs, RegistryKey typeKey) : this(assocs) {
			
			LoadFromKey( typeKey );
		}
		
		internal FileType(FileAssociations assocs, String progId) : this(assocs) {
			
			ProgId = progId;
			
			IsDirty = true;
		}
		
		public String                    ProgId       { get; private set; }
		public Collection<FileExtension> Extensions   { get; private set; }
		
		public String                    FriendlyName { get; set; }
		public String                    DefaultIcon  { get; set; }
		public FileTypeEditFlags         EditFlags    { get; set; }
		public Dictionary<String,String> ShellVerbs   { get; private set; }
		
		private void LoadFromKey(RegistryKey key) {
			
			ProgId = key.Name.Substring( key.Name.IndexOf('\\') + 1 );
			
			FriendlyName = (String)key.GetValue(null);
			
			////////////////////////////////
			// Default Icon
			RegistryKey defaultIconKey = key.OpenSubKey("DefaultIcon");
			
			if( defaultIconKey != null ) {
				
				DefaultIcon = (String)defaultIconKey.GetValue(null);
			}
			
			////////////////////////////////
			// Edit Flags
			Object editFlagsObj = key.GetValue("EditFlags");
			if( editFlagsObj != null ) {
				
				Byte[] flagsArr = editFlagsObj as Byte[];
				if( flagsArr != null && flagsArr.Length == 4 ) {
					
					Int32 flagsDword = ((flagsArr[0] | (flagsArr[1] << 8)) | (flagsArr[2] << 0x10)) | (flagsArr[3] << 0x18);
					
					EditFlags = (FileTypeEditFlags)flagsDword;
				}
				
			}
			
			////////////////////////////////
			// Shell Verbs
			
			RegistryKey verbsKey = key.OpenSubKey("shell");
			if( verbsKey != null ) {
				
				String[] verbs = verbsKey.GetSubKeyNames();
				foreach(String verb in verbs) {
					
					RegistryKey verbKey = verbsKey.OpenSubKey( verb );
					RegistryKey cmdKey  = verbKey.OpenSubKey("command");
					
					if( cmdKey != null ) {
						
						ShellVerbs.Add( verb, (String)cmdKey.GetValue(null) );
					}
					
				}
				
			}
			
			IsDirty = false;
		}
		
		protected internal override void Save() {
			
			if( !IsDirty ) return;
			
			if( DeleteOnCommit ) {
				
				Registry.ClassesRoot.DeleteSubKeyTree( ProgId );
				return;
				
			} else {
				
				////////////////////////////////
				// Key and Friendly Name
				
				RegistryKey progIdKey = Registry.ClassesRoot.CreateSubKey( ProgId, RegistryKeyPermissionCheck.ReadWriteSubTree );
				progIdKey.SetValue(null, FriendlyName);
				
				////////////////////////////////
				// DefaultIcon
				
				if( String.IsNullOrEmpty( DefaultIcon ) ) {
					
					progIdKey.DeleteSubKey("DefaultIcon", false);
					
				} else {
					
					RegistryKey diKey = progIdKey.CreateSubKey("DefaultIcon" );
					diKey.SetValue(null, DefaultIcon);
				}
				
				////////////////////////////////
				// EditFlags
				
				if( EditFlags == FileTypeEditFlags.None ) {
					
					progIdKey.DeleteValue("EditFlags", false);
				} else {
					
					progIdKey.SetValue("EditFlags", EditFlags, RegistryValueKind.DWord);
				}
				
				////////////////////////////////
				// ShellVerbs
				
				RegistryKey shellKey = progIdKey.CreateSubKey("shell");
				
				foreach(String verb in ShellVerbs.Keys) {
					
					String cmd = ShellVerbs[verb];
					if( cmd == null ) { // delete the verb/command
						
						shellKey.DeleteSubKey( verb, false );
						
					} else {
						
						RegistryKey verbKey = progIdKey.CreateSubKey( verb );
						RegistryKey cmdKey  = verbKey.CreateSubKey("command" );
						cmdKey.SetValue(null, cmd );
					}
					
				}
				
				////////////////////////////////
				// ...and that's it for now
				
				progIdKey.Close();
			}
			
		}
		
		public Int32 CompareTo(FileType other) {
			
			return String.Compare( ProgId, other.ProgId, StringComparison.OrdinalIgnoreCase );
		}
		
	}
	
	public class FileExtension : FileBase, IComparable<FileExtension> {
		
		private FileType _type;
		
		private FileExtension(FileAssociations assocs) : base(assocs) {
			
			OpenWithList    = new Collection<String>();
			OpenWithProgIds = new Collection<FileType>();
		}
		
		internal FileExtension(FileAssociations assocs, RegistryKey key) : this(assocs) {
			
			LoadFromKey(key);
		}
		
		internal FileExtension(FileAssociations assocs, String extension) : this(assocs) {
			
			Extension = extension;
			
			IsDirty = true;
		}
		
		public FileType FileType  {
			get { return _type; }
			set {
				if( value != null ) ProgId = value.ProgId;
				_type = value;
			}
		}
		public String   ProgId    { get; set; }
		
		/// <summary>List of all entries under HKCR\Applications that can open this extension. OpenWithProgIds is preferable. ( http://msdn.microsoft.com/en-us/library/bb166549(VS.80).aspx )</summary>
		public Collection<String>   OpenWithList    { get; private set; }
		/// <summary>List of all ProgIds that handle this extension.</summary>
		public Collection<FileType> OpenWithProgIds { get; private set; }
		
		public String   Extension         { get; private set; }
		public String   ContentType       { get; set; }
		public String   PerceivedType     { get; set; }
		public String   PersistentHandler { get; set; }
		
		private void LoadFromKey(RegistryKey key) {
			
			Extension     = key.Name.Substring( key.Name.IndexOf('\\') + 1 );
			ProgId        = (String)key.GetValue(null);
			
			ContentType   = (String)key.GetValue("Content Type");
			PerceivedType = (String)key.GetValue("PerceivedType");
			
			RegistryKey persistentHandler = key.OpenSubKey("PersistentHandler");
			if(persistentHandler != null) {
				
				PersistentHandler = (String)persistentHandler.GetValue(null);
			}
			
		}
		
		protected internal override void Save() {
			
			if( !IsDirty ) return;
			
			RegistryKey extKey = Registry.ClassesRoot.CreateSubKey( Extension, RegistryKeyPermissionCheck.ReadWriteSubTree );
			extKey.SetValue(null, ProgId);
			
			/////////////////////////////////
			// ContentType
			
			if( ContentType == null ) {
				extKey.DeleteValue("ContentType", false);
			} else {
				extKey.SetValue("ContentType", ContentType);
			}
			
			/////////////////////////////////
			// PerceivedType
			
			if( PerceivedType == null ) {
				extKey.DeleteValue("PerceivedType", false);
			} else {
				extKey.SetValue("PerceivedType", PerceivedType);
			}
			
			/////////////////////////////////
			// PersistentHandler
			
			if( PersistentHandler == null ) {
				extKey.DeleteSubKey("PersistentHandler", false);
			} else {
				RegistryKey persKey = extKey.CreateSubKey("PersistentHandler");
				persKey.SetValue(null, PersistentHandler);
			}
			
			/////////////////////////////////
			// That's all for now
			// in future, consider ShellNew and OpenWithProgIds
			
			extKey.Close();
			
		}
		
		public Int32 CompareTo(FileExtension other) {
			
			return String.Compare( Extension, other.Extension, StringComparison.OrdinalIgnoreCase );
		}
		
	}
	
	public static class FilePersistentHandlers {
		
		// Windows Desktop Search lists PersistentHandlers for each extension now
		// so that takes away the guesswork
		
		/// <summary>Indexes the whole file for textual content</summary>
		public const String Text = "{5e941d80-bf96-11cd-b579-08002b30bfeb}";
		
		/// <summary>Only indexes the VS_VERSIONINFO resource (if present)</summary>
		public const String Null = "{098f2470-bae0-11cd-b579-08002b30bfeb}";
		
		/// <summary>Indexes the file for HTML content</summary>
		public const String Html = "{eec97550-47a9-11cf-b952-00aa0051fe20}";
		
		/// <summary>Indexes multipart MIME documents (e.g. .eml and .mht / .html files where the HTML filter wouldn't work because it's multipart)</summary>
		/// <remarks>This is a custom filter since WDS doesn't recognise it as a built-in one. Googling suggests this GUID is regularly used for this filter.</remarks>
		public const String Mime = "{5645C8C1-E277-11CF-8FDA-00AA00A14F93}";
		
		/// <summary>Indexes the file for XML content</summary>
		public const String Xml  = "{7E9D8D44-6926-426F-AA2B-217A819A5CCE}";
		
	}
	
	public static class FileVerbs {
		
		public const String Open          = "open";
		public const String OpenNewWindow = "opennew";
		public const String OpenAsPrompt  = "openas";
		public const String Edit          = "edit";
		public const String Play          = "play";
		public const String Print         = "print";
		public const String Preview       = "preview";
		public const String PrintTo       = "printto";
		public const String RunAs         = "runas";
		
	}
	
	[Flags]
	public enum FileTypeEditFlags : uint {
		None          = 0x00000000,
		Exclude       = 0x00000001,
		Show          = 0x00000002,
		HasExtension  = 0x00000004,
		NoEdit        = 0x00000008,
		NoRemove      = 0x00000010,
		NoNewVerb     = 0x00000020,
		NoEditVerb    = 0x00000040,
		NoRemoveVerb  = 0x00000080,
		NoEditDesc    = 0x00000100,
		NoEditIcon    = 0x00000200,
		NoEditDflt    = 0x00000400,
		NoEditVerbCmd = 0x00000800,
		NoEditVerbExe = 0x00001000,
		NoDde         = 0x00002000,
		NoEditMime    = 0x00008000,
		OpenIsSafe    = 0x00010000,
		AlwaysUnsafe  = 0x00020000,
		AlwaysShowExt = 0x00040000,
		NoRecentDocs  = 0x00100000
	}
	
}
