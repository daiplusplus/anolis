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
			this.@__status = new System.Windows.Forms.Label();

			this.@__content.SuspendLayout();
			this.SuspendLayout();


			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__status);
			this.@__content.Controls.Add(this.@__progress);
			// 
			// __progress
			// 
			this.@__progress.Location = new System.Drawing.Point(13, 66);
			this.@__progress.Name = "__progress";
			this.@__progress.Size = new System.Drawing.Size(419, 23);
			this.@__progress.TabIndex = 0;
			// 
			// __status
			// 
			this.@__status.AutoSize = true;
			this.@__status.Location = new System.Drawing.Point(10, 102);
			this.@__status.Name = "__status";
			this.@__status.Size = new System.Drawing.Size(274, 13);
			this.@__status.TabIndex = 1;
			this.@__status.Text = "{0}% completed - {1}KB remain, downloading @ {2}KBps";
			// 
			// DownloadingPage
			// 
			this.Name = "DownloadingPage";
			this.PageTitle = "Downloading";
			this.Subtitle = "Now downloading the Anolis program files";

			this.@__content.ResumeLayout(false);
			this.@__content.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label __status;
		private System.Windows.Forms.ProgressBar __progress;
	}
}
