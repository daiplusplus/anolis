using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;

using Cult = System.Globalization.CultureInfo;

using Anolis.Core;
using Anolis.Resourcer.Controls;

namespace Anolis.Resourcer {
	
	public partial class MainForm : BaseForm {
		
		private ResourceDataView _viewData;
		private ResourceListView _viewList;
		
		public MainForm() {
			InitializeComponent();
			
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
			
			this.__tSrcOpen.ButtonClick     += new EventHandler(__tSrcOpen_ButtonClick);
			this.__tSrcOpen.DropDownOpening += new EventHandler(__tSrcOpen_DropDownOpening);
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

			this.__tree.NodeMouseClick += new TreeNodeMouseClickEventHandler(__tree_NodeMouseClick);
			this.__tree.BeforeCollapse += new TreeViewCancelEventHandler(__tree_BeforeCollapse);
			this.__tree.AfterSelect         += new TreeViewEventHandler(__tree_AfterSelect);
			this.__treeMenu.Opening         += new System.ComponentModel.CancelEventHandler(__treeMenu_Opening);
			
			//this.__treeStateImages.Images.Add( "Add", Resources.Tree_Add );
			//this.__treeStateImages.Images.Add( "Upd", Resources.Tree_Edit );
			//this.__treeStateImages.Images.Add( "Del", Resources.Tree_Delete );
			
			_history = new System.Collections.Generic.Stack<NavigateItem>();
			__navBack.Click += new EventHandler(__navBack_Click);
			__navUp.Click += new EventHandler(__navUp_Click);
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
			
			Mru.Save( Settings.Settings.Default );
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
			
			if(!lang.DataIsLoaded) node.StateImageKey = "";
			else {
				switch(lang.Action) {
					case Anolis.Core.Data.ResourceDataAction.Add:
						node.StateImageKey = "Add";
						return;
					case Anolis.Core.Data.ResourceDataAction.Delete:
						node.StateImageKey = "Del";
						return;
					case Anolis.Core.Data.ResourceDataAction.None:
						node.StateImageKey = "";
						return;
					case Anolis.Core.Data.ResourceDataAction.Update:
						node.StateImageKey = "Upd";
						return;
				}
			}
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
		
		private void __tSrcMruItem_Click(Object sender, EventArgs e) {
			
			String path = (sender as ToolStripItem).Tag as String;
			
			this.SourceLoad( path, null, true );
		}
		
		private void __tSrcMruClear_Click(Object sender, EventArgs e) {
			
			MruClear();
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
			
			SavePendingOperationsShow();
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
				
				Boolean is24 = Settings.Settings.Default.Toolbar24;
				
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
	
#endregion

	}
}