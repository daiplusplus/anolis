using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using Anolis.Core.Native;

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
		
		/// <summary>When a PE is modified, even by Win32's resource functions, the checksum value in the PE header remains unchanged. This method corrects the checksum header value.</summary>
		public unsafe static void CorrectPEChecksum(String fileName) {
			
			try {
				
				using(FileMapping map = new FileMapping(fileName, FileMapMode.ReadWrite)) {
					
					IntPtr pData = map.View;
					
					UInt32 oldChecksum = 0;
					UInt32 newChecksum = 0;
					
					UInt32 size = NativeMethods.GetFileSize( map.FileHandle, IntPtr.Zero );
					
					// CheckSumMappedFile parses the file passed in and returns a pointer to the start of the NTHeader
					IntPtr pNTHeader = NativeMethods.CheckSumMappedFile( pData, size, ref oldChecksum, ref newChecksum );
					
					// the Checksum field is always located at the same offset of the NTHeader, regardless of whether it's a PE or PE+ headered image file.
					
					// -------------------------------------------------
					// | Section     | Offset from 'PE00' | Size       |
					// -------------------------------------------------
					// | PE Sig      |  0                 |   4        |
					// | FileHeader  |  4                 |  20        |
					// | Optional    | 24                 |  96 or 112 |
					// -------------------------------------------------
					
					// CheckSum is 64 bytes away from the start of the OptionalHeader
					// Therefore it's 64 + 24
					
					UInt32* p = (UInt32*)pNTHeader.ToPointer() + ( (64 + 24) / sizeof(UInt32));
					*p = newChecksum;
				
				} // close the FileMapping and commit the memory changes to disk
				
			} catch(Win32Exception wex) {
				
				throw new AnolisException("Error during Checksum Correction", wex);
			}
			
		}
		
		/// <summary>Loads an image from a file without locking the file.</summary>
		public static Image ImageFromFile(String fileName) {
			
			// using Bitmap.FromFile causes it to lock the file, the lock seems to stick even when you .Clone() it
			// so load it from a stream
			
			Image image;
			
			using(FileStream fs = new FileStream( fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				
				image = Image.FromStream( fs, false, true );
			}
			
			return image;
		}
		
		public static Boolean IsElevatedAdministrator {
			get {
				WindowsIdentity identity = WindowsIdentity.GetCurrent();
				WindowsPrincipal principal = new WindowsPrincipal(identity);
				return principal.IsInRole(WindowsBuiltInRole.Administrator);
			}
		}
		
		public static String GetUnusedFileName(String suspectPath) {
			
			if( !File.Exists( suspectPath ) ) return suspectPath;
			
			String dir = Path.GetDirectoryName( suspectPath ),
			       nom = Path.GetFileNameWithoutExtension( suspectPath ),
			       ext = Path.GetExtension( suspectPath );
			
			Int32 i = 1;
			
			while( File.Exists( suspectPath ) ) {
				
				suspectPath = Path.Combine( dir, nom );
				suspectPath += " (" + i++ + ')';
				suspectPath += ext;
				
			}
			
			return suspectPath;
			
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
