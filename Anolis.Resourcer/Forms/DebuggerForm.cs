using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Anolis.Resourcer {
	
	/// <summary>Displays a message to the user until a debugger is attached to the process (or the form is closed).</summary>
	public partial class DebuggerForm : Form {
		
		public DebuggerForm() {
			InitializeComponent();
		}
		
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			
			this.__timer.Tick += new EventHandler(__timer_Tick);
			
			int pid = Process.GetCurrentProcess().Id;
			
			__message.Text = String.Format( Resources.DebuggerForm_Message, pid );
		}
		
		private void __timer_Tick(object sender, EventArgs e) {
			
			if( Debugger.IsAttached ) {
				
				this.Close();
			}
			
		}
		
	}
}
