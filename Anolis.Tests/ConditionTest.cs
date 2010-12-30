using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using Anolis.Packages;

using Env  = Anolis.Core.Utility.Environment;
using Cult = System.Globalization.CultureInfo;

namespace Anolis.Tools.ConditionTest {
	
	public class Program {
		
		public static void Main(String[] args) {
			
			using(StreamWriter wtr = new StreamWriter("ConditionText.txt")) {
				
				Console.SetOut( wtr );
				
				Console.WriteLine("OSVersion  : " + Environment.OSVersion.ToString() );
				Console.WriteLine("ServicePack: " + Environment.OSVersion.ServicePack );
				
				Dictionary<String,Double> symbols = BuildSymbols();
				
				TestExpression("( osversion == 5.1 && servicepack == 3 && architecture == 32 )", symbols);
				
				TestExpression("( osversion == 5.2 && servicepack == 2 && architecture == 32 )", symbols);
				
				TestExpression("( osversion == 5.2 && servicepack == 2 && architecture == 64 )", symbols);
				
			}
		}
		
		private static Dictionary<String,Double> BuildSymbols() {
			
			return new Dictionary<String,Double>() {
				{"osversion"   , Env.OSVersion.Version.Major + ( (Double)Env.OSVersion.Version.Minor ) / 10 },
				{"servicepack" , Env.ServicePack },
				{"architecture", Env.IsX64 ? 64 : 32 },
				{"installlang" , Cult.InstalledUICulture.LCID }
			};
			
		}
		
		private static void TestExpression(String expression, Dictionary<String,Double> symbols) {
			
			Console.WriteLine("".PadLeft( Console.WindowWidth, '-'));
			
			Console.WriteLine("Symbols");
			
			foreach(String key in symbols.Keys) {
				
				Console.WriteLine( '\t' + key + '\t' +  symbols[key].ToString() + " (" + symbols[key].ToString(Cult.InvariantCulture) + ")" );
			}
			
			Console.WriteLine();
			
			Console.WriteLine("Expression");
			
			Expression expr = new Expression( expression );
			
			Console.WriteLine("\tToString()\t" + expr.ToString() );
			
			Console.WriteLine("\tTokenize\t" + ArrayToString( expr.Tokenize() ) );
			
			Console.WriteLine("Evaluate");
			
			Double result;
			try {
				
				result = expr.Evaluate( symbols );
				
			} catch(ExpressionException eex) {
				
				Console.WriteLine("ExpressionException: " + eex.Message);
				return;
				
			} catch(Exception ex) {
				
				Console.WriteLine("Exception: " + ex.Message);
				return;
			}
			
			Console.WriteLine("Result: ");
			Console.WriteLine('\t' + result.ToString() );
			
		}
		
		private static String ArrayToString(String[] arr) {
			
			StringBuilder sb = new StringBuilder();
			
			for(int i=0;i<arr.Length;i++) {
				
				sb.Append('\'');
				sb.Append( arr[i] );
				sb.Append('\'');
				if( i != arr.Length - 1 ) sb.Append(", ");
			}
			
			return sb.ToString();
		}
		
	}
}
