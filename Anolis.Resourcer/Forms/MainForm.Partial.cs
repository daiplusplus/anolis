using System;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Resourcer.Settings;
using Anolis.Core.Data;

using Cult             = System.Globalization.CultureInfo;
using TypeViewerList   = System.Collections.Generic.List<Anolis.Resourcer.TypeViewers.TextViewer>;
using StringCollection = System.Collections.Specialized.StringCollection;

using Path     = System.IO.Path;
using File     = System.IO.File;

using FileResourceSource = Anolis.Core.Source.FileResourceSource;
using Anolis.Core.Source;
using System.Collections.Generic;

namespace Anolis.Resourcer {
	
	// This file contains all the "task logic" which is called by UI events
	
	public partial class MainForm {
		
		private TypeViewerList _viewers;
		private Mru _mru;
		private Settings.Settings _settings;
		
		private void MainFormInit() {
			
			_settings = Anolis.Resourcer.Settings.Settings.Default;
//			_settings.Upgrade();
			
			if( _settings.MruList == null ) _settings.MruList = new StringCollection();
			
			_viewers = new TypeViewerList();
			_mru     = new Mru( _settings.MruCount, _settings.MruList, StringComparison.InvariantCultureIgnoreCase );
			
			MainForm.LatestInstance = this;
		}
		
		public String         CurrentPath   { get; set; }
		public ResourceSource CurrentSource { get; set; }
		public ResourceData   CurrentData   { get; set; }
		
		public static MainForm LatestInstance { get; private set; }
		
		private void MainFormRefresh() {
			
			TreePopulate();
			
			// TODO: Refresh the current ListView, it should have overlay icons methinks
			
		}
		
#region Resource Type Images
		
		private static ImageList _typeImages16;
		private static ImageList _typeImages32;
		
		public static ImageList TypeImages16 {
			get {
				
				if( _typeImages16 == null ) {
					
					_typeImages16 = new ImageList();
					_typeImages16.ColorDepth = ColorDepth.Depth32Bit;
					_typeImages16.ImageSize = new System.Drawing.Size(16, 16);
					
					_typeImages16.Images.Add("Accelerator", Resources.Type_Accelerator16);
					_typeImages16.Images.Add("Binary",      Resources.Type_Binary16);
					_typeImages16.Images.Add("Bitmap",      Resources.Type_Bitmap16);
					_typeImages16.Images.Add("ColorTable",  Resources.Type_ColorTable16);
					_typeImages16.Images.Add("Cursor",      Resources.Type_Cursor16);
					_typeImages16.Images.Add("Dialog",      Resources.Type_Dialog16);
					_typeImages16.Images.Add("File",        Resources.Type_File16);
					_typeImages16.Images.Add("Html",        Resources.Type_Html16);
					_typeImages16.Images.Add("Icon",        Resources.Type_Icon16);
					_typeImages16.Images.Add("Menu",        Resources.Type_Menu16);
					_typeImages16.Images.Add("StringTable", Resources.Type_StringTable16);
					_typeImages16.Images.Add("Toolbar",     Resources.Type_Toolbar16);
					_typeImages16.Images.Add("Version",     Resources.Type_Version16);
					_typeImages16.Images.Add("Xml",         Resources.Type_Xml16);
					
				}
				return _typeImages16;
				
			}
		}
		
