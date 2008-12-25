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
			
			_viewData = new ResourceDataView();
			_viewList = new ResourceListView();
		}
		
		internal ResourcerContext Context { get; set; }
		
		public StatusStrip StatusBar { get { return __status; } }
		
#region UI Events
	
	#region Treeview
		
		private void __resources_NodeMouseClick(Object sender, TreeNodeMouseClickEventArgs e) {
			
			TreeNode node = e.Node;
			ResourceLang lang = node.Tag as ResourceLang;
			if(lang == null) return;
			
			LoadData( lang );
			
		}
	
	#endregion
	
	#region Toolbar
		
		private void __tSrcOpen_ButtonClick(object sender, EventArgs e) {
			
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
			options.ShowDialog(this);
			
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
			
			Context.AddData(this, __ofd);
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
				
				__tResAdd    .Enabled = Context.CurrentData != null;
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
			
			if( Context.CurrentSource != null && Context.CurrentSource.HasUnsavedChanges ) {
				
				String message = String.Format(Cult.InvariantCulture, "Save changes to {0}?", Path.GetFileName(Context.CurrentPath));
				
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
			
			return Context.LoadSource(path, __resources, __resCM);
		}
		
#endregion
		
#region ResourceData
		
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
		
	}
}