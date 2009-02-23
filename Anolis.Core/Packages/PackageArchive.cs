using System;
using System.IO;
using System.Threading;

namespace Anolis.Core.Packages {
	
	public abstract class PackageArchive {
		
		protected PackageArchive(String name) {
			Name = name;
		}
		
		public static PackageArchive FromStream(String name, PackageSubclass subclass, Stream stream) {
			
			switch(subclass) {
				case PackageSubclass.LzmaTarball:
					
					return new TarLzmaPackageArchive(name, stream );
					
				default:
					
					throw new NotSupportedException("Unrecognised Package format specified");
			}
			
		}
		
		public String Name { get; private set; }
		
		public abstract void Extract(String destinationDirectory);
		
		public abstract Package GetPackage();
		
		public PackageArchiveState State { get; protected set; }
		
		protected void OnPackageProgressEvent(PackageProgressEventArgs e) {
			
			if( PackageProgressEvent != null ) PackageProgressEvent(this, e);
			
		}
		
		public event EventHandler<PackageProgressEventArgs> PackageProgressEvent;
		
#region Async Extract
		
		/// <summary>Starts extracting the package on a separate thread to a temporary directory.</summary>
		public void BeginPackageExtract(Action<String> completionCallback) {
			
			ParameterizedThreadStart pts = new ParameterizedThreadStart( PackageExtract );
			
			Thread t = new Thread( pts );
			t.Start( completionCallback );
			
		}
		
		private void PackageExtract(Object callback) {
			
			Action<String> completionCallback = callback as Action<String>;
			
			
			String tempDir = Path.Combine( Path.GetTempPath(), "AnolisPackage" + this.Name );
			if( Directory.Exists( tempDir ) ) {
				
				OnPackageProgressEvent(new PackageProgressEventArgs(0, "Clearing old files"));
				
				Directory.Delete(tempDir, true);
			}
			
			OnPackageProgressEvent(new PackageProgressEventArgs(0, "Beginning extraction"));
			
			try {
				
				Extract( tempDir );
				
			} catch (Exception ex) {
				
				OnPackageProgressEvent(new PackageProgressEventArgs(100, "Exception: " + ex.Message));
				completionCallback( null );
				
				return;
			}
			
			OnPackageProgressEvent(new PackageProgressEventArgs(100, "Extraction complete"));
			
			completionCallback( tempDir );
			
		}
		
#endregion
		
	}
	
	public enum PackageSubclass {
		LzmaTarball
	}
	
	public sealed class PackageProgressEventArgs : EventArgs {
		
		public PackageProgressEventArgs(Int32 percentage, String message) {
			
			Percentage = percentage;
			Message    = message;
		}
		
		public Int32 Percentage { get; private set; }
		
		public String Message { get; private set; }
		
	}
	
	public enum PackageArchiveState {
		/// <summary>The package exists in a compressed state</summary>
		Compressed,
		/// <summary>The package is extracted to the filesystem</summary>
		Extracted
	}

	[Serializable]
	public class PackageArchiveException : Exception {
		
		public PackageArchiveException() {
		}
		
		public PackageArchiveException(String message) : base(message) {
		}
		
		public PackageArchiveException(String message, Exception inner) : base(message, inner) {
		}
		
		protected PackageArchiveException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) {
		}
		
	}
	
}
