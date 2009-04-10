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
			System.Windows.Forms.ToolStripSeparator @__toolsSep2;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZoomPictureBoxWrapper));
			this.@__tools = new System.Windows.Forms.ToolStrip();
			this.@__toolsZoomIn = new System.Windows.Forms.ToolStripButton();
			this.@__toolsZoomOut = new System.Windows.Forms.ToolStripButton();
			this.@__toolsZoom100 = new System.Windows.Forms.ToolStripButton();
			this.@__toolsZoom = new System.Windows.Forms.ToolStripLabel();
			this.@__toolsCenter = new System.Windows.Forms.ToolStripButton();
			this.@__toolsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.@__toolsInterp = new System.Windows.Forms.ToolStripDropDownButton();
			this.@__toolsColor = new System.Windows.Forms.ToolStripDropDownButton();
			this.@__toolsColorTrans = new System.Windows.Forms.ToolStripMenuItem();
			this.@__toolsColorWhite = new System.Windows.Forms.ToolStripMenuItem();
			this.@__toolsColorBlack = new System.Windows.Forms.ToolStripMenuItem();
			this.@__toolsColorGrey = new System.Windows.Forms.ToolStripMenuItem();
			this.@__toolsColorMagenta = new System.Windows.Forms.ToolStripMenuItem();
			this.@__toolsInfo = new System.Windows.Forms.ToolStripButton();
			this.@__infoPanel = new System.Windows.Forms.Panel();
			this.@__iPxFormat = new System.Windows.Forms.Label();
			this.@__iFormat = new System.Windows.Forms.Label();
			this.@__iHeight = new System.Windows.Forms.Label();
			this.@__iWidth = new System.Windows.Forms.Label();
			this.@__iPxFormatLbl = new System.Windows.Forms.Label();
			this.@__iFormatLbl = new System.Windows.Forms.Label();
			this.@__iHeightLbl = new System.Windows.Forms.Label();
			this.@__iWidthLbl = new System.Windows.Forms.Label();
			this.@__pb = new Anolis.Resourcer.Controls.ZoomPictureBox();
			@__toolsSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.@__tools.SuspendLayout();
			this.@__infoPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// __toolsSep2
			// 
			@__toolsSep2.Name = "__toolsSep2";
			@__toolsSep2.Size = new System.Drawing.Size(6, 25);
			// 
			// __tools
			// 
			this.@__tools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.@__tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__toolsZoomIn,
            this.@__toolsZoomOut,
            this.@__toolsZoom100,
            this.@__toolsZoom,
            this.@__toolsCenter,
            this.@__toolsSep1,
            this.@__toolsInterp,
            this.@__toolsColor,
            @__toolsSep2,
            this.@__toolsInfo});
			this.@__tools.Location = new System.Drawing.Point(0, 0);
			this.@__tools.Name = "__tools";
			this.@__tools.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.@__tools.Size = new System.Drawing.Size(556, 25);
			this.@__tools.TabIndex = 2;
			this.@__tools.Text = "toolStrip1";
			// 
			// __toolsZoomIn
			// 
			this.@__toolsZoomIn.Image = global::Anolis.Resourcer.Properties.Resources.ImageViewer_ZoomIn;
			this.@__toolsZoomIn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.@__toolsZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsZoomIn.Name = "__toolsZoomIn";
			this.@__toolsZoomIn.Size = new System.Drawing.Size(66, 22);
			this.@__toolsZoomIn.Text = "Zoom In";
			// 
			// __toolsZoomOut
			// 
			this.@__toolsZoomOut.Image = global::Anolis.Resourcer.Properties.Resources.ImageViewer_ZoomOut;
			this.@__toolsZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsZoomOut.Name = "__toolsZoomOut";
			this.@__toolsZoomOut.Size = new System.Drawing.Size(74, 22);
			this.@__toolsZoomOut.Text = "Zoom Out";
			// 
			// __toolsZoom100
			// 
			this.@__toolsZoom100.Image = global::Anolis.Resourcer.Properties.Resources.ImageViewer_Zoom100;
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
			// __toolsCenter
			// 
			this.@__toolsCenter.Checked = true;
			this.@__toolsCenter.CheckOnClick = true;
			this.@__toolsCenter.CheckState = System.Windows.Forms.CheckState.Checked;
			this.@__toolsCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__toolsCenter.Image = global::Anolis.Resourcer.Properties.Resources.ImageViewer_Center;
			this.@__toolsCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsCenter.Name = "__toolsCenter";
			this.@__toolsCenter.Size = new System.Drawing.Size(23, 22);
			this.@__toolsCenter.Text = "Center Image";
			// 
			// __toolsSep1
			// 
			this.@__toolsSep1.Name = "__toolsSep1";
			this.@__toolsSep1.Size = new System.Drawing.Size(6, 25);
			// 
			// __toolsInterp
			// 
			this.@__toolsInterp.Image = global::Anolis.Resourcer.Properties.Resources.ImageViewer_Interpolation;
			this.@__toolsInterp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsInterp.Name = "__toolsInterp";
			this.@__toolsInterp.Size = new System.Drawing.Size(98, 22);
			this.@__toolsInterp.Text = "Interpolation";
			// 
			// __toolsColor
			// 
			this.@__toolsColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__toolsColor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__toolsColorTrans,
            this.@__toolsColorWhite,
            this.@__toolsColorBlack,
            this.@__toolsColorGrey,
            this.@__toolsColorMagenta});
			this.@__toolsColor.Image = ((System.Drawing.Image)(resources.GetObject("__toolsColor.Image")));
			this.@__toolsColor.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsColor.Name = "__toolsColor";
			this.@__toolsColor.Size = new System.Drawing.Size(29, 22);
			this.@__toolsColor.Text = "Background Color";
			// 
			// __toolsColorTrans
			// 
			this.@__toolsColorTrans.Image = global::Anolis.Resourcer.Properties.Resources.ImageViewer_TransparentBg;
			this.@__toolsColorTrans.Name = "__toolsColorTrans";
			this.@__toolsColorTrans.Size = new System.Drawing.Size(133, 22);
			this.@__toolsColorTrans.Text = "Transparent";
			// 
			// __toolsColorWhite
			// 
			this.@__toolsColorWhite.Name = "__toolsColorWhite";
			this.@__toolsColorWhite.Size = new System.Drawing.Size(133, 22);
			this.@__toolsColorWhite.Text = "White";
			// 
			// __toolsColorBlack
			// 
			this.@__toolsColorBlack.Name = "__toolsColorBlack";
			this.@__toolsColorBlack.Size = new System.Drawing.Size(133, 22);
			this.@__toolsColorBlack.Text = "Black";
			// 
			// __toolsColorGrey
			// 
			this.@__toolsColorGrey.Name = "__toolsColorGrey";
			this.@__toolsColorGrey.Size = new System.Drawing.Size(133, 22);
			this.@__toolsColorGrey.Text = "Grey";
			// 
			// __toolsColorMagenta
			// 
			this.@__toolsColorMagenta.Name = "__toolsColorMagenta";
			this.@__toolsColorMagenta.Size = new System.Drawing.Size(133, 22);
			this.@__toolsColorMagenta.Text = "Magenta";
			// 
			// __toolsInfo
			// 
			this.@__toolsInfo.CheckOnClick = true;
			this.@__toolsInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__toolsInfo.Image = global::Anolis.Resourcer.Properties.Resources.ImageViewer_Information;
			this.@__toolsInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsInfo.Name = "__toolsInfo";
			this.@__toolsInfo.Size = new System.Drawing.Size(23, 22);
			this.@__toolsInfo.Text = "Show Information";
			// 
			// __infoPanel
			// 
			this.@__infoPanel.Controls.Add(this.@__iPxFormat);
			this.@__infoPanel.Controls.Add(this.@__iFormat);
			this.@__infoPanel.Controls.Add(this.@__iHeight);
			this.@__infoPanel.Controls.Add(this.@__iWidth);
			this.@__infoPanel.Controls.Add(this.@__iPxFormatLbl);
			this.@__infoPanel.Controls.Add(this.@__iFormatLbl);
			this.@__infoPanel.Controls.Add(this.@__iHeightLbl);
			this.@__infoPanel.Controls.Add(this.@__iWidthLbl);
			this.@__infoPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.@__infoPanel.Location = new System.Drawing.Point(0, 218);
			this.@__infoPanel.Name = "__infoPanel";
			this.@__infoPanel.Size = new System.Drawing.Size(556, 56);
			this.@__infoPanel.TabIndex = 4;
			this.@__infoPanel.Visible = false;
			// 
			// __iPxFormat
			// 
			this.@__iPxFormat.AutoSize = true;
			this.@__iPxFormat.Location = new System.Drawing.Point(242, 33);
			this.@__iPxFormat.Name = "__iPxFormat";
			this.@__iPxFormat.Size = new System.Drawing.Size(67, 13);
			this.@__iPxFormat.TabIndex = 7;
			this.@__iPxFormat.Text = "32bppARGB";
			// 
			// __iFormat
			// 
			this.@__iFormat.AutoSize = true;
			this.@__iFormat.Location = new System.Drawing.Point(242, 11);
			this.@__iFormat.Name = "__iFormat";
			this.@__iFormat.Size = new System.Drawing.Size(61, 13);
			this.@__iFormat.TabIndex = 6;
			this.@__iFormat.Text = "Bitmap (V3)";
			// 
			// __iHeight
			// 
			this.@__iHeight.AutoSize = true;
			this.@__iHeight.Location = new System.Drawing.Point(71, 33);
			this.@__iHeight.Name = "__iHeight";
			this.@__iHeight.Size = new System.Drawing.Size(24, 13);
			this.@__iHeight.TabIndex = 5;
			this.@__iHeight.Text = "0px";
			// 
			// __iWidth
			// 
			this.@__iWidth.AutoSize = true;
			this.@__iWidth.Location = new System.Drawing.Point(71, 11);
			this.@__iWidth.Name = "__iWidth";
			this.@__iWidth.Size = new System.Drawing.Size(24, 13);
			this.@__iWidth.TabIndex = 4;
			this.@__iWidth.Text = "0px";
			// 
			// __iPxFormatLbl
			// 
			this.@__iPxFormatLbl.AutoSize = true;
			this.@__iPxFormatLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.@__iPxFormatLbl.Location = new System.Drawing.Point(154, 33);
			this.@__iPxFormatLbl.Name = "__iPxFormatLbl";
			this.@__iPxFormatLbl.Size = new System.Drawing.Size(80, 13);
			this.@__iPxFormatLbl.TabIndex = 3;
			this.@__iPxFormatLbl.Text = "Pixel Format:";
			// 
			// __iFormatLbl
			// 
			this.@__iFormatLbl.AutoSize = true;
			this.@__iFormatLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.@__iFormatLbl.Location = new System.Drawing.Point(147, 11);
			this.@__iFormatLbl.Name = "__iFormatLbl";
			this.@__iFormatLbl.Size = new System.Drawing.Size(87, 13);
			this.@__iFormatLbl.TabIndex = 2;
			this.@__iFormatLbl.Text = "Image Format:";
			// 
			// __iHeightLbl
			// 
			this.@__iHeightLbl.AutoSize = true;
			this.@__iHeightLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.@__iHeightLbl.Location = new System.Drawing.Point(18, 33);
			this.@__iHeightLbl.Name = "__iHeightLbl";
			this.@__iHeightLbl.Size = new System.Drawing.Size(48, 13);
			this.@__iHeightLbl.TabIndex = 1;
			this.@__iHeightLbl.Text = "Height:";
			// 
			// __iWidthLbl
			// 
			this.@__iWidthLbl.AutoSize = true;
			this.@__iWidthLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.@__iWidthLbl.Location = new System.Drawing.Point(22, 11);
			this.@__iWidthLbl.Name = "__iWidthLbl";
			this.@__iWidthLbl.Size = new System.Drawing.Size(44, 13);
			this.@__iWidthLbl.TabIndex = 0;
			this.@__iWidthLbl.Text = "Width:";
			// 
			// __pb
			// 
			this.@__pb.AutoScroll = true;
			this.@__pb.AutoScrollMargin = new System.Drawing.Size(556, 193);
			this.@__pb.AutoScrollMinSize = new System.Drawing.Size(1280, 853);
			this.@__pb.BackgroundImage = global::Anolis.Resourcer.Properties.Resources.ImageViewer_TransparentBg;
			this.@__pb.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.@__pb.Centered = true;
			this.@__pb.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__pb.Image = null;
			this.@__pb.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			this.@__pb.Location = new System.Drawing.Point(0, 25);
			this.@__pb.Name = "__pb";
			this.@__pb.Size = new System.Drawing.Size(556, 193);
			this.@__pb.TabIndex = 3;
			this.@__pb.Zoom = 1F;
			// 
			// ZoomPictureBoxWrapper
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.@__pb);
			this.Controls.Add(this.@__infoPanel);
			this.Controls.Add(this.@__tools);
			this.Name = "ZoomPictureBoxWrapper";
			this.Size = new System.Drawing.Size(556, 274);
			this.@__tools.ResumeLayout(false);
			this.@__tools.PerformLayout();
			this.@__infoPanel.ResumeLayout(false);
			this.@__infoPanel.PerformLayout();
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
		private System.Windows.Forms.ToolStripDropDownButton __toolsColor;
		private System.Windows.Forms.ToolStripMenuItem __toolsColorTrans;
		private System.Windows.Forms.ToolStripMenuItem __toolsColorWhite;
		private System.Windows.Forms.ToolStripMenuItem __toolsColorBlack;
		private System.Windows.Forms.ToolStripMenuItem __toolsColorGrey;
		private System.Windows.Forms.ToolStripMenuItem __toolsColorMagenta;
		private System.Windows.Forms.ToolStripSeparator __toolsSep1;
		private System.Windows.Forms.ToolStripButton __toolsInfo;
		private System.Windows.Forms.Panel __infoPanel;
		private System.Windows.Forms.Label __iWidthLbl;
		private System.Windows.Forms.Label __iPxFormatLbl;
		private System.Windows.Forms.Label __iFormatLbl;
		private System.Windows.Forms.Label __iHeightLbl;
		private System.Windows.Forms.Label __iPxFormat;
		private System.Windows.Forms.Label __iFormat;
		private System.Windows.Forms.Label __iHeight;
		private System.Windows.Forms.Label __iWidth;
	}
}
