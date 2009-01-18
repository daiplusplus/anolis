using System;
using System.Drawing;
using System.Windows.Forms;

namespace Anolis.Resourcer {
	
	public class BaseForm : Form {
		
		public BaseForm() {
			
			this.Load += new EventHandler(BaseForm_Load);
		}
		
		private void BaseForm_Load(object sender, EventArgs e) {
			
			this.Font = SystemFonts.IconTitleFont;
			
		}
		
	}
}
