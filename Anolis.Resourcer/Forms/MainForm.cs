using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Anolis.Resourcer.TypeViewers;
using System.IO;

using Cult = System.Globalization.CultureInfo;

using Anolis.Core;
using Anolis.Core.PE;
using Anolis.Resourcer.Settings;
using Anolis.Core.Data;

namespace Anolis.Resourcer {
	
	public partial class MainForm : Form {
		
		private ResourceDataView _viewData;
		private ResourceListView _viewList;
		
		public MainForm() {
			InitializeComponent();
			
			this.Context = Program.Context;
			
			this.AllowDrop = true;
			
			ToolStripManager.Renderer = new Anolis.Resourcer.Controls.ToolStripImprovedSystemRenderer();
			
			this.Load += new EventHandler(MainForm_Load);
			__resources.NodeMouseClick += new TreeNodeMouseClickEventHandler(__resources_NodeMouseClick);
			
			this.DragDrop += new DragEventHandler(MainForm_DragDrop);
			this.DragEnter += new DragEventHandler(MainForm_DragEnter);
			
			this.__tSrcOpen.ButtonClick     += new EventHandler(__tSrcOpen_ButtonClick);
			this.__tSrcOpen.DropDownOpening += new EventHandler(__tSrcOpen_DropDownOpening);
			this.__tSrcSave.Click           += new EventHandler(__tSrcSave_Click);
			this.__tSrcReload.Click         += new EventHandler(__tSrcReload_Click);
			
			this.__tResAdd.Click            += new EventHandler(__tResAdd_Click);
			this.__tResExtract.Click        += new EventHandler(__tResExtract_Click);
			this.__tResReplace.Click        += new EventHandler(__tResReplace_Click);
			this.__tResDelete.Click         += new EventHandler(__tResDelete_Click);
			this.__tResUndo.Click           += new EventHandler(__tResUndo_Click);
			
			this.__tGenOptions.Click        += new EventHandler(__tGenOptions_Click);
			
			this.__resCM.Opening            += new System.ComponentModel.CancelEventHandler(__resCM_Opening);
			
			this.Context.CurrentDataChanged   += new EventHandler(Context_CurrentDataChanged);
			this.Context.CurrentSourceChanged += new EventHandler(Context_CurrentSourceChanged);
			
			this.__treeStateImages.Images.Add( "Add", Properties.Resources.Tree_Add );
			this.__treeStateImages.Images.Add( "Upd", Properties.Resources.Tree_Edit );
			this.__treeStateImages.Images.Add( "Del", Properties.Resources.Tree_Delete );
			
			_viewData = new ResourceDataView();
			_viewList = new ResourceListView();
		}
		
		internal ResourcerContext Context { get; set; }
		
		public StatusStrip StatusBar { get { return __status; } }
		
#region UI Events
	
	#region Treeview
		
		private void __resources_NodeMouseClick(Object sender, TreeNodeMouseClickEventArgs e) {
			
			TreeNode node = e.Node;
			
			ResourceType type = node.Tag as ResourceType;
			if(type != null) { LoadList(type); return; }
			
			ResourceName name = node.Tag as ResourceName;
			if(name != null) { LoadList(name); return; }
			
			ResourceLang lang = node.Tag as ResourceLang;
			if(lang != null) LoadData( lang );
			
		}
	
	#endregion
	
	#region Toolbar
		
		private void __tSrcOpen_ButtonClick(object sender, EventArgs e) {
			
			__ofd.Filter =
				"Portable Executables (*.exe;*.dll;*.scr;*.ocx;*.cpl)|*.exe;*.dll;*.scr;*.ocx;*.cpl|" +
				"New Executables (*.icl)|*.icl|" +
				"All Files (*.*)|*.*";
			
			if(__ofd.ShowDialog(this) != DialogResult.OK) return;
			
			LoadSource( __ofd.FileName );
		}
		
		private void __tSrcOpen_DropDownOpening(object sender, EventArgs e) {
			
			LoadMru();
		}
		
		private void __tSrcReload_Click(object sender, EventArgs e) {
			
			LoadSource( Context.CurrentPath );
		}
		
		private void __tSrcSave_Click(object sender, EventArgs e) {
			
			SaveSource();
		}
		
		private void __tResExtract_Click(object sender, EventArgs e) {
			
			ExtractData();
		}
		
		private void __tSrcMruItem_Click(object sender, EventArgs e) {
			
			String path = (sender as ToolStripItem).Tag as String;
			
			LoadSourceFromMru(path);
			
		}
		
