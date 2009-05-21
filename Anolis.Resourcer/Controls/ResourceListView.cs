﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Core.Data;

using Cult = System.Globalization.CultureInfo;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

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
			
			__bg.ProgressChanged    += new ProgressChangedEventHandler(__bg_ProgressChanged);
			__bg.DoWork             += new DoWorkEventHandler(PopulateCurrentObject);
			__bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(__bg_RunWorkerCompleted);
		}
		
		public Object CurrentObject { get; private set; }
		
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
		
		private void __bg_ProgressChanged(Object sender, ProgressChangedEventArgs e) {
			
			int p = e.ProgressPercentage;
			if( p > 100 ) p = 100;
			else if( p < 0 ) p = 0;
			
			this.BeginInvoke( new MethodInvoker( delegate() {
				__progessBar.Value = p;
			}));
			
		}
		
		private void __bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			
			__progessBar.Visible = false;
		}
		
#endregion
		
		public void ShowObject(Object o) {
			
			CurrentObject = o;
			
			if( __bg.IsBusy ) {
				
				__bg.CancelAsync();
			}
			
			while( __bg.IsBusy ) {
				
				Application.DoEvents();
			}
			
			__bg.RunWorkerAsync();
			
		}
		
		private List<ListViewItem> _itemsToAdd = new List<ListViewItem>();
		
		private void PopulateCurrentObject(Object sender, DoWorkEventArgs e) {
			
			Thread.CurrentThread.Name = "ListView Populator";
			
			BeginInvoke( new MethodInvoker( delegate() {
				__progessBar.Visible = true;
				__list.BeginUpdate();
				__list.Items.Clear();
				__images.Images.Clear();
			} ) );
			
			_itemsToAdd.Clear();
			
			try {
				
				ResourceSource coSource = CurrentObject as ResourceSource;
				if( coSource != null ) PopulateResourceSource( coSource, e );
				
				ResourceType coType = CurrentObject as ResourceType;
				if( coType != null ) PopulateResourceType( coType, e );
				
				ResourceName coName = CurrentObject as ResourceName;
				if( coName != null ) PopulateResourceName( coName, e );
				
			} finally {
				
				BeginInvoke( new MethodInvoker( delegate() {
					
					__list.Items.AddRange( _itemsToAdd.ToArray() );
					
					__list.EndUpdate();
					
				} ) );
				
				
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
				
				AddResourceTypeItem( type, showIcons );
				
				__bg.ReportProgress( Convert.ToInt32( 100 * i++ / cnt ) );
			}
			
			__bg.ReportProgress( 100 );
			
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
				
				AddResourceNameItem( name, showIcons );
				
				__bg.ReportProgress( Convert.ToInt32( 100 * i++ / cnt ) );
			}
			
			__bg.ReportProgress( 100 );
			
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
				
				AddResourceLangItem( lang, showIcons );
				
				__bg.ReportProgress( Convert.ToInt32( 100 * i++ / cnt ) );
			}
			
			__bg.ReportProgress( 100 );
			
		}
		
		private Boolean PopulatePromptIcons(Single cnt) {
			
			if(cnt > 800) {
				
				String message = String.Format("There are {0} resource icons to display. This might take a long time, do you want to show the icons?", cnt);
				
				DialogResult r = DialogResult.None;
				Invoke( new MethodInvoker( delegate() {
				
					r = MessageBox.Show(this, message, "Anolis Resourcer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				
				} ) );
				
				return r == DialogResult.Yes;
				
			} else return true;
			
		}
		
		////////////////////////////////////
		
		private void AddResourceTypeItem(ResourceType type, Boolean showIcon) {
			
			ListViewItem item = new ListViewItem();
			item.Text     = type.Identifier.FriendlyName;
			item.Tag      = type;
			item.SubItems.AddRange( GetSubItemsForType( type ) );
			
			if( showIcon ) {
				
				item.ImageKey = MainForm.GetTreeNodeImageListTypeKey( type.Identifier );
				
				if( !__images.Images.ContainsKey( item.ImageKey ) ) {
					
					Image img = GetIconForResourceType( type );
					AddImageAsync( item.ImageKey, img );
					
				}
			}
			
			_itemsToAdd.Add( item );
		}
		
		private void AddResourceNameItem(ResourceName name, Boolean showIcon) {
			
			ListViewItem item = new ListViewItem();
			item.Text     = name.Identifier.FriendlyName;
			item.Tag      = name;
			item.SubItems.AddRange( GetSubItemsForName( name ) );
			
			if(showIcon) {
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
			}
			
			_itemsToAdd.Add( item );
		}
		
		private void AddResourceLangItem(ResourceLang lang, Boolean showIcon) {
			
			ListViewItem item = new ListViewItem();
			item.Text = lang.LanguageId.ToString();
			item.Tag  = lang;
			item.SubItems.AddRange( GetSubItemsForLang( lang ) );
			
			if(showIcon) {
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
			}
			
			_itemsToAdd.Add( item );
		}
		
		private System.Collections.Generic.List<Int64> ticks = new System.Collections.Generic.List<long>();
		
		private void AddImageAsync(String key, Image image) {
			
			Stopwatch sw = new Stopwatch();
			sw.Start();
			
			BeginInvoke( new MethodInvoker(delegate() { __images.Images.Add( key, image ); } ) );
			
			sw.Stop();
			
			ticks.Add( sw.ElapsedTicks );
			
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
			
			} else if(data is CursorDirectoryResourceData) {
				
				CursorDirectoryResourceData curDir = data as CursorDirectoryResourceData;
				IconDirectoryMember bestMember = curDir.IconDirectory.GetIconForSize( __images.ImageSize );
				
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
			
			Int32 dataSize = 0;
			
			foreach(ResourceLang lang in name.Langs) {
				dataSize += lang.Data.RawData.Length;
			}
			
			return new String[] {
//				name.Identifier.FriendlyName,
				String.Format(Cult.CurrentCulture, "{0} bytes in {1} langs", dataSize, name.Langs.Count),
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
