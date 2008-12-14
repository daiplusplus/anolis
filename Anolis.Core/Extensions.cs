using System;

namespace Anolis.Core {

// Extension methods seem to require System.Core.dll, which is not in .NET2.0
/*	public static class Extensions {
		
		public static Byte[] SubArray(this Byte[] array, Int32 startIndex, Int32 length) {
			
			if(startIndex >= array.Length         ) throw new ArgumentOutOfRangeException("startIndex");
			if(startIndex + length >= array.Length) throw new ArgumentOutOfRangeException("length");
			
			Byte[] retval = new Byte[ length ];
			
			for(int i=0;i<length;i++) retval[i] = array[startIndex + i];
			
			return retval;
			
		}
		
	}*/
}
