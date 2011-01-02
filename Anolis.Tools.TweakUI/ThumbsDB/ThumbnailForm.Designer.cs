namespace Anolis.Tools.TweakUI.ThumbsDB {
	partial class ThumbnailForm {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.@__thumbs = new System.Windows.Forms.FlowLayoutPanel();
			this.@__thumbPathLbl = new System.Windows.Forms.Label();
			this.@__thumbPath = new System.Windows.Forms.TextBox();
			this.@__thumbPathLoad = new System.Windows.Forms.Button();
			this.@__thumbPathBrowse = new System.Windows.Forms.Button();
			this.@__export = new System.Windows.Forms.Button();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__sfd = new System.Windows.Forms.SaveFileDialog();
			this.@__credit = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// __thumbs
			// 
			this.@__thumbs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__thumbs.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.@__thumbs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.@__thumbs.Location = new System.Drawing.Point(12, 32);
			this.@__thumbs.Name = "__thumbs";
			this.@__thumbs.Size = new System.Drawing.Size(383, 181);
			this.@__thumbs.TabIndex = 0;
			// 
			// __thumbPathLbl
			// 
			this.@__thumbPathLbl.AutoSize = true;
			this.@__thumbPathLbl.Location = new System.Drawing.Point(12, 9);
			this.@__thumbPathLbl.Name = "__thumbPathLbl";
			this.@__thumbPathLbl.Size = new System.Drawing.Size(79, 13);
			this.@__thumbPathLbl.TabIndex = 1;
			this.@__thumbPathLbl.Text = "Thumbs.db File";
			// 
			// __thumbPath
			// 
			this.@__thumbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__thumbPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.@__thumbPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.@__thumbPath.Location = new System.Drawing.Point(97, 6);
			this.@__thumbPath.Name = "__thumbPath";
			this.@__thumbPath.Size = new System.Drawing.Size(136, 20);
			this.@__thumbPath.TabIndex = 2;
			// 
			// __thumbPathLoad
			// 
			this.@__thumbPathLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__thumbPathLoad.Location = new System.Drawing.Point(320, 4);
			this.@__thumbPathLoad.Name = "__thumbPathLoad";
			this.@__thumbPathLoad.Size = new System.Drawing.Size(75, 23);
			this.@__thumbPathLoad.TabIndex = 3;
			this.@__thumbPathLoad.Text = "Load";
			this.@__thumbPathLoad.UseVisualStyleBackColor = true;
			// 
			// __thumbPathBrowse
			// 
			this.@__thumbPathBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__thumbPathBrowse.Location = new System.Drawing.Point(239, 4);
			this.@__thumbPathBrowse.Name = "__thumbPathBrowse";
			this.@__thumbPathBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__thumbPathBrowse.TabIndex = 4;
			this.@__thumbPathBrowse.Text = "Browse...";
			this.@__thumbPathBrowse.UseVisualStyleBackColor = true;
			// 
			// __export
			// 
			this.@__export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__export.Location = new System.Drawing.Point(281, 219);
			this.@__export.Name = "__export";
			this.@__export.Size = new System.Drawing.Size(114, 23);
			this.@__export.TabIndex = 5;
			this.@__export.Text = "Export Selected...";
			this.@__export.UseVisualStyleBackColor = true;
			// 
			// __ofd
			// 
			this.@__ofd.Filter = "Thumbnail Archives (thumbs.db)|thumbs.db";
			// 
			// __credit
			// 
			this.@__credit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__credit.AutoSize = true;
			this.@__credit.Location = new System.Drawing.Point(12, 224);
			this.@__credit.Name = "__credit";
			this.@__credit.Size = new System.Drawing.Size(257, 13);
			this.@__credit.TabIndex = 6;
			this.@__credit.TabStop = true;
			this.@__credit.Text = "Uses ThumbDBLib by Pete Davis with Klaus Weisser";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(407, 254);
			this.Controls.Add(this.@__credit);
			this.Controls.Add(this.@__export);
			this.Controls.Add(this.@__thumbPathBrowse);
			this.Controls.Add(this.@__thumbPathLoad);
			this.Controls.Add(this.@__thumbPath);
			this.Controls.Add(this.@__thumbPathLbl);
			this.Controls.Add(this.@__thumbs);
			this.MinimumSize = new System.Drawing.Size(415, 288);
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Thumbs.db Viewer";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel __thumbs;
		private System.Windows.Forms.Label __thumbPathLbl;
		private System.Windows.Forms.TextBox __thumbPath;
		private System.Windows.Forms.Button __thumbPathLoad;
		private System.Windows.Forms.Button __thumbPathBrowse;
		private System.Windows.Forms.Button __export;
		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.SaveFileDialog __sfd;
		private System.Windows.Forms.LinkLabel __credit;
	}
}

