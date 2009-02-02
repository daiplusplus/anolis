using System;
using System.IO;

using Anolis.Console.Scripting;

using Con = System.Console;

namespace Anolis.Console {
	
	public class Program {
		
		public static Int32 Main(String[] args) {
			
			Script script = CreateScript(args);
			
			if(script == null) {
				PrintHelp();
				return 1;
			}
			
			script.Execute();
			
#if DEBUG
			Con.WriteLine();
			Con.WriteLine("Press <enter> to continue...");
			Con.ReadLine();
#endif
			
			return 0;
		}
		
		private static Script CreateScript(String[] args) {
			
			if(args != null && args.Length >= 2) {
				
				if( String.Equals(args[0], "-script", StringComparison.OrdinalIgnoreCase) ) {
					
					return Script.FromFile( args[1] );
					
				} else if( args.Length == 4 || args.Length == 5 ) {
					
					return Script.FromCmdArgs( args );
					
				}
				
			}
			
			return null;
			
		}
		
		private static Boolean PrintHelp() {
			
			String thisName = Path.GetFileName( System.Reflection.Assembly.GetEntryAssembly().Location );
			
			Con.WriteLine( Resource.Usage );
			
			// Anolis.Console is a 100% ResHacker-compatible command-line program
			// the documentation on ResHacker's command arguments are in its help file
			
			// for the most part it seems pretty easy to reimplement, except it supports reading and writing to both *.rc and *.res
			// and I've no idea how to work with those files...
			
			return true;
			
		}
		
	}
	
}
