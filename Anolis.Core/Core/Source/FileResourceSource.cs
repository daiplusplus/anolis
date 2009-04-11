using System;
using System.IO;
using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Source {
	
	public abstract class FileResourceSource : ResourceSource {
		
		private ResourceSourceInfo _sourceInfo;
		
		protected FileResourceSource(String filename, Boolean isReadOnly, ResourceSourceLoadMode mode) : base(  isReadOnly || IsPathReadonly(filename) , mode) {
			
			FileInfo = new FileInfo(filename);
			if(!FileInfo.Exists) throw new FileNotFoundException("", filename);
			
		}
		
		private static Boolean IsPathReadonly(String filename) {
			 
			return ( File.GetAttributes(filename) & FileAttributes.ReadOnly ) == FileAttributes.ReadOnly;
		}
		
		public FileInfo FileInfo { get; protected set; }
		
		public override String Name {
			get { return Path.GetFileName( FileInfo.FullName ); }
		}
		
		public override ResourceSourceInfo SourceInfo {
			get {
				if(_sourceInfo == null) GetSourceInfo();
				return _sourceInfo;
			}
		}
		
		protected void GetSourceInfo() {
			
			_sourceInfo = new ResourceSourceInfo();
			_sourceInfo.Add("Size"    , FileInfo.Length        .ToString(Cult.InvariantCulture));
			_sourceInfo.Add("Accessed", FileInfo.LastAccessTime.ToString(Cult.InvariantCulture));
			_sourceInfo.Add("Modified", FileInfo.LastWriteTime .ToString(Cult.InvariantCulture));
			_sourceInfo.Add("Created" , FileInfo.CreationTime  .ToString(Cult.InvariantCulture));
			
//			using(FileStream stream = FileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read)) {
//				
//				BinaryReader br = new BinaryReader(stream);
//				
//				// I CBA to work out how to read a COFF/PE header right now
//				
//			}
//			
//			_sourceInfo = null;
			
		}
		
	}
	
}
