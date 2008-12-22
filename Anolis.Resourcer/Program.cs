using System;
using System.IO;
using System.Windows.Forms;

namespace Anolis.Resourcer {
	
	public static class Program {
		
		private static ResourcerContext _context;
		
		[STAThread]
		public static void Main() {
			
			try {
				
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				
				_context = new ResourcerContext();
				
				MainForm main = new MainForm();
				main.Context = _context;
				
				Application.Run( main );
				
				_context.Save();
			
			} catch (Exception ex) {

#if DEBUG				
				String desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
				
				String path = Path.Combine(desktop, "Report.txt");
				
				using(StreamWriter wtr = new StreamWriter(path, true)) {
					
					wtr.WriteLine("".PadLeft(80, '-'));
					wtr.WriteLine( DateTime.Now.ToString("s") );
					wtr.WriteLine();
					
					Exception e = ex;
					while(e != null) {
						wtr.WriteLine(e.Message);
						wtr.WriteLine(e.StackTrace);
						wtr.WriteLine();
						
						e = e.InnerException;
					}
					
				}
#endif
				
				throw ex;
			}
			
		}
		
		public static String IfYouAreReadingThisThenYouHaveNoLife() {
			return "no, really you are wasting your time because the complete source code is on http://www.codeplex.com/anolis";
		}
		
	}
}