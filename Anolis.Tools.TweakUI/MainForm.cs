using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Anolis.Tools.TweakUI.FileTypes;
using Anolis.Tools.TweakUI.ThumbsDB;

namespace Anolis.Tools.TweakUI {
	
	public partial class MainForm : Form {
		
		public MainForm() {
			InitializeComponent();
			
			this.Load += new EventHandler(MainForm_Load);
			this.__toolsThumbs.Click += new EventHandler(__toolsThumbs_Click);
			this.__toolsTypes.Click += new EventHandler(__toolsTypes_Click);
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
					__sisCustomRad.Checked = true;
					__sisCustom.Value = size;
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
					__lisCustomRad.Checked = true;
					__lisCustom.Value = size;
					break;
			}
		}
		
		private void __toolsTypes_Click(object sender, EventArgs e) {
			
			FileTypeForm form = new FileTypeForm();
			form.ShowDialog(this);
			
		}
		
		private void __toolsThumbs_Click(object sender, EventArgs e) {
			
			ThumbnailForm form = new ThumbnailForm();
			form.ShowDialog(this);
			
		}
		
	}
}
