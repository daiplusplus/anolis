﻿using System;
using System.Text;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Resourcer.Settings;
using Anolis.Core.Data;

using Cult             = System.Globalization.CultureInfo;
using TypeViewerList   = System.Collections.Generic.List<Anolis.Resourcer.TypeViewers.TextViewer>;
using StringCollection = System.Collections.Specialized.StringCollection;

using Path     = System.IO.Path;
using File     = System.IO.File;
using FileInfo = System.IO.FileInfo;

using FileResourceSource = Anolis.Core.Source.FileResourceSource;

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
					
					_typeImages16.Images.Add("Accelerator", Properties.Resources.Type_Accelerator16);
					_typeImages16.Images.Add("Binary", Properties.Resources.Type_Binary16);
					_typeImages16.Images.Add("Bitmap", Properties.Resources.Type_Bitmap16);
					_typeImages16.Images.Add("ColorTable", Properties.Resources.Type_ColorTable16);
					_typeImages16.Images.Add("Cursor", Properties.Resources.Type_Cursor16);
					_typeImages16.Images.Add("Dialog", Properties.Resources.Type_Dialog16);
					_typeImages16.Images.Add("File", Properties.Resources.Type_File16);
					_typeImages16.Images.Add("Html", Properties.Resources.Type_Html16);
					_typeImages16.Images.Add("Icon", Properties.Resources.Type_Icon16);
					_typeImages16.Images.Add("Menu", Properties.Resources.Type_Menu16);
					_typeImages16.Images.Add("Toolbar", Properties.Resources.Type_Toolbar16);
					_typeImages16.Images.Add("Xml", Properties.Resources.Type_Xml16);
					
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
					
					_typeImages32.Images.Add("Accelerator", Properties.Resources.Type_Accelerator32);
					_typeImages32.Images.Add("Binary", Properties.Resources.Type_Binary32);
					_typeImages32.Images.Add("Bitmap", Properties.Resources.Type_Bitmap32);
					_typeImages32.Images.Add("ColorTable", Properties.Resources.Type_ColorTable32);
					_typeImages32.Images.Add("Cursor", Properties.Resources.Type_Cursor32);
					_typeImages32.Images.Add("Dialog", Properties.Resources.Type_Dialog32);
					_typeImages32.Images.Add("File", Properties.Resources.Type_File32);
					_typeImages32.Images.Add("Html", Properties.Resources.Type_Html32);
					_typeImages32.Images.Add("Icon", Properties.Resources.Type_Icon32);
					_typeImages32.Images.Add("Menu", Properties.Resources.Type_Menu32);
					_typeImages32.Images.Add("Toolbar", Properties.Resources.Type_Toolbar32);
					_typeImages32.Images.Add("Xml", Properties.Resources.Type_Xml32);
					
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
			
			__navBack.Enabled = _history.Count > 0;
			__navUp.Enabled   = (_currentViewMode != ViewMode.ViewSource && _currentViewMode != ViewMode.Other);
		}
		
		private void NavigateClear() {
			
			_history.Clear();
			__navBack.Enabled = false;
			__navUp.Enabled   = false;
			
		}
		
		private void NavigateBack() {
			
			NavigateItem item = _history.Pop();
			switch(item.ViewMode) {
				case ViewMode.ViewSource:
					
					ListLoad();
					break;
					
				case ViewMode.ViewType:
					
					ListLoad( item.Argument as ResourceType );
					break;
					
				case ViewMode.ViewName:
					
					ListLoad( item.Argument as ResourceName );
					break;
					
				case ViewMode.ViewLangData:
					
					DataLoad( item.Argument as ResourceLang );
					break;
					
				case ViewMode.Other:
					break;
			}
			
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
					item.Argument = (_viewList.CurrentObject as ResourceName).Type;
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
			
			__ofd.Filter =
				CreateFileFilter("Win32 Executable", "exe", "dll", "scr", "ocx", "cpl", "msstyles") + '|' +
//				CreateFileFilter("New Executable", "exe", "dll", "icl") + '|' +
				"All Files (*.*)|*.*";
			
			if(__ofd.ShowDialog(this) != DialogResult.OK) return;
			
			SourceLoad( __ofd.FileName, false );
			
		}
		
		/// <summary>Calls SourceUnload (which prompts the user) if there's a source currently loaded, then loads the specified source.</summary>
		private void SourceLoad(String path, Boolean removeFromMruOnError) {
			
			// TODO: Where do I ask to save unsaved changes?
			// maybe a SourceUnload method?
			
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
				
				ResourceSource source = ResourceSource.Open(path, false, ResourceSourceLoadMode.LazyLoadData );
				
				if(source == null) return;
				
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
		
		/// <summary>Prompts the user to save the CurrentSource or to cancel the operation. Returns False if operation was cancelled.</summary>
		private Boolean SourceUnload() {
			
			if( CurrentSource == null ) return true;
			
			if( CurrentSource.HasUnsavedChanges ) {
				
				int nofChanges = 0;
				foreach(ResourceLang change in CurrentSource.AllActiveLangs) nofChanges++;
				
				String message = String.Format(Cult.CurrentCulture, "Save {0} change{1} to {2} before closing?", nofChanges, nofChanges == 1 ? "" : "s", CurrentSource.Name);
				
				DialogResult r = MessageBox.Show(this, message, "Anolis Resourcer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
				switch(r) {
					
					case DialogResult.Yes:
						
						SourceSave();
						return true;
						
					case DialogResult.No:
						return true;
					case DialogResult.Cancel:
					default:
						return false;
				}
				
			} else {
				
				return true;
			}
			
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
			
			NavigateUpdateUI();
		}
		
		private void DataExport() {
			
			String filter = String.Empty;
			foreach(String f in CurrentData.SaveFileFilters) filter += f + '|';
			if(filter.EndsWith("|")) filter = filter.Substring(0, filter.Length - 1);
			
			String extensionOfFirstFilter = CurrentData.SaveFileFilters[0].Substring( CurrentData.SaveFileFilters[0].LastIndexOf('.') );
			
			__sfd.Filter = filter;
			
			String initialFilename =
				CurrentData.Lang.Name.Type.Identifier.FriendlyName + "-" +
				CurrentData.Lang.Name.Identifier.FriendlyName + "-" +
				CurrentData.Lang.LanguageId.ToString(Cult.InvariantCulture) +
				extensionOfFirstFilter;
			
			__sfd.FileName = RemoveIllegalChars( initialFilename, null );
			
			if(__sfd.ShowDialog(this) != DialogResult.OK) return;
			
			CurrentData.Save( __sfd.FileName );
			
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
			throw new NotImplementedException();
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
			
			_viewList.ShowResourceSource( CurrentSource );
			
			NavigateAdd();
			
			NavigateUpdateUI();
		}
		
		private void ListLoad(ResourceType type) {
			
			EnsureView( _viewList );
			
			_currentViewMode = ViewMode.ViewType;
			
			_viewList.ShowResourceType(type);
			
			NavigateUpdateUI();
		}
		
		private void ListLoad(ResourceName name) {
			
			EnsureView( _viewList );
			
			_currentViewMode = ViewMode.ViewName;
			
			_viewList.ShowResourceName(name);
			
			NavigateUpdateUI();
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
				typeNode.ImageKey = typeNode.SelectedImageKey = TreeNodeImageListTypeKey(type.Identifier);
				
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
			
			String path = GetResourcePath( lang );
			
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
		
		public static String TreeNodeImageListTypeKey(ResourceTypeIdentifier typeId) {
			
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
				case Win32ResourceType.Toolbar:
					return "Toolbar";
				case Win32ResourceType.Version:
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
				
				String text = (i+1).ToString(Cult.InvariantCulture) + ' ' + TrimPath( mruItems[i], 80 );
				
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
			
		}
		
#endregion
	
		
#region Utility
		
		private void ProgramSave() {
			
			_settings.MruList.AddRange( _mru.Items ); 
			_settings.MruCount = _mru.Capacity;
			
			_settings.Save();
			
		}
		
		private static String RemoveIllegalChars(String s, Char? replaceWith) {
			
			Char[] illegals = System.IO.Path.GetInvalidFileNameChars();
			Char[] str      = s.ToCharArray();
			
			if(replaceWith == null) {
				
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				
				for(int i=0;i<s.Length;i++) {
					
					Boolean isIllegal = false;
					for(int j=0;j<illegals.Length;j++) {
						if(str[i] == illegals[j]) {
							isIllegal = true;
							break;
						}
					}
					
					if(!isIllegal) sb.Append( str[i] );
					
				}
				
				return sb.ToString();
				
			} else {
				
				for(int i=0;i<s.Length;i++) {
					
					Boolean isIllegal = false;
					for(int j=0;j<illegals.Length;j++) {
						if(str[i] == illegals[j]) {
							isIllegal = true;
							break;
						}
					}
					
					if(isIllegal) str[i] = replaceWith.Value;
					
				}
				
				return new String( str );
				
			}
			
		}
		
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
		
		public static String GetResourcePath(ResourceLang lang) {
			
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
		
		private static String CreateFileFilter(String description, params String[] extensions) {
			
			StringBuilder sb = new StringBuilder();
			
			sb.Append( description );
			sb.Append( '(' );
			
			foreach(String ext in extensions) {
				sb.Append("*.");
				sb.Append( ext );
				sb.Append(';');
			}
			
			sb.Append(")|");
			
			foreach(String ext in extensions) {
				sb.Append("*.");
				sb.Append( ext );
				sb.Append(';');
			}
			
			if( sb[ sb.Length - 1 ] == ';' ) sb.Remove(sb.Length - 1, 1);
			
			String retval = sb.ToString();
			
			return retval;
			
		}
		
#endregion
		
	}
}
