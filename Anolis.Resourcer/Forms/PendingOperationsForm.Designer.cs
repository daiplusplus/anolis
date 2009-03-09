namespace Anolis.Resourcer.Forms {
	partial class PendingOperationsForm {
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
			this.@__operations = new System.Windows.Forms.ListView();
			this.@__ok = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// __operations
			// 
			this.@__operations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__operations.Location = new System.Drawing.Point(12, 12);
			this.@__operations.Name = "__operations";
			this.@__operations.Size = new System.Drawing.Size(430, 269);
			this.@__operations.TabIndex = 0;
			this.@__operations.UseCompatibleStateImageBehavior = false;
			// 
			// __ok
			// 
			this.@__ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__ok.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.@__ok.Location = new System.Drawing.Point(367, 287);
			this.@__ok.Name = "__ok";
			this.@__ok.Size = new System.Drawing.Size(75, 23);
			this.@__ok.TabIndex = 1;
			this.@__ok.Text = "OK";
			this.@__ok.UseVisualStyleBackColor = true;
			// 
			// PendingOperationsForm
			// 
			this.AcceptButton = this.@__ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.@__ok;
			this.ClientSize = new System.Drawing.Size(454, 322);
			this.Controls.Add(this.@__ok);
			this.Controls.Add(this.@__operations);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PendingOperationsForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Pending Operations";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView __operations;
		private System.Windows.Forms.Button __ok;
	}
}