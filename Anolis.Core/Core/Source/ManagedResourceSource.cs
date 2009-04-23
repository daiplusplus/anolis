using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace Anolis.Core.Source {
	
	public abstract class ManagedResourceSource {
		
		public abstract ManagedResourceInfo[] GetResourceInfo();
		
		public abstract Stream GetResourceStream(ManagedResourceInfo resource);
		
	}
	
	public class ManagedResourceInfo {
		
		public ManagedResourceInfo(ManagedResourceSource source, String name, ResourceLocation location) {
			
			Source   = source;
			Name     = name;
			Location = location;
		}
		
		public ManagedResourceSource Source { get; private set; }
		
		public String                Name   { get; private set; }
		
		public ResourceLocation      Location { get; private set; }
		
	}
}
