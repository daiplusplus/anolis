using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using Microsoft.Win32;

using Anolis.Packages.Native;

using PEPair = Anolis.Core.Utility.Pair<Anolis.Packages.Native.DosHeader, Anolis.Packages.Native.NTHeader>;


namespace Anolis.Packages.Utility {
	
	public class EmbeddedPackage {
		
		internal EmbeddedPackage(String name, Assembly assembly, String manifestResourceName) {
			
			Name                 = name;
			
			Assembly             = assembly;
			ManifestResourceName = manifestResourceName;
		}
		
		public String Name { get; private set; }
		
		internal Assembly Assembly             { get; set; }
		internal String   ManifestResourceName { get; set; }
		
		public override String ToString() {
			return Name;
		}
	}
	
	public static class PackageUtility {
		
#region Embedded Packages
		
		public static EmbeddedPackage[] GetEmbeddedPackages() {
			
			return GetEmbeddedPackages( Assembly.GetEntryAssembly() );
			
		}
		
		public static EmbeddedPackage[] GetEmbeddedPackages(Assembly inAssembly) {
			
			List<EmbeddedPackage> packages = new List<EmbeddedPackage>();
			
			String[] resourceNames = inAssembly.GetManifestResourceNames();
			foreach(String resName in resourceNames) {
				
				if( resName.EndsWith(".anop", StringComparison.Ordinal) ) {
					
					String packName = resName.Substring(0, resName.Length - ".anop".Length );
					
					packages.Add( new EmbeddedPackage( packName, inAssembly, resName ) );
					
				}
				
			}
			
			return packages.ToArray();
			
		}
		
		public static Stream GetEmbeddedPackage(EmbeddedPackage package) {
			
			return package.Assembly.GetManifestResourceStream( package.ManifestResourceName );
		}
		
#endregion
		
		public static void AllowProtectedRenames() {
			
			RegistryKey sessionManager = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager", true);
				
			if( (int)sessionManager.GetValue("AllowProtectedRenames", 0) == 0 ) {
				
				sessionManager.SetValue("AllowProtectedRenames", 1, RegistryValueKind.DWord);
			}
			
			sessionManager.Close();
			
		}
		
		public static Boolean HasPendingRestart() {
			
			if( HasPendingRenames() ) return true;
			
			if( HasPendingWURestart() ) return true;
			
			return false;
		}
		
		private static Boolean HasPendingRenames() {
			
			RegistryKey sessionManager = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager", false);
			if( sessionManager == null ) return false;
			
			String[] names = sessionManager.GetValueNames();
			Boolean hasEntry = false;
			foreach(String valueName in names) {
				
				if( String.Equals(valueName, "PendingFileRenameOperations", StringComparison.OrdinalIgnoreCase ) ) hasEntry = true;
			}
			
			if( !hasEntry ) {
				sessionManager.Close();
				return false;
			}
			
			RegistryValueKind kind = sessionManager.GetValueKind("PendingFileRenameOperations");
			if( kind != RegistryValueKind.MultiString ) {
				
				sessionManager.Close();
				return false;
			}
			
			String[] pfro = (String[])sessionManager.GetValue("PendingFileRenameOperations", null);
			if( pfro.Length == 0 ) {
				
				sessionManager.Close();
				return false;
			}
			
			sessionManager.Close();
			return true;
			
		}
		
		private static Boolean HasPendingWURestart() {
			
			RegistryKey autoUpdateKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Auto Update");
			if( autoUpdateKey == null ) return false;
			
			String[] subkeyNames = autoUpdateKey.GetSubKeyNames();
			foreach(String subkeyName in subkeyNames) {
				
				if( String.Equals(subkeyName, "RebootRequired", StringComparison.OrdinalIgnoreCase ) ) return true;
			}
			
			return false;
		}
		
		public static void AddPfroEntry(String fromFileName, String toFileName) {
			
			if( !NativeMethods.MoveFileEx( fromFileName, toFileName, MoveFileFlags.DelayUntilReboot | MoveFileFlags.ReplaceExisting ) ) {
				
				throw new PackageException("MoveFileEx failed: " + NativeMethods.GetLastErrorString() );
			}
			
		}
		
