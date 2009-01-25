using System;
using System.Globalization;

namespace Anolis.Resourcer {
	
	public static class Cultures {
		
		private static ListBoxItem<CultureInfo>[] _culturesByName;
		private static ListBoxItem<CultureInfo>[] _culturesByLcid;
		
		private static Object  _initLock = new Object();
		private static Boolean _isinit   = false;
		
		public static void Initialise() {
			
			lock(_initLock) {
				
				if(_isinit) return;
				
				CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
				
				// TODO: Add the Neutral Culture
				
				ListBoxItem<CultureInfo>[] culturesList = new ListBoxItem<CultureInfo>[ cultures.Length  ];
				
				for(int i=0;i<cultures.Length;i++) {
					
					CultureInfo cult = cultures[i];
					
					String itemText = cult.DisplayName + " (" + cult.LCID.ToString(CultureInfo.InvariantCulture) + ')';
					
					culturesList[i] = new ListBoxItem<CultureInfo>( cult, itemText );
					
				}
				
				_culturesByName = new ListBoxItem<CultureInfo>[ culturesList.Length ];
				_culturesByLcid = new ListBoxItem<CultureInfo>[ culturesList.Length ];
				
				culturesList.CopyTo(_culturesByName, 0);
				culturesList.CopyTo(_culturesByLcid, 0);
				
				Array.Sort<ListBoxItem<CultureInfo>>(_culturesByName, (x,y) => x.Item.DisplayName.CompareTo( y.Item.DisplayName ) );
				Array.Sort<ListBoxItem<CultureInfo>>(_culturesByLcid, (x,y) => x.Item.LCID.CompareTo( y.Item.LCID ) );
				
				_isinit = true;
				
			}
			
		}
		
		public static ListBoxItem<CultureInfo>[] CulturesByName {
			get {
				if( _culturesByName == null ) Initialise();
				return _culturesByName;
			}
		}
		
		public static ListBoxItem<CultureInfo>[] CulturesByLcid {
			get {
				if( _culturesByLcid == null ) Initialise();
				return _culturesByLcid;
			}
		}
		
	}
	
	public class ListBoxItem<T> {
		
		public ListBoxItem(T item, String text) {
			Item = item;
			Text = text;
		}
		
		public T      Item { get; private set; }
		public String Text { get; private set; }
		
		public override string ToString() {
			return Text;
		}
	}
	
}
