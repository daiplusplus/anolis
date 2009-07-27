namespace Anolis.Tools.DuplicateFiles {
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
			this.@__rootLbl = new System.Windows.Forms.Label();
			this.@__root = new System.Windows.Forms.TextBox();
			this.@__process = new System.Windows.Forms.Button();
			this.@__rootBrowse = new System.Windows.Forms.Button();
			this.@__excludeLbl = new System.Windows.Forms.Label();
			this.@__exclude = new System.Windows.Forms.ListBox();
			this.@__excludeAdd = new System.Windows.Forms.Button();
			this.@__excludeRemove = new System.Windows.Forms.Button();
			this.@__results = new System.Windows.Forms.ListView();
			this.@__colFileName = new System.Windows.Forms.ColumnHeader();
			this.@__progress = new System.Windows.Forms.ProgressBar();
			this.@__progressLbl = new System.Windows.Forms.Label();
			this.@__close = new System.Windows.Forms.Button();
			this.@__delete = new System.Windows.Forms.Button();
			this.@__fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.@__bw = new System.ComponentModel.BackgroundWorker();
			this.@__progressMessage = new System.Windows.Forms.Label();
			this.@__totalWastedSpace = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// __rootLbl
			// 
			this.@__rootLbl.Location = new System.Drawing.Point(15, 9);
			this.@__rootLbl.Name = "__rootLbl";
			this.@__rootLbl.Size = new System.Drawing.Size(94, 18);
			this.@__rootLbl.TabIndex = 0;
			this.@__rootLbl.Text = "Root Directory";
			this.@__rootLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __root
			// 
			this.@__root.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__root.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.@__root.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.@__root.Location = new System.Drawing.Point(115, 6);
			this.@__root.Name = "__root";
			this.@__root.Size = new System.Drawing.Size(446, 20);
			this.@__root.TabIndex = 1;
			// 
			// __process
			// 
			this.@__process.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__process.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__process.Location = new System.Drawing.Point(567, 103);
			this.@__process.Name = "__process";
			this.@__process.Size = new System.Drawing.Size(75, 23);
			this.@__process.TabIndex = 9;
			this.@__process.Text = "Search";
			this.@__process.UseVisualStyleBackColor = true;
			// 
			// __rootBrowse
			// 
			this.@__rootBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__rootBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__rootBrowse.Location = new System.Drawing.Point(567, 4);
			this.@__rootBrowse.Name = "__rootBrowse";
			this.@__rootBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__rootBrowse.TabIndex = 2;
			this.@__rootBrowse.Text = "Browse...";
			this.@__rootBrowse.UseVisualStyleBackColor = true;
			// 
			// __excludeLbl
			// 
			this.@__excludeLbl.Location = new System.Drawing.Point(12, 35);
			this.@__excludeLbl.Name = "__excludeLbl";
			this.@__excludeLbl.Size = new System.Drawing.Size(100, 23);
			this.@__excludeLbl.TabIndex = 3;
			this.@__excludeLbl.Text = "Exclude Directories";
			this.@__excludeLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __exclude
			// 
			this.@__exclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__exclude.FormattingEnabled = true;
			this.@__exclude.Location = new System.Drawing.Point(115, 32);
			this.@__exclude.Name = "__exclude";
			this.@__exclude.Size = new System.Drawing.Size(446, 56);
			this.@__exclude.TabIndex = 4;
			// 
			// __excludeAdd
			// 
			this.@__excludeAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__excludeAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__excludeAdd.Location = new System.Drawing.Point(567, 31);
			this.@__excludeAdd.Name = "__excludeAdd";
			this.@__excludeAdd.Size = new System.Drawing.Size(75, 23);
			this.@__excludeAdd.TabIndex = 5;
			this.@__excludeAdd.Text = "Add...";
			this.@__excludeAdd.UseVisualStyleBackColor = true;
			// 
			// __excludeRemove
			// 
			this.@__excludeRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__excludeRemove.Enabled = false;
			this.@__excludeRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__excludeRemove.Location = new System.Drawing.Point(567, 60);
			this.@__excludeRemove.Name = "__excludeRemove";
			this.@__excludeRemove.Size = new System.Drawing.Size(75, 23);
			this.@__excludeRemove.TabIndex = 6;
			this.@__excludeRemove.Text = "Remove";
			this.@__excludeRemove.UseVisualStyleBackColor = true;
			// 
			// __results
			// 
			this.@__results.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.@__colFileName});
			this.@__results.Location = new System.Drawing.Point(12, 152);
			this.@__results.Name = "__results";
			this.@__results.Size = new System.Drawing.Size(630, 300);
			this.@__results.TabIndex = 10;
			this.@__results.UseCompatibleStateImageBehavior = false;
			this.@__results.View = System.Windows.Forms.View.Details;
			// 
			// __colFileName
			// 
			this.@__colFileName.Text = "Path";
			this.@__colFileName.Width = 274;
			// 
			// __progress
			// 
			this.@__progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__progress.Location = new System.Drawing.Point(115, 108);
			this.@__progress.Name = "__progress";
			this.@__progress.Size = new System.Drawing.Size(446, 18);
			this.@__progress.TabIndex = 8;
			// 
			// __progressLbl
			// 
			this.@__progressLbl.AutoSize = true;
			this.@__progressLbl.Location = new System.Drawing.Point(61, 108);
			this.@__progressLbl.Name = "__progressLbl";
			this.@__progressLbl.Size = new System.Drawing.Size(48, 13);
			this.@__progressLbl.TabIndex = 7;
			this.@__progressLbl.Text = "Progress";
			this.@__progressLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __close
			// 
			this.@__close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__close.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.@__close.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__close.Location = new System.Drawing.Point(567, 458);
			this.@__close.Name = "__close";
			this.@__close.Size = new System.Drawing.Size(75, 23);
			this.@__close.TabIndex = 12;
			this.@__close.Text = "Close";
			this.@__close.UseVisualStyleBackColor = true;
			// 
			// __delete
			// 
			this.@__delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__delete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__delete.Location = new System.Drawing.Point(12, 458);
			this.@__delete.Name = "__delete";
			this.@__delete.Size = new System.Drawing.Size(123, 23);
			this.@__delete.TabIndex = 11;
			this.@__delete.Text = "Delete Duplicates";
			this.@__delete.UseVisualStyleBackColor = true;
			// 
			// __fbd
			// 
			this.@__fbd.ShowNewFolderButton = false;
			// 
			// __bw
			// 
			this.@__bw.WorkerReportsProgress = true;
			this.@__bw.WorkerSupportsCancellation = true;
			// 
			// __progressMessage
			// 
			this.@__progressMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__progressMessage.Location = new System.Drawing.Point(112, 129);
			this.@__progressMessage.Name = "__progressMessage";
			this.@__progressMessage.Size = new System.Drawing.Size(530, 20);
			this.@__progressMessage.TabIndex = 13;
			// 
			// __totalWastedSpace
			// 
			this.@__totalWastedSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__totalWastedSpace.AutoSize = true;
			this.@__totalWastedSpace.Location = new System.Drawing.Point(141, 463);
			this.@__totalWastedSpace.Name = "__totalWastedSpace";
			this.@__totalWastedSpace.Size = new System.Drawing.Size(133, 13);
			this.@__totalWastedSpace.TabIndex = 14;
			this.@__totalWastedSpace.Text = "Total Wasted Space: 0MB";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.@__close;
			this.ClientSize = new System.Drawing.Size(654, 493);
			this.Controls.Add(this.@__totalWastedSpace);
			this.Controls.Add(this.@__progressMessage);
			this.Controls.Add(this.@__delete);
			this.Controls.Add(this.@__close);
			this.Controls.Add(this.@__progressLbl);
			this.Controls.Add(this.@__progress);
			this.Controls.Add(this.@__results);
			this.Controls.Add(this.@__excludeRemove);
			this.Controls.Add(this.@__excludeAdd);
			this.Controls.Add(this.@__exclude);
			this.Controls.Add(this.@__excludeLbl);
			this.Controls.Add(this.@__rootBrowse);
			this.Controls.Add(this.@__process);
			this.Controls.Add(this.@__root);
			this.Controls.Add(this.@__rootLbl);
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Duplicate and Identical File Identifier";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label __rootLbl;
		private System.Windows.Forms.TextBox __root;
		private System.Windows.Forms.Button __process;
		private System.Windows.Forms.Button __rootBrowse;
		private System.Windows.Forms.Label __excludeLbl;
		private System.Windows.Forms.ListBox __exclude;
		private System.Windows.Forms.Button __excludeAdd;
		private System.Windows.Forms.Button __excludeRemove;
		private System.Windows.Forms.ListView __results;
		private System.Windows.Forms.ProgressBar __progress;
		private System.Windows.Forms.Label __progressLbl;
		private System.Windows.Forms.Button __close;
		private System.Windows.Forms.Button __delete;
		private System.Windows.Forms.FolderBrowserDialog __fbd;
		private System.ComponentModel.BackgroundWorker __bw;
		private System.Windows.Forms.Label __progressMessage;
		private System.Windows.Forms.ColumnHeader __colFileName;
		private System.Windows.Forms.Label __totalWastedSpace;
	}
}

