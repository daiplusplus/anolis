using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;

using Anolis.Core.Native;
using System.Globalization;

using Anolis.Core.Utility;

namespace Anolis.Core.Data {
	
	public abstract class DirectoryResourceDataFactory : ResourceDataFactory {
		
		protected IconOptions GetMergeOptions() {
			
			IconOptions ret = new IconOptions();
			
			ret.SizeLimit      = (Int32)  FactoryOptions.Instance[ IconSizeLimit ];
			ret.ClearOriginals = (Boolean)FactoryOptions.Instance[ IconClearOriginal ];
			
			return ret;
		}
		
		public const String IconSizeLimit     = @"iconSizeLimit";
		public const String IconClearOriginal = @"iconClearOriginal";
		
		public override void RegisterOptions() {
			
			FactoryOptions.Instance.AddDefault( IconSizeLimit    ,     0, typeof(Int32)  , "Ignore icon subimages larger than or equal to this value. Set to 0 to recognise all subimages regardless of size.", "Icons" );
			FactoryOptions.Instance.AddDefault( IconClearOriginal, false, typeof(Boolean), "Clears all existing subimages before adding these new subimages, rather than adding these subimages"              , "Icons" );
		}
		
	}
	
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
