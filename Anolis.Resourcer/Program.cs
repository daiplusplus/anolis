using System;
using System.IO;
using System.Windows.Forms;
using S = Anolis.Resourcer.Settings.Settings;
using Anolis.Resourcer.CommandLine;

using System.Configuration;

namespace Anolis.Resourcer {
	
	public static class Program {
		
		[STAThread]
		public static Int32 Main(String[] args) {
			
#if !DEBUG
			try {
#endif
				
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				
				try {
					
					///////////////////////////////
					// Upgrade Settings
					S.Default.Upgrade();
					
					///////////////////////////////
					// Load Extensibility
					if( S.Default.LoadAssemblies != null ) {
						
						String[] assemblyFilenames = new String[ S.Default.LoadAssemblies.Count ];
						for(int i=0;i<assemblyFilenames.Length;i++) {
							assemblyFilenames[i] = S.Default.LoadAssemblies[i];
						}
						
						if( assemblyFilenames.Length > 0 )
							Anolis.Core.Utility.Miscellaneous.SetAssemblyFileNames( assemblyFilenames );
						
					}
				
				} catch(ConfigurationException) {
					
					// delete all the config files
					
					Configuration exeConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
					if( exeConfig.HasFile ) File.Delete( exeConfig.FilePath );
					
					Configuration localConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
					if( localConfig.HasFile ) File.Delete( localConfig.FilePath );
					
					Configuration roamingConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
					if( roamingConfig.HasFile ) File.Delete( roamingConfig.FilePath );
					
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
				
				S.Default.Save();
				
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
						wtr.WriteLine(e.GetType().Name + " : " + e.Message);
						wtr.WriteLine(e.StackTrace);
						wtr.WriteLine();
						
						e = e.InnerException;
					}
					
				}
				
				String message = "There was a problem whilst attempting to run Resourcer:";
				Exception e2 = ex;
				while(e2 != null) {
					
					message += "\r\n" + e2.Message + " : " + e2.GetType().Name;
					e2 = e2.InnerException;
				}
				
				message += "\r\n\r\nA log has been saved to \"" + path + "\"";
				
				MessageBox.Show( message, "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1 );
				
				return 1;
			}
#endif
			
		}
		
	}
}