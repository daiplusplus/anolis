using System;
using System.IO;
using System.Collections.Generic;
using System.Management;
using System.Reflection;
using System.Security.Principal;
using System.Text;

using Anolis.Core.Native;
using Microsoft.Win32;

using Path          = System.IO.Path;
using Stream        = System.IO.Stream;
using Cult          = System.Globalization.CultureInfo;
using MoveFileFlags = Anolis.Core.Native.NativeMethods.MoveFileFlags;

namespace Anolis.Core.Packages {
	
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
		
		public static void AddPfroEntry(String fromFilename, String toFilename) {
			
			if( !NativeMethods.MoveFileEx( fromFilename, toFilename, MoveFileFlags.DelayUntilReboot | MoveFileFlags.ReplaceExisting ) ) {
				
				throw new PackageException("MoveFileEx failed: " + NativeMethods.GetLastErrorString() );
			}
			
		}
		
		public static void InitRestart() {
			
			NativeMethods.EnableProcessToken( NativeMethods.SePrivileges.SHUTDOWN );
			
			NativeMethods.ExitWindowsEx(NativeMethods.ExitWindows.Reboot, 0);
			
		}
		
		public static Boolean CreateSystemRestorePoint(String name, SystemRestoreType type, SystemRestoreEventType eventType) {
			
			// use WMI for now rather than the native API
			
			ManagementScope scope = new ManagementScope(@"\\localhost\root\default");
			ManagementPath  path  = new ManagementPath("SystemRestore");
			
			ObjectGetOptions getOpts = new ObjectGetOptions();
			
			ManagementClass clas = new ManagementClass(scope, path, getOpts);
			
			ManagementBaseObject inParams = clas.GetMethodParameters("CreateRestorePoint");
			inParams["Description"]       = name;
			inParams["RestorePointType"]  = (int)type;
			inParams["EventType"]         = (int)eventType;
			
			ManagementBaseObject outParams = clas.InvokeMethod("CreateRestorePoint", inParams, null);
			Object ret = outParams.Properties["ReturnValue"];
			
			if(ret is Int32) {
				
				return (int)ret == 0;
			} else {
				
				return false;
			}
		}
		
		public enum SystemRestoreType {
			ApplicationInstall   = 0,
			ApplicationUninstall = 1,
			DeviceDriverInstall  = 10,
			ModifySettings       = 12,
			CanceledOperation    = 13
		}
		
		public enum SystemRestoreEventType {
			BeginSystemChange       = 100,
			EndSystemChange         = 101,
			BeginNestedSystemChange = 102,
			EndNestedSystemChange   = 103
		}
		
		public static Boolean IsElevatedAdministrator {
			get {
				WindowsIdentity identity = WindowsIdentity.GetCurrent();
				WindowsPrincipal principal = new WindowsPrincipal(identity);
				return principal.IsInRole(WindowsBuiltInRole.Administrator);
			}
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
		
		public static UInt16 GetSystemInstallLanguage() {
			
			return NativeMethods.GetSystemDefaultUILanguage();
		}
		
		public static UInt16 GetSystemInstallPriLanguage() {
			
			UInt16 lang = NativeMethods.GetSystemDefaultUILanguage();
			
			// based on the PRIMARYLANGID macro
			lang = (ushort)( (ushort)lang & (ushort)0x3FF );
			
			return lang;
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
		
	}
}
