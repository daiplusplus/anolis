using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Anolis.Tools.PEInfo {
	
	public static class Program {
		
		[STAThread]
		public static Int32 Main(String[] args) {
			
			Application.SetCompatibleTextRenderingDefault(false);
			Application.EnableVisualStyles();
			
			Application.Run( new MainForm() );
			
			return 0;
		}//main
	}
}
