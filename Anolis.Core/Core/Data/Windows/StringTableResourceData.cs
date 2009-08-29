using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;

using Cult = System.Globalization.CultureInfo;
using System.Text;

namespace Anolis.Core.Data {
	
	public class StringTableResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			return (typeId.KnownType == Win32ResourceType.StringTable) ? Compatibility.Yes : Compatibility.No;
		}
		
		public override Compatibility HandlesExtension(String fileNameExtension) {
			return Compatibility.No;
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			return StringTableResourceData.Create(lang, data);
		}
		
		public override ResourceData FromFileToAdd(System.IO.Stream stream, String extension, UInt16 langId, ResourceSource currentSource) {
			throw new NotSupportedException();
		}
		
		public override ResourceData FromFileToUpdate(System.IO.Stream stream, string extension, ResourceLang currentLang) {
			throw new NotSupportedException();
		}
		
		public override String Name {
			get { return "String Table"; }
		}
		
		public override String OpenFileFilter {
			get { return null; }
		}
	}
	
	public class StringTableResourceData : ResourceData {
		
		private StringTableResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		protected override void SaveAs(System.IO.Stream stream, string extension) {
			throw new NotSupportedException();
		}
		
		protected override String[] SupportedFilters {
			get { return new String[0]; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier(Win32ResourceType.StringTable);
		}
		
		internal static StringTableResourceData Create(ResourceLang lang, Byte[] rawData) {
			
			//String resourceName = lang.Name.Identifier.FriendlyName;
			//Int32 resourceNameInt = -1;
			// !Int32.TryParse( resourceName, System.Globalization.NumberStyles.Integer, Cult.InvariantCulture, out resourceNameInt )
			if( lang.Name.Identifier.IntegerId == null ) {
				throw new ResourceDataException("String Table resource names must be integers");
			}
			
			StringTableResourceData ret = new StringTableResourceData(lang, rawData);
			ret.StartId = ResourceNameToStringId(lang.Name.Identifier.IntegerId.Value);
			
			List<StringInfo> underlying = new List<StringInfo>();
			
			// FSM parser time, yay
			using(MemoryStream ms = new MemoryStream(rawData))
			using(StreamReader rdr = new StreamReader(ms, Encoding.Unicode)) {
				
				Int32 stringIdx = 0;
				
				Int32 nc; Char c;
				while( (nc = rdr.Read()) != -1 ) {
					c = (Char)nc;
					
					if(c != (Char)0) { // NULL
						
						// this char is the length of the string
						Int32 length = nc;
						
						Char[] str = new Char[ length ];
						int cnt = rdr.Read( str, 0, length );
						if(cnt != length) throw new ResourceDataException("Unexpected string length");
						
						StringInfo info = new StringInfo(stringIdx + ret.StartId, new String( str ) );
						underlying.Add( info );
					}
					
					stringIdx++;
				}
				
			}
			
			ret.Strings = new StringInfoCollection( underlying );
			
			return ret;
		}
		
		public Int32                StartId { get; private set; }
		public StringInfoCollection Strings { get; private set; }
		
		
		private static Int32 ResourceNameToStringId(Int32 resourceNameId) {
			
			return (resourceNameId - 1) * 16;
		}
		
	}
	
	public class StringInfoCollection : ReadOnlyCollection<StringInfo> {
		
		public StringInfoCollection(IList<StringInfo> underlying) : base(underlying) {
		}
	}
	
	public class StringInfo {
		
		internal StringInfo(Int32 id, String str) {
			
			Id        = id;
			String    = str;
		}
		
		public Int32   Id        { get; private set; }
		public String  String    { get; private set; }
	}
	
}
