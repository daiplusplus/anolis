using System;
using System.Collections.Generic;
using System.Text;

using S = System.Runtime.Serialization;

namespace Anolis.Core.Packages {
	
	using P = Anolis.Core.Packages.Precedence;
	
	// TODO:
		// * Add support for boolean operators &&, ||, ^^, !
		// * Add support for functions, e.g. sin(), a function can be defined in the symbol table: with that functions' expression as the dictionary value
			// this is a bit hard. I'll leave it for now
		// I think booleans can be supported by having false == 0 and true == non-zero
	
	/// <summary>A C# implementation of Tom Niemann's Operator Precedence Parsing system ( http://epaperpress.com/oper/index.html ).</summary>
	public class Expression {
		
		// The precedence table; beautiful, isn't it?
/*
		S = Shift. The input takes precedence over what's at the top of the stack
		R = Reduce. The stack should be evaluated before the input is processed.
		      |                               input                            |
		      | +   -   *   /   ^   M   ,   (   )   ==  !=  <   <=  >   >=  $  |
		   ---| --  --  --  --  --  --  --  --  --  --  --  --  --  --  --  -- |
		   +  | R   R   S   S   S   S   R   S   R   R   R   R   R   R   R   R  |
		   -  | R   R   S   S   S   S   R   S   R   R   R   R   R   R   R   R  |
		   *  | R   R   R   R   S   S   R   S   R   R   R   R   R   R   R   R  |
		   /  | R   R   R   R   S   S   R   S   R   R   R   R   R   R   R   R  |
		   ^  | R   R   R   R   S   S   R   S   R   R   R   R   R   R   R   R  |
		   M  | R   R   R   R   R   S   R   S   R   R   R   R   R   R   R   R  |
		   ,  | R   R   R   R   R   R   E4  R   R   R   R   R   R   R   R   E4 |
		s  (  | S   S   S   S   S   S   S   S   S   S   S   S   S   S   S   E1 |
		t  )  | R   R   R   R   R   R   E4  E2  R   R   R   R   R   R   R   R  |
		k  == | R   R   R   R   R   R   R   S   R   R   R   R   R   R   R   R  |
		   != | R   R   R   R   R   R   R   S   R   R   R   R   R   R   R   R  |
		   <  | R   R   R   R   R   R   R   S   R   R   R   R   R   R   R   R  |
		   <= | R   R   R   R   R   R   R   S   R   R   R   R   R   R   R   R  |
		   >  | R   R   R   R   R   R   R   S   R   R   R   R   R   R   R   R  |
		   >= | R   R   R   R   R   R   R   S   R   R   R   R   R   R   R   R  |
		   $  | S   S   S   S   S   S   E4  S   E3  S   S   S   S   S   S   A  | */
		   
		   // comparison operators sit near the bottom of the precdence table, only bitwise operations are lower
		   // but we're not doing bitwise
		   // see: http://en.wikipedia.org/wiki/Order_of_operations
		
		// C# really needs C-style #defines at times...
		private static readonly P[,] _precedence = {
			//  +         -         *         /         ^         M         ,         (         )         ==        !=        <         <=        >         >=       $
	/*	+	*/{ P.Reduce, P.Reduce, P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Reduce, P.Shift,  P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce },
	/*	-	*/{ P.Reduce, P.Reduce, P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Reduce, P.Shift,  P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce },
	/*	*	*/{ P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Shift,  P.Shift,  P.Reduce, P.Shift,  P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce },
	/*	/	*/{ P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Shift,  P.Shift,  P.Reduce, P.Shift,  P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce },
	/*	^	*/{ P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Shift,  P.Shift,  P.Reduce, P.Shift,  P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce },
	/*	M	*/{ P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Shift,  P.Reduce, P.Shift,  P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce },
	/*	,	*/{ P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Error4, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Error4 },
	/*	(	*/{ P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Error1 },
	/*	)	*/{ P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Error4, P.Error2, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce },
	/*	==	*/{ P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Shift , P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce },
	/*	!=	*/{ P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Shift,  P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce },
	/*	<	*/{ P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Shift,  P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce },
	/*	<=	*/{ P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Shift,  P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce },
	/*	>	*/{ P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Shift,  P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce },
	/*	>=	*/{ P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Shift,  P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce, P.Reduce },
	/*	$	*/{ P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Error4, P.Shift,  P.Error3, P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Accept }
		};
		
		private Object _lock = new Object();
		
		private Stack<Double>   _valueStack    = new Stack<Double>();
		private Stack<Operator> _operatorStack = new Stack<Operator>();
		
