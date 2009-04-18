using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Core.Data;
using Anolis.Resourcer.TypeViewers;

namespace Anolis.Resourcer.Controls {
	
	public partial class ResourceDataView : UserControl {
		
		private ResourceData _data;
		
		private TypeViewer _currentViewer;
		
		public ResourceDataView() {
			
			InitializeComponent();
			
			__viewers.SelectionChangeCommitted  += new EventHandler(__viewers_SelectionChangeCommitted); // don't use SelectedIndexChange since that's raised by my code
		}
		
#region UI Events
		
		private void __viewers_SelectionChangeCommitted(object sender, EventArgs e) {
			
			TypeViewer viewer = (__viewers.SelectedItem as TypeViewerWrapper).Viewer;
			
			ShowViewer( viewer, _data );
		}
		
#endregion
		
		public ResourceData CurrentData { get { return _data; } }
		
		public void ShowResource(ResourceData data) {
			
			_data = data;
			
			// Set up the TypeViewers list
			
			TypeViewerWrapper lastIdeal = null;
			TypeViewerWrapper lastWorks = null;
			
			// There will always be something that "Works" (i.e. the Binary one, so don't worry about NREs)
			
			List<TypeViewerWrapper> iViewers = new List<TypeViewerWrapper>();
			List<TypeViewerWrapper> wViewers = new List<TypeViewerWrapper>();
			
			foreach(TypeViewerWrapper w in TypeViewerFactory.GetViewers() ) {
				
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
