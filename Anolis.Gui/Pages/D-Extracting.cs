using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Anolis.Gui.Pages {
	
	public partial class ExtractingPage : BaseInteriorPage {
		
		private Timer _t;
		
		public ExtractingPage() {
			InitializeComponent();
			
			this.Load += new EventHandler(Extracting_Load);
			
			_t = new Timer();
			_t.Tick += new EventHandler(t_Tick);
			_t.Interval = 3;
			
		}
		
		private void Extracting_Load(object sender, EventArgs e) {
			
			// simulate progress for now
			
			_t.Start();
			
			WizardForm.EnableNext = false;
			WizardForm.EnablePrev = false;
			
		}
		
		private void t_Tick(object sender, EventArgs e) {
			
			__progress.Value++;
			
			if( __progress.Value >= 100 ) {
				_t.Stop();
				
				WizardForm.LoadPage( NextPage );
			}
			
		}
		
		public override W3b.Wizards.WizardPage PreviousPage {
			get { return Program.PageCSelectPackage; }
		}
		
		public override W3b.Wizards.WizardPage NextPage {
			get { return Program.PageEModifyPackage; }
		}
		
	}
}
