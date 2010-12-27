namespace Anolis.Tools.AlphaBitmap {
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
			this.@__colorSourceLbl = new System.Windows.Forms.Label();
			this.@__colorSource = new System.Windows.Forms.TextBox();
			this.@__color = new System.Windows.Forms.PictureBox();
			this.@__alphaSourceLbl = new System.Windows.Forms.Label();
			this.@__alphaSource = new System.Windows.Forms.TextBox();
			this.@__colorBrowse = new System.Windows.Forms.Button();
			this.@__alphaBrowse = new System.Windows.Forms.Button();
			this.@__compose = new System.Windows.Forms.Button();
			this.@__colorLbl = new System.Windows.Forms.Label();
			this.@__layout = new System.Windows.Forms.TableLayoutPanel();
			this.@__alpha = new System.Windows.Forms.PictureBox();
			this.@__alphaLbl = new System.Windows.Forms.Label();
			this.@__save = new System.Windows.Forms.Button();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__sfd = new System.Windows.Forms.SaveFileDialog();
			this.@__finalLbl = new System.Windows.Forms.Label();
			this.@__final = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.@__color)).BeginInit();
			this.@__layout.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.@__alpha)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.@__final)).BeginInit();
			this.SuspendLayout();
			// 
			// __colorSourceLbl
			// 
			this.@__colorSourceLbl.AutoSize = true;
			this.@__colorSourceLbl.Location = new System.Drawing.Point(24, 9);
			this.@__colorSourceLbl.Name = "__colorSourceLbl";
			this.@__colorSourceLbl.Size = new System.Drawing.Size(123, 13);
			this.@__colorSourceLbl.TabIndex = 0;
			this.@__colorSourceLbl.Text = "Color Information Source";
			this.@__colorSourceLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __colorSource
			// 
			this.@__colorSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__colorSource.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.@__colorSource.Location = new System.Drawing.Point(153, 6);
			this.@__colorSource.Name = "__colorSource";
			this.@__colorSource.Size = new System.Drawing.Size(423, 20);
			this.@__colorSource.TabIndex = 1;
			// 
			// __color
			// 
			this.@__color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.@__color.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__color.Location = new System.Drawing.Point(3, 23);
			this.@__color.Name = "__color";
			this.@__color.Size = new System.Drawing.Size(206, 308);
			this.@__color.TabIndex = 2;
			this.@__color.TabStop = false;
			// 
			// __alphaSourceLbl
			// 
			this.@__alphaSourceLbl.AutoSize = true;
			this.@__alphaSourceLbl.Location = new System.Drawing.Point(21, 35);
			this.@__alphaSourceLbl.Name = "__alphaSourceLbl";
			this.@__alphaSourceLbl.Size = new System.Drawing.Size(126, 13);
			this.@__alphaSourceLbl.TabIndex = 3;
			this.@__alphaSourceLbl.Text = "Alpha Information Source";
			this.@__alphaSourceLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __alphaSource
			// 
			this.@__alphaSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__alphaSource.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.@__alphaSource.Location = new System.Drawing.Point(153, 32);
			this.@__alphaSource.Name = "__alphaSource";
			this.@__alphaSource.Size = new System.Drawing.Size(423, 20);
			this.@__alphaSource.TabIndex = 4;
			// 
			// __colorBrowse
			// 
			this.@__colorBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__colorBrowse.Location = new System.Drawing.Point(582, 4);
			this.@__colorBrowse.Name = "__colorBrowse";
			this.@__colorBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__colorBrowse.TabIndex = 5;
			this.@__colorBrowse.Text = "Browse...";
			this.@__colorBrowse.UseVisualStyleBackColor = true;
			// 
			// __alphaBrowse
			// 
			this.@__alphaBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__alphaBrowse.Location = new System.Drawing.Point(582, 30);
			this.@__alphaBrowse.Name = "__alphaBrowse";
			this.@__alphaBrowse.Size = new System.Drawing.Size(75, 23);
			this.@__alphaBrowse.TabIndex = 6;
			this.@__alphaBrowse.Text = "Browse...";
			this.@__alphaBrowse.UseVisualStyleBackColor = true;
			// 
			// __compose
			// 
			this.@__compose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__compose.Location = new System.Drawing.Point(490, 59);
			this.@__compose.Name = "__compose";
			this.@__compose.Size = new System.Drawing.Size(85, 23);
			this.@__compose.TabIndex = 7;
			this.@__compose.Text = "Compose";
			this.@__compose.UseVisualStyleBackColor = true;
			// 
			// __colorLbl
			// 
			this.@__colorLbl.AutoSize = true;
			this.@__colorLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__colorLbl.Location = new System.Drawing.Point(3, 0);
			this.@__colorLbl.Name = "__colorLbl";
			this.@__colorLbl.Size = new System.Drawing.Size(206, 20);
			this.@__colorLbl.TabIndex = 8;
			this.@__colorLbl.Text = "Color Information";
			// 
			// __layout
			// 
			this.@__layout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__layout.ColumnCount = 3;
			this.@__layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
			this.@__layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
			this.@__layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
			this.@__layout.Controls.Add(this.@__alpha, 1, 1);
			this.@__layout.Controls.Add(this.@__colorLbl, 0, 0);
			this.@__layout.Controls.Add(this.@__alphaLbl, 1, 0);
			this.@__layout.Controls.Add(this.@__color, 0, 1);
			this.@__layout.Controls.Add(this.@__finalLbl, 2, 0);
			this.@__layout.Controls.Add(this.@__final, 2, 1);
			this.@__layout.Location = new System.Drawing.Point(12, 88);
			this.@__layout.Name = "__layout";
			this.@__layout.RowCount = 2;
			this.@__layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.@__layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.@__layout.Size = new System.Drawing.Size(645, 334);
			this.@__layout.TabIndex = 9;
			// 
			// __alpha
			// 
			this.@__alpha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.@__alpha.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__alpha.Location = new System.Drawing.Point(215, 23);
			this.@__alpha.Name = "__alpha";
			this.@__alpha.Size = new System.Drawing.Size(206, 308);
			this.@__alpha.TabIndex = 10;
			this.@__alpha.TabStop = false;
			// 
			// __alphaLbl
			// 
			this.@__alphaLbl.AutoSize = true;
			this.@__alphaLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__alphaLbl.Location = new System.Drawing.Point(215, 0);
			this.@__alphaLbl.Name = "__alphaLbl";
			this.@__alphaLbl.Size = new System.Drawing.Size(206, 20);
			this.@__alphaLbl.TabIndex = 9;
			this.@__alphaLbl.Text = "Alpha Information";
			// 
			// __save
			// 
			this.@__save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__save.Location = new System.Drawing.Point(581, 59);
			this.@__save.Name = "__save";
			this.@__save.Size = new System.Drawing.Size(75, 23);
			this.@__save.TabIndex = 10;
			this.@__save.Text = "Save...";
			this.@__save.UseVisualStyleBackColor = true;
			// 
			// __ofd
			// 
			this.@__ofd.Filter = "GDI Images (*.bmp; *.jpg; *.png; *.gif)|*.bmp; *.jpg; *.png; *.gif";
			// 
			// __sfd
			// 
			this.@__sfd.Filter = "32-bit Bitmap (*.bmp)|*.bmp|PNG Image (*.png)|*.png";
			// 
			// __finalLbl
			// 
			this.@__finalLbl.AutoSize = true;
			this.@__finalLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__finalLbl.Location = new System.Drawing.Point(427, 0);
			this.@__finalLbl.Name = "__finalLbl";
			this.@__finalLbl.Size = new System.Drawing.Size(215, 20);
			this.@__finalLbl.TabIndex = 11;
			this.@__finalLbl.Text = "Final";
			// 
			// __final
			// 
			this.@__final.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.@__final.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__final.Location = new System.Drawing.Point(427, 23);
			this.@__final.Name = "__final";
			this.@__final.Size = new System.Drawing.Size(215, 308);
			this.@__final.TabIndex = 12;
			this.@__final.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(669, 434);
			this.Controls.Add(this.@__save);
			this.Controls.Add(this.@__layout);
			this.Controls.Add(this.@__compose);
			this.Controls.Add(this.@__alphaBrowse);
			this.Controls.Add(this.@__colorBrowse);
			this.Controls.Add(this.@__alphaSource);
			this.Controls.Add(this.@__alphaSourceLbl);
			this.Controls.Add(this.@__colorSource);
			this.Controls.Add(this.@__colorSourceLbl);
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Alpha Combiner";
			((System.ComponentModel.ISupportInitialize)(this.@__color)).EndInit();
			this.@__layout.ResumeLayout(false);
			this.@__layout.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.@__alpha)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.@__final)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label __colorSourceLbl;
		private System.Windows.Forms.TextBox __colorSource;
		private System.Windows.Forms.PictureBox __color;
		private System.Windows.Forms.Label __alphaSourceLbl;
		private System.Windows.Forms.TextBox __alphaSource;
		private System.Windows.Forms.Button __colorBrowse;
		private System.Windows.Forms.Button __alphaBrowse;
		private System.Windows.Forms.Button __compose;
		private System.Windows.Forms.Label __colorLbl;
		private System.Windows.Forms.TableLayoutPanel __layout;
		private System.Windows.Forms.Label __alphaLbl;
		private System.Windows.Forms.Button __save;
		private System.Windows.Forms.PictureBox __alpha;
		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.SaveFileDialog __sfd;
		private System.Windows.Forms.Label __finalLbl;
		private System.Windows.Forms.PictureBox __final;
	}
}

