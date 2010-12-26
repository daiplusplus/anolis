using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;

using Cult = System.Globalization.CultureInfo;

using Anolis.Core;
using Anolis.Resourcer.Controls;
using ARSettings = Anolis.Resourcer.Settings.ARSettings;

namespace Anolis.Resourcer {
	
	public partial class MainForm : BaseForm {
		
		private ResourceDataView _viewData;
		private ResourceListView _viewList;
		
		private FindForm _findForm;
		
		public MainForm() {
			InitializeComponent();
			
			// This is set in BaseForm, so override it so the main form opens in the right location rather than in the middle of <desktop>
			this.StartPosition = FormStartPosition.WindowsDefaultLocation;
			
			MainFormInit();
			
			this.AllowDrop = true;
			
			this.Load      += new EventHandler(MainForm_Load);
			this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
			this.DragEnter += new DragEventHandler(MainForm_DragEnter);
			this.DragLeave += new EventHandler(MainForm_DragLeave);
			
			this.__dropTarget.DragEnter       += new DragEventHandler(__dropTarget_DragEnter);
			this.__dropTarget.DragLeave2      += new EventHandler(__dropTarget_DragLeave);
			this.__dropTarget.DropDataAdd     += new EventHandler(__dropTarget_DropDataAdd);
			this.__dropTarget.DropDataReplace += new EventHandler(__dropTarget_DropDataReplace);
			this.__dropTarget.DropSourceLoad  += new EventHandler(__dropTarget_DropSourceLoad);
			
			ToolStripManager.Renderer = new Anolis.Resourcer.Controls.ToolStripImprovedSystemRenderer();
			
#region Toolstrip Events
			
			this.__tSrcNew.Click            += new EventHandler(__tSrcNew_Click);
			this.__tSrcOpen.ButtonClick     += new EventHandler(__tSrcOpen_ButtonClick);
			this.__tSrcOpen.DropDownOpening += new EventHandler(__tSrcOpen_DropDownOpening);
			this.__tSrcBatch.Click          += new EventHandler(__tSrcBatch_Click);
			this.__tSrcMruClear.Click       += new EventHandler(__tSrcMruClear_Click);
			this.__tSrcSave.ButtonClick     += new EventHandler(__tSrcSave_Click);
			this.__tSrcSaveBackup.Click     += new EventHandler(__tSrcSaveBackup_Click);
			this.__tSrcSavePending.Click    += new EventHandler(__tSrcSavePending_Click);
			this.__tSrcReve.Click           += new EventHandler(__tSrcReve_Click);
			this.__tResAdd.Click            += new EventHandler(__tResAdd_Click);
			this.__tResExt.Click            += new EventHandler(__tResExt_Click);
			this.__tResRep.Click            += new EventHandler(__tResRep_Click);
			this.__tResDel.Click            += new EventHandler(__tResDel_Click);
			this.__tResCan.Click            += new EventHandler(__tResCan_Click);
			this.__tGenOpt.Click            += new EventHandler(__tGenOptions_Click);
			
#endregion
			
			this.__tree.NodeMouseClick += new TreeNodeMouseClickEventHandler(__tree_NodeMouseClick);
			this.__tree.BeforeCollapse += new TreeViewCancelEventHandler(__tree_BeforeCollapse);
			this.__tree.AfterSelect         += new TreeViewEventHandler(__tree_AfterSelect);
			this.__treeMenu.Opening         += new System.ComponentModel.CancelEventHandler(__treeMenu_Opening);
			
#region Menu Events
			
			this.__mFileNew         .Click += new EventHandler(__mFileNew_Click);
			this.__mFileOpen        .Click += new EventHandler(__mFileOpen_Click);
			this.__mFileSave        .Click += new EventHandler(__mFileSave_Click);
			this.__mFileClose       .Click += new EventHandler(__mFileClose_Click);
			this.__mFileBackup      .Click += new EventHandler(__mFileBackup_Click);
			this.__mFileRevert      .Click += new EventHandler(__mFileRevert_Click);
			this.__mFileProperties.Click += new EventHandler(__mFileProperties_Click);
			this.__mFileExit        .Click += new EventHandler(__mFileExit_Click);
			
			this.__mEditCut         .Click += new EventHandler(__mEditCut_Click);
			this.__mEditCopy        .Click += new EventHandler(__mEditCopy_Click);
			this.__mEditPaste       .Click += new EventHandler(__mEditPaste_Click);
			this.__mEditFind        .Click += new EventHandler(__mEditFind_Click);
			this.__mEditFindNext     .Click += new EventHandler(__mEditFindNext_Click);
			this.__mEditSelectAll   .Click += new EventHandler(__mEditSelectAll_Click);
			
			this.__mViewToolbar     .Click += new EventHandler(__mViewToolbar_Click);
			this.__mViewToolbarLarge.Click += new EventHandler(__mViewToolbarLarge_Click);
			this.__mViewToolbarSmall.Click += new EventHandler(__mViewToolbarSmall_Click);
			this.__mViewMenus       .Click += new EventHandler(__mViewMenus_Click);
			this.__mViewEffects     .Click += new EventHandler(__mViewEffects_Click);
			
			this.__mActionImport    .Click += new EventHandler(__mActionImport_Click);
			this.__mActionExport    .Click += new EventHandler(__mActionExport_Click);
			this.__mActionReplace   .Click += new EventHandler(__mActionReplace_Click);
			this.__mActionDelete    .Click += new EventHandler(__mActionDelete_Click);
			this.__mActionCancel    .Click += new EventHandler(__mActionCancel_Click);
			
			this.__mToolsBatch      .Click += new EventHandler(__mToolsBatch_Click);
			this.__mToolsOptions    .Click += new EventHandler(__mToolsOptions_Click);
			this.__mToolsPending    .Click += new EventHandler(__mToolsPending_Click);
			
			this.__mHelpTopics      .Click += new EventHandler(__mHelpTopics_Click);
			this.__mHelpUpdates     .Click += new EventHandler(__mHelpUpdates_Click);
			this.__mHelpAbout       .Click += new EventHandler(__mHelpAbout_Click);
			
			this.__t.ContextMenu = __c;

			this.__c.Popup += new EventHandler(__c_Popup);
			
			this.__cToolbar         .Click += new EventHandler(__cToolbar_Click);
			this.__cToolbarLarge    .Click += new EventHandler(__cToolbarLarge_Click);
			this.__cToolbarSmall    .Click += new EventHandler(__cToolbarSmall_Click);
			this.__cMenu            .Click += new EventHandler(__cMenu_Click);
			
#endregion

			this._findForm.FindNextClicked += new EventHandler(_findForm_FindNextClicked);
			
			//this.__treeStateImages.Images.Add( "Add", Resources.Tree_Add );
			//this.__treeStateImages.Images.Add( "Upd", Resources.Tree_Edit );
			//this.__treeStateImages.Images.Add( "Del", Resources.Tree_Delete );
			
			_history = new System.Collections.Generic.Stack<NavigateItem>();
			this.__navBack.Click += new EventHandler(__navBack_Click);
			this.__navUp.Click += new EventHandler(__navUp_Click);
			this.__tree.ImageList = MainForm.TypeImages16;
			
			_viewData = new ResourceDataView();
			_viewList = new ResourceListView();
			
			_viewList.ItemActivated += new EventHandler<ResourceListViewEventArgs>(_viewList_ItemActivated);
			_viewList.SelectedItemChanged += new EventHandler<ResourceListViewEventArgs>(_viewList_SelectedItemChanged);
		}
		
