namespace Anolis.Installer.Pages {
	partial class SelectPackagePage {
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
			this.@__anopRad = new System.Windows.Forms.RadioButton();
			this.@__anopFilenameLbl = new System.Windows.Forms.Label();
			this.@__embedRad = new System.Windows.Forms.RadioButton();
			this.@__anopFilename = new System.Windows.Forms.TextBox();
			this.@__anopBrowse = new System.Windows.Forms.Button();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__embedList = new System.Windows.Forms.ListBox();
			this.@__packBrowse = new System.Windows.Forms.Button();
			this.@__packFilename = new System.Windows.Forms.TextBox();
			this.@__packFilenameLbl = new System.Windows.Forms.Label();
			this.@__packRad = new System.Windows.Forms.RadioButton();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__anopRad);
			this.@__content.Controls.Add(this.@__anopFilenameLbl);
			this.@__content.Controls.Add(this.@__anopFilename);
			this.@__content.Controls.Add(this.@__anopBrowse);
			this.@__content.Controls.Add(this.@__packRad);
			this.@__content.Controls.Add(this.@__packFilenameLbl);
			this.@__content.Controls.Add(this.@__packFilename);
			this.@__content.Controls.Add(this.@__packBrowse);
			this.@__content.Controls.Add(this.@__embedRad);
			this.@__content.Controls.Add(this.@__embedList);
			// 
			// __anopRad
			// 
			this.@__anopRad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__anopRad.Checked = true;
			this.@__anopRad.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__anopRad.Location = new System.Drawing.Point(22, 13);
			this.@__anopRad.Name = "__anopRad";
			this.@__anopRad.Size = new System.Drawing.Size(422, 18);
			this.@__anopRad.TabIndex = 0;
			this.@__anopRad.TabStop = true;
			this.@__anopRad.Text = "A package archive (*.anop) on my computer";
			this.@__anopRad.UseVisualStyleBackColor = true;
			// 
			// __anopFilenameLbl
			// 
			this.@__anopFilenameLbl.Location = new System.Drawing.Point(3, 38);
			this.@__anopFilenameLbl.Margin = new System.Windows.Forms.Padding(3);
			this.@__anopFilenameLbl.Name = "__anopFilenameLbl";
			this.@__anopFilenameLbl.Size = new System.Drawing.Size(111, 26);
			this.@__anopFilenameLbl.TabIndex = 1;
			this.@__anopFilenameLbl.Text = "Filename";
			this.@__anopFilenameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// __embedRad
			// 
			this.@__embedRad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__embedRad.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__embedRad.Location = new System.Drawing.Point(22, 132);
			this.@__embedRad.Name = "__embedRad";
			this.@__embedRad.Size = new System.Drawing.Size(422, 18);
			this.@__embedRad.TabIndex = 2;
			this.@__embedRad.Text = "A package embedded in this installer";
			this.@__embedRad.UseVisualStyleBackColor = true;
			// 
			// __anopFilename
			// 
			this.@__anopFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__anopFilename.BackColor = System.Drawing.SystemColors.Window;
			this.@__anopFilename.Location = new System.Drawing.Point(120, 42);
			this.@__anopFilename.Name = "__anopFilename";
			this.@__anopFilename.ReadOnly = true;
			this.@__anopFilename.Size = new System.Drawing.Size(210, 20);
			this.@__anopFilename.TabIndex = 4;
			// 
			// __anopBrowse
			// 
			this.@__anopBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__anopBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__anopBrowse.Location = new System.Drawing.Point(336, 40);
			this.@__anopBrowse.Name = "__anopBrowse";
			this.@__anopBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__anopBrowse.TabIndex = 5;
			this.@__anopBrowse.Text = "Browse...";
			this.@__anopBrowse.UseVisualStyleBackColor = true;
			// 
			// __ofd
			// 
			this.@__ofd.Filter = "Anolis Package (*.anop)|*.anop|All files (*.*)|*.*";
			this.@__ofd.Title = "Select Anolis Package File";
			// 
			// __embedList
			// 
			this.@__embedList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__embedList.FormattingEnabled = true;
			this.@__embedList.IntegralHeight = false;
			this.@__embedList.Location = new System.Drawing.Point(51, 156);
			this.@__embedList.Name = "__embedList";
			this.@__embedList.Size = new System.Drawing.Size(360, 69);
			this.@__embedList.TabIndex = 6;
			// 
			// __packBrowse
			// 
			this.@__packBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__packBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__packBrowse.Location = new System.Drawing.Point(336, 99);
			this.@__packBrowse.Name = "__packBrowse";
			this.@__packBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__packBrowse.TabIndex = 10;
			this.@__packBrowse.Text = "Browse...";
			this.@__packBrowse.UseVisualStyleBackColor = true;
			// 
			// __packFilename
			// 
			this.@__packFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__packFilename.BackColor = System.Drawing.SystemColors.Window;
			this.@__packFilename.Location = new System.Drawing.Point(120, 101);
			this.@__packFilename.Name = "__packFilename";
			this.@__packFilename.ReadOnly = true;
			this.@__packFilename.Size = new System.Drawing.Size(210, 20);
			this.@__packFilename.TabIndex = 9;
			// 
			// __packFilenameLbl
			// 
			this.@__packFilenameLbl.Location = new System.Drawing.Point(3, 97);
			this.@__packFilenameLbl.Margin = new System.Windows.Forms.Padding(3);
			this.@__packFilenameLbl.Name = "__packFilenameLbl";
			this.@__packFilenameLbl.Size = new System.Drawing.Size(111, 26);
			this.@__packFilenameLbl.TabIndex = 8;
			this.@__packFilenameLbl.Text = "Filename";
			this.@__packFilenameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// __packRad
			// 
			this.@__packRad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__packRad.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__packRad.Location = new System.Drawing.Point(22, 72);
			this.@__packRad.Name = "__packRad";
			this.@__packRad.Size = new System.Drawing.Size(422, 18);
			this.@__packRad.TabIndex = 7;
			this.@__packRad.Text = "A package definition file (package.xml) on my computer";
			this.@__packRad.UseVisualStyleBackColor = true;
			// 
			// SelectPackagePage
			// 
			this.Name = "SelectPackagePage";
			this.PageSubtitle = "The package to install can either be installed on this computer or embedded in th" +
				"is installer";
			this.PageTitle = "Select a package to install";
			this.@__content.ResumeLayout(false);
			this.@__content.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button __anopBrowse;
		private System.Windows.Forms.TextBox __anopFilename;
		private System.Windows.Forms.RadioButton __embedRad;
		private System.Windows.Forms.Label __anopFilenameLbl;
		private System.Windows.Forms.RadioButton __anopRad;
		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.ListBox __embedList;
		private System.Windows.Forms.Button __packBrowse;
		private System.Windows.Forms.TextBox __packFilename;
		private System.Windows.Forms.Label __packFilenameLbl;
		private System.Windows.Forms.RadioButton __packRad;
	}
}
