namespace Anolis.Resourcer.TypeViewers {
	partial class MenuDialogViewer {
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
			this.@__properties = new System.Windows.Forms.PropertyGrid();
			this.@__itemsTree = new System.Windows.Forms.TreeView();
			this.@__tools = new System.Windows.Forms.ToolStrip();
			this.@__tOpen = new System.Windows.Forms.ToolStripButton();
			this.@__split = new System.Windows.Forms.SplitContainer();
			this.@__tools.SuspendLayout();
			this.@__split.Panel1.SuspendLayout();
			this.@__split.Panel2.SuspendLayout();
			this.@__split.SuspendLayout();
			this.SuspendLayout();
			// 
			// __properties
			// 
			this.@__properties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__properties.Location = new System.Drawing.Point(0, 0);
			this.@__properties.Name = "__properties";
			this.@__properties.Size = new System.Drawing.Size(283, 419);
			this.@__properties.TabIndex = 0;
			// 
			// __itemsTree
			// 
			this.@__itemsTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__itemsTree.Location = new System.Drawing.Point(0, 0);
			this.@__itemsTree.Name = "__itemsTree";
			this.@__itemsTree.Size = new System.Drawing.Size(357, 419);
			this.@__itemsTree.TabIndex = 2;
			// 
			// __tools
			// 
			this.@__tools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.@__tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__tOpen});
			this.@__tools.Location = new System.Drawing.Point(0, 0);
			this.@__tools.Name = "__tools";
			this.@__tools.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.@__tools.Size = new System.Drawing.Size(644, 25);
			this.@__tools.TabIndex = 4;
			this.@__tools.Text = "toolStrip1";
			// 
			// __tOpen
			// 
			this.@__tOpen.CheckOnClick = true;
			this.@__tOpen.Image = global::Anolis.Resourcer.Resources.Dialogs_Run;
			this.@__tOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tOpen.Name = "__tOpen";
			this.@__tOpen.Size = new System.Drawing.Size(85, 22);
			this.@__tOpen.Text = "Open Dialog";
			// 
			// __split
			// 
			this.@__split.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__split.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.@__split.Location = new System.Drawing.Point(0, 25);
			this.@__split.Name = "__split";
			// 
			// __split.Panel1
			// 
			this.@__split.Panel1.Controls.Add(this.@__itemsTree);
			// 
			// __split.Panel2
			// 
			this.@__split.Panel2.Controls.Add(this.@__properties);
			this.@__split.Size = new System.Drawing.Size(644, 419);
			this.@__split.SplitterDistance = 357;
			this.@__split.TabIndex = 5;
			// 
			// MenuDialogViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__split);
			this.Controls.Add(this.@__tools);
			this.Name = "MenuDialogViewer";
			this.Size = new System.Drawing.Size(644, 444);
			this.@__tools.ResumeLayout(false);
			this.@__tools.PerformLayout();
			this.@__split.Panel1.ResumeLayout(false);
			this.@__split.Panel2.ResumeLayout(false);
			this.@__split.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PropertyGrid __properties;
		private System.Windows.Forms.TreeView __itemsTree;
		private System.Windows.Forms.ToolStrip __tools;
		private System.Windows.Forms.ToolStripButton __tOpen;
		private System.Windows.Forms.SplitContainer __split;

	}
}
