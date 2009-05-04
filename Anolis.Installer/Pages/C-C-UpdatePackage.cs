using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards;
using W3b.Wizards.WindowsForms;

using Anolis.Core.Packages;

namespace Anolis.Installer.Pages {
	
	public partial class UpdatePackagePage : BaseInteriorPage {
		
		public UpdatePackagePage() {
			
			InitializeComponent();
			
			this.Load       += new EventHandler(UpdatePackagePage_Load);
			this.PageLoad   += new EventHandler(UpdatePackagePage_PageLoad);
			this.PageUnload += new EventHandler<PageChangeEventArgs>(UpdatePackagePage_PageUnload);
			
			this.__bw.DoWork += new DoWorkEventHandler(__bw_DoWork);
			this.__bw.ProgressChanged += new ProgressChangedEventHandler(__bw_ProgressChanged);
			
			Localize();
		}
		
		protected override String LocalizePrefix { get { return "C_C"; } }
		
		private void UpdatePackagePage_Load(object sender, EventArgs e) {
			
			
			
		}
		
		private void UpdatePackagePage_PageLoad(object sender, EventArgs e) {
			
//			__bw.RunWorkerAsync();
		}
		
		private void __bw_DoWork(object sender, DoWorkEventArgs e) {
			
			__bw.ReportProgress( -1, "Checking for package update" );
			
			PackageUpdateInfo info = PackageInfo.Package.CheckForUpdates();
			if( info == null ) {
				
				__bw.ReportProgress(100, "Check for updates failed");
				return;
			}
			
			if( info.PackageLocation != null ) {
				
				__bw.ReportProgress(10, "Downloading updated package");
				
				
				
			}
			
			
			
		}
		
		private void __bw_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			
			if( e.ProgressPercentage == -1 ) {
				
				__progress.Style = ProgressBarStyle.Marquee;
				
			} else {
				
				if( __progress.Style != ProgressBarStyle.Blocks ) __progress.Style = ProgressBarStyle.Blocks;
				
				__progress.Value = e.ProgressPercentage;
			}
			
			__statusLbl.Text = e.UserState as String;
			
		}
		
		private void UpdatePackagePage_PageUnload(object sender, PageChangeEventArgs e) {
			
			
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageCASelectPackage; }
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageCDModifyPackage; }
		}
		
	}
}
