namespace Anolis.Resourcer {
	partial class BatchProcessForm {
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
			this.components = new System.ComponentModel.Container();
			this.@__optionsGrp = new System.Windows.Forms.GroupBox();
			this.@__exportDirectoryLbl = new System.Windows.Forms.Label();
			this.@__exportDirectory = new System.Windows.Forms.TextBox();
			this.@__exportBrowse = new System.Windows.Forms.Button();
			this.@__reportEnable = new System.Windows.Forms.CheckBox();
			this.@__reportFilenameLbl = new System.Windows.Forms.Label();
			this.@__reportFilename = new System.Windows.Forms.TextBox();
			this.@__reportBrowse = new System.Windows.Forms.Button();
			this.@__exportLangs = new System.Windows.Forms.CheckBox();
			this.@__exportNonvisual = new System.Windows.Forms.CheckBox();
			this.@__exportIconSubimages = new System.Windows.Forms.CheckBox();
			this.@__sourceDirectory = new System.Windows.Forms.TextBox();
			this.@__sourceBrowse = new System.Windows.Forms.Button();
			this.@__sourceDirectoryLbl = new System.Windows.Forms.Label();
			this.@__sourceRecurse = new System.Windows.Forms.CheckBox();
			this.@__sourceGrp = new System.Windows.Forms.GroupBox();
			this.@__sourceFilterLbl = new System.Windows.Forms.Label();
			this.@__sourceFilter = new System.Windows.Forms.TextBox();
			this.@__process = new System.Windows.Forms.Button();
			this.@__progGrp = new System.Windows.Forms.GroupBox();
			this.@__progSourceLbl = new System.Windows.Forms.Label();
			this.@__progSource = new System.Windows.Forms.ProgressBar();
			this.@__progOverallLbl = new System.Windows.Forms.Label();
			this.@__progOverall = new System.Windows.Forms.ProgressBar();
			this.@__cancel = new System.Windows.Forms.Button();
			this.@__fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.@__sfd = new System.Windows.Forms.SaveFileDialog();
			this.@__error = new System.Windows.Forms.ErrorProvider(this.components);
			this.@__bw = new System.ComponentModel.BackgroundWorker();
			this.@__optionsGrp.SuspendLayout();
			this.@__sourceGrp.SuspendLayout();
			this.@__progGrp.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.@__error)).BeginInit();
			this.SuspendLayout();
			// 
			// __optionsGrp
			// 
			this.@__optionsGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__optionsGrp.Controls.Add(this.@__exportDirectoryLbl);
			this.@__optionsGrp.Controls.Add(this.@__exportDirectory);
			this.@__optionsGrp.Controls.Add(this.@__exportBrowse);
			this.@__optionsGrp.Controls.Add(this.@__reportEnable);
			this.@__optionsGrp.Controls.Add(this.@__reportFilenameLbl);
			this.@__optionsGrp.Controls.Add(this.@__reportFilename);
			this.@__optionsGrp.Controls.Add(this.@__reportBrowse);
			this.@__optionsGrp.Controls.Add(this.@__exportLangs);
			this.@__optionsGrp.Controls.Add(this.@__exportNonvisual);
			this.@__optionsGrp.Controls.Add(this.@__exportIconSubimages);
			this.@__optionsGrp.Location = new System.Drawing.Point(12, 114);
			this.@__optionsGrp.Name = "__optionsGrp";
			this.@__optionsGrp.Size = new System.Drawing.Size(576, 164);
			this.@__optionsGrp.TabIndex = 1;
			this.@__optionsGrp.TabStop = false;
			this.@__optionsGrp.Text = "Export Options";
			// 
			// __exportDirectoryLbl
			// 
			this.@__exportDirectoryLbl.Location = new System.Drawing.Point(6, 22);
			this.@__exportDirectoryLbl.Name = "__exportDirectoryLbl";
			this.@__exportDirectoryLbl.Size = new System.Drawing.Size(104, 20);
			this.@__exportDirectoryLbl.TabIndex = 13;
			this.@__exportDirectoryLbl.Text = "Export Directory";
			this.@__exportDirectoryLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __exportDirectory
			// 
			this.@__exportDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__exportDirectory.Location = new System.Drawing.Point(116, 19);
			this.@__exportDirectory.Name = "__exportDirectory";
			this.@__exportDirectory.Size = new System.Drawing.Size(359, 20);
			this.@__exportDirectory.TabIndex = 0;
			// 
			// __exportBrowse
			// 
			this.@__exportBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__exportBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__exportBrowse.Location = new System.Drawing.Point(495, 17);
			this.@__exportBrowse.Name = "__exportBrowse";
			this.@__exportBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__exportBrowse.TabIndex = 1;
			this.@__exportBrowse.Text = "Browse...";
			this.@__exportBrowse.UseVisualStyleBackColor = true;
			// 
			// __reportEnable
			// 
			this.@__reportEnable.AutoSize = true;
			this.@__reportEnable.Enabled = false;
			this.@__reportEnable.Location = new System.Drawing.Point(116, 45);
			this.@__reportEnable.Name = "__reportEnable";
			this.@__reportEnable.Size = new System.Drawing.Size(155, 17);
			this.@__reportEnable.TabIndex = 2;
			this.@__reportEnable.Text = "Generate Report Webpage";
			this.@__reportEnable.UseVisualStyleBackColor = true;
			// 
			// __reportFilenameLbl
			// 
			this.@__reportFilenameLbl.Enabled = false;
			this.@__reportFilenameLbl.Location = new System.Drawing.Point(-1, 71);
			this.@__reportFilenameLbl.Name = "__reportFilenameLbl";
			this.@__reportFilenameLbl.Size = new System.Drawing.Size(111, 20);
			this.@__reportFilenameLbl.TabIndex = 10;
			this.@__reportFilenameLbl.Text = "Report Filename";
			this.@__reportFilenameLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __reportFilename
			// 
			this.@__reportFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__reportFilename.Enabled = false;
			this.@__reportFilename.Location = new System.Drawing.Point(116, 68);
			this.@__reportFilename.Name = "__reportFilename";
			this.@__reportFilename.Size = new System.Drawing.Size(359, 20);
			this.@__reportFilename.TabIndex = 3;
			// 
			// __reportBrowse
			// 
			this.@__reportBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__reportBrowse.Enabled = false;
			this.@__reportBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__reportBrowse.Location = new System.Drawing.Point(495, 66);
			this.@__reportBrowse.Name = "__reportBrowse";
			this.@__reportBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__reportBrowse.TabIndex = 4;
			this.@__reportBrowse.Text = "Browse...";
			this.@__reportBrowse.UseVisualStyleBackColor = true;
			// 
			// __exportLangs
			// 
			this.@__exportLangs.AutoSize = true;
			this.@__exportLangs.Enabled = false;
			this.@__exportLangs.Location = new System.Drawing.Point(116, 94);
			this.@__exportLangs.Name = "__exportLangs";
			this.@__exportLangs.Size = new System.Drawing.Size(218, 17);
			this.@__exportLangs.TabIndex = 5;
			this.@__exportLangs.Text = "Export all langs, not just the current LCID";
			this.@__exportLangs.UseVisualStyleBackColor = true;
			// 
			// __exportNonvisual
			// 
			this.@__exportNonvisual.AutoSize = true;
			this.@__exportNonvisual.Location = new System.Drawing.Point(116, 117);
			this.@__exportNonvisual.Name = "__exportNonvisual";
			this.@__exportNonvisual.Size = new System.Drawing.Size(161, 17);
			this.@__exportNonvisual.TabIndex = 6;
			this.@__exportNonvisual.Text = "Include non-visual resources";
			this.@__exportNonvisual.UseVisualStyleBackColor = true;
			// 
			// __exportIconSubimages
			// 
			this.@__exportIconSubimages.AutoSize = true;
			this.@__exportIconSubimages.Location = new System.Drawing.Point(116, 140);
			this.@__exportIconSubimages.Name = "__exportIconSubimages";
			this.@__exportIconSubimages.Size = new System.Drawing.Size(186, 17);
			this.@__exportIconSubimages.TabIndex = 7;
			this.@__exportIconSubimages.Text = "Export Icon Subimages (as PNGs)";
			this.@__exportIconSubimages.UseVisualStyleBackColor = true;
			// 
			// __sourceDirectory
			// 
			this.@__sourceDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__sourceDirectory.Location = new System.Drawing.Point(116, 19);
			this.@__sourceDirectory.Name = "__sourceDirectory";
			this.@__sourceDirectory.Size = new System.Drawing.Size(359, 20);
			this.@__sourceDirectory.TabIndex = 0;
			// 
			// __sourceBrowse
			// 
			this.@__sourceBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__sourceBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__sourceBrowse.Location = new System.Drawing.Point(495, 17);
			this.@__sourceBrowse.Name = "__sourceBrowse";
			this.@__sourceBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__sourceBrowse.TabIndex = 1;
			this.@__sourceBrowse.Text = "Browse...";
			this.@__sourceBrowse.UseVisualStyleBackColor = true;
			// 
			// __sourceDirectoryLbl
			// 
			this.@__sourceDirectoryLbl.Location = new System.Drawing.Point(23, 22);
			this.@__sourceDirectoryLbl.Name = "__sourceDirectoryLbl";
			this.@__sourceDirectoryLbl.Size = new System.Drawing.Size(87, 20);
			this.@__sourceDirectoryLbl.TabIndex = 4;
			this.@__sourceDirectoryLbl.Text = "Directory";
			this.@__sourceDirectoryLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __sourceRecurse
			// 
			this.@__sourceRecurse.AutoSize = true;
			this.@__sourceRecurse.Location = new System.Drawing.Point(116, 71);
			this.@__sourceRecurse.Name = "__sourceRecurse";
			this.@__sourceRecurse.Size = new System.Drawing.Size(130, 17);
			this.@__sourceRecurse.TabIndex = 3;
			this.@__sourceRecurse.Text = "Search Subdirectories";
			this.@__sourceRecurse.UseVisualStyleBackColor = true;
			// 
			// __sourceGrp
			// 
			this.@__sourceGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__sourceGrp.Controls.Add(this.@__sourceDirectoryLbl);
			this.@__sourceGrp.Controls.Add(this.@__sourceDirectory);
			this.@__sourceGrp.Controls.Add(this.@__sourceBrowse);
			this.@__sourceGrp.Controls.Add(this.@__sourceFilterLbl);
			this.@__sourceGrp.Controls.Add(this.@__sourceFilter);
			this.@__sourceGrp.Controls.Add(this.@__sourceRecurse);
			this.@__sourceGrp.Location = new System.Drawing.Point(12, 12);
			this.@__sourceGrp.Name = "__sourceGrp";
			this.@__sourceGrp.Size = new System.Drawing.Size(576, 96);
			this.@__sourceGrp.TabIndex = 0;
			this.@__sourceGrp.TabStop = false;
			this.@__sourceGrp.Text = "Source";
			// 
			// __sourceFilterLbl
			// 
			this.@__sourceFilterLbl.Location = new System.Drawing.Point(11, 48);
			this.@__sourceFilterLbl.Name = "__sourceFilterLbl";
			this.@__sourceFilterLbl.Size = new System.Drawing.Size(99, 17);
			this.@__sourceFilterLbl.TabIndex = 6;
			this.@__sourceFilterLbl.Text = "File Extension Filter";
			this.@__sourceFilterLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __sourceFilter
			// 
			this.@__sourceFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__sourceFilter.Location = new System.Drawing.Point(116, 45);
			this.@__sourceFilter.Name = "__sourceFilter";
			this.@__sourceFilter.Size = new System.Drawing.Size(359, 20);
			this.@__sourceFilter.TabIndex = 2;
			this.@__sourceFilter.Text = "*.exe;*.dll;*.scr;*.msstyles;*.ocx;*.cpl";
			// 
			// __process
			// 
			this.@__process.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__process.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__process.Location = new System.Drawing.Point(432, 401);
			this.@__process.Name = "__process";
			this.@__process.Size = new System.Drawing.Size(75, 23);
			this.@__process.TabIndex = 3;
			this.@__process.Text = "Process";
			this.@__process.UseVisualStyleBackColor = true;
			// 
			// __progGrp
			// 
			this.@__progGrp.Controls.Add(this.@__progSourceLbl);
			this.@__progGrp.Controls.Add(this.@__progSource);
			this.@__progGrp.Controls.Add(this.@__progOverallLbl);
			this.@__progGrp.Controls.Add(this.@__progOverall);
			this.@__progGrp.Location = new System.Drawing.Point(12, 284);
			this.@__progGrp.Name = "__progGrp";
			this.@__progGrp.Size = new System.Drawing.Size(576, 107);
			this.@__progGrp.TabIndex = 2;
			this.@__progGrp.TabStop = false;
			this.@__progGrp.Text = "Progress";
			// 
			// __progSourceLbl
			// 
			this.@__progSourceLbl.AutoSize = true;
			this.@__progSourceLbl.Location = new System.Drawing.Point(8, 22);
			this.@__progSourceLbl.Name = "__progSourceLbl";
			this.@__progSourceLbl.Size = new System.Drawing.Size(67, 13);
			this.@__progSourceLbl.TabIndex = 0;
			this.@__progSourceLbl.Text = "0% complete";
			// 
			// __progSource
			// 
			this.@__progSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__progSource.Location = new System.Drawing.Point(11, 38);
			this.@__progSource.Name = "__progSource";
			this.@__progSource.Size = new System.Drawing.Size(559, 14);
			this.@__progSource.TabIndex = 1;
			// 
			// __progOverallLbl
			// 
			this.@__progOverallLbl.AutoSize = true;
			this.@__progOverallLbl.Location = new System.Drawing.Point(8, 63);
			this.@__progOverallLbl.Name = "__progOverallLbl";
			this.@__progOverallLbl.Size = new System.Drawing.Size(114, 13);
			this.@__progOverallLbl.TabIndex = 2;
			this.@__progOverallLbl.Text = "0% complete - 0/0 files";
			// 
			// __progOverall
			// 
			this.@__progOverall.Location = new System.Drawing.Point(11, 80);
			this.@__progOverall.Name = "__progOverall";
			this.@__progOverall.Size = new System.Drawing.Size(559, 14);
			this.@__progOverall.TabIndex = 3;
			// 
			// __cancel
			// 
			this.@__cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.@__cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__cancel.Location = new System.Drawing.Point(513, 401);
			this.@__cancel.Name = "__cancel";
			this.@__cancel.Size = new System.Drawing.Size(75, 23);
			this.@__cancel.TabIndex = 4;
			this.@__cancel.Text = "Cancel";
			this.@__cancel.UseVisualStyleBackColor = true;
			// 
			// __error
			// 
			this.@__error.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.@__error.ContainerControl = this;
			// 
			// BatchProcessForm
			// 
			this.AcceptButton = this.@__process;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
			this.CancelButton = this.@__cancel;
			this.ClientSize = new System.Drawing.Size(600, 435);
			this.Controls.Add(this.@__sourceGrp);
			this.Controls.Add(this.@__optionsGrp);
			this.Controls.Add(this.@__progGrp);
			this.Controls.Add(this.@__process);
			this.Controls.Add(this.@__cancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BatchProcessForm";
			this.ShowIcon = false;
			this.Text = "Batch Process";
			this.@__optionsGrp.ResumeLayout(false);
			this.@__optionsGrp.PerformLayout();
			this.@__sourceGrp.ResumeLayout(false);
			this.@__sourceGrp.PerformLayout();
			this.@__progGrp.ResumeLayout(false);
			this.@__progGrp.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.@__error)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox __optionsGrp;
		private System.Windows.Forms.TextBox __sourceDirectory;
		private System.Windows.Forms.Button __sourceBrowse;
		private System.Windows.Forms.Label __sourceDirectoryLbl;
		private System.Windows.Forms.CheckBox __sourceRecurse;
		private System.Windows.Forms.GroupBox __sourceGrp;
		private System.Windows.Forms.TextBox __sourceFilter;
		private System.Windows.Forms.Label __sourceFilterLbl;
		private System.Windows.Forms.TextBox __reportFilename;
		private System.Windows.Forms.Button __reportBrowse;
		private System.Windows.Forms.Label __reportFilenameLbl;
		private System.Windows.Forms.GroupBox __progGrp;
		private System.Windows.Forms.ProgressBar __progOverall;
		private System.Windows.Forms.ProgressBar __progSource;
		private System.Windows.Forms.Button __process;
		private System.Windows.Forms.Label __progOverallLbl;
		private System.Windows.Forms.Label __progSourceLbl;
		private System.Windows.Forms.Button __cancel;
		private System.Windows.Forms.CheckBox __exportNonvisual;
		private System.Windows.Forms.TextBox __exportDirectory;
		private System.Windows.Forms.Button __exportBrowse;
		private System.Windows.Forms.Label __exportDirectoryLbl;
		private System.Windows.Forms.FolderBrowserDialog __fbd;
		private System.Windows.Forms.SaveFileDialog __sfd;
		private System.Windows.Forms.ErrorProvider __error;
		private System.Windows.Forms.CheckBox __reportEnable;
		private System.Windows.Forms.CheckBox __exportLangs;
		private System.ComponentModel.BackgroundWorker __bw;
		private System.Windows.Forms.CheckBox __exportIconSubimages;
	}
}