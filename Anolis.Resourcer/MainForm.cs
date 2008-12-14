using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Anolis.Resourcer.TypeViewers;
using System.IO;

using Cult = System.Globalization.CultureInfo;

using Anolis.Core;
using Anolis.Core.PE;

namespace Anolis.Resourcer {
	
	public partial class MainForm : Form {
		
		private ResourceLang   _currentResource;
		private ResourceSource _currentSource;
		private String         _currentPath;
		
		private ResourceDataView _viewData;
		private ResourceListView _viewList;
		
		public MainForm() {
			InitializeComponent();
			
			this.Load += new EventHandler(MainForm_Load);
			__resources.NodeMouseClick += new TreeNodeMouseClickEventHandler(__resources_NodeMouseClick);
			
			this.__tSrcOpen.Click += new EventHandler(__tSrcOpen_Click);
			
			_viewData = new ResourceDataView();
			_viewList = new ResourceListView();
		}
		
#region UI Events
	
	#region Toolbar
		
		private void __tSrcOpen_Click(object sender, EventArgs e) {
			
			if(__ofd.ShowDialog(this) != DialogResult.OK) return;
			
			LoadSource( _currentPath = __ofd.FileName );
			
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
		
		public StatusStrip StatusBar { get { return __status; } }
		
		private void LoadSource(String path) {
			
			this.Text = Path.GetFileName( path ) + " - Anolis Resourcer";
			
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
			
			_currentResource = resource;
			
			ResourceData data = resource.Data;
			
			// Status bar
			this.__sType.Text = data.GetType().Name;
			this.__sSize.Text = data.RawData.Length.ToString(Cult.CurrentCulture) + " Bytes";
			this.__sPath.Text = _currentPath; // TODO: Add in resource location details
			
			__sType.BackColor = data is Anolis.Core.Data.UnknownResourceData ? System.Drawing.Color.LightYellow : System.Drawing.SystemColors.Control;
			
			EnsureView( _viewData );
			
			_viewData.ShowResource( data );
			
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
			
			_currentResource.Data.Save( __sfd.FileName );
			
		}
		
	}
}