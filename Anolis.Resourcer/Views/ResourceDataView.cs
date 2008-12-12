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
		
		public ResourceDataView() {
			
			InitializeComponent();


			__viewers.SelectedIndexChanged += new EventHandler(__viewers_SelectedIndexChanged);
		}
		
#region UI Events
		
		private void __viewers_SelectedIndexChanged(object sender, EventArgs e) {
			
			TypeViewer viewer = (__viewers.SelectedItem as TypeViewerListWrapper).Viewer;
			
			ShowViewer( viewer, _currentlyOpenResource );
			
		}
		
#endregion
		
		public void ShowResource(ResourceData data) {
			
			// Set up the TypeViewers list
			
			// Select the right viewer
			
		}
		
		private void ShowViewer(TypeViewer viewer, ResourceData resource) {
			
			
			
		}
		
	}
}
