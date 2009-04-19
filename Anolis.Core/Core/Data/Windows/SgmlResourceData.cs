using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Anolis.Core.Data {
	
	public class SgmlResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			
			if(typeId.KnownType == Win32ResourceType.Html)     return Compatibility.Yes;
			if(typeId.KnownType == Win32ResourceType.Manifest) return Compatibility.Yes;
			
			if(typeId.StringId != null) {
				
				if( String.Equals( typeId.StringId, "XML", StringComparison.OrdinalIgnoreCase) )  return Compatibility.Yes;
				if( String.Equals( typeId.StringId, "HTML", StringComparison.OrdinalIgnoreCase) ) return Compatibility.Yes;
				if( String.Equals( typeId.StringId, "SGML", StringComparison.OrdinalIgnoreCase) ) return Compatibility.Yes;
				
			}
			
			return Compatibility.Maybe;
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			
			if( filenameExtension == "XML" ) return Compatibility.Yes;
			if( filenameExtension == "HTM" ) return Compatibility.Yes;
			if( filenameExtension == "HTML") return Compatibility.Yes;
			
			return Compatibility.No;
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			return SgmlResourceData.TryCreate(lang, data);
		}
		
		public override String Name {
			get { return "SGML"; }
		}
		
		protected override String GetOpenFileFilter() {
			return CreateFileFilter("Sgml", "htm", "html", "xml", "sgml");
		}
		
		public override ResourceData FromFileToAdd(Stream stream, String extension, UInt16 lang, ResourceSource currentSource) {
			throw new NotSupportedException();
		}
		
		public override ResourceData FromFileToUpdate(Stream stream, String extension, ResourceLang currentLang) {
			throw new NotSupportedException();
		}
	}
	
	public class SgmlResourceData : ResourceData {
		
		private SgmlResourceData(String text, ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
			
			DocumentText = text;
		}
		
		internal static SgmlResourceData TryCreate(ResourceLang lang, Byte[] rawData) {
			
			// check for a '<' character in the first 5 non-whitespace chars
			
			// assume UTF8 encoding
			String s = Encoding.UTF8.GetString( rawData );
			s = s.Trim();
			Boolean hasOpenBracket = false;
			for(int i=0;i<Math.Min(5, s.Length);i++) {
				
				if( s[i] == '<' ) hasOpenBracket = true;
			}
			
			if( !hasOpenBracket ) return null;
			
			return new SgmlResourceData(s, lang, rawData);
		}
		
		protected override void SaveAs(System.IO.Stream stream, String extension) {
			base.Save(stream, "bin");
		}
		
		protected override String[] SupportedFilters {
			get { return new String[] {"SGML File|*.xml|HTML File|*.htm|XML File|*.xml"}; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier(Win32ResourceType.Html);
		}
		
		public String DocumentText {
			get; private set;
		}
		
		private Boolean     _xmlDocumentLoaded;
		private XmlDocument _xmlDocument;
		
		public XmlDocument Document {
			get {
				if( !_xmlDocumentLoaded ) {
					
					_xmlDocument = new XmlDocument();
					try {
						_xmlDocument.LoadXml( DocumentText );
					} catch(XmlException) {
						_xmlDocument = null;
					}
					
					_xmlDocumentLoaded = true;
				}
				return _xmlDocument;
			}
		}
		
	}
	
	
}
