using System;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Core.Utility {
	
	public static class Miscellaneous {
		
		public static String CreateFileFilter(String description, params String[] extensions) {
			
			StringBuilder sb = new StringBuilder();
			
			sb.Append( description );
			sb.Append( " (" );
			
			for(int i=0;i<extensions.Length;i++) {
				String ext = extensions[i];
				
				sb.Append("*.");
				sb.Append( ext );
				if(i < extensions.Length - 1) sb.Append(';');
				
			}
			
			sb.Append(")|");
			
			for(int i=0;i<extensions.Length;i++) {
				String ext = extensions[i];
				
				sb.Append("*.");
				sb.Append( ext );
				if(i < extensions.Length - 1) sb.Append(';');
				
			}
			
			String retval = sb.ToString();
			
			return retval;
			
		}
		
	}
}
