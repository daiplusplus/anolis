namespace Anolis.Installer.Pages {
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
			this.@__infoPicture = new System.Windows.Forms.PictureBox();
			this.@__infoLbl = new System.Windows.Forms.Label();
			this.@__split = new System.Windows.Forms.SplitContainer();
			this.@__content.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.@__infoPicture)).BeginInit();
			this.@__split.Panel1.SuspendLayout();
			this.@__split.Panel2.SuspendLayout();
			this.@__split.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__split);
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
			this.@__packageView.ShowRootLines = false;
			this.@__packageView.Size = new System.Drawing.Size(232, 232);
			this.@__packageView.TabIndex = 0;
			// 
			// __infoPicture
			// 
			this.@__infoPicture.Dock = System.Windows.Forms.DockStyle.Top;
			this.@__infoPicture.Location = new System.Drawing.Point(0, 0);
			this.@__infoPicture.Margin = new System.Windows.Forms.Padding(0);
			this.@__infoPicture.Name = "__infoPicture";
			this.@__infoPicture.Size = new System.Drawing.Size(211, 168);
			this.@__infoPicture.TabIndex = 1;
			this.@__infoPicture.TabStop = false;
			// 
			// __infoLbl
			// 
			this.@__infoLbl.BackColor = System.Drawing.Color.Transparent;
			this.@__infoLbl.Dock = System.Windows.Forms.DockStyle.Top;
			this.@__infoLbl.Location = new System.Drawing.Point(0, 168);
			this.@__infoLbl.Name = "__infoLbl";
			this.@__infoLbl.Size = new System.Drawing.Size(211, 72);
			this.@__infoLbl.TabIndex = 3;
			this.@__infoLbl.Text = "The selected item \'{0}\' does not have any information associated with it";
			// 
			// __split
			// 
			this.@__split.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__split.Location = new System.Drawing.Point(0, 0);
			this.@__split.Name = "__split";
			// 
			// __split.Panel1
			// 
			this.@__split.Panel1.Controls.Add(this.@__packageView);
			this.@__split.Panel1MinSize = 100;
			// 
			// __split.Panel2
			// 
			this.@__split.Panel2.Controls.Add(this.@__infoLbl);
			this.@__split.Panel2.Controls.Add(this.@__infoPicture);
			this.@__split.Size = new System.Drawing.Size(447, 232);
			this.@__split.SplitterDistance = 232;
			this.@__split.TabIndex = 4;
			// 
			// ModifyPackagePage
			// 
			this.Name = "ModifyPackagePage";
			this.PageSubtitle = "You can choose whether or not to install components of this package";
			this.PageTitle = "Modify Package";
			this.@__content.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.@__infoPicture)).EndInit();
			this.@__split.Panel1.ResumeLayout(false);
			this.@__split.Panel2.ResumeLayout(false);
			this.@__split.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.TreeView __packageView;
		private System.Windows.Forms.PictureBox __infoPicture;
		private System.Windows.Forms.Label __infoLbl;
		private System.Windows.Forms.SplitContainer __split;
	}
}
