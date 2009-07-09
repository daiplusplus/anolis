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
			this.@__tabs = new System.Windows.Forms.TabControl();
			this.@__tPackages = new System.Windows.Forms.TabPage();
			this.@__packOrigLoad = new System.Windows.Forms.Button();
			this.@__packOrigBrowse = new System.Windows.Forms.Button();
			this.@__packOrigPath = new System.Windows.Forms.TextBox();
			this.@__packOrigLbl = new System.Windows.Forms.Label();
			this.@__packDistroSize = new System.Windows.Forms.Label();
			this.@__packAdd = new System.Windows.Forms.Button();
			this.@__packList = new System.Windows.Forms.ListView();
			this.@__packListColName = new System.Windows.Forms.ColumnHeader();
			this.@__packListColStatus = new System.Windows.Forms.ColumnHeader();
			this.@__packListColSize = new System.Windows.Forms.ColumnHeader();
			this.@__tCus = new System.Windows.Forms.TabPage();
			this.@__cusOptions = new System.Windows.Forms.GroupBox();
			this.@__cusOptsUpdateLbl = new System.Windows.Forms.Label();
			this.@__cusOptsUpdate = new System.Windows.Forms.CheckBox();
			this.@__cusOptsCheckDisableLbl = new System.Windows.Forms.Label();
			this.@__cusOptsCheckDisable = new System.Windows.Forms.CheckBox();
			this.@__cusOptsHideI386Lbl = new System.Windows.Forms.Label();
			this.@__cusOptsHideI386 = new System.Windows.Forms.CheckBox();
			this.@__cusOptSimpleLbl = new System.Windows.Forms.Label();
			this.@__cusOptSimple = new System.Windows.Forms.CheckBox();
			this.@__cusStrings = new System.Windows.Forms.GroupBox();
			this.@__cusStringDeveloper = new System.Windows.Forms.TextBox();
			this.@__cusStringDeveloperLbl = new System.Windows.Forms.Label();
			this.@__cusStringWebsite = new System.Windows.Forms.TextBox();
			this.@__cusStringWebsiteLbl = new System.Windows.Forms.Label();
			this.@__cusStringName = new System.Windows.Forms.TextBox();
			this.@__cusStringNameLbl = new System.Windows.Forms.Label();
			this.@__cusImages = new System.Windows.Forms.GroupBox();
			this.@__cusImagesBanner = new System.Windows.Forms.PictureBox();
			this.@__cusImagesBannerLbl = new System.Windows.Forms.Label();
			this.@__cusImagesWatermark = new System.Windows.Forms.PictureBox();
			this.@__cusImagesWatermarkLbl = new System.Windows.Forms.Label();
			this.@__create = new System.Windows.Forms.Button();
			this.@__cancel = new System.Windows.Forms.Button();
			this.@__ofdAnop = new System.Windows.Forms.OpenFileDialog();
			this.@__sfd = new System.Windows.Forms.SaveFileDialog();
			this.@__ofdInstaller = new System.Windows.Forms.OpenFileDialog();
			this.@__ofdImage = new System.Windows.Forms.OpenFileDialog();
			this.@__ofdIcon = new System.Windows.Forms.OpenFileDialog();
			this.@__tabs.SuspendLayout();
			this.@__tPackages.SuspendLayout();
			this.@__tCus.SuspendLayout();
			this.@__cusOptions.SuspendLayout();
			this.@__cusStrings.SuspendLayout();
			this.@__cusImages.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.@__cusImagesBanner)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.@__cusImagesWatermark)).BeginInit();
			this.SuspendLayout();
			// 
			// __tabs
			// 
			this.@__tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__tabs.Controls.Add(this.@__tPackages);
			this.@__tabs.Controls.Add(this.@__tCus);
			this.@__tabs.Location = new System.Drawing.Point(6, 7);
			this.@__tabs.Name = "__tabs";
			this.@__tabs.SelectedIndex = 0;
			this.@__tabs.Size = new System.Drawing.Size(561, 755);
			this.@__tabs.TabIndex = 3;
			// 
			// __tPackages
			// 
			this.@__tPackages.Controls.Add(this.@__packOrigLoad);
			this.@__tPackages.Controls.Add(this.@__packOrigBrowse);
			this.@__tPackages.Controls.Add(this.@__packOrigPath);
			this.@__tPackages.Controls.Add(this.@__packOrigLbl);
			this.@__tPackages.Controls.Add(this.@__packDistroSize);
			this.@__tPackages.Controls.Add(this.@__packAdd);
			this.@__tPackages.Controls.Add(this.@__packList);
			this.@__tPackages.Location = new System.Drawing.Point(4, 22);
			this.@__tPackages.Name = "__tPackages";
			this.@__tPackages.Padding = new System.Windows.Forms.Padding(3);
			this.@__tPackages.Size = new System.Drawing.Size(553, 729);
			this.@__tPackages.TabIndex = 0;
			this.@__tPackages.Text = "Embedded Packages";
			this.@__tPackages.UseVisualStyleBackColor = true;
			// 
			// __packOrigLoad
			// 
			this.@__packOrigLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__packOrigLoad.Location = new System.Drawing.Point(487, 4);
			this.@__packOrigLoad.Name = "__packOrigLoad";
			this.@__packOrigLoad.Size = new System.Drawing.Size(60, 23);
			this.@__packOrigLoad.TabIndex = 8;
			this.@__packOrigLoad.Text = "Load";
			this.@__packOrigLoad.UseVisualStyleBackColor = true;
			// 
			// __packOrigBrowse
			// 
			this.@__packOrigBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__packOrigBrowse.Location = new System.Drawing.Point(406, 4);
			this.@__packOrigBrowse.Name = "__packOrigBrowse";
			this.@__packOrigBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__packOrigBrowse.TabIndex = 7;
			this.@__packOrigBrowse.Text = "Browse...";
			this.@__packOrigBrowse.UseVisualStyleBackColor = true;
			// 
			// __packOrigPath
			// 
			this.@__packOrigPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__packOrigPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.@__packOrigPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.@__packOrigPath.Location = new System.Drawing.Point(93, 6);
			this.@__packOrigPath.Name = "__packOrigPath";
			this.@__packOrigPath.Size = new System.Drawing.Size(307, 20);
			this.@__packOrigPath.TabIndex = 6;
			// 
			// __packOrigLbl
			// 
			this.@__packOrigLbl.AutoSize = true;
			this.@__packOrigLbl.Location = new System.Drawing.Point(6, 9);
			this.@__packOrigLbl.Name = "__packOrigLbl";
			this.@__packOrigLbl.Size = new System.Drawing.Size(81, 13);
			this.@__packOrigLbl.TabIndex = 5;
			this.@__packOrigLbl.Text = "Original Installer";
			// 
			// __packDistroSize
			// 
			this.@__packDistroSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__packDistroSize.Location = new System.Drawing.Point(271, 705);
			this.@__packDistroSize.Name = "__packDistroSize";
			this.@__packDistroSize.Size = new System.Drawing.Size(276, 21);
			this.@__packDistroSize.TabIndex = 4;
			this.@__packDistroSize.Text = "Estimated Distribution Size: {0}";
			this.@__packDistroSize.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __packAdd
			// 
			this.@__packAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__packAdd.Location = new System.Drawing.Point(6, 700);
			this.@__packAdd.Name = "__packAdd";
			this.@__packAdd.Size = new System.Drawing.Size(123, 23);
			this.@__packAdd.TabIndex = 2;
			this.@__packAdd.Text = "Add Package(s)...";
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
            this.@__packListColStatus,
            this.@__packListColSize});
			this.@__packList.Location = new System.Drawing.Point(6, 33);
			this.@__packList.Name = "__packList";
			this.@__packList.Size = new System.Drawing.Size(541, 661);
			this.@__packList.TabIndex = 0;
			this.@__packList.UseCompatibleStateImageBehavior = false;
			this.@__packList.View = System.Windows.Forms.View.Details;
			// 
			// __packListColName
			// 
			this.@__packListColName.Text = "Name";
			this.@__packListColName.Width = 167;
			// 
			// __packListColStatus
			// 
			this.@__packListColStatus.Text = "Status";
			this.@__packListColStatus.Width = 70;
			// 
			// __packListColSize
			// 
			this.@__packListColSize.Text = "Size";
			this.@__packListColSize.Width = 78;
			// 
			// __tCus
			// 
			this.@__tCus.AutoScroll = true;
			this.@__tCus.Controls.Add(this.@__cusOptions);
			this.@__tCus.Controls.Add(this.@__cusStrings);
			this.@__tCus.Controls.Add(this.@__cusImages);
			this.@__tCus.Location = new System.Drawing.Point(4, 22);
			this.@__tCus.Name = "__tCus";
			this.@__tCus.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.@__tCus.Size = new System.Drawing.Size(553, 729);
			this.@__tCus.TabIndex = 2;
			this.@__tCus.Text = "Installer Customization";
			this.@__tCus.UseVisualStyleBackColor = true;
			// 
			// __cusOptions
			// 
			this.@__cusOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__cusOptions.Controls.Add(this.@__cusOptsUpdateLbl);
			this.@__cusOptions.Controls.Add(this.@__cusOptsUpdate);
			this.@__cusOptions.Controls.Add(this.@__cusOptsCheckDisableLbl);
			this.@__cusOptions.Controls.Add(this.@__cusOptsCheckDisable);
			this.@__cusOptions.Controls.Add(this.@__cusOptsHideI386Lbl);
			this.@__cusOptions.Controls.Add(this.@__cusOptsHideI386);
			this.@__cusOptions.Controls.Add(this.@__cusOptSimpleLbl);
			this.@__cusOptions.Controls.Add(this.@__cusOptSimple);
			this.@__cusOptions.Location = new System.Drawing.Point(12, 320);
			this.@__cusOptions.Name = "__cusOptions";
			this.@__cusOptions.Size = new System.Drawing.Size(532, 222);
			this.@__cusOptions.TabIndex = 10;
			this.@__cusOptions.TabStop = false;
			this.@__cusOptions.Text = "Options";
			// 
			// __cusOptsUpdateLbl
			// 
			this.@__cusOptsUpdateLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__cusOptsUpdateLbl.Location = new System.Drawing.Point(25, 188);
			this.@__cusOptsUpdateLbl.Name = "__cusOptsUpdateLbl";
			this.@__cusOptsUpdateLbl.Size = new System.Drawing.Size(501, 34);
			this.@__cusOptsUpdateLbl.TabIndex = 7;
			this.@__cusOptsUpdateLbl.Text = "The installer will not attempt to check for updates of the package or the install" +
				"er software.";
			// 
			// __cusOptsUpdate
			// 
			this.@__cusOptsUpdate.AutoSize = true;
			this.@__cusOptsUpdate.Location = new System.Drawing.Point(9, 168);
			this.@__cusOptsUpdate.Name = "__cusOptsUpdate";
			this.@__cusOptsUpdate.Size = new System.Drawing.Size(133, 17);
			this.@__cusOptsUpdate.TabIndex = 6;
			this.@__cusOptsUpdate.Text = "Disable Update Check";
			this.@__cusOptsUpdate.UseVisualStyleBackColor = true;
			// 
			// __cusOptsCheckDisableLbl
			// 
			this.@__cusOptsCheckDisableLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__cusOptsCheckDisableLbl.Location = new System.Drawing.Point(25, 147);
			this.@__cusOptsCheckDisableLbl.Name = "__cusOptsCheckDisableLbl";
			this.@__cusOptsCheckDisableLbl.Size = new System.Drawing.Size(501, 34);
			this.@__cusOptsCheckDisableLbl.TabIndex = 5;
			this.@__cusOptsCheckDisableLbl.Text = "Any precondition specified in the package will not be evaluated prior to installi" +
				"ng (not recommended).";
			// 
			// __cusOptsCheckDisable
			// 
			this.@__cusOptsCheckDisable.AutoSize = true;
			this.@__cusOptsCheckDisable.Location = new System.Drawing.Point(9, 127);
			this.@__cusOptsCheckDisable.Name = "__cusOptsCheckDisable";
			this.@__cusOptsCheckDisable.Size = new System.Drawing.Size(188, 17);
			this.@__cusOptsCheckDisable.TabIndex = 4;
			this.@__cusOptsCheckDisable.Text = "Disable Package Condition Check";
			this.@__cusOptsCheckDisable.UseVisualStyleBackColor = true;
			// 
			// __cusOptsHideI386Lbl
			// 
			this.@__cusOptsHideI386Lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__cusOptsHideI386Lbl.Location = new System.Drawing.Point(25, 96);
			this.@__cusOptsHideI386Lbl.Name = "__cusOptsHideI386Lbl";
			this.@__cusOptsHideI386Lbl.Size = new System.Drawing.Size(501, 34);
			this.@__cusOptsHideI386Lbl.TabIndex = 3;
			this.@__cusOptsHideI386Lbl.Text = "Removes the options to patch an I386 directory from within the installer. Note th" +
				"at /devmode switch overrides this.";
			// 
			// __cusOptsHideI386
			// 
			this.@__cusOptsHideI386.AutoSize = true;
			this.@__cusOptsHideI386.Location = new System.Drawing.Point(9, 76);
			this.@__cusOptsHideI386.Name = "__cusOptsHideI386";
			this.@__cusOptsHideI386.Size = new System.Drawing.Size(151, 17);
			this.@__cusOptsHideI386.TabIndex = 2;
			this.@__cusOptsHideI386.Text = "Hide I386 Patching Option";
			this.@__cusOptsHideI386.UseVisualStyleBackColor = true;
			// 
			// __cusOptSimpleLbl
			// 
			this.@__cusOptSimpleLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__cusOptSimpleLbl.Location = new System.Drawing.Point(25, 39);
			this.@__cusOptSimpleLbl.Name = "__cusOptSimpleLbl";
			this.@__cusOptSimpleLbl.Size = new System.Drawing.Size(501, 34);
			this.@__cusOptSimpleLbl.TabIndex = 1;
			this.@__cusOptSimpleLbl.Text = "Users will be presented with a streamlined wizard that automatically selects the " +
				"first embedded package and hides the UI to select other packages.";
			// 
			// __cusOptSimple
			// 
			this.@__cusOptSimple.AutoSize = true;
			this.@__cusOptSimple.Location = new System.Drawing.Point(9, 19);
			this.@__cusOptSimple.Name = "__cusOptSimple";
			this.@__cusOptSimple.Size = new System.Drawing.Size(192, 17);
			this.@__cusOptSimple.TabIndex = 0;
			this.@__cusOptSimple.Text = "Simple UI / Install Default Package";
			this.@__cusOptSimple.UseVisualStyleBackColor = true;
			// 
			// __cusStrings
			// 
			this.@__cusStrings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__cusStrings.Controls.Add(this.@__cusStringDeveloper);
			this.@__cusStrings.Controls.Add(this.@__cusStringDeveloperLbl);
			this.@__cusStrings.Controls.Add(this.@__cusStringWebsite);
			this.@__cusStrings.Controls.Add(this.@__cusStringWebsiteLbl);
			this.@__cusStrings.Controls.Add(this.@__cusStringName);
			this.@__cusStrings.Controls.Add(this.@__cusStringNameLbl);
			this.@__cusStrings.Location = new System.Drawing.Point(12, 210);
			this.@__cusStrings.Name = "__cusStrings";
			this.@__cusStrings.Size = new System.Drawing.Size(532, 104);
			this.@__cusStrings.TabIndex = 9;
			this.@__cusStrings.TabStop = false;
			this.@__cusStrings.Text = "Strings";
			// 
			// __cusStringDeveloper
			// 
			this.@__cusStringDeveloper.Location = new System.Drawing.Point(111, 72);
			this.@__cusStringDeveloper.Name = "__cusStringDeveloper";
			this.@__cusStringDeveloper.Size = new System.Drawing.Size(168, 20);
			this.@__cusStringDeveloper.TabIndex = 5;
			// 
			// __cusStringDeveloperLbl
			// 
			this.@__cusStringDeveloperLbl.Location = new System.Drawing.Point(6, 75);
			this.@__cusStringDeveloperLbl.Name = "__cusStringDeveloperLbl";
			this.@__cusStringDeveloperLbl.Size = new System.Drawing.Size(99, 19);
			this.@__cusStringDeveloperLbl.TabIndex = 4;
			this.@__cusStringDeveloperLbl.Text = "Developer";
			this.@__cusStringDeveloperLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __cusStringWebsite
			// 
			this.@__cusStringWebsite.Location = new System.Drawing.Point(111, 46);
			this.@__cusStringWebsite.Name = "__cusStringWebsite";
			this.@__cusStringWebsite.Size = new System.Drawing.Size(168, 20);
			this.@__cusStringWebsite.TabIndex = 3;
			// 
			// __cusStringWebsiteLbl
			// 
			this.@__cusStringWebsiteLbl.Location = new System.Drawing.Point(6, 49);
			this.@__cusStringWebsiteLbl.Name = "__cusStringWebsiteLbl";
			this.@__cusStringWebsiteLbl.Size = new System.Drawing.Size(99, 19);
			this.@__cusStringWebsiteLbl.TabIndex = 2;
			this.@__cusStringWebsiteLbl.Text = "Website";
			this.@__cusStringWebsiteLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __cusStringName
			// 
			this.@__cusStringName.Location = new System.Drawing.Point(111, 19);
			this.@__cusStringName.Name = "__cusStringName";
			this.@__cusStringName.Size = new System.Drawing.Size(168, 20);
			this.@__cusStringName.TabIndex = 1;
			// 
			// __cusStringNameLbl
			// 
			this.@__cusStringNameLbl.Location = new System.Drawing.Point(6, 22);
			this.@__cusStringNameLbl.Name = "__cusStringNameLbl";
			this.@__cusStringNameLbl.Size = new System.Drawing.Size(99, 19);
			this.@__cusStringNameLbl.TabIndex = 0;
			this.@__cusStringNameLbl.Text = "Installer Name";
			this.@__cusStringNameLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __cusImages
			// 
			this.@__cusImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__cusImages.Controls.Add(this.@__cusImagesBanner);
			this.@__cusImages.Controls.Add(this.@__cusImagesBannerLbl);
			this.@__cusImages.Controls.Add(this.@__cusImagesWatermark);
			this.@__cusImages.Controls.Add(this.@__cusImagesWatermarkLbl);
			this.@__cusImages.Location = new System.Drawing.Point(12, 9);
			this.@__cusImages.Name = "__cusImages";
			this.@__cusImages.Size = new System.Drawing.Size(532, 195);
			this.@__cusImages.TabIndex = 8;
			this.@__cusImages.TabStop = false;
			this.@__cusImages.Text = "Images";
			// 
			// __cusImagesBanner
			// 
			this.@__cusImagesBanner.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.@__cusImagesBanner.Cursor = System.Windows.Forms.Cursors.Hand;
			this.@__cusImagesBanner.Location = new System.Drawing.Point(147, 55);
			this.@__cusImagesBanner.Name = "__cusImagesBanner";
			this.@__cusImagesBanner.Size = new System.Drawing.Size(132, 134);
			this.@__cusImagesBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.@__cusImagesBanner.TabIndex = 3;
			this.@__cusImagesBanner.TabStop = false;
			// 
			// __cusImagesBannerLbl
			// 
			this.@__cusImagesBannerLbl.AutoSize = true;
			this.@__cusImagesBannerLbl.Location = new System.Drawing.Point(144, 26);
			this.@__cusImagesBannerLbl.Name = "__cusImagesBannerLbl";
			this.@__cusImagesBannerLbl.Size = new System.Drawing.Size(117, 26);
			this.@__cusImagesBannerLbl.TabIndex = 2;
			this.@__cusImagesBannerLbl.Text = "Banner Image\r\n100x58 Recommended";
			// 
			// __cusImagesWatermark
			// 
			this.@__cusImagesWatermark.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.@__cusImagesWatermark.Cursor = System.Windows.Forms.Cursors.Hand;
			this.@__cusImagesWatermark.Location = new System.Drawing.Point(9, 55);
			this.@__cusImagesWatermark.Name = "__cusImagesWatermark";
			this.@__cusImagesWatermark.Size = new System.Drawing.Size(132, 134);
			this.@__cusImagesWatermark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.@__cusImagesWatermark.TabIndex = 1;
			this.@__cusImagesWatermark.TabStop = false;
			// 
			// __cusImagesWatermarkLbl
			// 
			this.@__cusImagesWatermarkLbl.AutoSize = true;
			this.@__cusImagesWatermarkLbl.Location = new System.Drawing.Point(6, 26);
			this.@__cusImagesWatermarkLbl.Name = "__cusImagesWatermarkLbl";
			this.@__cusImagesWatermarkLbl.Size = new System.Drawing.Size(123, 26);
			this.@__cusImagesWatermarkLbl.TabIndex = 0;
			this.@__cusImagesWatermarkLbl.Text = "Watermark Image\r\n164x314 Recommended";
			// 
			// __create
			// 
			this.@__create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__create.Location = new System.Drawing.Point(361, 766);
			this.@__create.Name = "__create";
			this.@__create.Size = new System.Drawing.Size(121, 23);
			this.@__create.TabIndex = 4;
			this.@__create.Text = "Create Distribution...";
			this.@__create.UseVisualStyleBackColor = true;
			// 
			// __cancel
			// 
			this.@__cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__cancel.Location = new System.Drawing.Point(488, 766);
			this.@__cancel.Name = "__cancel";
			this.@__cancel.Size = new System.Drawing.Size(75, 23);
			this.@__cancel.TabIndex = 5;
			this.@__cancel.Text = "Cancel";
			this.@__cancel.UseVisualStyleBackColor = true;
			// 
			// __ofdAnop
			// 
			this.@__ofdAnop.Filter = "Anolis Packages (*.anop)|*.anop|LZMA Tarballs (*.tar.lzma)|*.tar.lzma";
			this.@__ofdAnop.Multiselect = true;
			this.@__ofdAnop.SupportMultiDottedExtensions = true;
			// 
			// __sfd
			// 
			this.@__sfd.DefaultExt = "exe";
			this.@__sfd.Filter = "Executable (*.exe)|*.exe";
			// 
			// __ofdInstaller
			// 
			this.@__ofdInstaller.Filter = "Executables (*.exe)|*.exe";
			// 
			// __ofdImage
			// 
			this.@__ofdImage.Filter = "Images (*.jpg;*.bmp;*.png;*.gif)|*.jpg;*.bmp;*.png;*.gif";
			// 
			// __ofdIcon
			// 
			this.@__ofdIcon.FileName = "Icon Files (*.ico)|*.ico";
			// 
			// DistributorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(571, 797);
			this.Controls.Add(this.@__cancel);
			this.Controls.Add(this.@__create);
			this.Controls.Add(this.@__tabs);
			this.Name = "DistributorForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Distributor";
			this.@__tabs.ResumeLayout(false);
			this.@__tPackages.ResumeLayout(false);
			this.@__tPackages.PerformLayout();
			this.@__tCus.ResumeLayout(false);
			this.@__cusOptions.ResumeLayout(false);
			this.@__cusOptions.PerformLayout();
			this.@__cusStrings.ResumeLayout(false);
			this.@__cusStrings.PerformLayout();
			this.@__cusImages.ResumeLayout(false);
			this.@__cusImages.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.@__cusImagesBanner)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.@__cusImagesWatermark)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl __tabs;
		private System.Windows.Forms.TabPage __tPackages;
		private System.Windows.Forms.Button __create;
		private System.Windows.Forms.Button __cancel;
		private System.Windows.Forms.TabPage __tCus;
		private System.Windows.Forms.Button __packAdd;
		private System.Windows.Forms.ListView __packList;
		private System.Windows.Forms.ColumnHeader __packListColName;
		private System.Windows.Forms.ColumnHeader __packListColSize;
		private System.Windows.Forms.OpenFileDialog __ofdAnop;
		private System.Windows.Forms.Label __packDistroSize;
		private System.Windows.Forms.ColumnHeader __packListColStatus;
		private System.Windows.Forms.Label __packOrigLbl;
		private System.Windows.Forms.Button __packOrigBrowse;
		private System.Windows.Forms.TextBox __packOrigPath;
		private System.Windows.Forms.Button __packOrigLoad;
		private System.Windows.Forms.SaveFileDialog __sfd;
		private System.Windows.Forms.OpenFileDialog __ofdInstaller;
		private System.Windows.Forms.GroupBox __cusStrings;
		private System.Windows.Forms.GroupBox __cusImages;
		private System.Windows.Forms.GroupBox __cusOptions;
		private System.Windows.Forms.Label __cusOptSimpleLbl;
		private System.Windows.Forms.CheckBox __cusOptSimple;
		private System.Windows.Forms.PictureBox __cusImagesBanner;
		private System.Windows.Forms.Label __cusImagesBannerLbl;
		private System.Windows.Forms.PictureBox __cusImagesWatermark;
		private System.Windows.Forms.Label __cusImagesWatermarkLbl;
		private System.Windows.Forms.Label __cusOptsHideI386Lbl;
		private System.Windows.Forms.CheckBox __cusOptsHideI386;
		private System.Windows.Forms.Label __cusOptsCheckDisableLbl;
		private System.Windows.Forms.CheckBox __cusOptsCheckDisable;
		private System.Windows.Forms.OpenFileDialog __ofdImage;
		private System.Windows.Forms.OpenFileDialog __ofdIcon;
		private System.Windows.Forms.TextBox __cusStringDeveloper;
		private System.Windows.Forms.Label __cusStringDeveloperLbl;
		private System.Windows.Forms.TextBox __cusStringWebsite;
		private System.Windows.Forms.Label __cusStringWebsiteLbl;
		private System.Windows.Forms.TextBox __cusStringName;
		private System.Windows.Forms.Label __cusStringNameLbl;
		private System.Windows.Forms.Label __cusOptsUpdateLbl;
		private System.Windows.Forms.CheckBox __cusOptsUpdate;
	}
}