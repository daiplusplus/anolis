namespace Anolis.Resourcer.TypeViewers {
	partial class IconCursorViewer {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.@__currentImage = new Anolis.Resourcer.Controls.ZoomPictureBoxWrapper();
			this.@__images = new System.Windows.Forms.FlowLayoutPanel();
			this.SuspendLayout();
			// 
			// __currentImage
			// 
			this.@__currentImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__currentImage.Image = null;
			this.@__currentImage.Location = new System.Drawing.Point(0, 0);
			this.@__currentImage.Name = "__currentImage";
			this.@__currentImage.ShowInterpolationDropDown = true;
			this.@__currentImage.ShowToolbar = true;
			this.@__currentImage.ShowToolbarText = true;
			this.@__currentImage.Size = new System.Drawing.Size(672, 312);
			this.@__currentImage.TabIndex = 0;
			// 
			// __images
			// 
			this.@__images.AutoScroll = true;
			this.@__images.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.@__images.Location = new System.Drawing.Point(0, 318);
			this.@__images.Name = "__images";
			this.@__images.Size = new System.Drawing.Size(672, 80);
			this.@__images.TabIndex = 0;
			this.@__images.WrapContents = false;
			// 
			// IconCursorViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__images);
			this.Controls.Add(this.@__currentImage);
			this.Name = "IconCursorViewer";
			this.Size = new System.Drawing.Size(672, 398);
			this.ResumeLayout(false);

		}

		#endregion

		private Anolis.Resourcer.Controls.ZoomPictureBoxWrapper __currentImage;
		private System.Windows.Forms.FlowLayoutPanel __images;
	}
}