		public static void AddPfroEntry(DirectoryInfo directoryToDelete) {
			
			// do a DFS with the files deleted before the directory, as is required by MoveFileEx
			
			foreach(DirectoryInfo child in directoryToDelete.GetDirectories()) {
				
				AddPfroEntry( child );
			}
			
			// then delete all the files
			
			foreach(FileInfo file in directoryToDelete.GetFiles()) {
				
				AddPfroEntry( file.FullName, null );
			}
			
			// then the directory itself
			
			AddPfroEntry( directoryToDelete.FullName, null );
			
			// then recurse back (i.e. return)
		}
		
		public static void InitRestart() {
			
			NativeMethods.EnableProcessToken( NativeMethods.SePrivileges.Shutdown );
			
			NativeMethods.ExitWindowsEx(NativeMethods.ExitWindows.Reboot, 0);
			
		}
		
/*		public static Boolean CreateSystemRestorePoint(String name, SystemRestoreType type, SystemRestoreEventType eventType) {
			
			// use WMI for now rather than the native API
			
			ManagementScope scope = new ManagementScope(@"\\localhost\root\default");
			ManagementPath  path  = new ManagementPath("SystemRestore");
			
			ObjectGetOptions getOpts = new ObjectGetOptions();
			
			ManagementClass clas = new ManagementClass(scope, path, getOpts);
			
			ManagementBaseObject inParams = clas.GetMethodParameters("CreateRestorePoint");
			inParams["Description"]       = name;
			inParams["RestorePointType"]  = (int)type;
			inParams["EventType"]         = (int)eventType;
			
			ManagementBaseObject outParams = null;
			try {
				
				outParams = clas.InvokeMethod("CreateRestorePoint", inParams, null);
				
				PropertyData returnValue = outParams.Properties["ReturnValue"];
				
				UInt32 retValue = (UInt32)(returnValue.Value);
				
				return retValue == 0;
				
			} catch( ManagementException ) {
				
				return false;
				
			} finally {
				
				if( outParams != null )
					outParams.Dispose();
					
				inParams.Dispose();
				clas.Dispose();
			}
			
		} */
		
		private static String _resolvedSystem32Path;
		private static String _resolvedSysWow64Path;
		private static String _resolvedProgFilePath;
		private static String _resolvedPrgFle32Path;
		
		/// <summary>If the <paramref name="fileName "/> value points to a file in %windir%\system32 and the equivalent file exists in SysWow64 it will return the path to that file, otherwise null.</summary>
		public static String GetSysWow64File(String fileName) {
			
			if( _resolvedSystem32Path == null ) {
				
				_resolvedSystem32Path = ResolvePath(@"%windir%\system32");
				_resolvedSysWow64Path = ResolvePath(@"%windir%\SysWow64");
				
				_resolvedProgFilePath = ResolvePath(@"%programfiles%"); // this also covers %commonprogramfiles% and %commonprogramfiles(x86)%
				_resolvedPrgFle32Path = ResolvePath(@"%programfiles(x86)%");
			}
			
			if( !Path.IsPathRooted( fileName ) ) return null;
			
			///////////////////////////////////////
			
			if( fileName.StartsWith( _resolvedSystem32Path, StringComparison.OrdinalIgnoreCase ) ) {
				
				String relativePath = fileName.Substring( _resolvedSystem32Path.Length );
				
				if( relativePath.StartsWith("\\", StringComparison.OrdinalIgnoreCase) ) relativePath = relativePath.Substring(1);
				
				fileName = Path.Combine( _resolvedSysWow64Path, relativePath );
				
				if( File.Exists( fileName ) ) return fileName;
			}
			
			///////////////////////////////////////
			
			if( fileName.StartsWith( _resolvedProgFilePath, StringComparison.OrdinalIgnoreCase ) ) {
				
				String relativePath = fileName.Substring( _resolvedProgFilePath.Length );
				
				if( relativePath.StartsWith("\\") ) relativePath = relativePath.Substring(1);
				
				fileName = Path.Combine( _resolvedPrgFle32Path, relativePath );
				
				if( File.Exists( fileName ) ) return fileName;
			}
			
			///////////////////////////////////////
			
			return null;
		}
		
		public static String ResolvePath(String path) {
			
			return ResolvePath(path, String.Empty);
		}
		
