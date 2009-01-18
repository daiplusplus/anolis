using System;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Core.Core.PE {
	
	public class NESource : ResourceSource {
		
		public NESource() : base(true) {
		}
		
		public override string Name {
			get { throw new NotImplementedException(); }
		}
		
		public override ResourceData GetResourceData(ResourceLang lang) {
			
			return null;
			
		}
		
		public override void CommitChanges(Boolean reload) {
			
		}
		
		public override void Reload() {
			
			
			
		}
	}
}
