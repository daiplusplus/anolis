namespace Anolis.Resourcer {
	partial class ReplaceResourceForm {
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
			this.@__oldLbl = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// __oldLbl
			// 
			this.@__oldLbl.AutoSize = true;
			this.@__oldLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.@__oldLbl.Location = new System.Drawing.Point(12, 9);
			this.@__oldLbl.Name = "__oldLbl";
			this.@__oldLbl.Size = new System.Drawing.Size(64, 13);
			this.@__oldLbl.TabIndex = 0;
			this.@__oldLbl.Text = "Replacing";
			// 
			// ReplaceResourceForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(380, 363);
			this.Controls.Add(this.@__oldLbl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ReplaceResourceForm";
			this.ShowIcon = false;
			this.Text = "Replace Resource";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label __oldLbl;
	}
}