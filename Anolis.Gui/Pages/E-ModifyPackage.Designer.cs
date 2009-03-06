namespace Anolis.Gui.Pages {
	partial class ModifyPackagePage {
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
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__packageView = new System.Windows.Forms.TreeView();
			this.@__banner.SuspendLayout();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			this.@__banner.Controls.SetChildIndex(this.@__bannerTitle, 0);
			this.@__banner.Controls.SetChildIndex(this.@__bannerSubtitle, 0);
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__packageView);
			// 
			// __bannerSubtitle
			// 
			this.@__bannerSubtitle.Text = "You can choose whether or not to install components of this package";
			// 
			// __bannerTitle
			// 
			this.@__bannerTitle.Text = "Modify Package";
			// 
			// __ofd
			// 
			this.@__ofd.Filter = "Anolis Package (*.anop)|*.anop|All files (*.*)|*.*";
			this.@__ofd.Title = "Select Anolis Package File";
			// 
			// __packageView
			// 
			this.@__packageView.CheckBoxes = true;
			this.@__packageView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__packageView.Location = new System.Drawing.Point(0, 0);
			this.@__packageView.Name = "__packageView";
			this.@__packageView.Size = new System.Drawing.Size(456, 228);
			this.@__packageView.TabIndex = 0;
			// 
			// ModifyPackagePage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "ModifyPackagePage";
			this.@__banner.ResumeLayout(false);
			this.@__content.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.TreeView __packageView;
	}
}
