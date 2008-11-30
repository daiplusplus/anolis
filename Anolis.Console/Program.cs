using System;
using System.Data;
using System.Collections.Generic;
using System.Xml;
using System.Text;

using Anolis.Core.Xml;

namespace Anolis.Console {
	
	/// <summary>Executes an Anolis Package</summary>
	public class Program {
		
		public static Int32 Main(String[] args) {
			
			if(!EnsureArgs(args)) {
				return 1;
			}
			
			///////////////
			// Test with the xml
			
			String packageExamplePath = @"D:\Users\David\My Documents\Visual Studio Projects\Anolis\Anolis.Core\ExamplePackage.xml";
//			String packageSchemaPath  = @"D:\Users\David\My Documents\Visual Studio Projects\Anolis\Anolis.Core\PackageSchema.xsd";
			
			DataSet ds = new DataSet();
//			ds.ReadXmlSchema( packageSchemaPath );
			ds.ReadXml      ( packageExamplePath );
			
			System.Console.WriteLine( ds.Tables["extra"].Rows.Count );
			
			Package st = new Package();
			st.ReadXml( packageExamplePath );
			
			System.Console.WriteLine( st.Tables["extra"].Rows.Count );
			
			return 0;
		}
		
		private static Boolean EnsureArgs(String[] args) {
			
			return true;
			
		}
		
	}
}
