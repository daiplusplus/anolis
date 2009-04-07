namespace Anolis.Resourcer {
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
			this.@__colAction = new System.Windows.Forms.ColumnHeader();
			this.@__colPath = new System.Windows.Forms.ColumnHeader();
			this.@__ok = new System.Windows.Forms.Button();
			this.@__cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// __operations
			// 
			this.@__operations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__operations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.@__colAction,
            this.@__colPath});
			this.@__operations.Location = new System.Drawing.Point(12, 12);
			this.@__operations.Name = "__operations";
			this.@__operations.Size = new System.Drawing.Size(319, 215);
			this.@__operations.TabIndex = 0;
			this.@__operations.UseCompatibleStateImageBehavior = false;
			this.@__operations.View = System.Windows.Forms.View.Details;
			// 
			// __colAction
			// 
			this.@__colAction.Text = "Action";
			// 
			// __colPath
			// 
			this.@__colPath.Text = "Path";
			this.@__colPath.Width = 240;
			// 
			// __ok
			// 
			this.@__ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__ok.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.@__ok.Location = new System.Drawing.Point(256, 233);
			this.@__ok.Name = "__ok";
			this.@__ok.Size = new System.Drawing.Size(75, 23);
			this.@__ok.TabIndex = 1;
			this.@__ok.Text = "OK";
			this.@__ok.UseVisualStyleBackColor = true;
			// 
			// __cancel
			// 
			this.@__cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__cancel.Enabled = false;
			this.@__cancel.Location = new System.Drawing.Point(12, 233);
			this.@__cancel.Name = "__cancel";
			this.@__cancel.Size = new System.Drawing.Size(124, 23);
			this.@__cancel.TabIndex = 2;
			this.@__cancel.Text = "Cancel Operation";
			this.@__cancel.UseVisualStyleBackColor = true;
			// 
			// PendingOperationsForm
			// 
			this.AcceptButton = this.@__ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.@__ok;
			this.ClientSize = new System.Drawing.Size(343, 268);
			this.Controls.Add(this.@__cancel);
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
		private System.Windows.Forms.ColumnHeader __colAction;
		private System.Windows.Forms.ColumnHeader __colPath;
		private System.Windows.Forms.Button __cancel;
	}
}