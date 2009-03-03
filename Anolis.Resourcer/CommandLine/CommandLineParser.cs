using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Anolis.Resourcer.CommandLine {
	
	public class CommandLineParser {
		
		public CommandLineParser(String[] args) {
			
			List<CommandLineFlag>  flags = new List<CommandLineFlag>();
			List<CommandLineString> strs = new List<CommandLineString>();
			
			Args    = new ReadOnlyCollection<String>( new List<String>( args ) );
			Flags   = new ReadOnlyCollection<CommandLineFlag>(flags);
			Strings = new ReadOnlyCollection<CommandLineString>(strs);
			
			for(int i=0;i<args.Length;i++) {
				
				String s = args[i];
				
				if( s == null || s.Length == 0 ) continue;
				
				if( s.StartsWith("-") || s.StartsWith("--") || s.StartsWith("/") ) {
					flags.Add( new CommandLineFlag(s, i) );
				} else {
					
					strs.Add( new CommandLineString(s, i) );
				}
				
			}
			
		}
		
		public ReadOnlyCollection<String> Args { get; private set; }
		
		public ReadOnlyCollection<CommandLineFlag> Flags {
			get; private set;
		}
		
		public ReadOnlyCollection<CommandLineString> Strings {
			get; private set;
		}
		
		public CommandLineFlag GetFlag(String name) {
			
			foreach(CommandLineFlag flag in Flags) {
				
				if( String.Equals( flag.Name, name, StringComparison.OrdinalIgnoreCase ) ) return flag;
				
			}
			
			return null;
		}
		
	}
	
	public abstract class CommandLineItem {
		
		protected CommandLineItem(String s, Int32 index) {
			
			String = s;
			Index  = index;
		}
		
		public Int32 Index { get; private set; }
		
		public String String { get; private set; }
		
	}
	
	public class CommandLineFlag : CommandLineItem {
		
		public CommandLineFlag(String s, Int32 index) : base(s, index) {
			
			Int32 colonIdx = s.IndexOf(':');
			if(colonIdx > 1) {
				
				Name     = s.Substring(1, colonIdx - 1);
				if(colonIdx < s.Length - 1) Argument = s.Substring( colonIdx + 1);
				
			} else {
				
				Name     = s.Substring(1);
			}
			
		}
		
		public String Name { get; private set; }
		
		public String Argument { get; private set; }
		
	}
	
	public class CommandLineString : CommandLineItem {
		
		public CommandLineString(String s, Int32 index) : base(s, index) {
		}
		
		/// <summary>Indicates if this string is a well-formed path</summary>
		public Boolean IsValidPath {
			get {
				
				Uri uri;
				return Uri.TryCreate( String, UriKind.RelativeOrAbsolute, out uri);
			}
		}
		
	}
	
}
