using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Anolis.Tools.TweakUI.FileTypes {
	
	public class ColumnHeader2 : ColumnHeader {
		
		private SortOrder _sortOrderIndicator;
		
		[DefaultValue( SortOrder.None )]
		public SortOrder SortOrderIndicator {
			get { return _sortOrderIndicator; }
			set {
				
				if( ListView == null ) return;
				
				_sortOrderIndicator = value;
				
				NativeMethods.ListView_SetHeaderSortIndicator( ListView.Handle, this.Index, value );
				
				// set the reference field in the other columns
				foreach(ColumnHeader header in ListView.Columns) {
					if(header == this) return;
					
					ColumnHeader2 header2 = header as ColumnHeader2;
					if(header2 != null) header2._sortOrderIndicator = SortOrder.None;
				}
			}
		}
		
		private Boolean _isSelected;
		
		[DefaultValue( false )]
		public Boolean IsSelected {
			get {
				return _isSelected;
			}
			set {
				
				if( ListView == null ) return;
				
				if( _isSelected = value ) NativeMethods.ListView_SetSelectedColumn( ListView.Handle, Index );
				
				// set the reference field in the other columns
				foreach(ColumnHeader header in ListView.Columns) {
					if(header == this) return;
					
					ColumnHeader2 header2 = header as ColumnHeader2;
					if(header2 != null) header2._isSelected = false;
				}
				
			}
			
			
		}
		
	}
	
}
