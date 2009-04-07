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
			this.@__layout.SuspendLayout();
			this.SuspendLayout();
			// 
			// __properties
			// 
			this.@__properties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__properties.Location = new System.Drawing.Point(325, 3);
			this.@__properties.Name = "__properties";
			this.@__properties.Size = new System.Drawing.Size(316, 441);
			this.@__properties.TabIndex = 0;
			// 
			// __itemsTree
			// 
			this.@__itemsTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__itemsTree.Location = new System.Drawing.Point(3, 3);
			this.@__itemsTree.Name = "__itemsTree";
			this.@__itemsTree.Size = new System.Drawing.Size(316, 441);
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
			this.@__layout.Location = new System.Drawing.Point(0, 0);
			this.@__layout.Name = "__layout";
			this.@__layout.RowCount = 1;
			this.@__layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.@__layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.@__layout.Size = new System.Drawing.Size(644, 447);
			this.@__layout.TabIndex = 3;
			// 
			// MenuViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__layout);
			this.Name = "MenuViewer";
			this.Size = new System.Drawing.Size(644, 447);
			this.@__layout.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PropertyGrid __properties;
		private System.Windows.Forms.TreeView __itemsTree;
		private System.Windows.Forms.TableLayoutPanel __layout;

	}
}
