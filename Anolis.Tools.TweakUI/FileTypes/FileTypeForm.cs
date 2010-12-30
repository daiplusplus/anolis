using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Anolis.Packages.Utility;

namespace Anolis.Tools.TweakUI.FileTypes {
	
	public partial class FileTypeForm : Form {
		
		private FileAssociations _assoc;
		
		public FileTypeForm() {
			InitializeComponent();

			this.Load += new EventHandler(MainForm_Load);
			this.__reload.Click += new EventHandler(__reload_Click);
			
			this.__typesList.ColumnClick += new ColumnClickEventHandler(ColumnClick);
			this.__extsList .ColumnClick += new ColumnClickEventHandler(ColumnClick);

			this.__typesList.SelectedIndexChanged += new EventHandler(__typesList_SelectedIndexChanged);
			this.__extsList .SelectedIndexChanged += new EventHandler(__extsList_SelectedIndexChanged);

			this.__extGotoType.Click += new EventHandler(__extGotoType_Click);
			
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
			
			col.IsSelected = true;
			
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
				__extEdit    .Enabled = true;
				
				__extEditor.Enabled = true;
				__extEditor.ShowExtension( ext );
				
			} else {
				
				__extGotoType.Enabled = false;
				__extEdit.Enabled     = false;
				
			}
			
		}
		
		private void __typesList_SelectedIndexChanged(object sender, EventArgs e) {
			
			if( __typesList.SelectedItems.Count == 1 ) {
				
				ListViewItem selectedItem = __typesList.SelectedItems[0];
				
				FileType type = selectedItem.Tag as FileType;
				
				__typeEditor.Enabled = true;
				__typeEditor.ShowFileType( type );
			}
			
		}
		
		////////////////////////////////////////////////
		// Buttons
		
		private void __extGotoType_Click(object sender, EventArgs e) {
			
			if( __extsList.SelectedItems.Count != 1 ) return;
			
			FileExtension ext = __extsList.SelectedItems[0].Tag as FileExtension;
			
			FileType type = ext.FileType;
			
			foreach(ListViewItem item in __typesList.Items) {
				
				FileType itemType = item.Tag as FileType;
				if( itemType == type ) {
					
					item.Selected = true;
					
					__typesList.EnsureVisible( item.Index );
					break;
				}
				
			}
			
			__tabs.SelectTab( __tabTypes );
			
		}
		
#endregion
		
		private void DataReload() {
			
			_assoc = FileAssociations.GetAssociations();
			
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
			
			///////////////////////////////////////
			// [1] = Friendly Name
			
			item.SubItems.Add( type.FriendlyName );
			
			if( type.FriendlyName == null ) {
				item.SubItems[1].Text = "(null)";
				item.SubItems[1].ForeColor = Color.Red;
			}
			
			///////////////////////////////////////
			// [2] = Extensions
			
			item.SubItems.Add( Concatenate( type.Extensions ) );
			
			return item;
		}
		
		private static ListViewItem CreateItemForExt(FileExtension ext) {
			
			ListViewItem item = new ListViewItem( ext.Extension );
			item.UseItemStyleForSubItems = false;
			item.Tag = ext;
			
			///////////////////////////////////////
			// [1] = ProgId
			
			item.SubItems.Add( ext.ProgId );
			
			if( ext.ProgId == null || ext.FileType == null ) {
				// so the progId specifies a type that doesn't exist, e.g. "Safari Download"
				
				if( ext.ProgId == null ) {
					item.SubItems[1].Text = "(null)";
				}
				
				item.SubItems[1].ForeColor = Color.Red;
			}
			
			///////////////////////////////////////
			// [2] = Type Friendly Name
			
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
