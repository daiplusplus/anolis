using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Globalization;

using XmlElement = System.Xml.XmlElement;
using C = System.Globalization.CultureInfo;

// Extension methods seem to require System.Core.dll, which is not in .NET2.0
// so here's an ersatz Extension attribute class

namespace System.Runtime.CompilerServices {
	
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class ExtensionAttribute : Attribute {
		
		public ExtensionAttribute() {
		}
		
	}
	
}

namespace Anolis {
	
	public static class Extensions {
		
		/// <summary>Copies the contents of this DirectoryInfo to the destination directory. If it doesn't exist it will be created.</summary>
		public static void CopyTo(this DirectoryInfo source, String destination) {
			
			if( !source.Exists ) throw new DirectoryNotFoundException("source directory doesn't exist: " + source.FullName);
			if( !Directory.Exists( destination ) ) Directory.CreateDirectory( destination );
			
			foreach(FileInfo file in source.GetFiles()) {
				
				file.CopyTo( Path.Combine( destination, file.Name ) );
			}
			
			foreach(DirectoryInfo child in source.GetDirectories()) {
				
				child.CopyTo(  Path.Combine( destination, child.Name ) );
				
			}
			
			
			
		}
		
#region Numbers
		
		public static String ToStringInvariant(this Byte n) {
			
			return n.ToString( C.InvariantCulture );
		}
		public static String ToStringInvariant(this SByte n) {
			
			return n.ToString( C.InvariantCulture );
		}
		public static String ToStringInvariant(this Int16 n) {
			
			return n.ToString( C.InvariantCulture );
		}
		public static String ToStringInvariant(this UInt16 n) {
			
			return n.ToString( C.InvariantCulture );
		}
		public static String ToStringInvariant(this Int32 n) {
			
			return n.ToString( C.InvariantCulture );
		}
		public static String ToStringInvariant(this UInt32 n) {
			
			return n.ToString( C.InvariantCulture );
		}
		public static String ToStringInvariant(this Int64 n) {
			
			return n.ToString( C.InvariantCulture );
		}
		public static String ToStringInvariant(this UInt64 n) {
			
			return n.ToString( C.InvariantCulture );
		}
		
		public static String ToStringInvariant(this Single n) {
			
			return n.ToString( C.InvariantCulture );
		}
		public static String ToStringInvariant(this Double n) {
			
			return n.ToString( C.InvariantCulture );
		}
		
		public static Boolean VersionTryParse(String s, out Version version) {
			
			version = null;
			
			try {
				version = new Version( s );
			} catch(ArgumentException) {
				return false;
			} catch(FormatException) {
				return false;
			} catch(OverflowException) {
				return false;
			}
			
			return true;
		}
		
#endregion
		
#region Strings
		
		public static String Left(this String str, Int32 length) {
			
			if(length < 0)          throw new ArgumentOutOfRangeException("length", length, "value cannot be less than zero");
			if(length > str.Length) throw new ArgumentOutOfRangeException("length", length, "value cannot be greater than the length of the string");
			
			return str.Substring(0, length);
			
		}
		
		/// <summary>Retreives a substring starting from the left to the right, missing <paramref name="fromRight" /> characters from the right. So "abcd".LeftFR(1) returns "abc"</summary>
		public static String LeftFR(this String str, Int32 fromRight) {
			
			if(fromRight < 0)          throw new ArgumentOutOfRangeException("fromRight", fromRight, "value cannot be less than zero");
			if(fromRight > str.Length) throw new ArgumentOutOfRangeException("fromRight", fromRight, "value cannot be greater than the length of the string");
			
			return str.Substring(0, str.Length - fromRight );
			
		}
		
		public static String Right(this String str, Int32 length) {
			
			if(length < 0)          throw new ArgumentOutOfRangeException("length", length, "value cannot be less than zero");
			if(length > str.Length) throw new ArgumentOutOfRangeException("length", length, "value cannot be greater than the length of the string");
			
			return str.Substring( str.Length - length );
			
		}
		
