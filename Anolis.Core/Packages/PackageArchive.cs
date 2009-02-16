using System;
using System.IO;

namespace Anolis.Core.Packages {
	
	public abstract class PackageArchive {
		
		public static PackageArchive FromStream(PackageSubclass subclass, Stream stream) {
			
			switch(subclass) {
				case PackageSubclass.LzmaTarball:
					
					return new LzmaTarPackageArchive( stream );
					
				default:
					
					throw new NotSupportedException("Unrecognised Package format specified");
			}
			
		}
		
		public abstract void Extract(String destinationDirectory);
		
		public abstract Package GetPackage();
		
		public PackageArchiveState State { get; protected set; }
		
		protected void OnPackageProgressEvent(PackageProgressEventArgs e) {
			
			if( PackageProgressEvent != null ) PackageProgressEvent(this, e);
			
		}
		
		public event EventHandler<PackageProgressEventArgs> PackageProgressEvent;
		
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