		public String OpenSourceOnLoad { get; set; }
		
		private void MainForm_Load(Object sender, EventArgs e) {
			
			// check for Wow64
			
			if( Anolis.Core.Utility.Environment.IsWow64 ) {
				
				String message = "Anolis Resourcer has detected it is running under WOW64 and so is accessing the system through a compatibility layer. This is not a recommended or supported scenario and modifications to system files might not take effect in the way you imagine.";
				
				MessageBox.Show(this, message, "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
				
			}
			
			ToolbarUpdate(true, true, true);
			StatusbarUpdate();
			
			if( OpenSourceOnLoad != null && OpenSourceOnLoad.Length > 0 && File.Exists( OpenSourceOnLoad ) ) {
				
				SourceLoad( OpenSourceOnLoad, null, false );
			}
			
			// Ugly hack time, pre-initialise the Cultures class on a separate thread
			
			Thread thread = new Thread( delegate() { Cultures.Initialise(); } );
			thread.Start();
			
		}
		
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			
			e.Cancel = !SourceUnload();
			
			Mru.Save( ARSettings.Default );
		}
		
#region UI Events
		
	#region Treeview
		
		private void __tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
			// TODO: Find a way to enable the Loading of items that are selected, but were re-clicked
			// if the line below is uncommented then the list is loaded twice which causes a threading exception
//			__tree_AfterSelect(sender, new TreeViewEventArgs(e.Node, TreeViewAction.ByMouse) );
		}
		
