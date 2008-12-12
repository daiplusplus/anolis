namespace Anolis.Resourcer {
	partial class MainForm {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.@__filePathLbl = new System.Windows.Forms.Label();
			this.@__filePath = new System.Windows.Forms.TextBox();
			this.@__browse = new System.Windows.Forms.Button();
			this.@__resources = new System.Windows.Forms.TreeView();
			this.@__split = new System.Windows.Forms.SplitContainer();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__sfd = new System.Windows.Forms.SaveFileDialog();
			this.@__status = new System.Windows.Forms.StatusStrip();
			this.@__split.Panel1.SuspendLayout();
			this.@__split.SuspendLayout();
			this.SuspendLayout();
			// 
			// __filePathLbl
			// 
			this.@__filePathLbl.AutoSize = true;
			this.@__filePathLbl.Location = new System.Drawing.Point(12, 9);
			this.@__filePathLbl.Name = "__filePathLbl";
			this.@__filePathLbl.Size = new System.Drawing.Size(26, 13);
			this.@__filePathLbl.TabIndex = 0;
			this.@__filePathLbl.Text = "File:";
			// 
			// __filePath
			// 
			this.@__filePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__filePath.Location = new System.Drawing.Point(44, 6);
			this.@__filePath.Name = "__filePath";
			this.@__filePath.Size = new System.Drawing.Size(494, 20);
			this.@__filePath.TabIndex = 1;
			// 
			// __browse
			// 
			this.@__browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__browse.Location = new System.Drawing.Point(544, 4);
			this.@__browse.Name = "__browse";
			this.@__browse.Size = new System.Drawing.Size(75, 23);
			this.@__browse.TabIndex = 2;
			this.@__browse.Text = "Browse...";
			this.@__browse.UseVisualStyleBackColor = true;
			// 
			// __resources
			// 
			this.@__resources.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__resources.Location = new System.Drawing.Point(0, 0);
			this.@__resources.Name = "__resources";
			this.@__resources.Size = new System.Drawing.Size(134, 376);
			this.@__resources.TabIndex = 3;
			// 
			// __split
			// 
			this.@__split.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__split.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.@__split.Location = new System.Drawing.Point(0, 32);
			this.@__split.Margin = new System.Windows.Forms.Padding(0);
			this.@__split.Name = "__split";
			// 
			// __split.Panel1
			// 
			this.@__split.Panel1.Controls.Add(this.@__resources);
			this.@__split.Size = new System.Drawing.Size(619, 376);
			this.@__split.SplitterDistance = 134;
			this.@__split.TabIndex = 4;
			// 
			// __status
			// 
			this.@__status.Location = new System.Drawing.Point(0, 411);
			this.@__status.Name = "__status";
			this.@__status.Size = new System.Drawing.Size(628, 22);
			this.@__status.TabIndex = 5;
			this.@__status.Text = "statusStrip1";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(628, 433);
			this.Controls.Add(this.@__status);
			this.Controls.Add(this.@__split);
			this.Controls.Add(this.@__browse);
			this.Controls.Add(this.@__filePath);
			this.Controls.Add(this.@__filePathLbl);
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Anolis Resourcer";
			this.@__split.Panel1.ResumeLayout(false);
			this.@__split.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label __filePathLbl;
		private System.Windows.Forms.TextBox __filePath;
		private System.Windows.Forms.Button __browse;
		private System.Windows.Forms.TreeView __resources;
		private System.Windows.Forms.SplitContainer __split;
		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.SaveFileDialog __sfd;
		private System.Windows.Forms.StatusStrip __status;
	}
}

