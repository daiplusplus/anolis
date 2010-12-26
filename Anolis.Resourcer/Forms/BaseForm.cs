using System;
using System.Drawing;
using System.Windows.Forms;

using Th = System.Threading;
using Anolis.Resourcer.Settings;

namespace Anolis.Resourcer {
	
	public class BaseForm : Form {
		
		private Timer   _t;
		private Boolean _fadeIn = true;
		
		public BaseForm() {
			
			this.StartPosition = FormStartPosition.CenterParent;
			
			if( ARSettings.Default.Gimmicks ) {
				
				FadeInit();
				
				this.Load += new EventHandler(BaseForm_Load);
				this.FormClosing += new FormClosingEventHandler(BaseForm_FormClosing);
				
			}
		}
		
#region Drop Shadow
		
		private const Int32 CS_DROPSHADOW = 0x00020000;
		
		protected override CreateParams CreateParams {
			get {
				CreateParams paras = base.CreateParams;
				
				if( ARSettings.Default.Gimmicks )
					paras.ClassStyle |= CS_DROPSHADOW;
				
				return paras;
			}
		}
		
#endregion
		
#region Fade
		
		private Boolean _isClosing;
		
		private void BaseForm_Load(object sender, EventArgs e) {
			
			if(!DesignMode) UpdateFonts();
			
			FadeIn();
		}
		
		private void BaseForm_FormClosing(object sender, FormClosingEventArgs e) {
			
			_isClosing = !e.Cancel;
			
			if( !_t.Enabled ) {
				
				FadeOut();
				e.Cancel = true;
			}
			
		}
		
		private void FadeInit() {
			
			_t = new Timer();
			_t.Interval = 10;
			_t.Tick += new EventHandler( delegate(Object sender, EventArgs e) {
				
				this.Opacity = _fadeIn ? this.Opacity + 0.033 : this.Opacity - 0.033;
					
				if( ( _fadeIn && this.Opacity >= 1.0 ) || ( !_fadeIn && this.Opacity <= 0 ) ) {
					
					if( _isClosing ) {
						this.Close();
					}
					
					_t.Stop();
					
				}
				
			} );
			
			if( _fadeIn ) this.Opacity = 0;
		}
		
		private void FadeIn() {
			
			_fadeIn = true;
			
			_t.Start();
		}
		
		private void FadeOut() {
			
			_fadeIn = false;
			
			_t.Start();
		}
		
#endregion
		
		protected void UpdateFonts() {
			
			this.Font = SystemFonts.IconTitleFont;
			
			foreach(Control c in this.Controls) {
				
				if(c != null && c.Font.Bold)
					c.Font = new Font( this.Font, FontStyle.Bold );
				
			}
			
		}
		
	}
}
