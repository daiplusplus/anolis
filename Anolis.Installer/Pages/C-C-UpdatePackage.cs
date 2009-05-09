using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards;
using W3b.Wizards.WindowsForms;

using Anolis.Core.Packages;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Installer.Pages {
	
	public partial class UpdatePackagePage : BaseInteriorPage {
		
		private PackageUpdateInfo _updateInfo;
		
		public UpdatePackagePage() {
			
			InitializeComponent();
			
			this.Load       += new EventHandler(UpdatePackagePage_Load);
			this.PageLoad   += new EventHandler(UpdatePackagePage_PageLoad);
			this.PageUnload += new EventHandler<PageChangeEventArgs>(UpdatePackagePage_PageUnload);
			
			this.__downloadYes.Click += new EventHandler(__downloadYes_Click);
			this.__downloadNo.Click += new EventHandler(__downloadNo_Click);
			
			this.__bw.DoWork += new DoWorkEventHandler(__bw_DoWork);
			this.__bw.ProgressChanged += new ProgressChangedEventHandler(__bw_ProgressChanged);
			
			Localize();
		}
		
		private void __downloadNo_Click(object sender, EventArgs e) {
			
			// advance to the next page
			
			// TODO: Add a "LoadNextPage()" method to IWizardForm
			WizardForm.LoadPage( NextPage );
			
		}
		
		private void __downloadYes_Click(object sender, EventArgs e) {
			
		}
		
		protected override String LocalizePrefix { get { return "C_C"; } }
		
		private void UpdatePackagePage_Load(object sender, EventArgs e) {
			
			
			
		}
		
		private void UpdatePackagePage_PageLoad(object sender, EventArgs e) {
			
			__bw.RunWorkerAsync();
		}
		
		/////////////////////////////////
		// Download update info
		
		private void DownloadInfo() {
			
			__bw.ReportProgress( -1, "Checking for package update" );
			
			_updateInfo = PackageInfo.Package.CheckForUpdates();
			
			if( _updateInfo == null ) {
				
				__bw.ReportProgress(100, "Check for updates failed: could not download update info");
				
				PackageInfo.Package.Log.Add( LogSeverity.Warning, "Could not download package update info" );
				
			} else {
				
				Boolean isNewer = _updateInfo.Version > PackageInfo.Package.Version;
				
				if( isNewer ) {
					
					__downloadInfo.Tag = _updateInfo.InformationLocation;
					__downloadInfo.Visible = true;
					
					if( _updateInfo.PackageLocation != null ) {
						
						String message = "An updated version ({0}) of this package is available. Would you like to download it now?";
						
						__updateAvailable.Text = String.Format(Cult.CurrentCulture, message, _updateInfo.Version );
						
						__downloadYes .Visible = true;
						__downloadNo  .Visible = true;
						
					} else {
						
						String message = "An updated version ({0}) of this package is available. But you need to download it manually. Click the Visit Package Webpage for more information.";
						
						__updateAvailable.Text = String.Format(Cult.CurrentCulture, message, _updateInfo.Version );
						
					}
				
				} else {
					
					String message = "You have the latest version ({0}) of this package.";
					
					__updateAvailable.Text = String.Format(Cult.CurrentCulture, message, PackageInfo.Package.Version );
					
				}
				
			}
			
		}
		
		/////////////////////////////////
		// Download updated package
		
		private void DownloadPackage(String savePackageTo) {
			
			if( _updateInfo == null ) return;
			
			WebClient client = new WebClient();
			client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
			client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
			
			client.DownloadFileAsync( _updateInfo.PackageLocation, savePackageTo, savePackageTo );
			
		}
		
		private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
			
			String path = e.UserState as String;
			
			// just prompt the user to restart the program?
			
		}
		
		private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
			
			BeginInvoke( new MethodInvoker( delegate() {
				
				__progress.Value = e.ProgressPercentage;
				__statusLbl.Text = String.Format(Cult.CurrentCulture, "{0}% - {1}KB/{2}KB downloaded", e.ProgressPercentage, e.BytesReceived / 1024, e.TotalBytesToReceive / 1024);
				
			} ) );
			
		}
		
		////////////////////////////////////////
		
		private void __bw_DoWork(object sender, DoWorkEventArgs e) {
			
			
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
