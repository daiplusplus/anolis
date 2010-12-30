using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

using Anolis.Core.Utility;
using Anolis.Packages.Utility;

using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public partial class DownloadingPage : BaseInteriorPage {
		
		private RateCalculator _rate = new RateCalculator();
		private WebClient _client;
		
		public DownloadingPage() {
			InitializeComponent();
			
			_client = new WebClient();
			_client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(_client_DownloadFileCompleted);
			_client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(_client_DownloadProgressChanged);
			_client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(_client_DownloadStringCompleted);

			this.PageLoad += new EventHandler(DownloadingPage_PageLoad);
		}
		
		protected override String LocalizePrefix { get { return "D_B"; } }
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageDADestination; }
		}
		
		private void DownloadingPage_PageLoad(object sender, EventArgs e) {
			
			_rate.Reset(0);
			
			WizardForm.EnableBack = false;
			
			_client.DownloadStringAsync( InstallationInfo.ToolsInfoUri );
		}
		
		private void _client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e) {
			
			if( e.Cancelled ) {
				
				ShowError( "Download Cancelled" );
				return;
				
			} else if( e.Error != null ) {
				
				ShowError( e.Error.Message );
				return;
			}
			
			String info = e.Result;
			String[] lines = info.Replace("\r\n", "\n").Split('\n');
			
			if( lines.Length < 2 ) {
				
				ShowError( "Not enough lines" );
				return;
			}
			
			// lines[0] == Date of last release in yyyy-MM-dd format
			// lines[1] == URL where to get latest release
			
			String date = lines[0];
			Uri    src  = new Uri( lines[1] );
			String dst  = Path.Combine( Path.GetTempPath(), "Tools-" + date  + ".tar.lzma" );
			
			_rate.Reset(0);
			
			_client.DownloadFileAsync( src, dst, dst );
		}
		
		private void _client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
			
			_rate.Add( e.BytesReceived );
			
			Int64 xferRate = _rate.GetRate();
			
			String message = InstallerResources.GetString("C_C_downloadProgress");
			message = String.Format(System.Globalization.CultureInfo.CurrentCulture, message, e.ProgressPercentage, e.BytesReceived / 1024, e.TotalBytesToReceive / 1024, xferRate);
			
			this.BeginInvoke( new MethodInvoker( delegate() {
				
				__progress.Value = e.ProgressPercentage;
				__statusLbl.Text = message;
				
			}));
			
		}
		
		private void _client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {
			
			if( e.Cancelled ) {
				
				ShowError("OperationCancelled");
				
				return;
				
			} else if( e.Error != null ) {
				
				ShowError( e.Error.Message );
				
				return;
			}
			
			String fileName = e.UserState as String;
			
			// whilst the "pure" option would be to have the tools as an Anolis package...
			// it really is easier just to dump 'em in the directory, create start menu items, and leave it as that
			
			// decompress
			using(W3b.TarLzma.TarLzmaDecoder decoder = new W3b.TarLzma.TarLzmaDecoder( fileName )) {
				
				decoder.ProgressEvent += new EventHandler<W3b.TarLzma.ProgressEventArgs>(decoder_ProgressEvent);
				
				decoder.Extract( InstallationInfo.ToolsDestination.FullName );
			}
			
			
			CreateStartMenuShortcuts();
			
			Invoke( new MethodInvoker( delegate() {
				
				WizardForm.LoadPage( Program.PageFFinished );
				
			}));
			
		}
		
		private void decoder_ProgressEvent(object sender, W3b.TarLzma.ProgressEventArgs e) {
			
			this.BeginInvoke( new MethodInvoker( delegate() {
				
				__progress.Value = e.Percentage;
				__statusLbl.Text = e.Message;
				
			}));
			
		}
		
		private void CreateStartMenuShortcuts() {
			
			if( InstallationInfo.ToolsStartMenu == InstallationInfo.StartMenu.None ) return;
			
			String smPath;
			if( InstallationInfo.ToolsStartMenu == InstallationInfo.StartMenu.AllUsers) {
				smPath = PackageUtility.ResolvePath(@"%AllUsersProfile%\Start Menu\Programs\Anolis");
			} else {
				smPath = PackageUtility.ResolvePath(@"%UserProfile%\Start Menu\Programs\Anolis");
			}
			
			DirectoryInfo startMenu = new DirectoryInfo(smPath);
			if( !startMenu.Exists ) startMenu.Create();
			
			String arLnkFn  = Path.Combine( smPath, "Resourcer.lnk" );
			String arTarget = Path.Combine( InstallationInfo.ToolsDestination.FullName, "Anolis.Resourcer.exe" );
			String arDesc   = "Extract, add and replace resources contained within applications";
			
			ShellLink.CreateShellLink( arLnkFn, arTarget, arDesc );
			
			String pkLnkFn  = Path.Combine( smPath, "Packager.lnk" );
			String pkTarget = Path.Combine( InstallationInfo.ToolsDestination.FullName, "Anolis.Packager.exe" );
			String pkDesc   = "Create Anolis Packages and distribute them with a GUI installer";
			
			ShellLink.CreateShellLink( pkLnkFn, pkTarget, pkDesc );
			
		}
		
		private void ShowError(String exceptionMessage) {
			
			Invoke( new MethodInvoker( delegate() {
				
				WizardForm.EnableBack = true;
				WizardForm.EnableNext = false;
				
				__statusLbl.Text = InstallerResources.GetString("D_B_downloadFailed");
				
				String message = InstallerResources.GetString("C_C_infoFailed") + ": " + exceptionMessage;
				
				MessageBox.Show(this, message, "Anolis Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				
			}));
			
		}
		
	}
}
