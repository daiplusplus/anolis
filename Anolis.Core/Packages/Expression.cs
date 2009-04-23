using System;
using System.Collections.Generic;
using System.Text;

using S = System.Runtime.Serialization;

namespace Anolis.Core.Packages {
	
	using P = Anolis.Core.Packages.Precedence;
	
	// TODO:
		// * Add support for radix points in numbers (you get an error about '.' being undefined)
		// * Add support for comparison operators ==, !=, <, <=, >, >=
		// * Add support for boolean operators &&, ||, ^^, !
		// * Add support for functions, e.g. sin(), a function can be defined in the symbol table: with that functions' expression as the dictionary value
		// I think booleans can be supported by having false == 0 and true == non-zero
	
	/// <summary>A C# implementation of Tom Niemann's Operator Precedence Parsing system ( http://epaperpress.com/oper/index.html ).</summary>
	public class Expression {
		
		// The precedence table; beautiful, isn't it?
/*
		S = Shift. The input takes precedence over what's at the top of the stack
		R = Reduce. The stack should be evaluated before the input is processed.
		      |                    input                           |
		      | +   -   *   /   ^   M   f   p   c   ,   (   )   $  |
		   ---| --  --  --  --  --  --  --  --  --  --  --  --  -- |
		   +  | R,  R,  S,  S,  S,  S,  S,  S,  S,  R,  S,  R,  R  |
		   -  | R,  R,  S,  S,  S,  S,  S,  S,  S,  R,  S,  R,  R  |
		   *  | R,  R,  R,  R,  S,  S,  S,  S,  S,  R,  S,  R,  R  |
		   /  | R,  R,  R,  R,  S,  S,  S,  S,  S,  R,  S,  R,  R  |
		s  ^  | R,  R,  R,  R,  S,  S,  S,  S,  S,  R,  S,  R,  R  |
		t  M  | R,  R,  R,  R,  R,  S,  S,  S,  S,  R,  S,  R,  R  |
		k  f  | E4, E4, E4, E4, E4, E4, E4, E4, E4, E4, S,  R,  R  |
		   p  | E4, E4, E4, E4, E4, E4, E4, E4, E4, E4, S,  R,  R  |
		   c  | E4, E4, E4, E4, E4, E4, E4, E4, E4, E4, S,  R,  R  |
		   ,  | R,  R,  R,  R,  R,  R,  R,  R,  R,  E4, R,  R,  E4 |
		   (  | S,  S,  S,  S,  S,  S,  S,  S,  S,  S,  S,  S,  E1 |
		   )  | R,  R,  R,  R,  R,  R,  E3, E3, E3, E4, E2, R,  R  |
		   $  | S,  S,  S,  S,  S,  S,  S,  S,  S,  E4, S,  E3, A  | */
		   
		   // comparison operators sit near the bottom of the precdence table, only bitwise operations are lower
		   // but we're not doing bitwise
		   // see: http://en.wikipedia.org/wiki/Order_of_operations
		
