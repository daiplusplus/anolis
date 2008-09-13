using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Anolis.Resourcer {
	
	public static class Program {
		
		[STAThread]
		public static void Main() {
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
		public static Boolean IfYouAreReadingThisThenYouHaveNoLife() {
			Boolean noReallyYouAreWastingYourTimeBecauseTheCompleteSourceCodeIsOnWwwCodeplexDotComSlashAnolis = true;
			return noReallyYouAreWastingYourTimeBecauseTheCompleteSourceCodeIsOnWwwCodeplexDotComSlashAnolis;
		}
		
	}
}