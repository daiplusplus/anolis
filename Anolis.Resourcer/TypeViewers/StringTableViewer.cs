using System;
using System.Windows.Forms;

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
				
				Int32 i = __dgv.Rows.Add( row );
				
				DataGridViewRow dgvRow = __dgv.Rows[i];
				
				Int32 prefHeight = dgvRow.GetPreferredHeight(i, DataGridViewAutoSizeRowMode.AllCellsExceptHeader, true);
				
				dgvRow.Height = prefHeight;
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
