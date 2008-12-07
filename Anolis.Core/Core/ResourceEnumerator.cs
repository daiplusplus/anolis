using System;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Core {
	
	internal class ResourceEnumerator : IEnumerator<ResourceData> {
		
		private ResourceTypeCollection _types;
		
		private Int32 _ti, _ni, _li = -1; // current indexes for types, names, and langs respectivly
		private ResourceData _currentData;
		private Boolean _onlyLoaded;
		
		/// <param name="onlyLoaded">Enumerate only loaded ResourceDatas (i.e. don't invoke lazy-loading by ResourceLang)</param>
		public ResourceEnumerator(ResourceTypeCollection types, Boolean onlyLoaded) {
			_types      = types;
			_onlyLoaded = onlyLoaded;
		}
		
		public ResourceData Current {
			get { return _currentData; }
		}
		
		public void Dispose() {}
		
		Object System.Collections.IEnumerator.Current {
			get { return this.Current; }
		}
		
		public Boolean MoveNext() {
			
			if( _types.Count == 0 ) return false;
			
			ResourceLang lang = GetNextLang();
			
			if( _onlyLoaded ) {
				
				while( !lang.DataIsLoaded ) {
					
					lang = GetNextLang();
					
					if(lang == null) return false;
				}
				
			}
			
			if(lang == null) {
				return false;
			} else {
				_currentData = lang.Data;
				return true;
			}
			
		}
		
		private ResourceLang GetNextLang() {
			
			// TODO: Can we be guaranteed there will never be any cases of there being a ResourceName with no child ResourceLangs, or ResourceTypes with no ResourceNames?
			
			_li++;
			
			if( _li >= _types[ _ti ].Names[ _ni ].Langs.Count ) { // if done all the langs for this name, move on to the next name
				
				_li = 0;
				_ni++;
			}
			
			if( _ni >= _types[ _ti ].Names.Count ) { // if done all the names for this type, move on to the next type
				
				_ni = 0;
				_ti++;
				
			}
			
			if( _ti >= _types.Count ) { // if done all the types, return false
				
				return null;
			}
			
			ResourceLang currentLang = _types[ _ti ].Names[ _ni ].Langs[ _li ];
			
			return currentLang;
			
		}
		
		public void Reset() {
			
			_ti = 0;
			_ni = 0;
			_li = -1;
			
		}
		
	}
}