		private void __tree_BeforeCollapse(object sender, TreeViewCancelEventArgs e) {
			
			// prevent collapse of the root node: the source
			
			if( __tree.Nodes.Count != 1 ) return;
			
			TreeNode root = __tree.Nodes[0];
			if(root == e.Node) e.Cancel = true;
			
		}
		
		private void __tree_AfterSelect(Object sender, TreeViewEventArgs e) {
			
			System.Diagnostics.Debug.WriteLine("afterSelect");
			
			TreeNode node = e.Node;
			
			ResourceSource source = node.Tag as ResourceSource;
			if(source != null) { ListLoad(); return; }
			
			ResourceType type = node.Tag as ResourceType;
			if(type != null) { ListLoad(type); return; }
			
			ResourceName name = node.Tag as ResourceName;
			if(name != null) {
				
				if(name.Langs.Count == 1) {
					
					DataLoad( name.Langs[0] );
					
				} else {
					
					ListLoad( name );
				}
				
				return;
			}
			
			ResourceLang lang = node.Tag as ResourceLang;
			if(lang != null) DataLoad( lang );
			
		}
		
		/// <summary>Updates the appearance of the TreeNode corresponding to the specified ResourceLang</summary>
		private void TreeRefresh(ResourceLang lang) {
			
			TreeNode node = TreeFindNode(lang);
			if(node == null) return;
			
			ResourceLang nLang = node.Tag as ResourceLang;
			if(lang ==  null || nLang != lang) return;
			
			if(!lang.DataIsLoaded)
				node.StateImageKey = "";
			else
				node.StateImageKey = TreeStateImageKey( lang );
			
		}
		
		private void TreeNodeEnsureVisible(Object tag) {
			
			TreeNode node = TreeFindNode( tag );
			if(node != null) node.EnsureVisible();
		}
		
		private TreeNode TreeFindNode(Object tag) {
			
			foreach(TreeNode root in __tree.Nodes) {
				
				if(root.Tag == tag) return root;
				
				TreeNode ret = TreeFindNode(root, tag);
				if(ret != null) return ret;
			}
			
			return null;
			
		}
		
		private static TreeNode TreeFindNode(TreeNode node, Object tag) {
			
			foreach(TreeNode n in node.Nodes) {
				
				if(n.Tag == tag) {
					return n;
				} else {
					TreeNode ret = TreeFindNode(n, tag);
					if(ret != null) return ret;
				}
				
			}
			
			return null;
			
		}
		
		private void TreeAdd(ResourceLang lang) {
			
			ResourceType type = lang.Name.Type;
			ResourceName name = lang.Name;
			
			TreeNode nodeForType = TreeFindNode(type);
			TreeNode nodeForName;
			TreeNode nodeForLang;
			
			if(nodeForType == null) {
				
				nodeForType = new TreeNode( type.Identifier.FriendlyName ) { Tag = type };
				
				__tree.Nodes.Add( nodeForType );
				
				// then there won't be one for the name, so add it
				nodeForName = new TreeNode( name.Identifier.FriendlyName ) { Tag = name };
				
				nodeForType.Nodes.Add( nodeForName );
				
				//
				nodeForLang = new TreeNode( lang.LanguageId.ToString() ) { Tag = lang };
				nodeForLang.ImageKey = TreeStateImageKey( lang );
				
				nodeForName.Nodes.Add( nodeForLang );
				
				return;
			}
			
			nodeForName = TreeFindNode(name);
			if(nodeForName == null) {
				
				nodeForName = new TreeNode( name.Identifier.FriendlyName ) { Tag = name };
				
				nodeForType.Nodes.Add( nodeForName );
			}
			
			//
			nodeForLang = new TreeNode( lang.LanguageId.ToString() ) { Tag = lang };
			
			nodeForName.Nodes.Add( nodeForLang );
			
		}
		
