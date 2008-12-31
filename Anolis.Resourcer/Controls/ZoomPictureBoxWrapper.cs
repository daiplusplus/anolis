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
			
			// HACK: Until I figure out why ZPB displays scrollbars when no image is set here's a dummy:
			__pb.Image = Properties.Resources.ZPB_Empty;
		}
		
		private void __toolsCenter_Click(object sender, EventArgs e) {
			
			__pb.Centered = __toolsCenter.Checked;
			
		}
		
		private void LoadInterpolation() {
			
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
		
		private static Single[] _zoomLevels = { // the same levels as Photoshop, I might add
			1, 2, 3, 4, 5, 6.25f, 100f / 12f, 12.5f, 100f / 6f, 25, 100f / 3f, 50, 200f / 3f,
			100,
			200, 300, 400, 500, 600, 700, 800, 1200, 1600
		};
		
		private Int32 _currentZoomLevel = 13; // 100
		
		private void __pb_ZoomChanged(object sender, EventArgs e) {
			__toolsZoom.Text = Math.Round(__pb.Zoom * 100).ToString() + '%';
		}
		
		private void __toolsZoomIn_Click(object sender, EventArgs e) {
			//__pb.Zoom += 0.2f;
			
			_currentZoomLevel++;
			if(_currentZoomLevel >= _zoomLevels.Length) _currentZoomLevel--;
			
			__pb.Zoom = _zoomLevels[ _currentZoomLevel ] / 100f;
			
		}
		
		private void __toolsZoomOut_Click(object sender, EventArgs e) {
			//Single valueToZoom = __pb.Zoom - 0.2f;
			//if(valueToZoom > 0) __pb.Zoom = valueToZoom;
			
			_currentZoomLevel--;
			if(_currentZoomLevel <= 0) _currentZoomLevel++;
			
			__pb.Zoom = _zoomLevels[ _currentZoomLevel ] / 100f;
			
		}
		
		private void __toolsZoom100_Click(object sender, EventArgs e) {
			_currentZoomLevel = 13;
			__pb.Zoom = _zoomLevels[ _currentZoomLevel ] / 100f;
			//__pb.Zoom = 1f;
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
