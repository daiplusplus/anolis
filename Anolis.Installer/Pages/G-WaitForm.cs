using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Anolis.Installer.Pages {
	
	public partial class WaitForm : BaseForm {
		
		public WaitForm() {
			
			InitializeComponent();
			
			this.__timer.Tick += new EventHandler(__timer_Tick);
			
			Localize();
		}
		
		protected override string LocalizePrefix {
			get { return "G"; }
		}
		
		private void __timer_Tick(object sender, EventArgs e) {
			
			// if, after 10 seconds this process hasn't terminated, do it the old fashioned way
			Process proc = Process.Start("shutdown.exe", "/S /T 0");
			
		}
		
	}
}