	#endregion
		
	#region Toolbar
		
		private void __tSrcOpen_ButtonClick(Object sender, EventArgs e) {
			
			this.SourceLoadDialog();
		}
		
		private void __tSrcOpen_DropDownOpening(Object sender, EventArgs e) {
			
			this.MruPopulate();
		}
		
		private void __tSrcNew_Click(object sender, EventArgs e) {
			
			this.SourceNew();
		}
		
		private void __tSrcMruItem_Click(Object sender, EventArgs e) {
			
			String path = (sender as ToolStripItem).Tag as String;
			
			this.SourceLoad( path, null, true );
		}
		
		private void __tSrcMruClear_Click(Object sender, EventArgs e) {
			
			this.MruClear();
		}
		
		private void __tSrcBatch_Click(Object sender, EventArgs e) {
			
			this.SourceBatchProcessShow();
		}
		
		private void __tSrcReve_Click(Object sender, EventArgs e) {
			
			this.SourceRevert();
		}
		
		private void __tSrcSave_Click(Object sender, EventArgs e) {
			
			this.SourceSave();
		}
		
		private void __tSrcSaveBackup_Click(Object sender, EventArgs e) {
			
			this.SourceBackup();
		}
		
		private void __tSrcSavePending_Click(Object sender, EventArgs e) {
			
			this.SavePendingOperationsShow();
		}
		
		///////////////////////////////////////
		
		private void __tResAdd_Click(Object sender, EventArgs e) {
			
			this.DataImport();
		}
		
		private void __tResExt_Click(Object sender, EventArgs e) {
			
			this.DataExport();
		}
		
		private void __tResRep_Click(object sender, EventArgs e) {
			
			this.DataReplace();
		}
		
		private void __tResDel_Click(object sender, EventArgs e) {
			
			this.DataRemove();
		}
		
		private void __tResCan_Click(Object sender, EventArgs e) {
			
			this.DataCancel();
		}
		
		///////////////////////////////////////
		
		private void __tGenOptions_Click(Object sender, EventArgs e) {
			
			this.OptionsShow();
		}
		
