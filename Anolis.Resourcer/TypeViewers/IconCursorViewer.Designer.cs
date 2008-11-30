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
			this.@__split = new System.Windows.Forms.SplitContainer();
			this.@__currentImage = new Anolis.Resourcer.Controls.ZoomPictureBoxWrapper();
			this.@__images = new Anolis.Resourcer.Controls.ImageList();
			this.@__split.Panel1.SuspendLayout();
			this.@__split.Panel2.SuspendLayout();
			this.@__split.SuspendLayout();
			this.SuspendLayout();
			// 
			// __split
			// 
			this.@__split.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__split.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.@__split.Location = new System.Drawing.Point(0, 0);
			this.@__split.Name = "__split";
			this.@__split.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// __split.Panel1
			// 
			this.@__split.Panel1.Controls.Add(this.@__currentImage);
			// 
			// __split.Panel2
			// 
			this.@__split.Panel2.Controls.Add(this.@__images);
			this.@__split.Size = new System.Drawing.Size(689, 420);
			this.@__split.SplitterDistance = 285;
			this.@__split.TabIndex = 0;
			// 
			// __currentImage
			// 
			this.@__currentImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__currentImage.Image = null;
			this.@__currentImage.Location = new System.Drawing.Point(0, 0);
			this.@__currentImage.Name = "__currentImage";
			this.@__currentImage.ShowInterpolationDropDown = true;
			this.@__currentImage.ShowToolbar = true;
			this.@__currentImage.ShowToolbarText = true;
			this.@__currentImage.Size = new System.Drawing.Size(689, 285);
			this.@__currentImage.TabIndex = 0;
			// 
			// __images
			// 
			this.@__images.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__images.Location = new System.Drawing.Point(0, 0);
			this.@__images.Name = "__images";
			this.@__images.Size = new System.Drawing.Size(689, 131);
			this.@__images.TabIndex = 0;
			// 
			// IconCursorViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__split);
			this.Name = "IconCursorViewer";
			this.Size = new System.Drawing.Size(689, 420);
			this.@__split.Panel1.ResumeLayout(false);
			this.@__split.Panel2.ResumeLayout(false);
			this.@__split.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer __split;
		private Anolis.Resourcer.Controls.ZoomPictureBoxWrapper __currentImage;
		private Anolis.Resourcer.Controls.ImageList __images;
	}
}
