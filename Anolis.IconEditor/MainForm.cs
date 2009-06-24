using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Anolis.Core.Utility;
using System.IO;

namespace Anolis.IconEditor {
	
	public partial class MainForm : Form {
		
		private FileInfo  _file;
		private Boolean   _isUnsaved;
		private IconGroup _icon;
		
		public MainForm() {
			InitializeComponent();
			
			this.__tNew.Click  += new EventHandler(__tNew_Click);
			this.__tOpen.Click += new EventHandler(__tOpen_Click);
			this.__tSave.Click += new EventHandler(__tSave_Click);
			this.__tOptions.Click += new EventHandler(__tOptions_Click);

			this.__tSAdd.Click += new EventHandler(__tSAdd_Click);
			this.__tSExport.Click += new EventHandler(__tSExport_Click);
			this.__tSReplace.Click += new EventHandler(__tSReplace_Click);
			this.__tSDelete.Click += new EventHandler(__tSDelete_Click);
		}
		
		private void MainForm_Load(object sender, EventArgs e) {
			
		}
		
#region UI Events
		
	#region Toolbar - Main
		
		private void __tNew_Click(object sender, EventArgs e) {
			
		}
		
		private void __tOpen_Click(object sender, EventArgs e) {
			
			IconLoadPrompt();
			
		}
		
		private void __tSave_Click(object sender, EventArgs e) {
			
		}
		
		private void __tOptions_Click(object sender, EventArgs e) {
			
		}
		
	#endregion
		
	#region Toolbar - Subimages
		
		private void __tSAdd_Click(object sender, EventArgs e) {
			
		}
		
		private void __tSExport_Click(object sender, EventArgs e) {
			
		}
		
		private void __tSReplace_Click(object sender, EventArgs e) {
			
		}
		
		private void __tSDelete_Click(object sender, EventArgs e) {
			
		}
		
	#endregion
		
#endregion
		
#region Work
		
		private void IconLoadPrompt() {
			
			if( _isUnsaved ) {
				
				DialogResult result = MessageBox.Show(this, "Save changes to " + _file.Name + " before unloading?", "Anolis Icon Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
				switch(result) {
					case DialogResult.Yes:
						
						IconSave();
						break;
						
					case DialogResult.No:
						
						break;
						
					case DialogResult.Cancel:
						return;
				}
				
			}
			
			if( __ofd.ShowDialog(this) == DialogResult.OK ) {
				
				String fileName = __ofd.FileName;
				if( !File.Exists( fileName ) ) return;
				
				IconLoad( fileName );
				
			}
			
			
		}
		
		private void IconLoad(String fileName) {
			
			_icon = new IconGroup( fileName );
			
			// Do the GUI
			
			this.Text = String.Format("{0} - Anolis Icon Editor", Path.GetFileName( fileName ) );
			
			
			
		}
		
		private void IconSave() {
			
			_icon.Save( _file.FullName );
			
		}
		
#endregion
		
		
	}
}
