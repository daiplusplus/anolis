using System;
using System.Collections.Generic;
using System.Text;
using Anolis.Core.Utility;
using System.IO;

namespace Anolis.Tools.CompositedImages {
	
	public class OneOffGen {
		
		public static void OneOffMain(String[] args) {
			
			DirectoryInfo root = new DirectoryInfo(@"D:\Users\David\My Documents\Visual Studio Projects\Anolis\_resources\xpize");
			
			String expr = @"comp:164,628;MCE\GenLong.png,0,0,164,817;Generator\Windows\system32\newdev.dll\101.png,19,144;MCE\GenStripe.png,160,0";
			
			CompositedImage ci = new CompositedImage(expr, root);
			ci.Save( @"D:\Users\David\Desktop\hdwiz\Test.bmp", System.Drawing.Imaging.ImageFormat.Bmp );
			
		}
		
	}
}
