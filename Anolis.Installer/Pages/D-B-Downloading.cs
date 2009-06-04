using System;
using System.IO;
using System.Net;

using W3b.Wizards.WindowsForms;

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
			
			_client.DownloadFileAsync( ToolsInfo.ToolsInfoUri, Path.Combine( ToolsInfo.DestinationDirectory, "Tools.zip" ) );
			
		}
		
		private void _client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
			
		}
		
		private void _client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {
			
		}
		
		
	}
}
