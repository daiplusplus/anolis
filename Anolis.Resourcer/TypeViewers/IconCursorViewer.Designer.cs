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
			this.zoomPictureBoxWrapper1 = new Anolis.Resourcer.Controls.ZoomPictureBoxWrapper();
			this.@__split.Panel1.SuspendLayout();
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
			this.@__split.Panel1.Controls.Add(this.zoomPictureBoxWrapper1);
			this.@__split.Size = new System.Drawing.Size(361, 228);
			this.@__split.SplitterDistance = 151;
			this.@__split.TabIndex = 0;
			// 
			// zoomPictureBoxWrapper1
			// 
			this.zoomPictureBoxWrapper1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.zoomPictureBoxWrapper1.Image = null;
			this.zoomPictureBoxWrapper1.Location = new System.Drawing.Point(0, 0);
			this.zoomPictureBoxWrapper1.Name = "zoomPictureBoxWrapper1";
			this.zoomPictureBoxWrapper1.ShowInterpolationDropDown = true;
			this.zoomPictureBoxWrapper1.ShowToolbar = true;
			this.zoomPictureBoxWrapper1.ShowToolbarText = true;
			this.zoomPictureBoxWrapper1.Size = new System.Drawing.Size(361, 151);
			this.zoomPictureBoxWrapper1.TabIndex = 0;
			// 
			// IconCursorViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__split);
			this.Name = "IconCursorViewer";
			this.Size = new System.Drawing.Size(361, 228);
			this.@__split.Panel1.ResumeLayout(false);
			this.@__split.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer __split;
		private Anolis.Resourcer.Controls.ZoomPictureBoxWrapper zoomPictureBoxWrapper1;
	}
}
