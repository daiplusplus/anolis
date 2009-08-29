using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Anolis.Packager {
	
	public partial class MainForm : Form {
		
		public MainForm() {
			InitializeComponent();
			
			this.__import.Click += new EventHandler(__button_Click);
			this.__define.Click += new EventHandler(__button_Click);
			this.__compre.Click += new EventHandler(__button_Click);
			this.__optimi.Click += new EventHandler(__button_Click);
			this.__distro.Click += new EventHandler(__button_Click);
			
			this.__icoed.Click += new EventHandler(__icoed_Click);
			this.__icoed.Enabled = File.Exists("Anolis.IconEditor.exe");
		}
		
		private void __icoed_Click(object sender, EventArgs e) {
			
			if( File.Exists("Anolis.IconEditor.exe") ) Process.Start("Anolis.IconEditor.exe");
		}
		
		private void __button_Click(object sender, EventArgs e) {
			
			Form form = (sender as Button).Tag as Form;
			if( form == null || form.IsDisposed ) {
				
				     if( sender == __import ) __import.Tag = form = new ImportForm();
				else if( sender == __define ) __define.Tag = form = new DefinitionForm();
				else if( sender == __compre ) __compre.Tag = form = new TarLzmaForm();
				else if( sender == __optimi ) __optimi.Tag = form = new OptimizerForm();
				else if( sender == __distro ) __distro.Tag = form = new DistributorForm();
			}
			
			if( form.Visible ) {
				
				form.BringToForeground();
				
			} else {
				
				form.Show(this);
			}
			
			
		}
		
	}
}
