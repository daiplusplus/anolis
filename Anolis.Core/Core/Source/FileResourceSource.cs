using System;
using System.IO;
using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Source {
	
	public abstract class FileResourceSource : ResourceSource {
		
		private ResourceSourceInfo _sourceInfo;
		
		protected FileResourceSource(String fileName, Boolean isReadOnly, ResourceSourceLoadMode mode) : base(  isReadOnly || IsPathReadonly(fileName) , mode) {
			
			FileInfo = new FileInfo(fileName);
			if(!FileInfo.Exists) throw new FileNotFoundException("", fileName);
			
		}
		
		private static Boolean IsPathReadonly(String fileName) {
			 
			return ( File.GetAttributes(fileName) & FileAttributes.ReadOnly ) == FileAttributes.ReadOnly;
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
