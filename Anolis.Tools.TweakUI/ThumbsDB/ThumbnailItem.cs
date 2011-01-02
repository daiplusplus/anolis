using System;
using System.Drawing;
using System.Windows.Forms;

namespace Anolis.Tools.TweakUI.ThumbsDB {
	
	public partial class ThumbnailItem : UserControl {
		
		public ThumbnailItem() {
			
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			
			InitializeComponent();
			
			__pic    .Click += new EventHandler(Control_Click);
			__caption.Click += new EventHandler(Control_Click);
		}
		
		private void Control_Click(object sender, EventArgs e) {
			OnClick(e);
		}
		
		public Image Image {
			get { return __pic.Image; }
			set {
				
				if( value.Width <= __pic.Width || value.Height <= __pic.Height ) {
					
					__pic.SizeMode = PictureBoxSizeMode.CenterImage;
				} else {
					
					__pic.SizeMode = PictureBoxSizeMode.Zoom;
				}
				
				__pic.Image = value;
				
			}
		}
		
		public String Caption {
			get { return __caption.Text; }
			set { __caption.Text = value; }
		}
		
		private Boolean _isSelected;
		
		public Boolean IsSelected {
			get { return _isSelected; }
			set { 
				
				if( value ) {
					
					BackColor           = SystemColors.Highlight;
					__caption.ForeColor = SystemColors.HighlightText;
				} else {
					
					BackColor           = Color.Transparent;
					__caption.ForeColor = SystemColors.ControlText;
				}
				
				_isSelected = value;
			}
		}
		
	}
}
