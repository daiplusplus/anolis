using System;
using System.Windows.Forms;

namespace Anolis.Packager {
	
	public static class Program {
		
		[STAThread]
		public static void Main() {
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			MainForm form = new MainForm();
			
			Application.Run( form );
			
		}
	}
}
