using System;
using System.Drawing;
using System.Windows.Forms;
using Anolis.Core;
using Anolis.Core.Data;

namespace Anolis.Resourcer {
	
	public partial class ResourceListView : UserControl {
		
		private ImageList _images;
		
		public ResourceListView() {
			InitializeComponent();
			
			_images = new ImageList();
			__list.LargeImageList = _images;
			
			__tSize16 .Tag = new Size(16,16);
			__tSize32 .Tag = new Size(32,32);
			__tSize48 .Tag = new Size(48,48);
			__tSize128.Tag = new Size(128,128);
			__tSize256.Tag = new Size(256,256);
			
			__tIconSize.Tag = (Size)__tSize32.Tag;
			
			foreach(ToolStripItem item in __tIconSize.DropDownItems) {
				
				ToolStripDropDownItem dd = item as ToolStripDropDownItem;
				if(dd == null) continue;
				
				item.Click += new EventHandler(delegate(object sender, EventArgs e) { __tIconSize.Tag = ((ToolStripDropDownItem)sender).Tag;} );
				
			}
			
		}
		
		public void ShowResourceType(ResourceType type) {
			
			__list.Items.Clear();
			
			_images.ImageSize = (Size)__tIconSize.Tag;
			
			// SubItems:
			// 0: Name
			// 1: Type
			// 2: Lang[0] LangId
			// 3: Lang[0] Size
			
			foreach(ResourceName name in type.Names) {
				
				String[] subitems = GetSubItemsForName(name);
				if(subitems == null) continue;
				
				String imageListKey = name.Identifier.FriendlyName;
				
				Icon ico = GetIconForResourceName(name);
				
				ListViewItem item;
				
				if(ico != null) {
					
					_images.Images.Add( imageListKey, ico );
					item = new ListViewItem(subitems, imageListKey);
					
				} else {
					
					item = new ListViewItem(subitems);
				}
				
				__list.Items.Add( item );
				
			}
			
		}
		
		private String[] GetSubItemsForName(ResourceName name) {
			
			String lang, size;
			
			if(name.Langs.Count > 1) {
				
				lang = name.Langs.Count.ToString() + " Langs";
				size = String.Empty;
				
			} else if(name.Langs.Count == 1) {
				
				lang = name.Langs[0].LanguageId.ToString();
				size = name.Langs[0].Data.RawData.Length + " bytes";
				
			} else {
				
				return null;
			}
			
			return new String[] {
				name.Identifier.FriendlyName,
				name.Type.Identifier.FriendlyName,
				lang,
				size
			};
			
		}
		
		private Icon GetIconForResourceName(ResourceName name) {
			
			if(name.Langs.Count == 1) {
				
				ResourceData data = name.Langs[0].Data;
				
				IconDirectoryResourceData icoDir = data as IconDirectoryResourceData;
				if(icoDir != null) {
					
					IconDirectoryMember bestMember = icoDir.GetIconForSize( (Size)__tIconSize.Tag );
					
					if(bestMember == null) return null;
					
					return (bestMember.ResourceData as IconCursorImageResourceData).Icon;
					
				}
				
			}
			
			return null;
			
		}
		
		public void ShowResourceName(ResourceName name) {
			
			
			
		}
		
	}
}