		/// <summary>Returns the next token in <paramref name="str"/> from position <paramref name="start"/>. Barring whitespace, it returns the sequence of characters from <paramref name="start"/> that share the same unicode category.</summary>
		public static String Tok(this String str, ref Int32 start) {
			
			if(start >= str.Length) throw new ArgumentOutOfRangeException("start");
			
			Int32 i = start;
			
			StringBuilder sb = new StringBuilder();
			
			Char c = str[i];
			
			// skip whitespace
			while( i < str.Length ) {
				
				c = str[ i ];
				if( !Char.IsWhiteSpace( c ) ) break;
				i++;
			}
			if( Char.IsWhiteSpace( c ) ) {
				start = str.Length;
				return null; // then it's got some trailing whitespace left, and it's at the end
			}
			
			Boolean doneRadixPoint = false;
			UnicodeCategory currentCat, initialCat;
			Char initialChar = c;
			initialCat = currentCat = Char.GetUnicodeCategory( c );
			
			// special case for ( and )
			if( initialChar == '(' || initialChar == ')' ) {
				start = i + 1;
				return initialChar.ToString();
			}
			
			while( i < str.Length && CategoryEquals(initialCat, currentCat) ) {
				
				c = str[i];
				sb.Append( str[i++] );
				
				if( i >= str.Length ) break;
				
				currentCat = Char.GetUnicodeCategory(str, i);
				
				// special case exception for radix points, there can only be 1
				if( initialCat == UnicodeCategory.DecimalDigitNumber && str[i] == '.' && !doneRadixPoint) {
					currentCat = UnicodeCategory.DecimalDigitNumber;
					doneRadixPoint = true;
				}
				
			}
			
			start = i;
			
			return sb.ToString();
			
		}
		
		private static Boolean CategoryEquals(UnicodeCategory x, UnicodeCategory y) {
			
			if( (int)x <= 2 && (int)y <= 2 ) return true; // both x and y are letters of any case
			
			return x == y;
			
		}
		
#endregion
		
#region Arrays
		
		public static String ToHexString(this Byte[] array) {
			
			StringBuilder sb = new StringBuilder( array.Length * 2 );
			for(int i=0;i<array.Length;i++) {
				
				sb.Append( array[i].ToString("X2", CultureInfo.InvariantCulture) );
			}
			
			return sb.ToString();
			
		}
		
		public static Byte[] SubArray(this Byte[] array, Int32 startIndex, Int32 length) {
			
			if(startIndex >= array.Length         ) throw new ArgumentOutOfRangeException("startIndex");
			if(startIndex + length >= array.Length) throw new ArgumentOutOfRangeException("length");
			
			Byte[] retval = new Byte[ length ];
			
			for(int i=0;i<length;i++) retval[i] = array[startIndex + i];
			
			return retval;
			
		}
		
		public static Int32 IndexOf(this Array array, Object item) {
			
			return Array.IndexOf( array, item );
			
		}
		
		public static Boolean Contains(this Array array, Object item) {
			return array.IndexOf( item ) > -1;
		}
		
		public static void AddRange2<T>(this IList<T> list, IEnumerable<T> items) {
			
			// not as fast as AddRange in List<T>, but it'll do
			
			foreach(T item in items) {
				list.Add( item );
			}
		}
		
#endregion
		
#region Binary Reader
		
		/// <summary>Reads a null-terminated string.</summary>
		public static String ReadSZString(this BinaryReader rdr) {
			
			StringBuilder sb = new StringBuilder();
			Char c;
			try {
				
				while( (c = rdr.ReadChar()) != 0 ) {
					sb.Append( c );
				}
				
			} catch(EndOfStreamException) {
			}
			
			return sb.ToString();
		}
		
		public static Byte[] Align2(this BinaryReader rdr) {
			
/*			Int64 pos = rdr.BaseStream.Position;
			pos = (pos + 1) & ~1;
			
			Int64 offset = pos - rdr.BaseStream.Position;
			
			return rdr.ReadBytes( (int)offset );*/
			
			Int64 pos = rdr.BaseStream.Position;
			Int32 rem = (int)(pos % 2L);
			
			return rdr.ReadBytes( rem );
		}
		
		public static Byte[] Align4(this BinaryReader rdr) {
			
			Int64 pos = rdr.BaseStream.Position;
			pos = (pos + 3) & ~3;
			
			Int64 offset = pos - rdr.BaseStream.Position;
			
			return rdr.ReadBytes( (int)offset );
		}
		
		public static Object ReadIdentifier(this BinaryReader rdr) {
			
			UInt16 word0 = rdr.ReadUInt16();
			if(word0 == 0x0000) return null;
			if(word0 == 0xFFFF) {
				// then the next UInt16 is a numeric identifier
				
				return rdr.ReadUInt16();
				
			} else {
				// then word0 is the first character in a null-terminated string
				// so concatenate it with the string it's about to read in
				
				Char c0 = (Char)word0;
				return c0 + rdr.ReadSZString();
				
			}
			
		}
		
#endregion
		
#region BinaryWriter
		
