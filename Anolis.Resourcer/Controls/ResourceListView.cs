using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Anolis.Core;
using Anolis.Core.Data;

namespace Anolis.Resourcer.Controls {
	
	public partial class ResourceListView : UserControl {
		
		private ResourceListViewMode _mode;
		
		public ResourceListView() {
			InitializeComponent();
			
			__list.SelectedIndexChanged += new EventHandler(__list_SelectedIndexChanged);
			__list.ItemActivate         += new EventHandler(__list_ItemActivate);
			
			__size16.Click += new EventHandler(__size_Click); __size16.Tag = new Size(16, 16);
			__size32.Click += new EventHandler(__size_Click); __size32.Tag = new Size(32, 32);
			__size96.Click += new EventHandler(__size_Click); __size96.Tag = new Size(96, 96);
			__sizeDetails.CheckedChanged += new EventHandler(__sizeDetails_CheckedChanged);
			
//			// bgq = feed-in queue for background worker which is used to wait for __bg to finish without blocking the main thread (see the calls to Invoke() )
			__bg.ProgressChanged += new ProgressChangedEventHandler(__bg_ProgressChanged);
			__bg.DoWork          += new DoWorkEventHandler(PopulateCurrentObject);
			__bgq.DoWork         += new DoWorkEventHandler(__bgq_DoWork);
		}
		
		public Object CurrentObject { get; private set; }
		
		public Boolean ShowIcons { get; set; }
		
#region UI Events
		
		private void __sizeDetails_CheckedChanged(object sender, EventArgs e) {
			
			__list.View = __sizeDetails.Checked ? View.Details : View.LargeIcon;
		}
		
		private void __size_Click(object sender, EventArgs e) {
			
			SetIconSize( (Size)(sender as ToolStripButton).Tag );
			
			__size16.Checked = sender == __size16;
			__size32.Checked = sender == __size32;
			__size96.Checked = sender == __size96;
			
		}
		
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
		
#region Threading
		
		private Boolean IsBusy { get; set; }
		
		private void __bg_ProgressChanged(Object sender, ProgressChangedEventArgs e) {
			
			int p = e.ProgressPercentage;
			if( p > 100 ) p = 100;
			else if( p < 0 ) p = 0;
			
			this.BeginInvoke( new MethodInvoker( delegate() {
				__progessBar.Value = p;
			}));
			
		}
		
		private void __bgq_DoWork(object sender, DoWorkEventArgs e) {
			
			while(IsBusy) {
				System.Threading.Thread.Sleep(5);
			}
			
			__bg.RunWorkerAsync();
			
		}
		
#endregion
		
		public void ShowObject(Object o) {
			
			__bg.CancelAsync();
			
			CurrentObject = o;
			
			__bgq.RunWorkerAsync();
			
		}
		
		private void PopulateCurrentObject(Object sender, DoWorkEventArgs e) {
			
			IsBusy = true;
			
			Invoke( new MethodInvoker( delegate() {
				
				__list.BeginUpdate();
				__list.Items.Clear();
				
			} ) );
			
			try {
				
				ResourceSource coSource = CurrentObject as ResourceSource;
				if( coSource != null ) PopulateResourceSource( coSource, e );
				
				ResourceType coType = CurrentObject as ResourceType;
				if( coType != null ) PopulateResourceType( coType, e );
				
				ResourceName coName = CurrentObject as ResourceName;
				if( coName != null ) PopulateResourceName( coName, e );
				
				
			} finally {
				
				Invoke( new MethodInvoker( delegate() {
					
					__list.EndUpdate();
					
				} ) );
				
				IsBusy = false;
			}
			
			
		}
		
