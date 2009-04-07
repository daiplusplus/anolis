using System;
using System.IO;
using System.Collections.Generic;
using Anolis.Core.Data;

namespace Anolis.Core.Source {
	
	public class ResResourceSource : ResourceSource {
		
		private FileInfo _file;
		
		public ResResourceSource(String filename, Boolean isReadOnly, ResourceSourceLoadMode mode) : base(isReadOnly, mode) {
			
			_file = new FileInfo(filename);
			if(!_file.Exists) throw new FileNotFoundException("RES Resource Source not found", filename);
			
			Reload();
		}
		
		public override string Name {
			get { throw new NotImplementedException(); }
		}
		
		public override ResourceData GetResourceData(ResourceLang lang) {
			throw new NotImplementedException();
		}
		
		public override void CommitChanges() {
			throw new NotImplementedException();
		}
		
		public override void Reload() {
			
			// the RES format is quite simple, it's just a concatenated list of Resource Data instances, all with their header info
			
			using(FileStream stream = _file.OpenRead())
			using(BinaryReader rdr = new BinaryReader(stream)) {
				
				while(rdr.BaseStream.Position < rdr.BaseStream.Length) {
					
					///////////////////////////////
					// Read the RESOURCEHEADER
					
					///////////////////////////////
					// Read the Resource itself
					
					rdr.Align4();
				}
				
			}
			
		}
		
		private class ResResource {
			
			public Int64                  Offset { get; private set; }
			
			public ResourceTypeIdentifier TypeId { get; private set; }
			public ResourceIdentifier     NameId { get; private set; }
			public UInt16                 LangId { get; private set; }
		}
	}
	
}
