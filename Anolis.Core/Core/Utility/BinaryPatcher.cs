using System;
using System.IO;
using System.Collections.Generic;
using Anolis.Core.Native;

namespace Anolis.Core.Utility.BinaryPatch {
	
	public enum PatchCurrentStatus {
		/// <summary>Cannot determine the current patch status of the file</summary>
		Unknown,
		/// <summary>The file is already patched</summary>
		Yes,
		/// <summary>The file is currently unpatched</summary>
		No,
		/// <summary>The file shows some signs of being patched, but is inconclusive</summary>
		Unsure
	}
	
	public class Patch {
		
		public FileInfo     File;
		public PatchEntry[] Entries;
		
		public Patch(FileInfo file) {
			File = file;
		}
		
		private Object     _lock = new Object();
		
		private FileStream _fs;
		
		public Boolean CanPatch {
			get {
				foreach(PatchEntry entry in Entries) if( entry.Status != PatchCurrentStatus.No ) return false;
				return true;
			}
		}
		
		public void ApplyPatch() {
			
			// things like backing up the source and disabling WFP are the responsibility of callers
			
			lock( _lock ) {
				
				using( _fs = new FileStream( File.FullName, FileMode.Open, FileAccess.Write, FileShare.Read, 0x400)) {
					
					foreach(PatchEntry entry in Entries)
						if( entry.Status == PatchCurrentStatus.No )
							ApplyPatch( entry );
					
				}
				
			}
			
		}
		
		private void ApplyPatch(PatchEntry entry) {
			
			// sanity checking
			if( entry.Offset >= File.Length ) throw new InvalidDataException("offset out of range");
			if( entry.Offset == -1          ) throw new InvalidDataException("offset undefined");
			
			_fs.Seek( entry.Offset, SeekOrigin.Begin );
			
			_fs.Write( entry.Bytes, 0, entry.Bytes.Length );
			
		}
		
		
	}
	
	public class PatchEntry {
		
		public Int64  Offset = -1;
		public Byte[] Bytes;
		public Int32  Matches;
		public PatchCurrentStatus Status;
		
	}
	
	public class SearchCriteria {
		
		public Byte[] SignBytes;
		public Byte[] SignIndexen;
		public Int64  MagicIndex;
		
		public Byte[] PatchBytes;
		
		public Int64 SearchBegin;
		public Int64 SearchEnd;
		
	}
	
	[Serializable]
	public class PatchException : AnolisException {
		
		public PatchException() {
		}
		
		public PatchException(String message) : base(message) {
		}
		
		public PatchException(String message, Exception inner) : base(message, inner) {
		}
		
