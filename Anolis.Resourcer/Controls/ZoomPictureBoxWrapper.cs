using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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

			__toolsInfo.Click += new EventHandler(__toolsInfo_Click);
			
			LoadInterpolation();
			LoadBackgroundColors();
			
			// HACK: Until I figure out why ZPB displays scrollbars when no image is set here's a dummy:
			__pb.Image = Resources.ZPB_Empty;
		}
		
		private void __toolsInfo_Click(object sender, EventArgs e) {
			ToggleInfoView();
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
		
		private void LoadBackgroundColors() {
			
			__toolsColor.Image = __toolsColorTrans.Image;
			
			__toolsColorWhite  .Tag = Color.White;
			__toolsColorBlack  .Tag = Color.Black;
			__toolsColorGrey   .Tag = Color.Gray;
			__toolsColorMagenta.Tag = Color.Magenta;
			
			foreach(ToolStripItem item in __toolsColor.DropDownItems) {

				item.Click += new EventHandler(item_Click);
				
				if(item.Tag == null) continue;
				
				Bitmap bmp = new Bitmap( 16, 16 ); // TODO: Find out how to get the size of a toolstripitem's image
				
				using(Graphics g = Graphics.FromImage( bmp )) {
					g.FillRectangle( new SolidBrush( (Color)item.Tag ), 0, 0, 16, 16 );
				}
				item.Image = bmp;
			}
			
		}
		
		private void ToggleInfoView() {
			
			__infoPanel.Visible = __toolsInfo.Checked;
			
			RefreshInfoView();
		}
		
		private void RefreshInfoView() {
			
			if(!__infoPanel.Visible) return;
			
			if( Image != null ) {
				
				__iWidth .Text = Image.Width.ToString() + "px";
				__iHeight.Text = Image.Height.ToString() + "px";
				
				__iFormat.Text = GetRawFormatName( Image.RawFormat );
				__iPxFormat.Text = Image.PixelFormat.ToString();
				
			} else {
				
				__iWidth   .Text = "";
				__iHeight  .Text = "";
				__iFormat  .Text = "";
				__iPxFormat.Text = "";
			}
			
		}
		
		private static String GetRawFormatName(ImageFormat format) {
			
			 // there's a bug here which means it gives the GUIDs rather than the name
			String def = format.ToString();
			if(def.StartsWith("[ImageFormat:")) {
				
				if( format.Guid == ImageFormat.Bmp.Guid )  return "DIB Bitmap"; // return some Dib info too?
				if( format.Guid == ImageFormat.Emf.Guid )  return "EMF";
				if( format.Guid == ImageFormat.Exif.Guid ) return "EXIF";
				if( format.Guid == ImageFormat.Gif.Guid )  return "GIF";
				if( format.Guid == ImageFormat.Icon.Guid ) return "Icon Image";
				if( format.Guid == ImageFormat.Jpeg.Guid ) return "JPEG";
				if( format.Guid == ImageFormat.MemoryBmp.Guid ) return "In-Memory DIB Bitmap";
				if( format.Guid == ImageFormat.Png.Guid )  return "PNG";
				if( format.Guid == ImageFormat.Tiff.Guid ) return "TIFF";
				if( format.Guid == ImageFormat.Wmf.Guid )  return "WMF";
				
			}
			
			return def;
			
		}
		
		private void item_Click(Object sender, EventArgs e) {
			
			ToolStripItem item = sender as ToolStripItem;
			if(item.Tag == null) {
				
				__pb.BackColor       = SystemColors.Window;
				__pb.BackgroundImage = Anolis.Resourcer.Resources.ImageViewer_TransparentBg;
				
			} else {
				
				__pb.BackColor       = (Color)item.Tag;
				__pb.BackgroundImage = null;
			}
			
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
			set {
				__pb.Image = value;
				RefreshInfoView();
			}
		}
		
	}
}
