using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Anolis.Tools.TweakUI.ThumbsDB;
using System.IO;

namespace Anolis.Tools.TweakUI {
	
	public static class Program {
		
		[STAThread]
		public static void Main(String[] args) {
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			if( args.Length > 0 && File.Exists( args[0] ) ) {
				
				ThumbnailForm form = new ThumbnailForm();
				form.InitialThumbsDb = args[0];
				
				Application.Run( form );
				
			} else {
				
				MainForm form = new MainForm();
				
				Application.Run( form );
			}
			
			
		}
	}
}
