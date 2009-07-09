using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;

using Anolis.Core.Packages;

namespace Anolis.Tools.DuplicateFiles {
	
	public class DuplicateFinder {
		
		private List<FileInfo>  _files;
		private DirectoryInfo   _root;
		private DirectoryInfo[] _exclude;
		
		private Dictionary<String,List<FileInfo>> _hashesToFiles;
		
		public DuplicateFinder(DirectoryInfo root, DirectoryInfo[] exclude) {
			
			if( root == null ) throw new ArgumentNullException("root");
			
			if( exclude == null ) _exclude = new DirectoryInfo[0];
			
			_files         = new List<FileInfo>();
			_hashesToFiles = new Dictionary<String,List<FileInfo>>();
			_root          = root;
			_exclude       = exclude;
		}
		
		public String StatusMessage       { get; private set; }
		public Int32  PercentageCompleted { get; private set; }
		
		public Boolean IsBusy             { get; private set; }
		
		public event EventHandler StatusUpdated;
		
		private void OnStatusUpdated() {
			
			if( StatusUpdated != null ) {
				StatusUpdated( this, EventArgs.Empty );
			}
			
		}
		
		public DuplicateFile[] Process() {
			
			if( IsBusy ) throw new InvalidOperationException("Already performing an operation");
			IsBusy = true;
			
			/////////////////////////////////
			// Step 1 - Enumerate files
			
			PercentageCompleted = -1;
			StatusMessage       = "Enumerating Files";
			OnStatusUpdated();
			
			EnumerateFiles( _root );
			
			/////////////////////////////////
			// Step 2 - Compute Hashes
			
			for(int i=0;i<_files.Count;i++) {
				FileInfo file = _files[i];
				
				/////////////////////////
				PercentageCompleted = Convert.ToInt32( (float)(100 * i) / (float)_files.Count );
				StatusMessage       = "Hashing: " + file.FullName;
				OnStatusUpdated();
				/////////////////////////
				
				String hash = PackageUtility.GetMD5Hash( file.FullName );
				
				List<FileInfo> duplicates;
				if( _hashesToFiles.TryGetValue( hash, out duplicates ) ) {
					
					duplicates.Add( file );
					
				} else {
					
					duplicates = new List<FileInfo>();
					duplicates.Add( file );
					
					_hashesToFiles.Add( hash, duplicates );
				}
				
			}
			
			/////////////////////////////////
			// Step 3 - Collate Results
			
			PercentageCompleted = -1;
			StatusMessage       = "Collating Results";
			OnStatusUpdated();
			
			List<DuplicateFile> ret = new List<DuplicateFile>();
			
			foreach(String hash in _hashesToFiles.Keys) {
				
				List<FileInfo> dupes = _hashesToFiles[hash];
				if( dupes.Count <= 1 ) continue;
				
				DuplicateFile d = new DuplicateFile( dupes, hash );
				ret.Add( d );
				
			}
			
			PercentageCompleted = 100;
			StatusMessage       = "Complete";
			OnStatusUpdated();
			
			IsBusy = false;
			
			return ret.ToArray();
		}
		
		private void EnumerateFiles(DirectoryInfo directory) {
			
			if( IsDirectoryExcluded( directory ) ) return;
			
			FileInfo[] files = directory.GetFiles();
			_files.AddRange( files );
			
			foreach(DirectoryInfo child in directory.GetDirectories()) {
				
				EnumerateFiles( child );
			}
			
		}
		
		private Boolean IsDirectoryExcluded(DirectoryInfo dir) {
			
			foreach(DirectoryInfo excludedDir in _exclude) {
				
				if( dir.FullName.StartsWith( excludedDir.FullName, StringComparison.OrdinalIgnoreCase ) ) return true;
			}
			
			return false;
		}
		
		public void Cancel() {
			
		}
		
	}
	
	public class DuplicateFile {
		
		public DuplicateFile(IList<FileInfo> matchingFiles, String hash) {
			
			Matches = new ReadOnlyCollection<FileInfo>(matchingFiles);
			Hash    = hash;
		}
		
		public ReadOnlyCollection<FileInfo> Matches { get; private set; }
		
		public String                       Hash    { get; private set; }
		
	}
	
}
