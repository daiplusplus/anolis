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
			this.@__hex.Size = new System.Drawing.Size(644, 447);
			this.@__hex.StringViewVisible = true;
			this.@__hex.TabIndex = 0;
			this.@__hex.UseFixedBytesPerLine = true;
			this.@__hex.VScrollBarVisible = true;
			// 
			// RawViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__hex);
			this.Name = "RawViewer";
			this.Size = new System.Drawing.Size(644, 447);
			this.ResumeLayout(false);

		}

		#endregion

		private Be.Windows.Forms.HexBox __hex;
	}
}
