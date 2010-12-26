using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Anolis.Core;
using Anolis.Core.Data;
using System.IO;

namespace Anolis.Resourcer {
	
	public partial class FindForm : Form {
		
		public FindForm() {
			InitializeComponent();
			
			this.__findNext.Click += new EventHandler(__findNext_Click);
			this.__searchContent.CheckedChanged += new EventHandler(__searchContent_CheckedChanged);
		}
		
		private void __searchContent_CheckedChanged(object sender, EventArgs e) {
			
			__searchVisual.Enabled = __searchContent.Checked;
		}
		
		private void __findNext_Click(object sender, EventArgs e) {
			
			if( FindNextClicked != null )
				FindNextClicked(this, EventArgs.Empty);
			
		}
		
		public String FindText {
			get { return __text.Text; }
		}
		
		public FinderOptions FindOptions {
			get {
				
				FinderOptions ret = FinderOptions.None;
				if( __matchCase    .Checked ) ret |= FinderOptions.MatchCase;
				if( __searchNames  .Checked ) ret |= FinderOptions.SearchNames;
				if( __searchContent.Checked ) ret |= FinderOptions.SearchContent;
				if( __searchVisual .Checked ) ret |= FinderOptions.SearchVisual;
				
				return ret;
			}
		}
		
		public event EventHandler FindNextClicked;
		
	}
	
	public class Finder {
		
		private IEnumerator<ResourceLang> _enumerator;
		
		private static Encoding[] _encoding = new Encoding[] {
			Encoding.UTF8,
			Encoding.Unicode
			// don't bother with the others, they're esoteric
		};
		
		public Finder(ResourceSource source, String text, FinderOptions options) {
			Source   = source;
			FindText = text;
			Options  = options;
			
			HasStarted  = false;
			_enumerator = Source.AllLangs.GetEnumerator();
		}
		
		public ResourceSource Source   { get; private set; }
		public String         FindText { get; private set; }
		public FinderOptions  Options  { get; private set; }
		
		public ResourceLang FindNext() {
			
			HasStarted = true;
			
			if( EndReached ) {
				_enumerator.Reset();
				EndReached = false;
			}
			
			while( _enumerator.MoveNext() ) {
				
				ResourceLang candidate = _enumerator.Current;
				
				if( SearchResource( candidate ) ) return candidate;
			}
			
			EndReached = true;
			_enumerator.Dispose();
			return null;
		}
		
		public bool HasStarted {
			get; private set;
		}
		
		public bool EndReached {
			get; private set;
		}
		
		private bool SearchResource(ResourceLang lang) {
			
			if( O(FinderOptions.SearchNames) ) {
				
				StringComparison comp = O(FinderOptions.MatchCase) ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
				
				if( lang.Name.Identifier.FriendlyName.IndexOf( FindText, comp ) > -1 ) {
					return true;
				}
				
			}
			
			if( O(FinderOptions.SearchContent) ) {
				
				ResourceData data = lang.Data;
				if( !O(FinderOptions.SearchVisual) ) {
					
					if( data is DirectoryResourceData ) return false;
					if( data is ImageResourceData     ) return false;
					if( data is MediaResourceData     ) return false;
				}
				
				// we have to try different means of encoding, it seems
				foreach(Encoding enc in _encoding) {
					
					if( SearchWithEncoding( data.RawData, enc ) ) return true;
				}
				
			}
			
			return false;
		}
		
		private bool SearchWithEncoding(Byte[] data, Encoding encoding) {
			
			StringComparison comp = O(FinderOptions.MatchCase) ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
			
			Char lookFor = FindText[0];
			Char[] buffer = new Char[ FindText.Length ];
			buffer[0] = lookFor;
			
			using(MemoryStream ms = new MemoryStream(data))
			using(StreamReader rdr = new StreamReader(ms, encoding)) {
				
				long pos;
				int nc; char c;
				while( (nc = rdr.Read()) != -1 ) {
					c = (char)nc;
					
					if( c == lookFor ) {
						pos = ms.Position;
						
						if( rdr.Read( buffer, 1, buffer.Length - 1 ) == buffer.Length - 1 ) {
							
							String found = new String( buffer );
							if( found.Equals( FindText, comp ) ) return true;
							else {
								// rewind to just after lookFor
								ms.Seek( pos + 1, SeekOrigin.Begin );
							}
							
						}
						
					}
					
				}
				
			}
			
			return false;
			
		}
		
		private bool O(FinderOptions opt) {
			
			return (Options & opt) == opt;
		}
		
	}
	
	[Flags]
	public enum FinderOptions {
		None          = 0,
		MatchCase     = 1,
		SearchNames   = 2,
		SearchContent = 4,
		SearchVisual  = 8
	}
	
}
