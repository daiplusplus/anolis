using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Collections.ObjectModel;

namespace Anolis.Core.Utility {
	
	public class BootIni {
		
		private String _bootIniPath;
		
		private Collection<String>         _options;
		private Collection<BootIniOSEntry> _oses;
		
		private BootIniOSEntry _default;
		
		public static BootIni FromDefaultBootIni() {
			
			// NOTE: C:\Boot.ini is a hardcoded location. I don't know about if Windows is somehow installed to D:\
			// I think it's obtained with a lookup from HKLM\System\Setup\SystemPartition
			
			String defaultBootIni = @"C:\boot.ini";
			
			if( !File.Exists( defaultBootIni ) ) return null;
			
			return new BootIni( defaultBootIni );
		}
		
		public BootIni(String bootIniPath) {
			
			_bootIniPath = bootIniPath;
			
			List<String> lines = new List<String>();
			_oses    = new Collection<BootIniOSEntry>();
			_options = new Collection<String>();
			
			using(FileStream fs = new FileStream(bootIniPath, FileMode.Open, FileAccess.Read))
			using(StreamReader rdr = new StreamReader(fs)) {
				
				Boolean inOS = false;
				
				String line;
				while( (line = rdr.ReadLine()) != null ) {
					
					lines.Add( line );
					
					if( !inOS && !line.StartsWith("[", StringComparison.Ordinal) && !line.StartsWith("default", StringComparison.OrdinalIgnoreCase) ) {
						_options.Add( line );
					}
					
					if( line == "[operating systems]" ) inOS = true;
					
					else if( inOS ) {
						
						BootIniOSEntry os = new BootIniOSEntry( line );
						
						_oses.Add( os );
						
					}
					
				}
				
			}
			
			// get the default line
			foreach(String line in lines) {
				
				if( line.StartsWith("default=", StringComparison.OrdinalIgnoreCase) ) {
					
					// get the default OS
					String partitionPath = line.Substring("default=".Length);
					
					foreach(BootIniOSEntry entry in _oses) {
						
						if( entry.PartitionPath == partitionPath ) {
							_default = entry;
							break;
						}
					}
					
					break;
					
				}
			}
			
			if( _default == null ) throw new AnolisException("Couldn't find default OS");
			
		}
		
		public BootIniOSEntry DefaultOS {
			get { return _default; }
			set {
				if( value == null ) throw new ArgumentNullException("value");
				if( !_oses.Contains( value ) ) {
					_oses.Add( value );
				}
				_default = value;
			}
		}
		
		public Collection<BootIniOSEntry> OperatingSystems {
			get { return _oses; }
		}
		
		public Collection<String> Options {
			get { return _options; }
		}
		
		public void Save() {
			
			if( _default == null ) throw new AnolisException("A default OS must be specified");
			
			try {
				
				FileInfo bootIniFile = new FileInfo( _bootIniPath );
				if( bootIniFile.IsReadOnly ) bootIniFile.IsReadOnly = false;
				
				
				using(FileStream fs = new FileStream(_bootIniPath, FileMode.Truncate, FileAccess.Write, FileShare.None))
				using(StreamWriter wtr = new StreamWriter(fs, Encoding.ASCII)) {
					
					wtr.WriteLine("[boot loader]");
					
					foreach(String option in _options) {
						
						wtr.WriteLine( option );
					}
					
					wtr.WriteLine("default=" + _default.PartitionPath);
					
					wtr.WriteLine("[operating systems]");
					
					foreach(BootIniOSEntry os in _oses) {
						
						wtr.WriteLine( os.ToString() );
					}
					
				}
				
			} catch(UnauthorizedAccessException uae) {
				throw new AnolisException("Couldn't save Boot.ini (UAEX): " + uae.Message, uae);
			} catch(IOException iox) {
				throw new AnolisException("Couldn't save Boot.ini (IOEX): " + iox.Message, iox);
			}
			
		}
		
	}//class
	
	public class BootIniOSEntry {
			
		private String           _partitionPath;
		private String           _osName;
		private SwitchCollection _switches;
		
		public BootIniOSEntry(String line) {
			
			_switches     = new SwitchCollection();
			
			String allSwitches;
			
			using(StringReader rdr = new StringReader(line)) {
				
				State state = State.InPartitionPath;
				
				StringBuilder sb = new StringBuilder();
				
				Int32 nc;
				while( (nc = rdr.Read()) != -1 ) {
					
					Char c = (Char)nc;
					
					sb.Append( c );
					
					if( state == State.InPartitionPath && c == '=' ) {
						
						_partitionPath = sb.ToString().LeftFR(1);
						sb.Length = 0;
						continue;
					}
					
					if(c == '"' ) {
						
						if( state == State.InPartitionPath ) {
							
							state = State.InName;
							
							sb.Length = 0; // skip the first "
							
						} else if( state == State.InName ) {
							
							state = State.InSwitches;
							
							_osName = sb.ToString().LeftFR(1); // remove the last "
							
							sb.Length = 0;
							continue;
						}
						
					}
					
				}
				
				allSwitches = sb.ToString();
				
			}//using
			
			using(StringReader rdr = new StringReader(allSwitches)) {
				
				StringBuilder ss = new StringBuilder();
				
				Boolean inString = false;
				
				Int32 nc;
				while( (nc = rdr.Read()) != -1 ) {
					
					Char c = (Char)nc;
					
					ss.Append( c );
					
					if(c == '"' ) {
						inString = !inString;
					}
					
					if(c == ' ' && !inString) {
						
						String addThis = ss.ToString().Trim();
						if( !String.IsNullOrEmpty( addThis ) ) {
							
							_switches.Add( addThis );
						}
						
						ss.Length = 0;
					}
					
				}
				
				if( ss.Length > 0 ) _switches.Add( ss.ToString().Trim() );
				
			}
			
		}
		
		public String PartitionPath {
			get { return _partitionPath; }
			set { _partitionPath = value; }
		}
		
		public String OSName {
			get { return _osName; }
			set { _osName = value; }
		}
		
		public SwitchCollection Switches {
			get { return _switches; }
		}
		
		public override String ToString() {
			
			String str = _partitionPath + "=\"" + _osName + "\" ";
			
			foreach(String sw in _switches) {
				
				str += sw + " ";
			}
			
			return str.Trim();
			
		}
		
		private enum State {
			InPartitionPath,
			InName,
			InSwitches
		}
		
	}//class
	
	public class SwitchCollection : Collection<String> {
		
		public void AddSwitch(String item) {
			
			if( !Contains( item ) ) Add( item );
		}
		
		public new Boolean Contains(String item) {
			
			foreach(String sw in this) {
				if( String.Equals( sw, item, StringComparison.OrdinalIgnoreCase ) ) return true;
			}
			return false;
		}
		
	}
	
}
