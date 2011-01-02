using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Anolis.Tools.PEInfo {
	
	public partial class MainForm : Form {
		
		private Object _f;
		
		public MainForm() {
			InitializeComponent();
			
			this.__openBrowse.Click += new EventHandler(__openBrowse_Click);
			this.__openLoad.Click += new EventHandler(__openLoad_Click);
			
			__openAs.SelectedIndex = 2;
		}
		
		private void __openBrowse_Click(object sender, EventArgs e) {
			
			if( __ofd.ShowDialog(this) == DialogResult.OK ) {
				
				__openFileName.Text = __ofd.FileName;
			}
			
		}
		
		private void __openLoad_Click(object sender, EventArgs e) {
			
			if( !File.Exists( __openFileName.Text ) ) return;
			
			_f = OpenFile( __openFileName.Text );
			
//			__grid.SelectedObject = new List<Object>(); // _f;
			
			PEFile pe = _f as PEFile;
			if( pe != null ) {
				
				__pe.PEFile = pe;
			}
			
		}
		
		private Object OpenFile(String fileName) {
			
			switch( __openAs.SelectedIndex ) {
				case 0: // DOS
					return new DosFile( fileName );
				case 1: // NE
					return new NEFile( fileName );
				case 2: // PE/COFF
					return new PEFile( fileName );
				default:
					return null;
			}
			
		}
		
	}
}
