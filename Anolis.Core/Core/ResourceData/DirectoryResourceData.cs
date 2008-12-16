using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Anolis.Core.Data {
	
	public abstract class DirectoryResourceData : ResourceData {
		
		public DirectoryMemberCollection Members { get; private set; }
		
		protected Collection<IDirectoryMember> UnderlyingMembers { get; private set; }
		
		protected DirectoryResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
			
			UnderlyingMembers = new Collection<IDirectoryMember>();
			Members           = new DirectoryMemberCollection( UnderlyingMembers );
			
		}
		
	}
	
	public sealed class DirectoryMemberCollection : ReadOnlyCollection<IDirectoryMember> {
		public DirectoryMemberCollection(IList<IDirectoryMember> list) : base(list) {
		}
	}
	
	public interface IDirectoryMember {
		String Description { get; }
		ResourceData GetResourceData();
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
