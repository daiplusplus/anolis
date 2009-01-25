using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Anolis.Resourcer {
	
	public partial class OptionsForm : BaseForm {
		
		public OptionsForm() {
			InitializeComponent();
			
			this.__ok.Click += new EventHandler(__ok_Click);
			
			this.__aboutLinkAnolis .Click += new EventHandler(__aboutLink_Click);
			this.__aboutLinkXpize  .Click += new EventHandler(__aboutLink_Click);
			this.__aboutLinkVize   .Click += new EventHandler(__aboutLink_Click);
			this.__aboutFriendsMsfn.Click += new EventHandler(__aboutLink_Click);
			this.__aboutFriendsLong.Click += new EventHandler(__aboutLink_Click);
			this.__aboutFriendsC9  .Click += new EventHandler(__aboutLink_Click);
			this.__aboutFriendsRafael.Click += new EventHandler(__aboutLink_Click);

			this.Load += new EventHandler(OptionsForm_Load);
			
		}
		
		public MainForm MainForm { get; set; }
		
		private void OptionsForm_Load(object sender, EventArgs e) {
			
			UpdateFonts();
			
			__licenseText.Text = Anolis.Resourcer.Properties.Resources.AnolisGplLicense;
			
			LoadSettings();
		}
		
		private void LoadSettings() {
			
			__sUIButtonsLarge.Checked = !Settings.Settings.Default.Toolbar24;
			
		}
		
		private void __ok_Click(Object sender, EventArgs e) {
			
			Settings.Settings.Default.Toolbar24 = !__sUIButtonsLarge.Checked;
			
			DialogResult = DialogResult.OK;
			
			Close();
			
		}
		
		private void __aboutLink_Click(Object sender, EventArgs e) {
			
			LinkLabel link = sender as LinkLabel;
			
			String url = link.Text.Substring( link.LinkArea.Start, link.LinkArea.Length - link.LinkArea.Start );
			
			System.Diagnostics.Process.Start( url );
			
		}
		
	}
	
}
