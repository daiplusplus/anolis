using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Core.Packages;

namespace Anolis.Packager {
	
	public partial class ConditionEditor : Form {
		
		public ConditionEditor() {
			InitializeComponent();
			
			__expression.Font = new Font(FontFamily.GenericMonospace, __expression.Font.SizeInPoints );
			
			__eval  .Click += new EventHandler(__eval_Click);
			__tok   .Click += new EventHandler(__tok_Click);
			
			__ok    .Click += new EventHandler(__ok_Click);
			__cancel.Click += new EventHandler(__cancel_Click);
		}
		
		private void __cancel_Click(object sender, EventArgs e) {
			
			this.Close();
		}
		
		private void __ok_Click(object sender, EventArgs e) {
			
			DialogResult = DialogResult.OK;
			
			this.Close();
		}
		
		public String Expression {
			get { return __expression.Text; }
			set { __expression.Text = value; }
		}
		
		private Dictionary<String,Double> GetSymbols() {
			
			Dictionary<String,Double> symbols = new Dictionary<string,double>();
			
			foreach( DataGridViewRow row in __symbols.Rows ) {
				
				String name = row.Cells[0].Value as String;
				String sval = row.Cells[1].Value as String;
				
				Double dval;
				if( Double.TryParse( sval, out dval ) )
					symbols.Add( name, dval );
			}
			
			return symbols;
			
		}
		
		private void __tok_Click(object sender, EventArgs e) {
			
			String expr = __expression.Text;
			__result.Text = "";
			
			Int32 i=0;
			while( i < expr.Length ) {
				
				String tok = expr.Tok(ref i);
				__result.Text += tok + ", ";
				
			}
			
			if( __result.Text.EndsWith(", ") ) __result.Text = __result.Text.LeftFR( 2 );
			
		}
		
		private void __eval_Click(object sender, EventArgs e) {
			
			Expression expr = new Expression( __expression.Text );
			
			try {
				
				Double result = expr.Evaluate( GetSymbols() );
				
				__result.Text = result.ToString();
			
			} catch(ExpressionException ex) {
				
				__result.Text = ex.Message;
				
			} catch(Exception ex) {
				
				__result.Text = "Unhandled: " + ex.Message;
				
			}
			
		}
	}
}
