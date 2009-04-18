using System;
using System.Windows.Forms;
using Anolis.Core;

namespace Anolis.Resourcer {
	
	public partial class PendingOperationsForm : Form {
		
		public PendingOperationsForm() {
			InitializeComponent();

			this.__operations.SelectedIndexChanged += new EventHandler(__operations_SelectedIndexChanged);
			this.Load += new EventHandler(PendingOperationsForm_Load);
		}
		
		private void __operations_SelectedIndexChanged(object sender, EventArgs e) {
			
			__cancel.Enabled = __operations.SelectedItems.Count > 0;
			
			__cancel.Text    = __operations.SelectedItems.Count > 1 ? "Cancel Operations" : "Cancel Operation";
			
		}
		
		private	void PendingOperationsForm_Load(object sender, EventArgs e) {
			
			Populate();
		}
		
		private void Populate() {
			
			__operations.BeginUpdate();
			
			__operations.Items.Clear();
			
			MainForm form = MainForm.LatestInstance;
			
			foreach(ResourceLang lang in form.CurrentSource.AllActiveLangs) {
				
				ListViewItem item = new ListViewItem( new String[] {
					lang.Action.ToString(),
					MainForm.GetResourcePath( lang )
				} );
				
				__operations.Items.Add( item );
			}
			
			__operations.EndUpdate();
			
		}
		
	}
}
