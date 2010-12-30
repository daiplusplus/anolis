namespace Anolis.Tools.ImageTools {
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
			this.@__alphaBitmapGrp = new System.Windows.Forms.GroupBox();
			this.@__alphaBitmapLbl = new System.Windows.Forms.Label();
			this.@__alphaBitmapBtn = new System.Windows.Forms.Button();
			this.@__pngToArgbGrp = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.@__pngToArgb = new System.Windows.Forms.TextBox();
			this.@__pngToArgbBtn = new System.Windows.Forms.Button();
			this.@__alphaBitmapGrp.SuspendLayout();
			this.@__pngToArgbGrp.SuspendLayout();
			this.SuspendLayout();
			// 
			// __alphaBitmapGrp
			// 
			this.@__alphaBitmapGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__alphaBitmapGrp.Controls.Add(this.@__alphaBitmapBtn);
			this.@__alphaBitmapGrp.Controls.Add(this.@__alphaBitmapLbl);
			this.@__alphaBitmapGrp.Location = new System.Drawing.Point(12, 12);
			this.@__alphaBitmapGrp.Name = "__alphaBitmapGrp";
			this.@__alphaBitmapGrp.Size = new System.Drawing.Size(280, 85);
			this.@__alphaBitmapGrp.TabIndex = 0;
			this.@__alphaBitmapGrp.TabStop = false;
			this.@__alphaBitmapGrp.Text = "RGB and Alpha Pixel Combinator";
			// 
			// __alphaBitmapLbl
			// 
			this.@__alphaBitmapLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__alphaBitmapLbl.Location = new System.Drawing.Point(11, 23);
			this.@__alphaBitmapLbl.Name = "__alphaBitmapLbl";
			this.@__alphaBitmapLbl.Size = new System.Drawing.Size(263, 57);
			this.@__alphaBitmapLbl.TabIndex = 0;
			this.@__alphaBitmapLbl.Text = "Combine the RGB pixel data from one image with the alpha pixel information from a" +
				"nother image.";
			// 
			// __alphaBitmapBtn
			// 
			this.@__alphaBitmapBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__alphaBitmapBtn.Location = new System.Drawing.Point(199, 56);
			this.@__alphaBitmapBtn.Name = "__alphaBitmapBtn";
			this.@__alphaBitmapBtn.Size = new System.Drawing.Size(75, 23);
			this.@__alphaBitmapBtn.TabIndex = 1;
			this.@__alphaBitmapBtn.Text = "Launch...";
			this.@__alphaBitmapBtn.UseVisualStyleBackColor = true;
			// 
			// __pngToArgbGrp
			// 
			this.@__pngToArgbGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__pngToArgbGrp.Controls.Add(this.@__pngToArgbBtn);
			this.@__pngToArgbGrp.Controls.Add(this.@__pngToArgb);
			this.@__pngToArgbGrp.Controls.Add(this.label1);
			this.@__pngToArgbGrp.Location = new System.Drawing.Point(12, 103);
			this.@__pngToArgbGrp.Name = "__pngToArgbGrp";
			this.@__pngToArgbGrp.Size = new System.Drawing.Size(280, 111);
			this.@__pngToArgbGrp.TabIndex = 1;
			this.@__pngToArgbGrp.TabStop = false;
			this.@__pngToArgbGrp.Text = "PNG to ARGB";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(6, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(263, 57);
			this.label1.TabIndex = 1;
			this.label1.Text = "Convert a directory full of PNG images to 32-bit ARGB Bitmaps.";
			// 
			// __pngToArgb
			// 
			this.@__pngToArgb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__pngToArgb.Location = new System.Drawing.Point(6, 83);
			this.@__pngToArgb.Name = "__pngToArgb";
			this.@__pngToArgb.Size = new System.Drawing.Size(182, 20);
			this.@__pngToArgb.TabIndex = 2;
			// 
			// __pngToArgbBtn
			// 
			this.@__pngToArgbBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__pngToArgbBtn.Location = new System.Drawing.Point(194, 81);
			this.@__pngToArgbBtn.Name = "__pngToArgbBtn";
			this.@__pngToArgbBtn.Size = new System.Drawing.Size(75, 23);
			this.@__pngToArgbBtn.TabIndex = 3;
			this.@__pngToArgbBtn.Text = "Convert";
			this.@__pngToArgbBtn.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(304, 227);
			this.Controls.Add(this.@__pngToArgbGrp);
			this.Controls.Add(this.@__alphaBitmapGrp);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Image Tools";
			this.@__alphaBitmapGrp.ResumeLayout(false);
			this.@__pngToArgbGrp.ResumeLayout(false);
			this.@__pngToArgbGrp.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox __alphaBitmapGrp;
		private System.Windows.Forms.Label __alphaBitmapLbl;
		private System.Windows.Forms.Button __alphaBitmapBtn;
		private System.Windows.Forms.GroupBox __pngToArgbGrp;
		private System.Windows.Forms.Button __pngToArgbBtn;
		private System.Windows.Forms.TextBox __pngToArgb;
		private System.Windows.Forms.Label label1;
	}
}