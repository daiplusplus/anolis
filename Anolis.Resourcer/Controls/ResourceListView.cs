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
			
			__bg.DoWork += new System.ComponentModel.DoWorkEventHandler(PopulateResourceType);
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
			
			Boolean showIcons = true;
			
			if(type.Names.Count > 256) {
				
				String message = String.Format("There are {0} resource icons to display. This might take a long time, do you want to show the icons?", type.Names.Count);
				
				DialogResult r = MessageBox.Show(this, message, "Anolis Resourcer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				showIcons = r == DialogResult.Yes;
				
			}
			
			__bg.RunWorkerAsync( new Object[] { showIcons, type.Names } );
			__bg.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(__bg_ProgressChanged);
		}
		
		private void __bg_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e) {
			
			int p = e.ProgressPercentage;
			if( p > 100 ) p = 100;
			else if( p < 0 ) p = 0;
			
			__progessBar.Value = p;
			
		}
		
		private void PopulateResourceType(Object sender, System.ComponentModel.DoWorkEventArgs e) {
			
			Object[] args = e.Argument as Object[];
			
			Boolean showIcons            = (Boolean)args[0];
			ResourceNameCollection names = args[1] as ResourceNameCollection;
			
			Invoke( new MethodInvoker(delegate() { __list.BeginUpdate(); } ) );
			
			Single nof = names.Count;
			Single i   = 1;
			
System.Diagnostics.Stopwatch swImageList = new System.Diagnostics.Stopwatch();
			
			foreach(ResourceName name in names) {
				
				ListViewItem item;
				
				if(name.Langs.Count == 0) continue;
				
				if(showIcons) {
					
					String imageListKey = name.Identifier.FriendlyName;
					
					ListViewThumb thumb = GetIconForResourceName(name);
					
					String[] subitems = GetSubItemsForName(name);
					
					if(thumb != null) {
						
swImageList.Start();
						
						if(thumb.IsIcon) {
							
							// I'd like to mention that ImageList is VERY slow for working with loads (i.e. >100 ) of Icons
							// so let's try converting it to a bitmap and seeing what happens
							
							Bitmap bmp = thumb.Icon.ToBitmap();
							
							//Invoke( new MethodInvoker(delegate() { _images.Images.Add( imageListKey, thumb.Icon ); } ) );
							Invoke( new MethodInvoker(delegate() { _images.Images.Add( imageListKey, bmp ); } ) );
							
						} else {
							
							Invoke( new MethodInvoker(delegate() { _images.Images.Add( imageListKey,  thumb.Image ); } ) );
							
						}
						
swImageList.Stop();
						
						item = new ListViewItem(subitems, imageListKey );
						
					} else {
						
						item = new ListViewItem(subitems);
					}
					
					
				} else {
					
					item = new ListViewItem(name.Identifier.FriendlyName);
					
				}
				
				item.Tag = name;
				
				// does this create a new MethodInvoker for every call?
				Invoke( new MethodInvoker(delegate() { __list.Items.Add( item ); }) );
				
				i++;
				
				__bg.ReportProgress( Convert.ToInt32( 100 * i / nof ) );
			}
			
			Invoke( new MethodInvoker(delegate() { __list.EndUpdate(); }) );
			
		}
		
		private class ListViewThumb {
			
			public ListViewThumb(Icon ico) {
				Icon = ico;
				IsIcon = true;
			}
			
			public ListViewThumb(Image img) {
				Image = img;
				IsIcon = false;
			}
			
			public Boolean IsIcon;
			public Icon Icon;
			public Image Image;
		}
		
		/// <remarks>Returns null if the ResourceName has no ResourceLang children</remarks>
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
		
		private ListViewThumb GetIconForResourceName(ResourceName name) {
			
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
				
				ListViewThumb thumb = GetIconForResourceLang(lang);
				
				ListViewItem item;
				
				if(thumb != null) {
					
					if(thumb.IsIcon) {
						
						_images.Images.Add( imageListKey, thumb.Icon );
						
					} else {
						
						_images.Images.Add( imageListKey, thumb.Image );
					}
					
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
		
		private ListViewThumb GetIconForResourceLang(ResourceLang lang) {
			
			ResourceData data = lang.Data;
			
			if(data is IconDirectoryResourceData) {
				
				IconDirectoryResourceData icoDir = data as IconDirectoryResourceData;
				IconDirectoryMember bestMember = icoDir.GetIconForSize( (Size)__tIconSize.Tag );
				
				if(bestMember == null) return null;
				
				IconCursorImageResourceData rd = (bestMember.ResourceData as IconCursorImageResourceData);
				if(rd.Icon == null) {
					return new ListViewThumb( rd.Image );
				} else {
					return new ListViewThumb( rd.Icon );
				}
				
				
				
			} else if(data is IconCursorImageResourceData) {
				
				IconCursorImageResourceData icoImg = data as IconCursorImageResourceData;
				if(icoImg.Icon == null) {
					return new ListViewThumb( icoImg.Image );
				} else {
					return new ListViewThumb( icoImg.Icon );
				}
				
			} else if(data is ImageResourceData) {
				
				ImageResourceData imgData = data as ImageResourceData;
				Size s = (Size)__tIconSize.Tag;
				
				Image thumb = imgData.Image.GetThumbnailImage( s.Width, s.Height, new Image.GetThumbnailImageAbort(delegate() { return true; }), IntPtr.Zero);
				
				return new ListViewThumb( thumb );
				
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