		protected PatchException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) {
		}
		
	} 
	
	public abstract class PatchFinder {
		
		public FileInfo File { get; private set; }
		
		protected PatchFinder(String fileName) {
			
			File = new FileInfo( fileName );
			
			if( !File.Exists ) throw new FileNotFoundException("The specified file does not exist", fileName);
		}
		
		public abstract Patch GetPatchStatus();
		
		protected Patch Search(SearchCriteria criteria) {
			
			if( criteria.SearchEnd >= File.Length ) criteria.SearchEnd = File.Length;
			
			Byte[] data;
			using(FileStream fs = new FileStream(File.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, 0x400))
			using(BinaryReader rdr = new BinaryReader(fs)) {
				
				fs.Seek( criteria.SearchBegin, SeekOrigin.Begin );
				
				int length = (int)(criteria.SearchEnd - criteria.SearchBegin);
				
				data = rdr.ReadBytes( length );
			}
			
			Patch ret = new Patch(File);
			List<PatchEntry> entries = new List<PatchEntry>();
			
			/////////////////////////////////
			// Do the search
			
			for(int i=0;i<data.Length-64;i++) {
				
				if(
				     data[i]                            == criteria.SignBytes[0] &&
				     data[i + criteria.SignIndexen[1] ] == criteria.SignBytes[1]  ) {
					///////////////////////////////////////////////////////////////
					// if the first 2 match, try the rest
					
					Boolean matched = true;
					
					for(int j=2;j<criteria.SignBytes.Length;j++) {
						
						if( data[i + criteria.SignIndexen[j] ] != criteria.SignBytes[j] ) {
							matched = false;
							break;
						}
						
					}
					
					if( matched ) {
						// let the subclasses take it from here
						PatchEntry entry = OnMatch(data, i, criteria);
						
						if( entry != null) entries.Add( entry );
						
					}
					
				}//if
				
			}//for
			
			ret.Entries = entries.ToArray();
			
			return ret;
			
		}
		
		protected abstract PatchEntry OnMatch(Byte[] data, Int32 i, SearchCriteria criteria);
		
	}
	
	public class UXThemePatchFinderFactory {
		
		public static PatchFinder Create(String fileName) {
			
			if( !File.Exists( fileName ) ) throw new FileNotFoundException("Specified filed does not exist", fileName);
			
			Version osv = Environment.OSVersion.Version;
			
			MachineType type = Miscellaneous.GetMachineType( fileName );
			switch(type) {
				case MachineType.I386:
					
					if( osv.Major == 5 && osv.Minor >= 1 ) {
						// XP x86
						// WS2003 x86
						
						return new UXThemeXP32Finder(fileName);
						
					} else if( osv.Major == 6 ) {
						// Vista / Win7 x86
						
						return new UXThemeVi32Finder(fileName);
						
					}
					break;
					
				case MachineType.Amd64:
					
					if( osv.Major == 5 && osv.Minor == 2 ) {
						//XP / WS2003 x64
						
						return new UXThemeXP64Finder(fileName);
						
					} else if( osv.Major == 6 ) {
						// Vista / Win7 x64
						
						return new UXThemeVi64Finder(fileName);
					}
					break;
					
			}
			
			return null;
			
		}
		
	}
	
	public class UXThemeXP32Finder : PatchFinder {
		
		public UXThemeXP32Finder(String fileName) : base(fileName) {
		}
		
		public override Patch GetPatchStatus() {
			
			SearchCriteria criteria = new SearchCriteria() {
				SignBytes   = new Byte[] {0x8B, 0xF6, 0x8B, 0xFF, 0x55, 0x8B, 0xEC, 0x56},
				SignIndexen = new Byte[] {   0,   37,   19,    1,    2,    3,   4,    22},
				MagicIndex  = 5,
				SearchBegin = 0x10000,
				SearchEnd   = 0x50000,
				PatchBytes  = new Byte[] {0x33, 0xF6, 0x8B, 0xC6, 0xC9, 0xC2, 0x08, 0x00}
			};
			
			return Search( criteria );
			
		}
		
		protected override PatchEntry OnMatch(Byte[] data, Int32 i, SearchCriteria criteria) {
			
			PatchEntry entry = new PatchEntry();
			entry.Status = PatchCurrentStatus.Unknown;
			
			if( data[ i + criteria.MagicIndex     ] == 0x33 &&
			    data[ i + criteria.MagicIndex + 1 ] == 0xF6 ) {
				
				entry.Status = PatchCurrentStatus.Yes;
				
			} else
			
			if( data[ i + criteria.MagicIndex     ] == 0x81 &&
			    data[ i + criteria.MagicIndex + 1 ] == 0xEc ) {
				
				entry.Status = PatchCurrentStatus.No;
				
			} else
			
			if( data[ i + criteria.MagicIndex ] == 0x33 ) {
				
				entry.Status = PatchCurrentStatus.Unsure;
			}
			
			if( entry.Status != PatchCurrentStatus.Unknown ) {
				
				entry.Offset = criteria.SearchBegin + i + criteria.MagicIndex;
			}
			
			return entry;
		}
		
	}
	
	public class UXThemeXP64Finder : PatchFinder {
		
		public UXThemeXP64Finder(String fileName) : base(fileName) {
		}
		
		public override Patch GetPatchStatus() {
			
			SearchCriteria criteria = new SearchCriteria() {
				SignBytes   = new Byte[] {0x4C, 0x48, 0x45, 0x8B, 0xDC, 0x48, 0x8B, 0x89, 0x4C, 0x8B, 0x11},
				SignIndexen = new Byte[] {   0,   17,   40,    1,    2,   10,   11,   18,   25,   26,   27},
				MagicIndex  = 3,
				SearchBegin = 0x02000,
				SearchEnd   = 0x50000,
				PatchBytes  = new Byte[] {0x48, 0x31, 0xC0, 0xC3, 0x00, 0x00, 0x00}
			};
			
			return Search( criteria );
			
		}
		
		protected override PatchEntry OnMatch(Byte[] data, Int32 i, SearchCriteria criteria) {
			
			PatchEntry entry = new PatchEntry();
			entry.Status = PatchCurrentStatus.Unknown;
			
			if( data[ i + criteria.MagicIndex     ] == 0x48 &&
			    data[ i + criteria.MagicIndex + 1 ] == 0x31 ) {
				
				entry.Status = PatchCurrentStatus.Yes;
				
			} else 
			
			if( data[ i + criteria.MagicIndex ] == 0x48 &&
			    data[ i + criteria.MagicIndex + 1] == 0x81 ) {
				
				entry.Status = PatchCurrentStatus.No;
				
			}
			
			if( entry.Status != PatchCurrentStatus.Unknown ) {
				
				entry.Offset = criteria.SearchBegin + i + criteria.MagicIndex;
				
			}
			
			return entry;
		}
		
	}
	
	public class UXThemeVi32Finder : PatchFinder {
		
		public UXThemeVi32Finder(String fileName) : base(fileName) {
		}
		
		public override Patch GetPatchStatus() {
			
			SearchCriteria criteria = new SearchCriteria() {
				SignBytes   = new Byte[] {0x8B, 0x33, 0x8B, 0xFF, 0x55, 0x8B, 0xEC},
				SignIndexen = new Byte[] {   0,   16,   23,    1,    2,    3,    4},
				MagicIndex  = 5,
				SearchBegin = 0x02000,
				SearchEnd   = 0x50000,
				PatchBytes  = new Byte[] {0x33, 0xC0, 0xC9, 0xC2, 0x04, 0x00}
			};
			
			return Search(  criteria );
			
		}
		
		protected override PatchEntry OnMatch(Byte[] data, Int32 i, SearchCriteria criteria) {
			
			PatchEntry entry = new PatchEntry();
			entry.Status = PatchCurrentStatus.Unknown;
			
			// Pre-Win7 M1
			if( data[ i + 33 ] == 0xF6 &&
			    data[ i + 34 ] == 0xD8 ) {
				
				
				if( data[ i + criteria.MagicIndex     ] == 0x33 &&
				    data[ i + criteria.MagicIndex + 1 ] == 0xC0 ) {
					
					entry.Status = PatchCurrentStatus.Yes;
					
				} else
				
				if( data[ i + criteria.MagicIndex     ] == 0x81 &&
				    data[ i + criteria.MagicIndex + 1 ] == 0xEC ) {
					
					entry.Status = PatchCurrentStatus.No;
					
				}
				
				
			} else 
			
			if( data[ i + 33 ] == 0x0F &&
			    data[ i + 36 ] == 0xF7 ) {
				
				if( data[ i + criteria.MagicIndex     ] == 0x33 &&
				    data[ i + criteria.MagicIndex + 1 ] == 0xC0 ) {
					
					entry.Status = PatchCurrentStatus.Yes;
					
				} else
				
				if( data[ i + criteria.MagicIndex     ] == 0x81 &&
				    data[ i + criteria.MagicIndex + 1 ] == 0xEC ) {
					
					entry.Status = PatchCurrentStatus.No;
					
				}
				
			}
			
			if( entry.Status != PatchCurrentStatus.Unknown ) {
				
				entry.Offset = criteria.SearchBegin + i + criteria.MagicIndex;
			}
			
			return entry;
			
		}
		
	}
	
	public class UXThemeVi64Finder : PatchFinder {
		
		public UXThemeVi64Finder(String fileName) : base(fileName) {
		}
		
		public override Patch GetPatchStatus() {
			
			SearchCriteria criteria = new SearchCriteria() {
				SignBytes   = new Byte[] {0x48, 0x8B, 0x48, 0x89, 0x55, 0x56, 0x57, 0x48, 0x08, 0x8B, 0xF9},
				SignIndexen = new Byte[] {   0,   16,   33,    1,    5,    6 ,   7,   22,   39,   34,   35},
				MagicIndex  = 8,
				SearchBegin = 0x02000,
				SearchEnd   = 0x80000,
				PatchBytes  = new Byte[] {0x33, 0xDB, 0x8B, 0xC3, 0x5F, 0x5E, 0x5D, 0xC3}
			};
			
			return Search( criteria );
			
		}
		
		protected override PatchEntry OnMatch(Byte[] data, Int32 i, SearchCriteria criteria) {
			
			PatchEntry entry = new PatchEntry();
			entry.Status = PatchCurrentStatus.Unknown;
			
			if( data[ i + criteria.MagicIndex ] == 0x33 &&
			    data[ i + 15                  ] == 0xC3 ) {
				
				// this file is patched
				entry.Status = PatchCurrentStatus.Yes;
				
			} else 
			
			if( data[ i + criteria.MagicIndex     ] == 0x48 &&
			    data[ i + criteria.MagicIndex + 1 ] == 0x81 &&
			    data[ i + 15                      ] == 0x48 ) {
				
				// then it isn't patch
				entry.Status = PatchCurrentStatus.No;
				
			} else 
			
			if( data[ i + criteria.MagicIndex ] == 0x33 ) {
				
				entry.Status = PatchCurrentStatus.Unsure;
				
			}
			
			if( entry.Status != PatchCurrentStatus.Unknown ) {
				
				entry.Offset = criteria.SearchBegin + i + criteria.MagicIndex;
			}
			
			return entry;
			
		}
		
	}
	
	
}
