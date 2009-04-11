using System;
using System.Drawing;
using System.Windows.Forms;

using Th = System.Threading;

namespace Anolis.Resourcer {
	
	public class BaseForm : Form {
		
		private Th.Timer _t;
		private Boolean _fadeIn = true;
		
		public BaseForm() {
			
			if( Settings.Settings.Default.Gimmicks ) {
				
				FadeInit();
				
				this.Load += new EventHandler(BaseForm_Load);
				
			}
		}
		
#region Fade
		
		private void BaseForm_Load(object sender, EventArgs e) {
			
			if(!DesignMode) UpdateFonts();
			
			FadeInBegin();
		}
		
		private void FadeInit() {
			
			Th.TimerCallback timerCallback = new Th.TimerCallback(
				delegate(Object o) {
					this.Invoke(new MethodInvoker( FadeIncr ) );
				}
			);
			
			_t = new Th.Timer( timerCallback, null, Th.Timeout.Infinite, 10);
			this.Opacity = 0;
		}
		
		private void FadeIncr() {
			
			this.Opacity = _fadeIn ? this.Opacity + 0.033 : this.Opacity - 0.033;
			
			if( ( _fadeIn && this.Opacity >= 1.0 ) || ( !_fadeIn && this.Opacity <= 0 ) )
				FadeStop();
			
		}
		
		private void FadeInBegin() {
			
			_fadeIn = true;
			
			_t.Change(0, 10);
		}
		
		private void FadeOutBegin() {
			
			_fadeIn = false;
			
			_t.Change(0, 10);
		}
		
		private void FadeStop() {
			_t.Change(Th.Timeout.Infinite, Th.Timeout.Infinite);
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
