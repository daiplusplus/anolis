namespace Anolis.Resourcer.TypeViewers {
	partial class HtmlViewer {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.@__browser = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// __browser
			// 
			this.@__browser.AllowNavigation = false;
			this.@__browser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__browser.Location = new System.Drawing.Point(0, 0);
			this.@__browser.MinimumSize = new System.Drawing.Size(20, 20);
			this.@__browser.Name = "__browser";
			this.@__browser.Size = new System.Drawing.Size(615, 465);
			this.@__browser.TabIndex = 0;
			// 
			// HtmlViewer
			// 
			this.Controls.Add(this.@__browser);
			this.Name = "HtmlViewer";
			this.Size = new System.Drawing.Size(615, 465);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser __browser;
	}
}