		private void ToolbarUpdate(Boolean updateSourceDetails, Boolean updateDataDetails, Boolean updateToolbarShape) {
			
			Boolean isReadOnly = (CurrentSource == null) ? true : CurrentSource.IsReadOnly;
			
			MenubarUpdate(isReadOnly);
			
			if( updateSourceDetails ) {
				
				//////////////////////////////////////
				// Title Bar
				
				if(CurrentSource == null)
					this.Text = "Anolis Resourcer";
				else
					this.Text = CurrentSource.Name + (isReadOnly ? " [Read-Only]" : "") + " - Anolis Resourcer";
				
				if(CurrentSource != null) {
					__tSrcSaveBackup.Enabled = !CurrentSource.IsReadOnly;
					__tSrcSaveBackup.Text = "Backup " + CurrentSource.Name + "...";
				}
				
			}
			
			if( updateDataDetails ) {
				
				//////////////////////////////////////
				// Toolbar Enabled
				
				__tSrcSave.Enabled = !isReadOnly;
				__tSrcReve.Enabled = !isReadOnly;
				
				__tResAdd.Enabled = !isReadOnly;
				__tResRep.Enabled = !isReadOnly;
				__tResExt.Enabled = this.CurrentData != null;
				__tResDel.Enabled = !isReadOnly;
				__tResCan.Enabled = !isReadOnly;
				
				if( !isReadOnly ) {
					
					__tResRep.Enabled = this.CurrentData != null;
					__tResExt.Enabled = this.CurrentData != null;
					__tResDel.Enabled = this.CurrentData != null && this.CurrentData.Lang.Action != Anolis.Core.Data.ResourceDataAction.Delete;
					__tResCan.Enabled = this.CurrentData != null && this.CurrentData.Lang.Action != Anolis.Core.Data.ResourceDataAction.None;
					
				}
			
			}
			
			////////////////////////////////////
			// Toolbar Shape
			
			if( updateToolbarShape ) {
				
				this.SuspendLayout();
				
				Boolean is24 = ARSettings.Default.Toolbar24;
				
				__t.ImageScalingSize         = is24 ? new System.Drawing.Size(24, 24)   : new System.Drawing.Size(48, 48);
				
				__tSrcOpen.TextImageRelation = is24 ? TextImageRelation.ImageBeforeText : TextImageRelation.ImageAboveText ;
				__tSrcSave.DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
				__tSrcReve.DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
				
				__tResAdd.TextImageRelation = is24 ? TextImageRelation.ImageBeforeText : TextImageRelation.ImageAboveText;
				__tResExt.TextImageRelation = is24 ? TextImageRelation.ImageBeforeText : TextImageRelation.ImageAboveText;
				__tResRep.TextImageRelation = is24 ? TextImageRelation.ImageBeforeText : TextImageRelation.ImageAboveText;
				__tResDel.DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
				__tResCan.DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
				
				__tGenOpt.DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
				
				__tSrcOpen.Image            = is24 ? Resources.Toolbar_SrcOpen24 : Resources.Toolbar_SrcOpen;
				__tSrcSave.Image            = is24 ? Resources.Toolbar_SrcSave24 : Resources.Toolbar_SrcSave;
				__tSrcReve.Image            = is24 ? Resources.Toolbar_SrcRev24  : Resources.Toolbar_SrcRev;
				
				__tResAdd.Image             = is24 ? Resources.Toolbar_ResAdd24 : Resources.Toolbar_ResAdd;
				__tResExt.Image             = is24 ? Resources.Toolbar_ResExt24 : Resources.Toolbar_ResExt;
				__tResRep.Image             = is24 ? Resources.Toolbar_ResRep24 : Resources.Toolbar_ResRep;
				__tResDel.Image             = is24 ? Resources.Toolbar_ResDel24 : Resources.Toolbar_ResDel;
				__tResCan.Image             = is24 ? Resources.Toolbar_ResCan24 : Resources.Toolbar_ResCan;
				
				__tGenOpt.Image             = is24 ? Resources.Toolbar_GenOpt24 : Resources.Toolbar_GenOpt;
				
				this.ResumeLayout(true);
			
			}
			
		}
		
		private void StatusbarUpdate() {
			
			if( CurrentData != null ) {
				
				this.__sType.Text = CurrentData.GetType().Name;
				this.__sSize.Text = CurrentData.RawData.Length.ToString(Cult.CurrentCulture) + " Bytes";
				this.__sPath.Text = CurrentPath + ',' + CurrentData.Lang.ResourcePath;
				
			} else if(CurrentSource != null) {
				
				this.__sType.Text = "Ready";
				this.__sSize.Text = "";
				this.__sPath.Text = CurrentPath;
				
			} else {
				
				this.__sType.Text = "";
				this.__sSize.Text = "";
				this.__sPath.Text = "Ready";
				
			}
			
			this.__sType.BackColor = CurrentData is Anolis.Core.Data.UnknownResourceData ? System.Drawing.Color.LightYellow : System.Drawing.SystemColors.Control;
			
		}
		
	#endregion
	
	#region Resource Context Menu
		
		private void __treeMenu_Opening(Object sender, System.ComponentModel.CancelEventArgs e) {
			
			this.TreeMenuPopulate();
		}
		
		private void castMi_Click(Object sender, EventArgs e) {
			
			Object[] paras = (sender as ToolStripMenuItem).Tag as Object[];
			
			DataCast( paras[0] as ResourceLang, paras[1] as Anolis.Core.Data.ResourceDataFactory );
		}
		
	#endregion
		
	#region Drag 'n' Drop
		
		private void MainForm_DragEnter(Object sender, DragEventArgs e) {
			
			System.Diagnostics.Debug.WriteLine("MainForm_DragEnter");
			
			if(e.Data.GetDataPresent(DataFormats.FileDrop)) {
				
				__dropTarget.DropDataAddEnabled     = CurrentSource != null;
				__dropTarget.DropDataReplaceEnabled = CurrentData != null;
				
				__dropTarget.Location = new System.Drawing.Point( (Width - __dropTarget.Width)/2, (Height - __dropTarget.Height)/2 );
				__dropTarget.Visible = true;
				__dropTarget.BringToFront();
				
			}
		}
		
