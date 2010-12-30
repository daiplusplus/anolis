using System;
using System.Collections.Generic;
using System.Text;

using Anolis.Core.Data;

namespace Anolis.Core {
	
	public class ResourceLangEnumerable : IEnumerable<ResourceLang> {
		
		private Predicate<ResourceLang> _selectionPredicate;
		private ResourceTypeCollection  _allTypes;
		
		internal ResourceLangEnumerable(ResourceTypeCollection allTypes, Predicate<ResourceLang> selectionPredicate) {
			
			_allTypes           = allTypes;
			_selectionPredicate = selectionPredicate;
		}
		
		public IEnumerator<ResourceLang> GetEnumerator() {
			
			return new ResourceLangEnumerator( _allTypes, _selectionPredicate );
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			
			return GetEnumerator();
		}
		
	}
	
	internal class ResourceLangEnumerator : IEnumerator<ResourceLang> {
		
		private ResourceTypeCollection _types;
		
		private Int32 _ti, _ni, _li = -1; // current indexes for types, names, and langs respectivly
		private ResourceLang            _currentLang;
		private Predicate<ResourceLang> _selectionPredicate;
		
		public ResourceLangEnumerator(ResourceTypeCollection types, Predicate<ResourceLang> selectionPredicate) {
			_types              = types;
			_selectionPredicate = selectionPredicate;
		}
		
		public ResourceLang Current {
			get { return _currentLang; }
		}
		
		public void Dispose() {
			GC.SuppressFinalize(this);
		}
		
		Object System.Collections.IEnumerator.Current {
			get { return this.Current; }
		}
		
		public Boolean MoveNext() {
			
			if( _selectionPredicate == null ) {
				
				_currentLang = GetNextLang();
				
				return _currentLang == null ? false : true;
				
			} else {
				
				do {
					
					_currentLang = GetNextLang();
				
					if( _currentLang == null ) return false;
					
				} while( !_selectionPredicate.Invoke(_currentLang) );
				
			}
			
			return true;
			
		}
		
		/// <summary>Returns the next ResourceLang in the tree or null if it's reached the end of the tree.</summary>
		private ResourceLang GetNextLang() {
			
			if( _types.Count == 0 ) return null;
			
			// HACK: Can we be guaranteed there will never be any cases of there being a ResourceName with no child ResourceLangs, or ResourceTypes with no ResourceNames?
			
			_li++;
			
			if( _li >= _types[ _ti ].Names[ _ni ].Langs.Count ) { // if done all the langs for this name, move on to the next name
				
				_li = 0;
				_ni++;
			}
			
			if( _ni >= _types[ _ti ].Names.Count ) { // if done all the names for this type, move on to the next type
				
				_ni = 0;
				_ti++;
			}
			
			if( _ti >= _types.Count ) { // if done all the types, return null
				
				return null;
			}
			
			ResourceLang currentLang = _types[ _ti ].Names[ _ni ].Langs[ _li ];
			// there should never be an ArgumentOutOfRangeExecption thrown here since all the checks are done above
				
			return currentLang;
		}
		
		public void Reset() {
			
			_ti = 0;
			_ni = 0;
			_li = -1;
			
		}
		
	}
}
