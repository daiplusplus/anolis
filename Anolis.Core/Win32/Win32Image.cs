using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Win32 {
	
	/// <summary>Encapsulates a Win32 Image which contains Win32 resources.</summary>
	public sealed class Win32Image : IResourceSource {
		
		private String  _path;
		private Boolean _readOnly;
		private IntPtr  _moduleHandle;
		
		public Win32Image(String path, Boolean readOnly) {
			if( !File.Exists( _path = path ) ) throw new FileNotFoundException("The specified Win32 Image was not found", path);
			_moduleHandle = (_readOnly = readOnly) ?
				NativeMethods.LoadLibraryEx(path, IntPtr.Zero, NativeMethods.LoadLibraryFlags.LoadLibraryAsDatafile ) :
				NativeMethods.LoadLibrary( path );
			// sven had some tips on using LoadLibrary
		}
		
		private FileInfo _fileInfo;
		
		public FileInfo FileInfo {
			get {
				if(_fileInfo == null) _fileInfo = new FileInfo( _path );
				return _fileInfo;
			}
		}
		
		~Win32Image() {
			Dispose(false);
		}
		
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize( this );
		}
		
		public void Dispose(Boolean disposeManaged) {
			NativeMethods.FreeLibrary( _moduleHandle );
		}
		
#region Resource Enumeration
		
		private List<Win32ResourceType> _types;
		
		public Win32ResourceType[] GetResourceTypes() {
			
			if( _types != null ) return _types.ToArray();
			
			_types = new List<Win32ResourceType>();
			
			NativeMethods.EnumResTypeProc callback = new NativeMethods.EnumResTypeProc( GetResourceTypesCallback );
			NativeMethods.EnumResourceTypes( _moduleHandle, callback, IntPtr.Zero );
			
			return _types.ToArray();
			
		}
		
		private Boolean GetResourceTypesCallback(IntPtr moduleHandle, IntPtr pType, IntPtr userParam) {
			
			Win32ResourceType type = new Win32ResourceType( pType, this );
			
			_types.Add( type );
			
			// enumerate all resources for that type
			NativeMethods.EnumResNameProc callback = new NativeMethods.EnumResNameProc( GetResourceNamesCallback );
			NativeMethods.EnumResourceNames( moduleHandle, pType, callback, IntPtr.Zero );
			
			return true;
			
		}
		
		private Boolean GetResourceNamesCallback(IntPtr moduleHandle, IntPtr pType, IntPtr pName, IntPtr userParam) {
			
			Win32ResourceType tempType = new Win32ResourceType( pType, this ); // temp type used for finding the one to reference
			Win32ResourceType type = _types.Find( new Predicate<Win32ResourceType>( delegate(Win32ResourceType resType) { return resType == tempType; } ) );
			
			if( type == null ) throw new InvalidOperationException("Resource names callback for a type that isn't known.");
			
			//
			
			Win32ResourceName name = new Win32ResourceName( pName, type );
			
			type.Names.Add( name );
			
			NativeMethods.EnumResLangProc callback = new NativeMethods.EnumResLangProc( GetResourceLanguagesCallback );
			NativeMethods.EnumResourceLanguages(moduleHandle, pType, pName, callback, IntPtr.Zero );
			
			return true;
		}
		
		private Boolean GetResourceLanguagesCallback(IntPtr moduleHandle, IntPtr pType, IntPtr pName, UInt16 langId, IntPtr userParam) {
			
			Win32ResourceType tempType = new Win32ResourceType( pType, this ); // temp type used for finding the one to reference
			Win32ResourceType type = _types.Find( new Predicate<Win32ResourceType>( delegate(Win32ResourceType resType) { return resType == tempType; } ) );
			
			if( type == null ) throw new InvalidOperationException("Resource language callback for a type that isn't known.");
			
			Win32ResourceName tempName = new Win32ResourceName( pName, type ); // temp Name used for finding the one to reference
			Win32ResourceName name = type.Names.Find( new Predicate<Win32ResourceName>( delegate(Win32ResourceName resName) { return resName == tempName; } ) );
			
			if( name == null ) throw new InvalidOperationException("Resource names callback for a Name that isn't known.");
			
			//
			
			Win32ResourceLanguage lang = new Win32ResourceLanguage( langId, name );
			
			name.Languages.Add( lang );
			
			return true;
		}
		
#endregion
		
#region Loading Resources
		
		[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, UnmanagedCode=true)]
		public Byte[] GetResourceData(Win32ResourceLanguage resource) {
			
			if( resource.ParentName.ParentType.ParentImage != this ) throw new ArgumentException("Provided resource does not exist in this Image");
			
			// use FindResourceEx and LoadResource to get a handle to the resource
			// use SizeOfResource to get the length of the byte array
			// then LockResource to get a pointer to it. Use Marshal to get a byte array and take it from there
			
			IntPtr resInfo = NativeMethods.FindResourceEx( _moduleHandle, resource.ParentName.ParentType.TypePtr, resource.ParentName.NamePtr, resource.LanguageId );
			IntPtr resData = NativeMethods.LoadResource  ( _moduleHandle, resInfo );
			Int32 size     = NativeMethods.SizeOfResource( _moduleHandle, resInfo );
			
			if(resData == IntPtr.Zero) return null;
			
			IntPtr resPtr  = NativeMethods.LockResource( resData ); // there is no method to unlock resources, but they should be freed anyway
			
			Byte[] data = new Byte[ size ];
			
			Marshal.Copy( resPtr, data, 0, size );
			
			NativeMethods.FreeResource( resData );
			
			return data;
			
		}
		
