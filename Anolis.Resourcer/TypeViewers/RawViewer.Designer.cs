namespace Anolis.Resourcer.TypeViewers {
	partial class RawViewer {
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
			this.@__hex = new Be.Windows.Forms.HexBox();
			this.@__status = new System.Windows.Forms.StatusStrip();
			this.@__statusSize = new System.Windows.Forms.ToolStripStatusLabel();
			this.@__statusType = new System.Windows.Forms.ToolStripStatusLabel();
			this.@__status.SuspendLayout();
			this.SuspendLayout();
			// 
			// __hex
			// 
			this.@__hex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__hex.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.@__hex.LineInfoForeColor = System.Drawing.Color.Empty;
			this.@__hex.LineInfoVisible = true;
			this.@__hex.Location = new System.Drawing.Point(0, 0);
			this.@__hex.Name = "__hex";
			this.@__hex.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			this.@__hex.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			this.@__hex.ShadowSelectionColor = System.Drawing.SystemColors.ControlDark;
			this.@__hex.Size = new System.Drawing.Size(644, 422);
			this.@__hex.StringViewVisible = true;
			this.@__hex.TabIndex = 0;
			this.@__hex.UseFixedBytesPerLine = true;
			this.@__hex.VScrollBarVisible = true;
			// 
			// __status
			// 
			this.@__status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__statusSize,
            this.@__statusType});
			this.@__status.Location = new System.Drawing.Point(0, 425);
			this.@__status.Name = "__status";
			this.@__status.Size = new System.Drawing.Size(644, 22);
			this.@__status.SizingGrip = false;
			this.@__status.TabIndex = 1;
			this.@__status.Text = "statusStrip1";
			// 
			// __statusSize
			// 
			this.@__statusSize.Name = "__statusSize";
			this.@__statusSize.Size = new System.Drawing.Size(49, 17);
			this.@__statusSize.Text = "Size: {0}";
			// 
			// __statusType
			// 
			this.@__statusType.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.@__statusType.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
			this.@__statusType.Name = "__statusType";
			this.@__statusType.Size = new System.Drawing.Size(58, 17);
			this.@__statusType.Text = "Type: {0}";
			// 
			// RawViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__status);
			this.Controls.Add(this.@__hex);
			this.Name = "RawViewer";
			this.Size = new System.Drawing.Size(644, 447);
			this.@__status.ResumeLayout(false);
			this.@__status.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Be.Windows.Forms.HexBox __hex;
		private System.Windows.Forms.StatusStrip __status;
		private System.Windows.Forms.ToolStripStatusLabel __statusSize;
		private System.Windows.Forms.ToolStripStatusLabel __statusType;
	}
}