		// C# really needs C-style #defines at times...
		private static readonly P[,] _precedence = {
			{ P.Reduce,  P.Reduce,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Reduce,  P.Shift,  P.Reduce,  P.Reduce  },
			{ P.Reduce,  P.Reduce,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Reduce,  P.Shift,  P.Reduce,  P.Reduce  },
			{ P.Reduce,  P.Reduce,  P.Reduce,  P.Reduce,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Reduce,  P.Shift,  P.Reduce,  P.Reduce  },
			{ P.Reduce,  P.Reduce,  P.Reduce,  P.Reduce,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Reduce,  P.Shift,  P.Reduce,  P.Reduce  },
			{ P.Reduce,  P.Reduce,  P.Reduce,  P.Reduce,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Reduce,  P.Shift,  P.Reduce,  P.Reduce  },
			{ P.Reduce,  P.Reduce,  P.Reduce,  P.Reduce,  P.Reduce,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Reduce,  P.Shift,  P.Reduce,  P.Reduce  },
			{ P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.Shift,  P.Reduce,  P.Reduce  },
			{ P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.Shift,  P.Reduce,  P.Reduce  },
			{ P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.ErrorInvalidFunctionArgument, P.Shift,  P.Reduce,  P.Reduce  },
			{ P.Reduce,  P.Reduce,  P.Reduce,  P.Reduce,  P.Reduce,  P.Reduce,  P.Reduce,  P.Reduce,  P.Reduce,  P.ErrorInvalidFunctionArgument, P.Reduce,  P.Reduce,  P.ErrorInvalidFunctionArgument },
			{ P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.ErrorMissingRightParens },
			{ P.Reduce,  P.Reduce,  P.Reduce,  P.Reduce,  P.Reduce,  P.Reduce,  P.ErrorUnbalancedRightParens, P.ErrorUnbalancedRightParens, P.ErrorUnbalancedRightParens, P.ErrorInvalidFunctionArgument, P.ErrorMissingOperator, P.Reduce,  P.Reduce  },
			{ P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.Shift,  P.ErrorInvalidFunctionArgument, P.Shift,  P.ErrorUnbalancedRightParens, P.Accept  }
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
							throw new ExpressionException( p.ToString() );
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
					case "+": _token = Operator.Add; break;
					case "-": _token = Operator.Sub; break;
					case "*": _token = Operator.Mul; break;
					case "/": _token = Operator.Div; break;
					case "^": _token = Operator.Pow; break;
					case "(": _token = Operator.PaL; break;
					case ")": _token = Operator.PaR; break;
					case ",": _token = Operator.Cmm; break;
//					case "f": _token = Operator.Fact; break;
//					case "p": _token = Operator.Perm; break;
//					case "c": _token = Operator.Comb; break;
					default:
						// either a number or a name
						// if it's a name, resolve it
						
						if( Double.TryParse( s, out _value ) ) {
							
							_token = Operator.Val;
							
						} else {
							
							if( _symbols.TryGetValue( s, out _value ) ) {
								
								_token = Operator.Val;
								
							} else {
								
								throw new ExpressionException("Undefined symbol: \"" + s + '"' );
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
					EnsureVal(1);
					Double aa = _valueStack.Pop();
					Double ab = _valueStack.Pop();
					_valueStack.Push( aa + ab );
					
					break;
				
				case Operator.Sub:
					
					// Apply E := E - E
					EnsureVal(1);
					Double sa = _valueStack.Pop();
					Double sb = _valueStack.Pop();
					_valueStack.Push( sb - sa );
					
					break;
				
				case Operator.Mul:
					
					EnsureVal(1);
					Double ma = _valueStack.Pop();
					Double mb = _valueStack.Pop();
					_valueStack.Push( ma * mb );
					
					break;
				
				case Operator.Div:
					
					EnsureVal(1);
					Double da = _valueStack.Pop();
					Double db = _valueStack.Pop();
					_valueStack.Push( db / da );
					
					break;
				
				case Operator.Neg:
					
					EnsureVal(0);
					Double na = _valueStack.Pop();
					_valueStack.Push( -na );
					
					break;
				
				case Operator.Pow:
					
					EnsureVal(1);
					Double pa = _valueStack.Pop();
					Double pb = _valueStack.Pop();
					_valueStack.Push( Math.Pow( pb, pa ) );
					
					break;
					
				case Operator.PaR:
					
					_operatorStack.Pop();
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
			
			if( _valueStack.Count < depth ) throw new ExpressionException("Syntax error");
			
		}
		
	}
	
	internal enum Operator { // numbering is importance because it's used as a lookup in the precedence table
		Add = 0,
		Sub = 1,
		Mul,
		Div,
		Pow,
		Neg, // unary negation
		Fac, // factorial
		Per, // nPr
		Com, // nCr
		Cmm, // comma
		PaL, // left parens
		PaR, // right parens
		Eof, // end of
		Max, // maximum number of operators
		Val, // value
	}
	
	internal enum Precedence {
		Shift  = 0,
		Reduce = 1,
		Accept = 2,
		ErrorMissingRightParens      = 6,
		ErrorMissingOperator         = 7,
		ErrorUnbalancedRightParens   = 8,
		ErrorInvalidFunctionArgument = 9
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
