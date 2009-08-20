using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Anolis.Core.Utility {
	
	public class RateCalculator {
		
		private Int64                      _total;
		private Int64                      _soFar;
		private List<Pair<DateTime,Int64>> _soFarOverTime = new List<Pair<DateTime,Int64>>();
		
		public virtual void Add(Int64 newSoFarValue) {
			
			_soFar = newSoFarValue;
			
			_soFarOverTime.Add( new Pair<DateTime,Int64>( DateTime.Now, newSoFarValue ) );
		}
		
		public virtual void Reset(Int64 total) {
			
			_total = total;
			_soFarOverTime.Clear();
			_soFar = 0;
		}
		
		public virtual Int64 GetRate() {
			
			if( _soFarOverTime.Count == 0 ) return 0;
			
			DateTime now = DateTime.Now;
			
			Int64 lastPoint = _soFarOverTime[ _soFarOverTime.Count - 1 ].Y;
			
			Int64 pointFromOneSecondAgo = GetPoint(now, 1000);
			Int64 pointFromTwoSecondAgo = GetPoint(now, 2000);
			Int64 pointFromThrSecondAgo = GetPoint(now, 3000);
			
			pointFromOneSecondAgo = lastPoint - pointFromOneSecondAgo;
			pointFromTwoSecondAgo = lastPoint - pointFromTwoSecondAgo;
			pointFromThrSecondAgo = lastPoint - pointFromThrSecondAgo;
			
			// average of the 3 points
			
			return (pointFromOneSecondAgo + pointFromTwoSecondAgo + pointFromThrSecondAgo) / 3;
		}
		
		private Int64 GetPoint(DateTime now, Int32 milisecondsAgo) {
			
			for(int i=_soFarOverTime.Count-1;i>=0;i--) {
				
				Pair<DateTime,Int64> xfer = _soFarOverTime[i];
				
				TimeSpan span = now.Subtract( xfer.X );
				if( span.TotalMilliseconds >= milisecondsAgo ) { // since it's counting down and the comparison is GTE then this will be the closest match
					
					return xfer.Y;
				}
				
			}
			
			return 0;
		}
		
		public virtual TimeSpan GetTimeRemaining() {
			
			Int64 rate = GetRate();
			if( rate == 0 ) return TimeSpan.MaxValue;
			
			Int64 remaining = _total - _soFar;
			if( remaining < 0 ) return TimeSpan.MaxValue;
			
			Int64 secondsRemaining = remaining / rate;
			
			return new TimeSpan(0, 0, (int)secondsRemaining);
			
		}
		
		public Int64 Total {
			get {
				return _total;
			}
		}
		
		public Int64 LastValue {
			get {
				if( _soFarOverTime.Count == 0 ) return 0;
				return _soFarOverTime[ _soFarOverTime.Count - 1 ].Y;
			}
		}
		
	}
	
}
