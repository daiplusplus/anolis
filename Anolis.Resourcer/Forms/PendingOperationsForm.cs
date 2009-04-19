using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Anolis.Core;

namespace Anolis.Resourcer {
	
	public partial class PendingOperationsForm : Form {
		
		public PendingOperationsForm() {
			InitializeComponent();
			
			this.__listCancelOp.Click += new EventHandler(__cancelOp_Click);
			this.__listOperations.SelectedIndexChanged += new EventHandler(__operations_SelectedIndexChanged);
			this.Load += new EventHandler(PendingOperationsForm_Load);
		}
		
		private void __cancelOp_Click(object sender, EventArgs e) {
			
			List<ListViewItem> removeThese = new List<ListViewItem>();
			
			foreach(ListViewItem item in __listOperations.SelectedItems) {
				
				removeThese.Add( item );
				
				ResourceLang lang = item.Tag as ResourceLang;
				
				lang.Name.Type.Source.Cancel( lang );
				
			}
			
			foreach(ListViewItem item in removeThese) __listOperations.Items.Remove( item );
			
		}
		
		private void __operations_SelectedIndexChanged(object sender, EventArgs e) {
			
			__listCancelOp.Enabled = __listOperations.SelectedItems.Count > 0;
			__listCancelOp.Text    = __listOperations.SelectedItems.Count > 1 ? "Cancel Operations" : "Cancel Operation";
		}
		
		private	void PendingOperationsForm_Load(object sender, EventArgs e) {
			
			Populate();
		}
		
		private void Populate() {
			
			__listOperations.BeginUpdate();
			
			__listOperations.Items.Clear();
			
			MainForm form = MainForm.LatestInstance;
			
			foreach(ResourceLang lang in form.CurrentSource.AllActiveLangs) {
				
				ListViewItem item = new ListViewItem( new String[] {
					lang.Action.ToString(),
					MainForm.GetResourcePath( lang )
				} );
				item.Tag = lang;
				
				__listOperations.Items.Add( item );
			}
			
			__listOperations.EndUpdate();
			
		}
		
	}
}
