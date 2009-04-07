using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using Anolis.Core.Native;

namespace Anolis.Core.Data {
	
	public class MenuResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier typeId) {
			return (typeId.KnownType == Win32ResourceType.Menu) ? Compatibility.Yes : Compatibility.No;
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			return Compatibility.No;
		}
		
		public override ResourceData FromResource(ResourceLang lang, Byte[] data) {
			
			return MenuResourceData.TryCreate(lang, data);
		}
		
		public override String Name {
			get { throw new NotImplementedException(); }
		}
		
		public override String OpenFileFilter {
			get { throw new NotImplementedException(); }
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
		
		public Menu Menu { get; private set; }
		
		internal static MenuResourceData TryCreate(ResourceLang lang, Byte[] rawData) {
			
			if(rawData.Length < 12) throw new ResourceDataException("");
			
			// rawData is an array of MenuTemplate instances, aligned to DWORD boundaries (fun)
			
			List<MenuItem> items = new List<MenuItem>();
			// if the first byte is '1' then it's MenuEx, otherwise its an old-style menu
			
			using(MemoryStream stream = new MemoryStream(rawData))
			using(BinaryReader rdr = new BinaryReader( stream, Encoding.Unicode )) {
				
				Menu menu = (rawData[0] == 1) ? CreateEx(rdr) : CreateOld(rdr);
				
				MenuResourceData ret = new MenuResourceData(lang, rawData);
				ret.Menu = menu;
				
				return ret;
			}
			
		}
		
		private static Menu CreateOld(BinaryReader rdr) {
			
			
			
			return null;
		}
		
		private static Menu CreateEx(BinaryReader rdr) {
			
			List<MenuExTemplateItem> items = new List<MenuExTemplateItem>();
						
			MenuExTemplateHeader header = new MenuExTemplateHeader(rdr);
			// header.wOffset is bytes offset from the wOffset member
			// so if it's 4 then just ignore it since those 4 bytes are taken up by the dwHelpId member
			// if it's less than 4, throw
			int advance = header.wOffset - 4;
			if(advance < 0) throw new ResourceDataException("Invalid MenuExTemplateHeader wOffset value");
			rdr.ReadBytes( advance );
			
			while(rdr.BaseStream.Position < rdr.BaseStream.Length) {
				
				MenuExTemplateItem item = new MenuExTemplateItem(rdr);
				
				items.Add( item );
			}
			
			return null;
		}
		
		protected override void SaveAs(System.IO.Stream stream, String extension) {
			throw new NotImplementedException();
		}
		
		protected override String[] SupportedFilters {
			get { return new String[] {}; }
		}
		
		protected override ResourceTypeIdentifier GetRecommendedTypeId() {
			return new ResourceTypeIdentifier(Win32ResourceType.Menu);
		}
	}
	
	public class Menu {
		
		private IList<MenuItem> _children;
		
		public Menu(IList<MenuItem> children) {
			
			_children = children;
			Children = new MenuItemCollection( _children );
		}
		
		public MenuItemCollection Children { get; private set; }
		
	}
	
	public class MenuItem {
		
		private List<MenuItem> _children;
		
		public MenuItem(String text) {
			
			Text = text;
			
			_children = new List<MenuItem>();
			Children = new MenuItemCollection( _children );
		}
		
		public String             Text     { get; private set; }
		
		public MenuItemCollection Children { get; private set; }
		
	}
	
	public class MenuItemCollection : System.Collections.ObjectModel.ReadOnlyCollection<MenuItem> {
		
		public MenuItemCollection(IList<MenuItem> underlying) : base(underlying) {
		}
		
	}
	
	
}
