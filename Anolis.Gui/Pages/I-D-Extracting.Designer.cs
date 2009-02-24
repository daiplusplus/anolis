namespace Anolis.Gui.Pages {
	partial class ExtractingPage {
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
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__progress = new System.Windows.Forms.ProgressBar();
			this.@__statusLabel = new System.Windows.Forms.Label();
			this.@__banner.SuspendLayout();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			this.@__banner.Controls.SetChildIndex(this.@__bannerTitle, 0);
			this.@__banner.Controls.SetChildIndex(this.@__bannerSubtitle, 0);
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__statusLabel);
			this.@__content.Controls.Add(this.@__progress);
			// 
			// __bannerSubtitle
			// 
			this.@__bannerSubtitle.Text = "Please wait whilst the installer prepares the package for installation";
			// 
			// __bannerTitle
			// 
			this.@__bannerTitle.Text = "Preparing Package";
			// 
			// __ofd
			// 
			this.@__ofd.Filter = "Anolis Package (*.anop)|*.anop|All files (*.*)|*.*";
			this.@__ofd.Title = "Select Anolis Package File";
			// 
			// __progress
			// 
			this.@__progress.Location = new System.Drawing.Point(22, 54);
			this.@__progress.Name = "__progress";
			this.@__progress.Size = new System.Drawing.Size(413, 23);
			this.@__progress.TabIndex = 0;
			// 
			// __statusLabel
			// 
			this.@__statusLabel.AutoSize = true;
			this.@__statusLabel.Location = new System.Drawing.Point(19, 80);
			this.@__statusLabel.Name = "__statusLabel";
			this.@__statusLabel.Size = new System.Drawing.Size(106, 13);
			this.@__statusLabel.TabIndex = 1;
			this.@__statusLabel.Text = "{0}% complete - {1}";
			// 
			// Extracting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "Extracting";
			this.@__banner.ResumeLayout(false);
			this.@__content.ResumeLayout(false);
			this.@__content.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.Label __statusLabel;
		private System.Windows.Forms.ProgressBar __progress;
	}
}
