using System;
using System.IO;
using System.Windows.Forms;

namespace Anolis.Packager {
	
	public partial class ImportForm : Form {
		
		public ImportForm() {
			
			InitializeComponent();
			
			this.__close.Click += new EventHandler(__close_Click);
			
			this.__nsiBrowse  .Click += new EventHandler(__nsiBrowse_Click);
			this.__filesBrowse.Click += new EventHandler(__filesBrowse_Click);
			this.__packBrowse .Click += new EventHandler(__packBrowse_Click);

			this.__convert.Click += new EventHandler(__convert_Click);
		}
		
		private void __convert_Click(object sender, EventArgs e) {
			
			Boolean nsiExists  = File.Exists( __nsiPath.Text );
			Boolean filesExist = Directory.Exists( __filesPath.Text );
			
			if( !nsiExists || !filesExist ) {
				
				MessageBox.Show(this, "The specified NSI script or files directory does not exist", "Anolis Packager", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				return;
			}
			
			XisImporter.Convert( __nsiPath.Text, __filesPath.Text, __packPath.Text );
			
			MessageBox.Show(this, "Converted", "Anolis Packager", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
		}
		
		private void __packBrowse_Click(object sender, EventArgs e) {
			
			if( __sfd.ShowDialog(this) == DialogResult.OK ) {
				
				__packPath.Text = __sfd.FileName;
			}
			
		}
		
		private void __filesBrowse_Click(object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) == DialogResult.OK ) {
				
				__filesPath.Text = __fbd.SelectedPath;
			}
			
		}
		
		private void __nsiBrowse_Click(object sender, EventArgs e) {
			
			if( __ofd.ShowDialog(this) == DialogResult.OK ) {
				
				__nsiPath.Text = __ofd.FileName;
			}
			
		}
		
		private void __close_Click(object sender, EventArgs e) {
			
			this.Close();
		}
		
		
		
	}
}
