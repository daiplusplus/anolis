using System;
using System.Runtime.InteropServices;
using Anolis.Core.Native;

namespace Anolis.Core.Data {
	
/*	public class VersionResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if(typeId.KnownType == Win32ResourceType.Version)
				return Compatibility.Yes;
			else
				return Compatibility.No;
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			return Compatibility.No;
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			return VersionResourceData.TryCreate(data, lang);
			
		}
		
		public override ResourceData FromFile(System.IO.Stream stream, String extension, ResourceSource currentSource) {
			throw new NotSupportedException();
		}
		
		public override String Name {
			get { return "Version Info"; }
		}
		
		public override String OpenFileFilter {
			get { return null; }
		}
	}*/
	
	public class VersionResourceData : ResourceData {
		
		// represent the Version data in a tree like how it's stored (see: the "Children" members of various structures)
		// might be useful to display the data like that in the UI
		
		private VersionResourceData(Byte[] rawData, ResourceLang lang) : base(lang, rawData) {
		}
		
		internal static VersionResourceData TryCreate(Byte[] data, ResourceLang lang) {
			
			using(System.IO.MemoryStream s = new System.IO.MemoryStream(data))
			using(System.IO.BinaryReader rdr = new System.IO.BinaryReader(s)) {
				
				VsVersionInfo vi;
				vi.wLength      = rdr.ReadUInt16();
				vi.wValueLength = rdr.ReadUInt16();
				vi.wType        = rdr.ReadUInt16();
				
				Byte[] str      = rdr.ReadBytes( "VS_VERSION_INFO".Length * 2 );
				vi.szKey        = System.Text.Encoding.Unicode.GetString(str);
				
				Int32 bitsSoFar = (16 * 3) + (str.Length * 8 * 2);
				
				// Padding1= to align on a 32-bit boundary
				Int32 sizeOfPadding1  = (bitsSoFar % 32) / 8;
				
				vi.Padding1     = rdr.ReadBytes(sizeOfPadding1);
				
				Byte[] value    = rdr.ReadBytes(vi.wValueLength);
				Int32 sizeOfVsFixedFileInfo = Marshal.SizeOf(typeof(VsFixedFileInfo));
				// maybe throw an exception or something if sizeOfVsFixedFileInfo != vi.wValueLength
				IntPtr p        = Marshal.AllocHGlobal(value.Length);
				Marshal.Copy(value, 0, p, value.Length);
				vi.Value = (VsFixedFileInfo)Marshal.PtrToStructure(p, typeof(VsFixedFileInfo));
				Marshal.FreeHGlobal(p);
				
				bitsSoFar += value.Length * 8;
				Int32 sizeOfPadding2 = (bitsSoFar % 32) / 8;
				
				vi.Padding2 = rdr.ReadBytes(sizeOfPadding2);
				
				ReadChildren(rdr);
				
				return null;
				
			}//using
			
		}
		
		private static void ReadChildren(System.IO.BinaryReader rdr) {
			
			// data is (zero or one StringFileInfo) and/or (zero or one VarFileInfo)
			
			
			
		}
		
		protected override void SaveAs(System.IO.Stream stream, String extension) {
			throw new NotSupportedException();
		}
		
		protected override String[] SupportedFilters {
			get { return new String[0]; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier( Win32ResourceType.Version );
		}
		
	}
	
}
