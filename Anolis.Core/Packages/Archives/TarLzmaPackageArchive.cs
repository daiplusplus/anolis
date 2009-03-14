using System;
using System.IO;

using W3b.TarLzma;

namespace Anolis.Core.Packages {
	
	public class TarLzmaPackageArchive : PackageArchive, IDisposable {
		
		private TarLzmaDecoder _dec;
		private String         _destinationDir;
		
		public TarLzmaPackageArchive(String name, Stream stream) : base(name) {
			
			_dec = new TarLzmaDecoder( stream );
			_dec.ProgressEvent += new EventHandler<ProgressEventArgs>(_dec_ProgressEvent);
		}
		
		private void _dec_ProgressEvent(object sender, ProgressEventArgs e) {
			
			OnPackageProgressEvent( new PackageProgressEventArgs( e.Percentage, e.Message ) );
		}
		
		public override void Extract(String destinationDirectory) {
			
			_dec.Extract( _destinationDir = destinationDirectory );
		}
		
		public override Package GetPackage() {
			
			if( State != PackageArchiveState.Extracted ) throw new InvalidOperationException("Cannot open the package when it hasn't been extracted");
			
			String packageXmlFilename = Path.Combine( _destinationDir, "Package.xml" ); 
			if( !System.IO.File.Exists( packageXmlFilename ) ) throw new FileNotFoundException("Could not find Package definition file", packageXmlFilename);
			
			return Package.FromFile( packageXmlFilename );
		}
		
		protected override void Dispose(Boolean managed) {
			
			if( managed ) _dec.Dispose();
			
			base.Dispose(managed);
		}
		
	}
}
