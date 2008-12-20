using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Anolis.Resourcer.TypeViewers;
using System.IO;

using Cult = System.Globalization.CultureInfo;

using Anolis.Core;
using Anolis.Core.PE;
using Anolis.Resourcer.Settings;

namespace Anolis.Resourcer {
	
	public partial class MainForm : Form {
		
		private ResourceLang   _currentResource;
		private String         _currentPath;
		
		private ResourceDataView _viewData;
		private ResourceListView _viewList;
		
		public MainForm() {
			InitializeComponent();
			
			ToolStripManager.Renderer = new Anolis.Resourcer.Controls.ToolStripImprovedSystemRenderer();
			
			this.Load += new EventHandler(MainForm_Load);
			__resources.NodeMouseClick += new TreeNodeMouseClickEventHandler(__resources_NodeMouseClick);
			
			this.__tSrcOpen.ButtonClick += new EventHandler(__tSrcOpen_ButtonClick);
			this.__tSrcOpen.DropDownOpening += new EventHandler(__tSrcOpen_DropDownOpening);
			this.__tResExtract.Click += new EventHandler(__tResExtract_Click);
			this.__tGenOptions.Click += new EventHandler(__tGenOptions_Click);

			this.__resCM.Opening += new System.ComponentModel.CancelEventHandler(__resCM_Opening);
			
			_viewData = new ResourceDataView();
			_viewList = new ResourceListView();
		}
		
		internal ResourcerContext Context { get; set; }
		
		public StatusStrip StatusBar { get { return __status; } }
		
#region UI Events
	
	#region Toolbar
		
		private void __tSrcOpen_ButtonClick(object sender, EventArgs e) {
			
			if(__ofd.ShowDialog(this) != DialogResult.OK) return;
			
			LoadSource( __ofd.FileName );
			
		}
		
		private void __tSrcOpen_DropDownOpening(object sender, EventArgs e) {
			
			LoadMru();
		}
		
		private void __tResExtract_Click(object sender, EventArgs e) {
			
			SaveCurrentResourceToFile();
			
		}
		
		private void __tSrcMruItem_Click(object sender, EventArgs e) {
			
			String path = (sender as ToolStripItem).Tag as String;
			
			LoadSourceFromMru(path);
			
		}
		
		private void __tGenOptions_Click(object sender, EventArgs e) {
			
			OptionsForm options = new OptionsForm();
			options.ShowDialog(this);
			
		}
		
	#endregion
	
	#region Context Menu
		
		private void __resCM_Opening(Object sender, System.ComponentModel.CancelEventArgs e) {
			
			// Tailor the context menu for the resource
			ContextMenuStrip strip = sender as ContextMenuStrip;
			
			
			
			
		}
		
	#endregion
		
		private void MainForm_Load(Object sender, EventArgs e) {
			
			
			
		}
		
		private void __resources_NodeMouseClick(Object sender, TreeNodeMouseClickEventArgs e) {
			
			TreeNode node = e.Node;
			ResourceLang lang = node.Tag as ResourceLang;
			if(lang == null) return;			
			
			LoadResource( lang );
			
		}
		
		private void __saveRaw_Click(object sender, EventArgs e) {
			SaveCurrentResourceToFile();
		}
		
#endregion
		
		/// <summary>Loads the MRU from the User Settings file.</summary>
		private void LoadMru() {
			
			__tSrcOpen.DropDown.Items.Clear();
			
			String[] mruItems = Context.Mru.Items;
			
			for(int i=0;i<mruItems.Length;i++) {
				
				String text = (i+1).ToString(Cult.InvariantCulture) + ' ' + TrimPath( mruItems[i], 80 );
				
				ToolStripItem item = new ToolStripMenuItem( text );
				item.Tag = mruItems[i];
				item.Click += new EventHandler(__tSrcMruItem_Click);
				
				__tSrcOpen.DropDown.Items.Add( item );
				
			}
			
		}
		
		private void EnsureView(Control control) {
			
			control.Dock = DockStyle.Fill;
			
			if(__split.Panel2.Controls.Count == 0) {
				
				__split.Panel2.Controls.Add( control );
				
			} else {
				
				Control currentControl = __split.Panel2.Controls[0];
				
				if(currentControl != control) {
					__split.Panel2.Controls.Clear();
					__split.Panel2.Controls.Add( control );
				}
			}
			
		}
		
		private void SetTitle(String resourceSourceName, Boolean isReadOnly) {
			
			this.Text = resourceSourceName + (isReadOnly ? " [Read-Only]" : "") + " - Anolis Resourcer";
		}
		
		////////////////////////////////////////////////
		// Utility Methods
		//
		
