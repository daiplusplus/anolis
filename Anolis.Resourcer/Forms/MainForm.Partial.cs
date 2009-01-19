using System;
using System.Text;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Resourcer.Settings;
using Anolis.Core.Data;

using Cult             = System.Globalization.CultureInfo;
using TypeViewerList   = System.Collections.Generic.List<Anolis.Resourcer.TypeViewers.TextViewer>;
using StringCollection = System.Collections.Specialized.StringCollection;

using Path = System.IO.Path;
using File = System.IO.File;

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
		
#region ResourceSource
		
		/// <summary>Presents a File Open Dialog to the user and either loads the selected source or does nothing if nothing was selected or the FOD was cancelled.</summary>
		private void SourceLoadDialog() {
			
			__ofd.Filter =
				CreateFileFilter("Portable Executable", "exe", "dll", "scr", "ocx", "cpl") + '|' +
				CreateFileFilter("New Executable", "exe", "dll", "icl") + '|' +
				"All Files (*.*)|*.*";
			
			if(__ofd.ShowDialog(this) != DialogResult.OK) return;
			
			SourceLoad( __ofd.FileName, false );
			
		}
		
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
				
				ResourceSource source = ResourceSource.Open(path, false);
				
				if(source == null) return;
				
				CurrentPath = path;
				
				CurrentSource = source;
				
				Mru.Push( path );
				
				ToolbarUpdate(true, true, false);
				
				TreePopulate();
				
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
			
			if( CurrentSource.HasUnsavedChanges ) {
				
				String message = String.Format(Cult.CurrentCulture, "Save changes to {0} before closing?", CurrentSource.Name);
				
				DialogResult r = MessageBox.Show(this, message, "Anolis Resourcer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
				switch(r) {
					
					case DialogResult.Yes:
						
						SourceSave();
						return true;
						
					case DialogResult.No:
						return true;
					case DialogResult.Cancel:
						return false;
					
					default:
						return false;
				}
				
			} else {
				
				return true;
			}
			
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
			
			CurrentSource.CommitChanges(true);
			
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
			
			
			
		}
		
#endregion
		
#region ResourceData
		
		private void DataLoad(ResourceLang lang) {
			
			CurrentData = lang.Data;
			
			// Status bar
			this.__sType.Text = CurrentData.GetType().Name;
			this.__sSize.Text = CurrentData.RawData.Length.ToString(Cult.CurrentCulture) + " Bytes";
			this.__sPath.Text = CurrentPath + ',' + GetResourcePath(lang);
			
			__sType.BackColor = CurrentData is Anolis.Core.Data.UnknownResourceData ? System.Drawing.Color.LightYellow : System.Drawing.SystemColors.Control;
			
			EnsureView( _viewData );
			
			_viewData.ShowResource( CurrentData );
			
			ToolbarUpdate(false, true, false);
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
				
			}
			
			if( CurrentData is IconCursorImageResourceData ) {
				
				DialogResult r = MessageBox.Show("You are attempting to delete an Icon or Cursor member image. This will render the state of the parent Icon or Cursor Directory invalid and will cause errors in any applications attempting to read the icon. Are you sure you want to continue?", "Anolis Resourcer", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				if( r != DialogResult.OK ) return;
				
			}
			
			CurrentSource.Remove( CurrentData );
			
			ToolbarUpdate(false, true, false);
		}
		
		private void DataImport() {
			
			AddResourceForm form = new AddResourceForm();
			
			if(form.ShowDialog(this) == DialogResult.OK) {
				
				ResourceData data = form.ResourceData;
				
				ResourceTypeIdentifier typeId = form.ResourceTypeIdentifier;
				ResourceIdentifier     nameId = form.ResourceNameIdentifier;
				UInt16                 langId = form.ResourceLangId;
				
				ResourceLang lang = CurrentSource.Add( typeId, nameId, langId, data );
				
				TreeAdd( lang );
				
				TreeRefresh( lang );
				
			}
			
		}
		
		/// <summary>Replaces the current ResourceData in the ResourceSource with the provided ResourceData.</summary>
		private void DataReplace() {
			
			if( CurrentData == null ) throw new InvalidOperationException("There is no current ResourceData.");
			
			ReplaceResourceForm form = new ReplaceResourceForm();
			if( form.ShowDialog() == DialogResult.OK ) {
				
				ResourceData newData = form.NewResourceData;
				
				// TODO: Perform actual ResourceData Replace
				
			}
			
		}
		
		private void DataCancel() {
			
		}
		
