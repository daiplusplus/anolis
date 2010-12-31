/* vbAccelerator Software License

Version 1.0

Copyright (c) 2002 vbAccelerator.com

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

   1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer
   2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
   3. The end-user documentation included with the redistribution, if any, must include the following acknowledgment:

      "This product includes software developed by vbAccelerator (http://vbaccelerator.com/)."

      Alternately, this acknowledgment may appear in the software itself, if and wherever such third-party acknowledgments normally appear.
   4. The names "vbAccelerator" and "vbAccelerator.com" must not be used to endorse or promote products derived from this software without prior written permission. For written permission, please contact vbAccelerator through steve@vbaccelerator.com.
   5. Products derived from this software may not be called "vbAccelerator", nor may "vbAccelerator" appear in their name, without prior written permission of vbAccelerator.

THIS SOFTWARE IS PROVIDED "AS IS" AND ANY EXPRESSED OR IMPLIED WARRANTIES, 
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY 
AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL 
VBACCELERATOR OR ITS CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, 
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY 
OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Anolis.Packages.Utility {

	public class ShellLink : IDisposable {
		
#region COM Interop
		
		[ComImportAttribute()]
		[GuidAttribute("0000010C-0000-0000-C000-000000000046")]
		[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		private interface IPersist {
			[PreserveSig]
			//[helpstring("Returns the class identifier for the component object")]
			void GetClassID(out Guid pClassID);
		}
		
		[ComImportAttribute()]
		[GuidAttribute("0000010B-0000-0000-C000-000000000046")]
		[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		private interface IPersistFile {
			// can't get this to go if I extend IPersist, so put it here:
			[PreserveSig]
			void GetClassID(out Guid pClassID);
			
			void IsDirty();
			
			void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, uint dwMode);
			
			void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);
				
			void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);
		
			void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
		}
		
		[ComImportAttribute()]
		[GuidAttribute("000214EE-0000-0000-C000-000000000046")]
		[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		private interface IShellLinkA {
			//[helpstring("Retrieves the path and filename of a shell link object")]
			void GetPath([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszFile, int cchMaxPath, ref _WIN32_FIND_DATAA pfd, uint fFlags);
			
			//[helpstring("Retrieves the list of shell link item identifiers")]
			void GetIDList(out IntPtr ppidl);
			
			//[helpstring("Sets the list of shell link item identifiers")]
			void SetIDList(IntPtr pidl);
			
			//[helpstring("Retrieves the shell link description string")]
			void GetDescription([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszFile, int cchMaxName);
			
			//[helpstring("Sets the shell link description string")]
			void SetDescription([MarshalAs(UnmanagedType.LPStr)] string pszName);
			
			//[helpstring("Retrieves the name of the shell link working directory")]
			void GetWorkingDirectory([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszDir, int cchMaxPath);
			
			//[helpstring("Sets the name of the shell link working directory")]
			void SetWorkingDirectory([MarshalAs(UnmanagedType.LPStr)] string pszDir);
			
			//[helpstring("Retrieves the shell link command-line arguments")]
			void GetArguments([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszArgs, int cchMaxPath);
			
			//[helpstring("Sets the shell link command-line arguments")]
			void SetArguments([MarshalAs(UnmanagedType.LPStr)] string pszArgs);
			
			//[propget, helpstring("Retrieves or sets the shell link hot key")]
			void GetHotkey(out short pwHotkey);
			//[propput, helpstring("Retrieves or sets the shell link hot key")]
			void SetHotkey(short pwHotkey);
			
			//[propget, helpstring("Retrieves or sets the shell link show command")]
			void GetShowCmd(out uint piShowCmd);
			//[propput, helpstring("Retrieves or sets the shell link show command")]
			void SetShowCmd(uint piShowCmd);
			
			//[helpstring("Retrieves the location (path and index) of the shell link icon")]
			void GetIconLocation([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
			
			//[helpstring("Sets the location (path and index) of the shell link icon")]
			void SetIconLocation([MarshalAs(UnmanagedType.LPStr)] string pszIconPath, int iIcon);
			
			//[helpstring("Sets the shell link relative path")]
			void SetRelativePath([MarshalAs(UnmanagedType.LPStr)] string pszPathRel, uint dwReserved);
			
			//[helpstring("Resolves a shell link. The system searches for the shell link object and updates the shell link path and its list of identifiers (if necessary)")]
			void Resolve(IntPtr hWnd, uint fFlags);
			
			//[helpstring("Sets the shell link path and filename")]
			void SetPath([MarshalAs(UnmanagedType.LPStr)] string pszFile);
		}
		
		[ComImportAttribute()]
		[GuidAttribute("000214F9-0000-0000-C000-000000000046")]
		[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		private interface IShellLinkW {
			
			void GetPath([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, ref _WIN32_FIND_DATAW pfd, uint fFlags);
			
			void GetIDList(out IntPtr ppidl);
			
			void SetIDList(IntPtr pidl);
		
			void GetDescription([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxName);
			
			void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
			
			void GetWorkingDirectory([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
			
			void SetWorkingDirectory( [MarshalAs(UnmanagedType.LPWStr)] string pszDir);
			
			void GetArguments([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
			
			void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
			
			void GetHotkey(out short pwHotkey);
			void SetHotkey(short pwHotkey);
			
			void GetShowCmd(out uint piShowCmd);
			void SetShowCmd(uint piShowCmd);
			
			void GetIconLocation([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
			
			void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
			
			void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, uint dwReserved);
			
			void Resolve(IntPtr hWnd, uint fFlags);
			
			void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
		}
		
		[GuidAttribute("00021401-0000-0000-C000-000000000046")]
		[ClassInterfaceAttribute(ClassInterfaceType.None)]
		[ComImportAttribute()]
		private class CShellLink {
		}
		
		private enum EShellLinkGP : uint {
			SLGP_SHORTPATH = 1,
			SLGP_UNCPRIORITY = 2
		}
		
		[StructLayoutAttribute(LayoutKind.Sequential, Pack = 4, Size = 0, CharSet = CharSet.Unicode)]
		private struct _WIN32_FIND_DATAW {
			public uint dwFileAttributes;
			public _FILETIME ftCreationTime;
			public _FILETIME ftLastAccessTime;
			public _FILETIME ftLastWriteTime;
			public uint nFileSizeHigh;
			public uint nFileSizeLow;
			public uint dwReserved0;
			public uint dwReserved1;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] // MAX_PATH
			public string cFileName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
			public string cAlternateFileName;
		}
		
		[StructLayoutAttribute(LayoutKind.Sequential, Pack = 4, Size = 0, CharSet = CharSet.Ansi)]
		private struct _WIN32_FIND_DATAA {
			public uint dwFileAttributes;
			public _FILETIME ftCreationTime;
			public _FILETIME ftLastAccessTime;
			public _FILETIME ftLastWriteTime;
			public uint nFileSizeHigh;
			public uint nFileSizeLow;
			public uint dwReserved0;
			public uint dwReserved1;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] // MAX_PATH
			public string cFileName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
			public string cAlternateFileName;
		}
		
		[StructLayoutAttribute(LayoutKind.Sequential, Pack = 4, Size = 0)]
		private struct _FILETIME {
			public uint dwLowDateTime;
			public uint dwHighDateTime;
		}
		
		// Use Unicode (W) under NT, otherwise use ANSI		
		private IShellLinkW linkW;
		private IShellLinkA linkA;
		private string shortcutFile = "";
		
#endregion
		
		/// <summary>Creates an instance of the Shell Link object.</summary>
		public ShellLink() {
			if(System.Environment.OSVersion.Platform == PlatformID.Win32NT) {
				linkW = (IShellLinkW)new CShellLink();
			} else {
				linkA = (IShellLinkA)new CShellLink();
			}
		}
		
		/// <summary>Creates an instance of a Shell Link object from the specified link file</summary>
		/// <param name="linkFile">The Shortcut file to open</param>
		public ShellLink(string linkFile) : this() {
			Open(linkFile);
		}
		
		~ShellLink() {
			// Call dispose just in case it hasn't happened yet
			Dispose(false);
		}
		
		/// <summary>Dispose the object, releasing the COM ShellLink object</summary>
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		
		protected virtual void Dispose(Boolean disposing) {
			
			if( disposing ) {
				// free managed resources
				
			}
			// free native resources
			
			if(linkW != null) {
				Marshal.ReleaseComObject(linkW);
				linkW = null;
			}
			if(linkA != null) {
				Marshal.ReleaseComObject(linkA);
				linkA = null;
			}
			
		}
		
		/// <summary>Gets the path to the file containing the icon for this shortcut.</summary>
		public string IconPath {
			get {
				StringBuilder iconPath = new StringBuilder(260, 260);
				int iconIndex = 0;
				if(linkA == null) {
					linkW.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
				} else {
					linkA.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
				}
				return iconPath.ToString();
			}
			set {
				StringBuilder iconPath = new StringBuilder(260, 260);
				int iconIndex = 0;
				if(linkA == null) {
					linkW.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
				} else {
					linkA.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
				}
				if(linkA == null) {
					linkW.SetIconLocation(value, iconIndex);
				} else {
					linkA.SetIconLocation(value, iconIndex);
				}
			}
		}
		
		/// <summary>Gets the index of this icon within the icon path's resources</summary>
		public int IconIndex {
			get {
				StringBuilder iconPath = new StringBuilder(260, 260);
				int iconIndex = 0;
				if(linkA == null) {
					linkW.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
				} else {
					linkA.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
				}
				return iconIndex;
			}
			set {
				StringBuilder iconPath = new StringBuilder(260, 260);
				int iconIndex = 0;
				if( linkW != null ) {
					linkW.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
					linkW.SetIconLocation(iconPath.ToString(), value);
				} else {
					linkA.GetIconLocation(iconPath, iconPath.Capacity, out iconIndex);
					linkA.SetIconLocation(iconPath.ToString(), value);
				}
			}
		}
		
		/// <summary>Gets/sets the fully qualified path to the link's target</summary>
		public string Target {
			get {
				StringBuilder target = new StringBuilder(260, 260);
				if(linkA == null) {
					_WIN32_FIND_DATAW fd = new _WIN32_FIND_DATAW();
					linkW.GetPath(target, target.Capacity, ref fd, (uint)EShellLinkGP.SLGP_UNCPRIORITY);
				} else {
					_WIN32_FIND_DATAA fd = new _WIN32_FIND_DATAA();
					linkA.GetPath(target, target.Capacity, ref fd, (uint)EShellLinkGP.SLGP_UNCPRIORITY);
				}
				return target.ToString();
			}
			set {
				if(linkA == null) {
					linkW.SetPath(value);
				} else {
					linkA.SetPath(value);
				}
			}
		}
		
		/// <summary>Gets/sets the Working Directory for the Link</summary>
		public string WorkingDirectory {
			get {
				StringBuilder path = new StringBuilder(260, 260);
				if(linkA == null) {
					linkW.GetWorkingDirectory(path, path.Capacity);
				} else {
					linkA.GetWorkingDirectory(path, path.Capacity);
				}
				return path.ToString();
			}
			set {
				if(linkA == null) {
					linkW.SetWorkingDirectory(value);
				} else {
					linkA.SetWorkingDirectory(value);
				}
			}
		}
		
		/// <summary>Gets/sets the description of the link</summary>
		public string Description {
			get {
				StringBuilder description = new StringBuilder(1024, 1024);
				if(linkA == null) {
					linkW.GetDescription(description, description.Capacity);
				} else {
					linkA.GetDescription(description, description.Capacity);
				}
				return description.ToString();
			}
			set {
				if(linkA == null) {
					linkW.SetDescription(value);
				} else {
					linkA.SetDescription(value);
				}
			}
		}
		
		/// <summary>Gets/sets any command line arguments associated with the link</summary>
		public string Arguments {
			get {
				StringBuilder arguments = new StringBuilder(260, 260);
				if(linkA == null) {
					linkW.GetArguments(arguments, arguments.Capacity);
				} else {
					linkA.GetArguments(arguments, arguments.Capacity);
				}
				return arguments.ToString();
			}
			set {
				if(linkA == null) {
					linkW.SetArguments(value);
				} else {
					linkA.SetArguments(value);
				}
			}
		}
		
		/// <summary>Gets/sets the initial display mode when the shortcut is run</summary>
		public ShellLinkWindowMode DisplayMode {
			get {
				uint cmd = 0;
				if(linkA == null) {
					linkW.GetShowCmd(out cmd);
				} else {
					linkA.GetShowCmd(out cmd);
				}
				return (ShellLinkWindowMode)cmd;
			}
			set {
				if(linkA == null) {
					linkW.SetShowCmd((uint)value);
				} else {
					linkA.SetShowCmd((uint)value);
				}
			}
		}
		
		/// <summary>Gets/sets the HotKey to start the shortcut (if any)</summary>
		public Int16 HotKey {
			get {
				short key = 0;
				if(linkA == null) {
					linkW.GetHotkey(out key);
				} else {
					linkA.GetHotkey(out key);
				}
				return key;
			}
			set {
				if(linkA == null) {
					linkW.SetHotkey((short)value);
				} else {
					linkA.SetHotkey((short)value);
				}
			}
		}
		
//		/// <summary>Saves the shortcut to ShortCutFile.</summary>
//		public void Save() {
//			Save(shortcutFile);
//		}
		
		/// <summary>Saves the shortcut to the specified file</summary>
		/// <param name="linkFile">The shortcut file (.lnk)</param>
		public void Save(string linkFile) {
			
			// Save the object to disk
			if(linkA == null) {
				((IPersistFile)linkW).Save(linkFile, true);
				shortcutFile = linkFile;
			} else {
				((IPersistFile)linkA).Save(linkFile, true);
				shortcutFile = linkFile;
			}
		}
		
		/// <summary>Loads a shortcut from the specified file</summary>
		/// <param name="linkFile">The shortcut file (.lnk) to load</param>
		public void Open(string linkFile) {
			
			Open(linkFile, IntPtr.Zero, (ShellLinkResolutionBehavior.AnyMatch | ShellLinkResolutionBehavior.NoUI), 1);
		}
		
		/// <summary>Loads a shortcut from the specified file, and allows flags controlling
		/// the UI behaviour if the shortcut's target isn't found to be set.</summary>
		/// <param name="linkFile">The shortcut file (.lnk) to load</param>
		/// <param name="hWnd">The window handle of the application's UI, if any</param>
		/// <param name="resolveFlags">Flags controlling resolution behaviour</param>
		public void Open(string linkFile, IntPtr hWnd, ShellLinkResolutionBehavior resolutionBehavior) {
			
			Open(linkFile, hWnd, resolutionBehavior, 1);
		}
		
		/// <summary>Loads a shortcut from the specified file, and allows flags controlling
		/// the UI behaviour if the shortcut's target isn't found to be set.  If
		/// no SLR_NO_UI is specified, you can also specify a timeout.</summary>
		/// <param name="linkFile">The shortcut file (.lnk) to load</param>
		/// <param name="hWnd">The window handle of the application's UI, if any</param>
		/// <param name="resolveFlags">Flags controlling resolution behaviour</param>
		/// <param name="timeOut">Timeout if SLR_NO_UI is specified, in ms.</param>
		public void Open(string linkFile, IntPtr hWnd, ShellLinkResolutionBehavior resolutionBehavior, ushort timeOut) {
			
			uint flags;
			
			if((resolutionBehavior & ShellLinkResolutionBehavior.NoUI) == ShellLinkResolutionBehavior.NoUI) {
				
				flags = (uint)((int)resolutionBehavior | (timeOut << 16));
				
			} else {
				
				flags = (uint)resolutionBehavior;
			}
			
			if(linkA == null) {
				((IPersistFile)linkW).Load(linkFile, 0); //STGM_DIRECT)
				linkW.Resolve(hWnd, flags);
				this.shortcutFile = linkFile;
			} else {
				((IPersistFile)linkA).Load(linkFile, 0); //STGM_DIRECT)
				linkA.Resolve(hWnd, flags);
				this.shortcutFile = linkFile;
			}
		}
		
#region Static
		
		public static void CreateShellLink(String lnkFileName, String target, String description) {
			
			using(ShellLink link = new ShellLink()) {
				
				link.Target           = target;
				link.WorkingDirectory = System.IO.Path.GetDirectoryName( target );
				link.Description      = description;
				link.Save( lnkFileName );
			}
			
		}
		
#endregion
		
	}
	
	/// <summary>Flags determining how the links with missing targets are resolved.</summary>
	[Flags]
	public enum ShellLinkResolutionBehavior : uint {
		None = 0,
		
		/// <summary>Do not display a dialog box if the link cannot be resolved. When SLR_NO_UI is set, a time-out value that specifies the maximum amount of time to be spent resolving the link can 
		/// be specified in milliseconds. The function returns if the link cannot be resolved within the time-out duration. If the timeout is not set, the time-out duration will be set to the default value of 3,000 milliseconds (3 seconds). </summary>										    
		NoUI = 0x01,
		
		/// <summary>Allow any match during resolution.  Has no effect on ME/2000 or above, use the other flags instead.</summary>
		AnyMatch = 0x02,
		
		/// <summary>If the link object has changed, update its path and list of identifiers. If SLR_UPDATE is set, you do not need to call IPersistFile::IsDirty to determine whether or not the link object has changed. </summary>
		Update = 0x04,
		
		/// <summary>Do not update the link information. </summary>
		NoUpdate = 0x08,
		
		/// <summary>Do not execute the search heuristics. </summary>
		NoSearch = 0x10,
		
		/// <summary>Do not use distributed link tracking. </summary>
		NoDistributedLinkTracking = 0x20,
		
		/// <summary>Disable distributed link tracking. By default, distributed link tracking tracks removable media across multiple devices based on the volume name. 
		/// It also uses the UNC path to track remote file systems whose drive letter has changed. Setting SLR_NOLINKINFO disables both types of tracking.</summary>
		NoLinkInfo = 0x40,
		
		/// <summary>Call the Microsoft Windows Installer. </summary>
		InvokeMsi = 0x80,
		
		/// <summary>Undocumented. Assume same as SLR_NO_UI but intended for applications without a hWnd.</summary>
		NoUIWithMessagePump = 0x101
	}
	
	public enum ShellLinkWindowMode : uint {
		Hide           = 0,
		Normal         = 1,
		Minimized      = 2,
		Maximized      = 3,
		ShowNoActivate = 4,
		Show           = 5,
		Minimize       = 6,
		ShowMinimizedNoActivate = 7,
		ShowNA         = 8,
		Restore        = 9,
		ShowDefault    = 10
	}

}