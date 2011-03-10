using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Anolis.Tools.PEInfo {
	
	public static class Program {
		
		[STAThread]
		public static Int32 Main(String[] args) {
			
//			Application.SetCompatibleTextRenderingDefault(false);
//			Application.EnableVisualStyles();
//			
//			Application.Run( new MainForm() );
			
//			PEResourceSource source = new PEResourceSource( args[0] );
//			source.GetResources();
			
			String thunk16 = @"D:\Users\David\My Documents\Visual Studio Projects\Solutions\Anolis Private\_notes\ResourceSource Formats\NE\EXE Files\twunk_16.exe";
			
			NEFile ne = new NEFile( thunk16 );
			
			return 0;
		}//main
	}
}
