using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Microsoft.Win32;
using System.IO;

namespace Anolis.Core.Utility {
	
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
		
		private FileAssociations(FileType[] allTypes, FileExtension[] allExts) {
			AllTypes      = allTypes;
			AllExtensions = allExts;
		}
		
		public FileType[]      AllTypes      { get; private set; }
		public FileExtension[] AllExtensions { get; private set; }
		
		public void CommitChanges() {
			
			
			
		}
		
		public static FileAssociations GetAssoctiations() {
			
			String[] subkeyNames = Registry.ClassesRoot.GetSubKeyNames();
			
			List<FileExtension> allExtensions = new List<FileExtension>();
			Dictionary<String, List<FileExtension>> progIds = new Dictionary<String,List<FileExtension>>();
			
			foreach(String keyName in subkeyNames) {
				if( !keyName.StartsWith(".") ) continue;
				
				RegistryKey key = Registry.ClassesRoot.OpenSubKey( keyName );
				
				FileExtension ext = FileExtension.FromRegKey( keyName, key );
				
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
				
				FileType type = FileType.FromRegKey(keyName, key );
				
				type.Extensions.AddRange2( progIds[keyName] );
				foreach(FileExtension ext in type.Extensions) ext.FileType = type;
				
				allTypes.Add( type );
			}
			
			return new FileAssociations( allTypes.ToArray(), allExtensions.ToArray() );
		}
		
	}
	
	public class FileBase {
		
		public Boolean IsDirty { get; set; }
		
		public Boolean DeleteOnCommit { get; set; }
		
	}
	
	public class FileType : FileBase {
		
		public FileType() {
			Extensions = new Collection<FileExtension>();
			ShellVerbs = new Collection<FileVerb>();
		}
		
		public static FileType FromRegKey(String name, RegistryKey key) {
			
			FileType ret = new FileType();
			
			ret.ProgId       = name;
			ret.FriendlyName = (String)key.GetValue(null);
			
			return ret;
		}
		
		public String                    ProgId       { get; set; }
		public Collection<FileExtension> Extensions   { get; private set; }
		
		public String                    FriendlyName { get; set; }
		public String                    DefaultIcon  { get; set; }
		public FileTypeEditFlags         EditFlags    { get; set; }
		public Collection<FileVerb>      ShellVerbs { get; private set; }
		
		
	}
	
	public class FileExtension : FileBase {
		
		public FileExtension() {
			OpenWithList    = new Collection<String>();
			OpenWithProgIds = new Collection<FileType>();
		}
		
		public static FileExtension FromRegKey(String name, RegistryKey key) {
			
			FileExtension ret = new FileExtension();
			
			ret.Extension     = name;
			ret.ProgId        = (String)key.GetValue(null);
			
			ret.ContentType   = (String)key.GetValue("Content Type");
			ret.PerceivedType = (String)key.GetValue("PerceivedType");
			
			RegistryKey persistentHandler = key.OpenSubKey("PersistentHandler");
			if(persistentHandler != null) {
				ret.PersistentHandler = (String)persistentHandler.GetValue(null);
			}
			
			
			
			return ret;
		}
		
		public FileType FileType  { get; set; }
		public String   ProgId    { get; set; }
		
		/// <summary>List of all entries under HKCR\Applications that can open this extension. OpenWithProgIds is preferable. ( http://msdn.microsoft.com/en-us/library/bb166549(VS.80).aspx )</summary>
		public Collection<String>   OpenWithList    { get; private set; }
		/// <summary>List of all ProgIds that handle this extension.</summary>
		public Collection<FileType> OpenWithProgIds { get; private set; }
		
		public String   Extension         { get; set; }
		public String   ContentType       { get; set; }
		public String   PerceivedType     { get; set; }
		public String   PersistentHandler { get; set; }
	}
	
	public static class FilePersistentHandlers {
		
		/// <summary>Indexes the whole file for textual content</summary>
		public const String Text = "{5e941d80-bf96-11cd-b579-08002b30bfeb}";
		
		/// <summary>Only indexes the VS_VERSIONINFO resource if present</summary>
		public const String Null = "{098f2470-bae0-11cd-b579-08002b30bfeb}";
		
	}
	
	public class FileVerb {
		
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
	
	public enum FileTypeEditFlags : uint {
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
		NoDDE         = 0x00002000,
		NoEditMIME    = 0x00008000,
		OpenIsSafe    = 0x00010000,
		AlwaysUnsafe  = 0x00020000,
		AlwaysShowExt = 0x00040000,
		NoRecentDocs  = 0x00100000
	}
	
}