		private void PopulateResourceSource(ResourceSource source, DoWorkEventArgs e) {
			
			CurrentObject = source;
			_mode = ResourceListViewMode.Type;
			
			float i = 0, cnt = source.AllTypes.Count;
			Boolean showIcons = PopulatePromptIcons( cnt );
			
			foreach(ResourceType type in source.AllTypes) {
				
				if( __bg.CancellationPending ) {
					e.Cancel = true;
					return;
				}
				
				AddResourceTypeItem( type );
				
				__bg.ReportProgress( Convert.ToInt32( 100 * i++ / cnt ) );
			}
			
		}
		
		private void PopulateResourceType(ResourceType type, DoWorkEventArgs e) {
			
			CurrentObject = type;
			_mode = ResourceListViewMode.Name;
			
			float i = 0, cnt = type.Names.Count;
			Boolean showIcons = PopulatePromptIcons( cnt );
			
			foreach(ResourceName name in type.Names) {
				
				if( __bg.CancellationPending ) {
					e.Cancel = true;
					return;
				}
				
				AddResourceNameItem( name );
				
				__bg.ReportProgress( Convert.ToInt32( 100 * i++ / cnt ) );
			}
			
		}
		
		private void PopulateResourceName(ResourceName name, DoWorkEventArgs e) {
			
			CurrentObject = name;
			_mode = ResourceListViewMode.Lang;
			
			float i = 0, cnt = name.Langs.Count;
			Boolean showIcons = PopulatePromptIcons( cnt );
			
			foreach(ResourceLang lang in name.Langs) {
				
				if( __bg.CancellationPending ) {
					e.Cancel = true;
					return;
				}
				
				AddResourceLangItem( lang );
				
				__bg.ReportProgress( Convert.ToInt32( 100 * i++ / cnt ) );
			}
			
		}
		
