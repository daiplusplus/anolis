using System;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Core.Utility {
	
/*	public class Pair {
		
		public Object X { get; set; }
		public Object Y { get; set; }
		
		public Pair(Object x, Object y) {
			X = x;
			Y = y;
		}
		
	}
	
	public sealed class Triple : Pair {
		
		public Object Z { get; set; }
		
		public Triple(Object x, Object y, Object z) : base(x, y) {
			Z = z;
		}
		
	}*/
	
	public class Pair<Tx,Ty> {
		
		public Tx X { get; set; }
		public Ty Y { get; set; }
		
		public Pair(Tx x, Ty y) {
			X = x;
			Y = y;
		}
		
		public override String ToString() {
			
			if(X is String) return X as String;
			return base.ToString();
			
		}
		
	}
	
	public sealed class Triple<Tx, Ty, Tz> : Pair<Tx, Ty> {
		
		public Object Z { get; set; }
		
		public Triple(Tx x, Ty y, Tz z) : base(x, y) {
			Z = z;
		}
		
	}
	
}
