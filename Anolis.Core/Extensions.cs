using System;
using XmlElement = System.Xml.XmlElement;

// Extension methods seem to require System.Core.dll, which is not in .NET2.0
// so here's an ersatz Extension attribute class

namespace System.Runtime.CompilerServices {
	
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class ExtensionAttribute : Attribute {
		
		public ExtensionAttribute() {
		}
		
	}
	
}

namespace Anolis.Core {
	
	public static class Extensions {
		
		public static Byte[] SubArray(this Byte[] array, Int32 startIndex, Int32 length) {
			
			if(startIndex >= array.Length         ) throw new ArgumentOutOfRangeException("startIndex");
			if(startIndex + length >= array.Length) throw new ArgumentOutOfRangeException("length");
			
			Byte[] retval = new Byte[ length ];
			
			for(int i=0;i<length;i++) retval[i] = array[startIndex + i];
			
			return retval;
			
		}
		
		public static String Left(this String s, Int32 length) {
			
			if(length < 0)        throw new ArgumentOutOfRangeException("length", length, "value cannot be less than zero");
			if(length > s.Length) throw new ArgumentOutOfRangeException("length", length, "value cannot be greater than the length of the string");
			
			return s.Substring(0, length);
			
		}
		
		public static String Right(this String s, Int32 length) {
			
			if(length < 0)        throw new ArgumentOutOfRangeException("length", length, "value cannot be less than zero");
			if(length > s.Length) throw new ArgumentOutOfRangeException("length", length, "value cannot be greater than the length of the string");
			
			return s.Substring( s.Length - length );
			
		}
		
		public static Int32 IndexOf(this Array a, Object o) {
			
			return Array.IndexOf( a, o );
			
		}
		
	}
}