#endregion
		
#region Resource Update
		
		public void UpdateResources() {
			
			if( _readOnly ) throw new InvalidOperationException("This Win32 image was opened as read-only");
			else throw new NotImplementedException();
		}
/*		public void UpdateResources(Win32ResourceOperation[] resources) {
			
			List<Win32ResourceLanguage> resourcesToAdd = new List<Win32ResourceLanguage>();
			List<Win32ResourceOperation> resourcesToMod = new List<Win32ResourceOperation>();
			List<Win32ResourceLanguage> resourcesToRem = new List<Win32ResourceLanguage>();
			
			for(int i=0;i<resources.Length;i++) {
				
				Win32ResourceOperation op = resources[i];
				if( op.A == null ) resourcesToAdd.Add( op.B );
				else if( op.B == null ) resourcesToRem.Add( op.A );
				else resourcesToMod.Add( op );
				
			}
			
			// According to ms-help://MS.MSDNQTR.v90.en/winui/winui/windowsuserinterface/resources/introductiontoresources/resourcereference/resourcefunctions/beginupdateresource.htm
			// it is recommended that the file be not loaded before calling BeginUpdateResources
			
			// NOTE: if there are any problems caused by the fact the file is loaded you could move LoadLibrary to a lazy-load pattern and throw an exception if it has been loaded
			
			IntPtr updateHandle = NativeMethods.BeginUpdateResources( this.FileInfo.FullName, false );
			
			foreach(Win32ResourceLanguage res in resourcesToAdd) {
				
				Byte[] resData = res.GetData();
				
				IntPtr unmanagedData = Marshal.AllocHGlobal( resData.Length );
				Marshal.Copy( resData, 0, unmanagedData, resData.Length );
				
				NativeMethods.UpdateResource( updateHandle, res.ParentName.ParentType.TypePtr, res.ParentName.NamePtr, res.LanguageId, unmanagedData, resData.Length );
				
			}
		}*/
		
#endregion
		
	}
	
	internal static class Win32ResourceHelper {
		
		private static Dictionary<Int32,String> _resourceTypeFriendlyNames;
		
		static Win32ResourceHelper() {
			_resourceTypeFriendlyNames = new Dictionary<Int32,String>();
			_resourceTypeFriendlyNames.Add(0, "Unknown");
			_resourceTypeFriendlyNames.Add(1, "Cursor (DD)");
			_resourceTypeFriendlyNames.Add(2, "Bitmap");
			_resourceTypeFriendlyNames.Add(3, "Icon (DD)");
			_resourceTypeFriendlyNames.Add(4, "Menu");
			_resourceTypeFriendlyNames.Add(5, "Dialog");
			_resourceTypeFriendlyNames.Add(6, "String Table");
			_resourceTypeFriendlyNames.Add(7, "Font Directory");
			_resourceTypeFriendlyNames.Add(8, "Font");
			_resourceTypeFriendlyNames.Add(9, "Accelerator");
			_resourceTypeFriendlyNames.Add(10, "RC Data");
			_resourceTypeFriendlyNames.Add(11, "Message Table");
			_resourceTypeFriendlyNames.Add(12, "Cursor (DI)");
			// 13 = unknown
			_resourceTypeFriendlyNames.Add(14, "Icon (DI)");
			// 15 = unknown
			_resourceTypeFriendlyNames.Add(16, "Version");
			_resourceTypeFriendlyNames.Add(17, "DLG Include");
			// 18 = unknown
			_resourceTypeFriendlyNames.Add(19, "Plug and Play");
			_resourceTypeFriendlyNames.Add(20, "VXD");
			_resourceTypeFriendlyNames.Add(21, "Cursor (Animated)");
			_resourceTypeFriendlyNames.Add(22, "Icon (Animated)");
			_resourceTypeFriendlyNames.Add(23, "HTML");
			_resourceTypeFriendlyNames.Add(24, "Manifest");
		}

		

		
		public static String GetTypeFriendlyName(Int32 typeInt) {
			
			String fName = _resourceTypeFriendlyNames.ContainsKey( typeInt ) ? _resourceTypeFriendlyNames[ typeInt ] : _resourceTypeFriendlyNames[0];
			return typeInt.ToString(Cult.InvariantCulture) + ": " + fName;
			
		}
				
		public static Boolean IsIntResource(IntPtr res) {
			return (Int64)res > 0xFFFF;
		}
		
	}
	
	public enum KnownWin32ResourceType {
		CursorDeviceDependent   = 1,
		Bitmap                  = 2,
		IconDeviceDependent     = 3, // TODO: are these members named correctly? Are the numbers equal to their use within Win32?
		Menu                    = 4,
		Dialog                  = 5,
		StringTable             = 6,
		FontDirectory           = 7,
		Font                    = 8,
		Accelerator             = 9,
		RCData                  = 10,
		MessageTable            = 11,
		CursorDeviceIndependent = 12,
		IconDeviceIndependent   = 14,
		Version                 = 16,
		DlgInclude              = 17,
		PlugAndPlay             = 19,
		Vxd                     = 20,
		CursorAnimated          = 21,
		IconAnimated            = 22,
		Html                    = 23,
		Manifest                = 24,
		Custom                  = -1, // See string
		Unknown                 =  0  // Unknown non-string ID resource type
	}
	
}
