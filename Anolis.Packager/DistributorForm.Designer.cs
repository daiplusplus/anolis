namespace Anolis.Packager {
	partial class DistributorForm {
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
			this.@__origLbl = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.@__browse = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// __origLbl
			// 
			this.@__origLbl.AutoSize = true;
			this.@__origLbl.Location = new System.Drawing.Point(12, 9);
			this.@__origLbl.Name = "__origLbl";
			this.@__origLbl.Size = new System.Drawing.Size(81, 13);
			this.@__origLbl.TabIndex = 0;
			this.@__origLbl.Text = "Original Installer";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(99, 6);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(389, 20);
			this.textBox1.TabIndex = 1;
			// 
			// __browse
			// 
			this.@__browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__browse.Location = new System.Drawing.Point(494, 4);
			this.@__browse.Name = "__browse";
			this.@__browse.Size = new System.Drawing.Size(75, 23);
			this.@__browse.TabIndex = 2;
			this.@__browse.Text = "Browse...";
			this.@__browse.UseVisualStyleBackColor = true;
			// 
			// DistributorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(581, 366);
			this.Controls.Add(this.@__browse);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.@__origLbl);
			this.Name = "DistributorForm";
			this.Text = "Distributor";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label __origLbl;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button __browse;
	}
}