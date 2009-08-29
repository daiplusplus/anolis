namespace Anolis.Packager {
	partial class OptimizerForm {
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
			this.@__fileNameLbl = new System.Windows.Forms.Label();
			this.@__fileName = new System.Windows.Forms.TextBox();
			this.@__load = new System.Windows.Forms.Button();
			this.@__loadBrowse = new System.Windows.Forms.Button();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__tabs = new System.Windows.Forms.TabControl();
			this.@__tabMessages = new System.Windows.Forms.TabPage();
			this.@__messages = new System.Windows.Forms.ListBox();
			this.@__tabFiles = new System.Windows.Forms.TabPage();
			this.@__layout = new System.Windows.Forms.TableLayoutPanel();
			this.@__missingFiles = new System.Windows.Forms.ListBox();
			this.@__unreferencedLbl = new System.Windows.Forms.Label();
			this.@__unreferencedFiles = new System.Windows.Forms.ListBox();
			this.@__missingLbl = new System.Windows.Forms.Label();
			this.@__tabDupes = new System.Windows.Forms.TabPage();
			this.@__duplicatesLbl = new System.Windows.Forms.Label();
			this.@__duplicateFiles = new System.Windows.Forms.ListView();
			this.@__dupeColName = new System.Windows.Forms.ColumnHeader();
			this.@__tabComps = new System.Windows.Forms.TabPage();
			this.@__compPreview = new System.Windows.Forms.PictureBox();
			this.@__compImages = new System.Windows.Forms.ListBox();
			this.@__compLayers = new System.Windows.Forms.ListBox();
			this.@__fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.@__bwLoad = new System.ComponentModel.BackgroundWorker();
			this.@__sfd = new System.Windows.Forms.SaveFileDialog();
			this.@__destBrowse = new System.Windows.Forms.Button();
			this.@__destPath = new System.Windows.Forms.TextBox();
			this.@__destLbl = new System.Windows.Forms.Label();
			this.@__dest = new System.Windows.Forms.Button();
			this.@__bwDest = new System.ComponentModel.BackgroundWorker();
			this.@__tabs.SuspendLayout();
			this.@__tabMessages.SuspendLayout();
			this.@__tabFiles.SuspendLayout();
			this.@__layout.SuspendLayout();
			this.@__tabDupes.SuspendLayout();
			this.@__tabComps.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.@__compPreview)).BeginInit();
			this.SuspendLayout();
			// 
			// __fileNameLbl
			// 
			this.@__fileNameLbl.AutoSize = true;
			this.@__fileNameLbl.Location = new System.Drawing.Point(12, 9);
			this.@__fileNameLbl.Name = "__fileNameLbl";
			this.@__fileNameLbl.Size = new System.Drawing.Size(120, 13);
			this.@__fileNameLbl.TabIndex = 2;
			this.@__fileNameLbl.Text = "Package XML Filename";
			// 
			// __fileName
			// 
			this.@__fileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__fileName.Location = new System.Drawing.Point(138, 6);
			this.@__fileName.Name = "__fileName";
			this.@__fileName.Size = new System.Drawing.Size(515, 20);
			this.@__fileName.TabIndex = 3;
			// 
			// __load
			// 
			this.@__load.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__load.Location = new System.Drawing.Point(740, 4);
			this.@__load.Name = "__load";
			this.@__load.Size = new System.Drawing.Size(75, 23);
			this.@__load.TabIndex = 4;
			this.@__load.Text = "Load";
			this.@__load.UseVisualStyleBackColor = true;
			// 
			// __loadBrowse
			// 
			this.@__loadBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__loadBrowse.Location = new System.Drawing.Point(659, 4);
			this.@__loadBrowse.Name = "__loadBrowse";
			this.@__loadBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__loadBrowse.TabIndex = 5;
			this.@__loadBrowse.Text = "Browse...";
			this.@__loadBrowse.UseVisualStyleBackColor = true;
			// 
			// __ofd
			// 
			this.@__ofd.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*";
			// 
			// __tabs
			// 
			this.@__tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__tabs.Controls.Add(this.@__tabMessages);
			this.@__tabs.Controls.Add(this.@__tabFiles);
			this.@__tabs.Controls.Add(this.@__tabDupes);
			this.@__tabs.Controls.Add(this.@__tabComps);
			this.@__tabs.Location = new System.Drawing.Point(15, 59);
			this.@__tabs.Name = "__tabs";
			this.@__tabs.SelectedIndex = 0;
			this.@__tabs.Size = new System.Drawing.Size(800, 466);
			this.@__tabs.TabIndex = 6;
			// 
			// __tabMessages
			// 
			this.@__tabMessages.Controls.Add(this.@__messages);
			this.@__tabMessages.Location = new System.Drawing.Point(4, 22);
			this.@__tabMessages.Name = "__tabMessages";
			this.@__tabMessages.Padding = new System.Windows.Forms.Padding(3);
			this.@__tabMessages.Size = new System.Drawing.Size(792, 440);
			this.@__tabMessages.TabIndex = 0;
			this.@__tabMessages.Text = "Validation Messages";
			this.@__tabMessages.UseVisualStyleBackColor = true;
			// 
			// __messages
			// 
			this.@__messages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__messages.FormattingEnabled = true;
			this.@__messages.IntegralHeight = false;
			this.@__messages.Location = new System.Drawing.Point(3, 3);
			this.@__messages.Name = "__messages";
			this.@__messages.Size = new System.Drawing.Size(786, 434);
			this.@__messages.TabIndex = 0;
			// 
			// __tabFiles
			// 
			this.@__tabFiles.Controls.Add(this.@__layout);
			this.@__tabFiles.Location = new System.Drawing.Point(4, 22);
			this.@__tabFiles.Name = "__tabFiles";
			this.@__tabFiles.Padding = new System.Windows.Forms.Padding(3);
			this.@__tabFiles.Size = new System.Drawing.Size(792, 440);
			this.@__tabFiles.TabIndex = 1;
			this.@__tabFiles.Text = "Missing and Unreferenced Files";
			this.@__tabFiles.UseVisualStyleBackColor = true;
			// 
			// __layout
			// 
			this.@__layout.ColumnCount = 2;
			this.@__layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.@__layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.@__layout.Controls.Add(this.@__missingFiles, 0, 1);
			this.@__layout.Controls.Add(this.@__unreferencedLbl, 1, 0);
			this.@__layout.Controls.Add(this.@__unreferencedFiles, 1, 1);
			this.@__layout.Controls.Add(this.@__missingLbl, 0, 0);
			this.@__layout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__layout.Location = new System.Drawing.Point(3, 3);
			this.@__layout.Name = "__layout";
			this.@__layout.RowCount = 2;
			this.@__layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.@__layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.@__layout.Size = new System.Drawing.Size(786, 434);
			this.@__layout.TabIndex = 4;
			// 
			// __missingFiles
			// 
			this.@__missingFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__missingFiles.FormattingEnabled = true;
			this.@__missingFiles.IntegralHeight = false;
			this.@__missingFiles.Location = new System.Drawing.Point(3, 24);
			this.@__missingFiles.Name = "__missingFiles";
			this.@__missingFiles.Size = new System.Drawing.Size(387, 407);
			this.@__missingFiles.TabIndex = 1;
			// 
			// __unreferencedLbl
			// 
			this.@__unreferencedLbl.AutoSize = true;
			this.@__unreferencedLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__unreferencedLbl.Location = new System.Drawing.Point(396, 0);
			this.@__unreferencedLbl.Name = "__unreferencedLbl";
			this.@__unreferencedLbl.Size = new System.Drawing.Size(387, 21);
			this.@__unreferencedLbl.TabIndex = 3;
			this.@__unreferencedLbl.Text = "Unreferenced Files";
			// 
			// __unreferencedFiles
			// 
			this.@__unreferencedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__unreferencedFiles.FormattingEnabled = true;
			this.@__unreferencedFiles.IntegralHeight = false;
			this.@__unreferencedFiles.Location = new System.Drawing.Point(396, 24);
			this.@__unreferencedFiles.Name = "__unreferencedFiles";
			this.@__unreferencedFiles.Size = new System.Drawing.Size(387, 407);
			this.@__unreferencedFiles.TabIndex = 0;
			// 
			// __missingLbl
			// 
			this.@__missingLbl.AutoSize = true;
			this.@__missingLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__missingLbl.Location = new System.Drawing.Point(3, 0);
			this.@__missingLbl.Name = "__missingLbl";
			this.@__missingLbl.Size = new System.Drawing.Size(387, 21);
			this.@__missingLbl.TabIndex = 2;
			this.@__missingLbl.Text = "Missing Files";
			// 
			// __tabDupes
			// 
			this.@__tabDupes.Controls.Add(this.@__duplicatesLbl);
			this.@__tabDupes.Controls.Add(this.@__duplicateFiles);
			this.@__tabDupes.Location = new System.Drawing.Point(4, 22);
			this.@__tabDupes.Name = "__tabDupes";
			this.@__tabDupes.Padding = new System.Windows.Forms.Padding(3);
			this.@__tabDupes.Size = new System.Drawing.Size(792, 440);
			this.@__tabDupes.TabIndex = 2;
			this.@__tabDupes.Text = "Duplicate Files";
			this.@__tabDupes.UseVisualStyleBackColor = true;
			// 
			// __duplicatesLbl
			// 
			this.@__duplicatesLbl.AutoSize = true;
			this.@__duplicatesLbl.Location = new System.Drawing.Point(6, 3);
			this.@__duplicatesLbl.Name = "__duplicatesLbl";
			this.@__duplicatesLbl.Size = new System.Drawing.Size(76, 13);
			this.@__duplicatesLbl.TabIndex = 1;
			this.@__duplicatesLbl.Text = "Duplicate Files";
			// 
			// __duplicateFiles
			// 
			this.@__duplicateFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.@__duplicateFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.@__dupeColName});
			this.@__duplicateFiles.Location = new System.Drawing.Point(6, 19);
			this.@__duplicateFiles.Name = "__duplicateFiles";
			this.@__duplicateFiles.Size = new System.Drawing.Size(780, 415);
			this.@__duplicateFiles.TabIndex = 0;
			this.@__duplicateFiles.UseCompatibleStateImageBehavior = false;
			this.@__duplicateFiles.View = System.Windows.Forms.View.Details;
			// 
			// __dupeColName
			// 
			this.@__dupeColName.Text = "File Path";
			this.@__dupeColName.Width = 500;
			// 
			// __tabComps
			// 
			this.@__tabComps.Controls.Add(this.@__compPreview);
			this.@__tabComps.Controls.Add(this.@__compImages);
			this.@__tabComps.Controls.Add(this.@__compLayers);
			this.@__tabComps.Location = new System.Drawing.Point(4, 22);
			this.@__tabComps.Name = "__tabComps";
			this.@__tabComps.Padding = new System.Windows.Forms.Padding(3);
			this.@__tabComps.Size = new System.Drawing.Size(792, 440);
			this.@__tabComps.TabIndex = 3;
			this.@__tabComps.Text = "Composited Images";
			this.@__tabComps.UseVisualStyleBackColor = true;
			// 
			// __compPreview
			// 
			this.@__compPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__compPreview.Location = new System.Drawing.Point(380, 152);
			this.@__compPreview.Name = "__compPreview";
			this.@__compPreview.Size = new System.Drawing.Size(406, 282);
			this.@__compPreview.TabIndex = 2;
			this.@__compPreview.TabStop = false;
			// 
			// __compImages
			// 
			this.@__compImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__compImages.FormattingEnabled = true;
			this.@__compImages.IntegralHeight = false;
			this.@__compImages.Location = new System.Drawing.Point(6, 6);
			this.@__compImages.Name = "__compImages";
			this.@__compImages.Size = new System.Drawing.Size(780, 140);
			this.@__compImages.TabIndex = 0;
			// 
			// __compLayers
			// 
			this.@__compLayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__compLayers.FormattingEnabled = true;
			this.@__compLayers.IntegralHeight = false;
			this.@__compLayers.Location = new System.Drawing.Point(6, 152);
			this.@__compLayers.Name = "__compLayers";
			this.@__compLayers.Size = new System.Drawing.Size(368, 282);
			this.@__compLayers.TabIndex = 1;
			// 
			// __bwLoad
			// 
			this.@__bwLoad.WorkerReportsProgress = true;
			// 
			// __sfd
			// 
			this.@__sfd.Filter = "Package XML (*.xml)|*.xml";
			// 
			// __destBrowse
			// 
			this.@__destBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__destBrowse.Location = new System.Drawing.Point(659, 30);
			this.@__destBrowse.Name = "__destBrowse";
			this.@__destBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__destBrowse.TabIndex = 9;
			this.@__destBrowse.Text = "Browse...";
			this.@__destBrowse.UseVisualStyleBackColor = true;
			// 
			// __destPath
			// 
			this.@__destPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__destPath.Location = new System.Drawing.Point(138, 32);
			this.@__destPath.Name = "__destPath";
			this.@__destPath.Size = new System.Drawing.Size(515, 20);
			this.@__destPath.TabIndex = 8;
			// 
			// __destLbl
			// 
			this.@__destLbl.AutoSize = true;
			this.@__destLbl.Location = new System.Drawing.Point(27, 35);
			this.@__destLbl.Name = "__destLbl";
			this.@__destLbl.Size = new System.Drawing.Size(105, 13);
			this.@__destLbl.TabIndex = 7;
			this.@__destLbl.Text = "Destination Directory";
			// 
			// __dest
			// 
			this.@__dest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__dest.Location = new System.Drawing.Point(740, 30);
			this.@__dest.Name = "__dest";
			this.@__dest.Size = new System.Drawing.Size(75, 23);
			this.@__dest.TabIndex = 10;
			this.@__dest.Text = "Export";
			this.@__dest.UseVisualStyleBackColor = true;
			// 
			// __bwDest
			// 
			this.@__bwDest.WorkerReportsProgress = true;
			// 
			// OptimizerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(827, 537);
			this.Controls.Add(this.@__dest);
			this.Controls.Add(this.@__destBrowse);
			this.Controls.Add(this.@__destPath);
			this.Controls.Add(this.@__destLbl);
			this.Controls.Add(this.@__tabs);
			this.Controls.Add(this.@__loadBrowse);
			this.Controls.Add(this.@__load);
			this.Controls.Add(this.@__fileName);
			this.Controls.Add(this.@__fileNameLbl);
			this.Name = "OptimizerForm";
			this.Text = "Validate and Optimize Package";
			this.@__tabs.ResumeLayout(false);
			this.@__tabMessages.ResumeLayout(false);
			this.@__tabFiles.ResumeLayout(false);
			this.@__layout.ResumeLayout(false);
			this.@__layout.PerformLayout();
			this.@__tabDupes.ResumeLayout(false);
			this.@__tabDupes.PerformLayout();
			this.@__tabComps.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.@__compPreview)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label __fileNameLbl;
		private System.Windows.Forms.TextBox __fileName;
		private System.Windows.Forms.Button __load;
		private System.Windows.Forms.Button __loadBrowse;
		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.TabControl __tabs;
		private System.Windows.Forms.TabPage __tabMessages;
		private System.Windows.Forms.TabPage __tabFiles;
		private System.Windows.Forms.ListBox __missingFiles;
		private System.Windows.Forms.ListBox __unreferencedFiles;
		private System.Windows.Forms.ListBox __messages;
		private System.Windows.Forms.Label __unreferencedLbl;
		private System.Windows.Forms.Label __missingLbl;
		private System.Windows.Forms.TableLayoutPanel __layout;
		private System.Windows.Forms.TabPage __tabDupes;
		private System.Windows.Forms.TabPage __tabComps;
		private System.Windows.Forms.PictureBox __compPreview;
		private System.Windows.Forms.ListBox __compImages;
		private System.Windows.Forms.ListBox __compLayers;
		private System.Windows.Forms.Label __duplicatesLbl;
		private System.Windows.Forms.ListView __duplicateFiles;
		private System.Windows.Forms.FolderBrowserDialog __fbd;
		private System.ComponentModel.BackgroundWorker __bwLoad;
		private System.Windows.Forms.ColumnHeader __dupeColName;
		private System.Windows.Forms.SaveFileDialog __sfd;
		private System.Windows.Forms.Button __destBrowse;
		private System.Windows.Forms.TextBox __destPath;
		private System.Windows.Forms.Label __destLbl;
		private System.Windows.Forms.Button __dest;
		private System.ComponentModel.BackgroundWorker __bwDest;
	}
}