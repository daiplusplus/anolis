using System;

namespace Anolis.Console {
	
	public class Program {
		
		public static Int32 Main(String[] args) {
			
			if(!EnsureArgs(args)) {
				return 1;
			}
			
			return 0;
		}
		
		private static Boolean EnsureArgs(String[] args) {
			
			// Anolis.Console is a 100% ResHacker-compatible command-line program
			// the documentation on ResHacker's command arguments are in its help file
			
			// for the most part it seems pretty easy to reimplement, except it supports reading and writing to both *.rc and *.res
			// and I've no idea how to work with those files...
			
			return true;
			
		}
		
	}
}
