using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Anolis.Tools.TweakUI.FileTypes {
	
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
		
#region Semi-Native Methods
		
		public static void ListView_SetSelectedColumn(IntPtr listViewHWnd, Int32 columnIdx) {
			
			IntPtr result = SendMessage( listViewHWnd, LVM_SETSELECTEDCOLUMN, new UIntPtr( (uint)columnIdx ), IntPtr.Zero );
			
		}
		
		public static void ListView_SetHeaderSortIndicator(IntPtr hWnd, Int32 columnIdx, System.Windows.Forms.SortOrder order) {
			
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
		
#endregion
		
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
		
		//////////////////////////////////////////////
		
		[DllImport("shell32.dll")]
		public static extern IntPtr ExtractIcon(IntPtr instanceHandle, String exeFileName, Int32 iconIndex);
		
		public static Icon ExtractIcon(String exeFileName, Int32 iconIndex) {
			
			IntPtr hInstance = Process.GetCurrentProcess().Handle;
			
			IntPtr hIcon = ExtractIcon( hInstance, exeFileName, iconIndex );
			
			if( hIcon == IntPtr.Zero )
				return null;
			if( hIcon.ToInt32() == 1)
				return null;
			
			return Icon.FromHandle( hIcon );
			
		}
		
		[DllImport("user32.dll", EntryPoint="DestroyIcon", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean DestroyIcon(IntPtr hIcon);
		
#region Error Handling
		
		[DllImport("Kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern Int32 FormatMessage(FormatMessageFlags flags, IntPtr source, Int32 messageId, Int32 languageId, out String buffer, Int32 size, IntPtr arguments);
		
		public enum LanguageId : ushort {
			Neutral = 0x00,
			Invariant = 0x7f
		}
		
		public enum SubLanguageId : ushort {
			Neutral = 0x00,
			Default = 0x01,
			SystemDefault = 0x02
		}
		
		public static Int16 MakeLangId(LanguageId primaryLanguage, SubLanguageId subLanguage) {
			UInt16 pl = (UInt16)primaryLanguage, sl = (UInt16)subLanguage;
			return (Int16)( pl << (ushort)10 | sl );
		}
		
		public static Int32 GetLastError() {
			return Marshal.GetLastWin32Error();
		}
		
		public static String GetLastErrorString() {
			return GetErrorString( GetLastError() );
		}
		
		[Flags]
		public enum FormatMessageFlags {
			AllocateBuffer = 0x0100,
			IgnoreInserts  = 0x0200,
			FromString     = 0x0400,
			FromHModule    = 0x0800,
			FromSystem     = 0x1000,
			ArgumentArray  = 0x2000
		}
		
		private static String GetErrorString(Int32 errorCode) {
			
			String failed = "Unable to FormatMessage({0}), cause: {1}";
			
			String message;
			
			Int16 languageId = MakeLangId( LanguageId.Neutral, SubLanguageId.Neutral );
			
			FormatMessageFlags flags = FormatMessageFlags.AllocateBuffer | FormatMessageFlags.FromSystem;
			IntPtr zero = IntPtr.Zero;
			
			Int32 length = FormatMessage(flags, zero, errorCode, languageId, out message, 0, zero);
			
			if(
				length         == 0    ||
				message        == null ||
				message.Length != length) return String.Format(System.Globalization.CultureInfo.InvariantCulture, failed, errorCode, GetLastErrorString());
			
			message = message.Trim('\r', '\n');
			
			return message;
			
		}
#endregion
		
	}
}
