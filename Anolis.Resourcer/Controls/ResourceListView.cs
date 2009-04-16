using System;
using System.Drawing;
using System.Windows.Forms;
using Anolis.Core;
using Anolis.Core.Data;

namespace Anolis.Resourcer.Controls {
	
	public partial class ResourceListView : UserControl {
		
		private ResourceListViewMode _mode;
		
		private Object _currentObject;
		
		public ResourceListView() {
			InitializeComponent();
			
			__list.SelectedIndexChanged += new EventHandler(__list_SelectedIndexChanged);
			__list.ItemActivate         += new EventHandler(__list_ItemActivate);
			
			__size16.Click += new EventHandler(__size_Click); __size16.Tag = new Size(16, 16);
			__size32.Click += new EventHandler(__size_Click); __size32.Tag = new Size(32, 32);
			__size96.Click += new EventHandler(__size_Click); __size96.Tag = new Size(96, 96);
			
			// bgq = feed-in queue for background worker which is used to wait for __bg to finish without blocking the main thread (see the calls to Invoke() )
			__bg.WorkerSupportsCancellation = true;
			__bg.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(__bg_ProgressChanged);
			__bg.DoWork += new System.ComponentModel.DoWorkEventHandler(PopulateResourceType);
			__bgq.DoWork += new System.ComponentModel.DoWorkEventHandler(__bgq_DoWork);
		}
		
		private void __size_Click(object sender, EventArgs e) {
			
			SetIconSize( (Size)(sender as ToolStripButton).Tag );
			
			__size16.Checked = sender == __size16;
			__size32.Checked = sender == __size32;
			__size96.Checked = sender == __size96;
			
		}
		
		public Object CurrentObject { get { return _currentObject; } }
		
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
		
		private Boolean _showingResourceType;
		
		public void ShowResourceType(ResourceType type) {
			
			_currentObject = type;
			_mode = ResourceListViewMode.Name;
			
			__list.Items.Clear();
			__images.Images.Clear();
			__list.LargeImageList = __images;
			
			// SubItems:
			// 0: Name
			// 1: Type
			// 2: Lang[0] LangId
			// 3: Lang[0] Size
			
			__bg.CancelAsync();
			
			Boolean showIcons = true;
			
			if(type.Names.Count > 256) {
				
				String message = String.Format("There are {0} resource icons to display. This might take a long time, do you want to show the icons?", type.Names.Count);
				
				DialogResult r = MessageBox.Show(this, message, "Anolis Resourcer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				showIcons = r == DialogResult.Yes;
				
			}
			
			__bgq.RunWorkerAsync( new Object[] { showIcons, type.Names } );
		}
		
		private void __bgq_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
			
			while(_showingResourceType) {
				System.Threading.Thread.Sleep(5);
			}
			
			__bg.RunWorkerAsync( e.Argument );
			
		}
		
		private void __bg_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e) {
			
			int p = e.ProgressPercentage;
			if( p > 100 ) p = 100;
			else if( p < 0 ) p = 0;
			
			this.BeginInvoke( new MethodInvoker( delegate() {
				__progessBar.Value = p;
			}));
			
		}
		
		private void PopulateResourceType(Object sender, System.ComponentModel.DoWorkEventArgs e) {
			
			_showingResourceType = true;
			
			Object[] args = e.Argument as Object[];
			
			Boolean showIcons            = (Boolean)args[0];
			ResourceNameCollection names = args[1] as ResourceNameCollection;
			
			BeginInvoke( new MethodInvoker(delegate() { __list.BeginUpdate(); } ) );
			
			Single nof = names.Count;
			Single i   = 1;
			
			foreach(ResourceName name in names) {
				
				if(__bg.CancellationPending) {
					e.Cancel = true;
					break;
				}
				
				ListViewItem item;
				
				if(name.Langs.Count == 0) continue;
				
				if(showIcons) {
					
					String imageListKey = name.Identifier.FriendlyName;
					
					ListViewThumb thumb = GetIconForResourceName(name);
					
					String[] subitems = GetSubItemsForName(name);
					
					if(thumb != null) {
						
						if(thumb.IsIcon) {
							
							// I'd like to mention that ImageList is VERY slow for working with loads (i.e. >100 ) of Icons
							// so let's try converting it to a bitmap and seeing what happens
							
							Bitmap bmp = thumb.Icon.ToBitmap();
							
							//Invoke( new MethodInvoker(delegate() { _images.Images.Add( imageListKey, thumb.Icon ); } ) );
							BeginInvoke( new MethodInvoker(delegate() { __images.Images.Add( imageListKey, bmp ); } ) );
							
						} else {
							
							BeginInvoke( new MethodInvoker(delegate() { __images.Images.Add( imageListKey,  thumb.Image ); } ) );
							
						}
						
						item = new ListViewItem(subitems, imageListKey );
						
					} else {
						
						item = new ListViewItem(subitems);
					}
					
					
				} else {
					
					item = new ListViewItem(name.Identifier.FriendlyName);
					
				}
				
				item.Tag = name;
				
				// does this create a new MethodInvoker for every call?
				BeginInvoke( new MethodInvoker(delegate() { __list.Items.Add( item ); }) );
				
				i++;
				
				__bg.ReportProgress( Convert.ToInt32( 100 * i / nof ) );
			}
			
			BeginInvoke( new MethodInvoker(delegate() { __list.EndUpdate(); }) );
			
			_showingResourceType = false;
		}
		