		private readonly String _expression;
		private Dictionary<String,Double> _symbols;
		
		private Operator _token; // current token
		private Double   _value; // current value
		private Operator _ptoken; // previous token
		
		private Boolean  _isFirstToken;
		private Int32    _toki;
		
		public Expression(String expression) {
			
			_expression = expression;
		}
		
		public Double Evaluate(Dictionary<String,Double> symbols) {
			
			lock( _lock ) {
			
			////////////////////////////
			// Reinit
			
			_valueStack.Clear();
			_operatorStack.Clear();
			_operatorStack.Push(Operator.Eof );
			
			_token  = Operator.Eof;
			_ptoken = Operator.Eof;
			_value  = 0;
			_isFirstToken = true;
			_toki         = 0;
			
			_symbols = symbols;			
			
			Advance();
			
			while(true) {
				
				if( _token == Operator.Val ) {
					// if the current token is a value
					// shift it to value stack
					
					Shift();
					
				} else {
					
					// get precedence for the last operator and the current operator
					Operator lastOp = _operatorStack.Peek();
					Precedence p = _precedence[ (int)lastOp, (int)_token ];
					switch(p) {
						case Precedence.Reduce:
							
							Reduce();
							
							break;
						case Precedence.Shift:
							
							Shift();
							
							break;
						case Precedence.Accept:
							
							EnsureVal(1);
							
							return _valueStack.Pop();
							
						default:
							throw new ExpressionException( p.ToString() + " at position " + _toki );
					}
					
				}
				
			}
		
			}
		}
		
		private void Advance() {
			
			if( _isFirstToken ) {
				
				_isFirstToken = false;
				_ptoken = Operator.Eof;
				
			}
			
			String s = Strtok();
			if( s == null ) {
				
				_token = Operator.Eof;
				
			} else {
				
				switch(s) {
// Arithmetic
					case "+" : _token = Operator.Add; break;
					case "-" : _token = Operator.Sub; break;
					case "*" : _token = Operator.Mul; break;
					case "/" : _token = Operator.Div; break;
					case "^" : _token = Operator.Pow; break;
// Punctuation
					case "," : _token = Operator.Cmm; break;
					case "(" : _token = Operator.PaL; break;
					case ")" : _token = Operator.PaR; break;
					
// Comparison
					case "==": _token = Operator.CoE; break;
					case "!=": _token = Operator.CoN; break;
					case "<" : _token = Operator.CoL; break;
					case "<=": _token = Operator.CLE; break;
					case ">" : _token = Operator.CoG; break;
					case ">=": _token = Operator.CGE; break;
// Logic
					case "&&": _token = Operator.And; break;
					case "||": _token = Operator.Or;  break;
					case "^^": _token = Operator.Xor; break;
					case "!" : _token = Operator.Not; break;
					default:
						// either a number or a name
						// if it's a name, resolve it
						
						if( Double.TryParse( s, out _value ) ) {
							
							_token = Operator.Val;
							
						} else {
							
							if( _symbols.TryGetValue( s, out _value ) ) {
								
								_token = Operator.Val;
								
							} else {
								
								throw new ExpressionException("Undefined symbol: \"" + s + "\" at position " + _toki );
							}
							
						}
						
						break;
				}
				
			}
			
			// check for unary minus
			if( _token == Operator.Sub ) {
				
				if( _ptoken != Operator.Val && _ptoken != Operator.PaR ) {
					
					_token = Operator.Neg;
				}
				
			}
			
			_ptoken = _token;
			
		}
		
		private String Strtok() {
			
			if( _toki >= _expression.Length ) return null;
			
			return _expression.Tok(ref _toki);
			
		}
		
		private void Shift() {
			
			if( _token == Operator.Val ) {
				
				_valueStack.Push( _value );
				
			} else {
				
				_operatorStack.Push( _token );
				
			}
			
			Advance();
		}
		
