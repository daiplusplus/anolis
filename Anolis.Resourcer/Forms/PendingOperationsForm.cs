using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Anolis.Core;
using System.Text;

using Cult               = System.Globalization.CultureInfo;
using FileResourceSource = Anolis.Core.Source.FileResourceSource;

namespace Anolis.Resourcer {
	
	public partial class PendingOperationsForm : BaseForm {
		
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
			
			////////////////////////////////////////////////
			// Operations List
			
			__listOperations.BeginUpdate();
			
			__listOperations.Items.Clear();
			
			MainForm form = MainForm.LatestInstance;
			
			ResourceSource source = form.CurrentSource;
			
			foreach(ResourceLang lang in source.AllActiveLangs) {
				
				ListViewItem item = new ListViewItem( new String[] {
					lang.Action.ToString(),
					lang.ResourcePath
				} );
				item.Tag = lang;
				
				__listOperations.Items.Add( item );
				
			}
			
			__listOperations.EndUpdate();
			
			////////////////////////////////////////////////
			// Package XML
			
			StringBuilder sb = new StringBuilder();
			
			FileResourceSource fSource = source as FileResourceSource;
			if( fSource != null ) {
				
				// I could do it all in 1 loop, but this is easier
				
				String patchElementA = @"<patch path=""{0}"">" + "\r\n";
				String patchElementB = @"</patch>";
				
				String resElement = '\t' + @"<res type=""{0}"" name=""{1}"" lang=""{2}"" src=""{3}"" {4}/>" + "\r\n";
				String addAttrib  = @"add=""true"" ";
				
				sb.AppendFormat(patchElementA, (source as FileResourceSource).FileInfo.FullName);
				
				foreach(ResourceLang lang in source.AllActiveLangs) {
					
					if(lang.Action != Anolis.Core.Data.ResourceDataAction.Add && lang.Action != Anolis.Core.Data.ResourceDataAction.Update) continue;
					
					if( !lang.Data.Tag.ContainsKey("sourceFile") ) continue; // so skip things like IconImage that wouldn't have this attribute
					
					String typeId = GetStringId( lang.Name.Type.Identifier );
					String nameId = GetStringId( lang.Name.Identifier );
					String langId = lang.LanguageId.ToString( Cult.InvariantCulture );
					String src    = (String)lang.Data.Tag["sourceFile"];
					
					String add = lang.Action == Anolis.Core.Data.ResourceDataAction.Add ? addAttrib : String.Empty;
					
					sb.AppendFormat( resElement, typeId, nameId, langId, src, add );
					
				}
				
				sb.Append( patchElementB );
				
				__xmlText.Text = sb.ToString();
			}
			
			////////////////////////////////////////////////
			// RC Script
			
		}
		
		private static String GetStringId(ResourceIdentifier id) {
			
			if( id.StringId != null ) return id.StringId;
			
			return id.IntegerId.Value.ToString( Cult.InvariantCulture );
			
		}
		
	}
}
