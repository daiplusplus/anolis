namespace Anolis.Packager {
	partial class TarLzmaForm {
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
			this.@__compressGrp = new System.Windows.Forms.GroupBox();
			this.@__compressRootLbl = new System.Windows.Forms.Label();
			this.@__compressRoot = new System.Windows.Forms.TextBox();
			this.@__compressRootBrowse = new System.Windows.Forms.Button();
			this.@__compress = new System.Windows.Forms.Button();
			this.@__compressRemove = new System.Windows.Forms.Button();
			this.@__compressAddDir = new System.Windows.Forms.Button();
			this.@__compressAddFile = new System.Windows.Forms.Button();
			this.@__items = new System.Windows.Forms.ListBox();
			this.@__decompressGrp = new System.Windows.Forms.GroupBox();
			this.@__decompress = new System.Windows.Forms.Button();
			this.@__decompresBrowse = new System.Windows.Forms.Button();
			this.@__decompressFilename = new System.Windows.Forms.TextBox();
			this.@__decompressLbl = new System.Windows.Forms.Label();
			this.@__fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__sfd = new System.Windows.Forms.SaveFileDialog();
			this.@__statusStrip = new System.Windows.Forms.StatusStrip();
			this.@__progress = new System.Windows.Forms.ToolStripProgressBar();
			this.@__status = new System.Windows.Forms.ToolStripStatusLabel();
			this.@__bw = new System.ComponentModel.BackgroundWorker();
			this.@__compressGrp.SuspendLayout();
			this.@__decompressGrp.SuspendLayout();
			this.@__statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// __compressGrp
			// 
			this.@__compressGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__compressGrp.Controls.Add(this.@__compressRootLbl);
			this.@__compressGrp.Controls.Add(this.@__compressRoot);
			this.@__compressGrp.Controls.Add(this.@__compressRootBrowse);
			this.@__compressGrp.Controls.Add(this.@__compress);
			this.@__compressGrp.Controls.Add(this.@__compressRemove);
			this.@__compressGrp.Controls.Add(this.@__compressAddDir);
			this.@__compressGrp.Controls.Add(this.@__compressAddFile);
			this.@__compressGrp.Controls.Add(this.@__items);
			this.@__compressGrp.Location = new System.Drawing.Point(12, 12);
			this.@__compressGrp.Name = "__compressGrp";
			this.@__compressGrp.Size = new System.Drawing.Size(509, 291);
			this.@__compressGrp.TabIndex = 0;
			this.@__compressGrp.TabStop = false;
			this.@__compressGrp.Text = "Compress";
			// 
			// __compressRootLbl
			// 
			this.@__compressRootLbl.AutoSize = true;
			this.@__compressRootLbl.Location = new System.Drawing.Point(6, 24);
			this.@__compressRootLbl.Name = "__compressRootLbl";
			this.@__compressRootLbl.Size = new System.Drawing.Size(73, 13);
			this.@__compressRootLbl.TabIndex = 7;
			this.@__compressRootLbl.Text = "Root directory";
			// 
			// __compressRoot
			// 
			this.@__compressRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__compressRoot.Location = new System.Drawing.Point(85, 21);
			this.@__compressRoot.Name = "__compressRoot";
			this.@__compressRoot.Size = new System.Drawing.Size(337, 20);
			this.@__compressRoot.TabIndex = 0;
			// 
			// __compressRootBrowse
			// 
			this.@__compressRootBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__compressRootBrowse.Location = new System.Drawing.Point(428, 19);
			this.@__compressRootBrowse.Name = "__compressRootBrowse";
			this.@__compressRootBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__compressRootBrowse.TabIndex = 1;
			this.@__compressRootBrowse.Text = "Browse...";
			this.@__compressRootBrowse.UseVisualStyleBackColor = true;
			// 
			// __compress
			// 
			this.@__compress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__compress.Location = new System.Drawing.Point(387, 262);
			this.@__compress.Name = "__compress";
			this.@__compress.Size = new System.Drawing.Size(116, 23);
			this.@__compress.TabIndex = 6;
			this.@__compress.Text = "Compress";
			this.@__compress.UseVisualStyleBackColor = true;
			// 
			// __compressRemove
			// 
			this.@__compressRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__compressRemove.Location = new System.Drawing.Point(189, 262);
			this.@__compressRemove.Name = "__compressRemove";
			this.@__compressRemove.Size = new System.Drawing.Size(75, 23);
			this.@__compressRemove.TabIndex = 5;
			this.@__compressRemove.Text = "Remove";
			this.@__compressRemove.UseVisualStyleBackColor = true;
			// 
			// __compressAddDir
			// 
			this.@__compressAddDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__compressAddDir.Location = new System.Drawing.Point(87, 262);
			this.@__compressAddDir.Name = "__compressAddDir";
			this.@__compressAddDir.Size = new System.Drawing.Size(96, 23);
			this.@__compressAddDir.TabIndex = 4;
			this.@__compressAddDir.Text = "Add Directory";
			this.@__compressAddDir.UseVisualStyleBackColor = true;
			// 
			// __compressAddFile
			// 
			this.@__compressAddFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__compressAddFile.Location = new System.Drawing.Point(6, 262);
			this.@__compressAddFile.Name = "__compressAddFile";
			this.@__compressAddFile.Size = new System.Drawing.Size(75, 23);
			this.@__compressAddFile.TabIndex = 3;
			this.@__compressAddFile.Text = "Add File";
			this.@__compressAddFile.UseVisualStyleBackColor = true;
			// 
			// __items
			// 
			this.@__items.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__items.FormattingEnabled = true;
			this.@__items.IntegralHeight = false;
			this.@__items.Location = new System.Drawing.Point(6, 48);
			this.@__items.Name = "__items";
			this.@__items.Size = new System.Drawing.Size(497, 208);
			this.@__items.TabIndex = 2;
			// 
			// __decompressGrp
			// 
			this.@__decompressGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__decompressGrp.Controls.Add(this.@__decompress);
			this.@__decompressGrp.Controls.Add(this.@__decompresBrowse);
			this.@__decompressGrp.Controls.Add(this.@__decompressFilename);
			this.@__decompressGrp.Controls.Add(this.@__decompressLbl);
			this.@__decompressGrp.Location = new System.Drawing.Point(12, 309);
			this.@__decompressGrp.Name = "__decompressGrp";
			this.@__decompressGrp.Size = new System.Drawing.Size(509, 84);
			this.@__decompressGrp.TabIndex = 1;
			this.@__decompressGrp.TabStop = false;
			this.@__decompressGrp.Text = "Decompress";
			// 
			// __decompress
			// 
			this.@__decompress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__decompress.Location = new System.Drawing.Point(387, 48);
			this.@__decompress.Name = "__decompress";
			this.@__decompress.Size = new System.Drawing.Size(116, 23);
			this.@__decompress.TabIndex = 2;
			this.@__decompress.Text = "Decompress";
			this.@__decompress.UseVisualStyleBackColor = true;
			// 
			// __decompresBrowse
			// 
			this.@__decompresBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__decompresBrowse.Location = new System.Drawing.Point(428, 19);
			this.@__decompresBrowse.Name = "__decompresBrowse";
			this.@__decompresBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__decompresBrowse.TabIndex = 1;
			this.@__decompresBrowse.Text = "Browse...";
			this.@__decompresBrowse.UseVisualStyleBackColor = true;
			// 
			// __decompressFilename
			// 
			this.@__decompressFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__decompressFilename.Location = new System.Drawing.Point(44, 21);
			this.@__decompressFilename.Name = "__decompressFilename";
			this.@__decompressFilename.Size = new System.Drawing.Size(378, 20);
			this.@__decompressFilename.TabIndex = 0;
			// 
			// __decompressLbl
			// 
			this.@__decompressLbl.AutoSize = true;
			this.@__decompressLbl.Location = new System.Drawing.Point(15, 24);
			this.@__decompressLbl.Name = "__decompressLbl";
			this.@__decompressLbl.Size = new System.Drawing.Size(23, 13);
			this.@__decompressLbl.TabIndex = 0;
			this.@__decompressLbl.Text = "File";
			// 
			// __ofd
			// 
			this.@__ofd.Filter = "LZMA Tar (*.lzma)|*.lzma|Tarball (*.tar)|*.tar|All files (*.*)|*.*";
			// 
			// __sfd
			// 
			this.@__sfd.Filter = "LZMA Tar (*.lzma)|*.lzma|Tarball (*.tar)|*.tar|All files (*.*)|*.*";
			// 
			// __statusStrip
			// 
			this.@__statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__progress,
            this.@__status});
			this.@__statusStrip.Location = new System.Drawing.Point(0, 401);
			this.@__statusStrip.Name = "__statusStrip";
			this.@__statusStrip.Size = new System.Drawing.Size(533, 22);
			this.@__statusStrip.TabIndex = 3;
			this.@__statusStrip.Text = "statusStrip1";
			// 
			// __progress
			// 
			this.@__progress.Name = "__progress";
			this.@__progress.Size = new System.Drawing.Size(250, 16);
			// 
			// __status
			// 
			this.@__status.Name = "__status";
			this.@__status.Size = new System.Drawing.Size(38, 17);
			this.@__status.Text = "Ready";
			// 
			// TarLzmaForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(533, 423);
			this.Controls.Add(this.@__statusStrip);
			this.Controls.Add(this.@__decompressGrp);
			this.Controls.Add(this.@__compressGrp);
			this.Name = "TarLzmaForm";
			this.Text = "LZMA Tarball Utility";
			this.@__compressGrp.ResumeLayout(false);
			this.@__compressGrp.PerformLayout();
			this.@__decompressGrp.ResumeLayout(false);
			this.@__decompressGrp.PerformLayout();
			this.@__statusStrip.ResumeLayout(false);
			this.@__statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox __compressGrp;
		private System.Windows.Forms.GroupBox __decompressGrp;
		private System.Windows.Forms.Button __compress;
		private System.Windows.Forms.Button __compressRemove;
		private System.Windows.Forms.Button __compressAddDir;
		private System.Windows.Forms.Button __compressAddFile;
		private System.Windows.Forms.ListBox __items;
		private System.Windows.Forms.Button __decompress;
		private System.Windows.Forms.Button __decompresBrowse;
		private System.Windows.Forms.TextBox __decompressFilename;
		private System.Windows.Forms.Label __decompressLbl;
		private System.Windows.Forms.Label __compressRootLbl;
		private System.Windows.Forms.TextBox __compressRoot;
		private System.Windows.Forms.Button __compressRootBrowse;
		private System.Windows.Forms.FolderBrowserDialog __fbd;
		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.SaveFileDialog __sfd;
		private System.Windows.Forms.StatusStrip __statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel __status;
		private System.Windows.Forms.ToolStripProgressBar __progress;
		private System.ComponentModel.BackgroundWorker __bw;
	}
}