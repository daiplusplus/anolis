using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Anolis.IconEditor {
	
	public static class Program {
		
		[STAThread]
		public static Int32 Main(String[] args) {
			
			if(args.Length > 0) {
				
				// automated command-line icon editing
				
				// not resource-based extraction/adding though, that's resourcer's job
				
			} else {
				
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
				
			}
			
			return 0;
		}
	}
}