		private void __tGenOptions_Click(object sender, EventArgs e) {
			
			OptionsForm options = new OptionsForm();
			if(options.ShowDialog(this) == DialogResult.OK) { // then a setting may have been changed
				
				this.SuspendLayout();
				
				Boolean is24 = Settings.Settings.Default.Toolbar24;
				
				__tSrcOpen   .TextImageRelation = is24 ? TextImageRelation.ImageBeforeText : TextImageRelation.ImageAboveText ;
				__tSrcSave   .DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
				__tSrcReload .DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
				
				__tResAdd    .TextImageRelation = is24 ? TextImageRelation.ImageBeforeText : TextImageRelation.ImageAboveText;
				__tResExtract.TextImageRelation = is24 ? TextImageRelation.ImageBeforeText : TextImageRelation.ImageAboveText;
				__tResReplace.TextImageRelation = is24 ? TextImageRelation.ImageBeforeText : TextImageRelation.ImageAboveText;
				__tResDelete .DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
				__tResUndo   .DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
				
				__tGenOptions.DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
				
				__t.ImageScalingSize            = is24 ? new System.Drawing.Size(24, 24)   : new System.Drawing.Size(48, 48);
				
				__split.Location = new System.Drawing.Point(0, __t.Height);
				
				__t.PerformLayout();
				
				this.ResumeLayout(true);
				this.PerformLayout();
				
			}
			
			
			
		}
		
		private void __tResUndo_Click(Object sender, EventArgs e) {
			
			//Context.CurrentSource.CancelAction( Context.CurrentData );
		}
		
		private void __tResDelete_Click(object sender, EventArgs e) {
			
			Context.RemoveData();
		}
		
		private void __tResReplace_Click(object sender, EventArgs e) {
			
			
		}
		
		private void __tResAdd_Click(object sender, EventArgs e) {
			
			Context.AddData(this);
		}
		
		private void UpdateUI() {
			
			Boolean isReadOnly = (Context.CurrentSource == null) ? true : Context.CurrentSource.IsReadOnly;
			
			__tSrcSave   .Enabled = !isReadOnly;
			__tSrcReload .Enabled = !isReadOnly;
			
			__tResAdd    .Enabled = !isReadOnly;
			__tResReplace.Enabled = !isReadOnly;
			__tResExtract.Enabled =  Context.CurrentData   != null;
			__tResDelete .Enabled = !isReadOnly;
			__tResUndo   .Enabled = !isReadOnly;
			
			if( !isReadOnly ) {
				
				__tResReplace.Enabled = Context.CurrentData != null;
				__tResExtract.Enabled = Context.CurrentData != null;
				__tResDelete .Enabled = Context.CurrentData != null;
				__tResUndo   .Enabled = Context.CurrentData != null;
				
			}
			
			String path = (Context.CurrentPath == null) ? null :  Path.GetFileName( Context.CurrentPath );
			
			SetTitle(path, Context.CurrentSource != null && isReadOnly );
			
		}
		
	#endregion
		
		private void Context_CurrentSourceChanged(object sender, EventArgs e) {
			
			UpdateUI();
		}
		
		private void Context_CurrentDataChanged(object sender, EventArgs e) {
			
			UpdateUI();
		}
	
	#region Resource Context Menu
		
		private void __resCM_Opening(Object sender, System.ComponentModel.CancelEventArgs e) {
			
			// Tailor the context menu for the resource
			TreeNode node = ((sender as ContextMenuStrip).SourceControl as TreeView).SelectedNode;
			
			ResourceLang lang = node.Tag as ResourceLang;
			
			String path = GetResourcePath( lang );
			
			///////////////////////////////
			
			__resCMCast.Text = String.Format(Cult.CurrentCulture, "Cast {0} to", path);
			
			__resCMCast.DropDownItems.Clear();
			
			ResourceDataFactory[] factories = ResourceDataFactory.GetFactoriesForType( lang.Name.Type.Identifier );
			foreach(ResourceDataFactory factory in factories) {
				
				ToolStripMenuItem tsmi = new ToolStripMenuItem( factory.Name );
				tsmi.Click += new EventHandler(tsmiConvert_Click);
				tsmi.Tag    = factory;
				
				__resCMCast.DropDownItems.Add( tsmi );
			}
			
			///////////////////////////////
			
			__resCMExtract.Text = String.Format(Cult.CurrentCulture, "Extract {0}...", path);
			__resCMReplace.Text = String.Format(Cult.CurrentCulture, "Replace {0}...", path);
			__resCMDelete .Text = String.Format(Cult.CurrentCulture, "Delete {0}"    , path);
			
			///////////////////////////////
			
			if(lang.Data.Action == ResourceDataAction.None) {
				
				__resCMCancel.Enabled = false;
				__resCMCancel.Text = "Cancel";
			} else {
				
				__resCMCancel .Text = String.Format(Cult.CurrentCulture, "Cancel {0}", lang.Data.Action.ToString());
			}
			
		}
		
		private void tsmiConvert_Click(Object sender, EventArgs e) {
			
			ResourceDataFactory factory = (sender as ToolStripMenuItem).Tag as ResourceDataFactory;
			
			throw new NotImplementedException();
		}
		
	#endregion
		
	#region Drag 'n' Drop
		
		private void MainForm_DragDrop(Object sender, DragEventArgs e) {
			
			if( !e.Data.GetDataPresent(DataFormats.FileDrop) ) return;
				
			String[] filenames = e.Data.GetData(DataFormats.FileDrop) as String[];
			if(filenames == null) return;
			if(filenames.Length < 1) return;
			
			// just load the first file
			
			LoadSource( filenames[0] );
			
		}
		
