using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Anolis.Tools.PEInfo {
	
	public static class Extensions {
		
		public static UInt16[] ReadUInt16Array(this BinaryReader rdr, int count) {
			
			UInt16[] ret = new UInt16[ count ];
			for(int i=0;i<count;i++)
				ret[i] = rdr.ReadUInt16();
			
			return ret;
		}
		
	}
}
