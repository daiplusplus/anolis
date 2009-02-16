namespace Anolis.Packager {
	partial class MainForm {
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
			this.@__decompressGrp = new System.Windows.Forms.GroupBox();
			this.@__items = new System.Windows.Forms.ListBox();
			this.@__compFile = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.@__compress = new System.Windows.Forms.Button();
			this.@__decompressLbl = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.@__decompresBrowse = new System.Windows.Forms.Button();
			this.@__decompress = new System.Windows.Forms.Button();
			this.@__progressGrp = new System.Windows.Forms.GroupBox();
			this.@__progress = new System.Windows.Forms.ProgressBar();
			this.@__progressStatus = new System.Windows.Forms.Label();
			this.@__compressGrp.SuspendLayout();
			this.@__decompressGrp.SuspendLayout();
			this.@__progressGrp.SuspendLayout();
			this.SuspendLayout();
			// 
			// __compressGrp
			// 
			this.@__compressGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__compressGrp.Controls.Add(this.@__compress);
			this.@__compressGrp.Controls.Add(this.button3);
			this.@__compressGrp.Controls.Add(this.button2);
			this.@__compressGrp.Controls.Add(this.@__compFile);
			this.@__compressGrp.Controls.Add(this.@__items);
			this.@__compressGrp.Location = new System.Drawing.Point(12, 12);
			this.@__compressGrp.Name = "__compressGrp";
			this.@__compressGrp.Size = new System.Drawing.Size(517, 161);
			this.@__compressGrp.TabIndex = 0;
			this.@__compressGrp.TabStop = false;
			this.@__compressGrp.Text = "Compress";
			// 
			// __decompressGrp
			// 
			this.@__decompressGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__decompressGrp.Controls.Add(this.@__decompress);
			this.@__decompressGrp.Controls.Add(this.@__decompresBrowse);
			this.@__decompressGrp.Controls.Add(this.textBox1);
			this.@__decompressGrp.Controls.Add(this.@__decompressLbl);
			this.@__decompressGrp.Location = new System.Drawing.Point(12, 179);
			this.@__decompressGrp.Name = "__decompressGrp";
			this.@__decompressGrp.Size = new System.Drawing.Size(517, 80);
			this.@__decompressGrp.TabIndex = 1;
			this.@__decompressGrp.TabStop = false;
			this.@__decompressGrp.Text = "Decompress";
			// 
			// __items
			// 
			this.@__items.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__items.FormattingEnabled = true;
			this.@__items.IntegralHeight = false;
			this.@__items.Location = new System.Drawing.Point(6, 19);
			this.@__items.Name = "__items";
			this.@__items.Size = new System.Drawing.Size(505, 107);
			this.@__items.TabIndex = 0;
			// 
			// __compFile
			// 
			this.@__compFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__compFile.Location = new System.Drawing.Point(6, 132);
			this.@__compFile.Name = "__compFile";
			this.@__compFile.Size = new System.Drawing.Size(75, 23);
			this.@__compFile.TabIndex = 1;
			this.@__compFile.Text = "Add File";
			this.@__compFile.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.Location = new System.Drawing.Point(87, 132);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Add Directory";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button3.Location = new System.Drawing.Point(189, 132);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 3;
			this.button3.Text = "Remove";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// __compress
			// 
			this.@__compress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__compress.Location = new System.Drawing.Point(395, 132);
			this.@__compress.Name = "__compress";
			this.@__compress.Size = new System.Drawing.Size(116, 23);
			this.@__compress.TabIndex = 4;
			this.@__compress.Text = "Compress";
			this.@__compress.UseVisualStyleBackColor = true;
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
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(44, 21);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(386, 20);
			this.textBox1.TabIndex = 1;
			// 
			// __decompresBrowse
			// 
			this.@__decompresBrowse.Location = new System.Drawing.Point(436, 19);
			this.@__decompresBrowse.Name = "__decompresBrowse";
			this.@__decompresBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__decompresBrowse.TabIndex = 2;
			this.@__decompresBrowse.Text = "Browse...";
			this.@__decompresBrowse.UseVisualStyleBackColor = true;
			// 
			// __decompress
			// 
			this.@__decompress.Location = new System.Drawing.Point(395, 48);
			this.@__decompress.Name = "__decompress";
			this.@__decompress.Size = new System.Drawing.Size(116, 23);
			this.@__decompress.TabIndex = 3;
			this.@__decompress.Text = "Decompress";
			this.@__decompress.UseVisualStyleBackColor = true;
			// 
			// __progressGrp
			// 
			this.@__progressGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__progressGrp.Controls.Add(this.@__progressStatus);
			this.@__progressGrp.Controls.Add(this.@__progress);
			this.@__progressGrp.Location = new System.Drawing.Point(12, 265);
			this.@__progressGrp.Name = "__progressGrp";
			this.@__progressGrp.Size = new System.Drawing.Size(517, 85);
			this.@__progressGrp.TabIndex = 2;
			this.@__progressGrp.TabStop = false;
			this.@__progressGrp.Text = "Progress";
			// 
			// __progress
			// 
			this.@__progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__progress.Location = new System.Drawing.Point(9, 19);
			this.@__progress.Name = "__progress";
			this.@__progress.Size = new System.Drawing.Size(502, 23);
			this.@__progress.TabIndex = 0;
			// 
			// __progressStatus
			// 
			this.@__progressStatus.AutoSize = true;
			this.@__progressStatus.Location = new System.Drawing.Point(6, 58);
			this.@__progressStatus.Name = "__progressStatus";
			this.@__progressStatus.Size = new System.Drawing.Size(37, 13);
			this.@__progressStatus.TabIndex = 1;
			this.@__progressStatus.Text = "Status";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(541, 362);
			this.Controls.Add(this.@__progressGrp);
			this.Controls.Add(this.@__decompressGrp);
			this.Controls.Add(this.@__compressGrp);
			this.Name = "MainForm";
			this.Text = "Anolis Package Editor";
			this.@__compressGrp.ResumeLayout(false);
			this.@__decompressGrp.ResumeLayout(false);
			this.@__decompressGrp.PerformLayout();
			this.@__progressGrp.ResumeLayout(false);
			this.@__progressGrp.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox __compressGrp;
		private System.Windows.Forms.GroupBox __decompressGrp;
		private System.Windows.Forms.Button __compress;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button __compFile;
		private System.Windows.Forms.ListBox __items;
		private System.Windows.Forms.Button __decompress;
		private System.Windows.Forms.Button __decompresBrowse;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label __decompressLbl;
		private System.Windows.Forms.GroupBox __progressGrp;
		private System.Windows.Forms.Label __progressStatus;
		private System.Windows.Forms.ProgressBar __progress;
	}
}