using System;
using System.IO;

namespace Anolis.Core.Data {
	
	public abstract class MediaResourceData : ResourceData {
		
		protected MediaResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		/// <summary>Saves this resource to a temporary location on the HDD for playback by media players that don't support Managed Streams. The caller of this method must ensure to delete the file after it is no longer needed.</summary>
		public String SaveToTemporaryFile() {
			
			String tempFilePath = Path.GetTempFileName() + TempExtension;
			
			File.WriteAllBytes(tempFilePath, RawData);
			
			return tempFilePath;
			
		}
		
		protected abstract String TempExtension { get; }
		
	}
	
	
	public sealed class RiffMediaResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if(typeId.KnownType != Win32ResourceType.Custom) return Compatibility.No;
			
			switch(typeId.StringId.ToUpperInvariant()) {
				case "RIFF":
				case "WAV":
				case "WAVE":
				case "AVI":
					return Compatibility.Yes;
			}
			
			return Compatibility.No;
			
		}
		
		public override Compatibility HandlesExtension(string filenameExtension) {
			
			switch(filenameExtension.ToUpperInvariant()) {
				case "WAV":
				case "AVI":
					return Compatibility.Yes;
			}
			
			return Compatibility.No;
			
		}
		
		public override ResourceData FromResource(ResourceLang lang, byte[] data) {
			
			return new RiffMediaResourceData(lang, data);
			
		}
		
		public override ResourceData FromFile(Stream stream, String extension, ResourceSource currentSource) {
			
			return FromResource(null, GetAllBytesFromStream(stream) );
			
		}
		
		public override string Name {
			get { return "RIFF Resource"; }
		}
		
		public override string OpenFileFilter {
			get { return "RiffMedia (*.avi; *.wav)|*.avi; *.wav"; }
		}
	}
	
	public sealed class RiffMediaResourceData : MediaResourceData {
		
		internal RiffMediaResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		protected override void SaveAs(Stream stream, String extension) {
			
			stream.Write( base.RawData, 0, base.RawData.Length );
		}
		
		protected override String[] SupportedFilters {
			get { return new String[] { "Video for Windows (*.avi)|*.avi" }; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier("RIFF");
		}
		
		protected override String TempExtension { get { return ".avi"; } }
	}
}
