namespace Anolis.Packager {
	partial class ProgressForm {
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
			this.@__progress = new System.Windows.Forms.ProgressBar();
			this.@__status = new System.Windows.Forms.Label();
			this.@__time = new System.Windows.Forms.Label();
			this.@__timer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// __progress
			// 
			this.@__progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__progress.Location = new System.Drawing.Point(12, 31);
			this.@__progress.Name = "__progress";
			this.@__progress.Size = new System.Drawing.Size(402, 23);
			this.@__progress.TabIndex = 0;
			// 
			// __status
			// 
			this.@__status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__status.Location = new System.Drawing.Point(9, 9);
			this.@__status.Name = "__status";
			this.@__status.Size = new System.Drawing.Size(405, 13);
			this.@__status.TabIndex = 1;
			this.@__status.Text = "Working...";
			// 
			// __time
			// 
			this.@__time.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__time.Location = new System.Drawing.Point(9, 59);
			this.@__time.Name = "__time";
			this.@__time.Size = new System.Drawing.Size(405, 24);
			this.@__time.TabIndex = 2;
			// 
			// __timer
			// 
			this.@__timer.Interval = 250;
			// 
			// ProgressForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(426, 92);
			this.ControlBox = false;
			this.Controls.Add(this.@__status);
			this.Controls.Add(this.@__time);
			this.Controls.Add(this.@__progress);
			this.Name = "ProgressForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Working...";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ProgressBar __progress;
		private System.Windows.Forms.Label __status;
		private System.Windows.Forms.Label __time;
		private System.Windows.Forms.Timer __timer;
	}
}