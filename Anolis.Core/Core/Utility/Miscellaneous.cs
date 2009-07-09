using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

using Anolis.Core.Source;
using Anolis.Core.Native;

using Sfh     = Microsoft.Win32.SafeHandles.SafeFileHandle;
using System.ComponentModel;

namespace Anolis.Core.Utility {
	
	public static class Miscellaneous {
		
		public static String CreateFileFilter(String description, params String[] extensions) {
			
			StringBuilder sb = new StringBuilder();
			
			sb.Append( description );
			sb.Append( " (" );
			
			for(int i=0;i<extensions.Length;i++) {
				String ext = extensions[i];
				
				sb.Append("*.");
				sb.Append( ext );
				if(i < extensions.Length - 1) sb.Append(';');
				
			}
			
			sb.Append(")|");
			
			for(int i=0;i<extensions.Length;i++) {
				String ext = extensions[i];
				
				sb.Append("*.");
				sb.Append( ext );
				if(i < extensions.Length - 1) sb.Append(';');
				
			}
			
			String retval = sb.ToString();
			
			return retval;
			
		}
		
		public static String TrimPath(String path, Int32 maxLength) {
			
			Char[] chars = new Char[] { '/', '\\' };
			
			if(path.Length <= maxLength) return path;
			
			String truncated = path;
			while(truncated.Length > maxLength) {
				// take stuff out from the middle
				// so starting from the middle search for the first slash remove it to the next slash on
				Int32 midSlashIdx = truncated.LastIndexOfAny( chars, truncated.Length / 2 );
				if( midSlashIdx == -1 ) return truncated;
				
				Int32 nextSlashForwardIdx = truncated.IndexOfAny( chars, midSlashIdx + 4 );
				
				truncated = truncated.Substring(0, midSlashIdx) + @"\.." + truncated.Substring( nextSlashForwardIdx );
			}
			
			return truncated;
		}
		
		public static String RemoveIllegalFileNameChars(String fileName, Char? replaceWith) {
			
			Char[] illegals = System.IO.Path.GetInvalidFileNameChars();
			Char[] str      = fileName.ToCharArray();
			
			if(replaceWith == null) {
				
				StringBuilder sb = new StringBuilder();
				
				for(int i=0;i<fileName.Length;i++) {
					
					Boolean isIllegal = false;
					for(int j=0;j<illegals.Length;j++) {
						if(str[i] == illegals[j]) {
							isIllegal = true;
							break;
						}
					}
					
					if(!isIllegal) sb.Append( str[i] );
					
				}
				
				return sb.ToString();
				
			} else {
				
				for(int i=0;i<fileName.Length;i++) {
					
					Boolean isIllegal = false;
					for(int j=0;j<illegals.Length;j++) {
						if(str[i] == illegals[j]) {
							isIllegal = true;
							break;
						}
					}
					
					if(isIllegal) str[i] = replaceWith.Value;
					
				}
				
				return new String( str );
				
			}
			
		}
		
		public static String FSSafeResPath(String path) {
			
			path = path.Replace('\\', '-');
			
			return RemoveIllegalFileNameChars( path, '_' );
		}
		
		public static void GrantFile(String fileName) {
			
			if( Environment.OSVersion.Version.Major >= 6 ) {
				
				RunProcHiddenSync("takeown", "/f " + fileName);
				
				RunProcHiddenSync("icacls", fileName + " /grant %username%:F");
				
				RunProcHiddenSync("icacls", fileName + " /grant *S-1-1-0:(F)");
				
			} else if( Environment.OSVersion.Version.Major == 5) {
				
				WfpResult result = NativeMethods.SetSfcFileException( 0, fileName, -1 );
				if( result != WfpResult.Success ) { // throw?
					
				}
				
			}
			
		}
		
