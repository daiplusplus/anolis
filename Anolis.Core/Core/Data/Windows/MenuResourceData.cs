using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using Anolis.Core.Native;
using Anolis.Core.Utility;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Core.Data {
	
	public class MenuResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			return (typeId.KnownType == Win32ResourceType.Menu) ? Compatibility.Yes : Compatibility.No;
		}
		
		public override Compatibility HandlesExtension(String fileNameExtension) {
			return Compatibility.No;
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			return MenuResourceData.TryCreate(lang, data);
		}
		
		public override String Name {
			get { return "Menu"; }
		}
		
		public override String OpenFileFilter {
			get { return null; }
		}
		
		public override ResourceData FromFileToAdd(Stream stream, string extension, ushort lang, ResourceSource currentSource) {
			throw new NotSupportedException();
		}
		
		public override ResourceData FromFileToUpdate(Stream stream, string extension, ResourceLang currentLang) {
			throw new NotSupportedException();
		}
	}
	
	public class MenuResourceData : ResourceData {
		
		private MenuResourceData(ResourceLang lang, Byte[] rawData) : base(lang, rawData) {
			
		}
		
		public DialogMenu Menu { get; private set; }
		
		internal static MenuResourceData TryCreate(ResourceLang lang, Byte[] rawData) {
			
			if(rawData.Length < 12) throw new ResourceDataException("Menus are at least 12 bytes long");
			
			// rawData is an array of MenuTemplate instances, aligned to DWORD boundaries (fun)
			
			// if the first byte is '1' then it's MenuEx, otherwise its an old-style menu
			
			using(MemoryStream stream = new MemoryStream(rawData))
			using(BinaryReader rdr = new BinaryReader( stream, Encoding.Unicode )) {
				
				DialogMenu menu;
				
				switch( rawData[0] ) {
					case 0:
						menu = Create(rdr);
						break;
					case 1:
						menu = CreateEx(rdr);
						break;
					default:
						throw new ResourceDataException("Unsupported Menu version word: '" + rawData[0].ToString(Cult.InvariantCulture) + "'");
				}
				
				MenuResourceData ret = new MenuResourceData(lang, rawData);
				ret.Menu = menu;
				
				return ret;
			}
			
		}
		
		private static DialogMenu Create(BinaryReader rdr) {
			
			List<MenuTemplateItem> itemTs = new List<MenuTemplateItem>();
			
			MenuTemplateHeader header = new MenuTemplateHeader(rdr);
			rdr.ReadBytes( header.wOffset );
			
			while(rdr.BaseStream.Position < rdr.BaseStream.Length) {
				
				MenuTemplateItem itemT = new MenuTemplateItem(rdr);
				itemTs.Add( itemT );
			}
			
			DialogMenuItem root = new DialogMenuItem("Root");
			
			lock(_buildLock) {
				
				i = 0;
				BuildMenu(root, itemTs);
				
			}
			
			return new DialogMenu(root);
		}
		
		private static Object _buildLock = new Object();
		private static Int32 i;
		
		private static void BuildMenu(DialogMenuItem current, List<MenuTemplateItem> itemTs) {
			
			while(i < itemTs.Count) {
				
				MenuTemplateItem itemT = itemTs[i++];
				
				DialogMenuItem item = new DialogMenuItem(itemT.mtString);
				
				if( HasChildren(itemT) ) {
					
					BuildMenu( item, itemTs );
					
				}
				
				current.Children.Add( item );
				
				if( IsLast(itemT) ) return;
				
			}
		
		}
		
		private static Boolean HasChildren(MenuTemplateItem itemT) {
			return (itemT.mtOption & MenuTemplateItemOptions.Popup)    == MenuTemplateItemOptions.Popup;
		}
		private static Boolean IsLast(MenuTemplateItem itemT) {
			return ((itemT.mtOption & MenuTemplateItemOptions.EndMenu) == MenuTemplateItemOptions.EndMenu);
		}
		
		private static DialogMenu CreateEx(BinaryReader rdr) {
			
			List<MenuExTemplateItem> itemTs = new List<MenuExTemplateItem>();
			
			MenuExTemplateHeader header = new MenuExTemplateHeader(rdr);
			// header.wOffset is bytes offset from the wOffset member
			// so if it's 4 then just ignore it since those 4 bytes are taken up by the dwHelpId member
			// if it's less than 4, throw
			int advance = header.wOffset - 4;
			if(advance < 0) throw new ResourceDataException("Invalid MenuExTemplateHeader wOffset value");
			rdr.ReadBytes( advance );
			
			while(rdr.BaseStream.Position < rdr.BaseStream.Length) {
				
				MenuExTemplateItem itemT = new MenuExTemplateItem(rdr);
				
				itemTs.Add( itemT );
			}
			
			DialogMenuItem root = new DialogMenuItem("Root");
			
			Int32 i = 0;
			BuildMenuEx(root, itemTs, ref i );
			
			return new DialogMenu(root);
		}
		
		private static void BuildMenuEx(DialogMenuItem current, List<MenuExTemplateItem> itemTs, ref Int32 pos) {
			
			for(int i=pos;i<itemTs.Count;i++) {
				
				MenuExTemplateItem itemT = itemTs[i];
				
				DialogMenuItem item = new DialogMenuItem(itemT.szText);
				
				if( HasChildren(itemT) ) {
					
					i++;
					BuildMenuEx( item, itemTs, ref i );
					
				}
				
				current.Children.Add( item );
				
				if( IsLast(itemT) ) return;
				
			}
			
		}
		
		private static Boolean HasChildren(MenuExTemplateItem itemT) {
			return (itemT.bResInfo & MenuExTemplateItemInfo.HasChildren) == MenuExTemplateItemInfo.HasChildren;
		}
		private static Boolean IsLast(MenuExTemplateItem itemT) {
			return (itemT.bResInfo & MenuExTemplateItemInfo.LastItem) == MenuExTemplateItemInfo.LastItem;
		}
		
		protected override void SaveAs(System.IO.Stream stream, String extension) {
			throw new NotSupportedException();
		}
		
		protected override String[] SupportedFilters {
			get { return new String[] {}; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier(Win32ResourceType.Menu);
		}
	}
	
}
