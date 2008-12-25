using System;
using System.IO;
using System.Windows.Forms;

namespace Anolis.Resourcer {
	
	public static class Program {
		
		private static ResourcerContext _context;
		
		internal static ResourcerContext Context {
			get { return _context; }
		}
		
		[STAThread]
		public static void Main() {
			
#if !DEBUG
			try {
#endif
				
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				
				_context = new ResourcerContext();
				
				MainForm main = new MainForm();
				
				Application.Run( main );
				
				_context.Save();

#if !DEBUG				
			} catch (Exception ex) {
				
			
				String dest = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				dest = Path.Combine(dest, "Anolis");
				
				String path = Path.Combine(dest, "Exceptions.log");
				
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
				throw ex;			
				
			}
#endif
			
		}
		
		public static String IfYouAreReadingThisThenYouHaveNoLife() {
			return "no, really you are wasting your time because the complete source code is on http://www.codeplex.com/anolis";
		}
		
	}
}