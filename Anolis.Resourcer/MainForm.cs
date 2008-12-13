using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Anolis.Resourcer.TypeViewers;
using System.IO;

using Anolis.Core;
using Anolis.Core.PE;

namespace Anolis.Resourcer {
	
	public partial class MainForm : Form {
		
		private ResourceLang _currentlyOpenResource;
		
		private ResourceDataView _viewData;
		private ResourceListView _viewList;
		
		public MainForm() {
			InitializeComponent();
			
			this.Load += new EventHandler(MainForm_Load);
			__resources.NodeMouseClick += new TreeNodeMouseClickEventHandler(__resources_NodeMouseClick);
			
			
			
			_viewData = new ResourceDataView();
			_viewList = new ResourceListView();
		}
		
#region UI Events
		
		private void MainForm_Load(Object sender, EventArgs e) {
			
		}
		
		private void __browse_Click(Object sender, EventArgs e) {
			
			if(__ofd.ShowDialog(this) != DialogResult.OK) return;
			
			LoadSource( __filePath.Text = __ofd.FileName );
			
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
		
		public StatusStrip StatusBar { get { return __status; } }
		
		private void LoadSource(String path) {
			
			ResourceSource source = ResourceSource.Open(path, true);
			
			ResourceTypeCollection types = source.Types;
			
			__resources.Nodes.Clear();
			foreach(ResourceType type in types) {
				
				TreeNode typeNode = new TreeNode( type.Identifier.FriendlyName ) { Tag = type };
				
				foreach(ResourceName name in type.Names) {
					
					TreeNode nameNode = new TreeNode( name.Identifier.FriendlyName ) { Tag = name };
					
					foreach(ResourceLang lang in name.Langs) {
						
						TreeNode langNode = new TreeNode( lang.LanguageId.ToString() ) { Tag = lang };
						nameNode.Nodes.Add( langNode );
						
					}
					
					typeNode.Nodes.Add( nameNode );
					
				}
				
				__resources.Nodes.Add( typeNode );
			}
			
		}
		
		private void LoadResource(ResourceLang resource) {
			
			_currentlyOpenResource = resource;
			
			ResourceType type = resource.Name.Type;
			
			EnsureView( _viewData );
			
			_viewData.ShowResource( resource.Data );
			
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
		
		private void SaveCurrentResourceToFile() {
			
			if(__sfd.ShowDialog(this) != DialogResult.OK) return;
			
			_currentlyOpenResource.Data.Save( __sfd.FileName );
			
		}
		
	}
}