		private Boolean __dropTargetHasDrag;
		
		private void __dropTarget_DragEnter(object sender, DragEventArgs e) {
			
			__dropTargetHasDrag = true;
			
			System.Diagnostics.Debug.WriteLine("dropTarget_DragEnter");
			
			__dropTarget.DropDataAddEnabled     = CurrentSource != null && !CurrentSource.IsReadOnly;
			__dropTarget.DropDataReplaceEnabled = CurrentData   != null && !CurrentSource.IsReadOnly;
			
			__dropTarget.Visible = true;
			
		}
		
		private void __dropTarget_DragLeave(object sender, EventArgs e) {
			
			__dropTargetHasDrag = false;
		}
		
		private void MainForm_DragLeave(object sender, EventArgs e) {
			
			System.Diagnostics.Debug.WriteLine("MainForm_DragLeave");
			
			// MainForm_DragLeave is fired before __dropTarget_DragEnter, hmm
			
			// delay 20ms. The __dropTarget_DragEnter will set a flag to show the drag is currently on there
			// if it isn't, hide it
			
			// gah, this doesn't seem to be working right
			System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
			t.Interval = 20;
			t.Tick += new EventHandler(delegate(Object s, EventArgs ea) {
				
				if( !__dropTargetHasDrag ) __dropTarget.Visible = false;
				t.Stop();
			});
			t.Start();
		}
		
		private void __dropTarget_DropSourceLoad(object sender, EventArgs e) {
			
			String filename = __dropTarget.DropFile;
			
			SourceLoad( filename, null, false );
			
		}
		
		private void __dropTarget_DropDataReplace(object sender, EventArgs e) {
			
			if( CurrentSource != null && CurrentData != null && !CurrentSource.IsReadOnly ) {
				
				DataReplace( __dropTarget.DropFile );
				
			}
			
		}
		
		private void __dropTarget_DropDataAdd(object sender, EventArgs e) {
			
			if( CurrentSource != null && !CurrentSource.IsReadOnly ) {
				
				DataImport( __dropTarget.DropFile );
				
			}
			
		}
		
	#endregion
	
	#region List View
		
		private void _viewList_SelectedItemChanged(Object sender, ResourceListViewEventArgs e) {
			
			if(e.SelectedItem != null) {
				
				if(e.Mode == ResourceListViewMode.Name) {
					
					ResourceName name = e.SelectedItem as ResourceName;
					if(name != null) {
						
						if(name.Langs.Count == 1) {
							
							ResourceLang lang = name.Langs[0];
							
							DataSelect( lang );
							return;
							
						}
						
					}
					
				} else if(e.Mode == ResourceListViewMode.Lang) {
					
					ResourceLang lang = e.SelectedItem as ResourceLang;
					if(lang != null) {
						
						DataSelect( lang );
						return;
						
					}
					
				}
				
			}
			
			DataDeselect();
			
		}
		
		private void _viewList_ItemActivated(Object sender, ResourceListViewEventArgs e) {
			
			if(e.SelectedItem != null) {
				
				switch(e.Mode) {
					case ResourceListViewMode.Type:
						
						ListLoad( e.SelectedItem as ResourceType );
						
						break;
					case ResourceListViewMode.Name:
						
						ListLoad( e.SelectedItem as ResourceName );
						
						break;
					case ResourceListViewMode.Lang:
						
						DataLoad( e.SelectedItem as ResourceLang );
						
						break;
				}
				
			}
			
		}
		
	#endregion
	
	#region Navigation
		
		private void __navUp_Click(object sender, EventArgs e) {
			
			NavigateUp();
		}
		
		private void __navBack_Click(object sender, EventArgs e) {
			
			NavigateBack();
		}
		
	#endregion
	
	#region Main Menu
		