		public static void RunProcHiddenSync(String processFileName, String arguments) {
			
			ProcessStartInfo procStart = new ProcessStartInfo(processFileName, arguments);
			procStart.CreateNoWindow = true;
			procStart.WindowStyle    = ProcessWindowStyle.Hidden;
			
			Process proc = Process.Start( procStart );
			
			if( !proc.WaitForExit( 500 ) )
				throw new AnolisException("Process did not terminate after 500ms");
			
			
		}
		
		internal static MachineType GetMachineType(String fileName) {
			
			Pair<DosHeader,NTHeader> headers = GetPEHeaders(fileName);
			
			DosHeader dos = headers.X;
			NTHeader  nt  = headers.Y;
			
			if( dos.e_magic  != DosHeader.DosMagic   ) return MachineType.Unknown;
			if( nt.Signature != NTHeader.NTSignature ) return MachineType.Unknown;
			
			MachineType type = nt.FileHeader.Machine;
			
			return type;
		}
		
		internal static Pair<DosHeader,NTHeader> GetPEHeaders(String fileName) {
			
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
			
			Pair<DosHeader,NTHeader> ret = new Pair<DosHeader,NTHeader>( dos, nt );
			
			return ret;
			
		}
		
		/// <summary>When a PE is modified, even by Win32's resource functions, the checksum value in the PE header remains unchanged. This method corrects the checksum header value.</summary>
		public static void CorrectPEChecksum(String fileName) {
			
			try {
				
				using(FileMapping map = new FileMapping(fileName, FileMapMode.ReadWrite)) {
					
					IntPtr pData = map.View;
					
					UInt32 oldChecksum = 0;
					UInt32 newChecksum = 0;
					
					UInt32 size = NativeMethods.GetFileSize( map.FileHandle, IntPtr.Zero );
					
					IntPtr pNTHeader = NativeMethods.CheckSumMappedFile( pData, size, ref oldChecksum, ref newChecksum );
					
					// don't worry about NTHeader being different for x64 vs. x86 PE files as the CheckSum field has the same offset in both cases
					// also, the x86 version is smaller than the x64 version, so you can use it to overwrite a x64 version without having to worry about access violations
					
					NTHeader ntHeader = (NTHeader)Marshal.PtrToStructure( pNTHeader, typeof(NTHeader) );
					ntHeader.OptionalHeader.CheckSum = newChecksum;
					
					Marshal.StructureToPtr( ntHeader, pNTHeader, false );
					
				}
				
			} catch(Win32Exception wex) {
				
				throw new AnolisException("Error during Checksum Correction", wex);
			}
			
		}
		
#if NEVER
		
