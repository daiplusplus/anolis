using System;
using System.IO;
using System.Windows.Forms;

using G = System.Collections.Generic;
using S = Anolis.Resourcer.Settings.Settings;
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

//				Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
				
				///////////////////////////////
				// Load Extensibility
				if( S.Default.LoadAssemblies != null ) {
					
					String[] assemblyFilenames = new String[ S.Default.LoadAssemblies.Count ];
					for(int i=0;i<assemblyFilenames.Length;i++) {
						assemblyFilenames[i] = S.Default.LoadAssemblies[i];
					}
					
					if( assemblyFilenames.Length > 0 )
						Anolis.Core.Utility.Miscellaneous.SetAssemblyFilenames( assemblyFilenames );
					
				}
				
				CommandLineParser cmd = new CommandLineParser(args);
				
				CommandLineFlag batchFlag = cmd.GetFlag("batch");
				if( batchFlag != null ) {
					
					return StatelessResourceEditor.ProcessBatch( batchFlag.Argument );
				}
				
				if( cmd.Args.Count > 1 ) {
					
					Int32 retval = StatelessResourceEditor.PerformOneOff(cmd);
					
					switch(retval) {
						case 2: // file not found
							
							MessageBox.Show("File not found error: " + cmd.ToString(), "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
							
							break;
						case 1: // syntax error
							
							String message = 
@"Anolis.Resourcer.exe -batch:""C:\batchfile.txt""
Anolis.Resourcer.exe -op:add -src:""C:\dest.exe"" -type:ICONGROUP -name:NAME  -lang:1033  -file:""C:\foo\icon.ico""
Anolis.Resourcer.exe -op:upd -src:""C:\dest.exe"" -type:ICONGROUP -name:NAME [-lang:1033] -file:""C:\foo\icon.ico""
Anolis.Resourcer.exe -op:ext -src:""C:\dest.exe"" -type:ICONGROUP -name:NAME  -lang:1033  -file:""C:\foo\icon.ico""
Anolis.Resourcer.exe -op:del -src:""C:\dest.exe"" -type:ICONGROUP -name:NAME [-lang:1033]";
							
							MessageBox.Show("Syntax error: " + cmd.ToString() + "\r\n\r\nExpected:\r\n" + message, "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
							
							break;
						case 0: // OK
							break;
					}
					
					return retval;
				}
				
				MainForm main = new MainForm();
				
				if( cmd.Strings.Count == 1 )
					main.OpenSourceOnLoad = cmd.Strings[0].String;
				
				Application.Run( main );
				
				Settings.Settings.Default.Save();
				
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
		
//		private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {			
//		}
		
		public static String IfYouAreReadingThisThenYouHaveNoLife() {
			return "no, really you are wasting your time because the complete source code is on http://www.codeplex.com/anolis";
		}
		
	}
}