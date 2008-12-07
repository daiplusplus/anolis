using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Win32 {

/*	
	/// <summary>Encapsulates a Win32 Image which contains Win32 resources.</summary>
	public sealed class Win32Image : IResourceSource {
		
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
		} * /
		
	} */
	
	
}