		private void MainForm_DragEnter(Object sender, DragEventArgs e) {

			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				
				e.Effect = DragDropEffects.Move;
				
			} else {
				
				e.Effect = DragDropEffects.None;
				
			}
		}
		
	#endregion
		
		private void MainForm_Load(Object sender, EventArgs e) {
			
			// check for Wow64
			
			if( Anolis.Core.Utility.Environment.IsWow64 ) {
				
				String message = "Anolis Resourcer has detected it is running under WOW64 and so is accessing the system through a compatibility layer. This is not a recommended or supported scenario and modifications to system files might not take effect in the way you imagine.";
				
				MessageBox.Show(this, message, "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
				
			}
			
			UpdateUI();
			
		}
		
		
		
#endregion
		
		////////////////////////////////////////////////
		// UI Methods
		//
		
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
			
			if(resourceSourceName == null)
				this.Text = "Anolis Resourcer";
			else
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
				
				truncated = truncated.Substring(0, midSlashIdx) + @"\..." + truncated.Substring( nextSlashForwardIdx );
			}
			
			return truncated;
		}
		
		private static String GetResourcePath(ResourceLang lang) {
			
			String retval = "";
			
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
		
#region ResourceSource
		
		private DialogResult SaveSource() {
			
			if( Context.CurrentSource != null ) { // && Context.CurrentSource.HasUnsavedChanges ) {
				
				String message = String.Format(Cult.InvariantCulture, "Are you sure you want to save your changes to {0}?", Path.GetFileName(Context.CurrentPath));
				
				DialogResult r = MessageBox.Show( this, message, "Anolis Resourcer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
				
				if(r == DialogResult.Yes) Context.SaveSource();
				
				return r;
				
			}
			
			return DialogResult.Yes;
			
		}
		
		private void LoadSourceFromMru(String path) {
			
			if(!LoadSource(path)) {
				
				String message = String.Format(Cult.InvariantCulture, "Resourcer could not open the file \"{0}\", would you like to remove it from the most-recently-ued list?");
				
				DialogResult result = MessageBox.Show(this, message, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				
				if(result == DialogResult.Yes) {
					Context.Mru.Remove( path );
				}
			}
			
		}
		
		private Boolean LoadSource(String path) {
			
			DialogResult r = SaveSource();
			if(r == DialogResult.Cancel) return true; // return false on error, not user abortion
			
			ResourceTypeCollection types = Context.LoadSource(path);
			
			if(types != null) PopulateTree(types);
			
			return types != null;
		}
		
#endregion
		
		private void PopulateTree(ResourceTypeCollection types) {
			
			__resources.Nodes.Clear();
			foreach(ResourceType type in types) {
				
				TreeNode typeNode = new TreeNode( type.Identifier.FriendlyName ) { Tag = type };
				
				foreach(ResourceName name in type.Names) {
					
					TreeNode nameNode = new TreeNode( name.Identifier.FriendlyName ) { Tag = name };
					
					foreach(ResourceLang lang in name.Langs) {
						
						TreeNode langNode = new TreeNode( lang.LanguageId.ToString() ) { Tag = lang };
						nameNode.Nodes.Add( langNode );
						
						langNode.ContextMenuStrip = __resCM;
						
						if( lang.DataIsLoaded ) {
							
							switch(lang.Data.Action) {
								case ResourceDataAction.Add:
									langNode.StateImageKey = "Add";
									break;
								case ResourceDataAction.Update:
									langNode.StateImageKey = "Upd";
									break;
								case ResourceDataAction.Delete:
									langNode.StateImageKey = "Del";
									break;
							}
							
						}
						
					}
					
					typeNode.Nodes.Add( nameNode );
					
				}
				
				__resources.Nodes.Add( typeNode );
			}
			
		}
		
#region ResourceData
		
		private void LoadList(ResourceType type) {
			
			EnsureView( _viewList );
			
			_viewList.ShowResourceType(type);
			
		}
		
		private void LoadList(ResourceName name) {
			
			EnsureView( _viewList );
			
			_viewList.ShowResourceName(name);
			
		}
		
		////////////////////////////////////////////////
		// Current Resource Actions
		//
		
		private void LoadData(ResourceLang resource) {
			
			Context.LoadData(resource);
			
			// Status bar
			this.__sType.Text = Context.CurrentData.GetType().Name;
			this.__sSize.Text = Context.CurrentData.RawData.Length.ToString(Cult.CurrentCulture) + " Bytes";
			this.__sPath.Text = Context.CurrentPath + ',' + GetResourcePath(resource);
			
			__sType.BackColor = Context.CurrentData is Anolis.Core.Data.UnknownResourceData ? System.Drawing.Color.LightYellow : System.Drawing.SystemColors.Control;
			
			EnsureView( _viewData );
			
			_viewData.ShowResource( Context.CurrentData );
			
		}
		
		private void ExtractData() {
			
			Context.SaveData(this, __sfd);
			
		}
		
		private void UpdateData(ResourceData newData) {
			
			
			
		}
		
#endregion

		private void MainForm_Load_1(object sender, EventArgs e) {

		}
		
	}
}