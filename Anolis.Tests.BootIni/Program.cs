using System;
using System.Collections.Generic;
using System.Text;

using Anolis.Core.Utility;

namespace Anolis.Tests.Boot {
	
	public class Program {
		
		public static void Main(String[] args) {
			
			BootIni boot = BootIni.FromDefaultBootIni();
			boot.OperatingSystems[0].Switches.AddSwitch("/noguiboot");
			boot.OperatingSystems[0].Switches.AddSwitch("/bootlogo");
			boot.OperatingSystems[0].Switches.AddSwitch("/foobar");
			
			boot.Save();
			
		}
	}
}
