using System;
using System.IO;
using System.Collections.Generic;
using Anolis.Core.Data;
using Anolis.Core.Native;

namespace Anolis.Core.Source {
	
	public class ResResourceSource : FileResourceSource {
		
		private FileStream _stream;
		private List<ResResource> _resources;
		
		public ResResourceSource(String filename, Boolean isReadOnly, ResourceSourceLoadMode mode) : base(filename, true, mode) {
			
			_stream = new FileStream(filename, FileMode.Open);
			if(!_stream.CanSeek) throw new AnolisException("Res FileStream must support seeking");
			
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
		
		public override void CommitChanges() {
			throw new NotSupportedException();
		}
		
		protected override void Dispose(Boolean managed) {
			
			if(managed) {
				
				_stream.Dispose();
			}
			
			base.Dispose(managed);
		}
		
		public override void Reload() {
			
			_resources.Clear();
			
			// the RES format is quite simple, it's just a concatenated list of Resource Data instances, all with their header info
			
			_stream.Seek(0, SeekOrigin.Begin);
			
			BinaryReader rdr = new BinaryReader(_stream, System.Text.Encoding.Unicode);
			
			while(rdr.BaseStream.Position < rdr.BaseStream.Length) {
				
				///////////////////////////////
				// Read the RESOURCEHEADER
				
				Int64 start = rdr.BaseStream.Position;
				
				ResourceHeader header = new ResourceHeader( rdr );
				
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
					Int32 headerType = Convert.ToInt32( header.Type );
					if( headerType == 0 ) {
						rdr.Align4();
						continue;
					}
				}
				
				///////////////////////////////
				// Create ResourceType, Name, and Lang instance
				
				ResourceTypeIdentifier typeId = header.Type is String ? new ResourceTypeIdentifier( (String)header.Type ) : new ResourceTypeIdentifier( Convert.ToInt32( header.Type ) );
				
				ResourceType type = UnderlyingFind( t => t.Identifier.Equals( typeId ) );
				if(type == null) {
					
					type = new ResourceType( typeId, this );
					
					UnderlyingAdd( type );
				}
				
				///////////////////////////////////////////////////////////
				
				ResourceIdentifier nameId = header.Name is String ? new ResourceIdentifier( (String)header.Name ) : new ResourceIdentifier( Convert.ToInt32( header.Name ) );
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
		
		private class ResResource {
			
			public Int64                  HeaderOffset { get; set; }
			public Int64                  HeaderLength { get; set; }
			public Int64                  DataOffset   { get; set; }
			public Int32                  DataLength   { get; set; }
			
			public ResourceLang           Lang   { get; set; }
		}
	}
	
}
