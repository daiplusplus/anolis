using System;
using System.IO;

namespace Anolis.Core.Data {
	
	public interface IResourceDataFactory {
		
		/// <summary>Indicates the compatibility between the specified ResourceType and this factory's ResourceData.</summary>
		Compatibility HandlesType(ResourceTypeIdentifier type);
		
		/// <summary>Returns null if unsuccessful.</summary>
		ResourceData FromFile(Stream stream);
		
		/// <summary>Returns null if unsuccessful.</summary>
		ResourceData FromResource(ResourceLang lang, Byte[] data);
		
		/// <summary>Gets the (human-readable) message as to why the previously loaded resource could not be loaded.</summary>
		String LastErrorMessage { get; }
		
		/// <summary>Gets the (human-readable) name of the data handled by this IResourceDataFactory.</summary>
		String Name { get; }
		
	}
	
	public enum Compatibility {
		Yes,
		Maybe,
		No
	}
	
}
