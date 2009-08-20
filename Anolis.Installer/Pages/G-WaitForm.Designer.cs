namespace Anolis.Installer.Pages {
	partial class WaitForm {
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
			this.components = new System.ComponentModel.Container();
			this.@__marquee = new System.Windows.Forms.ProgressBar();
			this.@__restarting = new System.Windows.Forms.Label();
			this.@__timer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// __marquee
			// 
			this.@__marquee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__marquee.Location = new System.Drawing.Point(12, 40);
			this.@__marquee.Name = "__marquee";
			this.@__marquee.Size = new System.Drawing.Size(180, 16);
			this.@__marquee.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.@__marquee.TabIndex = 0;
			// 
			// __restarting
			// 
			this.@__restarting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__restarting.Location = new System.Drawing.Point(12, 11);
			this.@__restarting.Name = "__restarting";
			this.@__restarting.Size = new System.Drawing.Size(180, 23);
			this.@__restarting.TabIndex = 1;
			this.@__restarting.Text = "Now restarting your computer";
			this.@__restarting.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// __timer
			// 
			this.@__timer.Enabled = true;
			this.@__timer.Interval = 20000;
			// 
			// WaitForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(204, 68);
			this.ControlBox = false;
			this.Controls.Add(this.@__restarting);
			this.Controls.Add(this.@__marquee);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "WaitForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Please Wait...";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ProgressBar __marquee;
		private System.Windows.Forms.Label __restarting;
		private System.Windows.Forms.Timer __timer;
	}
}