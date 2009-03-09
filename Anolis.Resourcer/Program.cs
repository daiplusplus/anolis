using System;
using System.IO;
using System.Windows.Forms;

using Anolis.Resourcer.CommandLine;

namespace Anolis.Resourcer {
	
	public static class Program {
		
		[STAThread]
		public static Int32 Main(String[] args) {
			
#if !DEBUG
			try {
#endif
				
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				
				Console.WriteLine("testing, lol");
				
				CommandLineParser cmd = new CommandLineParser(args);
				
				CommandLineFlag scriptFlag = cmd.GetFlag("script");
				if( scriptFlag != null ) {
					
					return StatelessResourceEditor.ProcessBatch( scriptFlag.Argument );
				}
				
				if( cmd.Args.Count > 1 ) {
					
					return StatelessResourceEditor.PerformOneOff(cmd);
				}
				
				MainForm main = new MainForm();
				
				if( cmd.Strings.Count == 1 )
					main.OpenSourceOnLoad = cmd.Strings[0].String;
				
				Application.Run( main );
				
				return 0;
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