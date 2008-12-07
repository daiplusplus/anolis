using System;
using System.Data;
using System.Collections.Generic;
using System.Xml;
using System.Text;

using Anolis.Core.Packages;

namespace Anolis.Console {
	
	/// <summary>Executes an Anolis Package</summary>
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
