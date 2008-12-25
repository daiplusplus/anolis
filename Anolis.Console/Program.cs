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
			
			return true;
			
		}
		
	}
}
