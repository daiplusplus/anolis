using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Anolis.FileTypes {
	
	public class ColumnHeader2 : ColumnHeader {
		
		private SortOrder _sortOrderIndicator;
		
		public SortOrder SortOrderIndicator {
			get { return _sortOrderIndicator; }
			set {
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
		
		public Boolean IsSelected {
			get {
				return _isSelected;
			}
			set {
				
				if( value ) NativeMethods.ListView_SetSelectedColumn( ListView.Handle, Index );
				
				// set the reference field in the other columns
				foreach(ColumnHeader header in ListView.Columns) {
					if(header == this) return;
					
					ColumnHeader2 header2 = header as ColumnHeader2;
					if(header2 != null) header2._isSelected = false;
				}
				
			}
			
			
		}
		
	}
	
	internal static class NativeMethods {
		
		[DllImport("uxtheme.dll", CharSet=CharSet.Unicode, SetLastError=true, ThrowOnUnmappableChar=true)]
		public static extern IntPtr SetWindowTheme(IntPtr hWnd, String pszSubAppName, String pszSubIdList);
		
		
		// Attribution to WinAPIZone for this:
		// http://www.winapizone.net/tutorials/winapi/listview/columnsortimage.php
		
		[DllImport("user32.dll", CharSet=CharSet.Unicode, SetLastError=true, ThrowOnUnmappableChar=true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 message, UIntPtr wParam, IntPtr lParam);
		
		private const UInt32 HDF_SORTUP       = 0x0400;
		private const UInt32 HDF_SORTDOWN     = 0x0200;
		
		private const UInt32 LVM_FIRST        = 0x1000;
		private const UInt32 LVM_GETHEADER    = LVM_FIRST + 31;
		private const UInt32 LVM_SETSELECTEDCOLUMN = LVM_FIRST + 140;
		private const UInt32 HDI_FORMAT       = 0x0004;
		
		private const UInt32 HDM_FIRST        = 0x1200;
		private const UInt32 HDM_GETITEMCOUNT = HDM_FIRST + 0;
		private const UInt32 HDM_GETITEM      = HDM_FIRST + 11;
		private const UInt32 HDM_SETITEM      = HDM_FIRST + 12;
		
		public static void ListView_SetSelectedColumn(IntPtr listViewHWnd, Int32 columnIdx) {
			
			IntPtr result = SendMessage( listViewHWnd, LVM_SETSELECTEDCOLUMN, new UIntPtr( (uint)columnIdx ), IntPtr.Zero );
			
		}
		
		public static void ListView_SetHeaderSortIndicator(IntPtr hWnd, Int32 columnIdx, SortOrder order) {
			
			IntPtr header     = ListView_GetHeader( hWnd );
			Int32  nofColumns = Header_GetItemCount( header );
			
			for(int i=0;i<nofColumns;i++) {
				
				HdItem item = new HdItem();
				item.mask = HDI_FORMAT;
				item = Header_GetItem( header, i, item );
				
				if( i == columnIdx ) {
					
					item.fmt &= ~( HDF_SORTDOWN | HDF_SORTUP );
					
					if(order == SortOrder.Ascending)
						item.fmt |= HDF_SORTUP;
					else if(order == SortOrder.Descending)
						item.fmt |= HDF_SORTDOWN;
					
				} else {
					
					item.fmt &= ~( HDF_SORTDOWN | HDF_SORTUP );
					
				}
				
				Header_SetItem(header, i, ref item );
			}
		}
		
		private static IntPtr ListView_GetHeader(IntPtr listViewHWnd) {
			
			IntPtr ret = SendMessage( listViewHWnd, LVM_GETHEADER, UIntPtr.Zero, IntPtr.Zero );
			
			return ret;
			
		}
		
		private static Int32 Header_GetItemCount(IntPtr headerHWnd) {
			
			IntPtr result = SendMessage( headerHWnd, HDM_GETITEMCOUNT, UIntPtr.Zero, IntPtr.Zero );
			
			return result.ToInt32();
		}
		
		private static HdItem Header_GetItem(IntPtr headerHWnd, Int32 columnIndex, HdItem info) {
			
			Int32 size = Marshal.SizeOf( info );
			IntPtr p = Marshal.AllocHGlobal( size );
			
			Marshal.StructureToPtr( info, p, true );
			
			IntPtr result = SendMessage( headerHWnd, HDM_GETITEM, new UIntPtr( (uint)columnIndex ), p );
			
			HdItem updated = (HdItem)Marshal.PtrToStructure( p, typeof(HdItem) );
			
			Marshal.FreeHGlobal( p );
			
			return updated;
		}
		
		private static void Header_SetItem(IntPtr headerHWnd, Int32 columnIndex, ref HdItem info) {
			
			Int32 size = Marshal.SizeOf( info );
			IntPtr p = Marshal.AllocHGlobal( size );
			
			Marshal.StructureToPtr( info, p, true );
			
			IntPtr result = SendMessage( headerHWnd, HDM_SETITEM, new UIntPtr( (uint)columnIndex ), p );
			
			Marshal.FreeHGlobal( p );
		}
		
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		private struct HdItem {
			
			public UInt32 mask;
			public Int32  cxy;
			public String pszText;
			/// <summary>Handle to a Bitmap</summary>
			public IntPtr hbm;
			public Int32  cchTextMax;
			public UInt32 fmt;
			public IntPtr lParam;
			
			// IE > 3
			/// <summary>ImageList bitmap index</summary>
			public Int32  iImage;
			public Int32  iOrder;
			
			// IE > 5
			public UInt32 type;
			public IntPtr pvFilter;
			
			// IE > 6
			public UInt32 state;
		}
		
	}
	
}
