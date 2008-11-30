namespace Anolis.Resourcer.Controls {
	partial class ImageList {
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
			this.@__flow = new System.Windows.Forms.FlowLayoutPanel();
			this.@__panel = new System.Windows.Forms.Panel();
			this.@__panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// __flow
			// 
			this.@__flow.AutoSize = true;
			this.@__flow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.@__flow.Location = new System.Drawing.Point(0, 0);
			this.@__flow.MinimumSize = new System.Drawing.Size(128, 128);
			this.@__flow.Name = "__flow";
			this.@__flow.Size = new System.Drawing.Size(128, 128);
			this.@__flow.TabIndex = 0;
			this.@__flow.WrapContents = false;
			// 
			// __panel
			// 
			this.@__panel.AutoScroll = true;
			this.@__panel.Controls.Add(this.@__flow);
			this.@__panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__panel.Location = new System.Drawing.Point(0, 0);
			this.@__panel.Name = "__panel";
			this.@__panel.Size = new System.Drawing.Size(590, 155);
			this.@__panel.TabIndex = 1;
			// 
			// ImageList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__panel);
			this.Name = "ImageList";
			this.Size = new System.Drawing.Size(590, 155);
			this.@__panel.ResumeLayout(false);
			this.@__panel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel __flow;
		private System.Windows.Forms.Panel __panel;
	}
}