		private void MenubarUpdate(Boolean isReadOnly) {
			
			////////////////////////
			// Enable/Disable
			
			__mFileSave  .Enabled = !isReadOnly;
			__mFileBackup.Enabled = !isReadOnly;
			__mFileRevert.Enabled = !isReadOnly;
			__mFileClose .Enabled = CurrentSource != null;
			__mFileProperties.Enabled = CurrentSource as Anolis.Core.Source.FileResourceSource != null;
			
			__mToolsPending.Enabled = !isReadOnly;
			
			__mActionImport .Enabled = !isReadOnly;
			__mActionExport .Enabled = this.CurrentData != null;
			__mActionReplace.Enabled = !isReadOnly;
			__mActionDelete .Enabled = !isReadOnly;
			__mActionCancel .Enabled = !isReadOnly;
			
			if( !isReadOnly ) {
				
				__mActionReplace.Enabled = this.CurrentData != null;
				__mActionExport .Enabled = this.CurrentData != null;
				__mActionDelete .Enabled = this.CurrentData != null && this.CurrentData.Lang.Action != Anolis.Core.Data.ResourceDataAction.Delete;
				__mActionCancel .Enabled = this.CurrentData != null && this.CurrentData.Lang.Action != Anolis.Core.Data.ResourceDataAction.None;
				
			}
			
			////////////////////////
			// MRU
			
			__mFileRecent.MenuItems.Clear();
			
			if( _mru.Items.Length == 0 ) {
				
				__mFileRecent.Enabled = false;
				
			} else {
				
				__mFileRecent.Enabled = true;
				
				foreach(String fileName in _mru.Items) {
					
					MenuItem mruItem = new MenuItem( fileName );
					
					__mFileRecent.MenuItems.Add( mruItem );
				}
				
				
			}
			
			////////////////////////
			// View Menu
			
			__mViewToolbar.Checked      = ARSettings.Default.ToolbarVisible;
			
			__mViewToolbarSmall.Checked = ARSettings.Default.Toolbar24;
			__mViewToolbarLarge.Checked = !__mViewToolbarSmall.Checked;
			
			__mViewMenus.Checked        = ARSettings.Default.MenuVisible;
		}
		
		#region File
		
		private void __mFileNew_Click(object sender, EventArgs e) {
			
			SourceNew();
		}
		
		private void __mFileOpen_Click(object sender, EventArgs e) {
			
			SourceLoadDialog();
		}
		
		private void __mFileSave_Click(object sender, EventArgs e) {
			
			SourceSave();
		}
		
		private void __mFileClose_Click(object sender, EventArgs e) {
			
			SourceUnload();
		}
		
		private void __mFileBackup_Click(object sender, EventArgs e) {
			
			SourceBackup();
		}
		
		private void __mFileRevert_Click(object sender, EventArgs e) {
			
			SourceRevert();
		}
		
		private void __mFileProperties_Click(object sender, EventArgs e) {
			
			SourcePropertiesShow();
		}
		
		private void __mFileExit_Click(object sender, EventArgs e) {
			
			if( SourceUnload() ) {
				
				Close();
			}
		}
		
		#endregion
		
		#region Edit
		
		private void __mEditCut_Click(object sender, EventArgs e) {
			
		}
		
		private void __mEditCopy_Click(object sender, EventArgs e) {
			
		}
		
		private void __mEditPaste_Click(object sender, EventArgs e) {
			
		}
		
		private void __mEditFind_Click(object sender, EventArgs e) {
			
			FindShow();
		}
		
		private void __mEditFindNext_Click(object sender, EventArgs e) {
			
			FindNext();
		}
		
		private void __mEditSelectAll_Click(object sender, EventArgs e) {
			
		}
		
		
		#endregion
		
		#region View
		
		private void __mViewEffects_Click(object sender, EventArgs e) {
			
			Boolean useGimmicks = !__mViewEffects.Checked;
			
			ARSettings.Default.Gimmicks = useGimmicks;
			
			__mViewEffects.Checked = useGimmicks;
		}
		
		private void __mViewMenus_Click(object sender, EventArgs e) {
			
			// you need to manually update .Checked
			
			Boolean showMenu = !__mViewMenus.Checked;
			
			if( !showMenu && !__t.Visible ) return; // don't allow the user to hide both at the same time
			
			__mViewToolbar.Enabled = showMenu;
			__mViewMenus  .Checked = showMenu;
			
			// you can't hide a MainMenu itself, only its root items
			foreach(MenuItem item in __menu.MenuItems) item.Visible = showMenu;
			
			// TODO: Save this to settings
		}
		
