using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Anolis.Installer.Pages {
	
	public partial class WaitForm : Form {
		
		public WaitForm() {
			
			InitializeComponent();
			
			this.Text              = InstallerResources.GetString("G_Title");
			this.__restarting.Text = InstallerResources.GetString("G_WaitMessage");

			this.__timer.Tick += new EventHandler(__timer_Tick);
		}
		
		public String MessageText {
			get { return __restarting.Text; }
			set { __restarting.Text = value; }
		}
		
		private void __timer_Tick(object sender, EventArgs e) {
			
			// if, after 10 seconds this process hasn't terminated, do it the old fashioned way
			Process proc = Process.Start("shutdown.exe", "/S /T 0");
			
		}
		
	}
}
