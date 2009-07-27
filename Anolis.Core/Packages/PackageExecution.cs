using System;
using System.IO;

namespace Anolis.Core.Packages {
	
	public class PackageExecutionSettings {
		
		public PackageExecutionMode ExecutionMode            { get; set; }
		
		public DirectoryInfo        BackupDirectory          { get; set; }
		
		public Boolean              CreateSystemRestorePoint { get; set; }
		
		public DirectoryInfo        I386Directory            { get; set; }
		
	}
	
	/// <summary>A read-only version of PackageExecutionSettings for passing to operations and consumers of Package</summary>
	public class PackageExecutionSettingsInfo {
		
		internal PackageExecutionSettingsInfo(Package package, PackageExecutionMode mode, Boolean createSysRes, Group backupGroup, DirectoryInfo cdImageDirectory) {
			
			ExecutionMode            = mode;
			CreateSystemRestorePoint = createSysRes;
			BackupGroup              = backupGroup;
			
			if( cdImageDirectory != null ) {
				CDImage              = CDImage.CreateFromDirectory( cdImageDirectory );
			}
			
			ApplyToDefault           = true;
		}
		
		public Package              Package                  { get; private set; }
		
		public PackageExecutionMode ExecutionMode            { get; private set; }
		
		public Boolean              CreateSystemRestorePoint { get; private set; }
		
		public Boolean              RequiresRestart          { get; set; }
		
		public Boolean              ApplyToDefault           { get; set; }
		
		public CDImage              CDImage                  { get; set; }
		
#region Backup
		
		public Group BackupGroup {
			get; private set;
		}
		
		public Package BackupPackage {
			get { return BackupGroup == null ? null : BackupGroup.Package; }
		}
		
		public DirectoryInfo BackupDirectory {
			get { return BackupGroup == null ? null : BackupPackage.RootDirectory; }
		}
		
		public Boolean MakeBackup {
			get { return BackupGroup != null; }
		}
		
#endregion
		
	}
	
	public enum PackageExecutionMode {
		Regular,
		CDImage
	}
	
}
