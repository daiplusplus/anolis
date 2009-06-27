using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards;
using W3b.Wizards.WindowsForms;

using Anolis.Core.Packages;
using Anolis.Core.Utility;


using Cult = System.Globalization.CultureInfo;

namespace Anolis.Installer.Pages {
	
	public partial class UpdatePackagePage : BaseInteriorPage {
		
		private PackageUpdateInfo _updateInfo;
		
		public UpdatePackagePage() {
			
			InitializeComponent();
			
			this.Load       += new EventHandler(UpdatePackagePage_Load);
			this.PageLoad   += new EventHandler(UpdatePackagePage_PageLoad);
			this.PageUnload += new EventHandler<PageChangeEventArgs>(UpdatePackagePage_PageUnload);
			
			this.__downloadInfo.Click += new EventHandler(__downloadInfo_Click);
			this.__downloadYes .Click += new EventHandler(__downloadYes_Click);
			this.__downloadNo  .Click += new EventHandler(__downloadNo_Click);
			
			this.__bw.DoWork += new DoWorkEventHandler(__bw_DoWork);
			this.__bw.ProgressChanged += new ProgressChangedEventHandler(__bw_ProgressChanged);
			
			Localize();
		}
		
		protected override String LocalizePrefix { get { return "C_C"; } }
		
		private void UpdatePackagePage_Load(object sender, EventArgs e) {
			
			
		}
		
		private void UpdatePackagePage_PageLoad(object sender, EventArgs e) {
			
			if( PackageInfo.Package.UpdateUri == null ) {
				
				WizardForm.LoadPage( NextPage );
				return;
			}
			
			__bw.RunWorkerAsync();
		}
		
		private void __bw_DoWork(object sender, DoWorkEventArgs e) {
			
			DownloadInfo();
			
		}
		
		/////////////////////////////////
		// Download update info
		
		private void DownloadInfo() {
			
			__bw.ReportProgress( -1, InstallerResources.GetString("C_C_infoChecking") );
			
			_updateInfo = PackageInfo.Package.CheckForUpdates();
			
			if( _updateInfo == null ) {
				
				__bw.ReportProgress(100, InstallerResources.GetString("C_C_infoFailed") );
				
				PackageInfo.Package.Log.Add( LogSeverity.Warning, InstallerResources.GetString("C_C_infoFailed") );
				
			} else {
				
				Boolean isNewer = _updateInfo.Version > PackageInfo.Package.Version;
				
				if( isNewer ) {
					
					if( _updateInfo.InformationLocation != null ) {
						
						BeginInvoke( new MethodInvoker( delegate() {
							
							__downloadInfo.Tag = _updateInfo.InformationLocation;
							__downloadInfo.Visible = true;
						} ) );
					}
					
					if( _updateInfo.PackageLocation != null ) {
						
						String message = InstallerResources.GetString("C_C_infoUpdateAvailableAutomatic");
						message = String.Format(Cult.CurrentCulture, message, _updateInfo.Version );
						
						__bw.ReportProgress( 0, message );
						
						BeginInvoke( new MethodInvoker( delegate() {
							
							__downloadYes .Visible = true;
							__downloadNo  .Visible = true;
						} ) );
						
					} else {
						
						String message = InstallerResources.GetString("C_C_infoUpdateAvailableManual");
						message = String.Format(Cult.CurrentCulture, message, _updateInfo.Version );
						
						__bw.ReportProgress( 100, message );
						
					}
				
				} else {
					
					String message = InstallerResources.GetString("C_C_infoUpdateLatest");
					message = String.Format(Cult.CurrentCulture, message, PackageInfo.Package.Version );
					
					__bw.ReportProgress( 100, message );
					
				}
				
			}
			
		}
		
		/////////////////////////////////
		// Download Buttons
		
		private	void __downloadInfo_Click(object sender, EventArgs e) {
			
			Uri dest = __downloadInfo.Tag as Uri;
			
			System.Diagnostics.Process.Start( dest.ToString() );
			
		}
		
		private void __downloadYes_Click(object sender, EventArgs e) {
			
			__sfd.Title = "Save downloaded package to...";
			
			if(__sfd.ShowDialog(this) == DialogResult.OK) {
				
				__downloadYes.Enabled = false;
				__downloadNo .Enabled = false;
				WizardForm.EnableNext = false;
				WizardForm.EnablePrev = false;
				
				DownloadPackage( __sfd.FileName );
			}
			
		}
		
		private void __downloadNo_Click(object sender, EventArgs e) {
			
			// advance to the next page
			
			WizardForm.LoadNextPage();
			
		}
		
		/////////////////////////////////
		// Download updated package
		
		private void DownloadPackage(String savePackageTo) {
			
			if( _updateInfo == null ) return;
			
			WebClient client = new WebClient();
			client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
			client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
			
			_transferTimes.Clear();
			
			client.DownloadFileAsync( _updateInfo.PackageLocation, savePackageTo, savePackageTo );
			
		}
		
		private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
			
			String path = e.UserState as String;
			
			// reload the package and go back to Extracting
			
			Stream fs = File.OpenRead( path );
			
			PackageInfo.Source = PackageSource.Archive;
			PackageInfo.SourcePath = path;
			PackageInfo.Archive = PackageArchive.FromStream( Path.GetFileNameWithoutExtension( path), PackageSubclass.LzmaTarball, fs );
			
			_nextPage = Program.PageCBExtracting;
			
			WizardForm.EnableNext = true;
			WizardForm.EnablePrev = false;
			
		}
		
		private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
			
			_transferTimes.Add( new Pair<DateTime,Int32>( DateTime.Now, (int)e.BytesReceived / 1024 ) );
			
			Int32 xferRate = GetTransferRate();
			
			String message = InstallerResources.GetString("C_C_downloadProgress");
			message = String.Format(Cult.CurrentCulture, message, e.ProgressPercentage, e.BytesReceived / 1024, e.TotalBytesToReceive / 1024, xferRate);
			
			BeginInvoke( new MethodInvoker( delegate() {
				
				__progress.Value = e.ProgressPercentage;
				__statusLbl.Text = message;
				
			} ) );
			
			
			
		}
		
		////////////////////////////////////////
		// Transfer rate calculations
		
		private List<Pair<DateTime,Int32>> _transferTimes = new List<Pair<DateTime,Int32>>();
		
		private Int32 GetTransferRate() {
			
			DateTime now = DateTime.Now;
			
			Pair<DateTime,Int32> last      = _transferTimes[ _transferTimes.Count - 1 ];
			
			Int32 kbSecondAgo = 0;
			
			// get the transfer that happened 1 second ago
			for(int i=_transferTimes.Count-1;i>=0;i--) {
				
				Pair<DateTime,Int32> xfer = _transferTimes[i];
				
				TimeSpan span = now.Subtract( xfer.X );
				if( span.TotalSeconds >= 1 ) { // this is good enough
					
					kbSecondAgo = xfer.Y;
					break;
				}
				
			}
			
			// the difference in bytes is then the 
			
			return last.Y - kbSecondAgo;
			
		}
		
		////////////////////////////////////////
		
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
		
		private BaseWizardPage _nextPage;
		
		public override BaseWizardPage NextPage {
			get {
				if( _nextPage == null ) _nextPage = Program.PageCDReleaseNotes;
				return _nextPage;
			}
		}
		
	}
}
