namespace Anolis.Installer.Pages {
	partial class DestinationPage {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DestinationPage));
			this.@__destBrowse = new System.Windows.Forms.Button();
			this.@__progGroup = new System.Windows.Forms.CheckBox();
			this.@__progGroupAll = new System.Windows.Forms.RadioButton();
			this.@__progGroupMe = new System.Windows.Forms.RadioButton();
			this.@__destLbl = new System.Windows.Forms.Label();
			this.@__fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.@__toolsLbl = new System.Windows.Forms.Label();
			this.@__destPath = new System.Windows.Forms.TextBox();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__destPath);
			this.@__content.Controls.Add(this.@__toolsLbl);
			this.@__content.Controls.Add(this.@__destLbl);
			this.@__content.Controls.Add(this.@__progGroupMe);
			this.@__content.Controls.Add(this.@__progGroupAll);
			this.@__content.Controls.Add(this.@__progGroup);
			this.@__content.Controls.Add(this.@__destBrowse);
			// 
			// __destBrowse
			// 
			this.@__destBrowse.Location = new System.Drawing.Point(365, 84);
			this.@__destBrowse.Name = "__destBrowse";
			this.@__destBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__destBrowse.TabIndex = 1;
			this.@__destBrowse.Text = "Browse...";
			this.@__destBrowse.UseVisualStyleBackColor = true;
			// 
			// __progGroup
			// 
			this.@__progGroup.AutoSize = true;
			this.@__progGroup.Checked = true;
			this.@__progGroup.CheckState = System.Windows.Forms.CheckState.Checked;
			this.@__progGroup.Location = new System.Drawing.Point(22, 135);
			this.@__progGroup.Name = "__progGroup";
			this.@__progGroup.Size = new System.Drawing.Size(240, 17);
			this.@__progGroup.TabIndex = 2;
			this.@__progGroup.Text = "Create Anolis program group in the start menu";
			this.@__progGroup.UseVisualStyleBackColor = true;
			// 
			// __progGroupAll
			// 
			this.@__progGroupAll.AutoSize = true;
			this.@__progGroupAll.Checked = true;
			this.@__progGroupAll.Location = new System.Drawing.Point(42, 158);
			this.@__progGroupAll.Name = "__progGroupAll";
			this.@__progGroupAll.Size = new System.Drawing.Size(81, 17);
			this.@__progGroupAll.TabIndex = 3;
			this.@__progGroupAll.TabStop = true;
			this.@__progGroupAll.Text = "For all users";
			this.@__progGroupAll.UseVisualStyleBackColor = true;
			// 
			// __progGroupMe
			// 
			this.@__progGroupMe.AutoSize = true;
			this.@__progGroupMe.Location = new System.Drawing.Point(42, 181);
			this.@__progGroupMe.Name = "__progGroupMe";
			this.@__progGroupMe.Size = new System.Drawing.Size(76, 17);
			this.@__progGroupMe.TabIndex = 4;
			this.@__progGroupMe.Text = "Just for me";
			this.@__progGroupMe.UseVisualStyleBackColor = true;
			// 
			// __destLbl
			// 
			this.@__destLbl.Location = new System.Drawing.Point(1, 89);
			this.@__destLbl.Name = "__destLbl";
			this.@__destLbl.Size = new System.Drawing.Size(119, 27);
			this.@__destLbl.TabIndex = 5;
			this.@__destLbl.Text = "Destination directory";
			this.@__destLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __toolsLbl
			// 
			this.@__toolsLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__toolsLbl.Location = new System.Drawing.Point(3, 9);
			this.@__toolsLbl.Name = "__toolsLbl";
			this.@__toolsLbl.Size = new System.Drawing.Size(437, 64);
			this.@__toolsLbl.TabIndex = 7;
			this.@__toolsLbl.Text = resources.GetString("__toolsLbl.Text");
			// 
			// __destPath
			// 
			this.@__destPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__destPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.@__destPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.@__destPath.Location = new System.Drawing.Point(126, 86);
			this.@__destPath.Name = "__destPath";
			this.@__destPath.Size = new System.Drawing.Size(233, 20);
			this.@__destPath.TabIndex = 8;
			// 
			// DestinationPage
			// 
			this.Name = "DestinationPage";
			this.PageSubtitle = "Select a directory in which to place the downloaded files and set other options";
			this.PageTitle = "Destination Directory and Installation Options";
			this.@__content.ResumeLayout(false);
			this.@__content.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label __destLbl;
		private System.Windows.Forms.RadioButton __progGroupMe;
		private System.Windows.Forms.RadioButton __progGroupAll;
		private System.Windows.Forms.CheckBox __progGroup;
		private System.Windows.Forms.Button __destBrowse;
		private System.Windows.Forms.FolderBrowserDialog __fbd;
		private System.Windows.Forms.Label __toolsLbl;
		private System.Windows.Forms.TextBox __destPath;
	}
}
