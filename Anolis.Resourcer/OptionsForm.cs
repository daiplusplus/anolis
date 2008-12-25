using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Anolis.Resourcer {
	
	public partial class OptionsForm : Form {
		
		public OptionsForm() {
			InitializeComponent();
			
			this.__ok.Click += new EventHandler(__ok_Click);
			
			this.__aboutLinkAnolis .Click += new EventHandler(__aboutLink_Click);
			this.__aboutLinkXpize  .Click += new EventHandler(__aboutLink_Click);
			this.__aboutLinkVize   .Click += new EventHandler(__aboutLink_Click);
			this.__aboutFriendsMsfn.Click += new EventHandler(__aboutLink_Click);
			this.__aboutFriendsLong.Click += new EventHandler(__aboutLink_Click);
			this.__aboutFriendsC9  .Click += new EventHandler(__aboutLink_Click);

			this.Load += new EventHandler(OptionsForm_Load);
			
		}
		
		private void OptionsForm_Load(object sender, EventArgs e) {
			
			__licenseText.Text = Anolis.Resourcer.Properties.Resources.AnolisGplLicense;
			
		}
		
		private void __ok_Click(Object sender, EventArgs e) {
			
			Close();
			
		}
		
		private void __aboutLink_Click(Object sender, EventArgs e) {
			
			LinkLabel link = sender as LinkLabel;
			
			String url = link.Text.Substring( link.Text.IndexOf("http://") );
			
			System.Diagnostics.Process.Start( url );
			
		}
		
	}
	
}
