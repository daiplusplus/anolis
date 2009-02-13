namespace Anolis.Gui.Pages {
	partial class SelectBackupPage {
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
			this.@__lbl = new System.Windows.Forms.Label();
			this.@__dirpath = new System.Windows.Forms.TextBox();
			this.@__browse = new System.Windows.Forms.Button();
			this.@__banner.SuspendLayout();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			this.@__banner.Controls.SetChildIndex(this.@__bannerTitle, 0);
			this.@__banner.Controls.SetChildIndex(this.@__bannerSubtitle, 0);
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__browse);
			this.@__content.Controls.Add(this.@__dirpath);
			this.@__content.Controls.Add(this.@__lbl);
			// 
			// __bannerSubtitle
			// 
			this.@__bannerSubtitle.Text = "Select the directory to recover from";
			// 
			// __bannerTitle
			// 
			this.@__bannerTitle.Text = "Select Backup Directory";
			// 
			// __lbl
			// 
			this.@__lbl.AutoSize = true;
			this.@__lbl.Location = new System.Drawing.Point(19, 59);
			this.@__lbl.Name = "__lbl";
			this.@__lbl.Size = new System.Drawing.Size(51, 13);
			this.@__lbl.TabIndex = 0;
			this.@__lbl.Text = "Directory";
			// 
			// __dirpath
			// 
			this.@__dirpath.Location = new System.Drawing.Point(76, 56);
			this.@__dirpath.Name = "__dirpath";
			this.@__dirpath.Size = new System.Drawing.Size(293, 21);
			this.@__dirpath.TabIndex = 1;
			// 
			// __browse
			// 
			this.@__browse.Location = new System.Drawing.Point(375, 54);
			this.@__browse.Name = "__browse";
			this.@__browse.Size = new System.Drawing.Size(75, 23);
			this.@__browse.TabIndex = 2;
			this.@__browse.Text = "Browse...";
			this.@__browse.UseVisualStyleBackColor = true;
			// 
			// U_SelectBackup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Name = "U_SelectBackup";
			this.@__banner.ResumeLayout(false);
			this.@__content.ResumeLayout(false);
			this.@__content.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button __browse;
		private System.Windows.Forms.TextBox __dirpath;
		private System.Windows.Forms.Label __lbl;
	}
}
