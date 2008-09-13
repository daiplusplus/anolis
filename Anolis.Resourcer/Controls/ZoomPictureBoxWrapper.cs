using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Anolis.Resourcer.Controls {
	
	public partial class ZoomPictureBoxWrapper : UserControl {
		
		public ZoomPictureBoxWrapper() {
			InitializeComponent();
			
			__toolsZoomIn .Click += new EventHandler(__toolsZoomIn_Click);
			__toolsZoomOut.Click += new EventHandler(__toolsZoomOut_Click);
			__toolsZoom100.Click += new EventHandler(__toolsZoom100_Click);
			__toolsCenter .Click += new EventHandler(__toolsCenter_Click);
			__pb.ZoomChanged += new EventHandler(__pb_ZoomChanged);
			
			LoadInterpolation();
		}
		
		private void __toolsCenter_Click(object sender, EventArgs e) {
			
			__pb.Centered = __toolsCenter.Checked;
			
		}
		
		private void LoadInterpolation() {
			
//			Array    values = Enum.GetValues( typeof(InterpolationMode) );
//			String[] names  = Enum.GetNames ( typeof(InterpolationMode) );
//			
//			for(int i=0;i<names.Length;i++) {
//			
//				ToolStripMenuItem item = new ToolStripMenuItem( names[i] ) { Tag = (Int32)values.GetValue( i ) };
//				
//				__toolsInterp.DropDownItems.Add( item );
//			}
			
			__toolsInterp.DropDownItems.Add( new ToolStripMenuItem("Nearest Neighbour)")     { Tag = InterpolationMode.NearestNeighbor } );
			__toolsInterp.DropDownItems.Add( new ToolStripMenuItem("Bilinear")               { Tag = InterpolationMode.Bilinear } );
			__toolsInterp.DropDownItems.Add( new ToolStripMenuItem("Bilinear (Prefiltered)") { Tag = InterpolationMode.HighQualityBilinear } );
			__toolsInterp.DropDownItems.Add( new ToolStripMenuItem("Bicubic")                { Tag = InterpolationMode.Bicubic } );
			__toolsInterp.DropDownItems.Add( new ToolStripMenuItem("Bicubic (Prefiltered)")  { Tag = InterpolationMode.HighQualityBicubic } );
			
			__toolsInterp.DropDownItemClicked += new ToolStripItemClickedEventHandler( delegate(Object sender, ToolStripItemClickedEventArgs e) {
				ToolStripItem item = e.ClickedItem;
				InterpolationMode mode = (InterpolationMode)item.Tag;
				
				__pb.InterpolationMode = mode;
			});
			
		}
		
		private void __pb_ZoomChanged(object sender, EventArgs e) {
			__toolsZoom.Text = Math.Round(__pb.Zoom * 100).ToString() + '%';
		}
		
		private void __toolsZoomIn_Click(object sender, EventArgs e) {
			__pb.Zoom += 0.2f;
		}
		
		private void __toolsZoomOut_Click(object sender, EventArgs e) {
			Single valueToZoom = __pb.Zoom - 0.2f;
			if(valueToZoom > 0) __pb.Zoom = valueToZoom;
		}
		
		private void __toolsZoom100_Click(object sender, EventArgs e) {
			__pb.Zoom = 1f;
		}
		
		//////////////////////////////////////////
		
		public ZoomPictureBox PictureBox {
			get { return __pb; }
		}
		
		public Boolean ShowToolbar {
			get { return __tools.Visible; }
			set { __tools.Visible = value; }
		}
		
		public Boolean ShowToolbarText {
			get { return __toolsZoom100.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText; }
			set {
				ToolStripItemDisplayStyle style = value ? ToolStripItemDisplayStyle.ImageAndText : ToolStripItemDisplayStyle.Image;
				__toolsZoomIn .DisplayStyle = style;
				__toolsZoomOut.DisplayStyle = style;
				__toolsZoom100.DisplayStyle = style;
				__toolsInterp .DisplayStyle = style;
			}
		}
		
		public Boolean ShowInterpolationDropDown {
			get { return __toolsInterp.Visible; }
			set { __toolsInterp.Visible = value; }
		}
		
		//////////////////////////////////////////
		// Shortcut properties
		
		public Image Image {
			get { return __pb.Image; }
			set { __pb.Image = value; }
		}
		
	}
}
