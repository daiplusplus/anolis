using System;
using System.Drawing;
using System.Windows.Forms;
using Anolis.Core;
using Anolis.Core.Data;

namespace Anolis.Resourcer.Controls {
	
	public partial class ResourceListView : UserControl {
		
		private ImageList _images;
		private ResourceListViewMode _mode;
		
		public ResourceListView() {
			InitializeComponent();
			
			_images = new ImageList();
			_images.ColorDepth = ColorDepth.Depth32Bit;
			
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
			
			__list.SelectedIndexChanged += new EventHandler(__list_SelectedIndexChanged);
			__list.ItemActivate         += new EventHandler(__list_ItemActivate);
			
		}
		
#region Events
		
		private void __list_ItemActivate(object sender, EventArgs e) {
			
			Object tag = __list.SelectedItems.Count == 1 ? __list.SelectedItems[0].Tag : null;
			if(tag == null) return;
			
			OnItemActivated( new ResourceListViewEventArgs( tag, _mode ) );
			
		}
		
		private void __list_SelectedIndexChanged(object sender, EventArgs e) {
			
			Object tag = __list.SelectedItems.Count == 1 ? __list.SelectedItems[0].Tag : null;
			
			OnSelectedItemChanged( new ResourceListViewEventArgs( tag, _mode ) );
			
		}
		
		public event EventHandler<ResourceListViewEventArgs> SelectedItemChanged;
		
		public event EventHandler<ResourceListViewEventArgs> ItemActivated;
		
		protected void OnSelectedItemChanged(ResourceListViewEventArgs e) {
			
			if( SelectedItemChanged != null ) {
				SelectedItemChanged(this, e);
			}
			
		}
		
		protected void OnItemActivated(ResourceListViewEventArgs e) {
			
			if(ItemActivated != null) {
				ItemActivated(this, e);
			}
			
		}
		
#endregion
		
		public void ShowResourceType(ResourceType type) {
			
			_mode = ResourceListViewMode.Name;
			
			__list.Items.Clear();
			_images.Images.Clear();
			
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
				
				ListViewItem item ;
				
				if(ico != null) {
					
					_images.Images.Add( imageListKey, ico );
					item = new ListViewItem(subitems, imageListKey );
					
				} else {
					
					item = new ListViewItem(subitems);
				}
				
				item.Tag = name;
				
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
				
				return GetIconForResourceLang( name.Langs[0] );
			}
			
			return null;
			
		}
		
		public void ShowResourceName(ResourceName name) {
			
			_mode = ResourceListViewMode.Lang;
			
			__list.Items.Clear();
			_images.Images.Clear();
			
			_images.ImageSize = (Size)__tIconSize.Tag;
			
			foreach(ResourceLang lang in name.Langs) {
				
				String[] subitems = GetSubItemsForLang(lang);
				if(subitems == null) continue;
				
				String imageListKey = lang.LanguageId.ToString();
				
				Icon ico = GetIconForResourceLang(lang);
				
				ListViewItem item;
				
				if(ico != null) {
					_images.Images.Add( imageListKey, ico );
					item = new ListViewItem(subitems, imageListKey );
				} else {
					
					item = new ListViewItem(subitems);
					
				}
				
				item.Tag = lang;
				
				__list.Items.Add( item );
				
			}
			
		}
		
		private String[] GetSubItemsForLang(ResourceLang lang) {
			
			return new String[] {
				lang.LanguageId.ToString(),
				lang.Name.Type.Identifier.FriendlyName,
				"",
				lang.Data.RawData.Length + " bytes"
			};
			
		}
		
		private Icon GetIconForResourceLang(ResourceLang lang) {
			
			ResourceData data = lang.Data;
			
			if(data is IconDirectoryResourceData) {
				
				IconDirectoryResourceData icoDir = data as IconDirectoryResourceData;
				IconDirectoryMember bestMember = icoDir.GetIconForSize( (Size)__tIconSize.Tag );
				
				if(bestMember == null) return null;
				
				return (bestMember.ResourceData as IconCursorImageResourceData).Icon;
				
			} else if(data is IconCursorImageResourceData) {
				
				IconCursorImageResourceData icoImg = data as IconCursorImageResourceData;
				return icoImg.Icon;
				
			} else if(data is ImageResourceData) {
				
				// TODO: return a thumbnail
				return null;
				
			} else {
				
				// choose an icon based on the ResourceType
				return null;
				
			}
			
		}
		
	}
	
	public class ResourceListViewEventArgs : EventArgs {
		
		public ResourceListViewEventArgs(Object selectedItem, ResourceListViewMode mode) {
			Mode = mode;
			SelectedItem = selectedItem;
		}
		
		public ResourceListViewMode  Mode         { get; private set; }
		public Object                SelectedItem { get; private set; }
		
	}
	
	public enum ResourceListViewMode {
		Type, Name, Lang
	}
	
	public class ResourceListViewItemActivatedEventArgs : EventArgs {
		
		public ResourceListViewItemActivatedEventArgs(ResourceData activatedItem) {
			
		}
		
	}
	
}
