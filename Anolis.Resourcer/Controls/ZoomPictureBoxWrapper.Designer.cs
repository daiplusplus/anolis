namespace Anolis.Resourcer.Controls {
	partial class ZoomPictureBoxWrapper {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZoomPictureBoxWrapper));
			this.@__tools = new System.Windows.Forms.ToolStrip();
			this.@__toolsZoomIn = new System.Windows.Forms.ToolStripButton();
			this.@__toolsZoomOut = new System.Windows.Forms.ToolStripButton();
			this.@__toolsZoom100 = new System.Windows.Forms.ToolStripButton();
			this.@__toolsZoom = new System.Windows.Forms.ToolStripLabel();
			this.@__toolsInterp = new System.Windows.Forms.ToolStripDropDownButton();
			this.@__toolsCenter = new System.Windows.Forms.ToolStripButton();
			this.@__pb = new Anolis.Resourcer.Controls.ZoomPictureBox();
			this.@__tools.SuspendLayout();
			this.SuspendLayout();
			// 
			// __tools
			// 
			this.@__tools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.@__tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__toolsZoomIn,
            this.@__toolsZoomOut,
            this.@__toolsZoom100,
            this.@__toolsZoom,
            this.@__toolsInterp,
            this.@__toolsCenter});
			this.@__tools.Location = new System.Drawing.Point(0, 0);
			this.@__tools.Name = "__tools";
			this.@__tools.Size = new System.Drawing.Size(413, 25);
			this.@__tools.TabIndex = 2;
			this.@__tools.Text = "toolStrip1";
			// 
			// __toolsZoomIn
			// 
			this.@__toolsZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("__toolsZoomIn.Image")));
			this.@__toolsZoomIn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.@__toolsZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsZoomIn.Name = "__toolsZoomIn";
			this.@__toolsZoomIn.Size = new System.Drawing.Size(66, 22);
			this.@__toolsZoomIn.Text = "Zoom In";
			// 
			// __toolsZoomOut
			// 
			this.@__toolsZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("__toolsZoomOut.Image")));
			this.@__toolsZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsZoomOut.Name = "__toolsZoomOut";
			this.@__toolsZoomOut.Size = new System.Drawing.Size(74, 22);
			this.@__toolsZoomOut.Text = "Zoom Out";
			// 
			// __toolsZoom100
			// 
			this.@__toolsZoom100.Image = ((System.Drawing.Image)(resources.GetObject("__toolsZoom100.Image")));
			this.@__toolsZoom100.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsZoom100.Name = "__toolsZoom100";
			this.@__toolsZoom100.Size = new System.Drawing.Size(85, 22);
			this.@__toolsZoom100.Text = "Zoom 100%";
			// 
			// __toolsZoom
			// 
			this.@__toolsZoom.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.@__toolsZoom.Name = "__toolsZoom";
			this.@__toolsZoom.Size = new System.Drawing.Size(36, 22);
			this.@__toolsZoom.Text = "100%";
			// 
			// __toolsInterp
			// 
			this.@__toolsInterp.Image = ((System.Drawing.Image)(resources.GetObject("__toolsInterp.Image")));
			this.@__toolsInterp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsInterp.Name = "__toolsInterp";
			this.@__toolsInterp.Size = new System.Drawing.Size(98, 22);
			this.@__toolsInterp.Text = "Interpolation";
			// 
			// __toolsCenter
			// 
			this.@__toolsCenter.Checked = true;
			this.@__toolsCenter.CheckOnClick = true;
			this.@__toolsCenter.CheckState = System.Windows.Forms.CheckState.Checked;
			this.@__toolsCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__toolsCenter.Image = ((System.Drawing.Image)(resources.GetObject("__toolsCenter.Image")));
			this.@__toolsCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsCenter.Name = "__toolsCenter";
			this.@__toolsCenter.Size = new System.Drawing.Size(23, 22);
			this.@__toolsCenter.Text = "Center Image";
			// 
			// __pb
			// 
			this.@__pb.AutoScroll = true;
			this.@__pb.AutoScrollMargin = new System.Drawing.Size(413, 241);
			this.@__pb.AutoScrollMinSize = new System.Drawing.Size(1280, 853);
			this.@__pb.BackgroundImage = global::Anolis.Resourcer.Properties.Resources.TransparentBg;
			this.@__pb.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.@__pb.Centered = true;
			this.@__pb.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__pb.Image = null;
			this.@__pb.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			this.@__pb.Location = new System.Drawing.Point(0, 25);
			this.@__pb.Name = "__pb";
			this.@__pb.Size = new System.Drawing.Size(413, 241);
			this.@__pb.TabIndex = 3;
			this.@__pb.Zoom = 1F;
			// 
			// ZoomPictureBoxWrapper
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__pb);
			this.Controls.Add(this.@__tools);
			this.Name = "ZoomPictureBoxWrapper";
			this.Size = new System.Drawing.Size(413, 266);
			this.@__tools.ResumeLayout(false);
			this.@__tools.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ZoomPictureBox __pb;
		private System.Windows.Forms.ToolStrip __tools;
		private System.Windows.Forms.ToolStripButton __toolsZoomIn;
		private System.Windows.Forms.ToolStripButton __toolsZoomOut;
		private System.Windows.Forms.ToolStripButton __toolsZoom100;
		private System.Windows.Forms.ToolStripLabel __toolsZoom;
		private System.Windows.Forms.ToolStripDropDownButton __toolsInterp;
		private System.Windows.Forms.ToolStripButton __toolsCenter;
	}
}
