namespace Anolis.Resourcer.TypeViewers {
	partial class ImageViewer {
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
			this.@__pv = new Anolis.Resourcer.Controls.ZoomPictureBoxWrapper();
			this.SuspendLayout();
			// 
			// __pv
			// 
			this.@__pv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__pv.Image = null;
			this.@__pv.Location = new System.Drawing.Point(0, 0);
			this.@__pv.Name = "__pv";
			this.@__pv.Size = new System.Drawing.Size(525, 295);
			this.@__pv.TabIndex = 0;
			// 
			// PictureViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.@__pv);
			this.Name = "PictureViewer";
			this.Size = new System.Drawing.Size(525, 295);
			this.ResumeLayout(false);

		}

		#endregion

		private Anolis.Resourcer.Controls.ZoomPictureBoxWrapper __pv;

	}
}
