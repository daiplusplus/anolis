using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Xml;

using Anolis.Packages.Utility;

using P = System.IO.Path;

namespace Anolis.Packages.Operations {
	
	public abstract class PatchOperation : PathOperation {
		
		protected PatchOperation(Group parent, XmlElement operationElement) : base(parent, operationElement) {
			
			ConditionHash = operationElement.GetAttribute("conditionHash");
			I386Path      = operationElement.GetAttribute("i386path");
		}
		
		protected PatchOperation(Group parent, String path) : base(parent, path) {
		}
		
		public String  ConditionHash { get; private set; }
		public String  I386Path      { get; set; }
		
		protected virtual void Backup(Group backupGroup, String originalFileName, String postPatchedFileName) {
			
			String patchedHash = PackageUtility.GetMD5Hash( postPatchedFileName );
			
			DirectoryInfo backupDir = backupGroup.Package.RootDirectory;
			
			String backupToThis = P.Combine( backupDir.FullName, P.GetFileName( originalFileName ) );
			backupToThis = PackageUtility.GetUnusedFileName( backupToThis );
			
			File.Copy( originalFileName, backupToThis );
			
			FileOperation op = new FileOperation( backupGroup, backupToThis, originalFileName, FileOperationType.Copy);
			op.ConditionHash = patchedHash;
			backupGroup.Operations.Add( op );
			
		}
		
		public override void Execute() {
			
			String fileName = Path;
			
			if( Package.ExecutionInfo.ExecutionMode == PackageExecutionMode.Regular ) {
				
				// check to see if the equivalent file exists in 32-bit Program Files or SysWow64
				String sysWow64Path = PackageUtility.GetSysWow64File( fileName );
				if( sysWow64Path != null ) {
					
					ExecuteRegularFile( sysWow64Path );
				}
				
				ExecuteRegularFile( fileName );
				
			} else if( Package.ExecutionInfo.ExecutionMode == PackageExecutionMode.CDImage ) {
				
				if( !String.IsNullOrEmpty( I386Path ) ) fileName = I386Path;
				
				ExecuteCDImageFile( fileName );
			}
			
		}
		
		/// <summary>Creates an *.anofp copy, Patches it, and adds an entry to PFRO</summary>
		private void ExecuteRegularFile(String fileName) {
			
			if( !File.Exists( fileName ) ) {
				
				Package.Log.Add( new LogItem(LogSeverity.Error, "Source file not found: " + fileName) );
				return;
			}
			
			if( System.Environment.OSVersion.Version.Major == 5 ) {
				
				ExecuteRegularFileNT5( fileName );
				
			} else if( System.Environment.OSVersion.Version.Major == 6 ) {
				
				ExecuteRegularFileNT6( fileName );
				
			}
			
		}
		
		private void ExecuteRegularFileNT5(String fileName) {
			
			// copy the file first
			// there used to be support for the SaveTo attribute, but I removed it in the current version of the schema
			String workOnThis = fileName + ".anofp"; // "Anolis File Pending"
			
			if(File.Exists( workOnThis )) Package.Log.Add( LogSeverity.Warning, "Overwritten *.anofp: " + workOnThis);
			File.Copy( fileName, workOnThis, true );
			
			if( PatchFile( workOnThis ) ) {
				
				if( Package.ExecutionInfo.MakeBackup ) {
					
					Backup( Package.ExecutionInfo.BackupGroup, fileName, workOnThis );
				}
				
				// if it throws, this won't be encountered
				PackageUtility.AddPfroEntry( workOnThis, fileName );
				
				Package.ExecutionInfo.RequiresRestart = true;
				
#if !DEBUG
				Package.Log.Add( new LogItem(LogSeverity.Info, "Patched: " + fileName) );
#endif
			} else {
				
				Package.Log.Add( new LogItem(LogSeverity.Error, "Didn't patch: " + fileName + ", deleted " + workOnThis) );
				File.Delete( workOnThis );
				
			}
			
		}
		
		private void ExecuteRegularFileNT6(String fileName) {
			
			// it's the shame the more elegant NT5 system doesn't work on NT6
			// ah well
			
			String workOnThis = fileName + ".anorp"; // "Anolis Rename Pending"
			
			if( File.Exists( workOnThis ) ) Package.Log.Add( LogSeverity.Warning, "Overwritten *.anorp: " + workOnThis);
			File.Copy( fileName, workOnThis, true );
			
			// problem: the file replacement system is working fine... the resources just aren't being replaced in the anorp file, wtf
			
			if( PatchFile( workOnThis ) ) {
				
				// Take Control of the original file
				
				PackageUtility.TakeOwnershipAndFullControl( fileName );
				
				String holdingName = fileName + ".anodp"; // "Anolis Delete Pending"
				
				if( File.Exists( holdingName ) ) {
					File.Delete( holdingName );
					Package.Log.Add( LogSeverity.Warning, "Deleted anodp file: " + holdingName );
				}
				
				File.Move( fileName, holdingName ); // apparently you can do this for files currently in use
				
				File.Move( workOnThis, fileName );
				
				PackageUtility.AddPfroEntry( holdingName, null ); // delete the *.anodp file on reboot
				Package.ExecutionInfo.RequiresRestart = true;
				
				PackageUtility.ResetOwnershipAndRelinquishControl( fileName );
				
#if !DEBUG
				Package.Log.Add( new LogItem(LogSeverity.Info, "Patched: " + fileName) );
#endif
				
			} else {
				
				Package.Log.Add( new LogItem(LogSeverity.Error, "Didn't patch: " + fileName + ", deleted " + workOnThis) );
				File.Delete( workOnThis );
				
			}
			
		}
		
