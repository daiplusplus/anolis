using System;
using System.Runtime.Serialization;

namespace Anolis.Core {

	[System.Serializable]
	public class AnolisException : Exception {
		
		public AnolisException() {
		}
		
		public AnolisException(string message) : base(message) {
		}
		
		public AnolisException(string message, Exception inner) : base(message, inner) {
		}
		
		protected AnolisException(SerializationInfo info, StreamingContext context) : base(info, context) {
		}
	}
	
}
