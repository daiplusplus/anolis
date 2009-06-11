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
		
		/// <summary>When this directory is removed from the ResourceSource it will ensure all child (member) directory members are removed with it</summary>
		protected internal override void OnRemove(Boolean underlyingDelete, RemoveFunction removeFunction) {
			
			foreach(IDirectoryMember member in Members) {
				
				removeFunction( member.ResourceData.Lang );
			}
			
		}
		
		public abstract DirectoryMemberCollection Members { get; }
		
	}
	
	public sealed class DirectoryMemberCollection : ReadOnlyCollection<IDirectoryMember> {
		public DirectoryMemberCollection(IList<IDirectoryMember> list) : base(list) {
		}
	}
	
	public interface IDirectoryMember : IComparable<IDirectoryMember>, IEquatable<IDirectoryMember> {
		String Description { get; }
		ResourceData ResourceData { get; }
	}
	
}