		private void Reduce() {
			
			Operator op = _operatorStack.Peek();
			switch(op) {
				
				case Operator.Add:
					
					// Apply E := E + E
					EnsureVal(2);
					Double aa = _valueStack.Pop();
					Double ab = _valueStack.Pop();
					_valueStack.Push( aa + ab );
					
					break;
				
				case Operator.Sub:
					
					// Apply E := E - E
					EnsureVal(2);
					Double sa = _valueStack.Pop();
					Double sb = _valueStack.Pop();
					_valueStack.Push( sb - sa );
					
					break;
				
				case Operator.Mul:
					
					EnsureVal(2);
					Double ma = _valueStack.Pop();
					Double mb = _valueStack.Pop();
					_valueStack.Push( ma * mb );
					
					break;
				
				case Operator.Div:
					
					EnsureVal(2);
					Double da = _valueStack.Pop();
					Double db = _valueStack.Pop();
					_valueStack.Push( db / da );
					
					break;
				
				case Operator.Neg:
					
					EnsureVal(1);
					Double na = _valueStack.Pop();
					_valueStack.Push( -na );
					
					break;
				
				case Operator.Pow:
					
					EnsureVal(2);
					Double pa = _valueStack.Pop();
					Double pb = _valueStack.Pop();
					_valueStack.Push( Math.Pow( pb, pa ) );
					
					break;
					
				case Operator.PaR:
					
					_operatorStack.Pop();
					break;
				
				case Operator.CoE:
				case Operator.CoN:
				case Operator.CoL:
				case Operator.CLE:
				case Operator.CoG:
				case Operator.CGE:
					
					EnsureVal(2);
					Double ea = _valueStack.Pop();
					Double eb = _valueStack.Pop();
					
					Boolean eq = ea == eb;
					Boolean lt = eb <  ea;
					Boolean gt = eb >  ea;
					
					Boolean result = eq;
					
					if     ( op == Operator.CoE ) _valueStack.Push( eq       ? 1 : 0 );
					else if( op == Operator.CoN ) _valueStack.Push( eq       ? 0 : 1 );
					else if( op == Operator.CoL ) _valueStack.Push( lt       ? 1 : 0 );
					else if( op == Operator.CLE ) _valueStack.Push( lt || eq ? 1 : 0 );
					else if( op == Operator.CoG ) _valueStack.Push( gt       ? 1 : 0 );
					else if( op == Operator.CGE ) _valueStack.Push( gt || eq ? 1 : 0 );
					
					break;
					
				case Operator.And:
				case Operator.Or:
				case Operator.Xor:
					
					EnsureVal(2);
					Double binA = _valueStack.Pop();
					Double binB = _valueStack.Pop();
					
					Boolean and = binA == 1 && binB == 1;
					Boolean or  = binA == 1 || binB == 1;
					Boolean xor = (binA == 1 && binB != 1) || (binA != 1 && binB == 1);
					
					if( op == Operator.And ) _valueStack.Push( and ? 1 : 0 );
					if( op == Operator.Or  ) _valueStack.Push( or  ? 1 : 0 );
					if( op == Operator.Xor ) _valueStack.Push( xor ? 1 : 0 );
					
					break;
					
				case Operator.Not:
					
					EnsureVal(1);
					Double notA = _valueStack.Pop();
					if(notA == 1) notA = 0;
					else          notA = 1;
					
					_valueStack.Push( notA );
					
					break;
				
//				case Operator.Val:
//				case Operator.Eof:
//					throw new InvalidOperationException();
//				
//				default:
//					throw new NotImplementedException();
				
			}
			
			_operatorStack.Pop();
		}
		
		private void EnsureVal(Int32 depth) {
			
			if( _valueStack.Count < depth ) throw new ExpressionException("Syntax error (EnsureVal) at position " + _toki );
			
		}
		
	}
	
	internal enum Operator { // numbering is importance because it's used as a lookup in the precedence table
// Arithmetic	
		Add = 0,
		Sub = 1,
		Mul,
		Div,
		Pow,
		Neg, // unary negation
// Punctuation
		Cmm, // comma
		PaL, // left parens
		PaR, // right parens
// Comparison
		CoE, // Comparison: Equals
		CoN, // Comparison: Not Equals
		CoL, // Comparison: Less Than
		CLE, // Comparison: Less Than or Equal To
		CoG, // Comparison: Greater Than
		CGE, // Comparison: Greater Than or Equal To
// Logic
		And,
		Or,
		Not,
		Xor,
// Special
		Eof, // end of
		Max, // maximum number of operators
		Val, // value
	}
	
	internal enum Precedence {
		Shift  = 0,
		Reduce = 1,
		Accept = 2,
		/// <summary>Missing right parenthesis</summary>
		Error1 = 6,
		/// <summary>Missing operator</summary>
		Error2 = 7,
		/// <summary>Unbalanced right parenthesis</summary>
		Error3 = 8,
		/// <summary>Invalid function argument</summary>
		Error4 = 9
	}
	
	[Serializable]
	public class ExpressionException : Exception {
		
		public ExpressionException() {
		}
		
		public ExpressionException(string message) : base(message) {
		}
		
		public ExpressionException(string message, Exception inner) : base(message, inner) {
		}
		
		protected ExpressionException( S.SerializationInfo info, S.StreamingContext context) : base(info, context) {
		}
		
	}
	
}
