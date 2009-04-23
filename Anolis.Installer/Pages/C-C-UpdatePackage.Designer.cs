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
			this.@__fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.@__bw = new System.ComponentModel.BackgroundWorker();
			this.@__statusLbl = new System.Windows.Forms.Label();
			this.@__progress = new System.Windows.Forms.ProgressBar();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__statusLbl);
			this.@__content.Controls.Add(this.@__progress);
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
			this.@__progress.Location = new System.Drawing.Point(22, 54);
			this.@__progress.Name = "__progress";
			this.@__progress.Size = new System.Drawing.Size(413, 23);
			this.@__progress.TabIndex = 2;
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

		private System.Windows.Forms.FolderBrowserDialog __fbd;
		private System.ComponentModel.BackgroundWorker __bw;
		private System.Windows.Forms.Label __statusLbl;
		private System.Windows.Forms.ProgressBar __progress;

	}
}
