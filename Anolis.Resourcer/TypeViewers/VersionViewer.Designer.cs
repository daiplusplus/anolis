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
			this.@__split = new System.Windows.Forms.SplitContainer();
			this.@__split.Panel1.SuspendLayout();
			this.@__split.Panel2.SuspendLayout();
			this.@__split.SuspendLayout();
			this.SuspendLayout();
			// 
			// __versionItems
			// 
			this.@__versionItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__versionItems.Location = new System.Drawing.Point(0, 0);
			this.@__versionItems.Name = "__versionItems";
			this.@__versionItems.Size = new System.Drawing.Size(631, 144);
			this.@__versionItems.TabIndex = 1;
			// 
			// __value
			// 
			this.@__value.BackColor = System.Drawing.SystemColors.Window;
			this.@__value.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__value.Location = new System.Drawing.Point(0, 0);
			this.@__value.Multiline = true;
			this.@__value.Name = "__value";
			this.@__value.ReadOnly = true;
			this.@__value.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.@__value.Size = new System.Drawing.Size(631, 263);
			this.@__value.TabIndex = 2;
			// 
			// __split
			// 
			this.@__split.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__split.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.@__split.Location = new System.Drawing.Point(0, 0);
			this.@__split.Name = "__split";
			this.@__split.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// __split.Panel1
			// 
			this.@__split.Panel1.Controls.Add(this.@__versionItems);
			// 
			// __split.Panel2
			// 
			this.@__split.Panel2.Controls.Add(this.@__value);
			this.@__split.Size = new System.Drawing.Size(631, 411);
			this.@__split.SplitterDistance = 144;
			this.@__split.TabIndex = 3;
			// 
			// VersionViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__split);
			this.Name = "VersionViewer";
			this.Size = new System.Drawing.Size(631, 411);
			this.@__split.Panel1.ResumeLayout(false);
			this.@__split.Panel2.ResumeLayout(false);
			this.@__split.Panel2.PerformLayout();
			this.@__split.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView __versionItems;
		private System.Windows.Forms.TextBox __value;
		private System.Windows.Forms.SplitContainer __split;

	}
}
