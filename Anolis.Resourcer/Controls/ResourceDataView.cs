using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Core.Data;
using Anolis.Resourcer.TypeViewers;

namespace Anolis.Resourcer.Controls {
	
	public partial class ResourceDataView : UserControl {
		
		private class TypeViewerWrapper {
			
			public TypeViewerCompatibility Recommended { get; set; }
			public TypeViewer Viewer { get; private set; }
			public TypeViewerWrapper(TypeViewer viewer) {
				Viewer = viewer;
			}
			public override string ToString() {
				return Viewer.ViewerName +
					(Recommended == TypeViewerCompatibility.Ideal ? " (Recommended)" : 
					(Recommended == TypeViewerCompatibility.None  ? " (Not recommended)" : ""));
			}
			public static TypeViewerWrapper[] FromArray(params TypeViewer[] viewers) {
				
				TypeViewerWrapper[] retval = new TypeViewerWrapper[viewers.Length];
				for(int i=0;i<viewers.Length;i++) retval[i] = new TypeViewerWrapper( viewers[i] );
				
				return retval;
			}
		}
		
		private ResourceData _data;
		private TypeViewerWrapper[] _viewers;
		
		private TypeViewer _currentViewer;
		
		public ResourceDataView() {
			
			InitializeComponent();
			
			// reverse the array because ShowResource chooses the /last/ compatible viewer. Reversing this list makes RawViewer preferable over TextViewer
			TypeViewerWrapper[] viewers = TypeViewerWrapper.FromArray(
#if MEDIAVIEWER
				new MediaViewer(),
#endif
				new MenuDialogViewer(),
				new StringTableViewer(),
				new VersionViewer(),
				new ImageViewer(),
				new IconCursorViewer(),
				new RawViewer(),
				new TextViewer(),
				new SgmlViewer()
			);
			
			Array.Reverse( viewers );
			_viewers = viewers;
			
			__viewers.SelectionChangeCommitted  += new EventHandler(__viewers_SelectionChangeCommitted); // don't use SelectedIndexChange since that's raised by my code
		}
		
#region UI Events
		
		private void __viewers_SelectionChangeCommitted(object sender, EventArgs e) {
			
			TypeViewer viewer = (__viewers.SelectedItem as TypeViewerWrapper).Viewer;
			
			ShowViewer( viewer, _data );
		}
		
#endregion
		
		public void ShowResource(ResourceData data) {
			
			_data = data;
			
			// Set up the TypeViewers list
			
			TypeViewerWrapper lastIdeal = null;
			TypeViewerWrapper lastWorks = null;
			
			// There will always be something that "Works" (i.e. the Binary one, so don't worry about NREs)
			
			List<TypeViewerWrapper> iViewers = new List<TypeViewerWrapper>();
			List<TypeViewerWrapper> wViewers = new List<TypeViewerWrapper>();
			
			foreach(TypeViewerWrapper w in _viewers) {
				
				w.Recommended = w.Viewer.CanHandleResource(data);
				
				if(w.Recommended == TypeViewerCompatibility.Ideal) { lastIdeal = w; iViewers.Add( w ); }
				if(w.Recommended == TypeViewerCompatibility.Works) { lastWorks = w; wViewers.Add( w ); }
				
			}
			
			// Select the right viewer
			
			TypeViewerWrapper wrapper = lastIdeal != null ? lastIdeal : lastWorks;
			
			__viewers.Items.Clear();
			
			__viewers.Items.AddRange( iViewers.ToArray() );
			__viewers.Items.AddRange( wViewers.ToArray() );
			
			__viewers.SelectedItem = wrapper;
			
			ShowViewer( wrapper.Viewer, _data );
			
		}
		
		private void ShowViewer(TypeViewer viewer, ResourceData data) {
			
#if !DEBUG
			try {
				
				viewer.RenderResource( data );			
			} catch (AnolisException ex) {
				
				String exTemplate = "\r\nMessage:\r\n{0}\r\n\r\nStack Trace:\r\n{1}";
				String message    = "An unhandled exception was thrown whilst trying to load the resource.\r\n";
				
				Exception e = ex;
				while(e != null) {
					message += String.Format(exTemplate, ex.Message, ex.StackTrace);
					e = e.InnerException;
				}
				
				DialogResult result = MessageBox.Show(this, message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
				if( result == DialogResult.Cancel ) return;
				else ShowViewer(viewer, data);
				
			}
#else
			viewer.RenderResource( data );
#endif
			
			// don't load it if it's already the currently displayed viewer
			if( _currentViewer != viewer ) {
				
				viewer.Dock = DockStyle.Fill;
				
				__viewer.Controls.Clear();
				__viewer.Controls.Add( viewer );
				
				_currentViewer = viewer;
			}

			
		}
		
	}
}
