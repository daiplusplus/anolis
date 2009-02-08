namespace Anolis.Gui.Pages {
	partial class BaseInteriorPage {
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
			this.@__bannerImage = new System.Windows.Forms.PictureBox();
			this.@__banner.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.@__bannerImage)).BeginInit();
			this.SuspendLayout();
			// 
			// __banner
			// 
			this.@__banner.Controls.Add(this.@__bannerImage);
			this.@__banner.Controls.SetChildIndex(this.@__bannerTitle, 0);
			this.@__banner.Controls.SetChildIndex(this.@__bannerSubtitle, 0);
			this.@__banner.Controls.SetChildIndex(this.@__bannerImage, 0);
			// 
			// __bannerSubtitle
			// 
			this.@__bannerSubtitle.Size = new System.Drawing.Size(350, 30);
			// 
			// __bannerTitle
			// 
			this.@__bannerTitle.Size = new System.Drawing.Size(372, 13);
			// 
			// __bannerImage
			// 
			this.@__bannerImage.Dock = System.Windows.Forms.DockStyle.Right;
			this.@__bannerImage.Image = global::Anolis.Gui.Properties.Resources.Banner;
			this.@__bannerImage.Location = new System.Drawing.Point(397, 0);
			this.@__bannerImage.Name = "__bannerImage";
			this.@__bannerImage.Size = new System.Drawing.Size(106, 58);
			this.@__bannerImage.TabIndex = 4;
			this.@__bannerImage.TabStop = false;
			// 
			// BaseInteriorPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "BaseInteriorPage";
			this.@__banner.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.@__bannerImage)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox __bannerImage;
	}
}
