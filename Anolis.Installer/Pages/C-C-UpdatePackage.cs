using System;
using System.IO;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;

using W3b.Wizards.WindowsForms;

using Anolis.Packages;
using Anolis.Core.Utility;

using Cult = System.Globalization.CultureInfo;
using Anolis.Packages.Utility;

namespace Anolis.Installer.Pages {
	
	public partial class UpdatePackagePage : BaseInteriorPage {
		
		private PackageUpdateInfo _updateInfo;
		
		private RateCalculator _rate = new RateCalculator();
		
		public UpdatePackagePage() {
			
			InitializeComponent();
			
			this.PageLoad   += new EventHandler(UpdatePackagePage_PageLoad);
			
			this.__downloadInfo.Click += new EventHandler(__downloadInfo_Click);
			this.__downloadYes .Click += new EventHandler(__downloadYes_Click);
			this.__downloadNo  .Click += new EventHandler(__downloadNo_Click);
			
			this.__bw.DoWork += new DoWorkEventHandler(__bw_DoWork);
			this.__bw.ProgressChanged += new ProgressChangedEventHandler(__bw_ProgressChanged);
		}
		
		protected override String LocalizePrefix { get { return "C_C"; } }
		
		protected override void Localize() {
			base.Localize();
			
			if( InstallerResources.IsCustomized ) {
				
				__downloadInfo.Text = InstallerResources.GetString("C_C_downloadInfo_Cus", InstallerResources.CustomizedSettings.InstallerName);
			}
			
		}
		
		private void UpdatePackagePage_PageLoad(object sender, EventArgs e) {
			
			if( (InstallerResources.IsCustomized && InstallerResources.CustomizedSettings.DisableUpdateCheck ) || ( PackageInfo.Package.UpdateUri == null ) ) {
				
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
			
			__sfd.Title = InstallerResources.GetString("C_C_sfdTitle");
			
			if(__sfd.ShowDialog(this) == DialogResult.OK) {
				
				__downloadYes.Enabled = false;
				__downloadNo .Enabled = false;
				WizardForm.EnableBack = false;
				WizardForm.EnableNext = false;
				
				
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
			
			_rate.Reset(0);
			
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
			
			WizardForm.EnableBack = false;
			WizardForm.EnableNext = true;
			
		}
		
		private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
			
			_rate.Add( e.BytesReceived );
			
			Int64 xferRate = _rate.GetRate();
			
			String message = InstallerResources.GetString("C_C_downloadProgress");
			message = String.Format(Cult.CurrentCulture, message, e.ProgressPercentage, e.BytesReceived / 1024, e.TotalBytesToReceive / 1024, xferRate);
			
			BeginInvoke( new MethodInvoker( delegate() {
				
				__progress.Value = e.ProgressPercentage;
				__statusLbl.Text = message;
				
			} ) );
			
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
		
		public override BaseWizardPage PrevPage {
			get {
				
				if( InstallerResources.IsCustomized && InstallerResources.CustomizedSettings.SimpleUI ) {
					
					return Program.PageBMainAction;
					
				} else {
					return Program.PageCASelectPackage;
				}
			}
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
