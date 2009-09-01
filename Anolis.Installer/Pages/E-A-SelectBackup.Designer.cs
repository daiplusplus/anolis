namespace Anolis.Installer.Pages {
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
			this.@__culture = new Anolis.Installer.Pages.LanguageComboBox();
			this.@__uninstallationLanguage = new System.Windows.Forms.Label();
			this.@__feedback = new System.Windows.Forms.TextBox();
			this.@__feedbackLbl = new System.Windows.Forms.Label();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__feedbackLbl);
			this.@__content.Controls.Add(this.@__feedback);
			this.@__content.Controls.Add(this.@__uninstallationLanguage);
			this.@__content.Controls.Add(this.@__culture);
			this.@__content.Controls.Add(this.@__sysRes);
			this.@__content.Controls.Add(this.@__browse);
			this.@__content.Controls.Add(this.@__dir);
			this.@__content.Controls.Add(this.@__dirLbl);
			// 
			// __dirLbl
			// 
			this.@__dirLbl.AutoSize = true;
			this.@__dirLbl.Location = new System.Drawing.Point(19, 55);
			this.@__dirLbl.Name = "__dirLbl";
			this.@__dirLbl.Size = new System.Drawing.Size(49, 13);
			this.@__dirLbl.TabIndex = 0;
			this.@__dirLbl.Text = "Directory";
			// 
			// __dir
			// 
			this.@__dir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__dir.Location = new System.Drawing.Point(76, 52);
			this.@__dir.Name = "__dir";
			this.@__dir.Size = new System.Drawing.Size(287, 20);
			this.@__dir.TabIndex = 1;
			// 
			// __browse
			// 
			this.@__browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__browse.Location = new System.Drawing.Point(369, 50);
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
			this.@__sysRes.Checked = true;
			this.@__sysRes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.@__sysRes.Location = new System.Drawing.Point(22, 89);
			this.@__sysRes.Name = "__sysRes";
			this.@__sysRes.Size = new System.Drawing.Size(422, 22);
			this.@__sysRes.TabIndex = 7;
			this.@__sysRes.Text = "Create System Restore Point (Recommended)";
			this.@__sysRes.UseVisualStyleBackColor = true;
			// 
			// __culture
			// 
			this.@__culture.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.@__culture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.@__culture.FormattingEnabled = true;
			this.@__culture.Location = new System.Drawing.Point(76, 25);
			this.@__culture.Name = "__culture";
			this.@__culture.Size = new System.Drawing.Size(189, 21);
			this.@__culture.TabIndex = 9;
			this.@__culture.Visible = false;
			// 
			// __uninstallationLanguage
			// 
			this.@__uninstallationLanguage.AutoSize = true;
			this.@__uninstallationLanguage.Location = new System.Drawing.Point(73, 9);
			this.@__uninstallationLanguage.Name = "__uninstallationLanguage";
			this.@__uninstallationLanguage.Size = new System.Drawing.Size(88, 13);
			this.@__uninstallationLanguage.TabIndex = 10;
			this.@__uninstallationLanguage.Text = "Uninst Language";
			this.@__uninstallationLanguage.Visible = false;
			// 
			// __feedback
			// 
			this.@__feedback.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__feedback.Location = new System.Drawing.Point(22, 158);
			this.@__feedback.Multiline = true;
			this.@__feedback.Name = "__feedback";
			this.@__feedback.Size = new System.Drawing.Size(422, 57);
			this.@__feedback.TabIndex = 11;
			// 
			// __feedbackLbl
			// 
			this.@__feedbackLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__feedbackLbl.Location = new System.Drawing.Point(22, 125);
			this.@__feedbackLbl.Name = "__feedbackLbl";
			this.@__feedbackLbl.Size = new System.Drawing.Size(422, 30);
			this.@__feedbackLbl.TabIndex = 12;
			this.@__feedbackLbl.Text = "Why are you uninstalling this package? Your feedback will be sent to the develope" +
				"r to improve the package in future.";
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
		private System.Windows.Forms.Label __uninstallationLanguage;
		private LanguageComboBox __culture;
		private System.Windows.Forms.Label __feedbackLbl;
		private System.Windows.Forms.TextBox __feedback;
	}
}
