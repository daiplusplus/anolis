namespace Anolis.Installer.Pages {
	partial class BaseInteriorPage {
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
			this.@__banner.SuspendLayout();
			this.SuspendLayout();
			this.@__banner.Controls.SetChildIndex(this.@__bannerTitle, 0);
			this.@__banner.Controls.SetChildIndex(this.@__bannerSubtitle, 0);
			// 
			// __bannerSubtitle
			// 
			this.@__bannerSubtitle.Size = new System.Drawing.Size(350, 30);
			// 
			// __bannerTitle
			// 
			this.@__bannerTitle.Size = new System.Drawing.Size(372, 13);
			// 
			// BaseInteriorPage
			// 
			this.Name = "BaseInteriorPage";
			this.@__banner.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

	}
}