		private static String TrimPath(String path, Int32 maxLength) {
			
			Char[] chars = new Char[] { '/', '\\' };
			
			if(path.Length <= maxLength) return path;
			
			String truncated = path;
			while(truncated.Length > maxLength) {
				// take stuff out from the middle
				// so starting from the middle search for the first slash remove it to the next slash on
				Int32 midSlashIdx = truncated.LastIndexOfAny( chars, truncated.Length / 2 );
				if( midSlashIdx == -1 ) return truncated;
				
				Int32 nextSlashForwardIdx = truncated.IndexOfAny( chars, midSlashIdx + 1 );
				
				truncated = truncated.Substring(0, midSlashIdx) + "..." + truncated.Substring( nextSlashForwardIdx );
			}
			
			return truncated;
		}
		
		private static String GetResourcePath(ResourceLang lang) {
			
			String retval = ",";
			
			if(lang.Name.Type.Identifier.KnownType == Win32ResourceType.Custom) {
				
				retval += lang.Name.Type.Identifier.StringId;
				
			} else {
				
				retval += lang.Name.Type.Identifier.IntegerId.Value.ToString(Cult.InvariantCulture);
			}
			
			retval += '\\' + lang.Name.Identifier.FriendlyName;
			
			retval += '\\' + lang.LanguageId.ToString(Cult.InvariantCulture);
			
			return retval;
			
		}
		
		////////////////////////////////////////////////
		// Current Source Actions
		//
		
		private void LoadSourceFromMru(String path) {
			
			if(!LoadSource(path)) {
				
				DialogResult result = MessageBox.Show(this, "Resourcer could not open the file \"" + path + "\", would you like to remove it from the most-recently-ued list?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				
				if(result == DialogResult.Yes) {
					Context.Mru.Remove( path );
				}
			}
			
		}
		
		private Boolean LoadSource(String path) {
			
			_currentPath = path;
			
			SetTitle( Path.GetFileName(path), true );
			
			///////////////////////////
			
			ResourceSource source = ResourceSource.Open(path, true);
			if(source == null) return false;
			
			Context.Source = source;
			Context.Mru.Push( path );
			
			ResourceTypeCollection types = source.Types;
			
			__resources.Nodes.Clear();
			foreach(ResourceType type in types) {
				
				TreeNode typeNode = new TreeNode( type.Identifier.FriendlyName ) { Tag = type };
				
				foreach(ResourceName name in type.Names) {
					
					TreeNode nameNode = new TreeNode( name.Identifier.FriendlyName ) { Tag = name };
					
					foreach(ResourceLang lang in name.Langs) {
						
						TreeNode langNode = new TreeNode( lang.LanguageId.ToString() ) { Tag = lang };
						nameNode.Nodes.Add( langNode );
						
						langNode.ContextMenuStrip = __resCM;
						
					}
					
					typeNode.Nodes.Add( nameNode );
					
				}
				
				__resources.Nodes.Add( typeNode );
			}
			
			/////////////////////////
			
			// set various buttons if it's readonly
			
			__tSrcSave   .Enabled = !source.IsReadOnly;
			__tResAdd    .Enabled = !source.IsReadOnly;
			__tResReplace.Enabled = !source.IsReadOnly;
			__tResDelete .Enabled = !source.IsReadOnly;
			
			return true;
			
		}
		
		////////////////////////////////////////////////
		// Current Resource Actions
		//
		
		private void LoadResource(ResourceLang resource) {
			
			_currentResource = resource;
			
			ResourceData data = resource.Data;
			
			// Status bar
			this.__sType.Text = data.GetType().Name;
			this.__sSize.Text = data.RawData.Length.ToString(Cult.CurrentCulture) + " Bytes";
			this.__sPath.Text = _currentPath + GetResourcePath(resource);
			
			__sType.BackColor = data is Anolis.Core.Data.UnknownResourceData ? System.Drawing.Color.LightYellow : System.Drawing.SystemColors.Control;
			
			EnsureView( _viewData );
			
			_viewData.ShowResource( data );
			
		}
		
		private void SaveCurrentResourceToFile() {
			
			__sfd.Filter = _currentResource.Data.SaveFileFilter + "|Binary Data File (*.bin)|*.bin";;
			
			if(__sfd.ShowDialog(this) != DialogResult.OK) return;
			
			if( __sfd.FilterIndex == 1 ) { // .FilterIndex has 1 = first item, 2 = second item, and so on
				
				_currentResource.Data.SaveAs( __sfd.FileName );
				
			} else { 
				
				_currentResource.Data.Save( __sfd.FileName );
			}
			
		}
		
	}
}