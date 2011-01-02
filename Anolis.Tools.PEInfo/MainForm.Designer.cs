namespace Anolis.Tools.PEInfo {
	partial class MainForm {
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
			this.@__openFileNameLbl = new System.Windows.Forms.Label();
			this.@__openFileName = new System.Windows.Forms.TextBox();
			this.@__openBrowse = new System.Windows.Forms.Button();
			this.@__openLoad = new System.Windows.Forms.Button();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__openAs = new System.Windows.Forms.ComboBox();
			this.@__pe = new Anolis.Tools.PEInfo.PEView();
			this.SuspendLayout();
			// 
			// __openFileNameLbl
			// 
			this.@__openFileNameLbl.AutoSize = true;
			this.@__openFileNameLbl.Location = new System.Drawing.Point(12, 17);
			this.@__openFileNameLbl.Name = "__openFileNameLbl";
			this.@__openFileNameLbl.Size = new System.Drawing.Size(26, 13);
			this.@__openFileNameLbl.TabIndex = 0;
			this.@__openFileNameLbl.Text = "File:";
			// 
			// __openFileName
			// 
			this.@__openFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__openFileName.Location = new System.Drawing.Point(44, 14);
			this.@__openFileName.Name = "__openFileName";
			this.@__openFileName.Size = new System.Drawing.Size(243, 20);
			this.@__openFileName.TabIndex = 1;
			this.@__openFileName.Text = "C:\\Users\\David\\Documents\\Visual Studio Projects\\Anolis\\FileAlyzer\\FileAlyzer\\File" +
				"Alyzer.exe";
			// 
			// __openBrowse
			// 
			this.@__openBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__openBrowse.Location = new System.Drawing.Point(293, 12);
			this.@__openBrowse.Name = "__openBrowse";
			this.@__openBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__openBrowse.TabIndex = 2;
			this.@__openBrowse.Text = "Browse...";
			this.@__openBrowse.UseVisualStyleBackColor = true;
			// 
			// __openLoad
			// 
			this.@__openLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__openLoad.Location = new System.Drawing.Point(493, 12);
			this.@__openLoad.Name = "__openLoad";
			this.@__openLoad.Size = new System.Drawing.Size(75, 23);
			this.@__openLoad.TabIndex = 3;
			this.@__openLoad.Text = "Load";
			this.@__openLoad.UseVisualStyleBackColor = true;
			// 
			// __ofd
			// 
			this.@__ofd.Filter = "Executable Images (*.exe, *.dll, *.com)|*.exe;*.dll;*.com|All files (*.*)|*.*";
			// 
			// __openAs
			// 
			this.@__openAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__openAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.@__openAs.FormattingEnabled = true;
			this.@__openAs.Items.AddRange(new object[] {
            "DOS",
            "NE",
            "PE/COFF"});
			this.@__openAs.Location = new System.Drawing.Point(374, 14);
			this.@__openAs.Name = "__openAs";
			this.@__openAs.Size = new System.Drawing.Size(113, 21);
			this.@__openAs.TabIndex = 5;
			// 
			// __pe
			// 
			this.@__pe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__pe.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.@__pe.Location = new System.Drawing.Point(12, 41);
			this.@__pe.Name = "__pe";
			this.@__pe.PEFile = null;
			this.@__pe.Size = new System.Drawing.Size(556, 365);
			this.@__pe.TabIndex = 6;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(580, 418);
			this.Controls.Add(this.@__pe);
			this.Controls.Add(this.@__openAs);
			this.Controls.Add(this.@__openLoad);
			this.Controls.Add(this.@__openBrowse);
			this.Controls.Add(this.@__openFileName);
			this.Controls.Add(this.@__openFileNameLbl);
			this.Name = "MainForm";
			this.Text = "PE Inspector";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label __openFileNameLbl;
		private System.Windows.Forms.TextBox __openFileName;
		private System.Windows.Forms.Button __openBrowse;
		private System.Windows.Forms.Button __openLoad;
		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.ComboBox __openAs;
		private PEView __pe;
	}
}