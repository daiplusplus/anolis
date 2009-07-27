using System;
using System.IO;
using System.Collections.Generic;
using Anolis.Core.Data;
using Anolis.Core.Native;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Source {
	
	public sealed class ResResourceSourceFactory : ResourceSourceFactory {
		
		public override ResourceSource Create(String fileName, Boolean isReadOnly, ResourceSourceLoadMode mode) {
			
			return new ResResourceSource(fileName, isReadOnly, mode);
		}
		
		protected override void CreateNew(String fileName) {
			
			using(FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write)) {
				
				// Write out the signature for a 32-bit *.res file
				
				Byte[] signature = new Byte[] { 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00 };
				
				fs.Write( signature );
				
			}
			
		}
		
		public override Compatibility HandlesExtension(String extension) {
			switch( extension.ToUpperInvariant() ) {
				case "RES":
				case "RCT":
					return Compatibility.Yes;
				default:
					return Compatibility.No;
			}
		}
		
		public override String OpenFileFilter {
			get {
				return CreateFileFilter("Compiled Resource Script", "res", "rct");
			}
		}
		
		public override String NewFileFilter {
			get {
				return CreateFileFilter("Compiled Resource Script", "res");
			}
		}
		
	}
	
	public sealed class ResResourceSource : FileResourceSource {
		
		private FileStream _stream;
		private List<ResResource> _resources;
		
		public ResResourceSource(String fileName, Boolean isReadOnly, ResourceSourceLoadMode mode) : base(fileName, isReadOnly, mode) {
			
			_resources = new List<ResResource>();
			
			Reload();
		}
		
		public override ResourceData GetResourceData(ResourceLang lang) {
			
			ResResource res = _resources.Find( r => r.Lang == lang );
			if(res == null) throw new ArgumentException("ResourceLang not found");
			
			_stream.Seek( res.DataOffset, SeekOrigin.Begin );
			
			Byte[] data = new Byte[ res.DataLength ];
			if( _stream.Read( data, 0, res.DataLength ) != res.DataLength ) {
				
				throw new AnolisException("Couldn't read all Resource Data");
			}
			
			return ResourceData.FromResource(lang, data);
			
		}
		
		protected override void Dispose(Boolean managed) {
			
			if(managed) {
				
				Unload();
			}
			
			base.Dispose(managed);
		}
		
		public override void Reload() {
			
			UnderlyingClear();
			
			if( _stream != null ) _stream.Close(); // consequtive calls to .Close() is safe
			_stream = new FileStream( FileInfo.FullName, FileMode.Open);
			if(!_stream.CanSeek) throw new AnolisException("RES FileStream must support seeking");
			
			_resources.Clear();
			
			// the RES format is quite simple, it's just a concatenated list of Resource Data instances, all with their header info
			
			_stream.Seek(0, SeekOrigin.Begin);
			
			BinaryReader rdr = new BinaryReader(_stream, System.Text.Encoding.Unicode);
			
			while(rdr.BaseStream.Position < rdr.BaseStream.Length) {
				
				///////////////////////////////
				// Read the RESOURCEHEADER
				
				Int64 start = rdr.BaseStream.Position;
				
				ResResourceHeader header = new ResResourceHeader( rdr );
				if(header.DataSize == 0 && rdr.BaseStream.Position == rdr.BaseStream.Length) break;
				
				Int64 stop  = rdr.BaseStream.Position;
				Int32 headerSize = (int)(stop - start);
				
				ResResource res = new ResResource();
				res.DataOffset = rdr.BaseStream.Position;
				res.DataLength = (int)header.DataSize; // HACK: this might cause problems for resources larger than 2GB
				res.HeaderOffset = start;
				res.HeaderLength = header.HeaderSize;
				
				// Read past the Resource data
				_stream.Seek( header.DataSize, SeekOrigin.Current );
				
				// don't do anything if it's empty
				if( !(header.Type is String )) {
					Int32 headerType = Convert.ToInt32( header.Type, Cult.InvariantCulture );
					if( headerType == 0 ) {
						rdr.Align4();
						continue;
					}
				}
				
				///////////////////////////////
				// Create ResourceType, Name, and Lang instance
				
				ResourceTypeIdentifier typeId = header.Type is String ? new ResourceTypeIdentifier( (String)header.Type ) : new ResourceTypeIdentifier( Convert.ToInt32( header.Type, Cult.InvariantCulture ) );
				
				ResourceType type = UnderlyingFind( t => t.Identifier.Equals( typeId ) );
				if(type == null) {
					
					type = new ResourceType( typeId, this );
					
					UnderlyingAdd( type );
				}
				
				///////////////////////////////////////////////////////////
				
				ResourceIdentifier nameId = header.Name is String ? new ResourceIdentifier( (String)header.Name ) : new ResourceIdentifier( Convert.ToInt32( header.Name, Cult.InvariantCulture ) );
				ResourceName name = UnderlyingFind(type, n => n.Identifier.Equals( nameId ) );
				if(name == null) {
					
					name = new ResourceName( nameId, type );
					
					UnderlyingAdd( type, name );
				}
				
				///////////////////////////////////////////////////////////
				
				// TODO: Maybe do some validation to ensure the same lang hasn't been added twice? 
				
				ResourceLang lang = new ResourceLang( header.LanguageId, name );
				
				UnderlyingAdd( name, lang );
				
				///////////////////////////////////////////////////////////
				
				res.Lang = lang;
				
				_resources.Add( res );
				
				rdr.Align4();
			}
			
		}
		
		private void Unload() {
			
			if( _stream != null ) _stream.Close();
			// closing a FileStream flushes itself
			
			_stream = null;
		}
		
		public override void CommitChanges() {
			
			// if the only operations to perform are 'Add' then just append them to the end of the file
			// but if other changes exist, it's easiest to create them from scratch
				// in which case create a new file, write out everything to there, then replace the original and reload
			
			if( !this.HasUnsavedChanges ) return;
			
			if( HasOnlyAddOperationsPending ) {
				
				CommitAddChanges();
				
				
			} else {
				
				CommitToNew();
			}
			
		}
		
		private void CommitAddChanges() {
			
			Unload();
			
			_stream = new FileStream( FileInfo.FullName, FileMode.Open, FileAccess.Write );
			
			_stream.Seek( 0, SeekOrigin.End ); // seek to end
			
			BinaryWriter wtr = new BinaryWriter( _stream, System.Text.Encoding.Unicode );
			
			wtr.Align4();
			
			foreach(ResourceLang lang in AllActiveLangs) {
				
				ResResourceHeader.WriteResource( lang, wtr );
			}
			
			_stream.Close();
			
			Reload();
			
		}
		
		private void CommitToNew() {
			
			// don't unload this instance since it needs to read from it first
			
			// NOTE: this method does not preserve custom ResourceHeader fields like Characteristics and MemoryFlags
			
			String tempFileName = Path.GetTempFileName();
			
			using(FileStream o = new FileStream( tempFileName, FileMode.Open, FileAccess.Write )) {
				
				BinaryWriter wtr = new BinaryWriter( o, System.Text.Encoding.Unicode );
				
				foreach(ResourceLang lang in this.AllLangs) {
					
					if( lang.Action == ResourceDataAction.Delete ) continue; // skip langs to delete, which means they'll get deleted
					
					// the actual data will be lazy-loaded on each write
					ResResourceHeader.WriteResource( lang, wtr );
					
				}
				
			}
			
			// close the current input stream
			Unload();
			
			// and overwrite the file
			FileInfo.Delete();
			File.Move( tempFileName, FileInfo.FullName );
			
			Reload();
		}
		
		private Boolean HasOnlyAddOperationsPending {
			get {
				foreach(ResourceLang lang in AllActiveLangs) if( lang.Action != ResourceDataAction.Add ) return false;
				return true;
			}
		}
		
		private class ResResource {
			
			public Int64                  HeaderOffset { get; set; }
			public Int64                  HeaderLength { get; set; }
			public Int64                  DataOffset   { get; set; }
			public Int32                  DataLength   { get; set; }
			
			public ResourceLang           Lang   { get; set; }
		}
	}
	
}
