﻿namespace Anolis.Installer.Pages {
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
			this.@__dirLbl = new System.Windows.Forms.Label();
			this.@__dir = new System.Windows.Forms.TextBox();
			this.@__browse = new System.Windows.Forms.Button();
			this.@__fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.@__sysRes = new System.Windows.Forms.CheckBox();
			this.@__sysResDesc = new System.Windows.Forms.Label();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__sysRes);
			this.@__content.Controls.Add(this.@__sysResDesc);
			this.@__content.Controls.Add(this.@__browse);
			this.@__content.Controls.Add(this.@__dir);
			this.@__content.Controls.Add(this.@__dirLbl);
			// 
			// __dirLbl
			// 
			this.@__dirLbl.AutoSize = true;
			this.@__dirLbl.Location = new System.Drawing.Point(19, 54);
			this.@__dirLbl.Name = "__dirLbl";
			this.@__dirLbl.Size = new System.Drawing.Size(49, 13);
			this.@__dirLbl.TabIndex = 0;
			this.@__dirLbl.Text = "Directory";
			// 
			// __dir
			// 
			this.@__dir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__dir.Location = new System.Drawing.Point(76, 51);
			this.@__dir.Name = "__dir";
			this.@__dir.Size = new System.Drawing.Size(287, 20);
			this.@__dir.TabIndex = 1;
			// 
			// __browse
			// 
			this.@__browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__browse.Location = new System.Drawing.Point(369, 49);
			this.@__browse.Name = "__browse";
			this.@__browse.Size = new System.Drawing.Size(75, 23);
			this.@__browse.TabIndex = 2;
			this.@__browse.Text = "Browse...";
			this.@__browse.UseVisualStyleBackColor = true;
			// 
			// __fbd
			// 
			this.@__fbd.Description = "Select the backup directory to recover from";
			this.@__fbd.ShowNewFolderButton = false;
			// 
			// __sysRes
			// 
			this.@__sysRes.AutoSize = true;
			this.@__sysRes.Checked = true;
			this.@__sysRes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.@__sysRes.Location = new System.Drawing.Point(22, 88);
			this.@__sysRes.Name = "__sysRes";
			this.@__sysRes.Size = new System.Drawing.Size(242, 17);
			this.@__sysRes.TabIndex = 7;
			this.@__sysRes.Text = "Create System Restore Point (Recommended)";
			this.@__sysRes.UseVisualStyleBackColor = true;
			// 
			// __sysResDesc
			// 
			this.@__sysResDesc.Location = new System.Drawing.Point(63, 108);
			this.@__sysResDesc.Name = "__sysResDesc";
			this.@__sysResDesc.Size = new System.Drawing.Size(341, 30);
			this.@__sysResDesc.TabIndex = 8;
			this.@__sysResDesc.Text = "An added layer of protection. Note this does not always protect against modificat" +
				"ions made to non-Windows files under Program Files";
			// 
			// SelectBackupPage
			// 
			this.Name = "SelectBackupPage";
			this.PageSubtitle = "Select the directory to recover from";
			this.PageTitle = "Select Backup Directory";
			this.@__content.ResumeLayout(false);
			this.@__content.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button __browse;
		private System.Windows.Forms.TextBox __dir;
		private System.Windows.Forms.Label __dirLbl;
		private System.Windows.Forms.FolderBrowserDialog __fbd;
		private System.Windows.Forms.CheckBox __sysRes;
		private System.Windows.Forms.Label __sysResDesc;
	}
}
