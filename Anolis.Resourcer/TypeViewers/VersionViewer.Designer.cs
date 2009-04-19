namespace Anolis.Resourcer.TypeViewers {
	partial class VersionViewer {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.@__versionItems = new System.Windows.Forms.TreeView();
			this.@__value = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// __versionItems
			// 
			this.@__versionItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__versionItems.Location = new System.Drawing.Point(0, 0);
			this.@__versionItems.Name = "__versionItems";
			this.@__versionItems.Size = new System.Drawing.Size(631, 271);
			this.@__versionItems.TabIndex = 1;
			// 
			// __value
			// 
			this.@__value.BackColor = System.Drawing.SystemColors.Window;
			this.@__value.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.@__value.Location = new System.Drawing.Point(0, 271);
			this.@__value.Multiline = true;
			this.@__value.Name = "__value";
			this.@__value.ReadOnly = true;
			this.@__value.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.@__value.Size = new System.Drawing.Size(631, 140);
			this.@__value.TabIndex = 2;
			// 
			// VersionViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__versionItems);
			this.Controls.Add(this.@__value);
			this.Name = "VersionViewer";
			this.Size = new System.Drawing.Size(631, 411);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView __versionItems;
		private System.Windows.Forms.TextBox __value;

	}
}
