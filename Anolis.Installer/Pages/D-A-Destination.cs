using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using W3b.Wizards.WindowsForms;


namespace Anolis.Installer.Pages {
	
	public partial class DestinationPage : Anolis.Installer.Pages.BaseInteriorPage {
		
		public DestinationPage() {
			InitializeComponent();
			
			this.Load += new EventHandler(DestinationPage_Load);
			this.PageUnload += new EventHandler<W3b.Wizards.PageChangeEventArgs>(DestinationPage_PageUnload);
			this.__progGroup.CheckedChanged += new EventHandler(__progGroup_CheckedChanged);
			
			this.__destBrowse.Click += new EventHandler(__destBrowse_Click);
		}
		
		private void __destBrowse_Click(object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) == DialogResult.OK ) {
				
				__destPath.Text = __fbd.SelectedPath;
			}
			
		}
		
		protected override string LocalizePrefix {
			get { return "D_A"; }
		}
		
		private void __progGroup_CheckedChanged(object sender, EventArgs e) {
			
			__progGroupAll.Enabled = __progGroup.Checked;
			__progGroupMe .Enabled = __progGroup.Checked;
		}
		
		private void DestinationPage_Load(object sender, EventArgs e) {
			
			__destPath.Text = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Anolis" );
			
		}
		
		private void DestinationPage_PageUnload(object sender, W3b.Wizards.PageChangeEventArgs e) {
			
			InstallationInfo.ToolsDestination       = new DirectoryInfo( __destPath.Text );
			
			if     ( !__progGroup  .Checked ) InstallationInfo.ToolsStartMenu = InstallationInfo.StartMenu.None;
			else if( __progGroupAll.Checked ) InstallationInfo.ToolsStartMenu = InstallationInfo.StartMenu.AllUsers;
			else if( __progGroupMe .Checked ) InstallationInfo.ToolsStartMenu = InstallationInfo.StartMenu.Myself;
			
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageDBDownloading; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageBMainAction; }
		}
		
	}
}
