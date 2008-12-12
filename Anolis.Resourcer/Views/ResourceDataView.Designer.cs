namespace Anolis.Resourcer {
	partial class ResourceDataView {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.@__viewer = new System.Windows.Forms.Panel();
			this.@__viewers = new System.Windows.Forms.ComboBox();
			this.@__saveRaw = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// __viewer
			// 
			this.@__viewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__viewer.Location = new System.Drawing.Point(3, 27);
			this.@__viewer.Name = "__viewer";
			this.@__viewer.Size = new System.Drawing.Size(757, 454);
			this.@__viewer.TabIndex = 5;
			// 
			// __viewers
			// 
			this.@__viewers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__viewers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.@__viewers.FormattingEnabled = true;
			this.@__viewers.Location = new System.Drawing.Point(0, 0);
			this.@__viewers.Name = "__viewers";
			this.@__viewers.Size = new System.Drawing.Size(679, 21);
			this.@__viewers.TabIndex = 4;
			// 
			// __saveRaw
			// 
			this.@__saveRaw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__saveRaw.Location = new System.Drawing.Point(685, -2);
			this.@__saveRaw.Name = "__saveRaw";
			this.@__saveRaw.Size = new System.Drawing.Size(75, 23);
			this.@__saveRaw.TabIndex = 3;
			this.@__saveRaw.Text = "Save As";
			this.@__saveRaw.UseVisualStyleBackColor = true;
			// 
			// ResourceDataView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__viewer);
			this.Controls.Add(this.@__viewers);
			this.Controls.Add(this.@__saveRaw);
			this.Name = "ResourceDataView";
			this.Size = new System.Drawing.Size(760, 481);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel __viewer;
		private System.Windows.Forms.ComboBox __viewers;
		private System.Windows.Forms.Button __saveRaw;
	}
}
