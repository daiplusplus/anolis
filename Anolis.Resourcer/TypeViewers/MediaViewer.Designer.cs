namespace Anolis.Resourcer.TypeViewers {
	partial class MediaViewer {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediaViewer));
			this.@__wmp = new AxWMPLib.AxWindowsMediaPlayer();
			((System.ComponentModel.ISupportInitialize)(this.@__wmp)).BeginInit();
			this.SuspendLayout();
			// 
			// __wmp
			// 
			this.@__wmp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__wmp.Enabled = true;
			this.@__wmp.Location = new System.Drawing.Point(0, 0);
			this.@__wmp.Name = "__wmp";
			this.@__wmp.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("__wmp.OcxState")));
			this.@__wmp.Size = new System.Drawing.Size(585, 357);
			this.@__wmp.TabIndex = 0;
			// 
			// MediaViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.@__wmp);
			this.Name = "MediaViewer";
			this.Size = new System.Drawing.Size(585, 357);
			((System.ComponentModel.ISupportInitialize)(this.@__wmp)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private AxWMPLib.AxWindowsMediaPlayer __wmp;
	}
}