		public static ImageList TypeImages32 {
			get {
				
				if( _typeImages32 == null ) {
					
					_typeImages32 = new ImageList();
					_typeImages32.ColorDepth = ColorDepth.Depth32Bit;
					_typeImages32.ImageSize = new System.Drawing.Size(32, 32);
					
					_typeImages32.Images.Add("Accelerator", Resources.Type_Accelerator32);
					_typeImages32.Images.Add("Binary",      Resources.Type_Binary32);
					_typeImages32.Images.Add("Bitmap",      Resources.Type_Bitmap32);
					_typeImages32.Images.Add("ColorTable",  Resources.Type_ColorTable32);
					_typeImages32.Images.Add("Cursor",      Resources.Type_Cursor32);
					_typeImages32.Images.Add("Dialog",      Resources.Type_Dialog32);
					_typeImages32.Images.Add("File",        Resources.Type_File32);
					_typeImages32.Images.Add("Html",        Resources.Type_Html32);
					_typeImages32.Images.Add("Icon",        Resources.Type_Icon32);
					_typeImages32.Images.Add("Menu",        Resources.Type_Menu32);
					_typeImages32.Images.Add("StringTable", Resources.Type_StringTable32);
					_typeImages32.Images.Add("Toolbar",     Resources.Type_Toolbar32);
					_typeImages32.Images.Add("Version",     Resources.Type_Version32);
					_typeImages32.Images.Add("Xml",         Resources.Type_Xml32);
					
				}
				return _typeImages32;
				
			}
		}
		
#endregion
		
#region Navigation
		
		private ViewMode _currentViewMode;
		
		private System.Collections.Generic.Stack<NavigateItem> _history;
		
		private class NavigateItem {
			public ViewMode ViewMode;
			public Object Argument;
		}
		
		private enum ViewMode {
			ViewSource,
			ViewType,
			ViewName,
			ViewLangData,
			Other
		}
		
		private void NavigateUpdateUI() {
			
			__navBack.Enabled = _history.Count > 1; // the 0th element will be the start point
			__navUp.Enabled   = (_currentViewMode != ViewMode.ViewSource && _currentViewMode != ViewMode.Other);
		}
		
		private void NavigateClear() {
			
			_history.Clear();
			__navBack.Enabled = false;
			__navUp.Enabled   = false;
			
		}
		
		private void NavigateBack() {
			
			NavigateItem thisItem = _history.Pop();
			NavigateItem lastItem = _history.Pop();
			
			switch(lastItem.ViewMode) {
				case ViewMode.ViewSource:
					
					ListLoad();
					break;
					
				case ViewMode.ViewType:
					
					ListLoad( lastItem.Argument as ResourceType );
					break;
					
				case ViewMode.ViewName:
					
					ListLoad( lastItem.Argument as ResourceName );
					break;
					
				case ViewMode.ViewLangData:
					
					DataLoad( lastItem.Argument as ResourceLang );
					break;
					
				case ViewMode.Other:
					break;
			}
			
			NavigateUpdateUI();
		}
		
		private void NavigateUp() {
			
			switch(_currentViewMode) {
				case ViewMode.ViewLangData:
					ListLoad( _viewData.CurrentData.Lang.Name );
					break;
				case ViewMode.ViewName:
					ListLoad( (_viewList.CurrentObject as ResourceName).Type );
					break;
				case ViewMode.ViewType:
					ListLoad();
					break;
				case ViewMode.ViewSource:
					break;
			}
			
			NavigateUpdateUI();
		}
		
		/// <summary>Pushes the current view to the top of the history stack and updates the Navigation UI</summary>
		private void NavigateAdd() {
			
			NavigateItem item = new NavigateItem();
			item.ViewMode = _currentViewMode;
			switch(item.ViewMode) {
				case ViewMode.ViewSource:
					// don't reference the current source, we don't want to lose memory from the GC
//					item.Argument = CurrentSource;
					break;
				case ViewMode.ViewType:
					item.Argument = _viewList.CurrentObject as ResourceType;
					break;
				case ViewMode.ViewName:
					item.Argument = _viewList.CurrentObject as ResourceName;
					break;
				case ViewMode.ViewLangData:
					item.Argument = _viewData.CurrentData;
					break;
			}
			
			_history.Push( item );
			
			NavigateUpdateUI();
			
		}
		
#endregion
		
#region ResourceSource
		
