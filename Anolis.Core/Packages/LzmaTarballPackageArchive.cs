using System;
using System.IO;
using SevenZip.Compression.LZMA;

namespace Anolis.Core.Packages {
	
	public class LzmaTarballPackageArchive : PackageArchive {
		
		private Stream        _lzmaStream;
		private Stream        _tarballStream;
		private DirectoryInfo _rootDirectory;
		
		internal LzmaTarballPackageArchive(Stream lzmaStream) {
			_lzmaStream = lzmaStream;
			
			State = PackageArchiveState.Compressed;
		}
		
		public override void Extract(String destinationDirectory) {
			
			DecompressLzma( true );
			
			
			
			State = PackageArchiveState.Extracted;
		}
		
		public override Package GetPackage() {
			
			if( State != PackageArchiveState.Extracted ) throw new InvalidOperationException("Cannot open the package when it hasn't been extracted");
			
			String packageXmlFilename = Path.Combine( _rootDirectory.FullName, "Package.xml" ); 
			if( !System.IO.File.Exists( packageXmlFilename ) ) throw new FileNotFoundException("Package does not contain a definition file", packageXmlFilename);
			
			return PackageReader.ReadPackage( packageXmlFilename );
			
		}
		
#region LZMA
		
		private void DecompressLzma(Boolean useMemoryStream) {
			
			if( useMemoryStream ) { // shame you can't use the ternary operator with subclass types
				_tarballStream = new MemoryStream();
			} else {
				_tarballStream = new FileStream( Path.GetTempFileName(), FileMode.Create );
			}
			
			
			Byte[] properties = new Byte[5];
			if (_lzmaStream.Read(properties, 0, 5) != 5) throw (new Exception("Input LZMA Stream has invalid length"));
			
			///////////////////////////////
			// Set up LZMA Decoder
			
			Decoder decoder = new Decoder();
			decoder.SetDecoderProperties(properties);
			
			Int64 outSize = 0;
			for (int i = 0; i < 8; i++) {
				int v = _lzmaStream.ReadByte();
				if (v < 0) throw (new Exception("Can't Read 1"));
				outSize |= ((long)(byte)v) << (8 * i);
			}
			Int64 compressedSize = _lzmaStream.Length - _lzmaStream.Position;
			
			
			///////////////////////////////
			// Set up Progress Callback
			
			LzmaEventHandler prog = new LzmaEventHandler( new EventHandler<PackageProgressEventArgs>(LzmaEventHandler_Callback), _lzmaStream.Length );
			
			///////////////////////////////
			// Decompress
			
			decoder.Code(_lzmaStream, _tarballStream, compressedSize, outSize, prog);
			
		}
		
		private void LzmaEventHandler_Callback(Object sender, PackageProgressEventArgs e) {
			
			OnPackageProgressEvent(e);
		}
		
		private class LzmaEventHandler : SevenZip.ICodeProgress {
			
			private Single _inLength;
			private EventHandler<PackageProgressEventArgs> _callback;
			
			public LzmaEventHandler(EventHandler<PackageProgressEventArgs> callback, Int64 inLength) {
				
				_callback = callback;
				_inLength = inLength;
			}
			
			public void SetProgress(long inSize, long outSize) {
				
				Single perc = (Single)inSize / _inLength;
				
				_callback(this, new PackageProgressEventArgs( (int)perc, "Decompressing LZMA" ) );
				
			}
		}
		
#endregion

#region Tarball
		
		
		
#endregion
		
	}
}
