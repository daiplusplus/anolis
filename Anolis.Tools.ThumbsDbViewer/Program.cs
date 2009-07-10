using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Anolis.Tools.ThumbsDbViewer {
	
	public static class Program {
		
		[STAThread]
		public static void Main(String[] args) {
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			MainForm form = new MainForm();
			
			if( args.Length > 0 ) {
				
				form.InitialThumbsDb = args[0];
			}
			
			Application.Run( form );
		}
	}
}