#endregion
		
#region ResourceType and ResourceName
		
		private void ListLoad(ResourceType type) {
			
			EnsureView( _viewList );
			
			_viewList.ShowResourceType(type);
		}
		
		private void ListLoad(ResourceName name) {
			
			EnsureView( _viewList );
			
			_viewList.ShowResourceName(name);
		}
		
#endregion
		
#region Resource Tree
		
		/// <summary>Clears and Repopulates the Resource tree based on the information gleaned from the currrent ResourceSource.</summary>
		private void TreePopulate() {
			
			__tree.Nodes.Clear();
			
			if(CurrentSource == null) return;
			
			foreach(ResourceType type in CurrentSource.Types) {
				
				TreeNode typeNode = new TreeNode( type.Identifier.FriendlyName ) { Tag = type };
				
				foreach(ResourceName name in type.Names) {
					
					TreeNode nameNode = new TreeNode( name.Identifier.FriendlyName ) { Tag = name };
					
					foreach(ResourceLang lang in name.Langs) {
						
						TreeNode langNode = new TreeNode( lang.LanguageId.ToString() ) { Tag = lang };
						nameNode.Nodes.Add( langNode );
						
						langNode.ContextMenuStrip = __treeMenu;
						
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
				
				__tree.Nodes.Add( typeNode );
			}
			
		}
		
		private void TreeMenuPopulate() {
			
			ContextMenuStrip sender = __treeMenu;
			
			TreeNode node = ((sender as ContextMenuStrip).SourceControl as TreeView).SelectedNode;
			
			ResourceLang lang = node.Tag as ResourceLang;
			
			String path = GetResourcePath( lang );
			
			///////////////////////////////
			
			__resCMCast.Visible = false;
			
//			__resCMCast.Text = String.Format(Cult.CurrentCulture, "Cast {0} to", path);
//			
//			__resCMCast.DropDownItems.Clear();
//			
//			ResourceDataFactory[] factories = ResourceDataFactory.GetFactoriesForType( lang.Name.Type.Identifier );
//			foreach(ResourceDataFactory factory in factories) {
//				
//				ToolStripMenuItem tsmi = new ToolStripMenuItem( factory.Name );
//				tsmi.Click += new EventHandler(tsmiConvert_Click);
//				tsmi.Tag    = factory;
//				
//				__resCMCast.DropDownItems.Add( tsmi );
//			}
			
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
		
#endregion
		
#region MRU
		
		private Mru Mru { get { return _mru; } }
		
		private void MruRemove(String path) {
			
			Mru.Remove( path );
			
			// refresh the MRU drop-down if it's open
		}
		
		/// <summary>Populates the MRU menu with data from the Mru instance.</summary>
		private void MruPopulate() {
			
			__tSrcOpen.DropDown.Items.Clear();
			
			String[] mruItems = Mru.Items;
			
			for(int i=0;i<mruItems.Length;i++) {
				
				String text = (i+1).ToString(Cult.InvariantCulture) + ' ' + TrimPath( mruItems[i], 80 );
				
				ToolStripItem item = new ToolStripMenuItem( text );
				item.Tag = mruItems[i];
				item.Click += new EventHandler(__tSrcMruItem_Click);
				
				__tSrcOpen.DropDown.Items.Add( item );
				
			}
			
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
			
			if( sb[ sb.Length - 1 ] == ';' ) sb.Remove(sb.Length - 2, 1);
			
			String retval = sb.ToString();
			
			return retval;
			
		}
		
#endregion
		
	}
}