		/// <summary>Presents a File Open Dialog to the user and either loads the selected source or does nothing if nothing was selected or the FOD was cancelled.</summary>
		private void SourceLoadDialog() {
			
			IList<ResourceSourceFactory> factories = ResourceSourceFactory.ListFactories();
			
			String filter = String.Empty;
			foreach(ResourceSourceFactory factory in factories) filter += factory.OpenFileFilter + '|';
			
			filter += "All Files (*.*)|*.*";
			__ofd.Filter = filter;
			
			if(__ofd.ShowDialog(this) != DialogResult.OK) return;
			
			
			// filterIndex is from 1
			ResourceSourceFactory selectedFactory = __ofd.FilterIndex > factories.Count ? null : factories[ __ofd.FilterIndex - 1 ];
			
			SourceLoad( __ofd.FileName, selectedFactory, false );
			
		}
		
		/// <summary>Calls SourceUnload (which prompts the user) if there's a source currently loaded, then loads the specified source.</summary>
		private void SourceLoad(String path, ResourceSourceFactory selectedFactory, Boolean removeFromMruOnError) {
			
			if( CurrentSource != null ) {
				if(!SourceUnload()) return;
			}
			
			if(!File.Exists(path)) {
				
				String notFoundMessage = "Resourcer could not locate the file \"{0}\"";
				if(removeFromMruOnError) notFoundMessage += "\r\n\r\n Would you like to remove it from the Most Recently Used menu?";
				
				notFoundMessage = String.Format(Cult.InvariantCulture, notFoundMessage, path);
				
				DialogResult result = MessageBox.Show(this, notFoundMessage, "Anolis Resourcer", removeFromMruOnError ? MessageBoxButtons.YesNo : MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				
				if(result == DialogResult.Yes)
					MruRemove(path);
				
				return;
			}
			
			try {
				
				ResourceSource source;
				
				if( selectedFactory != null ) {
					source = selectedFactory.Create( path, false, ResourceSourceLoadMode.LazyLoadData );
				} else {
					source = ResourceSource.Open(path, false, ResourceSourceLoadMode.LazyLoadData );
				}
				
				if(source == null) {
					
					MessageBox.Show(this, "Unable to load the file " + Path.GetFileName(path), "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					return;
				}
				
				CurrentPath = path;
				
				CurrentSource = source;
				
				Mru.Push( path );
				
				ToolbarUpdate(true, true, false);
				StatusbarUpdate();
				
				TreePopulate();
				
				ListLoad();
				
			} catch (AnolisException aex) {
				
				String errorMessage = "Resourcer could not open \"{0}\" because {1}";
				if(removeFromMruOnError) errorMessage += "\r\n\r\n Would you like to remove it from the Most Recently Used menu?";
				
				errorMessage = String.Format(Cult.InvariantCulture, errorMessage, Path.GetFileName(path), aex.Message);
				
				DialogResult result = MessageBox.Show(this, errorMessage, "Anolis Resourcer", removeFromMruOnError ? MessageBoxButtons.YesNo : MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				
				if(result == DialogResult.Yes)
					MruRemove(path);
				
				return;
				
			}
			
		}
		
		/// <summary>Prompts the user to save the CurrentSource or to cancel the operation. If the source is to be uploaded it's Disposes it. Returns False if operation was cancelled.</summary>
		private Boolean SourceUnload() {
			
			if( CurrentSource == null ) return true;
			
			Boolean retval = false;
			
			if( CurrentSource.HasUnsavedChanges ) {
				
				int nofChanges = 0;
				foreach(ResourceLang change in CurrentSource.AllActiveLangs) nofChanges++;
				
				String message = String.Format(Cult.CurrentCulture, "Save {0} change{1} to \"{2}\" before closing?", nofChanges, nofChanges == 1 ? "" : "s", CurrentSource.Name);
				
				DialogResult r = MessageBox.Show(this, message, "Anolis Resourcer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
				switch(r) {
					
					case DialogResult.Yes:
						
						SourceSave();
						retval = true;
						break;
						
					case DialogResult.No:
						retval = true;
						break;
					case DialogResult.Cancel:
					default:
						retval = false;
						break;
				}
				
			} else {
				
				retval = true;
			}
			
			if( retval ) {
				NavigateClear();
				CurrentSource.Dispose();
			}	
			
			return retval;
			
		}
		
		/// <summary>Makes a copy of the current pre-modified Resource Source. It asks the user for a save location.</summary>
		private void SourceBackup() {
			
			if(CurrentSource.IsReadOnly) return;
			
			FileResourceSource source = CurrentSource as FileResourceSource;
			if(source == null) {
				
				MessageBox.Show(this, "The current source file is not file-based and cannot be backed-up.", "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				return;
			}
			
			if( __sfd.ShowDialog(this) != DialogResult.OK ) return;
				
			File.Copy( source.FileInfo.FullName, __sfd.FileName );
			
		}
		
		/// <summary>Asks the user to confirm if they want to save the source if they have made modifications.</summary>
		private void SourceSaveConfirm() {
			
			if( CurrentSource == null || !CurrentSource.HasUnsavedChanges || CurrentSource.IsReadOnly ) return;
			
			String message = String.Format(Cult.CurrentCulture, "Are you sure you want to save changes to {0}?\r\n\r\n Once you save you cannot revert to an earlier version.", CurrentSource.Name);
			
			DialogResult r = MessageBox.Show(this, message, "Anolis Resourcer", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);	
			if(r == DialogResult.OK) {
				
				SourceSave();
			}
			
		}
		
		private void SourceSave() {
			
			if( CurrentSource == null || !CurrentSource.HasUnsavedChanges || CurrentSource.IsReadOnly ) return;
			
			CurrentSource.CommitChanges();
			
			MainFormRefresh();
		}
		
		private void SourceRevertConfirm() {
			
			if( CurrentSource == null ) return;
			
			DateTime dt = File.GetLastWriteTime(CurrentPath);
			
			String message = String.Format(Cult.CurrentCulture, "Are you sure you want to revert to the version last modified at {0}?", dt);
			
			DialogResult r = MessageBox.Show(this, message, "Anolis Resourcer", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if(r == DialogResult.OK) {
				
				SourceRevert();
			}
			
		}
		
		private void SourceRevert() {
			
			if( CurrentSource == null ) return;
			
			CurrentSource.Reload();
			
		}
		
#endregion
		
#region ResourceData
		
		private void DataSelect(ResourceLang lang) {
			
			CurrentData = lang.Data;
			
			StatusbarUpdate();
			
			ToolbarUpdate(false, true, false);
		}
		
		/// <summary>Unloads any current ResourceData and sets the toolbar status accordingly</summary>
		private void DataDeselect() {
			
			CurrentData = null;
			
			ToolbarUpdate(false, true, false);
		}
		
		private void DataLoad(ResourceLang lang) {
			
			DataSelect(lang);
			
			EnsureView( _viewData );
			
			_currentViewMode = ViewMode.ViewLangData;
			
			_viewData.ShowResource( CurrentData );
			
			NavigateAdd();
		}
		
		private void DataExport() {
			
			String filter = String.Empty;
			foreach(String f in CurrentData.SaveFileFilters) filter += f + '|';
			if(filter.EndsWith("|")) filter = filter.Substring(0, filter.Length - 1);
			
			String extensionOfFirstFilter = CurrentData.RecommendedExtension;
			
			__sfd.Filter = filter;
			
			String initialFilename =
				CurrentData.Lang.ResourcePath +
				extensionOfFirstFilter;
			
			__sfd.FileName = Anolis.Core.Utility.Miscellaneous.FSSafeResPath( initialFilename );
			
			if(__sfd.ShowDialog(this) != DialogResult.OK) return;
			
			try {
				
				CurrentData.Save( __sfd.FileName );
			
			} catch(UnauthorizedAccessException aex) {
				
				MessageBox.Show(this, "Unable to write the file: " + aex.Message, "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				
			} catch(System.IO.IOException iex) {
				
				MessageBox.Show(this, "Unable to write the file: " + iex.Message, "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				
			}
			
		}
		
		private void DataRemove() {
			
			if( CurrentData is DirectoryResourceData ) {
				
				//DialogResult r = MessageBox.Show("Do you want to delete just this ResourceData for the directory, or the directory and all of its members?", "Anolis Resourcer", MessageBoxButtons.);
				// TODO: How do you do a messagebox with custom button labels?
				
				DialogResult r = MessageBox.Show("You are attempting to delete an Icon or Cursor directory resource. This will also delete all associated Icon or Cursor member images. Are you sure you want to continue?", "Anolis Resourcer", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				if( r != DialogResult.OK ) return;
			}
			
			if( CurrentData is IconCursorImageResourceData ) {
				
				DialogResult r = MessageBox.Show("You are attempting to delete an Icon or Cursor member image. This will render the state of the parent Icon or Cursor Directory invalid and will cause errors in any applications attempting to read the icon. Are you sure you want to continue?", "Anolis Resourcer", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				if( r != DialogResult.OK ) return;
			}
			
			CurrentSource.Remove( CurrentData.Lang );
			
			ToolbarUpdate(false, true, false);
			TreeRefresh( CurrentData.Lang );
		}
		
		private void DataImport() {
			
			DataImport(null);
		}
		
		private void DataImport(String dataFilename) {
			
			AddResourceForm form = new AddResourceForm();
			if(dataFilename != null) form.LoadFile( dataFilename );
			
			if(form.ShowDialog(this) == DialogResult.OK) {
				
				ResourceData data = form.ResourceData;
				
				ResourceTypeIdentifier typeId = form.ResourceTypeIdentifier;
				ResourceIdentifier     nameId = form.ResourceNameIdentifier;
				UInt16                 langId = form.ResourceLangId;
				
				ResourceLang lang = CurrentSource.Add( typeId, nameId, langId, data );
				
				TreeAdd( lang );
				
				//TreeRefresh( lang );
				MainFormRefresh();
				
			}
			
		}
		
		/// <summary>Replaces the current ResourceData in the ResourceSource with the provided ResourceData.</summary>
		private void DataReplace() {
			
			if( CurrentData == null ) throw new InvalidOperationException("There is no current ResourceData.");
			
			ReplaceResourceForm form = new ReplaceResourceForm();
			form.InitialResource = CurrentData;
			
			if( form.ShowDialog() != DialogResult.OK ) return;
			
			ResourceData newData = form.ReplacementResource;
			if(newData != null) {
				
				ResourceLang lang = CurrentData.Lang;
				lang.SwapData( newData );
				
			}
			
		}
		
		private void DataReplace(String newDataFilename) {
			
			if( CurrentData == null ) throw new InvalidOperationException("There is no current ResourceData.");
			
			ReplaceResourceForm form = new ReplaceResourceForm();
			form.InitialResource = CurrentData;
			form.LoadReplacement( newDataFilename );
			
			if( form.ShowDialog() != DialogResult.OK ) return;
			ResourceData newData = form.ReplacementResource;
			if(newData != null) {
				
				ResourceLang lang = CurrentData.Lang;
				lang.SwapData( newData );
				
			}
			
		}
		
		private void DataCancel() {
			
			if( CurrentData == null ) throw new InvalidOperationException("There is no current ResourceData.");
			
			CurrentSource.Cancel( CurrentData.Lang );
			
			ToolbarUpdate( false, true, false );
			
			TreeRefresh( CurrentData.Lang );
			
		}
		
		private void DataCast(ResourceLang lang, ResourceDataFactory factory) {
			
			throw new NotImplementedException();
			
		}
		
#endregion
		
#region ResourceType and ResourceName
		
		/// <summary>Lists the types in the current resource source</summary>
		private void ListLoad() {
			
			EnsureView( _viewList );
			
			_currentViewMode = ViewMode.ViewSource;
			
			_viewList.ShowObject( CurrentSource );
			
			NavigateAdd();
		}
		
		private void ListLoad(ResourceType type) {
			
			EnsureView( _viewList );
			
			_currentViewMode = ViewMode.ViewType;
			
			_viewList.ShowObject(type);
			
			NavigateAdd();
		}
		
		private void ListLoad(ResourceName name) {
			
			EnsureView( _viewList );
			
			_currentViewMode = ViewMode.ViewName;
			
			_viewList.ShowObject(name);
			
			NavigateAdd();
		}
		
#endregion
		
#region Resource Tree
		
		/// <summary>Clears and Repopulates the Resource tree based on the information gleaned from the currrent ResourceSource.</summary>
		private void TreePopulate() {
			
			__tree.BeginUpdate();
			
			__tree.Nodes.Clear();
			
			if(CurrentSource == null) return;
			
			TreeNode root = new TreeNode( CurrentSource.Name ) { Tag = CurrentSource };
			root.ImageKey = root.SelectedImageKey = "File";
			__tree.Nodes.Add( root );
			
			foreach(ResourceType type in CurrentSource.AllTypes) {
				
				TreeNode typeNode = new TreeNode( type.Identifier.FriendlyName ) { Tag = type };
				typeNode.ImageKey = typeNode.SelectedImageKey = GetTreeNodeImageListTypeKey(type.Identifier);
				
				foreach(ResourceName name in type.Names) {
					
					TreeNode nameNode = new TreeNode( name.Identifier.FriendlyName ) { Tag = name };
					
					foreach(ResourceLang lang in name.Langs) {
						
						TreeNode langNode = new TreeNode( lang.LanguageId.ToString() ) { Tag = lang };
						nameNode.Nodes.Add( langNode );
						
						langNode.ContextMenuStrip = __treeMenu;
						
						langNode.StateImageKey = TreeStateImageKey( lang );
						
					}
					
					typeNode.Nodes.Add( nameNode );
					
				}
				
				root.Nodes.Add( typeNode );
			}
			
			root.Expand();
			
			__tree.EndUpdate();
		}
		
		private String TreeStateImageKey(ResourceLang lang) {
			
			switch(lang.Action) {
				case ResourceDataAction.Add:
					return "Add";
				case ResourceDataAction.Update:
					return "Upd";
				case ResourceDataAction.Delete:
					return "Del";
				default:
					return String.Empty;
			}
		}
		
		private void TreeMenuPopulate() {
			
			ContextMenuStrip sender = __treeMenu;
			
			TreeNode node;
			
//			if( __tree.HideSelection )
//				node = (sender.SourceControl as TreeView).SelectedNode;
//			else {
				
				System.Drawing.Point p1 = __tree.PointToClient( sender.Location );
				node = __tree.GetNodeAt( p1 );
				
				if(node == null) {
					MessageBox.Show(this, "Unable to load context menu", "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					return;
				}
//			}
			
			ResourceLang lang = node.Tag as ResourceLang;
			
			String path = lang.ResourcePath;
			
			///////////////////////////////
			
			__resCMCast.Text = String.Format(Cult.CurrentCulture, "Cast {0} to", lang.Data.GetType().Name);
			
			__resCMCast.DropDownItems.Clear();
			
			ResourceDataFactory[] factories = ResourceDataFactory.GetFactoriesForType( lang.Name.Type.Identifier );
			foreach(ResourceDataFactory factory in factories) {
				
				ToolStripMenuItem castMi = new ToolStripMenuItem( factory.GetType().Name );
				castMi.Click += new EventHandler(castMi_Click);
				castMi.Tag    = new Object[] { lang, factory };
				
				__resCMCast.DropDownItems.Add( castMi );
				
				castMi.Enabled = false;
			}
			
			///////////////////////////////
			
			__resCMExtract.Text = String.Format(Cult.CurrentCulture, "Extract {0}...", path);
			__resCMReplace.Text = String.Format(Cult.CurrentCulture, "Replace {0}...", path);
			__resCMDelete .Text = String.Format(Cult.CurrentCulture, "Delete {0}"    , path);
			
			///////////////////////////////
			
			String action = lang.Action == ResourceDataAction.None ? "Operation" : lang.Action.ToString();
			
			__resCMCancel.Enabled = lang.Action == ResourceDataAction.None;
			__resCMCancel.Text    = String.Format(Cult.CurrentCulture, "Cancel {0}", action );
			
		}
		
		public static String GetTreeNodeImageListTypeKey(ResourceTypeIdentifier typeId) {
			
			switch(typeId.KnownType) {
				case Win32ResourceType.Accelerator:
					return "Accelerator";
				case Win32ResourceType.Bitmap:
					return "Bitmap";
				case Win32ResourceType.CursorDirectory:
				case Win32ResourceType.CursorImage:
					return "Cursor";
				case Win32ResourceType.Dialog:
					return "Dialog";
				case Win32ResourceType.Menu:
					return "Menu";
				case Win32ResourceType.IconDirectory:
				case Win32ResourceType.IconImage:
					return "Icon";
				case Win32ResourceType.Html:
					return "Html";
				case Win32ResourceType.Manifest:
					return "Xml";
				case Win32ResourceType.StringTable:
					return "StringTable";
				case Win32ResourceType.Toolbar:
					return "Toolbar";
				case Win32ResourceType.Version:
					return "Version";
				case Win32ResourceType.Custom:
				default:
					return "Binary";
			}
			
		}
		
#endregion
		
#region MRU
		
		private Mru Mru { get { return _mru; } }
		
		private void MruRemove(String path) {
			
			Mru.Remove( path );
			
			// refresh the MRU drop-down if it's open
		}
		
		/// <summary>Populates the MRU menu with data from the Mru instance.</summary>
		private void MruPopulate() {
			
			while( __tSrcOpen.DropDown.Items[0].Tag is String ) {
				// remove existing MRU items
				__tSrcOpen.DropDown.Items.RemoveAt( 0 );
			}
			
			String[] mruItems = Mru.Items;
			
			__tSrcMruInfo.Visible = !(__tSrcMruClear.Enabled = mruItems.Length > 0);
			
			for(int i=mruItems.Length - 1;i>=0;i--) {
				
				String text = (i+1).ToString(Cult.InvariantCulture) + ' ' + Core.Utility.Miscellaneous.TrimPath( mruItems[i], 80 );
				
				ToolStripItem item = new ToolStripMenuItem( text );
				item.Tag = mruItems[i];
				item.Click += new EventHandler(__tSrcMruItem_Click);
				
				__tSrcOpen.DropDown.Items.Insert( 0, item );
				
			}
			
		}
		
		private void MruClear() {
			Mru.Clear();
			
			while( __tSrcOpen.DropDown.Items[0].Tag is String ) {
				// remove existing MRU items
				__tSrcOpen.DropDown.Items.RemoveAt( 0 );
			}
			
			__tSrcMruInfo.Visible = !(__tSrcMruClear.Enabled = Mru.Items.Length > 0);
			
		}
		
#endregion
		
		
#region Misc
		
		private void OptionsShow() {
			
			OptionsForm options = new OptionsForm();
			if(options.ShowDialog(this) == DialogResult.OK) { // then a setting may have been changed
				
				ToolbarUpdate(false, false, true);
				
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
		
		private void SavePendingOperationsShow() {
			
			PendingOperationsForm pendingForm = new PendingOperationsForm();
			pendingForm.ShowDialog(this);
			
			// in case anything happened, it'd need to be repopulated
			TreePopulate();
			ToolbarUpdate( false, true, false );
			
		}
		
#endregion
	
		
#region Utility
		
		private void ProgramSave() {
			
			_settings.MruList.AddRange( _mru.Items ); 
			_settings.MruCount = _mru.Capacity;
			
			_settings.Save();
			
		}
		
#endregion
		
	}
}
