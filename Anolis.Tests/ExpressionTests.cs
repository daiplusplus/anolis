using System;
using System.Text;
using System.Collections.Generic;

using Anolis.Packages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Anolis.Test.Expressions {
	
	[TestClass]
	public class ExpressionTests {
		
		public ExpressionTests() {
			
		}
		
		public TestContext TestContext { get; set; }
		
		// since behaviour when an invalid expression is entered is undefined, there's no point spending effort to test for that scenario :)
		
		[TestMethod]
		public void TestComparison() {
			
			RunTest("1 == 1", 1);
			RunTest("1 != 1", 0);
			
			RunTest("1 == 0", 0);
			RunTest("1 != 0", 1);
			
			RunTest("1 == 1 == 1", 1);
			RunTest("1 == 2 == 0", 1);
			
		}
		
		[TestMethod]
		public void TestParenthesisArithmetic() {
			
			RunTest("( ( 1 / 2 ) * 2 )", 1 );
			RunTest(" (3 + 4 - 2) - 6", -1);
			
		}
		
		[TestMethod]
		public void TestParenthesisComparison() {
			
			RunTest("( ( 1 == 2 ) * 2 )", 0 );
			RunTest(" (3 + 4 - 2) - 6", -1);
			
		}
		
		[TestMethod]
		public void TestParenthesisBoolean() {
			
			RunTest("   2 == 2  && 2 ", 0 );
			RunTest("  (2 == 2) && 2 ", 0 );
			RunTest(" ((2 == 2) && 2 )", 0 );
			
			RunTest("  (2 == 2) && ( 2 != 3 ) ", 1 );
			
			RunTest("  1 == 1 && 1 == 1  ", 1 );
			RunTest("  2 == 2 && (2 == 2)  ", 1 );
			RunTest("  (2 == 2) && 2 == 2  ", 1 );
			RunTest("  (2 == 2) && (2 == 2)  ", 1 );
			RunTest("  2 == 2 && 2 == 2 ", 1 );
			
			RunTest("  2 == 2 && 2 != 2  ", 0 );
			RunTest("  2 == 2 && 2 != 2  ", 0 );
			RunTest("  2 == 2 && 3 == 3  ", 1 );
			
			RunTest(" ( 1 == 1 && 1 != 2 ) ", 1 );
			
			RunTest(" ( 1 == 2 || 1 == 1 ) ", 1 );
			RunTest(" ( 1 == 2 || 1 == 2 ) ", 0 );
			
			RunTest( " 1 && 1 && 1 ", 1 );
			
			RunTest("   1 == 1  && 1 == 1 ", 1 );
			
		}
		
		[TestMethod]
		public void TestSampleConditions() {
			
			Dictionary<String,Double> xp86SP3 = new Dictionary<string,double>() {
				{ "osversion", 5.1 },
				{ "servicepack", 3 },
				{ "architecture", 32 }
			};
			
			Dictionary<String,Double> xp64SP2 = new Dictionary<string,double>() {
				{ "osversion", 5.2 },
				{ "servicepack", 2 },
				{ "architecture", 64 }
			};
			
			Dictionary<String,Double> vi86SP2 = new Dictionary<string,double>() {
				{ "osversion", 6.0 },
				{ "servicepack", 2 },
				{ "architecture", 32 }
			};
			
			RunTest("1 == 1 && 3 == 3", 1 );
			RunTest("2 == 1 && 2 == 3", 0 );
			
			RunTest("1 == 1 && 3 == 3", 1, xp86SP3 );
			RunTest("2 == 1 && 2 == 3", 0, xp64SP2 );
			
			RunTest("5.1 == 5.1 && 3 == 3", 1, xp86SP3 );
			RunTest("5.2 == 5.1 && 2 == 3", 0, xp64SP2 );
			
			RunTest("osversion == 5.1 && servicepack == 3", 1, xp86SP3 );
			RunTest("osversion == 5.1 && servicepack == 3", 0, xp64SP2 );
			
			RunTest(" (osversion == 5.1 && servicepack == 3)", 1, xp86SP3 );
			RunTest(" (osversion == 5.1 && servicepack == 3)", 0, xp64SP2 );
			
			RunTest(" (osversion == 5.1 && servicepack == 3) && architecture == 32 ", 1, xp86SP3 );
			RunTest(" (osversion == 5.1 && servicepack == 3) && architecture == 32 ", 0, xp64SP2 );
			
			RunTest(" osversion == 5.1 && servicepack == 3 && architecture == 32 ", 1, xp86SP3 );
			RunTest(" osversion == 5.1 && servicepack == 3 && architecture == 32 ", 0, xp64SP2 );
			
			RunTest("( osversion == 5.1 && servicepack == 3 && architecture == 32 )", 1, xp86SP3 );
			RunTest("( osversion == 5.1 && servicepack == 3 && architecture == 32 )", 0, xp64SP2 );
			
			RunTest("( osversion == 5.1 && servicepack == 3 && architecture == 32 ) || ( osversion == 5.2 && servicepack == 2 && architecture == 64 )", 1, xp86SP3 );
			RunTest("( osversion == 5.1 && servicepack == 3 && architecture == 32 ) || ( osversion == 5.2 && servicepack == 2 && architecture == 64 )", 1, xp64SP2 );
			
			RunTest("( osversion == 5.1 && servicepack == 3 ) || ( osversion == 5.2 && servicepack == 2 )", 1, xp86SP3 );
			RunTest("( osversion == 5.1 && servicepack == 3 ) || ( osversion == 5.2 && servicepack == 2 )", 1, xp64SP2 );
		}
		
		private void RunTest(String expression, Double expectedResult) {
			
			Expression expr = new Expression(expression);
			Double result = expr.Evaluate(null);
			
			Assert.AreEqual( expectedResult, result );
			
		}
		
		private void RunTest(String expression, Double expectedResult, params Object[] symbols) {
			
			Dictionary<String,Double> syms = new Dictionary<string,double>();
			for(int i=0;i<symbols.Length;i+=2) {
				
				String s = (String)symbols[i];
				Double v = (Double)symbols[i+1];
				
				syms.Add( s, v );
			}
			
			Expression expr = new Expression(expression);
			Double result = expr.Evaluate(syms);
			
			Assert.AreEqual( expectedResult, result );
			
		}
		
		private void RunTest(String expression, Double expectedResult, Dictionary<String,Double> symbols) {
			
			Expression expr = new Expression(expression);
			Double result = expr.Evaluate(symbols);
			
			Assert.AreEqual( expectedResult, result );
			
		}
		
	}
}