		private void SetIconSize(Size size) {
			
			__images.ImageSize = size;
			
			// reload
			
			switch(_mode) {
				case ResourceListViewMode.Type:
					
					ShowResourceSource( _currentObject as ResourceSource );
					break;
				case ResourceListViewMode.Name:
					
					ShowResourceType( _currentObject as ResourceType );
					break;
				case ResourceListViewMode.Lang:
					ShowResourceName( _currentObject as ResourceName );
					break;
			}
			
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
		
		public void ShowResourceSource(ResourceSource source) {
			
			_currentObject = source;
			
			_mode = ResourceListViewMode.Type;
			__list.BeginUpdate();
			__list.Items.Clear();
			__list.LargeImageList = __images.ImageSize.Width == 16 ? MainForm.TypeImages16 : MainForm.TypeImages32;
			
			foreach(ResourceType type in source.AllTypes) {
				
				ListViewItem item = new ListViewItem( type.Identifier.FriendlyName );
				item.ImageKey = MainForm.TreeNodeImageListTypeKey( type.Identifier );
				
				item.Tag = type;
				
				__list.Items.Add( item );
				
			}
			
			__list.EndUpdate();
			
		}
		
		public void ShowResourceName(ResourceName name) {
			
			// this isn't using background worker because a ResourceName rarely has more than a single data
			// and when it does they're often loaded fast enough
			
			_currentObject = name;
			
			_mode = ResourceListViewMode.Lang;
			
			__list.BeginUpdate();
			__list.Items.Clear();
			__list.LargeImageList = __images;
			__images.Images.Clear();
			
			foreach(ResourceLang lang in name.Langs) {
				
				String[] subitems = GetSubItemsForLang(lang);
				if(subitems == null) continue;
				
				String imageListKey = lang.LanguageId.ToString();
				
				ListViewThumb thumb = GetIconForResourceLang(lang);
				
				ListViewItem item;
				
				if(thumb != null) {
					
					if(thumb.IsIcon) {
						
						__images.Images.Add( imageListKey, thumb.Icon );
						
					} else {
						
						__images.Images.Add( imageListKey, thumb.Image );
					}
					
					item = new ListViewItem(subitems, imageListKey );
					
				} else {
					
					item = new ListViewItem(subitems);
				}
				
				item.Tag = lang;
				
				__list.Items.Add( item );
				
			}
			
			__list.EndUpdate();
			
		}
		
		private String[] GetSubItemsForLang(ResourceLang lang) {
			
			return new String[] {
				lang.LanguageId.ToString(),
				lang.Name.Type.Identifier.FriendlyName,
				"",
				lang.Data.RawData.Length + " bytes"
			};
			
		}
		
#region GetIconFor
		
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
		
		private ListViewThumb GetIconForResourceName(ResourceName name) {
			
			if(name.Langs.Count == 1) {
				
				return GetIconForResourceLang( name.Langs[0] );
			}
			
			// use the type's icon
			
			String key = MainForm.TreeNodeImageListTypeKey( name.Type.Identifier );
			Image image = __images.ImageSize.Width == 16 ? MainForm.TypeImages16.Images[key] :  MainForm.TypeImages32.Images[key];
			
			return new ListViewThumb( image );
		}
		
		private ListViewThumb GetIconForResourceLang(ResourceLang lang) {
			
			ResourceData data = lang.Data;
			
			if(data is IconDirectoryResourceData) {
				
				IconDirectoryResourceData icoDir = data as IconDirectoryResourceData;
				IconDirectoryMember bestMember = icoDir.IconDirectory.GetIconForSize( __images.ImageSize );
				
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
				
				Size s = __images.ImageSize;
				
				Image thumb = imgData.Image.GetThumbnailImage( s.Width, s.Height, new Image.GetThumbnailImageAbort(delegate() { return true; }), IntPtr.Zero);
				
				return new ListViewThumb( thumb );
				
			} else {
				
				// choose an icon based on the ResourceType
				String key = MainForm.TreeNodeImageListTypeKey( lang.Name.Type.Identifier );
				Image image = __images.ImageSize.Width == 16 ? MainForm.TypeImages16.Images[key] :  MainForm.TypeImages32.Images[key];
				
				return new ListViewThumb( image );
				
			}
			
		}
		
#endregion
		
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
