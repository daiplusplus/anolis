namespace Anolis.Resourcer {
	partial class DebuggerForm {
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
			this.@__message = new System.Windows.Forms.Label();
			this.@__timer = new System.Windows.Forms.Timer(this.components);
			this.@__marq = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// __message
			// 
			this.@__message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__message.Location = new System.Drawing.Point(12, 9);
			this.@__message.Name = "__message";
			this.@__message.Size = new System.Drawing.Size(279, 36);
			this.@__message.TabIndex = 0;
			this.@__message.Text = "Waiting for debugger to attach to process (PID {0}).";
			this.@__message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// __marq
			// 
			this.@__marq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__marq.Location = new System.Drawing.Point(12, 48);
			this.@__marq.Name = "__marq";
			this.@__marq.Size = new System.Drawing.Size(279, 10);
			this.@__marq.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.@__marq.TabIndex = 1;
			// 
			// DebuggerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(303, 70);
			this.Controls.Add(this.@__marq);
			this.Controls.Add(this.@__message);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DebuggerForm";
			this.ShowIcon = false;
			this.Text = "Waiting for Debugger";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label __message;
		private System.Windows.Forms.Timer __timer;
		private System.Windows.Forms.ProgressBar __marq;
	}
}