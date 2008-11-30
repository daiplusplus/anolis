using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Anolis.Resourcer.Controls {
	
	[Description("Represents a Windows picture box control for displaying, panning, and zooming an image."), Designer(typeof(ZoomPictureBoxDesigner)), DefaultProperty("Image")]
	public partial class ZoomPictureBox : ScrollableControl {
		
		private Image             _image;
		private Single            _zoom              = 1f;
		private InterpolationMode _interpolationMode = InterpolationMode.HighQualityBicubic;
		private Boolean           _centered          = true;
		
		public ZoomPictureBox() {
			InitializeComponent();
			
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			AutoScroll = true;
			
			Scroll += new ScrollEventHandler(ZoomPictureBox_Scroll);
		}
		
		private void ZoomPictureBox_Scroll(object sender, ScrollEventArgs e) {
			Invalidate();
		}
		
		[Category("Appearance"), Description("The image to be displayed.")]
		public Image Image {
			get { return _image; }
			set {
				_image = value;
				UpdateScaleFactor();
				Invalidate();
			}
		}
		
		protected void UpdateScaleFactor() {
			
			if( _image == null ) {
				AutoScrollMargin = this.Size;
			} else {
				Single width  =  _image.Width * _zoom + 0.5F;
				Single height = _image.Height * _zoom + 0.5F;
				AutoScrollMinSize = new Size( (Int32)width, (Int32)height );
			}
			
		}
		
		[Category("Appearance"), Description("The zoom factor. Less than 1 to reduce. 1 for 100%. More than 1 to magnify. Set to -1 to stretch or shrink to fit but an image must be specified.")]
		public Single Zoom {
			get { return _zoom; }
			set {
				if(_image != null && value == -1) {
					
					// stretch or shrink to fit, get the right zoom factor
					Rectangle cli = DeflateRectangle(ClientRectangle, Padding);
					
					Single horizZoom = cli.Width / _image.Width;
					Single vertiZoom = cli.Height / _image.Height;
					
					value = Math.Min( horizZoom, vertiZoom ); // choose the smaller zoom factor
					
				}
				
				if( value <= 0) {
					throw new ArgumentOutOfRangeException("value", value, "Zoom value must be greater than zero (or -1 when an image is specified).");
				}
				_zoom = value;
				UpdateScaleFactor();
				Invalidate();
				
				if( ZoomChanged != null ) ZoomChanged(this, EventArgs.Empty );
			}
		}
		
		[Category("Appearance"), Description("The interpolation mode used to smooth the drawing.")]
		public InterpolationMode InterpolationMode {
			get { return _interpolationMode; }
			set {
				_interpolationMode = value;
				Invalidate();
			}
		}
		
		[Category("Appearance"), Description("If the image will be displayed in the center or the top-left corner.")]
		public Boolean Centered {
			get { return _centered; }
			set {
				if(value != _centered) {
					_centered = value;
					Invalidate();
				}
			}
		}
		
		/*protected override void OnPaintBackground(PaintEventArgs e) {
			// Do nothing
		}*/
		
		protected override void OnPaint(PaintEventArgs e) {
			
			if( _image == null ) {
				base.OnPaint(e);
				return;
			}
			
			Matrix zoomMatrix = new Matrix(_zoom, 0, 0, _zoom, 0, 0);
			zoomMatrix.Translate( AutoScrollPosition.X / _zoom, AutoScrollPosition.Y / _zoom);
			
			e.Graphics.Transform = zoomMatrix;
			e.Graphics.InterpolationMode = _interpolationMode;
			
			e.Graphics.DrawImage( _image, GetRectangle(Centered), 0, 0, _image.Width, _image.Height, GraphicsUnit.Pixel );
			
			base.OnPaint(e);
		}
		
		private static Rectangle DeflateRectangle(Rectangle rect, Padding padding) {
			rect.X += padding.Left;
			rect.Y += padding.Top;
			rect.Width -= padding.Horizontal;
			rect.Height -= padding.Vertical;
			return rect;
		}
		
		private Rectangle GetRectangle(Boolean centered) {
			
			Rectangle rect = DeflateRectangle( ClientRectangle, Padding );
			if( _image == null ) return rect;
			
			if(centered) {
				Rectangle retVal = new Rectangle();
				retVal.X = ( rect.Width  - _image.Width  ) / 2;
				retVal.Y = ( rect.Height - _image.Height ) / 2;
				retVal.Size = _image.Size;
				return retVal;
			}
			else return new Rectangle( 0, 0, _image.Width, _image.Height );
			
			
		}
		
		public event EventHandler ZoomChanged;
		
		
		private BorderStyle _borderStyle;
		
		public BorderStyle BorderStyle {
			get { return _borderStyle; }
			set {
				Int32 val = (int)value;
				if(val < 0 || val > 2) throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
				
				if(_borderStyle != value) {
					_borderStyle = value;
					base.RecreateHandle();
					
				}
			}
		}
		
/* #region TODO
	
		// Animation-related
		
		public Int32 Framerate {
			get;
			set; // -1 means use the framerate in the file, 0 means still frame
		}
		
		public CreateParams CreateParams { get; }
		public ImeMode DefaultImageMode { get; }
		public Size DefaultSize { get; }
		public Image ErrorImage { get; set; }
		public String ImageLocation { get; set; }
		
		public Image InitialImage { get; set; }
		
		public Boolean WaitOnLoad { get; set; }
		
		// Events
		
	#region EditorBrowseable(Never)
		
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public override RightToLeft RightToLeft {
			get { return base.RightToLeft; }
			set { base.RightToLeft = value; }
		}
		
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new Int32 TabIndex {
			get { return base.TabIndex; }
			set { TabIndex = value; }
		}
		
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public override Boolean AllowDrop {
			get { return base.AllowDrop; }
			set { AllowDrop = value; }
		}

		public override Boolean CausesValidation { get; set; }
		
		public override Font Font { get; set; }
		public override Font ForeColor { get; set; }
		public override ImeMode ImeMode { get; set; }
		
		public override Boolean TabStop { get; set; }
		
		[Bindable(false), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public override String Text { get; set; }
		
	#endregion
		
#endregion
		*/
		
	}
	
}
