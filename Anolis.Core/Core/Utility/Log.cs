using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Text;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Utility {
	
	public class Log : Collection<LogItem> {
		
		public void Add(LogSeverity severity, String message) {
			
			LogItem item = new LogItem(severity, message);
			
			this.Add( item );
		}
		
		public void Save(String path) {
			
			using(FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
			using(StreamWriter wtr = new StreamWriter(fs)) {
				
				foreach(LogItem item in this) {
					
					item.Write( wtr );
				}
				
			}
			
		}
		
		public Int32 CountNonNominal {
			get {
				
				Int32 ret = 0;
				foreach(LogItem item in this) {
					
					if( item.Severity != LogSeverity.Info ) ret++;
				}
				
				return ret;
			}
		}
		
	}
	
	public class LogItem {
		
		public LogItem(LogSeverity severity, String message) : this(severity, null, message) {
		}
		
		public LogItem(LogSeverity severity, Exception ex, String message) {
			
			Timestamp = DateTime.Now;
			
			Exception = ex;
			Severity  = severity;
			Message   = message;
		}
		
		public DateTime    Timestamp { get; private set; }
		public LogSeverity Severity  { get; private set; }
		public String      Message   { get; private set; }
		
		public Exception   Exception { get; private set; }
		
		public void Write(TextWriter wtr) {
			
			wtr.Write( Timestamp.ToString("s", Cult.InvariantCulture) );
			wtr.Write(" - ");
			wtr.Write( Severity.ToString() );
			wtr.Write(" - ");
			wtr.WriteLine( Message );
			
			Exception ex = Exception;
			Int32 indent = 1;
			while(ex != null) {
				
				String indentString = "".PadRight( indent, '\t' );
				
				wtr.WriteLine( indentString + ex.GetType().Name );
				wtr.WriteLine( indentString + ex.Message );
				
				String[] stackTrace = ex.StackTrace.Replace("\r\n", "\n").Split('\n');
				
				foreach(String call in stackTrace) {
					
					// indent the stack trace at (indent + 1)
					wtr.Write( indentString + '\t' );
					wtr.WriteLine( call );
				}
				
				ex = ex.InnerException;
				
				indent++;
			}
		}
		
		public void Write(StringBuilder sb) {
			
			StringWriter wtr = new StringWriter(sb);
			Write( wtr );
		}
		
	}
	
	public enum LogSeverity {
		Fatal,
		Error,
		Warning,
		Info
	}
}
