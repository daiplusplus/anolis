using System;
using System.IO;
using System.Net;

using W3b.Wizards.WindowsForms;
using System.Windows.Forms;
using Anolis.Core.Packages;

namespace Anolis.Installer.Pages {
	
	public partial class DownloadingPage : BaseInteriorPage {
		
		
		private WebClient _client;
		
		public DownloadingPage() {
			InitializeComponent();
			
			_client = new WebClient();
			_client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(_client_DownloadFileCompleted);
			_client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(_client_DownloadProgressChanged);
			
			this.Load += new EventHandler(DownloadingPage_Load);
			
			Localize();
		}
		
		protected override String LocalizePrefix { get { return "D_B"; } }
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageDADestination; }
		}
		
		private void DownloadingPage_Load(object sender, EventArgs e) {
			
			BeginDownload();
			
		}
		
		private void BeginDownload() {
			
			// get the info about the tools first
			
			String info = _client.DownloadString( ToolsInfo.ToolsInfoUri );
			String[] infoLines = info.Replace("\r\n", "\n").Split('\n');
			
			String ver = infoLines[0].Replace('.', '-');
			Uri    src = new Uri( infoLines[1] );
			String dst = Path.Combine( ToolsInfo.DestinationDirectory, "Tools-" + ver  + ".anop" );
			
			_client.DownloadFileAsync( src, dst, dst );
			
		}
		
		private void _client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
			
			this.BeginInvoke( new MethodInvoker( delegate() {
				
				__progress.Value = e.ProgressPercentage;
				
			} ) );
			
		}
		
		private void _client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {
			
			String filename = e.UserState as String;
			
			// decompress
			using(FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read)) {
				
				
				
			}
			
			
			
			
		}
		
		
	}
}
