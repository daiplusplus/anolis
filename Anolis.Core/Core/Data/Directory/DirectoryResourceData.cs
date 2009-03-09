using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;

using Anolis.Core.Native;
using System.Globalization;

namespace Anolis.Core.Data {
	
	public abstract class DirectoryResourceData : ResourceData {
		
		protected DirectoryResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
		}
		
		protected internal override void OnRemove(Boolean underlyingDelete, Remove deleteFunction) {
			
			foreach(IDirectoryMember member in Members) {
				
				deleteFunction( member.ResourceData.Lang );
			}
			
		}
		
		public abstract DirectoryMemberCollection Members { get; }
		
	}
	
	public sealed class DirectoryMemberCollection : ReadOnlyCollection<IDirectoryMember> {
		public DirectoryMemberCollection(IList<IDirectoryMember> list) : base(list) {
		}
	}
	
	public interface IDirectoryMember : IComparable<IDirectoryMember> {
		String Description { get; }
		ResourceData ResourceData { get; }
	}
	
}
