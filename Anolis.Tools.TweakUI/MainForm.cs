using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Anolis.TweakUI {
	
	public partial class MainForm : Form {
		
		public MainForm() {
			InitializeComponent();
			
			
			this.Load += new EventHandler(MainForm_Load);
		}
		
		private void MainForm_Load(object sender, EventArgs e) {
			
			//////////////////////////////
			// Small Icon Size
			
			Int32 size = ShellSettings.IconSmallSize;
			switch(size) {
				case 8:
					__sis08.Checked = true;
					break;
				case 16:
					__sis16.Checked = true;
					break;
				case 24:
					__sis24.Checked = true;
					break;
				default:
					__sisCustom.Checked = true;
					__sisCustomTxt.Text = size.ToString();
					break;
			}
			
			//////////////////////////////
			// Large Icon Size
			
			size = ShellSettings.IconLargeSize;
			switch(size) {
				case 24:
					__lis24.Checked = true;
					break;
				case 32:
					__lis32.Checked = true;
					break;
				case 48:
					__lis48.Checked = true;
					break;
				default:
					__lisCustom.Checked = true;
					__lisCustomTxt.Text = size.ToString();
					break;
			}
			
			
			
		}
		
	}
}
