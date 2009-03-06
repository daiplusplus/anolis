using System;
using System.IO;

using Anolis.Core;
using Anolis.Core.Data;

namespace Anolis.Resourcer.CommandLine {
	
	public static class StatelessResourceEditor {
	
		public static Int32 ProcessScript(String scriptfile) {
			
			
			return 0;
		}
		
		public static Int32 PerformOneOff(CommandLineParser args) {
			
			// one-off command:
			// Anolis.Resourcer.exe -op:del -src:"C:\dest.exe" -type:ICONGROUP -name:NAME -lang:1033
			// Anolis.Resourcer.exe -op:upd -src:"C:\dest.exe" -type:ICONGROUP -name:NAME [-lang:1033] -file:"C:\foo\icon.ico"
			// Anolis.Resourcer.exe -op:ext -src:"C:\dest.exe" -type:ICONGROUP -name:NAME [-lang:1033] -file:"C:\foo\icon.ico"
			
			CommandLineFlag
				operation = args.GetFlag("op"),
				resSource = args.GetFlag("src"),
				dataType  = args.GetFlag("type"),
				dataName  = args.GetFlag("name"),
				dataLang  = args.GetFlag("lang"),
				dataFile  = args.GetFlag("file");
			
			if(operation == null || resSource == null || dataType == null || dataName == null)            return 1;
			if(operation.Argument != "del" && operation.Argument != "upd" && operation.Argument != "ext") return 1;
			
			if( operation.Argument.StartsWith("del") && dataLang == null) return 1;
			if(!operation.Argument.StartsWith("del") && dataFile == null) return 1;
			
			if( !File.Exists( resSource.Argument ) )                               return 2;
			if( operation.Argument == "upd" && !File.Exists( dataFile.Argument ) ) return 2;
			
			switch(operation.Argument) {
				case "del":
					
					ResourceSource source = ResourceSource.Open( resSource.Argument, false, true );
					
					
					break;
				case "upd":
					break;
				case "ext":
					break;
			}
			
			return 0;
		}
	}
}
