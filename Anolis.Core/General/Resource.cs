using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Anolis.Core.General {
	
	public interface IResourceSource : IDisposable {
		
		ResourceCollection GetResources();
		
		void AddResource(Resource resource);
		
		void RemoveResource(Resource resource);
		
		void CommitChanges();
		
		void Rollback();
		
	}
	
	public class ResourceCollection : ReadOnlyCollection<Resource> {
		public ResourceCollection(List<Resource> list) : base(list) {}
	}
	
	public abstract class Resource {
		
		protected Resource(IResourceSource source) {
			Source = source;
		}
		
		/// <summary>Gets the IResourceSource ths Resource originates from.</summary>
		public IResourceSource Source { get; private set; }
		
		////////////////////////////////////
		
		/// <summary>Returns the raw bytes of the resource's data.</summary>
		public abstract Byte[] GetRawData();
		
		/// <summary>Sets the raw bytes of the resource's data. Implementations may throw an exception if the data is not in the correct format, but this is not guaranteed.</summary>
		public abstract void   SetRawData(Byte[] data);
		
		////////////////////////////////////
		
		/// <summary>Returns an object that describes the enveloping type this resource is data is associated with.</summary>
		public abstract ResourceType Type { get; }
		
		/// <summary>Returns a String that is unique to this resource.</summary>
		public abstract String Name { get; }
		
		/// <summary>Returns an object that describes the language of this resource.</summary>
		public abstract ResourceLanguage Language { get; }
		
	}
	
	public abstract class ImageResource : Resource {
		
		protected ImageResource(IResourceSource source) : base(source) {
		}
		
		public abstract System.Drawing.Image Image { get; }
		
	}
	
	public enum ResourceType {
		
	}
	
	public abstract class ResourceLanguage {
		
		/// <summary>Returns the LocaleId of the language the resource is associated with.</summary>
		/// <example>Returns 1033 for "English - United States"</example>
		public abstract Int32 LocaleId { get; }
		
	}
	
}
