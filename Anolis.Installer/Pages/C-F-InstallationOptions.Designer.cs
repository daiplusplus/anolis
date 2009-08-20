namespace Anolis.Installer.Pages {
	partial class InstallationOptionsPage {
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
			this.@__backupPath = new System.Windows.Forms.TextBox();
			this.@__backupBrowse = new System.Windows.Forms.Button();
			this.@__fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.@__backup = new System.Windows.Forms.CheckBox();
			this.@__backupDesc = new System.Windows.Forms.Label();
			this.@__bw = new System.ComponentModel.BackgroundWorker();
			this.@__comp = new System.Windows.Forms.RadioButton();
			this.@__i386 = new System.Windows.Forms.RadioButton();
			this.@__i386Lbl = new System.Windows.Forms.Label();
			this.@__i386Browse = new System.Windows.Forms.Button();
			this.@__i386Path = new System.Windows.Forms.TextBox();
			this.@__advanced = new System.Windows.Forms.Button();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__advanced);
			this.@__content.Controls.Add(this.@__comp);
			this.@__content.Controls.Add(this.@__backup);
			this.@__content.Controls.Add(this.@__backupDesc);
			this.@__content.Controls.Add(this.@__backupPath);
			this.@__content.Controls.Add(this.@__backupBrowse);
			this.@__content.Controls.Add(this.@__i386);
			this.@__content.Controls.Add(this.@__i386Lbl);
			this.@__content.Controls.Add(this.@__i386Path);
			this.@__content.Controls.Add(this.@__i386Browse);
			// 
			// __backupPath
			// 
			this.@__backupPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.@__backupPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.@__backupPath.Location = new System.Drawing.Point(91, 85);
			this.@__backupPath.Name = "__backupPath";
			this.@__backupPath.Size = new System.Drawing.Size(257, 20);
			this.@__backupPath.TabIndex = 2;
			// 
			// __backupBrowse
			// 
			this.@__backupBrowse.Location = new System.Drawing.Point(354, 83);
			this.@__backupBrowse.Name = "__backupBrowse";
			this.@__backupBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__backupBrowse.TabIndex = 3;
			this.@__backupBrowse.Text = "Browse...";
			this.@__backupBrowse.UseVisualStyleBackColor = true;
			// 
			// __backup
			// 
			this.@__backup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__backup.Checked = true;
			this.@__backup.CheckState = System.Windows.Forms.CheckState.Checked;
			this.@__backup.Location = new System.Drawing.Point(47, 28);
			this.@__backup.Name = "__backup";
			this.@__backup.Size = new System.Drawing.Size(366, 17);
			this.@__backup.TabIndex = 1;
			this.@__backup.Text = "Create uninstallation backup directory (Recommended)";
			this.@__backup.UseVisualStyleBackColor = true;
			// 
			// __backupDesc
			// 
			this.@__backupDesc.Location = new System.Drawing.Point(91, 48);
			this.@__backupDesc.Name = "__backupDesc";
			this.@__backupDesc.Size = new System.Drawing.Size(338, 31);
			this.@__backupDesc.TabIndex = 4;
			this.@__backupDesc.Text = "You must create a backup directory if you ever wish to uninstall this package in " +
				"future";
			// 
			// __comp
			// 
			this.@__comp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__comp.Checked = true;
			this.@__comp.Location = new System.Drawing.Point(14, 5);
			this.@__comp.Name = "__comp";
			this.@__comp.Size = new System.Drawing.Size(430, 17);
			this.@__comp.TabIndex = 0;
			this.@__comp.TabStop = true;
			this.@__comp.Text = "Install to this Computer";
			this.@__comp.UseVisualStyleBackColor = true;
			// 
			// __i386
			// 
			this.@__i386.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__i386.Location = new System.Drawing.Point(14, 115);
			this.@__i386.Name = "__i386";
			this.@__i386.Size = new System.Drawing.Size(430, 17);
			this.@__i386.TabIndex = 5;
			this.@__i386.Text = "Install to Windows CD";
			this.@__i386.UseVisualStyleBackColor = true;
			// 
			// __i386Lbl
			// 
			this.@__i386Lbl.Enabled = false;
			this.@__i386Lbl.Location = new System.Drawing.Point(3, 144);
			this.@__i386Lbl.Name = "__i386Lbl";
			this.@__i386Lbl.Size = new System.Drawing.Size(82, 23);
			this.@__i386Lbl.TabIndex = 9;
			this.@__i386Lbl.Text = "CD Root";
			this.@__i386Lbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __i386Browse
			// 
			this.@__i386Browse.Enabled = false;
			this.@__i386Browse.Location = new System.Drawing.Point(354, 139);
			this.@__i386Browse.Name = "__i386Browse";
			this.@__i386Browse.Size = new System.Drawing.Size(75, 23);
			this.@__i386Browse.TabIndex = 7;
			this.@__i386Browse.Text = "Browse...";
			this.@__i386Browse.UseVisualStyleBackColor = true;
			// 
			// __i386Path
			// 
			this.@__i386Path.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.@__i386Path.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.@__i386Path.Enabled = false;
			this.@__i386Path.Location = new System.Drawing.Point(91, 141);
			this.@__i386Path.Name = "__i386Path";
			this.@__i386Path.Size = new System.Drawing.Size(257, 20);
			this.@__i386Path.TabIndex = 6;
			// 
			// __advanced
			// 
			this.@__advanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__advanced.Location = new System.Drawing.Point(310, 206);
			this.@__advanced.Name = "__advanced";
			this.@__advanced.Size = new System.Drawing.Size(134, 23);
			this.@__advanced.TabIndex = 10;
			this.@__advanced.Text = "Advanced Options...";
			this.@__advanced.UseVisualStyleBackColor = true;
			// 
			// InstallationOptionsPage
			// 
			this.Name = "InstallationOptionsPage";
			this.PageSubtitle = "Select a backup directory and other options";
			this.PageTitle = "Installation Options";
			this.@__content.ResumeLayout(false);
			this.@__content.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button __backupBrowse;
		private System.Windows.Forms.TextBox __backupPath;
		private System.Windows.Forms.FolderBrowserDialog __fbd;
		private System.Windows.Forms.CheckBox __backup;
		private System.Windows.Forms.Label __backupDesc;
		private System.ComponentModel.BackgroundWorker __bw;
		private System.Windows.Forms.RadioButton __i386;
		private System.Windows.Forms.RadioButton __comp;
		private System.Windows.Forms.Button __i386Browse;
		private System.Windows.Forms.TextBox __i386Path;
		private System.Windows.Forms.Label __i386Lbl;
		private System.Windows.Forms.Button __advanced;

	}
}
