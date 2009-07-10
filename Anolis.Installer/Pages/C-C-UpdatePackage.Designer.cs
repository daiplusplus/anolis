namespace Anolis.Installer.Pages {
	partial class UpdatePackagePage {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.@__bw = new System.ComponentModel.BackgroundWorker();
			this.@__statusLbl = new System.Windows.Forms.Label();
			this.@__progress = new System.Windows.Forms.ProgressBar();
			this.@__downloadYes = new System.Windows.Forms.Button();
			this.@__downloadNo = new System.Windows.Forms.Button();
			this.@__downloadInfo = new System.Windows.Forms.Button();
			this.@__sfd = new System.Windows.Forms.SaveFileDialog();
			this.@__skipCheckLbl = new System.Windows.Forms.Label();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__skipCheckLbl);
			this.@__content.Controls.Add(this.@__downloadInfo);
			this.@__content.Controls.Add(this.@__downloadNo);
			this.@__content.Controls.Add(this.@__downloadYes);
			this.@__content.Controls.Add(this.@__statusLbl);
			this.@__content.Controls.Add(this.@__progress);
			// 
			// __bw
			// 
			this.@__bw.WorkerReportsProgress = true;
			this.@__bw.WorkerSupportsCancellation = true;
			// 
			// __statusLbl
			// 
			this.@__statusLbl.AutoSize = true;
			this.@__statusLbl.Location = new System.Drawing.Point(19, 80);
			this.@__statusLbl.Name = "__statusLbl";
			this.@__statusLbl.Size = new System.Drawing.Size(98, 13);
			this.@__statusLbl.TabIndex = 3;
			this.@__statusLbl.Text = "{0}% complete - {1}";
			// 
			// __progress
			// 
			this.@__progress.Enabled = false;
			this.@__progress.Location = new System.Drawing.Point(22, 54);
			this.@__progress.Name = "__progress";
			this.@__progress.Size = new System.Drawing.Size(413, 23);
			this.@__progress.TabIndex = 2;
			// 
			// __downloadYes
			// 
			this.@__downloadYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__downloadYes.Location = new System.Drawing.Point(279, 161);
			this.@__downloadYes.Name = "__downloadYes";
			this.@__downloadYes.Size = new System.Drawing.Size(75, 23);
			this.@__downloadYes.TabIndex = 5;
			this.@__downloadYes.Text = "Yes";
			this.@__downloadYes.UseVisualStyleBackColor = true;
			this.@__downloadYes.Visible = false;
			// 
			// __downloadNo
			// 
			this.@__downloadNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__downloadNo.Location = new System.Drawing.Point(360, 161);
			this.@__downloadNo.Name = "__downloadNo";
			this.@__downloadNo.Size = new System.Drawing.Size(75, 23);
			this.@__downloadNo.TabIndex = 6;
			this.@__downloadNo.Text = "No";
			this.@__downloadNo.UseVisualStyleBackColor = true;
			this.@__downloadNo.Visible = false;
			// 
			// __downloadInfo
			// 
			this.@__downloadInfo.AutoSize = true;
			this.@__downloadInfo.Location = new System.Drawing.Point(22, 161);
			this.@__downloadInfo.Name = "__downloadInfo";
			this.@__downloadInfo.Size = new System.Drawing.Size(163, 23);
			this.@__downloadInfo.TabIndex = 7;
			this.@__downloadInfo.Text = "Visit Package Webpage";
			this.@__downloadInfo.UseVisualStyleBackColor = true;
			this.@__downloadInfo.Visible = false;
			// 
			// __sfd
			// 
			this.@__sfd.Filter = "Anolis Package (*.anop)|*.anop";
			// 
			// __skipCheckLbl
			// 
			this.@__skipCheckLbl.Location = new System.Drawing.Point(22, 16);
			this.@__skipCheckLbl.Name = "__skipCheckLbl";
			this.@__skipCheckLbl.Size = new System.Drawing.Size(413, 35);
			this.@__skipCheckLbl.TabIndex = 8;
			this.@__skipCheckLbl.Text = "You can skip the update check by clicking the Next button at any time.";
			// 
			// UpdatePackagePage
			// 
			this.Name = "UpdatePackagePage";
			this.PageSubtitle = "{0} may have been updated since you acquired it. Would you like to check for upda" +
				"tes?";
			this.PageTitle = "Check for Package Updates";
			this.@__content.ResumeLayout(false);
			this.@__content.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.ComponentModel.BackgroundWorker __bw;
		private System.Windows.Forms.Label __statusLbl;
		private System.Windows.Forms.ProgressBar __progress;
		private System.Windows.Forms.Button __downloadNo;
		private System.Windows.Forms.Button __downloadYes;
		private System.Windows.Forms.Button __downloadInfo;
		private System.Windows.Forms.SaveFileDialog __sfd;
		private System.Windows.Forms.Label __skipCheckLbl;

	}
}
