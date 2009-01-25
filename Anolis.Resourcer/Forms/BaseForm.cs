using System;
using System.Drawing;
using System.Windows.Forms;

namespace Anolis.Resourcer {
	
	public class BaseForm : Form {
		
		public BaseForm() {
			
			this.Font = SystemFonts.IconTitleFont;
		}
		
		private void BaseForm_Load(object sender, EventArgs e) {
			
			
			
		}
		
		protected void UpdateFonts() {
			
			this.Font = SystemFonts.IconTitleFont;
			
			foreach(Control c in this.Controls) {
				
				if(c.Font.Bold) {
					
					c.Font = new Font( this.Font, FontStyle.Bold );
					
				}
				
			}
			
		}
		
	}
}
