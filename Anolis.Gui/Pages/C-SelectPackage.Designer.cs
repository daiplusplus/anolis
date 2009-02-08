namespace Anolis.Gui.Pages {
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
			this.@__optBrowse = new System.Windows.Forms.RadioButton();
			this.@__optBrowseFilenameLbl = new System.Windows.Forms.Label();
			this.@__optPackages = new System.Windows.Forms.RadioButton();
			this.@__optPackagesList = new System.Windows.Forms.ListView();
			this.@__optBrowseFilename = new System.Windows.Forms.TextBox();
			this.@__optBrowseBrowse = new System.Windows.Forms.Button();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__banner.SuspendLayout();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			this.@__banner.Controls.SetChildIndex(this.@__bannerTitle, 0);
			this.@__banner.Controls.SetChildIndex(this.@__bannerSubtitle, 0);
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__optBrowseBrowse);
			this.@__content.Controls.Add(this.@__optBrowseFilename);
			this.@__content.Controls.Add(this.@__optPackagesList);
			this.@__content.Controls.Add(this.@__optPackages);
			this.@__content.Controls.Add(this.@__optBrowseFilenameLbl);
			this.@__content.Controls.Add(this.@__optBrowse);
			// 
			// __bannerSubtitle
			// 
			this.@__bannerSubtitle.Text = "The package to install can either be installed on this computer or embedded in th" +
				"is installer";
			// 
			// __bannerTitle
			// 
			this.@__bannerTitle.Text = "Select a package to install";
			// 
			// __optBrowse
			// 
			this.@__optBrowse.AutoSize = true;
			this.@__optBrowse.Location = new System.Drawing.Point(22, 17);
			this.@__optBrowse.Name = "__optBrowse";
			this.@__optBrowse.Size = new System.Drawing.Size(204, 17);
			this.@__optBrowse.TabIndex = 0;
			this.@__optBrowse.TabStop = true;
			this.@__optBrowse.Text = "Browse my computer for the package";
			this.@__optBrowse.UseVisualStyleBackColor = true;
			// 
			// __optBrowseFilenameLbl
			// 
			this.@__optBrowseFilenameLbl.AutoSize = true;
			this.@__optBrowseFilenameLbl.Location = new System.Drawing.Point(48, 47);
			this.@__optBrowseFilenameLbl.Name = "__optBrowseFilenameLbl";
			this.@__optBrowseFilenameLbl.Size = new System.Drawing.Size(49, 13);
			this.@__optBrowseFilenameLbl.TabIndex = 1;
			this.@__optBrowseFilenameLbl.Text = "Filename";
			// 
			// __optPackages
			// 
			this.@__optPackages.AutoSize = true;
			this.@__optPackages.Location = new System.Drawing.Point(22, 74);
			this.@__optPackages.Name = "__optPackages";
			this.@__optPackages.Size = new System.Drawing.Size(199, 17);
			this.@__optPackages.TabIndex = 2;
			this.@__optPackages.TabStop = true;
			this.@__optPackages.Text = "A package embedded in this installer";
			this.@__optPackages.UseVisualStyleBackColor = true;
			// 
			// __optPackagesList
			// 
			this.@__optPackagesList.Location = new System.Drawing.Point(51, 97);
			this.@__optPackagesList.Name = "__optPackagesList";
			this.@__optPackagesList.Size = new System.Drawing.Size(321, 128);
			this.@__optPackagesList.TabIndex = 3;
			this.@__optPackagesList.UseCompatibleStateImageBehavior = false;
			// 
			// __optBrowseFilename
			// 
			this.@__optBrowseFilename.BackColor = System.Drawing.SystemColors.Window;
			this.@__optBrowseFilename.Location = new System.Drawing.Point(103, 44);
			this.@__optBrowseFilename.Name = "__optBrowseFilename";
			this.@__optBrowseFilename.ReadOnly = true;
			this.@__optBrowseFilename.Size = new System.Drawing.Size(188, 21);
			this.@__optBrowseFilename.TabIndex = 4;
			// 
			// __optBrowseBrowse
			// 
			this.@__optBrowseBrowse.Location = new System.Drawing.Point(297, 44);
			this.@__optBrowseBrowse.Name = "__optBrowseBrowse";
			this.@__optBrowseBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__optBrowseBrowse.TabIndex = 5;
			this.@__optBrowseBrowse.Text = "Browse...";
			this.@__optBrowseBrowse.UseVisualStyleBackColor = true;
			// 
			// __ofd
			// 
			this.@__ofd.Filter = "Anolis Package (*.anop)|*.anop|All files (*.*)|*.*";
			this.@__ofd.Title = "Select Anolis Package File";
			// 
			// SelectPackage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "SelectPackage";
			this.@__banner.ResumeLayout(false);
			this.@__content.ResumeLayout(false);
			this.@__content.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button __optBrowseBrowse;
		private System.Windows.Forms.TextBox __optBrowseFilename;
		private System.Windows.Forms.ListView __optPackagesList;
		private System.Windows.Forms.RadioButton __optPackages;
		private System.Windows.Forms.Label __optBrowseFilenameLbl;
		private System.Windows.Forms.RadioButton __optBrowse;
		private System.Windows.Forms.OpenFileDialog __ofd;
	}
}
