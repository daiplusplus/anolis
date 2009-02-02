using System;
using System.Drawing;
using System.Windows.Forms;

namespace Anolis.Resourcer.Controls {
	
	// is this class necessary since BaseForm does this?
	
	public class BaseUserControl : UserControl {
		
		public BaseUserControl() {
			this.Load += new EventHandler(BaseUserControl_Load);
		}
		
		private void BaseUserControl_Load(object sender, EventArgs e) {
			
			UpdateFonts();
			
		}
		
		protected void UpdateFonts() {
			
			this.Font = SystemFonts.IconTitleFont;
			
			foreach(Control c in this.Controls) {
				
				if(c != null && c.Font.Bold)
					c.Font = new Font( this.Font, FontStyle.Bold );
				
			}
			
		}
		
	}
}
