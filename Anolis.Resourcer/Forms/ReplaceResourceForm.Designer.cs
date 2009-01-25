namespace Anolis.Resourcer {
	partial class ReplaceResourceForm {
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
			this.@__originalResourceGrp = new System.Windows.Forms.GroupBox();
			this.@__subclass = new System.Windows.Forms.Label();
			this.@__path = new System.Windows.Forms.Label();
			this.@__subclassLbl = new System.Windows.Forms.Label();
			this.@__pathLbl = new System.Windows.Forms.Label();
			this.@__replacementGrp = new System.Windows.Forms.GroupBox();
			this.@__browse = new System.Windows.Forms.Button();
			this.@__filename = new System.Windows.Forms.TextBox();
			this.@__fileLbl = new System.Windows.Forms.Label();
			this.@__ok = new System.Windows.Forms.Button();
			this.@__cancel = new System.Windows.Forms.Button();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__originalResourceGrp.SuspendLayout();
			this.@__replacementGrp.SuspendLayout();
			this.SuspendLayout();
			// 
			// __originalResourceGrp
			// 
			this.@__originalResourceGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__originalResourceGrp.Controls.Add(this.@__subclass);
			this.@__originalResourceGrp.Controls.Add(this.@__path);
			this.@__originalResourceGrp.Controls.Add(this.@__subclassLbl);
			this.@__originalResourceGrp.Controls.Add(this.@__pathLbl);
			this.@__originalResourceGrp.Location = new System.Drawing.Point(12, 12);
			this.@__originalResourceGrp.Name = "__originalResourceGrp";
			this.@__originalResourceGrp.Size = new System.Drawing.Size(344, 91);
			this.@__originalResourceGrp.TabIndex = 0;
			this.@__originalResourceGrp.TabStop = false;
			this.@__originalResourceGrp.Text = "Original Resource";
			// 
			// __subclass
			// 
			this.@__subclass.AutoSize = true;
			this.@__subclass.Location = new System.Drawing.Point(69, 53);
			this.@__subclass.Name = "__subclass";
			this.@__subclass.Size = new System.Drawing.Size(119, 13);
			this.@__subclass.TabIndex = 3;
			this.@__subclass.Text = "UnknownResourceData";
			// 
			// __path
			// 
			this.@__path.AutoSize = true;
			this.@__path.Location = new System.Drawing.Point(69, 26);
			this.@__path.Name = "__path";
			this.@__path.Size = new System.Drawing.Size(89, 13);
			this.@__path.TabIndex = 2;
			this.@__path.Text = "Type\\Name\\Lang";
			// 
			// __subclassLbl
			// 
			this.@__subclassLbl.AutoSize = true;
			this.@__subclassLbl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.@__subclassLbl.Location = new System.Drawing.Point(15, 53);
			this.@__subclassLbl.Name = "__subclassLbl";
			this.@__subclassLbl.Size = new System.Drawing.Size(56, 13);
			this.@__subclassLbl.TabIndex = 1;
			this.@__subclassLbl.Text = "Subclass";
			// 
			// __pathLbl
			// 
			this.@__pathLbl.AutoSize = true;
			this.@__pathLbl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.@__pathLbl.Location = new System.Drawing.Point(36, 26);
			this.@__pathLbl.Name = "__pathLbl";
			this.@__pathLbl.Size = new System.Drawing.Size(33, 13);
			this.@__pathLbl.TabIndex = 0;
			this.@__pathLbl.Text = "Path";
			// 
			// __replacementGrp
			// 
			this.@__replacementGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__replacementGrp.Controls.Add(this.@__browse);
			this.@__replacementGrp.Controls.Add(this.@__filename);
			this.@__replacementGrp.Controls.Add(this.@__fileLbl);
			this.@__replacementGrp.Location = new System.Drawing.Point(12, 109);
			this.@__replacementGrp.Name = "__replacementGrp";
			this.@__replacementGrp.Size = new System.Drawing.Size(344, 65);
			this.@__replacementGrp.TabIndex = 1;
			this.@__replacementGrp.TabStop = false;
			this.@__replacementGrp.Text = "Replacement Resource";
			// 
			// __browse
			// 
			this.@__browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__browse.Location = new System.Drawing.Point(253, 22);
			this.@__browse.Name = "__browse";
			this.@__browse.Size = new System.Drawing.Size(75, 23);
			this.@__browse.TabIndex = 2;
			this.@__browse.Text = "Browse...";
			this.@__browse.UseVisualStyleBackColor = true;
			// 
			// __filename
			// 
			this.@__filename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__filename.Location = new System.Drawing.Point(72, 24);
			this.@__filename.Name = "__filename";
			this.@__filename.Size = new System.Drawing.Size(175, 21);
			this.@__filename.TabIndex = 1;
			// 
			// __fileLbl
			// 
			this.@__fileLbl.AutoSize = true;
			this.@__fileLbl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.@__fileLbl.Location = new System.Drawing.Point(34, 27);
			this.@__fileLbl.Name = "__fileLbl";
			this.@__fileLbl.Size = new System.Drawing.Size(26, 13);
			this.@__fileLbl.TabIndex = 0;
			this.@__fileLbl.Text = "File";
			// 
			// __ok
			// 
			this.@__ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__ok.Location = new System.Drawing.Point(200, 188);
			this.@__ok.Name = "__ok";
			this.@__ok.Size = new System.Drawing.Size(75, 23);
			this.@__ok.TabIndex = 2;
			this.@__ok.Text = "OK";
			this.@__ok.UseVisualStyleBackColor = true;
			// 
			// __cancel
			// 
			this.@__cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.@__cancel.Location = new System.Drawing.Point(281, 188);
			this.@__cancel.Name = "__cancel";
			this.@__cancel.Size = new System.Drawing.Size(75, 23);
			this.@__cancel.TabIndex = 3;
			this.@__cancel.Text = "Cancel";
			this.@__cancel.UseVisualStyleBackColor = true;
			// 
			// ReplaceResourceForm
			// 
			this.AcceptButton = this.@__ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.@__cancel;
			this.ClientSize = new System.Drawing.Size(368, 223);
			this.Controls.Add(this.@__cancel);
			this.Controls.Add(this.@__ok);
			this.Controls.Add(this.@__replacementGrp);
			this.Controls.Add(this.@__originalResourceGrp);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ReplaceResourceForm";
			this.ShowIcon = false;
			this.Text = "Replace Resource";
			this.@__originalResourceGrp.ResumeLayout(false);
			this.@__originalResourceGrp.PerformLayout();
			this.@__replacementGrp.ResumeLayout(false);
			this.@__replacementGrp.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox __originalResourceGrp;
		private System.Windows.Forms.Label __subclass;
		private System.Windows.Forms.Label __path;
		private System.Windows.Forms.Label __subclassLbl;
		private System.Windows.Forms.Label __pathLbl;
		private System.Windows.Forms.GroupBox __replacementGrp;
		private System.Windows.Forms.Button __browse;
		private System.Windows.Forms.TextBox __filename;
		private System.Windows.Forms.Label __fileLbl;
		private System.Windows.Forms.Button __ok;
		private System.Windows.Forms.Button __cancel;
		private System.Windows.Forms.OpenFileDialog __ofd;

	}
}