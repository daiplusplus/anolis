using System;
using System.Runtime.Serialization;

namespace Anolis.Core.Data {
	
	[Serializable]
	public class ResourceDataException : AnolisException {
		
		private const String DefaultMessage = "An exception was thrown during a ResourceData operation.";
		
		public ResourceDataException() {
		}
		
		public ResourceDataException(String message) : base(message) {
		}
		
		public ResourceDataException(String message, Exception inner) : base(message, inner) {
		}
		
		public ResourceDataException(Exception inner) : base(DefaultMessage, inner) {
		}
		
		protected ResourceDataException(SerializationInfo info, StreamingContext context) : base(info, context) {
		}
		
	}
	
}
