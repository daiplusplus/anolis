namespace Anolis.Packager {
	partial class DistributorForm {
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Installer Options");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Welcome");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Select Action");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Select Package");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Extracting...");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Modify Package");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Installation Options");
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Installing...");
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Install Package", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Select Backup");
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Uninstalling...");
			System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Uninstall Package", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
			System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Destination");
			System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Downloading...");
			System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Download Tools", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
			System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Finished");
			this.@__tabs = new System.Windows.Forms.TabControl();
			this.@__tPackages = new System.Windows.Forms.TabPage();
			this.@__packDistroSize = new System.Windows.Forms.Label();
			this.@__packListLbl = new System.Windows.Forms.Label();
			this.@__packAdd = new System.Windows.Forms.Button();
			this.@__packList = new System.Windows.Forms.ListView();
			this.@__packListColName = new System.Windows.Forms.ColumnHeader();
			this.@__packListColSize = new System.Windows.Forms.ColumnHeader();
			this.@__tPages = new System.Windows.Forms.TabPage();
			this.@__pagesOptions = new System.Windows.Forms.TabControl();
			this.@__pagesPageOptions = new System.Windows.Forms.TabPage();
			this.@__pagesPageStrings = new System.Windows.Forms.TabPage();
			this.@__pagesPageImages = new System.Windows.Forms.TabPage();
			this.@__pagesTree = new System.Windows.Forms.TreeView();
			this.@__create = new System.Windows.Forms.Button();
			this.@__cancel = new System.Windows.Forms.Button();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__tabs.SuspendLayout();
			this.@__tPackages.SuspendLayout();
			this.@__tPages.SuspendLayout();
			this.@__pagesOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// __tabs
			// 
			this.@__tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__tabs.Controls.Add(this.@__tPackages);
			this.@__tabs.Controls.Add(this.@__tPages);
			this.@__tabs.Location = new System.Drawing.Point(6, 7);
			this.@__tabs.Name = "__tabs";
			this.@__tabs.SelectedIndex = 0;
			this.@__tabs.Size = new System.Drawing.Size(554, 355);
			this.@__tabs.TabIndex = 3;
			// 
			// __tPackages
			// 
			this.@__tPackages.Controls.Add(this.@__packDistroSize);
			this.@__tPackages.Controls.Add(this.@__packListLbl);
			this.@__tPackages.Controls.Add(this.@__packAdd);
			this.@__tPackages.Controls.Add(this.@__packList);
			this.@__tPackages.Location = new System.Drawing.Point(4, 22);
			this.@__tPackages.Name = "__tPackages";
			this.@__tPackages.Padding = new System.Windows.Forms.Padding(3);
			this.@__tPackages.Size = new System.Drawing.Size(546, 329);
			this.@__tPackages.TabIndex = 0;
			this.@__tPackages.Text = "Embedded Packages";
			this.@__tPackages.UseVisualStyleBackColor = true;
			// 
			// __packDistroSize
			// 
			this.@__packDistroSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__packDistroSize.Location = new System.Drawing.Point(264, 305);
			this.@__packDistroSize.Name = "__packDistroSize";
			this.@__packDistroSize.Size = new System.Drawing.Size(276, 21);
			this.@__packDistroSize.TabIndex = 4;
			this.@__packDistroSize.Text = "Estimated Distribution Size: {0}";
			this.@__packDistroSize.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __packListLbl
			// 
			this.@__packListLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__packListLbl.Location = new System.Drawing.Point(6, 3);
			this.@__packListLbl.Name = "__packListLbl";
			this.@__packListLbl.Size = new System.Drawing.Size(534, 18);
			this.@__packListLbl.TabIndex = 3;
			this.@__packListLbl.Text = "Unchecked packages will be removed when the distribution is created";
			// 
			// __packAdd
			// 
			this.@__packAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__packAdd.Location = new System.Drawing.Point(6, 300);
			this.@__packAdd.Name = "__packAdd";
			this.@__packAdd.Size = new System.Drawing.Size(123, 23);
			this.@__packAdd.TabIndex = 2;
			this.@__packAdd.Text = "Add Package...";
			this.@__packAdd.UseVisualStyleBackColor = true;
			// 
			// __packList
			// 
			this.@__packList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__packList.CheckBoxes = true;
			this.@__packList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.@__packListColName,
            this.@__packListColSize});
			this.@__packList.Location = new System.Drawing.Point(6, 24);
			this.@__packList.Name = "__packList";
			this.@__packList.Size = new System.Drawing.Size(534, 270);
			this.@__packList.TabIndex = 0;
			this.@__packList.UseCompatibleStateImageBehavior = false;
			this.@__packList.View = System.Windows.Forms.View.Details;
			// 
			// __packListColName
			// 
			this.@__packListColName.Text = "Name";
			this.@__packListColName.Width = 167;
			// 
			// __packListColSize
			// 
			this.@__packListColSize.Text = "Size";
			this.@__packListColSize.Width = 78;
			// 
			// __tPages
			// 
			this.@__tPages.Controls.Add(this.@__pagesOptions);
			this.@__tPages.Controls.Add(this.@__pagesTree);
			this.@__tPages.Location = new System.Drawing.Point(4, 22);
			this.@__tPages.Name = "__tPages";
			this.@__tPages.Size = new System.Drawing.Size(546, 329);
			this.@__tPages.TabIndex = 2;
			this.@__tPages.Text = "Pages and Options";
			this.@__tPages.UseVisualStyleBackColor = true;
			// 
			// __pagesOptions
			// 
			this.@__pagesOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__pagesOptions.Controls.Add(this.@__pagesPageOptions);
			this.@__pagesOptions.Controls.Add(this.@__pagesPageStrings);
			this.@__pagesOptions.Controls.Add(this.@__pagesPageImages);
			this.@__pagesOptions.Location = new System.Drawing.Point(182, 3);
			this.@__pagesOptions.Name = "__pagesOptions";
			this.@__pagesOptions.SelectedIndex = 0;
			this.@__pagesOptions.Size = new System.Drawing.Size(360, 323);
			this.@__pagesOptions.TabIndex = 2;
			// 
			// __pagesPageOptions
			// 
			this.@__pagesPageOptions.Location = new System.Drawing.Point(4, 22);
			this.@__pagesPageOptions.Name = "__pagesPageOptions";
			this.@__pagesPageOptions.Padding = new System.Windows.Forms.Padding(3);
			this.@__pagesPageOptions.Size = new System.Drawing.Size(352, 297);
			this.@__pagesPageOptions.TabIndex = 0;
			this.@__pagesPageOptions.Text = "Options";
			this.@__pagesPageOptions.UseVisualStyleBackColor = true;
			// 
			// __pagesPageStrings
			// 
			this.@__pagesPageStrings.Location = new System.Drawing.Point(4, 22);
			this.@__pagesPageStrings.Name = "__pagesPageStrings";
			this.@__pagesPageStrings.Padding = new System.Windows.Forms.Padding(3);
			this.@__pagesPageStrings.Size = new System.Drawing.Size(369, 259);
			this.@__pagesPageStrings.TabIndex = 1;
			this.@__pagesPageStrings.Text = "Strings";
			this.@__pagesPageStrings.UseVisualStyleBackColor = true;
			// 
			// __pagesPageImages
			// 
			this.@__pagesPageImages.Location = new System.Drawing.Point(4, 22);
			this.@__pagesPageImages.Name = "__pagesPageImages";
			this.@__pagesPageImages.Size = new System.Drawing.Size(369, 259);
			this.@__pagesPageImages.TabIndex = 2;
			this.@__pagesPageImages.Text = "Images";
			this.@__pagesPageImages.UseVisualStyleBackColor = true;
			// 
			// __pagesTree
			// 
			this.@__pagesTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.@__pagesTree.Location = new System.Drawing.Point(3, 3);
			this.@__pagesTree.Name = "__pagesTree";
			treeNode1.Name = "__pInstaller";
			treeNode1.Text = "Installer Options";
			treeNode2.Name = "__pWelcome";
			treeNode2.Text = "Welcome";
			treeNode3.Name = "__pAction";
			treeNode3.Text = "Select Action";
			treeNode4.Name = "__pInstallC";
			treeNode4.Text = "Select Package";
			treeNode5.Name = "__pInstallD";
			treeNode5.Text = "Extracting...";
			treeNode6.Name = "__pInstallE";
			treeNode6.Text = "Modify Package";
			treeNode7.Name = "__pInstallF";
			treeNode7.Text = "Installation Options";
			treeNode8.Name = "__pInstallG";
			treeNode8.Text = "Installing...";
			treeNode9.Name = "__pInstall";
			treeNode9.Text = "Install Package";
			treeNode10.Name = "__pUninstallC";
			treeNode10.Text = "Select Backup";
			treeNode11.Name = "__pUninstallD";
			treeNode11.Text = "Uninstalling...";
			treeNode12.Name = "__pUninstall";
			treeNode12.Text = "Uninstall Package";
			treeNode13.Name = "__pDownloadC";
			treeNode13.Text = "Destination";
			treeNode14.Name = "__pDownloadD";
			treeNode14.Text = "Downloading...";
			treeNode15.Name = "__pDownload";
			treeNode15.Text = "Download Tools";
			treeNode16.Name = "__pFinished";
			treeNode16.Text = "Finished";
			this.@__pagesTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode9,
            treeNode12,
            treeNode15,
            treeNode16});
			this.@__pagesTree.Size = new System.Drawing.Size(173, 321);
			this.@__pagesTree.TabIndex = 1;
			// 
			// __create
			// 
			this.@__create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__create.Location = new System.Drawing.Point(354, 366);
			this.@__create.Name = "__create";
			this.@__create.Size = new System.Drawing.Size(121, 23);
			this.@__create.TabIndex = 4;
			this.@__create.Text = "Create Distribution...";
			this.@__create.UseVisualStyleBackColor = true;
			// 
			// __cancel
			// 
			this.@__cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__cancel.Location = new System.Drawing.Point(481, 366);
			this.@__cancel.Name = "__cancel";
			this.@__cancel.Size = new System.Drawing.Size(75, 23);
			this.@__cancel.TabIndex = 5;
			this.@__cancel.Text = "Cancel";
			this.@__cancel.UseVisualStyleBackColor = true;
			// 
			// __ofd
			// 
			this.@__ofd.Filter = "Anolis Packages (*.anop)|*.anop|LZMA-compressed Tarballs (*.tar.lzma)|*.tar.lzma";
			// 
			// DistributorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(564, 397);
			this.Controls.Add(this.@__cancel);
			this.Controls.Add(this.@__create);
			this.Controls.Add(this.@__tabs);
			this.Name = "DistributorForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Distributor";
			this.@__tabs.ResumeLayout(false);
			this.@__tPackages.ResumeLayout(false);
			this.@__tPages.ResumeLayout(false);
			this.@__pagesOptions.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl __tabs;
		private System.Windows.Forms.TabPage __tPackages;
		private System.Windows.Forms.Button __create;
		private System.Windows.Forms.Button __cancel;
		private System.Windows.Forms.TabPage __tPages;
		private System.Windows.Forms.Label __packListLbl;
		private System.Windows.Forms.Button __packAdd;
		private System.Windows.Forms.ListView __packList;
		private System.Windows.Forms.ColumnHeader __packListColName;
		private System.Windows.Forms.ColumnHeader __packListColSize;
		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.TreeView __pagesTree;
		private System.Windows.Forms.Label __packDistroSize;
		private System.Windows.Forms.TabControl __pagesOptions;
		private System.Windows.Forms.TabPage __pagesPageOptions;
		private System.Windows.Forms.TabPage __pagesPageStrings;
		private System.Windows.Forms.TabPage __pagesPageImages;
	}
}