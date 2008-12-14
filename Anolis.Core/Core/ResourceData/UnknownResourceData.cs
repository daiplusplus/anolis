using System;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Core.Data {
	
	public sealed class UnknownResourceData : ResourceData {
		
		private UnknownResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		public static UnknownResourceData TryCreate(ResourceLang lang, Byte[] rawData) {
			
			return new UnknownResourceData(lang, rawData);
		}
		
	}
}
