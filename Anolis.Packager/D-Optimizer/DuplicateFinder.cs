using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;

using Anolis.Core.Utility;
using Anolis.Packages.Utility;

namespace Anolis.Packager {
	
	public class DuplicateFinder {
		
		private List<DuplicateFile> _duplicates;
		private ReadOnlyCollection<DuplicateFile> _duplicatesPublic;
		private DirectoryInfo       _root;
		private DirectoryInfo[]     _exclude;
		
		private DuplicateFileIndex  _reverse;
		
		public DuplicateFinder(DirectoryInfo root, DirectoryInfo[] exclude) {
			
			if( root == null ) throw new ArgumentNullException("root");
			if( !root.Exists ) throw new DirectoryNotFoundException("root directory not found");
			_root          = root;
			
			if( exclude == null ) _exclude = new DirectoryInfo[0];
			else                  _exclude = exclude;
		}
		
#region Status
		
		public event EventHandler StatusUpdated;
		
		private void OnStatusUpdated() {
			
			if( StatusUpdated != null ) {
				StatusUpdated( this, EventArgs.Empty );
			}
			
		}
		
		public String StatusMessage    { get; private set; }
		public Int32  StatusPercentage { get; private set; }
		
		public Boolean IsBusy          { get; private set; }
		public Boolean SearchCompleted { get; private set; }
		
#endregion
		
#region Search
		
		public ReadOnlyCollection<DuplicateFile> DuplicateFiles {
			get {
				if( _duplicatesPublic == null ) _duplicatesPublic = new ReadOnlyCollection<DuplicateFile>( _duplicates );
				return _duplicatesPublic;
			}
		}
		
		public void Search() {
			
			if( IsBusy ) throw new InvalidOperationException("Already performing an operation");
			IsBusy = true;
			
			/////////////////////////////////
			// Step 1 - Enumerate files
			
			StatusPercentage = -1;
			StatusMessage       = "Enumerating Files";
			OnStatusUpdated();
			
			List<FileInfo> allFiles = new List<FileInfo>();
			EnumerateFiles( _root, allFiles );
			
			Dictionary<String,List<FileInfo>> hashesToFiles = new Dictionary<String,List<FileInfo>>();;
			
			/////////////////////////////////
			// Step 2 - Compute Hashes
			
			for(int i=0;i<allFiles.Count;i++) {
				FileInfo file = allFiles[i];
				
				/////////////////////////
				StatusPercentage = Convert.ToInt32( (float)(100 * i) / (float)allFiles.Count );
				StatusMessage       = "Hashing: " + file.FullName;
				OnStatusUpdated();
				/////////////////////////
				
				String hash = PackageUtility.GetMD5Hash( file.FullName );
				
				List<FileInfo> duplicates;
				if( hashesToFiles.TryGetValue( hash, out duplicates ) ) {
					
					duplicates.Add( file );
					
				} else {
					
					duplicates = new List<FileInfo>();
					duplicates.Add( file );
					
					hashesToFiles.Add( hash, duplicates );
				}
				
			}
			
			/////////////////////////////////
			// Step 3 - Collate Results
			
			StatusPercentage = -1;
			StatusMessage       = "Collating Results";
			OnStatusUpdated();
			
			if( _duplicates == null ) _duplicates = new List<DuplicateFile>();
			else _duplicates.Clear();
			
			foreach(String hash in hashesToFiles.Keys) {
				
				List<FileInfo> dupes = hashesToFiles[hash];
				if( dupes.Count < 2 ) continue;
				
				DuplicateFile d = new DuplicateFile( dupes, hash );
				_duplicates.Add( d );
				
			}
			
			//////////////////////////
			// Step 4 - Build Reverse Lookup Table
			
			StatusPercentage = -1;
			StatusMessage    = "Building Reverse Lookup Table";
			
			_reverse = new DuplicateFileIndex( _duplicates, _root );
			
			//////////////////////////
			// Step 5 Done
			
			StatusPercentage = 100;
			StatusMessage    = "Complete";
			
			IsBusy = false;
			SearchCompleted = true;
			
			OnStatusUpdated();
		}
		
		private void EnumerateFiles(DirectoryInfo directory, List<FileInfo> files) {
			
			if( IsDirectoryExcluded( directory ) ) return;
			
			files.AddRange( directory.GetFiles() );
			
			foreach(DirectoryInfo child in directory.GetDirectories()) {
				
				EnumerateFiles( child, files );
			}
			
		}
		
		private Boolean IsDirectoryExcluded(DirectoryInfo dir) {
			
			foreach(DirectoryInfo excludedDir in _exclude) {
				
				if( dir.FullName.StartsWith( excludedDir.FullName, StringComparison.OrdinalIgnoreCase ) ) return true;
			}
			
			return false;
		}
		
#endregion
		
#region Separate
		
		/// <summary>Removes duplicate files from the directory and recreates their FS structure in the specified directory (so they can be merged back harmlessly later)</summary>
		public void SeparateOutDuplicateFiles(DirectoryInfo destination) {
			
			if( IsBusy ) throw new InvalidOperationException("Already performing an operation");
			if( !SearchCompleted ) throw new InvalidOperationException("Duplicate files must be processed first");
			
			IsBusy = true;
			
			Single count = _duplicates.Count;
			Single soFar = 1;
			
			foreach(DuplicateFile dupe in _duplicates) {
				
				// keep file 1
				
				for(int i=1;i<dupe.Matches.Count;i++) {
					
					MoveFile( dupe.Matches[i], _root, destination );
				}
				
				StatusMessage    = dupe.Matches[0].FullName;
				StatusPercentage = (Int32)((100f * soFar++) / count);
				OnStatusUpdated();
			}
			
			IsBusy = false;
			
		}
		
