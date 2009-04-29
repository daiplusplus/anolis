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
			this.@__layout = new System.Windows.Forms.TableLayoutPanel();
			this.@__tools = new System.Windows.Forms.ToolStrip();
			this.@__tOpen = new System.Windows.Forms.ToolStripButton();
			this.@__layout.SuspendLayout();
			this.@__tools.SuspendLayout();
			this.SuspendLayout();
			// 
			// __properties
			// 
			this.@__properties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__properties.Location = new System.Drawing.Point(325, 3);
			this.@__properties.Name = "__properties";
			this.@__properties.Size = new System.Drawing.Size(316, 416);
			this.@__properties.TabIndex = 0;
			// 
			// __itemsTree
			// 
			this.@__itemsTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__itemsTree.Location = new System.Drawing.Point(3, 3);
			this.@__itemsTree.Name = "__itemsTree";
			this.@__itemsTree.Size = new System.Drawing.Size(316, 416);
			this.@__itemsTree.TabIndex = 2;
			// 
			// __layout
			// 
			this.@__layout.ColumnCount = 2;
			this.@__layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.@__layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.@__layout.Controls.Add(this.@__properties, 1, 0);
			this.@__layout.Controls.Add(this.@__itemsTree, 0, 0);
			this.@__layout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__layout.Location = new System.Drawing.Point(0, 25);
			this.@__layout.Name = "__layout";
			this.@__layout.RowCount = 1;
			this.@__layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.@__layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 422F));
			this.@__layout.Size = new System.Drawing.Size(644, 422);
			this.@__layout.TabIndex = 3;
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
			// MenuDialogViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__layout);
			this.Controls.Add(this.@__tools);
			this.Name = "MenuDialogViewer";
			this.Size = new System.Drawing.Size(644, 447);
			this.@__layout.ResumeLayout(false);
			this.@__tools.ResumeLayout(false);
			this.@__tools.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PropertyGrid __properties;
		private System.Windows.Forms.TreeView __itemsTree;
		private System.Windows.Forms.TableLayoutPanel __layout;
		private System.Windows.Forms.ToolStrip __tools;
		private System.Windows.Forms.ToolStripButton __tOpen;

	}
}
