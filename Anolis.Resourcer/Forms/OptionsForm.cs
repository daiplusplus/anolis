using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Anolis.Resourcer.Settings;

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

			this.__legalToggle.Click += new EventHandler(__legalToggle_Click);
			this.__legalToggle.Tag = false;
			
			this.Load += new EventHandler(OptionsForm_Load);
			
		}
		
		private Settings.Settings S {
			get { return Settings.Settings.Default; }
		}
		
		public MainForm MainForm { get; set; }
		
		private void OptionsForm_Load(object sender, EventArgs e) {
			
			__legalText.Text = Anolis.Core.Resources.LegalOverview;
			
			LoadSettings();
		}
		
		private void LoadSettings() {
			
			__sUIButtonsLarge.Checked = !S.Toolbar24;
			
			TriState isAssoc = S.IsAssociatedWithFiles;
			
			__sAssoc.CheckState =
				isAssoc == TriState.True  ? CheckState.Checked : 
				isAssoc == TriState.False ? CheckState.Unchecked : CheckState.Indeterminate;
			
		}
		
		private void __legalToggle_Click(object sender, EventArgs e) {
			
			Boolean showOverview = (Boolean)__legalToggle.Tag;
			
			__legalText.Text = showOverview ? Anolis.Core.Resources.LegalOverview : Anolis.Core.Resources.LegalGpl;
			
			__legalToggle.Text = showOverview ? "Show GPLv2 License" : "Show License Overview";
			
			__legalToggle.Tag = !showOverview;
		}
		
		private void __ok_Click(Object sender, EventArgs e) {
			
			S.Toolbar24 = !__sUIButtonsLarge.Checked;
			if( __sAssoc.CheckState != CheckState.Indeterminate ) S.AssociateWithFiles( __sAssoc.Checked );
			
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
