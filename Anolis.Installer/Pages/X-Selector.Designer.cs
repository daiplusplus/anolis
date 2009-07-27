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
			this.@__visualStyles = new System.Windows.Forms.ComboBox();
			this.@__wallpaper = new System.Windows.Forms.ComboBox();
			this.@__visualStylesLbl = new System.Windows.Forms.Label();
			this.@__wallpaperLbl = new System.Windows.Forms.Label();
			this.@__preview = new System.Windows.Forms.PictureBox();
			this.@__bitmaps = new System.Windows.Forms.ComboBox();
			this.@__bitmapsLbl = new System.Windows.Forms.Label();
			this.@__advanced = new System.Windows.Forms.Button();
			this.@__grp = new System.Windows.Forms.GroupBox();
			this.@__presets = new System.Windows.Forms.ComboBox();
			this.@__content.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.@__preview)).BeginInit();
			this.@__grp.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__preview);
			// 
			// __visualStyles
			// 
			this.@__visualStyles.FormattingEnabled = true;
			this.@__visualStyles.Location = new System.Drawing.Point(173, 44);
			this.@__visualStyles.Name = "__visualStyles";
			this.@__visualStyles.Size = new System.Drawing.Size(143, 21);
			this.@__visualStyles.TabIndex = 1;
			// 
			// __wallpaper
			// 
			this.@__wallpaper.FormattingEnabled = true;
			this.@__wallpaper.Location = new System.Drawing.Point(322, 44);
			this.@__wallpaper.Name = "__wallpaper";
			this.@__wallpaper.Size = new System.Drawing.Size(109, 21);
			this.@__wallpaper.TabIndex = 2;
			// 
			// __visualStylesLbl
			// 
			this.@__visualStylesLbl.AutoSize = true;
			this.@__visualStylesLbl.Location = new System.Drawing.Point(170, 28);
			this.@__visualStylesLbl.Name = "__visualStylesLbl";
			this.@__visualStylesLbl.Size = new System.Drawing.Size(61, 13);
			this.@__visualStylesLbl.TabIndex = 2;
			this.@__visualStylesLbl.Text = "Visual Style";
			// 
			// __wallpaperLbl
			// 
			this.@__wallpaperLbl.AutoSize = true;
			this.@__wallpaperLbl.Location = new System.Drawing.Point(319, 28);
			this.@__wallpaperLbl.Name = "__wallpaperLbl";
			this.@__wallpaperLbl.Size = new System.Drawing.Size(55, 13);
			this.@__wallpaperLbl.TabIndex = 3;
			this.@__wallpaperLbl.Text = "Wallpaper";
			// 
			// __preview
			// 
			this.@__preview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.@__preview.Location = new System.Drawing.Point(0, 79);
			this.@__preview.Name = "__preview";
			this.@__preview.Size = new System.Drawing.Size(447, 153);
			this.@__preview.TabIndex = 4;
			this.@__preview.TabStop = false;
			// 
			// __bitmaps
			// 
			this.@__bitmaps.FormattingEnabled = true;
			this.@__bitmaps.Location = new System.Drawing.Point(17, 44);
			this.@__bitmaps.Name = "__bitmaps";
			this.@__bitmaps.Size = new System.Drawing.Size(150, 21);
			this.@__bitmaps.TabIndex = 0;
			// 
			// __bitmapsLbl
			// 
			this.@__bitmapsLbl.AutoSize = true;
			this.@__bitmapsLbl.Location = new System.Drawing.Point(14, 28);
			this.@__bitmapsLbl.Name = "__bitmapsLbl";
			this.@__bitmapsLbl.Size = new System.Drawing.Size(44, 13);
			this.@__bitmapsLbl.TabIndex = 6;
			this.@__bitmapsLbl.Text = "Bitmaps";
			// 
			// __advanced
			// 
			this.@__advanced.Location = new System.Drawing.Point(387, 10);
			this.@__advanced.Name = "__advanced";
			this.@__advanced.Size = new System.Drawing.Size(75, 23);
			this.@__advanced.TabIndex = 1;
			this.@__advanced.Text = "Advanced";
			this.@__advanced.UseVisualStyleBackColor = true;
			// 
			// __grp
			// 
			this.@__grp.Controls.Add(this.@__bitmapsLbl);
			this.@__grp.Controls.Add(this.@__bitmaps);
			this.@__grp.Controls.Add(this.@__visualStylesLbl);
			this.@__grp.Controls.Add(this.@__visualStyles);
			this.@__grp.Controls.Add(this.@__wallpaperLbl);
			this.@__grp.Controls.Add(this.@__wallpaper);
			this.@__grp.Location = new System.Drawing.Point(28, 14);
			this.@__grp.Name = "__grp";
			this.@__grp.Size = new System.Drawing.Size(447, 73);
			this.@__grp.TabIndex = 8;
			this.@__grp.TabStop = false;
			// 
			// __presets
			// 
			this.@__presets.FormattingEnabled = true;
			this.@__presets.Location = new System.Drawing.Point(44, 10);
			this.@__presets.Name = "__presets";
			this.@__presets.Size = new System.Drawing.Size(171, 21);
			this.@__presets.TabIndex = 0;
			// 
			// SelectorPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__presets);
			this.Controls.Add(this.@__advanced);
			this.Controls.Add(this.@__grp);
			this.Name = "SelectorPage";
			this.Controls.SetChildIndex(this.@__content, 0);
			this.Controls.SetChildIndex(this.@__grp, 0);
			this.Controls.SetChildIndex(this.@__advanced, 0);
			this.Controls.SetChildIndex(this.@__presets, 0);
			this.@__content.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.@__preview)).EndInit();
			this.@__grp.ResumeLayout(false);
			this.@__grp.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox __visualStyles;
		private System.Windows.Forms.ComboBox __wallpaper;
		private System.Windows.Forms.Label __visualStylesLbl;
		private System.Windows.Forms.Label __wallpaperLbl;
		private System.Windows.Forms.PictureBox __preview;
		private System.Windows.Forms.ComboBox __bitmaps;
		private System.Windows.Forms.Label __bitmapsLbl;
		private System.Windows.Forms.Button __advanced;
		private System.Windows.Forms.GroupBox __grp;
		private System.Windows.Forms.ComboBox __presets;
	}
}
