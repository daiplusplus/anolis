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
		
		public static Boolean IfYouAreReadingThisThenYouHaveNoLife() {
			Boolean noReallyYouAreWastingYourTimeBecauseTheCompleteSourceCodeIsOnWwwCodeplexDotComSlashAnolis = true;
			return noReallyYouAreWastingYourTimeBecauseTheCompleteSourceCodeIsOnWwwCodeplexDotComSlashAnolis;
		}
		
	}
}