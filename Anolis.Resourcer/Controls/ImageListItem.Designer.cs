namespace Anolis.Resourcer.Controls {
	partial class ImageListItem {
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
			this.@__pb = new Anolis.Resourcer.Controls.ZoomPictureBox();
			this.@__label = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// __pb
			// 
			this.@__pb.AutoScroll = true;
			this.@__pb.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.@__pb.Centered = true;
			this.@__pb.Image = null;
			this.@__pb.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			this.@__pb.Location = new System.Drawing.Point(1, 0);
			this.@__pb.Name = "__pb";
			this.@__pb.Size = new System.Drawing.Size(128, 128);
			this.@__pb.TabIndex = 0;
			this.@__pb.Text = "zoomPictureBox1";
			this.@__pb.Zoom = 1F;
			// 
			// __label
			// 
			this.@__label.Location = new System.Drawing.Point(3, 131);
			this.@__label.Name = "__label";
			this.@__label.Size = new System.Drawing.Size(124, 39);
			this.@__label.TabIndex = 1;
			this.@__label.Text = "32-bit - 32x32";
			this.@__label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// ImageListItem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__label);
			this.Controls.Add(this.@__pb);
			this.Name = "ImageListItem";
			this.Size = new System.Drawing.Size(130, 170);
			this.ResumeLayout(false);

		}

		#endregion

		private ZoomPictureBox __pb;
		private System.Windows.Forms.Label __label;
	}
}
