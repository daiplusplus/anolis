using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;

using P = System.IO.Path;
using Env = Anolis.Core.Utility.Environment;
using W3b.TarLzma;

using Anolis.Core.Utility;
using Anolis.Core.Utility.BinaryPatch;

namespace Anolis.Core.Packages.Operations {
	
	public class UXThemeOperation : Operation {
		
		public UXThemeOperation(Package package, Group parent, XmlElement element) :  base(package, parent, element) {
		}
		
		public UXThemeOperation(Package package, Group parent) :  base(package, parent, (String)null) {
		}
		
		public override Boolean Merge(Operation operation) {
			throw new PackageException("There can only be one active UXThemeOperation in a package");
		}
		
		public override String OperationName {
			get { return "UxTheme"; }
		}
		
		public override Boolean SupportsI386 {
			// TODO: Support I386 patching of uxtheme
			get { return false; }
		}
		
		public override void Execute() {
			
			PatchFile( PackageUtility.ResolvePath(@"%windir%\System32\uxtheme.dll") );
			PatchFile( PackageUtility.ResolvePath(@"%windir%\SysWow64\uxtheme.dll") );
			
		}
		
		private void PatchFile(String fileName) {
			
			if( !File.Exists( fileName ) ) {
				
				Package.Log.Add(LogSeverity.Error, "UxTheme file not found: " + fileName);
				return;
				
			}
			
			// make a copy then patch that, then add it to PFRO
			
			String workOnThis = fileName += ".anofp";
			
			if( File.Exists( workOnThis ) ) Package.Log.Add(LogSeverity.Warning, "Overwritten *.anofp: " + workOnThis); 
			File.Copy( fileName, workOnThis, true );
			
			// begin patching
			
			PatchFinder finderSystem32 = UXThemePatchFinderFactory.Create( workOnThis );
			Patch patch = finderSystem32.GetPatchStatus();
			
			if( patch.CanPatch ) {
				
				patch.ApplyPatch();
				
				Miscellaneous.CorrectPEChecksum( workOnThis );
				
				PackageUtility.AddPfroEntry( workOnThis, fileName );
				
				////////////////////////
				// Make backup
				
				if( Package.ExecutionInfo.BackupGroup != null ) {
				
					String hash = PackageUtility.GetMD5Hash( workOnThis );
					Backup( Package.ExecutionInfo.BackupGroup, fileName, hash );
					
				}
				
			} else {
				
				Package.Log.Add( LogSeverity.Warning, "Did not UxTheme Patch: " +  fileName + ", deleted working file" );
				File.Delete( workOnThis );
				
			}
			
		}
		
		private void Backup(Group backupGroup, String originalFileName, String patchedHash) {
			
			DirectoryInfo backupDir = backupGroup.Package.RootDirectory;
			
			String backupToThis = P.Combine( backupDir.FullName, P.GetFileName( originalFileName ) );
			backupToThis = PackageUtility.GetUnusedFileName( backupToThis );
			
			File.Copy( originalFileName, backupToThis );
			
			FileOperation op = new FileOperation(backupGroup.Package, backupGroup, backupToThis, originalFileName, FileOperationType.Copy);
			op.ConditionHash = patchedHash;
			backupGroup.Operations.Add( op );
			
		}
		
		public override void Write(XmlElement parent) {
			CreateElement( parent, "uxtheme" );
		}
		
	}
}
