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
	
	public class Pair<TX,TY> {
		
		public TX X { get; set; }
		public TY Y { get; set; }
		
		public Pair(TX x, TY y) {
			X = x;
			Y = y;
		}
		
		public override String ToString() {
			
			if(X is String) return X as String;
			return base.ToString();
			
		}
		
	}
	
	public sealed class Triple<TX, TY, TZ> : Pair<TX, TY> {
		
		public Object Z { get; set; }
		
		public Triple(TX x, TY y, TZ z) : base(x, y) {
			Z = z;
		}
		
	}
	
}
