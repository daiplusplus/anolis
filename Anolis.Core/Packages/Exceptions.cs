using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

using ValidationEventArgs = System.Xml.Schema.ValidationEventArgs;

namespace Anolis.Core.Packages {
	
	[Serializable]
	public class PackageException : AnolisException {
		
		public PackageException() {
		}
		
		public PackageException(String message) : base(message) {
		}
		
		public PackageException(String message, Exception inner) : base(message, inner) {
		}
		
		protected PackageException(SerializationInfo info, StreamingContext context) : base(info, context) {
		}
		
	}
	
	[Serializable]
	public sealed class PackageValidationException : PackageException {
		
		// should I suppress FxCop on the first two constructors?
		public PackageValidationException() {
		}
		
		public PackageValidationException(String message) : base(message) {
		}
		
		public PackageValidationException(String message, Collection<ValidationEventArgs> errors) : base(message) {
			
			ValidationErrors = errors;
		}
		
		/*protected PackageValidationException(SerializationInfo info, StreamingContext context) : base(info, context) {
		}*/
		
		public Collection<ValidationEventArgs> ValidationErrors {
			get; private set;
		}
		
	}
	
}
