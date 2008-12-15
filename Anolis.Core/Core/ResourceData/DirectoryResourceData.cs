using System;
using System.Collections.Generic;
using System.Text;

namespace Anolis.Core.Data {
	
	public abstract class DirectoryResourceData : ResourceData {
		
		protected DirectoryResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		// expose directory as a dictionary?
		
	}
	
	public sealed class IconDirectoryResourceData : DirectoryResourceData {
		
		private IconDirectoryResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] rawData, out ResourceData data) {
			
			data = null;
			return false;
			
			// TODO
			
		}

		public override string FileFilter {
			get { return "Icon File (*.ico)|.ico"; }
		}
		
	}
	
	public sealed class CursorDirectoryResourceData : DirectoryResourceData {
		
		private CursorDirectoryResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		public static Boolean TryCreate(ResourceLang lang, Byte[] rawData, out ResourceData data) {
			
			data = null;
			return false;
			
			// TODO
			
		}
		
		public override string FileFilter {
			get { return "Cursor File (*.cur)|.cur"; }
		}
		
	}
	
}
