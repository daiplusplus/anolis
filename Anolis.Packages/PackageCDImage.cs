using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace Anolis.Packages {
	
	public abstract class CDImage {
		
		internal CDImage(DirectoryInfo root) {
			
			if( root == null ) throw new ArgumentNullException("root");
			
			RootDirectory = root;
		}
		
		public DirectoryInfo RootDirectory { get; private set; }
		
		public abstract DirectoryInfo OemRoot    { get; }
		
		public abstract DirectoryInfo OemWindows { get; }
		
		/// <summary>Searches the CD Image for the specified filename, if that fails then it tries for the file that would match the specified post-installation filename. Multiple source files can map to the same post-install filename which is why it returns an array.</summary>
		/// <remarks>The method also searches for both x64 and x86 versions of the same file, so it isn't necessary to search for System32 and SysWow64 at the same time.</remarks>
		public FileInfo[] GetFiles(String fileName) {
			
			return GetFiles( fileName, false );
		}
		
		public abstract FileInfo[] GetFiles(String fileName, Boolean includeNonexistantFiles);
		
		public abstract CDImageOperatingSystem OperatingSystem { get; }
		
		//////////////////////////////////////////
		// Utility
		
		protected static FileInfo[] BuildArray(params FileInfo[] files) {
			
			Int32 nofNotNull = 0;
			for(int i=0;i<files.Length;i++) if( files[i] != null ) nofNotNull++;
			
			FileInfo[] ret = new FileInfo[ nofNotNull ];
			Int32 j=0;
			for(int i=0;i<ret.Length;i++) {
				
				while( files[j] == null ) j++;
				
				ret[i] = files[j++];
			}
			
			return ret;
		}
		
		protected static readonly FileSearchComparer _comp = new FileSearchComparer();
		
		protected class FileSearchComparer : IComparer<Object> {
			
			public Int32 Compare(object x, object y) {
				
				FileInfo fx = x as FileInfo;
				String   sy = y as String;
				
				return String.Compare( fx.Name, sy, StringComparison.OrdinalIgnoreCase );
				
			}
		}
		
		public static CDImage CreateFromDirectory(DirectoryInfo root) {
			
			if( root.GetDirectory("AMD64").Exists ) return new NT5X64CDImage( root );
			
			if( root.GetDirectory("I386").Exists ) return new NT5X86CDImage( root );
			
			throw new NotSupportedException("Only NT5 CD Image formts are supported");
		}
		
	}
	
	public class CDImageOperatingSystem {
		
		public CDImageOperatingSystem(Int32 servicePack, OperatingSystem os, Boolean isX64, CultureInfo language) {
			
			ServicePack = servicePack;
			OSVersion   = os;
			IsX64       = isX64;
			Language    = language;
		}
		
		public Int32           ServicePack { get; private set; }
		
		public OperatingSystem OSVersion   { get; private set; }
		
		public Boolean         IsX64       { get; private set; }
		
		public CultureInfo     Language    { get; private set; }
		
	}
	
	public abstract class NT5CDImage : CDImage {
		
		private DirectoryInfo _oemRoot;
		private DirectoryInfo _oemWindows;
		
		protected NT5CDImage(DirectoryInfo root) : base(root) {
			
		}
		
//		\                       = $1
//		\Windows                = $OEM$\$$
//		\Documents and Settings = $Docs
//		\Program Files          = $Progs
		
		public override DirectoryInfo OemRoot {
			get {
				if( _oemRoot == null ) {
					
					_oemRoot = RootDirectory.GetDirectory("$OEM$");
					if( !_oemRoot.Exists ) _oemRoot.Create();
					
				}
				return _oemRoot;
			}
		}
		
		public override DirectoryInfo OemWindows {
			get {
				if( _oemWindows == null ) {
					
					_oemWindows = OemRoot.GetDirectory("$$");
					if( !_oemWindows.Exists ) _oemWindows.Create();
					
				}
				return _oemWindows;
			}
		}
		
		protected static FileInfo FindFile( FileInfo[] files, String fileName ) {
			
			if( files == null ) return null;
			
			Int32 idx = Array.BinarySearch( files, fileName, _comp );
			
			if( idx < 0 ) return null;
			
			return files[idx];
		}
		
		protected static void FindFiles(FileInfo[] files, String fileName, out FileInfo verbatim, out FileInfo compressed) {
			
			////////////////////////////
			// Try for the verbatim filename
			
			// ignore the path of the file since the I386 format is 'flat'
			String fn = Path.GetFileName( fileName );
			verbatim = FindFile( files, fn );
			
			////////////////////////////
			// Try for the compressed filename format
			
			fn = Path.GetFileName( fileName ).LeftFR(1) + '_';
			compressed  = FindFile( files, fn );
		}
		
		//////////////////////////////
		
		private CDImageOperatingSystem _os;
		
		public override CDImageOperatingSystem OperatingSystem {
			get {
				if( _os == null ) {
					
					// get the OS version from the version of Setup.exe
					// get the SP level from the precense of a file ".sp3" or ".sp2"
					// get the x64 info by the precense of AMD64
					// get the lang...how? I don't think it's possible; Setup.exe returns 1033 in all cases
					
					FileInfo setupExe = RootDirectory.GetFile("Setup.exe");
					if( !setupExe.Exists ) throw new FileNotFoundException("Setup.exe not found");
					
					Version version = PackageBase.GetFileVersion( setupExe.FullName );
					
					///////////////////////////////
					
					Int32 spLevel = GetSPLevel();
					
					OperatingSystem os = new OperatingSystem(PlatformID.Win32NT, version);
					
					Boolean isx64 = RootDirectory.GetDirectory("AMD64").Exists;
					
					//////////////////////////////
					
					_os = new CDImageOperatingSystem( spLevel, os, isx64, CultureInfo.InvariantCulture );
					
				}
				return _os;
			}
		}
		
		private Int32 GetSPLevel() {
			
			// SP4, never can be too sure!
			FileInfo[] spFiles = RootDirectory.GetFiles("*.sp4");
			if( spFiles.Length >= 1 ) return 4;
			
			spFiles = RootDirectory.GetFiles("*.sp3");
			if( spFiles.Length >= 1 ) return 3;
			
			spFiles = RootDirectory.GetFiles("*.sp2");
			if( spFiles.Length >= 1 ) return 2;
			
			spFiles = RootDirectory.GetFiles("*.sp1");
			if( spFiles.Length >= 1 ) return 1;
			
			return 0;
		}
		
	}
	
	public class NT5X86CDImage : NT5CDImage {
		
		private DirectoryInfo _i386;
		
		private FileInfo[] _i386Files;
		
		public NT5X86CDImage(DirectoryInfo root) : base(root) {
			
			_i386 = root.GetDirectory("I386");
			if( _i386.Exists ) {
				_i386Files = _i386.GetFiles();
				Array.Sort( _i386Files , (x,y) => String.Compare( x.Name, y.Name, StringComparison.OrdinalIgnoreCase ) );
			}
			
		}
		
		public override FileInfo[] GetFiles(String fileName, Boolean includeNonexistantFiles) {
			
			FileInfo fiVerbatim, fiCompressed;
			
			FindFiles( _i386Files, fileName, out fiVerbatim, out fiCompressed);
			
			if( !includeNonexistantFiles ) return BuildArray( fiVerbatim, fiCompressed );
			
			///////////////////////////////////////////
			
			fileName = Path.GetFileName( fileName );
			
			FileInfo hypotheticalI386File = _i386.GetFile( fileName );
			
			return BuildArray( fiVerbatim, fiCompressed, hypotheticalI386File );
		}
		
	}
	
	public class NT5X64CDImage : NT5CDImage {
		
		private DirectoryInfo _wi386;
		private DirectoryInfo _amd64;
		
		private FileInfo[] _wi386Files;
		private FileInfo[] _amd64Files;
		
		public NT5X64CDImage(DirectoryInfo root) : base(root) {
			
			_wi386 = root.GetDirectory("I386");
			if( _wi386.Exists ) {
				_wi386Files = _wi386.GetFiles();
				Array.Sort( _wi386Files , (x,y) => String.Compare( x.Name, y.Name, StringComparison.OrdinalIgnoreCase ) );
			}
			
			_amd64 = root.GetDirectory("AMD64");
			if( _amd64.Exists ) {
				_amd64Files = _amd64.GetFiles();
				Array.Sort( _amd64Files , (x,y) => String.Compare( x.Name, y.Name, StringComparison.OrdinalIgnoreCase ) );
			}
			
		}
		
		public override FileInfo[] GetFiles(String fileName, Boolean includeNonexistantFiles) {
			
			FileInfo amd64Verbatim, and64Compressed, wi386Verbatim, wi386Compressed;
			
			FindFiles( _amd64Files, fileName, out amd64Verbatim, out and64Compressed );
			
			FindFilesWI386( fileName, out wi386Verbatim, out wi386Compressed );
			
			if( !includeNonexistantFiles ) return BuildArray( amd64Verbatim, and64Compressed, wi386Verbatim, wi386Compressed );
			
			////////////////////////////////
			
			fileName = Path.GetFileName( fileName );
			
			FileInfo amd64Hypothetical = _amd64.GetFile( fileName );
			FileInfo wi386Hypothetical = _wi386.GetFile( fileName );
			
			return BuildArray( amd64Verbatim, and64Compressed, amd64Hypothetical, wi386Verbatim, wi386Compressed, wi386Hypothetical );
		}
		
		private void FindFilesWI386(String fileName, out FileInfo verbatim, out FileInfo compressed) {
			
			////////////////////////////
			// Try for the verbatim filename
			
			String fn = Path.GetFileName( fileName );
			verbatim = FindFile( _wi386Files, fn );
			
			////////////////////////////
			// Try for the compressed filename format
			// in WOW64 there is a 'W' prepended and it can exceed 8.3
			
			fn = "W" + Path.GetFileName( fileName ).LeftFR(1) + '_';
			compressed  = FindFile( _wi386Files, fn );
		}
		
	}
	
//	
//	public class NT6CDImage : CDImage {
//		
//	}
//	
}
