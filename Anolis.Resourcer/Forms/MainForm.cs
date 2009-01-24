using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Anolis.Resourcer.TypeViewers;
using System.IO;

using Cult = System.Globalization.CultureInfo;

using Anolis.Core;
using Anolis.Core.Data;
using Anolis.Resourcer.Controls;
using Anolis.Resourcer.Settings;

namespace Anolis.Resourcer {
	
	public partial class MainForm : BaseForm {
		
		private ResourceDataView _viewData;
		private ResourceListView _viewList;
		
		public MainForm() {
			InitializeComponent();
			
			MainFormInit();
			
			this.AllowDrop = true;
			
			this.Load      += new EventHandler(MainForm_Load);
			this.DragDrop  += new DragEventHandler(MainForm_DragDrop);
			this.DragEnter += new DragEventHandler(MainForm_DragEnter);
			
			ToolStripManager.Renderer = new Anolis.Resourcer.Controls.ToolStripImprovedSystemRenderer();
			
			this.__tSrcOpen.ButtonClick     += new EventHandler(__tSrcOpen_ButtonClick);
			this.__tSrcOpen.DropDownOpening += new EventHandler(__tSrcOpen_DropDownOpening);
			this.__tSrcSave.Click           += new EventHandler(__tSrcSave_Click);
			this.__tSrcReve.Click           += new EventHandler(__tSrcReve_Click);
			this.__tResAdd.Click            += new EventHandler(__tResAdd_Click);
			this.__tResExt.Click            += new EventHandler(__tResExt_Click);
			this.__tResRep.Click            += new EventHandler(__tResRep_Click);
			this.__tResDel.Click            += new EventHandler(__tResDel_Click);
			this.__tResCan.Click            += new EventHandler(__tResCan_Click);
			this.__tGenOpt.Click            += new EventHandler(__tGenOptions_Click);
			
			this.__tree.AfterSelect         += new TreeViewEventHandler(__tree_AfterSelect);
			this.__treeMenu.Opening         += new System.ComponentModel.CancelEventHandler(__treeMenu_Opening);
			
			//this.__treeStateImages.Images.Add( "Add", Properties.Resources.Tree_Add );
			//this.__treeStateImages.Images.Add( "Upd", Properties.Resources.Tree_Edit );
			//this.__treeStateImages.Images.Add( "Del", Properties.Resources.Tree_Delete );
			
			_viewData = new ResourceDataView();
			_viewList = new ResourceListView();
			
			_viewList.ItemActivated += new EventHandler<ResourceListViewEventArgs>(_viewList_ItemActivated);
			_viewList.SelectedItemChanged += new EventHandler<ResourceListViewEventArgs>(_viewList_SelectedItemChanged);
		}
		
		public String[] CommandLineArgs { get; set; }
		
		private void MainForm_Load(Object sender, EventArgs e) {
			
			// check for Wow64
			
			if( Anolis.Core.Utility.Environment.IsWow64 ) {
				
				String message = "Anolis Resourcer has detected it is running under WOW64 and so is accessing the system through a compatibility layer. This is not a recommended or supported scenario and modifications to system files might not take effect in the way you imagine.";
				
				MessageBox.Show(this, message, "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
				
			}
			
			ToolbarUpdate(true, true, true);
			StatusbarUpdate();
			
			if( CommandLineArgs != null && CommandLineArgs.Length > 0 ) {
				
				String filename = CommandLineArgs[0];
				if( File.Exists( filename ) ) {
					
					SourceLoad( filename, false );
					
				}
				
			}
			
		}
		
#region UI Events
		
	#region Treeview
		
		private void __tree_AfterSelect(Object sender, TreeViewEventArgs e) {
			
			TreeNode node = e.Node;
			
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
				switch(lang.Data.Action) {
					case ResourceDataAction.Add:
						node.StateImageKey = "Add";
						return;
					case ResourceDataAction.Delete:
						node.StateImageKey = "Del";
						return;
					case ResourceDataAction.None:
						node.StateImageKey = "";
						return;
					case ResourceDataAction.Update:
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
			
			this.SourceLoad( path, true );
		}
		
		private void __tSrcReve_Click(Object sender, EventArgs e) {
			
			this.SourceRevert();
		}
		
		private void __tSrcSave_Click(Object sender, EventArgs e) {
			
			this.SourceSave();
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
					__tResDel.Enabled = this.CurrentData != null;
					__tResCan.Enabled = this.CurrentData != null && this.CurrentData.Action != ResourceDataAction.None;
					
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
				
				__tSrcOpen.Image            = is24 ? Properties.Resources.Toolbar_SrcOpen24 : Properties.Resources.Toolbar_SrcOpen;
				__tSrcSave.Image            = is24 ? Properties.Resources.Toolbar_SrcSave24 : Properties.Resources.Toolbar_SrcSave;
				__tSrcReve.Image            = is24 ? Properties.Resources.Toolbar_SrcRev24  : Properties.Resources.Toolbar_SrcRev;
				
				__tResAdd.Image             = is24 ? Properties.Resources.Toolbar_ResAdd24 : Properties.Resources.Toolbar_ResAdd;
				__tResExt.Image             = is24 ? Properties.Resources.Toolbar_ResExt24 : Properties.Resources.Toolbar_ResExt;
				__tResRep.Image             = is24 ? Properties.Resources.Toolbar_ResRep24 : Properties.Resources.Toolbar_ResRep;
				__tResDel.Image             = is24 ? Properties.Resources.Toolbar_ResDel24 : Properties.Resources.Toolbar_ResDel;
				__tResCan.Image             = is24 ? Properties.Resources.Toolbar_ResCan24 : Properties.Resources.Toolbar_ResCan;
				
				__tGenOpt.Image             = is24 ? Properties.Resources.Toolbar_GenOpt24 : Properties.Resources.Toolbar_GenOpt;
				
				this.ResumeLayout(true);
			
			}
			
		}
		
		private void StatusbarUpdate() {
			
			if( CurrentData != null ) {
				
				this.__sType.Text = CurrentData.GetType().Name;
				this.__sSize.Text = CurrentData.RawData.Length.ToString(Cult.CurrentCulture) + " Bytes";
				this.__sPath.Text = CurrentPath + ',' + GetResourcePath(CurrentData.Lang);
				
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
		
	#endregion
		
	#region Drag 'n' Drop
		
		private void MainForm_DragDrop(Object sender, DragEventArgs e) {
			
			if( !e.Data.GetDataPresent(DataFormats.FileDrop) ) return;
				
			String[] filenames = e.Data.GetData(DataFormats.FileDrop) as String[];
			if(filenames == null) return;
			if(filenames.Length < 1) return;
			
			// just load the first file
			
			SourceLoad( filenames[0], false );
			
		}
		
		private void MainForm_DragEnter(Object sender, DragEventArgs e) {

			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				
				e.Effect = DragDropEffects.Move;
				
			} else {
				
				e.Effect = DragDropEffects.None;
				
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
		
#endregion

	}
}