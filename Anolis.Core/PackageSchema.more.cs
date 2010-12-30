using System;
using System.ComponentModel;

namespace Anolis.Core.Xml {
	
	public partial class Package {
		
		public Package() : base("package") {
			
			this.BeginInit();
			this.InitClass();
			
			CollectionChangeEventHandler schemaChangedHandler = new CollectionChangeEventHandler(this.SchemaChanged);
			
			base.Tables.CollectionChanged += schemaChangedHandler;
			base.Relations.CollectionChanged += schemaChangedHandler;
			
			EndInit();
			
		}
		
	}
	
}