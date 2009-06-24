namespace Anolis.Packager.Importer {
	partial class ImportForm {
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
			this.@__input = new System.Windows.Forms.GroupBox();
			this.@__filesPath = new System.Windows.Forms.TextBox();
			this.@__filesBrowse = new System.Windows.Forms.Button();
			this.@__filesLbl = new System.Windows.Forms.Label();
			this.@__nsiPath = new System.Windows.Forms.TextBox();
			this.@__nsiBrowse = new System.Windows.Forms.Button();
			this.@__nsiLbl = new System.Windows.Forms.Label();
			this.@__output = new System.Windows.Forms.GroupBox();
			this.@__convert = new System.Windows.Forms.Button();
			this.@__packPath = new System.Windows.Forms.TextBox();
			this.@__packBrowse = new System.Windows.Forms.Button();
			this.@__packLbl = new System.Windows.Forms.Label();
			this.@__close = new System.Windows.Forms.Button();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__sfd = new System.Windows.Forms.SaveFileDialog();
			this.@__fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.@__input.SuspendLayout();
			this.@__output.SuspendLayout();
			this.SuspendLayout();
			// 
			// __input
			// 
			this.@__input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__input.Controls.Add(this.@__filesPath);
			this.@__input.Controls.Add(this.@__filesBrowse);
			this.@__input.Controls.Add(this.@__filesLbl);
			this.@__input.Controls.Add(this.@__nsiPath);
			this.@__input.Controls.Add(this.@__nsiBrowse);
			this.@__input.Controls.Add(this.@__nsiLbl);
			this.@__input.Location = new System.Drawing.Point(12, 12);
			this.@__input.Name = "__input";
			this.@__input.Size = new System.Drawing.Size(438, 87);
			this.@__input.TabIndex = 0;
			this.@__input.TabStop = false;
			this.@__input.Text = "Input";
			// 
			// __filesPath
			// 
			this.@__filesPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__filesPath.Location = new System.Drawing.Point(109, 51);
			this.@__filesPath.Name = "__filesPath";
			this.@__filesPath.Size = new System.Drawing.Size(242, 20);
			this.@__filesPath.TabIndex = 5;
			// 
			// __filesBrowse
			// 
			this.@__filesBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__filesBrowse.Location = new System.Drawing.Point(357, 49);
			this.@__filesBrowse.Name = "__filesBrowse";
			this.@__filesBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__filesBrowse.TabIndex = 4;
			this.@__filesBrowse.Text = "Browse...";
			this.@__filesBrowse.UseVisualStyleBackColor = true;
			// 
			// __filesLbl
			// 
			this.@__filesLbl.AutoSize = true;
			this.@__filesLbl.Location = new System.Drawing.Point(30, 54);
			this.@__filesLbl.Name = "__filesLbl";
			this.@__filesLbl.Size = new System.Drawing.Size(73, 13);
			this.@__filesLbl.TabIndex = 3;
			this.@__filesLbl.Text = "Files Directory";
			// 
			// __nsiPath
			// 
			this.@__nsiPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__nsiPath.Location = new System.Drawing.Point(109, 21);
			this.@__nsiPath.Name = "__nsiPath";
			this.@__nsiPath.Size = new System.Drawing.Size(242, 20);
			this.@__nsiPath.TabIndex = 2;
			// 
			// __nsiBrowse
			// 
			this.@__nsiBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__nsiBrowse.Location = new System.Drawing.Point(357, 19);
			this.@__nsiBrowse.Name = "__nsiBrowse";
			this.@__nsiBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__nsiBrowse.TabIndex = 1;
			this.@__nsiBrowse.Text = "Browse...";
			this.@__nsiBrowse.UseVisualStyleBackColor = true;
			// 
			// __nsiLbl
			// 
			this.@__nsiLbl.AutoSize = true;
			this.@__nsiLbl.Location = new System.Drawing.Point(48, 24);
			this.@__nsiLbl.Name = "__nsiLbl";
			this.@__nsiLbl.Size = new System.Drawing.Size(55, 13);
			this.@__nsiLbl.TabIndex = 0;
			this.@__nsiLbl.Text = "NSI Script";
			// 
			// __output
			// 
			this.@__output.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__output.Controls.Add(this.@__convert);
			this.@__output.Controls.Add(this.@__packPath);
			this.@__output.Controls.Add(this.@__packBrowse);
			this.@__output.Controls.Add(this.@__packLbl);
			this.@__output.Location = new System.Drawing.Point(12, 105);
			this.@__output.Name = "__output";
			this.@__output.Size = new System.Drawing.Size(438, 82);
			this.@__output.TabIndex = 1;
			this.@__output.TabStop = false;
			this.@__output.Text = "Output";
			// 
			// __convert
			// 
			this.@__convert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__convert.Location = new System.Drawing.Point(357, 53);
			this.@__convert.Name = "__convert";
			this.@__convert.Size = new System.Drawing.Size(75, 23);
			this.@__convert.TabIndex = 9;
			this.@__convert.Text = "Convert";
			this.@__convert.UseVisualStyleBackColor = true;
			// 
			// __packPath
			// 
			this.@__packPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__packPath.Location = new System.Drawing.Point(109, 21);
			this.@__packPath.Name = "__packPath";
			this.@__packPath.Size = new System.Drawing.Size(242, 20);
			this.@__packPath.TabIndex = 8;
			// 
			// __packBrowse
			// 
			this.@__packBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__packBrowse.Location = new System.Drawing.Point(357, 19);
			this.@__packBrowse.Name = "__packBrowse";
			this.@__packBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__packBrowse.TabIndex = 7;
			this.@__packBrowse.Text = "Browse...";
			this.@__packBrowse.UseVisualStyleBackColor = true;
			// 
			// __packLbl
			// 
			this.@__packLbl.AutoSize = true;
			this.@__packLbl.Location = new System.Drawing.Point(6, 24);
			this.@__packLbl.Name = "__packLbl";
			this.@__packLbl.Size = new System.Drawing.Size(97, 13);
			this.@__packLbl.TabIndex = 6;
			this.@__packLbl.Text = "Package Definition";
			// 
			// __close
			// 
			this.@__close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.@__close.Location = new System.Drawing.Point(375, 196);
			this.@__close.Name = "__close";
			this.@__close.Size = new System.Drawing.Size(75, 23);
			this.@__close.TabIndex = 2;
			this.@__close.Text = "Close";
			this.@__close.UseVisualStyleBackColor = true;
			// 
			// __ofd
			// 
			this.@__ofd.FileName = "openFileDialog1";
			// 
			// ImportForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.@__close;
			this.ClientSize = new System.Drawing.Size(462, 231);
			this.Controls.Add(this.@__close);
			this.Controls.Add(this.@__output);
			this.Controls.Add(this.@__input);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "ImportForm";
			this.Text = "Import NSI/XIS";
			this.@__input.ResumeLayout(false);
			this.@__input.PerformLayout();
			this.@__output.ResumeLayout(false);
			this.@__output.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox __input;
		private System.Windows.Forms.Button __nsiBrowse;
		private System.Windows.Forms.Label __nsiLbl;
		private System.Windows.Forms.GroupBox __output;
		private System.Windows.Forms.TextBox __nsiPath;
		private System.Windows.Forms.TextBox __filesPath;
		private System.Windows.Forms.Button __filesBrowse;
		private System.Windows.Forms.Label __filesLbl;
		private System.Windows.Forms.Button __convert;
		private System.Windows.Forms.TextBox __packPath;
		private System.Windows.Forms.Button __packBrowse;
		private System.Windows.Forms.Label __packLbl;
		private System.Windows.Forms.Button __close;
		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.SaveFileDialog __sfd;
		private System.Windows.Forms.FolderBrowserDialog __fbd;
	}
}