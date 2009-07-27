using System;
using System.Collections.Generic;
using System.Text;
using Anolis.Core.Utility;

namespace Anolis.Installer {
	
	public class TransferRateCalculator {
		
		private List<Pair<DateTime,Int32>> _transferTimes = new List<Pair<DateTime,Int32>>();
		
		public void Add(Int32 bytesTransferedSoFar) {
			
			_transferTimes.Add( new Pair<DateTime,Int32>( DateTime.Now, bytesTransferedSoFar / 1024 ) );
		}
		
		public void Add(Int64 bytesTransferedSoFar) {
			
			Int32 kilobytes = (int)(bytesTransferedSoFar / 1024L);
			
			_transferTimes.Add( new Pair<DateTime,Int32>( DateTime.Now, kilobytes ) );
		}
		
		public void Reset() {
			
			_transferTimes.Clear();
		}
		
		/// <summary>Returns the current transfer rate in KB/s</summary>
		public Int32 GetTransferRate() {
			
			DateTime now = DateTime.Now;
			
			Pair<DateTime,Int32> last = _transferTimes[ _transferTimes.Count - 1 ];
			
			Int32 kbSecondAgo = 0;
			
			// get the transfer that happened 1 second ago
			for(int i=_transferTimes.Count-1;i>=0;i--) {
				
				Pair<DateTime,Int32> xfer = _transferTimes[i];
				
				TimeSpan span = now.Subtract( xfer.X );
				if( span.TotalSeconds >= 1 ) { // this is good enough
					
					kbSecondAgo = xfer.Y;
					break;
				}
				
			}
			
			// the difference in bytes is then the 
			
			return last.Y - kbSecondAgo;
		}
	}
}
