using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Anolis.Resourcer.Controls {
	
	public class ZoomPictureBoxDesigner : ControlDesigner {
		
		public ZoomPictureBoxDesigner() {
			base.AutoResizeHandles = true;
		}
		
		protected override void OnPaintAdornments(PaintEventArgs pe) {
			
			ZoomPictureBox pb = (ZoomPictureBox)base.Component;
			
			if(pb.BorderStyle == BorderStyle.None) {
				
				Graphics g = pe.Graphics;
				
				Rectangle clientRectangle = pb.ClientRectangle;
				Color color = pb.BackColor.GetBrightness() < 0.5 ? ControlPaint.Light(pb.BackColor) : ControlPaint.Dark(pb.BackColor);
				
				using(Pen pen = new Pen(color) { DashStyle = DashStyle.Dash }) {
				
					clientRectangle.Width--;
					clientRectangle.Height--;
					
					g.DrawRectangle(pen, clientRectangle);
				}
				
			}
			
			base.OnPaintAdornments(pe);
		}
		
	}
}
