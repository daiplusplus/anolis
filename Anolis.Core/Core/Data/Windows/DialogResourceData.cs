using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

using Anolis.Core.Native;
using Anolis.Core.Utility;

namespace Anolis.Core.Data {
	
	public class DialogResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			return (typeId.KnownType == Win32ResourceType.Dialog) ? Compatibility.Yes : Compatibility.No;
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			return Compatibility.No;
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			return DialogResourceData.TryCreate(lang, data);
		}
		
		public override String Name {
			get { return "Dialog Box"; }
		}
		
		protected override String GetOpenFileFilter() {
			return null;
		}
		
		public override String OpenFileFilter {
			get { return null; }
		}
		
		public override ResourceData FromFileToAdd(Stream stream, string extension, ushort lang, ResourceSource currentSource) {
			throw new NotSupportedException();
		}
		
		public override ResourceData FromFileToUpdate(Stream stream, string extension, ResourceLang currentLang) {
			throw new NotSupportedException();
		}
	}
	
	public class DialogResourceData : ResourceData {
		
		private DialogResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
			
		}
		
		internal static DialogResourceData TryCreate(ResourceLang lang, Byte[] rawData) {
			
			if(rawData.Length < 18) {
				return null;
			}
			
			try {
				
				MemoryStream stream = new MemoryStream(rawData);
				BinaryReader rdr = new BinaryReader(stream, Encoding.Unicode);
				
				Boolean isTemplateEx = true;;
				Byte[] templateExSignature = new Byte[] { 0x01, 0x00, 0xFF, 0xFF };
				for(int i=0;i<templateExSignature.Length;i++) if( rawData[i] != templateExSignature[i] ) {
					isTemplateEx = false;
					break;
				}
				
				Dialog d = isTemplateEx ? BuildEx(rdr) : Build(rdr);
				
				DialogResourceData ret = new DialogResourceData(lang, rawData);
				ret.Dialog = d;
				
				return ret;
			
			} catch(Exception) {
				
				return null;
			}
			
		}
		
		private static Dialog Build(BinaryReader rdr) {
			
			DlgTemplate header = new DlgTemplate(rdr);
			
			List<DlgItemTemplate> ctrls = new List<DlgItemTemplate>();
			
			rdr.Align4();
			
			for(int i=0;i<header.cdit;i++) {
				
				DlgItemTemplate ctrl = new DlgItemTemplate(rdr);
				ctrls.Add( ctrl );
				
				rdr.Align4();
			}
			
			Dialog ret = new Dialog(header);
			foreach(DlgItemTemplate itemT in ctrls) ret.Controls.Add( new DialogControl(itemT) );
			return ret;
		}
		
		private static Dialog BuildEx(BinaryReader rdr) {
			
			DlgTemplateEx header = new DlgTemplateEx(rdr);
			
			List<DlgItemTemplateEx> ctrls = new List<DlgItemTemplateEx>();
			
			rdr.Align4();
			
			for(int i=0;i<header.cDlgItems;i++) {
				
				DlgItemTemplateEx ctrl = new DlgItemTemplateEx(rdr);
				ctrls.Add( ctrl );
				
				rdr.Align4();
			}
			
			Dialog ret = new Dialog(header);
			foreach(DlgItemTemplateEx itemT in ctrls) ret.Controls.Add( new DialogControl(itemT) );
			return ret;
		}
		
		protected override void SaveAs(System.IO.Stream stream, String extension) {
			throw new NotImplementedException();
		}
		
		protected override String[] SupportedFilters {
			get { return new String[] {}; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier(Win32ResourceType.Dialog);
		}
		
		public Dialog Dialog { get; private set; }
		
	}
	
}
