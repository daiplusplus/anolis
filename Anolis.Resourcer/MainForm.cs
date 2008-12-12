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
		
		public MainForm() {
			InitializeComponent();
			
			this.Load += new EventHandler(MainForm_Load);
			__browse   .Click          += new EventHandler(__browse_Click);
			__resources.NodeMouseClick += new TreeNodeMouseClickEventHandler(__resources_NodeMouseClick);
			
		}
		
#region UI Events
		
		private void MainForm_Load(Object sender, EventArgs e) {
			LoadTypeViewers();
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
		
		private void __viewers_SelectedIndexChanged(object sender, EventArgs e) {
			
			if( _currentlyOpenResource == null ) return;
			
			
			
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
				
				
				List<TypeViewer> viewersForThisType = new List<TypeViewer>();
				foreach(TypeViewer viewer in _viewers) if( viewer.CanHandleResourceType( type ) ) viewersForThisType.Add( viewer );
				_viewersForType.Add( type, viewersForThisType );
				
			}
			
		}
		
		private ResourceType _lastType;
		private TypeViewer   _lastViewer;
		
		private class TypeViewerListWrapper {
			public Boolean Recommended { get; set; }
			public TypeViewer Viewer { get; private set; }
			public TypeViewerListWrapper(TypeViewer viewer) {
				Viewer = viewer;
			}
			public override string ToString() {
				return Viewer.ViewerName;
			}
		}
		
		private void LoadResource(ResourceLang resource) {
			
			_currentlyOpenResource = resource;
			
			ResourceType type = resource.Name.Type;
			TypeViewer selectedViewer;
			
			if( type != _lastType ) {
				
				List<TypeViewer> validViewers = _viewersForType[ type ];
				
				foreach(TypeViewerListWrapper viewer in __viewers.Items) {
					
					viewer.Recommended = validViewers.Contains( viewer.Viewer );
				}
				
				if( validViewers.Count > 0 ) selectedViewer = validViewers[0];
				else return;
				
				_lastType = type;
				_lastViewer = selectedViewer;
				
			} else {
				
				selectedViewer = _lastViewer;
				
			}
			
			ShowViewer( selectedViewer, resource );
			
		}
		
		private void ShowViewer(TypeViewer viewer, ResourceLang resource) {
			
			try {
				
				viewer.RenderResource( resource.Data );
				
			} catch (Exception ex) {
				
				
				String exTemplate = "\r\nMessage:\r\n{0}\r\n\r\nStack Trace:\r\n{1}";
				String message    = "An unhandled exception was thrown whilst trying to load the resource.\r\n";
				
				while(ex != null) {
					message += String.Format(exTemplate, ex.Message, ex.StackTrace);
					ex = ex.InnerException;
				}
				
				DialogResult result = MessageBox.Show(this, message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
				if( result == DialogResult.Cancel ) return;
				else ShowViewer(viewer, resource);
				
			}
			
			UserControl ctrl = viewer as UserControl;
			ctrl.Dock = DockStyle.Fill;
			
			__viewer.Controls.Clear();
			__viewer.Controls.Add( viewer as Control );
			
		}
		
		private void LoadTypeViewers() {
			
			// TODO: Maybe some reflection-based loading here?
			_viewers.Clear();
			_viewers.Add( new PictureViewer() );
			_viewers.Add( new IconCursorViewer() );
			_viewers.Add( new RawViewer() );
			_viewers.Add( new TextViewer() );
			_viewers.Add( new HtmlViewer() );
			
			foreach(TypeViewer viewer in _viewers) {
				
				TypeViewerListWrapper wrapper = new TypeViewerListWrapper( viewer );
				__viewers.Items.Add( wrapper );
				
			}
			
		}
		
		private void SaveCurrentResourceToFile() {
			
			if(__sfd.ShowDialog(this) != DialogResult.OK) return;
			
			_currentlyOpenResource.Data.Save( __sfd.FileName );
			
		}
		
	}
}