		/// <summary>Resolves environment variables inside a path</summary>
		/// <param name="root">If the resolved path is not rooted, this parameter will be intelligently prepended</param>
		public static String ResolvePath(String path, String root) {
			
			String resolved = Environment.ExpandEnvironmentVariables( path );
			if( Path.IsPathRooted( resolved ) ) return resolved;
			
			return Path.Combine( root, resolved );
			
/*			
			
			if( path.IndexOf('%') == -1 ) return path;
			
			StringBuilder retval = new StringBuilder();
			StringBuilder currentEnvVar = new StringBuilder();
			
			Boolean inEnvVar = false;
			for(int i=0;i<path.Length;i++) {
				Char c = path[i];
				
				if(c == '%') inEnvVar = !inEnvVar;
				if(c == '%' && inEnvVar) {
					inEnvVar = path.IndexOf('%', i) > -1; // it doesn't count if there isn't another % in the string later on
					if(inEnvVar) continue; // no point logging the % character
				}
				if(c == '%' && !inEnvVar) {
					continue;
				}
				
				if(!inEnvVar) retval.Append( c );
				else {
					
					currentEnvVar.Append( c );
					
					if( path[i+1] == '%' ) { // if we're at the end of an envvar; // TODO: How do I avoid an indexoutofrange?
						// actually, I don't think this will happen since the check above means we won't be inEnvVar if there isn't another one in the string
						
						String envVar = currentEnvVar.ToString(); currentEnvVar.Length = 0;
						retval.Append( Environment.GetEnvironmentVariable( envVar ) );
						
					}
				}
			}
			
			String ret = retval.ToString();
			
			if( !Path.IsPathRooted( ret ) ) {
				
				ret = Path.Combine( root, ret );
			}
			
			return ret; */
		}
		
		public static void ClearIconCache() {
			
			// until I find a better way, just delete it
			
			// you can delete it on Windows XP without any trouble
			// the file is in the same location on Vista, I imagine this is the same in Win7
			
			String iconCacheFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			       iconCacheFile = Path.Combine( iconCacheFile, "IconCache.db" );
			
			AddPfroEntry( iconCacheFile, null );
		}
		
		/// <summary>If the specified file exists it will be renamed to the next available name by adding an incrementing number to the end of it. Returns null if the file does not exist.</summary>
		public static String ReplaceFile(String path) {
			
			if( !File.Exists( path ) ) return null;
			
			String dir = Path.GetDirectoryName( path );
			String nom = Path.GetFileNameWithoutExtension( path );
			String ext = Path.GetExtension( path );
			
			String old = path;
			
			Int32 i = 1;
			while( File.Exists( path ) ) {
				
				path = Path.Combine( dir, nom );
				path += " (" + i++ + ")";
				path += ext;
			}
			
			File.Move( old, path );
			
			return path;
			
		}
		
		public static String GetUnusedFileName(String suspectPath) {
			
			return Anolis.Core.Utility.Miscellaneous.GetUnusedFileName( suspectPath );
		}
		
		public static String GetUnusedDirectoryName(String suspectPath) {
			
			if( !Directory.Exists( suspectPath ) ) return suspectPath;
			
			String original = suspectPath;
			
			Int32 i=1;
			while( Directory.Exists( suspectPath ) ) {
				
				suspectPath = original + " (" + i++ + ")";
			}
			
			return suspectPath;
		}
		
