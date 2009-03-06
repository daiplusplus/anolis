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
			this.@__sysres = new System.Windows.Forms.CheckBox();
			this.@__sysResDesc = new System.Windows.Forms.Label();
			this.@__banner.SuspendLayout();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			this.@__banner.Controls.SetChildIndex(this.@__bannerTitle, 0);
			this.@__banner.Controls.SetChildIndex(this.@__bannerSubtitle, 0);
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__sysResDesc);
			this.@__content.Controls.Add(this.@__sysres);
			this.@__content.Controls.Add(this.@__backupDesc);
			this.@__content.Controls.Add(this.@__backup);
			this.@__content.Controls.Add(this.@__backupBrowse);
			this.@__content.Controls.Add(this.@__backupPath);
			// 
			// __bannerSubtitle
			// 
			this.@__bannerSubtitle.Text = "Select a backup directory and other options";
			// 
			// __bannerTitle
			// 
			this.@__bannerTitle.Text = "Installation Options";
			// 
			// __backupPath
			// 
			this.@__backupPath.Location = new System.Drawing.Point(44, 65);
			this.@__backupPath.Name = "__backupPath";
			this.@__backupPath.Size = new System.Drawing.Size(257, 21);
			this.@__backupPath.TabIndex = 1;
			// 
			// __backupBrowse
			// 
			this.@__backupBrowse.Location = new System.Drawing.Point(307, 63);
			this.@__backupBrowse.Name = "__backupBrowse";
			this.@__backupBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__backupBrowse.TabIndex = 2;
			this.@__backupBrowse.Text = "Browse...";
			this.@__backupBrowse.UseVisualStyleBackColor = true;
			// 
			// __backup
			// 
			this.@__backup.AutoSize = true;
			this.@__backup.Location = new System.Drawing.Point(22, 18);
			this.@__backup.Name = "__backup";
			this.@__backup.Size = new System.Drawing.Size(289, 17);
			this.@__backup.TabIndex = 3;
			this.@__backup.Text = "Create uninstallation backup directory (Recommended)";
			this.@__backup.UseVisualStyleBackColor = true;
			// 
			// __backupDesc
			// 
			this.@__backupDesc.AutoSize = true;
			this.@__backupDesc.Location = new System.Drawing.Point(41, 38);
			this.@__backupDesc.Name = "__backupDesc";
			this.@__backupDesc.Size = new System.Drawing.Size(393, 13);
			this.@__backupDesc.TabIndex = 4;
			this.@__backupDesc.Text = "You must create a backup directory if you wish to uninstall this package in futur" +
				"e";
			// 
			// __sysres
			// 
			this.@__sysres.AutoSize = true;
			this.@__sysres.Location = new System.Drawing.Point(22, 101);
			this.@__sysres.Name = "__sysres";
			this.@__sysres.Size = new System.Drawing.Size(246, 17);
			this.@__sysres.TabIndex = 5;
			this.@__sysres.Text = "Create System Restore Point (Recommended)";
			this.@__sysres.UseVisualStyleBackColor = true;
			// 
			// __sysResDesc
			// 
			this.@__sysResDesc.Location = new System.Drawing.Point(41, 121);
			this.@__sysResDesc.Name = "__sysResDesc";
			this.@__sysResDesc.Size = new System.Drawing.Size(393, 30);
			this.@__sysResDesc.TabIndex = 6;
			this.@__sysResDesc.Text = "An added layer of protection. Note this does not always protect against modificat" +
				"ions made to non-Windows files under Program Files";
			// 
			// InstallationOptionsPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "InstallationOptionsPage";
			this.@__banner.ResumeLayout(false);
			this.@__content.ResumeLayout(false);
			this.@__content.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button __backupBrowse;
		private System.Windows.Forms.TextBox __backupPath;
		private System.Windows.Forms.FolderBrowserDialog __fbd;
		private System.Windows.Forms.CheckBox __backup;
		private System.Windows.Forms.Label __sysResDesc;
		private System.Windows.Forms.CheckBox __sysres;
		private System.Windows.Forms.Label __backupDesc;

	}
}
