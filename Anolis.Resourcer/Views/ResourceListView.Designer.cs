namespace Anolis.Resourcer.Views {
	partial class ResourceListView {
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
			this.@__list = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// __list
			// 
			this.@__list.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__list.Location = new System.Drawing.Point(0, 0);
			this.@__list.Name = "__list";
			this.@__list.Size = new System.Drawing.Size(522, 342);
			this.@__list.TabIndex = 0;
			this.@__list.UseCompatibleStateImageBehavior = false;
			// 
			// ResourceListView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__list);
			this.Name = "ResourceListView";
			this.Size = new System.Drawing.Size(522, 342);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView __list;
	}
}