		public static String GetMD5Hash(String fileName) {
			
			using(FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
			using(System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider()) {
				
				Byte[] hash = md5.ComputeHash( fs );
				
				return hash.ToHexString();
			}
			
		}
		
/*		I can't figure it out. You need to properly parse each RTF document, which is expensive. There are LGPL RTF libraries available, but I CBA.
		public static String ConcatenateRtf(String seperator, params String[] rtfDocuments) {
			
			// remove the end line of the first file
			// for each middle file, remove the first and last lines
			// remove the first line from the last file
			
			StringBuilder sb = new StringBuilder();
			
			if( rtfDocuments.Length == 1 ) return rtfDocuments[0];
			
			for(int i=0;i<rtfDocuments.Length;i++) {
				
				String[] lines = rtfDocuments[i].Split( new String[] { "\r\n" }, StringSplitOptions.None);
				
				if( i == 0 ) { // if first
					
					for(int j=0;j<lines.Length-1;j++)
						sb.AppendLine( lines[j] );
					
				} else if( i < rtfDocuments.Length - 1 ) { // if middle
					
					if( lines.Length > 1 )
						for(int j=1;j<lines.Length-1;j++)
							sb.AppendLine( lines[j] );
					
				} else { // if last
					
					for(int j=0;j<lines.Length;j++)
						sb.AppendLine( lines[j] );
					
				}
				
				if( !String.IsNullOrEmpty( seperator ) ) sb.Append( seperator );
				
			}
			
			return sb.ToString();
			
		} */
		
		public static String GetShortPath(String fileName) {
			
			const int MAX_PATH = 260;
			
			StringBuilder sb = new StringBuilder(MAX_PATH);
			
			uint result = NativeMethods.GetShortPathName( fileName, sb, MAX_PATH );
			if( result == 0 ) throw new AnolisException( "GetShortPath: " + NativeMethods.GetLastErrorString() );
			
			return sb.ToString();
		}
		
		public static void TakeOwnershipAndFullControl(String fileName) {
			
			NativeMethods.EnableProcessToken( NativeMethods.SePrivileges.TakeOwnership );
			NativeMethods.EnableProcessToken( NativeMethods.SePrivileges.Restore );
			
			FileInfo file = new FileInfo( fileName );
			
			// TrustedInstaller is the default owner of system files in Vista, this adds an additional layer of system protection
			// So you need to Take Ownership, then add permission for yourself to do whatever it is you're doing
			
			SecurityIdentifier builtinAdministratorsGroupSid = new SecurityIdentifier( WellKnownSidType.BuiltinAdministratorsSid, null );
			
			// Take Ownership (I think this must be done before Full Control can be granted)
			FileSecurity ownerSec = file.GetAccessControl(AccessControlSections.Owner);
			ownerSec.SetOwner( builtinAdministratorsGroupSid );
			file.SetAccessControl( ownerSec );
			
			// Grant Full Control
			FileSecurity aclSec = file.GetAccessControl(AccessControlSections.Access);
			aclSec.SetAccessRule( new FileSystemAccessRule( builtinAdministratorsGroupSid, FileSystemRights.FullControl, AccessControlType.Allow ) );
			file.SetAccessControl( aclSec );
			
		}
		
		public static void ResetOwnershipAndRelinquishControl(String fileName) {
			
//			// at least, I think this is SDDL format
//			const String trustedInstallerSidSddl = @"S-1-5-80-956008885-3418522649-1831038044-1853292631-2271478464 ";
			
			NativeMethods.EnableProcessToken( NativeMethods.SePrivileges.TakeOwnership );
			NativeMethods.EnableProcessToken( NativeMethods.SePrivileges.Restore );
			
			FileInfo file = new FileInfo( fileName );
			
			// TrustedInstaller is the default owner of system files in Vista, this adds an additional layer of system protection
			// So you need to Take Ownership, then add permission for yourself to do whatever it is you're doing
			
			SecurityIdentifier builtinAdministratorsGroupSid = new SecurityIdentifier( WellKnownSidType.BuiltinAdministratorsSid, null );
//			SecurityIdentifier trustedInstallerSid           = new SecurityIdentifier( trustedInstallerSidSddl );
			
			NTAccount trustedInstallerServiceAccount         = new NTAccount("NT Service\\TrustedInstaller");
			SecurityIdentifier trustedInstallerSid           = (SecurityIdentifier)trustedInstallerServiceAccount.Translate( typeof(SecurityIdentifier) );
			
			// Change Ownership
			FileSecurity ownerSec = file.GetAccessControl(AccessControlSections.Owner);
			ownerSec.SetOwner( trustedInstallerSid );
			file.SetAccessControl( ownerSec );
			
			// Reliquish Full Control
			FileSecurity aclSec = file.GetAccessControl(AccessControlSections.Access);
			aclSec.RemoveAccessRule( new FileSystemAccessRule( builtinAdministratorsGroupSid, FileSystemRights.FullControl, AccessControlType.Allow ) );
			file.SetAccessControl( aclSec );
			
		}
		
		private static readonly String RegPath = PackageUtility.ResolvePath( @"%windir%\system32\reg.exe" );
		
		public static void RegistryExport(String keyName, String fileName) {
			
			// the /y switch is only supported on NT5.2 (and later?) and not NT5.1, so this command fails on XP x86 unless you remove it
			
			//String args = String.Format(CultureInfo.InvariantCulture, "EXPORT \"{0}\" \"{1}\" /y", keyName, fileName );
			String args = String.Format(CultureInfo.InvariantCulture, "EXPORT \"{0}\" \"{1}\"", keyName, fileName );
			
			RunProcHiddenSync( RegPath, args, 2000 );
		}
		
		public static void RegistryImport(String fileName) {
			
			String args = String.Format(CultureInfo.InvariantCulture, "IMPORT \"{0}\"", fileName );
			
			RunProcHiddenSync( RegPath, args, 2000 );
		}
		
		
		
		public static void GrantFile(String fileName) {
			
			if( Environment.OSVersion.Version.Major >= 6 ) {
				
				RunProcHiddenSync("takeown", "/f " + fileName, 500);
				
				RunProcHiddenSync("icacls", fileName + " /grant %username%:F", 500);
				
				RunProcHiddenSync("icacls", fileName + " /grant *S-1-1-0:(F)", 500);
				
			} else if( Environment.OSVersion.Version.Major == 5) {
				
				WfpResult result = NativeMethods.SetSfcFileException( 0, fileName, -1 );
				if( result != WfpResult.Success ) { // throw?
					// TODO
				}
				
			}
			
		}
		
		/// <summary>Starts a process and waits for it to exit. If it hasn't quit by the timeout (and if the timeout is specified in negative units) the process is terminated, otherwise the program resumes, leaving the started process running</summary>
		/// <returns>true if the process finished within the timeout period. False if the process was killed.</returns>
		public static ProcessStartState RunProcHiddenSync(String processFileName, String arguments, Int32 timeout) {
			
			ProcessStartInfo procStart = new ProcessStartInfo(processFileName, arguments);
			procStart.CreateNoWindow = true;
			procStart.WindowStyle    = ProcessWindowStyle.Hidden;
			
			try {
				Process proc = Process.Start( procStart );
				
				if( !proc.WaitForExit( Math.Abs( timeout ) ) ) {
					
					if( timeout < 0 ) {
						
						proc.Kill();
						return ProcessStartState.WasTerminated;
						
					} else return ProcessStartState.StillRunning;
				}
				
			} catch(Win32Exception wex) {
				
				throw new AnolisException("Could not launch process: " + wex.Message, wex);
			}
			
			return ProcessStartState.FinishedWithinTimeout;
		}
		
		public static void RunProcHiddenAsync(String processFileName, String arguments) {
			
			ProcessStartInfo procStart = new ProcessStartInfo(processFileName, arguments);
			procStart.CreateNoWindow = true;
			procStart.WindowStyle    = ProcessWindowStyle.Hidden;
			
			try {
				Process proc = Process.Start( procStart );
				
			} catch(Win32Exception wex) {
				
				throw new AnolisException("Could not launch process: " + wex.Message, wex);
			}
		}
		
		internal static MachineType GetMachineType(String fileName) {
			
			PEPair headers = GetPEHeaders(fileName);
			
			if( headers == null ) return MachineType.Unknown;
			
			DosHeader dos = headers.X;
			NTHeader  nt  = headers.Y;
			
			if( dos.e_magic  != DosHeader.DosMagic   ) return MachineType.Unknown;
			if( nt.Signature != NTHeader.NTSignature ) return MachineType.Unknown;
			
			MachineType type = nt.FileHeader.Machine;
			
			return type;
		}
		
		internal static PEPair GetPEHeaders(String fileName) {
			
			Byte[] fileData = new Byte[0x400]; // need only the first 0x400 bytes
			using(FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				
				if( fs.Read(fileData, 0, 0x400) != 0x400 ) return null;
			}
			
			////////////////////////////
			
			IntPtr p = Marshal.AllocHGlobal( 0x400 );
			Marshal.Copy( fileData, 0, p, 0x400 );
			
			DosHeader dos = (DosHeader)Marshal.PtrToStructure( p, typeof(DosHeader) );
			
			IntPtr ntp = new IntPtr( p.ToInt64() + dos.e_lfanew );
			
			NTHeader nt = (NTHeader)Marshal.PtrToStructure( ntp, typeof(NTHeader) );
			
			Marshal.FreeHGlobal( p );
			
			PEPair ret = new PEPair( dos, nt );
			
			return ret;
			
		}
		
		
	}
	
	public enum ProcessStartState {
		FinishedWithinTimeout,
		WasTerminated,
		StillRunning
	}
	
}
