namespace Anolis.Resourcer {
	partial class AddResourceForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddResourceForm));
			this.@__fileLbl = new System.Windows.Forms.Label();
			this.@__file = new System.Windows.Forms.TextBox();
			this.@__fileBrowse = new System.Windows.Forms.Button();
			this.@__typeGrp = new System.Windows.Forms.GroupBox();
			this.@__typeSort = new System.Windows.Forms.Button();
			this.@__typeWarning = new System.Windows.Forms.Label();
			this.@__typeString = new System.Windows.Forms.TextBox();
			this.@__typeKnown = new System.Windows.Forms.ComboBox();
			this.@__typeStringRad = new System.Windows.Forms.RadioButton();
			this.@__typeWin32Rad = new System.Windows.Forms.RadioButton();
			this.@__nameGrp = new System.Windows.Forms.GroupBox();
			this.@__nameString = new System.Windows.Forms.TextBox();
			this.@__nameStringRad = new System.Windows.Forms.RadioButton();
			this.@__nameAuto = new System.Windows.Forms.CheckBox();
			this.@__nameCustom = new System.Windows.Forms.NumericUpDown();
			this.@__nameIntRad = new System.Windows.Forms.RadioButton();
			this.@__langGrp = new System.Windows.Forms.GroupBox();
			this.@__langWarning = new System.Windows.Forms.Label();
			this.@__langSort = new System.Windows.Forms.Button();
			this.@__lang = new System.Windows.Forms.ComboBox();
			this.@__cancel = new System.Windows.Forms.Button();
			this.@__ok = new System.Windows.Forms.Button();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__fileGrp = new System.Windows.Forms.GroupBox();
			this.@__resourceDetails = new System.Windows.Forms.Panel();
			this.@__action = new System.Windows.Forms.Panel();
			this.@__typeGrp.SuspendLayout();
			this.@__nameGrp.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.@__nameCustom)).BeginInit();
			this.@__langGrp.SuspendLayout();
			this.@__fileGrp.SuspendLayout();
			this.@__resourceDetails.SuspendLayout();
			this.@__action.SuspendLayout();
			this.SuspendLayout();
			// 
			// __fileLbl
			// 
			this.@__fileLbl.AutoSize = true;
			this.@__fileLbl.Location = new System.Drawing.Point(12, 30);
			this.@__fileLbl.Name = "__fileLbl";
			this.@__fileLbl.Size = new System.Drawing.Size(23, 13);
			this.@__fileLbl.TabIndex = 0;
			this.@__fileLbl.Text = "File";
			// 
			// __file
			// 
			this.@__file.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__file.Location = new System.Drawing.Point(41, 27);
			this.@__file.Name = "__file";
			this.@__file.ReadOnly = true;
			this.@__file.Size = new System.Drawing.Size(183, 21);
			this.@__file.TabIndex = 0;
			// 
			// __fileBrowse
			// 
			this.@__fileBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__fileBrowse.Location = new System.Drawing.Point(230, 25);
			this.@__fileBrowse.Name = "__fileBrowse";
			this.@__fileBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__fileBrowse.TabIndex = 1;
			this.@__fileBrowse.Text = "Browse...";
			this.@__fileBrowse.UseVisualStyleBackColor = true;
			// 
			// __typeGrp
			// 
			this.@__typeGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__typeGrp.Controls.Add(this.@__typeSort);
			this.@__typeGrp.Controls.Add(this.@__typeWarning);
			this.@__typeGrp.Controls.Add(this.@__typeString);
			this.@__typeGrp.Controls.Add(this.@__typeKnown);
			this.@__typeGrp.Controls.Add(this.@__typeStringRad);
			this.@__typeGrp.Controls.Add(this.@__typeWin32Rad);
			this.@__typeGrp.Location = new System.Drawing.Point(12, 189);
			this.@__typeGrp.Name = "__typeGrp";
			this.@__typeGrp.Size = new System.Drawing.Size(311, 187);
			this.@__typeGrp.TabIndex = 3;
			this.@__typeGrp.TabStop = false;
			this.@__typeGrp.Text = "Resource Type";
			// 
			// __typeSort
			// 
			this.@__typeSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__typeSort.Image = global::Anolis.Resourcer.Properties.Resources.ARF_SortByLCID;
			this.@__typeSort.Location = new System.Drawing.Point(275, 95);
			this.@__typeSort.Name = "__typeSort";
			this.@__typeSort.Size = new System.Drawing.Size(30, 30);
			this.@__typeSort.TabIndex = 5;
			this.@__typeSort.UseVisualStyleBackColor = true;
			// 
			// __typeWarning
			// 
			this.@__typeWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__typeWarning.Location = new System.Drawing.Point(12, 16);
			this.@__typeWarning.Name = "__typeWarning";
			this.@__typeWarning.Size = new System.Drawing.Size(293, 59);
			this.@__typeWarning.TabIndex = 4;
			this.@__typeWarning.Text = resources.GetString("__typeWarning.Text");
			// 
			// __typeString
			// 
			this.@__typeString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__typeString.Location = new System.Drawing.Point(43, 154);
			this.@__typeString.MaxLength = 255;
			this.@__typeString.Name = "__typeString";
			this.@__typeString.Size = new System.Drawing.Size(262, 21);
			this.@__typeString.TabIndex = 3;
			// 
			// __typeKnown
			// 
			this.@__typeKnown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__typeKnown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.@__typeKnown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.@__typeKnown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.@__typeKnown.FormattingEnabled = true;
			this.@__typeKnown.Location = new System.Drawing.Point(43, 101);
			this.@__typeKnown.Name = "__typeKnown";
			this.@__typeKnown.Size = new System.Drawing.Size(226, 21);
			this.@__typeKnown.TabIndex = 1;
			// 
			// __typeStringRad
			// 
			this.@__typeStringRad.AutoSize = true;
			this.@__typeStringRad.Location = new System.Drawing.Point(15, 131);
			this.@__typeStringRad.Name = "__typeStringRad";
			this.@__typeStringRad.Size = new System.Drawing.Size(100, 17);
			this.@__typeStringRad.TabIndex = 2;
			this.@__typeStringRad.Text = "String Identifier";
			this.@__typeStringRad.UseVisualStyleBackColor = true;
			// 
			// __typeWin32Rad
			// 
			this.@__typeWin32Rad.AutoSize = true;
			this.@__typeWin32Rad.Checked = true;
			this.@__typeWin32Rad.Location = new System.Drawing.Point(15, 78);
			this.@__typeWin32Rad.Name = "__typeWin32Rad";
			this.@__typeWin32Rad.Size = new System.Drawing.Size(117, 17);
			this.@__typeWin32Rad.TabIndex = 0;
			this.@__typeWin32Rad.TabStop = true;
			this.@__typeWin32Rad.Text = "Win32 Known Type";
			this.@__typeWin32Rad.UseVisualStyleBackColor = true;
			// 
			// __nameGrp
			// 
			this.@__nameGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__nameGrp.Controls.Add(this.@__nameString);
			this.@__nameGrp.Controls.Add(this.@__nameStringRad);
			this.@__nameGrp.Controls.Add(this.@__nameAuto);
			this.@__nameGrp.Controls.Add(this.@__nameCustom);
			this.@__nameGrp.Controls.Add(this.@__nameIntRad);
			this.@__nameGrp.Location = new System.Drawing.Point(12, 382);
			this.@__nameGrp.Name = "__nameGrp";
			this.@__nameGrp.Size = new System.Drawing.Size(311, 123);
			this.@__nameGrp.TabIndex = 4;
			this.@__nameGrp.TabStop = false;
			this.@__nameGrp.Text = "Resource Name";
			// 
			// __nameString
			// 
			this.@__nameString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__nameString.Location = new System.Drawing.Point(43, 88);
			this.@__nameString.MaxLength = 255;
			this.@__nameString.Name = "__nameString";
			this.@__nameString.Size = new System.Drawing.Size(262, 21);
			this.@__nameString.TabIndex = 4;
			// 
			// __nameStringRad
			// 
			this.@__nameStringRad.AutoSize = true;
			this.@__nameStringRad.Location = new System.Drawing.Point(15, 65);
			this.@__nameStringRad.Name = "__nameStringRad";
			this.@__nameStringRad.Size = new System.Drawing.Size(100, 17);
			this.@__nameStringRad.TabIndex = 3;
			this.@__nameStringRad.TabStop = true;
			this.@__nameStringRad.Text = "String Identifier";
			this.@__nameStringRad.UseVisualStyleBackColor = true;
			// 
			// __nameAuto
			// 
			this.@__nameAuto.AutoSize = true;
			this.@__nameAuto.Checked = true;
			this.@__nameAuto.CheckState = System.Windows.Forms.CheckState.Checked;
			this.@__nameAuto.Location = new System.Drawing.Point(43, 42);
			this.@__nameAuto.Name = "__nameAuto";
			this.@__nameAuto.Size = new System.Drawing.Size(104, 17);
			this.@__nameAuto.TabIndex = 1;
			this.@__nameAuto.Text = "Automatic Name";
			this.@__nameAuto.UseVisualStyleBackColor = true;
			// 
			// __nameCustom
			// 
			this.@__nameCustom.Enabled = false;
			this.@__nameCustom.Location = new System.Drawing.Point(196, 41);
			this.@__nameCustom.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.@__nameCustom.Name = "__nameCustom";
			this.@__nameCustom.Size = new System.Drawing.Size(81, 21);
			this.@__nameCustom.TabIndex = 2;
			// 
			// __nameIntRad
			// 
			this.@__nameIntRad.AutoSize = true;
			this.@__nameIntRad.Checked = true;
			this.@__nameIntRad.Location = new System.Drawing.Point(15, 19);
			this.@__nameIntRad.Name = "__nameIntRad";
			this.@__nameIntRad.Size = new System.Drawing.Size(108, 17);
			this.@__nameIntRad.TabIndex = 0;
			this.@__nameIntRad.TabStop = true;
			this.@__nameIntRad.Text = "Integer Identifier";
			this.@__nameIntRad.UseVisualStyleBackColor = true;
			// 
			// __langGrp
			// 
			this.@__langGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__langGrp.Controls.Add(this.@__langWarning);
			this.@__langGrp.Controls.Add(this.@__langSort);
			this.@__langGrp.Controls.Add(this.@__lang);
			this.@__langGrp.Location = new System.Drawing.Point(12, 12);
			this.@__langGrp.Name = "__langGrp";
			this.@__langGrp.Size = new System.Drawing.Size(311, 105);
			this.@__langGrp.TabIndex = 5;
			this.@__langGrp.TabStop = false;
			this.@__langGrp.Text = "Resource Language";
			// 
			// __langWarning
			// 
			this.@__langWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__langWarning.Location = new System.Drawing.Point(12, 17);
			this.@__langWarning.Name = "__langWarning";
			this.@__langWarning.Size = new System.Drawing.Size(293, 40);
			this.@__langWarning.TabIndex = 2;
			this.@__langWarning.Text = "The resource language must be set before continuing. Once the Resource Data has b" +
				"een instantiated the language cannot be changed.";
			// 
			// __langSort
			// 
			this.@__langSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__langSort.Image = global::Anolis.Resourcer.Properties.Resources.ARF_SortByLCID;
			this.@__langSort.Location = new System.Drawing.Point(275, 63);
			this.@__langSort.Name = "__langSort";
			this.@__langSort.Size = new System.Drawing.Size(30, 30);
			this.@__langSort.TabIndex = 1;
			this.@__langSort.UseVisualStyleBackColor = true;
			// 
			// __lang
			// 
			this.@__lang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__lang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.@__lang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.@__lang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.@__lang.FormattingEnabled = true;
			this.@__lang.Location = new System.Drawing.Point(15, 69);
			this.@__lang.Name = "__lang";
			this.@__lang.Size = new System.Drawing.Size(254, 21);
			this.@__lang.TabIndex = 0;
			// 
			// __cancel
			// 
			this.@__cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.@__cancel.Location = new System.Drawing.Point(248, 7);
			this.@__cancel.Name = "__cancel";
			this.@__cancel.Size = new System.Drawing.Size(75, 23);
			this.@__cancel.TabIndex = 3;
			this.@__cancel.Text = "Cancel";
			this.@__cancel.UseVisualStyleBackColor = true;
			// 
			// __ok
			// 
			this.@__ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__ok.Location = new System.Drawing.Point(167, 7);
			this.@__ok.Name = "__ok";
			this.@__ok.Size = new System.Drawing.Size(75, 23);
			this.@__ok.TabIndex = 2;
			this.@__ok.Text = "OK";
			this.@__ok.UseVisualStyleBackColor = true;
			// 
			// __fileGrp
			// 
			this.@__fileGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__fileGrp.Controls.Add(this.@__fileLbl);
			this.@__fileGrp.Controls.Add(this.@__file);
			this.@__fileGrp.Controls.Add(this.@__fileBrowse);
			this.@__fileGrp.Location = new System.Drawing.Point(12, 123);
			this.@__fileGrp.Name = "__fileGrp";
			this.@__fileGrp.Size = new System.Drawing.Size(311, 60);
			this.@__fileGrp.TabIndex = 6;
			this.@__fileGrp.TabStop = false;
			this.@__fileGrp.Text = "Resource Data File";
			// 
			// __resourceDetails
			// 
			this.@__resourceDetails.AutoScroll = true;
			this.@__resourceDetails.Controls.Add(this.@__langGrp);
			this.@__resourceDetails.Controls.Add(this.@__fileGrp);
			this.@__resourceDetails.Controls.Add(this.@__typeGrp);
			this.@__resourceDetails.Controls.Add(this.@__nameGrp);
			this.@__resourceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__resourceDetails.Location = new System.Drawing.Point(0, 0);
			this.@__resourceDetails.Name = "__resourceDetails";
			this.@__resourceDetails.Size = new System.Drawing.Size(335, 520);
			this.@__resourceDetails.TabIndex = 7;
			// 
			// __action
			// 
			this.@__action.Controls.Add(this.@__ok);
			this.@__action.Controls.Add(this.@__cancel);
			this.@__action.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.@__action.Location = new System.Drawing.Point(0, 520);
			this.@__action.Name = "__action";
			this.@__action.Size = new System.Drawing.Size(335, 40);
			this.@__action.TabIndex = 8;
			// 
			// AddResourceForm
			// 
			this.AcceptButton = this.@__ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.CancelButton = this.@__cancel;
			this.ClientSize = new System.Drawing.Size(335, 560);
			this.Controls.Add(this.@__resourceDetails);
			this.Controls.Add(this.@__action);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddResourceForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Add Resource Data";
			this.@__typeGrp.ResumeLayout(false);
			this.@__typeGrp.PerformLayout();
			this.@__nameGrp.ResumeLayout(false);
			this.@__nameGrp.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.@__nameCustom)).EndInit();
			this.@__langGrp.ResumeLayout(false);
			this.@__fileGrp.ResumeLayout(false);
			this.@__fileGrp.PerformLayout();
			this.@__resourceDetails.ResumeLayout(false);
			this.@__action.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label __fileLbl;
		private System.Windows.Forms.TextBox __file;
		private System.Windows.Forms.Button __fileBrowse;
		private System.Windows.Forms.GroupBox __typeGrp;
		private System.Windows.Forms.RadioButton __typeStringRad;
		private System.Windows.Forms.RadioButton __typeWin32Rad;
		private System.Windows.Forms.TextBox __typeString;
		private System.Windows.Forms.ComboBox __typeKnown;
		private System.Windows.Forms.GroupBox __nameGrp;
		private System.Windows.Forms.NumericUpDown __nameCustom;
		private System.Windows.Forms.RadioButton __nameIntRad;
		private System.Windows.Forms.CheckBox __nameAuto;
		private System.Windows.Forms.TextBox __nameString;
		private System.Windows.Forms.RadioButton __nameStringRad;
		private System.Windows.Forms.GroupBox __langGrp;
		private System.Windows.Forms.ComboBox __lang;
		private System.Windows.Forms.Button __cancel;
		private System.Windows.Forms.Button __ok;
		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.Label __typeWarning;
		private System.Windows.Forms.Button __langSort;
		private System.Windows.Forms.Button __typeSort;
		private System.Windows.Forms.GroupBox __fileGrp;
		private System.Windows.Forms.Label __langWarning;
		private System.Windows.Forms.Panel __resourceDetails;
		private System.Windows.Forms.Panel __action;
	}
}