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
			this.@__browse = new System.Windows.Forms.Button();
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
			this.@__tabs.SuspendLayout();
			this.@__tabMessages.SuspendLayout();
			this.@__tabFiles.SuspendLayout();
			this.@__layout.SuspendLayout();
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
			this.@__fileName.Size = new System.Drawing.Size(535, 20);
			this.@__fileName.TabIndex = 3;
			// 
			// __load
			// 
			this.@__load.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__load.Location = new System.Drawing.Point(760, 4);
			this.@__load.Name = "__load";
			this.@__load.Size = new System.Drawing.Size(75, 23);
			this.@__load.TabIndex = 4;
			this.@__load.Text = "Load";
			this.@__load.UseVisualStyleBackColor = true;
			// 
			// __browse
			// 
			this.@__browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__browse.Location = new System.Drawing.Point(679, 4);
			this.@__browse.Name = "__browse";
			this.@__browse.Size = new System.Drawing.Size(75, 23);
			this.@__browse.TabIndex = 5;
			this.@__browse.Text = "Browse...";
			this.@__browse.UseVisualStyleBackColor = true;
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
			this.@__tabs.Location = new System.Drawing.Point(15, 46);
			this.@__tabs.Name = "__tabs";
			this.@__tabs.SelectedIndex = 0;
			this.@__tabs.Size = new System.Drawing.Size(820, 450);
			this.@__tabs.TabIndex = 6;
			// 
			// __tabMessages
			// 
			this.@__tabMessages.Controls.Add(this.@__messages);
			this.@__tabMessages.Location = new System.Drawing.Point(4, 22);
			this.@__tabMessages.Name = "__tabMessages";
			this.@__tabMessages.Padding = new System.Windows.Forms.Padding(3);
			this.@__tabMessages.Size = new System.Drawing.Size(812, 424);
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
			this.@__messages.Size = new System.Drawing.Size(806, 418);
			this.@__messages.TabIndex = 0;
			// 
			// __tabFiles
			// 
			this.@__tabFiles.Controls.Add(this.@__layout);
			this.@__tabFiles.Location = new System.Drawing.Point(4, 22);
			this.@__tabFiles.Name = "__tabFiles";
			this.@__tabFiles.Padding = new System.Windows.Forms.Padding(3);
			this.@__tabFiles.Size = new System.Drawing.Size(812, 424);
			this.@__tabFiles.TabIndex = 1;
			this.@__tabFiles.Text = "Files";
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
			this.@__layout.Size = new System.Drawing.Size(806, 418);
			this.@__layout.TabIndex = 4;
			// 
			// __missingFiles
			// 
			this.@__missingFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__missingFiles.FormattingEnabled = true;
			this.@__missingFiles.IntegralHeight = false;
			this.@__missingFiles.Location = new System.Drawing.Point(3, 24);
			this.@__missingFiles.Name = "__missingFiles";
			this.@__missingFiles.Size = new System.Drawing.Size(397, 391);
			this.@__missingFiles.TabIndex = 1;
			// 
			// __unreferencedLbl
			// 
			this.@__unreferencedLbl.AutoSize = true;
			this.@__unreferencedLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__unreferencedLbl.Location = new System.Drawing.Point(406, 0);
			this.@__unreferencedLbl.Name = "__unreferencedLbl";
			this.@__unreferencedLbl.Size = new System.Drawing.Size(397, 21);
			this.@__unreferencedLbl.TabIndex = 3;
			this.@__unreferencedLbl.Text = "Unreferenced Files";
			// 
			// __unreferencedFiles
			// 
			this.@__unreferencedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__unreferencedFiles.FormattingEnabled = true;
			this.@__unreferencedFiles.IntegralHeight = false;
			this.@__unreferencedFiles.Location = new System.Drawing.Point(406, 24);
			this.@__unreferencedFiles.Name = "__unreferencedFiles";
			this.@__unreferencedFiles.Size = new System.Drawing.Size(397, 391);
			this.@__unreferencedFiles.TabIndex = 0;
			// 
			// __missingLbl
			// 
			this.@__missingLbl.AutoSize = true;
			this.@__missingLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__missingLbl.Location = new System.Drawing.Point(3, 0);
			this.@__missingLbl.Name = "__missingLbl";
			this.@__missingLbl.Size = new System.Drawing.Size(397, 21);
			this.@__missingLbl.TabIndex = 2;
			this.@__missingLbl.Text = "Missing Files";
			// 
			// __tabDupes
			// 
			this.@__tabDupes.Location = new System.Drawing.Point(4, 22);
			this.@__tabDupes.Name = "__tabDupes";
			this.@__tabDupes.Padding = new System.Windows.Forms.Padding(3);
			this.@__tabDupes.Size = new System.Drawing.Size(812, 424);
			this.@__tabDupes.TabIndex = 2;
			this.@__tabDupes.Text = "Duplicate Files";
			this.@__tabDupes.UseVisualStyleBackColor = true;
			// 
			// OptimizerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(847, 508);
			this.Controls.Add(this.@__tabs);
			this.Controls.Add(this.@__browse);
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
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label __fileNameLbl;
		private System.Windows.Forms.TextBox __fileName;
		private System.Windows.Forms.Button __load;
		private System.Windows.Forms.Button __browse;
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
	}
}