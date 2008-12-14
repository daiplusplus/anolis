using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Resourcer.TypeViewers;

namespace Anolis.Resourcer {
	
	public partial class ResourceDataView : UserControl {
		
		private class TypeViewerWrapper {
			public TypeViewerCompatibility Recommended { get; set; }
			public TypeViewer Viewer { get; private set; }
			public TypeViewerWrapper(TypeViewer viewer) {
				Viewer = viewer;
			}
			public override string ToString() {
				return Viewer.ViewerName +
					(Recommended == TypeViewerCompatibility.Ideal ? " *" : 
					(Recommended == TypeViewerCompatibility.None  ? " (Not recommended)" : ""));
			}
		}
		
		private ResourceData _data;
		private List<TypeViewer> _viewers;
		
		public ResourceDataView() {
			
			InitializeComponent();
			
			_viewers = new List<TypeViewer>();
			_viewers.AddRange( new TypeViewer[] {
				new PictureViewer(), new IconCursorViewer(), new RawViewer(), new TextViewer(), new HtmlViewer()
			});
			
			foreach(TypeViewer viewer in _viewers) __viewers.Items.Add( new TypeViewerWrapper( viewer ) );
			
			__viewers.SelectionChangeCommitted  += new EventHandler(__viewers_SelectionChangeCommitted); // don't use SelectedIndexChange since that's raised by my code
		}
		
#region UI Events
		
		private void __viewers_SelectionChangeCommitted(object sender, EventArgs e) {
			
			TypeViewer viewer = (__viewers.SelectedItem as TypeViewerWrapper).Viewer;
			
			ShowViewer( viewer, _data );
		}
		
#endregion
		
		private TypeViewerWrapper GetWrapperForTypeViewer(TypeViewer viewer) {
			
			foreach(TypeViewerWrapper w in __viewers.Items) {
				
				if( w.Viewer == viewer ) return w;
			}
			return null;
		}
		
		public void ShowResource(ResourceData data) {
			
			_data = data;
			
			// Set up the TypeViewers list
			
			TypeViewerWrapper lastIdeal = null;
			TypeViewerWrapper lastWorks = null;
			
			// There will always be something that "Works" (i.e. the Binary one, so don't worry about NREs)
			
			foreach(TypeViewer v in _viewers) {
				
				TypeViewerWrapper w;
				
				(w = GetWrapperForTypeViewer( v )).Recommended = v.CanHandleResource(data);
				
				if(w.Recommended == TypeViewerCompatibility.Ideal) lastIdeal = w;
				if(w.Recommended == TypeViewerCompatibility.Works) lastWorks = w;
				
			}
			
			// Select the right viewer
			
			TypeViewerWrapper wrapper = lastIdeal != null ? lastIdeal : lastWorks;
			
			__viewers.SelectedItem = wrapper;
			
			ShowViewer( wrapper.Viewer, _data );
			
		}
		
		private void ShowViewer(TypeViewer viewer, ResourceData data) {
			
			try {
				
				viewer.RenderResource( data );
				
			} catch (Exception ex) {
				
				
				String exTemplate = "\r\nMessage:\r\n{0}\r\n\r\nStack Trace:\r\n{1}";
				String message    = "An unhandled exception was thrown whilst trying to load the resource.\r\n";
				
				while(ex != null) {
					message += String.Format(exTemplate, ex.Message, ex.StackTrace);
					ex = ex.InnerException;
				}
				
				DialogResult result = MessageBox.Show(this, message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
				if( result == DialogResult.Cancel ) return;
				else ShowViewer(viewer, data);
				
			}
			
			UserControl ctrl = viewer as UserControl;
			ctrl.Dock = DockStyle.Fill;
			
			__viewer.Controls.Clear();
			__viewer.Controls.Add( viewer as Control );
			
		}
		
	}
}
