namespace Anolis.Resourcer.TypeViewers {
	partial class AnimationViewer {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnimationViewer));
			this.@__ani = new Anolis.Resourcer.Controls.AnimationControl();
			this.@__t = new System.Windows.Forms.ToolStrip();
			this.@__tPlay = new System.Windows.Forms.ToolStripButton();
			this.@__tStop = new System.Windows.Forms.ToolStripButton();
			this.@__tTransparent = new System.Windows.Forms.ToolStripButton();
			this.@__t.SuspendLayout();
			this.SuspendLayout();
			// 
			// __ani
			// 
			this.@__ani.CenterVideo = true;
			this.@__ani.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__ani.IsPlaying = false;
			this.@__ani.Location = new System.Drawing.Point(0, 25);
			this.@__ani.MultithreadedVideo = true;
			this.@__ani.Name = "__ani";
			this.@__ani.Size = new System.Drawing.Size(757, 414);
			this.@__ani.TabIndex = 0;
			this.@__ani.TabStop = false;
			this.@__ani.TransparentVideo = false;
			// 
			// __t
			// 
			this.@__t.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.@__t.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__tPlay,
            this.@__tStop,
            this.@__tTransparent});
			this.@__t.Location = new System.Drawing.Point(0, 0);
			this.@__t.Name = "__t";
			this.@__t.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.@__t.Size = new System.Drawing.Size(757, 25);
			this.@__t.TabIndex = 1;
			this.@__t.Text = "toolStrip1";
			// 
			// __tPlay
			// 
			this.@__tPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.@__tPlay.Image = ((System.Drawing.Image)(resources.GetObject("__tPlay.Image")));
			this.@__tPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tPlay.Name = "__tPlay";
			this.@__tPlay.Size = new System.Drawing.Size(31, 22);
			this.@__tPlay.Text = "Play";
			// 
			// __tStop
			// 
			this.@__tStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.@__tStop.Image = ((System.Drawing.Image)(resources.GetObject("__tStop.Image")));
			this.@__tStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tStop.Name = "__tStop";
			this.@__tStop.Size = new System.Drawing.Size(33, 22);
			this.@__tStop.Text = "Stop";
			// 
			// __tTransparent
			// 
			this.@__tTransparent.CheckOnClick = true;
			this.@__tTransparent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.@__tTransparent.Image = ((System.Drawing.Image)(resources.GetObject("__tTransparent.Image")));
			this.@__tTransparent.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tTransparent.Name = "__tTransparent";
			this.@__tTransparent.Size = new System.Drawing.Size(70, 22);
			this.@__tTransparent.Text = "Transparent";
			// 
			// AnimationViewer
			// 
			this.Controls.Add(this.@__ani);
			this.Controls.Add(this.@__t);
			this.Name = "AnimationViewer";
			this.Size = new System.Drawing.Size(757, 439);
			this.@__t.ResumeLayout(false);
			this.@__t.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Anolis.Resourcer.Controls.AnimationControl __ani;
		private System.Windows.Forms.ToolStrip __t;
		private System.Windows.Forms.ToolStripButton __tPlay;
		private System.Windows.Forms.ToolStripButton __tStop;
		private System.Windows.Forms.ToolStripButton __tTransparent;
	}
}
