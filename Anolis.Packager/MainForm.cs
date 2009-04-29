using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Anolis.Packager {
	
	public partial class MainForm : Form {
		
		public MainForm() {
			InitializeComponent();
			
			this.__import.Tag = new ImportForm();
			this.__define.Tag = new DefinitionForm();
			this.__compre.Tag = new TarLzmaForm();
			this.__distro.Tag = new DistributorForm();
			
			
			this.__import.Click += new EventHandler(__button_Click);
			this.__define.Click += new EventHandler(__button_Click);
			this.__compre.Click += new EventHandler(__button_Click);
			this.__distro.Click += new EventHandler(__button_Click);

			this.__icoed.Click += new EventHandler(__icoed_Click);
		}
		
		private void __icoed_Click(object sender, EventArgs e) {
			
			Process.Start("Anolis.IconEditor.exe");
		}
		
		private void __button_Click(object sender, EventArgs e) {
			
			Form form = (sender as Button).Tag as Form;
			
			if( form.Visible ) {
				
				form.BringToForeground();
				
			} else {
				
				form.Show(this);
			}
			
			
		}
		
	}
}
