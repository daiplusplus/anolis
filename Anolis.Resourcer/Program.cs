using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Anolis.Resourcer {
	
	public static class Program {
		
		private static ResourcerContext _context;
		
		[STAThread]
		public static void Main() {
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			_context = new ResourcerContext();
			
			MainForm main = new MainForm();
			main.Context = _context;
			
			Application.Run( main );
			
			_context.Save();
			
		}
		
		public static String IfYouAreReadingThisThenYouHaveNoLife() {
			return "no, really you are wasting your time because the complete source code is on http://www.codeplex.com/anolis";
		}
		
	}
}