		private static void MoveFile(FileInfo currentFile, DirectoryInfo currentRoot, DirectoryInfo destinationRoot) {
			
			// get the path relative to the current root
			// then get the new path by appending that to the destination
			// then move (and create directories as necessary)
			
			if( !currentFile.FullName.StartsWith( currentRoot.FullName, StringComparison.OrdinalIgnoreCase ) ) throw new ArgumentException("currentFile is not a child of currentRoot");
			
			String relativePath = currentFile.FullName.Substring( currentRoot.FullName.Length );
			if( relativePath.StartsWith("/", StringComparison.OrdinalIgnoreCase ) || relativePath.StartsWith("\\", StringComparison.OrdinalIgnoreCase) ) relativePath = relativePath.Substring(1);
			
			String destinationPath = Path.Combine( destinationRoot.FullName, relativePath );
			
			String destinationDirectory = Path.GetDirectoryName( destinationPath );
			if( !Directory.Exists( destinationDirectory ) ) Directory.CreateDirectory( destinationDirectory );
			
			currentFile.MoveTo( destinationPath );
			
		}
		
#endregion
		
#region Process Package
		
		/// <summary>Changes all references to duplicate files to the first occurence of each file</summary>
		public void ProcessPackage(XmlDocument document) {
			
			ProcessElement( document.DocumentElement );
		}
		
		private void ProcessElement(XmlElement element) {
			
			foreach(String attributeName in PackageOptimizer.FileAttributes) {
				
				if( attributeName == "path" && element.Name == "file" ) continue;
				
				String attributeValue = element.GetAttribute(attributeName);
				if( attributeValue.Length == 0 ) continue;
				
				if( attributeValue.StartsWith("comp:", StringComparison.OrdinalIgnoreCase) ) {
					
					String nuValue = ProcessCompositedImage( attributeValue );
					if( nuValue != null ) element.SetAttribute( attributeName, nuValue );
					
				} else {
					
					String newRelativeFn = _reverse.FileIsDuplicate( attributeValue );
					if( newRelativeFn != null ) {
						
						element.SetAttribute( attributeName, newRelativeFn );
					}
					
				}
				
			}
			
			foreach(XmlNode child in element.ChildNodes) {
				
				XmlElement childElement = child as XmlElement;
				if(childElement != null)
					ProcessElement( childElement );
				
			}
			
		}
		
		
		
		private String ProcessCompositedImage(String compExpr) {
			
			Boolean isUpdated = false;
			
			CompositedImage img = new CompositedImage( compExpr, _root );
			foreach(Layer layer in img.Layers) {
				
				String nonDupeFn = _reverse.FileIsDuplicate( layer.ImageRelativeFileName );
				if( nonDupeFn != null ) {
					
					layer.ImageRelativeFileName = nonDupeFn;
					isUpdated = true;
				}
				
			}
			
			return isUpdated ? img.ToString() : null;
		}
		
#endregion
		
	}
	
	
	public class DuplicateFile {
		
		public DuplicateFile(IList<FileInfo> matchingFiles, String hash) {
			
			Matches = new ReadOnlyCollection<FileInfo>(matchingFiles);
			Hash    = hash;
		}
		
		public ReadOnlyCollection<FileInfo> Matches { get; private set; }
		
		public String                       Hash    { get; private set; }
		
	}
	
	public class DuplicateFileIndex {
		
		private IEnumerable<DuplicateFile>       _duplicateFileSets;
		private Dictionary<String,DuplicateFile> _reverseLookup;
		private DirectoryInfo                    _root;
		
		public DuplicateFileIndex( IEnumerable<DuplicateFile> duplicateFileSets, DirectoryInfo root ) {
			
			_root = root;
			_duplicateFileSets = duplicateFileSets;
			
			_reverseLookup = new Dictionary<String,DuplicateFile>();
			
			foreach(DuplicateFile duplicateFileSet in duplicateFileSets) {
				foreach(FileInfo file in duplicateFileSet.Matches) {
					
					String relative = file.FullName.Substring( root.FullName.Length ).ToLowerInvariant();
					if( relative.StartsWith("\\") || relative.StartsWith("/") ) relative = relative.Substring( 1 );
					
					_reverseLookup.Add( relative, duplicateFileSet );
				}
				
			}
			
		}
		
		public String FileIsDuplicate(String relativeFn) {
			
			relativeFn = relativeFn.ToLowerInvariant();
			
			DuplicateFile ret;
			if( _reverseLookup.TryGetValue( relativeFn, out ret ) ) {
				
				// return the relative filename of the 'keeper' match
				
				return GetRelativeFileName( ret.Matches[0], _root );
			}
			
			return null;
		}
		
		private static String GetRelativeFileName(FileInfo file, DirectoryInfo root) {
			
			if( !file.FullName.StartsWith( root.FullName, StringComparison.OrdinalIgnoreCase ) ) throw new ArgumentException("file is not a child of root");
			
			String name = file.FullName.Substring( root.FullName.Length );
			if( name.StartsWith("\\") || name.StartsWith("/") ) name = name.Substring(1);
			
			return name;
		}
		
	}
	
}
