using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Anolis.Packager {
	
	public partial class OptimizerForm : Form {
		
		private PackageOptimizer _opt;
		
		public OptimizerForm() {
			
			InitializeComponent();
			
			this.__browse.Click += new EventHandler(__browse_Click);
			this.__load  .Click += new EventHandler(__load_Click);
			
		}
		
		private void __browse_Click(object sender, EventArgs e) {
			
			if( __ofd.ShowDialog(this) == DialogResult.OK ) {
				
				__fileName.Text = __ofd.FileName;
			}
			
		}
		
		private void __load_Click(object sender, EventArgs e) {
			
			_opt = new PackageOptimizer( __fileName.Text );
			List<String> messages = _opt.LoadAndValidate();
			
			__messages.Items.Clear();
			__messages.Items.AddRange( messages.ToArray() );
			
			String[] missing, unreferenced;
			
			_opt.GetFiles(out missing, out unreferenced);
			
			__missingFiles.Items.Clear();
			__missingFiles.Items.AddRange( missing );
			
			__unreferencedFiles.Items.Clear();
			__unreferencedFiles.Items.AddRange( unreferenced );
			
			
		}
	}
}