		/// <summary>When a PE is modified, even by Win32's resource functions, the checksum value in the PE header remains unchanged. This method corrects the checksum header value.</summary>
		public static void CorrectPEChecksum(String fileName) {
			
			// This method seems to cause some problems with file handles being invalid
			// when I disabled this method errors stopped being reported
			// but I've since changed the exception behaviour (it wasn't checking the return value of the CloseHandle functions in the }finally{} block) so I'll need to test this out
			
			// Get handle to a file with read/write access
			// CreateFileMapping
			// MapViewOfFile
			// Check DOS/NT Headers (optional, not needed in this case since if it's gotten this far it's going to be a Win32 PE)
			// CheckSumMappedFile
			// Close the handles
			
			Sfh    sfh   = null;
			IntPtr hMap  = IntPtr.Zero;
			IntPtr pData = IntPtr.Zero;
			
			try {
				
				sfh = NativeMethods.CreateFile( fileName, FileAccess.ReadWrite, FileShare.None, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero );
				
				if( sfh.IsInvalid ) throw new AnolisException("Invalid SafeFileHandle: " + NativeMethods.GetLastErrorString() );
				
				hMap = NativeMethods.CreateFileMapping( sfh, IntPtr.Zero, FileMapProtection.PageReadWrite, 0, 0, null );
				
				if( hMap == IntPtr.Zero ) throw new AnolisException("Invalid File Mapping: " + NativeMethods.GetLastErrorString() );
				
				pData = NativeMethods.MapViewOfFile( hMap, FileMapAccess.FileMapWrite, 0, 0, IntPtr.Zero );
				
				if( pData == IntPtr.Zero ) throw new AnolisException("Invalid Map View of File: " + NativeMethods.GetLastErrorString() );
				
				// checking the PE headers for validity isn't required in this scenario
				
				UInt32 oldChecksum = 0;
				UInt32 newChecksum = 0;
				
				UInt32 size = NativeMethods.GetFileSize( sfh, IntPtr.Zero );
				
				// Get the NTHeader
				IntPtr pNTHeader = NativeMethods.CheckSumMappedFile( pData, size, ref oldChecksum, ref newChecksum );
				
				if( pNTHeader == IntPtr.Zero ) throw new AnolisException("CheckSumMappedFile Failed: " + NativeMethods.GetLastErrorString() );
				
				NTHeader ntHeader = (NTHeader)Marshal.PtrToStructure( pNTHeader, typeof(NTHeader) );
				
				// Set the CheckSum, but as this is a new allocation by Marshal it needs to be copied back to the mapped memory
				ntHeader.OptionalHeader.CheckSum = newChecksum;
				
				// this overwrites the old NTHeader @ pNTHeader
				Marshal.StructureToPtr( ntHeader, pNTHeader, false );
				
			} finally {
				
				String errors = String.Empty;
				
				if( pData != IntPtr.Zero )
					if( !NativeMethods.UnmapViewOfFile( pData ) ) {
						errors += "UnmapViewOfFile failed: " + NativeMethods.GetLastErrorString() + "\r\n";
					}
					
				if( hMap  != IntPtr.Zero )
					if( !NativeMethods.CloseHandle( hMap ) ) {
						errors += "CloseHandle(hMap) failed: " + NativeMethods.GetLastErrorString() + "\r\n";
					}
					
				if( sfh   != null && !sfh.IsInvalid )
					if( !NativeMethods.CloseHandle( sfh ) ) {
						errors += "CloseHandle(sfh) failed: " + NativeMethods.GetLastErrorString() + "\r\n";
					}
				
				
			}
			
		}
#endif
		
#region Extensibility
		
		/// <summary>Tells the factories where they can find additional assemblies. This only takes effect if the factories haven't already enumerated types</summary>
		public static void SetAssemblyFileNames(String[] assemblies) {
			
			FactoryBase.AssemblyFileNames = assemblies;
		}
		
		public static Boolean IsAssembly(String fileName) {
			
			try {
				
				AssemblyName name = AssemblyName.GetAssemblyName( fileName );
				
				return true;
			
			} catch( ArgumentException ) {
				
				return false;
				
			} catch( BadImageFormatException ) {
				
				return false;
			}
			
		}
		
		/// <summary>Searches the specified assembly for objects derived (or implementing) superType then instantiates and returns them.</summary>
		public static T[] InstantiateTypes<T>(Assembly assembly, Type superType) where T : class {
			
			Type[] types = assembly.GetTypes();
			
			List<T> retval = new List<T>();
			
			if( superType.IsInterface ) {
				
				foreach(Type t in types) {
					
					if( t.IsClass && !t.IsAbstract ) continue;
					
					if( t.GetInterface( superType.FullName ) == null ) continue;
					
					// if it implements the interface and is constructable
					ConstructorInfo constructor = t.GetConstructor( new Type[] { } );
					if(constructor != null) retval.Add( Activator.CreateInstance( t ) as T );
					
				}
				
			} else if( superType.IsClass ) {
				
				foreach(Type t in types) {
					
					if( t.IsClass && !t.IsAbstract ) continue;
					
					if( t.IsSubclassOf( superType ) ) {
						
						// if it derives from superType and is constructable
						ConstructorInfo constructor = t.GetConstructor( new Type[] { } );
						if(constructor != null) retval.Add( Activator.CreateInstance( t ) as T );
						
					}
					
				}
				
			}
			
			return retval.ToArray();
			
		}
		
#endregion
		
	}
}
