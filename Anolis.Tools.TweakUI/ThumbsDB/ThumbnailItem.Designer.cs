namespace Anolis.Tools.TweakUI.ThumbsDB {
	partial class ThumbnailItem {
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
			this.@__pic = new System.Windows.Forms.PictureBox();
			this.@__caption = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.@__pic)).BeginInit();
			this.SuspendLayout();
			// 
			// __pic
			// 
			this.@__pic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__pic.Location = new System.Drawing.Point(3, 3);
			this.@__pic.Name = "__pic";
			this.@__pic.Size = new System.Drawing.Size(139, 98);
			this.@__pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.@__pic.TabIndex = 0;
			this.@__pic.TabStop = false;
			// 
			// __caption
			// 
			this.@__caption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__caption.Location = new System.Drawing.Point(3, 104);
			this.@__caption.Name = "__caption";
			this.@__caption.Size = new System.Drawing.Size(139, 30);
			this.@__caption.TabIndex = 1;
			this.@__caption.Text = "FileName Goes Here";
			// 
			// ThumbnailItem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__caption);
			this.Controls.Add(this.@__pic);
			this.Name = "ThumbnailItem";
			this.Size = new System.Drawing.Size(145, 134);
			((System.ComponentModel.ISupportInitialize)(this.@__pic)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label __caption;
		private System.Windows.Forms.PictureBox __pic;

	}
}
