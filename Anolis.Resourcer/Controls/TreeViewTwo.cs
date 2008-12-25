using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Anolis.Resourcer.Controls {
	
	public class TreeViewTwo : TreeView {
		
		public TreeViewTwo() {
			base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
		}
		
		protected override void OnPaintBackground(PaintEventArgs pevent) {
			
			Color start = SystemColors.Window;
			Color end   = SystemColors.Control;
			
			Rectangle rect = new Rectangle(0, 0, Width, Height);
			
			Brush brush = new LinearGradientBrush(rect, start, end, 0, false);
			
			pevent.Graphics.FillRectangle(brush, rect);
			
			base.OnPaintBackground(pevent);
			
		}
		
	}
}