		private Boolean PopulatePromptIcons(Single cnt) {
			
			if(cnt > 256) {
				
				String message = String.Format("There are {0} resource icons to display. This might take a long time, do you want to show the icons?", cnt);
				
				DialogResult r = DialogResult.None;
				Invoke( new MethodInvoker( delegate() {
				
					r = MessageBox.Show(this, message, "Anolis Resourcer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				
				} ) );
				
				return r == DialogResult.Yes;
				
			} else return true;
			
		}
		
		////////////////////////////////////
		
		private void AddResourceTypeItem(ResourceType type) {
			
			ListViewItem item = new ListViewItem();
			item.Text     = type.Identifier.FriendlyName;
			item.Tag      = type;
			item.ImageKey = MainForm.GetTreeNodeImageListTypeKey( type.Identifier );
			item.SubItems.AddRange( GetSubItemsForType( type ) );
			
			if( !__images.Images.ContainsKey( item.ImageKey ) ) {
				
				Image img = GetIconForResourceType( type );
				AddImageAsync( item.ImageKey, img );
			}
			
			BeginInvoke( new MethodInvoker(delegate() { __list.Items.Add( item ); }) );
		}
		
		private void AddResourceNameItem(ResourceName name) {
			
			ListViewItem item = new ListViewItem();
			item.Text     = name.Identifier.FriendlyName;
			item.Tag      = name;
			item.SubItems.AddRange( GetSubItemsForName( name ) );
			
			Image itemImage = GetIconForResourceName( name );
			if(itemImage != null ) {
				
				AddImageAsync( name.Identifier.FriendlyName, itemImage );
				item.ImageKey = name.Identifier.FriendlyName;
				
			} else {
				
				// use the type's icon
				String key = MainForm.GetTreeNodeImageListTypeKey( name.Type.Identifier );
				item.ImageKey = key;
				
				if( !__images.Images.ContainsKey( key ) ) {
					
					Image typeImage = GetIconForResourceType( name.Type );
					AddImageAsync( key, typeImage );
				}
				
			}
			
			BeginInvoke( new MethodInvoker(delegate() { __list.Items.Add( item ); }) );
		}
		
		private void AddResourceLangItem(ResourceLang lang) {
			
			ListViewItem item = new ListViewItem();
			item.Text = lang.LanguageId.ToString();
			item.Tag  = lang;
			item.SubItems.AddRange( GetSubItemsForLang( lang ) );
			
			Image itemImage = GetIconForResourceLang( lang );
			
			if(itemImage != null) { // the itemImage is unique for this lang. If it's null then use a non-unique image (i.e. the icon for its type)
				
				AddImageAsync( lang.ResourcePath, itemImage );
				item.ImageKey = lang.ResourcePath;
				
			} else {
				
				// get the image for the type, check to see if it's already been added, in which case use that key
				
				// otherwise, add the type icon to the list and set the key
				
				String key = MainForm.GetTreeNodeImageListTypeKey( lang.Name.Type.Identifier );
				item.ImageKey = key;
				
				if( !__images.Images.ContainsKey( key ) ) {
					
					Image typeImage = GetIconForResourceType( lang.Name.Type );
					AddImageAsync( key, typeImage );
				}
				
			}
			
			
			BeginInvoke( new MethodInvoker(delegate() { __list.Items.Add( item ); }) );
		}
		
		private void AddImageAsync(String key, Image image) {
			
			BeginInvoke( new MethodInvoker(delegate() { __images.Images.Add( key, image ); } ) );
		}
		
		///////////////////////////////////////
		
		private Image GetIconForResourceLang(ResourceLang lang) {
			
			ResourceData data = lang.Data;
			
			if(data is IconDirectoryResourceData) {
				
				IconDirectoryResourceData icoDir = data as IconDirectoryResourceData;
				IconDirectoryMember bestMember = icoDir.IconDirectory.GetIconForSize( __images.ImageSize );
				
				if(bestMember == null) return null;
				
				IconCursorImageResourceData rd = (bestMember.ResourceData as IconCursorImageResourceData);
				return rd.Image;
				
			} else if(data is IconCursorImageResourceData) {
				
				IconCursorImageResourceData icoImg = data as IconCursorImageResourceData;
				return icoImg.Image;
				
			} else if(data is ImageResourceData) {
				
				ImageResourceData imgData = data as ImageResourceData;
				Size s = __images.ImageSize;
				
				Image thumb = imgData.Image.GetThumbnailImage( s.Width, s.Height, new Image.GetThumbnailImageAbort(delegate() { return true; }), IntPtr.Zero);
				
				return thumb;
				
			} else {
				
				return null;
			}
			
		}
		
		private Image GetIconForResourceName(ResourceName name) {
			
			if(name.Langs.Count == 1) {
				
				return GetIconForResourceLang( name.Langs[0] );
			}
			
			return null;
		}
		
		private Image GetIconForResourceType(ResourceType type) {
			
			String key = MainForm.GetTreeNodeImageListTypeKey( type.Identifier );
			
			Image image = __images.ImageSize.Width == 16 ? MainForm.TypeImages16.Images[key] :  MainForm.TypeImages32.Images[key];
			
			return image;
		}
		
		///////////////////////////////////////
		
		private static String[] GetSubItemsForType(ResourceType type) {
			
			return new String[] {
//				type.Identifier.FriendlyName,
				type.Names.Count + " names",
				""
			};
			
		}
		
		private static String[] GetSubItemsForName(ResourceName name) {
			
			return new String[] {
//				name.Identifier.FriendlyName,
				name.Langs.Count + " langs",
				name.Type.Identifier.FriendlyName
			};
			
		}
		
		private static String[] GetSubItemsForLang(ResourceLang lang) {
			
			return new String[] {				
//				lang.LanguageId.ToString(),
				lang.Data.RawData.Length + " bytes",
				lang.Data.GetType().Name
			};
			
		}
		
#if never	
		private void PopulateResourceType(Object sender, DoWorkEventArgs e) {
			
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
		
#endif
		
		private void SetIconSize(Size size) {
			
			__images.ImageSize = size;
			
			// reload
			ShowObject( CurrentObject );
			
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
	
}