		private void ExecuteCDImageFile(String fileName) {
			
			NT5CDImage nt5CD = Package.ExecutionInfo.CDImage as NT5CDImage;
			if( nt5CD == null ) {
				Package.Log.Add(LogSeverity.Error, "PatchOperation only supports the Windows NT5.x CD format");
				return;
			}
			
			FileInfo[] fileNames = nt5CD.GetFiles( fileName );
			
			if( fileNames.Length == 0 ) {
				Package.Log.Add(LogSeverity.Warning, "No filename matches found for " + fileName);
			}
			
			foreach(FileInfo file in fileNames) {
				
				String workingPath;
				Boolean isCompressed;
				CDImagePrepare( file.FullName, out workingPath, out isCompressed );
				if( workingPath == null ) continue;
				
				if( !File.Exists( workingPath ) ) {
					Package.Log.Add( LogSeverity.Error, "Didn't expand file:" + file.FullName + " > " + workingPath );
					continue;
				}
				
				Boolean patchOK = PatchFile( workingPath );
				
				if( patchOK ) {
					
					if( isCompressed ) CDImageAftermath( workingPath, file.FullName );
						
				} else {
					
					Package.Log.Add(LogSeverity.Warning, "Didn't patch file: " + fileName + " > " + file.FullName + " > " + workingPath  );
				}
				
			}
			
		}
		
#region CD Image Expand/Compress
		
		/// <param name="path">The path to the file within the CD Image</param>
		/// <param name="workingFilePath">The path to the file to perform the patch operation on (located under the Temp directory)</param>
		private void CDImagePrepare(String path, out String workingFilePath, out Boolean isCompressed) {
			
			String destTempDir =  P.Combine( P.GetTempPath(), @"Anolis\I386");
			if( !Directory.Exists( destTempDir ) ) Directory.CreateDirectory( destTempDir );
			
			// Note in future I'll need to deal with the NT6 setup files format which is different
			
			if( path.EndsWith("_", StringComparison.OrdinalIgnoreCase) ) { // then it's compressed
				
				isCompressed = true;
				
				// expand it
				
				String destFile = PackageUtility.GetUnusedFileName( P.Combine(destTempDir, P.GetFileName( path ) ) );
				
				// don't use the -r switch if specifying an output filename
				String args = String.Format(CultureInfo.InvariantCulture, @"""{0}"" ""{1}""", path, destFile);
				
				ProcessStartInfo procStart = new ProcessStartInfo("expand", args);
				procStart.WindowStyle = ProcessWindowStyle.Hidden;
				procStart.CreateNoWindow = true;
				
				Process p = Process.Start( procStart );
				
				if( !p.Start() ) {
					Package.Log.Add( LogSeverity.Error, "Couldn't start expand process");
					
					workingFilePath = null;
					return;
				}
				
				if( !p.WaitForExit( 5000 ) ) {
					Package.Log.Add( LogSeverity.Error, "expand took longer than 5000ms");
					
					workingFilePath = null;
					return;
				}
				
				if( !File.Exists( destFile ) ) {
					Package.Log.Add( LogSeverity.Error, "File expansion failed");
					workingFilePath = null;
					return;
				}
				
				workingFilePath = destFile;
				
			} else {
				// uncompressed file
				
				workingFilePath = path;
				isCompressed    = false;
				
			}
			
		}
		
		private void CDImageAftermath(String expandedPath, String originalPath) {
			
			// compress and overwrite
			
			Thread thread = new Thread( new ParameterizedThreadStart( CDImageAftermathThreadStart ) );
			thread.Start( new Object[] { expandedPath, originalPath } );
		}
		
		private void CDImageAftermathThreadStart(Object arg) {
			
			Object[] args = arg as Object[];
			
			String expandedPath = (String)args[0];
			String originalPath = (String)args[1];
			
			/////////////////////////
			
			String makecabArgs = String.Format(CultureInfo.InvariantCulture, @"/D CompressionType=LZX /D CompressionMemory=21 ""{0}"" ""{1}""", expandedPath, originalPath);
			
			ProcessStartInfo procStart = new ProcessStartInfo("makecab", makecabArgs);
			procStart.WindowStyle = ProcessWindowStyle.Hidden;
			procStart.CreateNoWindow = true;
			
			Process p = Process.Start( procStart );
			
			if( !p.Start() ) {
				
				Package.Log.Add( LogSeverity.Error, "Couldn't start makecab");
				return;
			}
			
			try {
				p.WaitForExit();
			} catch(Win32Exception wex) {
				Package.Log.Add( LogSeverity.Error, "WaitForExit: " + wex.Message);
				return;
			}
			
			Thread.Sleep(250); // 250ms pause for any locks on the file to expire
			
			try {
				// delete the expanded file
				File.Delete( expandedPath );
			} catch(IOException iex) {
				Package.Log.Add( LogSeverity.Error, "File.Delete: " + iex.Message);
				return;
			}
			
		}
		
#endregion
		
		protected abstract Boolean PatchFile(String fileName);
		
	}
}
