using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Resourcer.Settings {
	
	/// <summary>Encapsulates a Most-Recently-Used list of strings.</summary>
	public class Mru {
		
		private List<String> _entries;
		
		/// <summary>Creates an empty Mru</summary>
		public Mru(Int32 capacity) : this(capacity, (IEnumerable<String>)null, StringComparison.InvariantCultureIgnoreCase) {
		}
		
		/// <summary>Creates an empty Mru</summary>
		public Mru(Int32 capacity, StringComparison comparison) : this(capacity, (IEnumerable<String>)null, comparison) {
		}
		
		public Mru(Int32 capacity, StringCollection entries, StringComparison comparison) : this(capacity, (IEnumerable<String>)null, comparison) {
			
			if(entries != null) foreach(String s in entries) _entries.Add( s );
			
			EnsureCapacity();
		}
		
		public Mru(Int32 capacity, IEnumerable<String> entries, StringComparison comparison) {
			
			if(capacity < 0) throw new ArgumentException("Capacity must be a non-negative integer", "capacity");
			
			Capacity   = capacity;
			Comparison = comparison;
			
			_entries = entries == null ? new List<String>() : new List<String>( entries );
			
			EnsureCapacity();
		}
		
		public StringComparison Comparison { get; set; }
		public Int32 Capacity { get; set; }
		
		public void Push(String entry) {
			
			// if the _entries list already contains the item, promote that item to the #1 position
			Int32 idx;
			if( (idx = _entries.FindIndex( s => s.Equals(entry, Comparison) )) != -1 ) {
				
				_entries.RemoveAt( idx );
				_entries.Insert(0, entry);
				
			} else { // otherwise add the item at the first position
				
				_entries.Insert( 0, entry );
				
				
				// if it's gone over capacity, truncate
				EnsureCapacity();
			}
			
		}
		
		/// <summary>Removes an entry from the Mru.</summary>
		public void Remove(String entry) {
			
			Int32 idx;
			while( (idx = _entries.FindIndex( s => s.Equals(entry, Comparison) )) != -1 ) {
				
				_entries.RemoveAt( idx );
				
			}
			
		}
		
		public void Clear() {
			_entries.Clear();
		}
		
		private void EnsureCapacity() {
			while(_entries.Count > Capacity) _entries.RemoveAt( Capacity );
		}
		
		public String[] Items {
			get { return _entries.ToArray(); }
		}
		
	}
}
