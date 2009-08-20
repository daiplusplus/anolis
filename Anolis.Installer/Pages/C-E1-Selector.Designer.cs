namespace Anolis.Installer.Pages {
	partial class SelectorPage {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.@__preview = new System.Windows.Forms.PictureBox();
			this.@__advanced = new System.Windows.Forms.Button();
			this.@__presets = new System.Windows.Forms.ComboBox();
			this.@__bitmapsLbl = new System.Windows.Forms.Label();
			this.@__bitmaps = new System.Windows.Forms.ComboBox();
			this.@__visualStylesLbl = new System.Windows.Forms.Label();
			this.@__visualStyles = new System.Windows.Forms.ComboBox();
			this.@__wallpaperLbl = new System.Windows.Forms.Label();
			this.@__wallpaper = new System.Windows.Forms.ComboBox();
			this.@__fields = new System.Windows.Forms.GroupBox();
			this.@__logonLbl = new System.Windows.Forms.Label();
			this.@__logonPreview = new System.Windows.Forms.CheckBox();
			this.@__logon = new System.Windows.Forms.ComboBox();
			this.@__previewLbl = new System.Windows.Forms.Label();
			this.@__content.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.@__preview)).BeginInit();
			this.@__fields.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__previewLbl);
			this.@__content.Controls.Add(this.@__presets);
			this.@__content.Controls.Add(this.@__fields);
			this.@__content.Controls.Add(this.@__preview);
			// 
			// __preview
			// 
			this.@__preview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__preview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.@__preview.Location = new System.Drawing.Point(3, 19);
			this.@__preview.Name = "__preview";
			this.@__preview.Size = new System.Drawing.Size(266, 210);
			this.@__preview.TabIndex = 4;
			this.@__preview.TabStop = false;
			// 
			// __advanced
			// 
			this.@__advanced.Location = new System.Drawing.Point(91, 187);
			this.@__advanced.Name = "__advanced";
			this.@__advanced.Size = new System.Drawing.Size(75, 23);
			this.@__advanced.TabIndex = 4;
			this.@__advanced.Text = "Advanced";
			this.@__advanced.UseVisualStyleBackColor = true;
			// 
			// __presets
			// 
			this.@__presets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__presets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.@__presets.FormattingEnabled = true;
			this.@__presets.Location = new System.Drawing.Point(288, 0);
			this.@__presets.Name = "__presets";
			this.@__presets.Size = new System.Drawing.Size(148, 21);
			this.@__presets.TabIndex = 0;
			// 
			// __bitmapsLbl
			// 
			this.@__bitmapsLbl.AutoSize = true;
			this.@__bitmapsLbl.Location = new System.Drawing.Point(6, 24);
			this.@__bitmapsLbl.Name = "__bitmapsLbl";
			this.@__bitmapsLbl.Size = new System.Drawing.Size(44, 13);
			this.@__bitmapsLbl.TabIndex = 12;
			this.@__bitmapsLbl.Text = "Bitmaps";
			// 
			// __bitmaps
			// 
			this.@__bitmaps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.@__bitmaps.FormattingEnabled = true;
			this.@__bitmaps.Location = new System.Drawing.Point(6, 40);
			this.@__bitmaps.Name = "__bitmaps";
			this.@__bitmaps.Size = new System.Drawing.Size(160, 21);
			this.@__bitmaps.TabIndex = 0;
			// 
			// __visualStylesLbl
			// 
			this.@__visualStylesLbl.AutoSize = true;
			this.@__visualStylesLbl.Location = new System.Drawing.Point(6, 144);
			this.@__visualStylesLbl.Name = "__visualStylesLbl";
			this.@__visualStylesLbl.Size = new System.Drawing.Size(61, 13);
			this.@__visualStylesLbl.TabIndex = 10;
			this.@__visualStylesLbl.Text = "Visual Style";
			// 
			// __visualStyles
			// 
			this.@__visualStyles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__visualStyles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.@__visualStyles.FormattingEnabled = true;
			this.@__visualStyles.Location = new System.Drawing.Point(6, 160);
			this.@__visualStyles.Name = "__visualStyles";
			this.@__visualStyles.Size = new System.Drawing.Size(160, 21);
			this.@__visualStyles.TabIndex = 3;
			// 
			// __wallpaperLbl
			// 
			this.@__wallpaperLbl.AutoSize = true;
			this.@__wallpaperLbl.Location = new System.Drawing.Point(6, 104);
			this.@__wallpaperLbl.Name = "__wallpaperLbl";
			this.@__wallpaperLbl.Size = new System.Drawing.Size(55, 13);
			this.@__wallpaperLbl.TabIndex = 11;
			this.@__wallpaperLbl.Text = "Wallpaper";
			// 
			// __wallpaper
			// 
			this.@__wallpaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.@__wallpaper.FormattingEnabled = true;
			this.@__wallpaper.Location = new System.Drawing.Point(6, 120);
			this.@__wallpaper.Name = "__wallpaper";
			this.@__wallpaper.Size = new System.Drawing.Size(160, 21);
			this.@__wallpaper.TabIndex = 2;
			// 
			// __fields
			// 
			this.@__fields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__fields.Controls.Add(this.@__bitmapsLbl);
			this.@__fields.Controls.Add(this.@__bitmaps);
			this.@__fields.Controls.Add(this.@__logonLbl);
			this.@__fields.Controls.Add(this.@__logonPreview);
			this.@__fields.Controls.Add(this.@__logon);
			this.@__fields.Controls.Add(this.@__wallpaperLbl);
			this.@__fields.Controls.Add(this.@__wallpaper);
			this.@__fields.Controls.Add(this.@__visualStylesLbl);
			this.@__fields.Controls.Add(this.@__visualStyles);
			this.@__fields.Controls.Add(this.@__advanced);
			this.@__fields.Location = new System.Drawing.Point(274, 3);
			this.@__fields.Name = "__fields";
			this.@__fields.Size = new System.Drawing.Size(170, 226);
			this.@__fields.TabIndex = 13;
			this.@__fields.TabStop = false;
			// 
			// __logonLbl
			// 
			this.@__logonLbl.AutoSize = true;
			this.@__logonLbl.Location = new System.Drawing.Point(6, 64);
			this.@__logonLbl.Name = "__logonLbl";
			this.@__logonLbl.Size = new System.Drawing.Size(89, 13);
			this.@__logonLbl.TabIndex = 14;
			this.@__logonLbl.Text = "Welcome Screen";
			// 
			// __logonPreview
			// 
			this.@__logonPreview.AutoSize = true;
			this.@__logonPreview.Location = new System.Drawing.Point(102, 63);
			this.@__logonPreview.Name = "__logonPreview";
			this.@__logonPreview.Size = new System.Drawing.Size(64, 17);
			this.@__logonPreview.TabIndex = 15;
			this.@__logonPreview.Text = "Preview";
			this.@__logonPreview.UseVisualStyleBackColor = true;
			// 
			// __logon
			// 
			this.@__logon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.@__logon.FormattingEnabled = true;
			this.@__logon.Location = new System.Drawing.Point(6, 80);
			this.@__logon.Name = "__logon";
			this.@__logon.Size = new System.Drawing.Size(160, 21);
			this.@__logon.TabIndex = 1;
			// 
			// __previewLbl
			// 
			this.@__previewLbl.AutoSize = true;
			this.@__previewLbl.Location = new System.Drawing.Point(3, 3);
			this.@__previewLbl.Name = "__previewLbl";
			this.@__previewLbl.Size = new System.Drawing.Size(70, 13);
			this.@__previewLbl.TabIndex = 14;
			this.@__previewLbl.Text = "Preview Area";
			// 
			// SelectorPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "SelectorPage";
			this.@__content.ResumeLayout(false);
			this.@__content.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.@__preview)).EndInit();
			this.@__fields.ResumeLayout(false);
			this.@__fields.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox __preview;
		private System.Windows.Forms.Button __advanced;
		private System.Windows.Forms.ComboBox __presets;
		private System.Windows.Forms.GroupBox __fields;
		private System.Windows.Forms.ComboBox __wallpaper;
		private System.Windows.Forms.Label __wallpaperLbl;
		private System.Windows.Forms.Label __bitmapsLbl;
		private System.Windows.Forms.ComboBox __visualStyles;
		private System.Windows.Forms.ComboBox __bitmaps;
		private System.Windows.Forms.Label __visualStylesLbl;
		private System.Windows.Forms.Label __previewLbl;
		private System.Windows.Forms.ComboBox __logon;
		private System.Windows.Forms.Label __logonLbl;
		private System.Windows.Forms.CheckBox __logonPreview;
	}
}
