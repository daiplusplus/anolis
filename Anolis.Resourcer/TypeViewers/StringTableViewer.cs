using System;

using Anolis.Core.Data;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class StringTableViewer : TypeViewer {
		
		public StringTableViewer() {
			InitializeComponent();
		}
		
		public override void RenderResource(ResourceData resource) {
			
			StringTableResourceData rd = resource as StringTableResourceData;
			if(rd == null) return;
			
			__dgv.Rows.Clear();
			
			foreach(StringInfo info in rd.Strings) {
				
				String[] row = new String[2];
				row[0] = info.Id.ToString();
				row[1] = info.String;
				
				__dgv.Rows.Add( row );
			}
			
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			return data is StringTableResourceData ? TypeViewerCompatibility.Ideal : TypeViewerCompatibility.None;
		}
		
		public override string ViewerName {
			get { return "String Table Viewer"; }
		}
		
	}
}
