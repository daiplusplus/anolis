namespace Anolis.Installer.Pages {
	partial class DownloadingPage {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.@__progress = new System.Windows.Forms.ProgressBar();
			this.@__statusLbl = new System.Windows.Forms.Label();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__statusLbl);
			this.@__content.Controls.Add(this.@__progress);
			// 
			// __progress
			// 
			this.@__progress.Location = new System.Drawing.Point(22, 54);
			this.@__progress.Name = "__progress";
			this.@__progress.Size = new System.Drawing.Size(419, 23);
			this.@__progress.TabIndex = 0;
			// 
			// __statusLbl
			// 
			this.@__statusLbl.AutoSize = true;
			this.@__statusLbl.Location = new System.Drawing.Point(19, 80);
			this.@__statusLbl.Name = "__statusLbl";
			this.@__statusLbl.Size = new System.Drawing.Size(274, 13);
			this.@__statusLbl.TabIndex = 1;
			this.@__statusLbl.Text = "{0}% completed - {1}KB remain, downloading @ {2}KBps";
			// 
			// DownloadingPage
			// 
			this.Name = "DownloadingPage";
			this.PageSubtitle = "Now downloading the Anolis program files";
			this.PageTitle = "Downloading";
			this.@__content.ResumeLayout(false);
			this.@__content.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label __statusLbl;
		private System.Windows.Forms.ProgressBar __progress;
	}
}
