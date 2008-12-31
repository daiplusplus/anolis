using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;

using Anolis.Core.Native;
using System.Globalization;

namespace Anolis.Core.Data {
	
	public abstract class DirectoryResourceData : ResourceData {
		
		public    DirectoryMemberCollection    Members           { get; private set; }
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
		ResourceData ResourceData { get; }
	}
	
}
