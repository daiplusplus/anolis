using System;
using System.Windows.Forms;

using Anolis.Core.Utility;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using SI = System.Windows.Forms.ListViewItem.ListViewSubItem;
using System.Drawing;

namespace Anolis.FileTypes {
	
	public partial class MainForm : Form {
		
		private FileAssociations _assoc;
		
		public MainForm() {
			InitializeComponent();

			this.Load += new EventHandler(MainForm_Load);
			this.__reload.Click += new EventHandler(__reload_Click);
			
			this.__typesList.ColumnClick += new ColumnClickEventHandler(ColumnClick);
			this.__extsList .ColumnClick += new ColumnClickEventHandler(ColumnClick);

			this.__typesList.SelectedIndexChanged += new EventHandler(__typesList_SelectedIndexChanged);
			this.__extsList.SelectedIndexChanged += new EventHandler(__extsList_SelectedIndexChanged);
			
			NativeMethods.SetWindowTheme( __typesList.Handle, "explorer", null );
			NativeMethods.SetWindowTheme( __extsList .Handle, "explorer", null );
		}
		
#region UI Events
		
		private void __reload_Click(Object sender, EventArgs e) {
			
			DataReload();
		}
		
		private void MainForm_Load(Object sender, EventArgs e) {
			
			DataReload();
		}
		
		private void ColumnClick(Object sender, ColumnClickEventArgs e) {
			
			ListView list = sender as ListView;
			
			Boolean sortAsc = true;
			
			ColumnHeader2 col = list.Columns[ e.Column ] as ColumnHeader2;
			if(col != null) {
				
				SortOrder oldOrder = col.SortOrderIndicator;
				if(oldOrder == SortOrder.Ascending) sortAsc = false;
				
				col.SortOrderIndicator = sortAsc ? SortOrder.Ascending : SortOrder.Descending;
			}
			
			list.ListViewItemSorter = new ColumnIndexComparator( sortAsc, e.Column );
		}
		
		private class ColumnIndexComparator : IComparer {
			
			private Boolean _asc;
			private Int32 _columnIdx;
			
			public ColumnIndexComparator(Boolean ascending, Int32 columnIdx) {
				
				_asc       = ascending;
				_columnIdx = columnIdx;
			}
			
			public Int32 Compare(Object x, Object y) {
				
				ListViewItem xi = x as ListViewItem;
				ListViewItem yi = y as ListViewItem;
				
				if( xi == null || yi == null ) return 0;
				
				String a = xi.SubItems[ _columnIdx ].Text;
				String b = yi.SubItems[ _columnIdx ].Text;
				
				if( _asc ) {
					
					return String.Compare( a, b, StringComparison.CurrentCulture );
				}
				return String.Compare( b, a, StringComparison.CurrentCulture );
				
				
			}
		}
		
		private void __extsList_SelectedIndexChanged(object sender, EventArgs e) {
			
			if( __extsList.SelectedItems.Count == 1 ) {
				
				ListViewItem selectedItem = __extsList.SelectedItems[0];
				
				FileExtension ext = selectedItem.Tag as FileExtension;
				__extGotoType.Enabled = ext.FileType != null;
				__extEdit.Enabled     = true;
				
			} else {
				
				__extGotoType.Enabled = false;
				__extEdit.Enabled     = false;
				
			}
			
		}
		
		private void __typesList_SelectedIndexChanged(object sender, EventArgs e) {
			
			if( __typesList.SelectedItems.Count == 1 ) {
				
				ListViewItem selectedItem = __typesList.SelectedItems[0];
				__typeEdit.Enabled     = true;
				
			} else {
				
				__typeEdit.Enabled     = false;
				
			}
			
		}
		
#endregion
		
		private void DataReload() {
			
			_assoc = FileAssociations.GetAssoctiations();
			
			//////////////////////////////////
			// Types List
			
			__typesList.BeginUpdate();
			__typesList.Items.Clear();
			
			foreach(FileType type in _assoc.AllTypes) {
				
				ListViewItem item = CreateItemForType( type );
				
				__typesList.Items.Add( item );
			}
			
			__typesList.EndUpdate();
			
			//////////////////////////////////
			// Extensions List
			
			__extsList.BeginUpdate();
			__extsList.Items.Clear();
			
			foreach(FileExtension ext in _assoc.AllExtensions) {
				
				ListViewItem item = CreateItemForExt( ext );
				
				__extsList.Items.Add( item );
			}
			
			__extsList.EndUpdate();
			
		}
		
		private static ListViewItem CreateItemForType(FileType type) {
			
			ListViewItem item = new ListViewItem( type.ProgId );
			item.UseItemStyleForSubItems = false;
			item.Tag = type;
			
			String[] subitems = new String[] {
				type.FriendlyName == null ? "" : type.FriendlyName,
				Concatenate( type.Extensions )
			};
			
			item.SubItems.AddRange( subitems );
			
			return item;
		}
		
		private static ListViewItem CreateItemForExt(FileExtension ext) {
			
			ListViewItem item = new ListViewItem( ext.Extension );
			item.UseItemStyleForSubItems = false;
			item.Tag = ext;
			
			///////////////////////////////////////
			// [0] = ProgId
			
			item.SubItems.Add( new SI( item, ext.ProgId ) );
			
			if( ext.ProgId == null || ext.FileType == null ) {
				// so the progId specifies a type that doesn't exist, e.g. "Safari Download"
				
				if( ext.ProgId == null ) {
					item.SubItems[1].Text = "(null)";
				}
				
				item.SubItems[1].ForeColor = Color.Red;
			}
			
			///////////////////////////////////////
			// [1] = Type Friendly Name
			
			if( ext.FileType == null ) {
				
				item.SubItems.Add( "" );
				
			} else {
				
				item.SubItems.Add( ext.FileType.FriendlyName );
				
				if( ext.FileType.FriendlyName == null ) {
					
					item.SubItems[2].Text = "(null)";
					item.SubItems[2].ForeColor = Color.Red;
					
				}
				
			}
			
			return item;
		}
		
		private static String Concatenate(IEnumerable<FileExtension> exts) {
			
			StringBuilder sb = new StringBuilder();
			foreach(FileExtension e in exts) {
				
				sb.Append( e.Extension );
				sb.Append(", ");
			}
			if( sb.Length > 2 ) sb.Remove( sb.Length - 2, 2 );
			
			return sb.ToString();
		}
		
	}
}