		private void __mViewToolbarSmall_Click(object sender, EventArgs e) {
			
			if( __mViewToolbarSmall.Checked ) return; // don't bother re-setting it
			
			ARSettings.Default.Toolbar24 = true;
			
			ToolbarUpdate(false, false, true);
			
			__mViewToolbarSmall.Checked = true;
			__mViewToolbarLarge.Checked = false;
		}
		
		private void __mViewToolbarLarge_Click(object sender, EventArgs e) {
			
			if( __mViewToolbarLarge.Checked ) return; // don't bother re-setting it
			
			ARSettings.Default.Toolbar24 = false;
			
			ToolbarUpdate(false, false, true);
			
			__mViewToolbarSmall.Checked = false;
			__mViewToolbarLarge.Checked = true;
		}
		
		private void __mViewToolbar_Click(object sender, EventArgs e) {
			
			Boolean showToolbar = !__mViewToolbar.Checked;
			
			if( !showToolbar && !__menu.MenuItems[0].Visible ) return; // don't allow the user to hide both at the same time
			
			__mViewMenus  .Enabled = showToolbar;
			__mViewToolbar.Checked = showToolbar;
			
			__t.Visible = showToolbar;
			
			// TODO: Save this to settings
		}
		
		#endregion
		
		#region Action
		
		private void __mActionCancel_Click(object sender, EventArgs e) {
			
			DataCancel();
		}
		
		private void __mActionDelete_Click(object sender, EventArgs e) {
			
			DataRemove();
		}
		
		private void __mActionReplace_Click(object sender, EventArgs e) {
			
			DataReplace();
		}
		
		private void __mActionExport_Click(object sender, EventArgs e) {
			
			DataExport();
		}
		
		private void __mActionImport_Click(object sender, EventArgs e) {
			
			DataImport();
		}
		
		#endregion
		
		#region Tools
		
		private void __mToolsOptions_Click(object sender, EventArgs e) {
			
			OptionsShow();
		}
		
		private void __mToolsBatch_Click(object sender, EventArgs e) {
			
			SourceBatchProcessShow();
		}
		
		private void __mToolsPending_Click(object sender, EventArgs e) {
			
			SavePendingOperationsShow();
		}
		
		#endregion
		
		#region Help
		
		private void __mHelpAbout_Click(object sender, EventArgs e) {
			
			OptionsAboutShow();
		}
		
		private void __mHelpUpdates_Click(object sender, EventArgs e) {
			
			OptionsUpdateShow();
		}
		
		private void __mHelpTopics_Click(object sender, EventArgs e) {
			
			HelpShow();
		}
		
		#endregion
		
	#endregion
	
	#region ToolbarContext Menu
		
		private void __c_Popup(object sender, EventArgs e) {
			
			__cMenu   .Checked = __menu.MenuItems[0].Visible; // need to test first child, can't check actual menu's visiblity
			__cToolbar.Checked = __t.Visible;
			
			__cToolbarSmall.Checked = ARSettings.Default.Toolbar24;
			__cToolbarLarge.Checked = !__cToolbarSmall.Checked;
			
			if( !__t.Visible                 ) __cMenu   .Enabled = false;
			if( !__menu.MenuItems[0].Visible ) __cToolbar.Enabled = false;
			
		}
		
		private void __cMenu_Click(object sender, EventArgs e) {
			
			__mViewMenus_Click(sender, e);
		}
		
		private void __cToolbarSmall_Click(object sender, EventArgs e) {
			
			__mViewToolbarSmall_Click(sender, e);
		}
		
		private void __cToolbarLarge_Click(object sender, EventArgs e) {
			
			__mViewToolbarLarge_Click(sender, e);
		}
		
		private void __cToolbar_Click(object sender, EventArgs e) {
			
			__mViewToolbar_Click(sender, e);
		}
		
	#endregion
	
	#region Find Events
		
		private void _findForm_FindNextClicked(object sender, EventArgs e) {
			
			FindForm form = sender as FindForm;
			
			if( _currentFind == null || _currentFind.FindText != form.FindText ) {
				
				_currentFind = new Finder( CurrentSource, form.FindText, form.FindOptions );
			}
				
			FindNext();
			
		}
		
	#endregion
	
#endregion
	
	}
}