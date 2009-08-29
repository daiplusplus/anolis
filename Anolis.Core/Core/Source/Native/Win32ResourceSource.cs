using System;
using System.IO;

using Anolis.Core.Data;
using Anolis.Core.Native;

using Cult    = System.Globalization.CultureInfo;
using Marshal = System.Runtime.InteropServices.Marshal;
using Anolis.Core.Utility;

namespace Anolis.Core.Source {
	
	public sealed class Win32ResourceSourceFactory : ResourceSourceFactory {
		
		public override ResourceSource Create(String fileName, Boolean isReadOnly, ResourceSourceLoadMode mode) {
			
			return new Win32ResourceSource(fileName, isReadOnly, mode);
		}
		
		protected override void CreateNew(String fileName) {
			
			using(FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write)) {
				
				Byte[] emptyDll = Resources.Win32EmptyPE;
				
				fs.Write( emptyDll, 0, emptyDll.Length );
			}
			
		}
		
		public override String OpenFileFilter {
			get {
				return CreateFileFilter("Win32 Executable", "exe", "dll", "cpl", "ocx", "scr", "msstyles", "mui");
			}
		}
		
		public override String NewFileFilter {
			get {
				return CreateFileFilter("Win32 Resource DLL", "dll");
			}
		}
		
		public override Compatibility HandlesExtension(String extension) {
			switch( extension.ToUpperInvariant() ) {
				case "EXE":
				case "DLL":
				case "CPL":
				case "OCX":
				case "SCR":
				case "MSSTYLES":
				case "MUI":
					return Compatibility.Yes;
				default:
					return Compatibility.Maybe;
			}
		}
		
	}
	
	/// <summary>Encapsulates a resource source that can be handled by Win32's Resource editing API.</summary>
	public sealed class Win32ResourceSource : FileResourceSource {
		
		private IntPtr   _moduleHandle;
		
		public Win32ResourceSource(String fileName, Boolean isReadOnly, ResourceSourceLoadMode mode) : base(fileName, isReadOnly, mode) {
			
			if( mode == ResourceSourceLoadMode.PreemptiveLoad ) throw new NotImplementedException("Support for preemptive data loading is not implemented yet");
			
			if(LoadMode > 0) Reload();
			
		}
		
		public override void CommitChanges() {
			
			if( IsReadOnly ) throw new InvalidOperationException("Changes cannot be commited because the current ResourceSource is read-only");
			
			Boolean finalizeNT6 = false;
			if( System.Environment.OSVersion.Version.Major >= 6 ) {
				
				finalizeNT6 = PrepareNT6Update();
			}
			
			// Unload self
			if(LoadMode > 0) Unload(); // don't unload before PrepareNT6Update() because it needs the valid handle to load the MUI bytes first
			
			CommitChangesImpl();
			
			if( finalizeNT6 ) {
				
				FinishNT6Update();
			}
			
			Miscellaneous.CorrectPEChecksum( FileInfo.FullName );
			
			if(LoadMode > 0) Reload();
		}
		
		private void CommitChangesImpl() {
			
			IntPtr updateHandle = NativeMethods.BeginUpdateResource( FileInfo.FullName, false );
			
			foreach(ResourceLang lang in AllActiveLangs) {
				
				IntPtr pData;
				Int32 length;
				
				if(lang.Action == ResourceDataAction.Delete) {
					
					// pData must be NULL to delete resource
					pData  = IntPtr.Zero;
					length = 0;
					
				} else {
					
					if( !lang.DataIsLoaded ) throw new AnolisException("Cannot perform action when ResourceData is not loaded");
					
					length = lang.Data.RawData.Length;
					pData  = Marshal.AllocHGlobal( length );
					
					Marshal.Copy( lang.Data.RawData, 0, pData, length );
				}
				
				IntPtr typeId = lang.Name.Type.Identifier.NativeId;
				IntPtr nameId = lang.Name.Identifier.NativeId;
				ushort langId = lang.LanguageId;
				
				Boolean uSuccess = NativeMethods.UpdateResource( updateHandle, typeId, nameId, langId, pData, length );
				if( !uSuccess ) throw new AnolisException("UpdatedResource failed: " + NativeMethods.GetLastErrorString() );
				
				Marshal.FreeHGlobal( pData );
				
				lang.Action = ResourceDataAction.None;
			}
			
			Boolean success = NativeMethods.EndUpdateResource(updateHandle, false);
			if( !success ) throw new AnolisException("EndUpdateResource failed: " + NativeMethods.GetLastErrorString() );
		}
		
		public override void Reload() {
			
			Unload();
			
			_moduleHandle = NativeMethods.LoadLibraryEx( FileInfo.FullName, IntPtr.Zero, NativeMethods.LoadLibraryFlags.LoadLibraryAsDatafile );
			
			if( _moduleHandle == IntPtr.Zero ) {
				
				String win32Message = NativeMethods.GetLastErrorString();
				
				throw new AnolisException("PE/COFF ResourceSource could not be loaded: " + win32Message);
			}
			
			UnderlyingClear();
			
			PopulateResources();
			
		}
		
		private void Unload() {
			
			if( _moduleHandle != IntPtr.Zero ) {
				
				if( !NativeMethods.FreeLibrary( _moduleHandle ) ) {
					
					throw new AnolisException("FreeLibrary Failed");
				}
			}
			
			_moduleHandle = IntPtr.Zero;
		}
		
		public override ResourceData GetResourceData(ResourceLang lang) {
			
			if( lang.Name.Type.Source != this ) throw new ArgumentException("The specified ResourceLang does not exist in this ResourceSource");
			
			if( lang.Action == ResourceDataAction.Add ) throw new ArgumentException("The specified ResourceLang's data does not exist in this ResourceSource");
			
			// use FindResourceEx and LoadResource to get a handle to the resource
			// use SizeOfResource to get the length of the byte array
			// then LockResource to get a pointer to it. Use Marshal to get a byte array and take it from there
			
			IntPtr resInfo = NativeMethods.FindResourceEx( _moduleHandle, lang.Name.Type.Identifier.NativeId, lang.Name.Identifier.NativeId, lang.LanguageId );
			IntPtr resData = NativeMethods.LoadResource  ( _moduleHandle, resInfo );
			Int32  size    = NativeMethods.SizeOfResource( _moduleHandle, resInfo );
			
			if(resData == IntPtr.Zero) return null;
			
			IntPtr resPtr  = NativeMethods.LockResource( resData ); // there is no method to unlock resources, but they should be freed anyway
			
			Byte[] data = new Byte[ size ];
			
			Marshal.Copy( resPtr, data, 0, size );
			
			NativeMethods.FreeResource( resData );
			
			ResourceData retval = ResourceData.FromResource(lang, data);
			
			return retval;
			
		}
		
		////////////////////////////
		// Destructor / Dispose
		
		protected override void Dispose(Boolean managed) {
			
			Unload();
			
			base.Dispose(managed);
		}
		
//		~Win32ResourceSource() {
//			Dispose(false);
//		}
//		
//		public override sealed void Dispose() {
//			Dispose(true);
//			GC.SuppressFinalize( this );
//		}
//		
//		public void Dispose(Boolean disposeManaged) {
//			Unload();
//		}
		
#region Vista LC / MUI Workaround
		
		// Windows Vista (NT6) and later introduces some new rules when it comes to the Win32 ResourceUpdate function
		// if a file contains an "RC Config" then it means you cannot perform certain resource update operations since officially you're meant to use MUI files instead
		// so the workaround is to remove this RC Config, save changes, then perform the resource operation, then restore the RC Config
		// the RC config is stored as "MUI"\1\langId (where langId == 1033 on EN Windows)
		
		// Refs:
		// http://msdn.microsoft.com/en-us/library/ms776221%28VS.85%29.aspx
		// http://msdn.microsoft.com/en-us/library/dd318661%28VS.85%29.aspx
		// http://msdn.microsoft.com/en-us/library/dd319100%28VS.85%29.aspx
		
		private UInt16 _rcConfigLangId;
		private Byte[] _rcConfig;
		
		/// <summary>Returns true if it's necessary to call EndNT6Update afterwards</summary>
		private Boolean PrepareNT6Update() {
			
			if( _rcConfig != null ) throw new AnolisException("RC Configuration not saved. Program is in an unexpected state. Did the last CommitChanges not complete successfully?");
			
			///////////////////
			// See if there's a MUI entry
			
			ResourceLang rcConfigLang = FindRCConfigLang( this );
			if( rcConfigLang == null ) return false;
			
			_rcConfigLangId = rcConfigLang.LanguageId;
			_rcConfig       = rcConfigLang.Data.RawData;
			
			//////////////////////////
			// Now, remove it
			
			// you don't need to re-open the source and remove it then save then carry on
			// it works just as well to remove it normally
			
			this.Remove( rcConfigLang );
			
			return true;
		}
		
		private static ResourceLang FindRCConfigLang(ResourceSource source) {
			
			foreach(ResourceType type in source.AllTypes) {
				
				if( type.Identifier.StringId == "MUI" ) {
					
					if( type.Names.Count == 0 ) return null;
					
					if( type.Names.Count == 1 ) {
						ResourceName name = type.Names[0];
						if( name.Langs.Count == 0 ) return null;
						if( name.Langs.Count == 1 ) return name.Langs[0];
						if( name.Langs.Count >  1 ) throw new AnolisException("Too many RC Configuration entries (langs)");
					}
					
					if( type.Names.Count >  1 ) throw new AnolisException("To many RC Configuration entries (names)");
				}
			}
			
			return null;
		}
		
		private void FinishNT6Update() {
			
			// re-add the RC Config
			
			Win32ResourceSource source = new Win32ResourceSource( FileInfo.FullName, false, ResourceSourceLoadMode.LazyLoadData );
			
			// Just make sure it doesn't exist
			ResourceLang rcConfig = FindRCConfigLang(source);
			if( rcConfig != null ) throw new AnolisException("There must not be an RC Config");
			
			MemoryStream ms = new MemoryStream( _rcConfig );
			ResourceData data = ResourceData.FromFileToAdd( ms, ".bin", _rcConfigLangId, this ); 
			
			source.Add( new ResourceTypeIdentifier("MUI"), new ResourceIdentifier(1), _rcConfigLangId, data );
			source.CommitChangesImpl();
		}
		
#endregion
		
#region Resource Enumeration
		
		private Object _enumerating = new Object();
		
		private void PopulateResources() {
			
			lock(_enumerating) {
				
				if( Utility.Environment.IsGteVista ) {
					
					NativeMethods.EnumResTypeProc callback = new NativeMethods.EnumResTypeProc( GetResourceTypesCallbackEx );
					NativeMethods.EnumResourceTypesEx( _moduleHandle, callback, IntPtr.Zero, NativeMethods.MuiResourceFlags.EnumLn, 0 );
					
				} else {
					
					NativeMethods.EnumResTypeProc callback = new NativeMethods.EnumResTypeProc( GetResourceTypesCallback );
					NativeMethods.EnumResourceTypes( _moduleHandle, callback, IntPtr.Zero);
					
				}
				
				
			}
			
		}
		
		private Boolean GetResourceTypesCallback(IntPtr moduleHandle, IntPtr pType, IntPtr userParam) {
			
			ResourceType type = new ResourceType( pType, this );
			
			UnderlyingAdd( _currentType = type );
			
			// enumerate all resources for that type
			NativeMethods.EnumResNameProc callback = new NativeMethods.EnumResNameProc( GetResourceNamesCallback );
			NativeMethods.EnumResourceNames( moduleHandle, pType, callback, IntPtr.Zero );
			
			return true;
			
		}
		
		private ResourceType _currentType;
		
		private Boolean GetResourceNamesCallback(IntPtr moduleHandle, IntPtr pType, IntPtr pName, IntPtr userParam) {
			
			ResourceType type = _currentType;
			
			ResourceName name = new ResourceName( pName, type );
			
			UnderlyingAdd( type, _currentName = name );
			
			NativeMethods.EnumResLangProc callback = new NativeMethods.EnumResLangProc( GetResourceLanguagesCallback );
			NativeMethods.EnumResourceLanguages(moduleHandle, pType, pName, callback, IntPtr.Zero );
			
			return true;
		}
		
		private ResourceName _currentName;
		
		private Boolean GetResourceLanguagesCallback(IntPtr moduleHandle, IntPtr pType, IntPtr pName, UInt16 langId, IntPtr userParam) {
			
			ResourceName name = _currentName;
			
			ResourceLang lang = new ResourceLang( langId, name );
			
			UnderlyingAdd( name, lang );
			
			return true;
		}
		
	#region Resource Enumeration Ex
		
		// I could just branch within the callbacks, but I want to accomodate Vista separately
		
		private Boolean GetResourceTypesCallbackEx(IntPtr moduleHandle, IntPtr pType, IntPtr userParam) {
			
			ResourceType type = new ResourceType( pType, this );
			
			UnderlyingAdd( _currentType = type );
			
			// enumerate all resources for that type
			NativeMethods.EnumResNameProc callback = new NativeMethods.EnumResNameProc( GetResourceNamesCallbackEx );
			NativeMethods.EnumResourceNamesEx( moduleHandle, pType, callback, IntPtr.Zero, NativeMethods.MuiResourceFlags.EnumLn, 0 );
			
			return true;
			
		}
		
		private Boolean GetResourceNamesCallbackEx(IntPtr moduleHandle, IntPtr pType, IntPtr pName, IntPtr userParam) {
			
			ResourceType type = _currentType;
			
			ResourceName name = new ResourceName( pName, type );
			
			UnderlyingAdd( type, _currentName = name );
			
			NativeMethods.EnumResLangProc callback = new NativeMethods.EnumResLangProc( GetResourceLanguagesCallbackEx );
			NativeMethods.EnumResourceLanguagesEx(moduleHandle, pType, pName, callback, IntPtr.Zero, NativeMethods.MuiResourceFlags.EnumLn, 0 );
			
			return true;
		}
		
		private Boolean GetResourceLanguagesCallbackEx(IntPtr moduleHandle, IntPtr pType, IntPtr pName, UInt16 langId, IntPtr userParam) {
			
			ResourceName name = _currentName;
			
			ResourceLang lang = new ResourceLang( langId, name );
			
			UnderlyingAdd( name, lang );
			
			return true;
		}
		
	#endregion
		
#endregion
		
	}
}