		public static void WriteSZString(this BinaryWriter wtr, String s) {
			
			Char[] chars = s.ToCharArray();
			
			wtr.Write( chars );
			wtr.Write( '\0' ); // by using char the writer's encoding will do it right (rather than using (ushort)0x00 or 0x00 on its own (which is a literal 4-byte int32)
			
		}
		
		public static void Align2(this BinaryWriter wtr) {
			
			Int64 pos = wtr.BaseStream.Position;
			Int32 rem = (int)(pos % 2L);
			
			Byte[] writeThis = new Byte[rem];
			writeThis.Initialize();
			
			wtr.Write( writeThis );
			
		}
		
		public static void Align4(this BinaryWriter wtr) {
			
			Int64 pos = wtr.BaseStream.Position;
			pos = (pos + 3) & ~3;
			
			Int64 rem = pos - wtr.BaseStream.Position;
			
			Byte[] writeThis = new Byte[rem];
			writeThis.Initialize();
			
			wtr.Write( writeThis );
			
		}
		
#endregion
		
#region Stream and IO
		
		public static void Write(this Stream stream, Byte[] buffer) {
			
			stream.Write(buffer, 0, buffer.Length);
		}
		
		public static FileInfo GetFile(this DirectoryInfo directory, String relativeFileName) {
			
			if( String.IsNullOrEmpty( relativeFileName ) ) throw new ArgumentNullException(relativeFileName);
			if( !directory.Exists ) throw new DirectoryNotFoundException("Could not find " + directory.FullName);
			
			if( relativeFileName[0] == '\\' || relativeFileName[0] == '/' ) relativeFileName = relativeFileName.Substring(1);
			
			String fileName = Path.Combine( directory.FullName, relativeFileName );
			
			return new FileInfo( fileName );
		}
		
		public static DirectoryInfo GetDirectory(this DirectoryInfo directory, String relativeDirName) {
			
			if( directory == null ) throw new ArgumentNullException("directory");
			if( String.IsNullOrEmpty( relativeDirName ) ) throw new ArgumentNullException(relativeDirName);
			
			directory.Refresh(); // there was a case where the directory does exist but the .Exists returns false even though it was created, you must call Refresh
			if( !directory.Exists ) throw new DirectoryNotFoundException("Could not find " + directory.FullName);
			
			if( relativeDirName[0] == '\\' || relativeDirName[0] == '/' ) relativeDirName = relativeDirName.Substring(1);
			
			String dirName = Path.Combine( directory.FullName, relativeDirName );
			
			return new DirectoryInfo( dirName );
		}
		
		public static Boolean IsEmpty(this DirectoryInfo directory) {
			
			directory.Refresh();
			
			if( directory.GetDirectories().Length > 0 ) return false;
			
			if( directory.GetFiles().Length > 0 ) return false;
			
			return true;
		}
		
		/// <summary>Recursively deletes a directory, listing all of the files it couldn't delete.</summary>
		public static String[] DeleteSafely(this DirectoryInfo directory) {
			
			// a directory must be empty before deleting
			
			List<String> undeletable = new List<String>();
			
			DeleteDirectory( directory, undeletable);
			
			return undeletable.ToArray();
			
		}
		
		private static void DeleteDirectory(DirectoryInfo directory, List<String> undeletable) {
			
			foreach(DirectoryInfo child in directory.GetDirectories()) {
				
				DeleteDirectory( child, undeletable );
			}
			
			foreach(FileInfo file in directory.GetFiles()) {
				
				try {
					file.Delete();
				} catch(IOException) {
					undeletable.Add( file.FullName );
				} catch(UnauthorizedAccessException) {
					undeletable.Add( file.FullName );
				}
				
			}
			
			try {
				if( directory.IsEmpty() ) directory.Delete();
			
			} catch(IOException) {
				
				undeletable.Add( directory.FullName );
			}
		}
		
#endregion
		
		public static Boolean IsGraphicsSupported(this PixelFormat pixelFormat) {
			
			return (pixelFormat & PixelFormat.Indexed) != PixelFormat.Undefined;
		}
		
		public static Boolean IsIndexed(this PixelFormat pixelFormat) {
			return (pixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed;
		}
		
		public static Dictionary<TKey,TValue> Clone<TKey,TValue>(this Dictionary<TKey,TValue> dict) {
			
			Dictionary<TKey,TValue> ret = new Dictionary<TKey,TValue>( dict.Keys.Count );
			foreach(TKey key in dict.Keys) {
				
				ret.Add( key, dict[key] );
			}
			
			return ret;
		}
		
	}
}
