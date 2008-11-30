using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Anolis.Resourcer.TypeViewers;
using System.IO;

using Anolis.Core;

namespace Anolis.Resourcer {
	
	public partial class MainForm : Form {
		
		private List<TypeViewer> _viewers;
		private Dictionary<Win32ResourceType, List<TypeViewer>> _viewersForType;
		
		private Win32ResourceLanguage _currentlyOpenResource;
		
		public MainForm() {
			InitializeComponent();
			
			_viewers = new List<TypeViewer>();
			_viewersForType = new Dictionary<Win32ResourceType,List<TypeViewer>>();
			
			this.Load += new EventHandler(MainForm_Load);
			__browse.Click += new EventHandler(__browse_Click);
			__resources.NodeMouseClick += new TreeNodeMouseClickEventHandler(__resources_NodeMouseClick);

			__viewers.SelectedIndexChanged += new EventHandler(__viewers_SelectedIndexChanged);
		}
		
#region UI Events
		
		private void MainForm_Load(Object sender, EventArgs e) {
			LoadTypeViewers();
		}
		
		private void __browse_Click(Object sender, EventArgs e) {
			
			if(__ofd.ShowDialog(this) != DialogResult.OK) return;
			
			LoadImage( __filePath.Text = __ofd.FileName );
			
		}
		
		private void __resources_NodeMouseClick(Object sender, TreeNodeMouseClickEventArgs e) {
			
			TreeNode node = e.Node;
			Win32ResourceLanguage lang = node.Tag as Win32ResourceLanguage;
			if(lang == null) return;			
			
			LoadResource( lang );
			
		}
		
		private void __viewers_SelectedIndexChanged(object sender, EventArgs e) {
			
			if( _currentlyOpenResource == null ) return;
			
			TypeViewer viewer = (__viewers.SelectedItem as TypeViewerListWrapper).Viewer;
			
			ShowViewer( viewer, _currentlyOpenResource );
			
		}
		
		private void __saveRaw_Click(object sender, EventArgs e) {
			SaveCurrentResourceToFile();
		}
		
#endregion
		
		private void LoadImage(String path) {
			
			Win32Image image = new Win32Image( path, true );
			
			Win32ResourceType[] types = image.GetResourceTypes();
			
			__resources.Nodes.Clear();
			foreach(Win32ResourceType type in types) {
				
				TreeNode typeNode = new TreeNode( type.FriendlyName ) { Tag = type };
				
				foreach( Win32ResourceName name in type.Names ) {
					
					TreeNode nameNode = new TreeNode( name.FriendlyName ) { Tag = name };
					
					foreach( Win32ResourceLanguage lang in name.Languages ) {
						
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
		
		private Win32ResourceType _lastType;
		private TypeViewer       _lastViewer;
		
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
		
		private void LoadResource( Win32ResourceLanguage resource ) {
			
			_currentlyOpenResource = resource;
			
			Win32ResourceType type = resource.ParentName.ParentType;
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
		
		private void ShowViewer(TypeViewer viewer, Win32ResourceLanguage resource) {
			
			try {
				
				viewer.RenderResource( resource );
				
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
			
			Byte[] data = _currentlyOpenResource.GetData();
			
			String path = __sfd.FileName;
			using(FileStream stream = new FileStream( path, FileMode.Create ))
			using(BinaryWriter wtr = new BinaryWriter( stream )) {
				wtr.Write( data );
			}
			
		}
		
	}
}