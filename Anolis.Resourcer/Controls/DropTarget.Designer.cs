namespace Anolis.Resourcer.Controls {
	partial class DropTarget {
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
			this.@__sourceOpen = new System.Windows.Forms.CheckBox();
			this.@__dataAdd = new System.Windows.Forms.CheckBox();
			this.@__dataReplace = new System.Windows.Forms.CheckBox();
			this.@__layout = new System.Windows.Forms.TableLayoutPanel();
			this.@__layout.SuspendLayout();
			this.SuspendLayout();
			// 
			// __sourceOpen
			// 
			this.@__sourceOpen.AllowDrop = true;
			this.@__sourceOpen.Appearance = System.Windows.Forms.Appearance.Button;
			this.@__sourceOpen.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__sourceOpen.Image = global::Anolis.Resourcer.Resources.Toolbar_SrcOpen;
			this.@__sourceOpen.Location = new System.Drawing.Point(3, 3);
			this.@__sourceOpen.Name = "__sourceOpen";
			this.@__sourceOpen.Size = new System.Drawing.Size(123, 83);
			this.@__sourceOpen.TabIndex = 0;
			this.@__sourceOpen.Text = "Open as Source";
			this.@__sourceOpen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.@__sourceOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__sourceOpen.UseVisualStyleBackColor = true;
			// 
			// __dataAdd
			// 
			this.@__dataAdd.AllowDrop = true;
			this.@__dataAdd.Appearance = System.Windows.Forms.Appearance.Button;
			this.@__dataAdd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__dataAdd.Image = global::Anolis.Resourcer.Resources.Toolbar_ResAdd;
			this.@__dataAdd.Location = new System.Drawing.Point(132, 3);
			this.@__dataAdd.Name = "__dataAdd";
			this.@__dataAdd.Size = new System.Drawing.Size(123, 83);
			this.@__dataAdd.TabIndex = 1;
			this.@__dataAdd.Text = "Add as Data";
			this.@__dataAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.@__dataAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__dataAdd.UseVisualStyleBackColor = true;
			// 
			// __dataReplace
			// 
			this.@__dataReplace.AllowDrop = true;
			this.@__dataReplace.Appearance = System.Windows.Forms.Appearance.Button;
			this.@__dataReplace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__dataReplace.Image = global::Anolis.Resourcer.Resources.Toolbar_ResRep;
			this.@__dataReplace.Location = new System.Drawing.Point(261, 3);
			this.@__dataReplace.Name = "__dataReplace";
			this.@__dataReplace.Size = new System.Drawing.Size(123, 83);
			this.@__dataReplace.TabIndex = 2;
			this.@__dataReplace.Text = "Replace Current Data";
			this.@__dataReplace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.@__dataReplace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__dataReplace.UseVisualStyleBackColor = true;
			// 
			// __layout
			// 
			this.@__layout.ColumnCount = 3;
			this.@__layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.@__layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.@__layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.@__layout.Controls.Add(this.@__sourceOpen, 0, 0);
			this.@__layout.Controls.Add(this.@__dataReplace, 2, 0);
			this.@__layout.Controls.Add(this.@__dataAdd, 1, 0);
			this.@__layout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__layout.Location = new System.Drawing.Point(0, 0);
			this.@__layout.Name = "__layout";
			this.@__layout.RowCount = 1;
			this.@__layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.@__layout.Size = new System.Drawing.Size(387, 89);
			this.@__layout.TabIndex = 3;
			// 
			// DropTarget
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.@__layout);
			this.Name = "DropTarget";
			this.Size = new System.Drawing.Size(387, 89);
			this.@__layout.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckBox __sourceOpen;
		private System.Windows.Forms.CheckBox __dataAdd;
		private System.Windows.Forms.CheckBox __dataReplace;
		private System.Windows.Forms.TableLayoutPanel __layout;
	}
}
