namespace Anolis.Installer.Pages {
	partial class FinishedPage {
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
			this.@__anolisWebsite = new System.Windows.Forms.LinkLabel();
			this.@__anolisWebsiteLbl = new System.Windows.Forms.Label();
			this.@__installationComplete = new System.Windows.Forms.Label();
			this.@__authorWebsiteLbl = new System.Windows.Forms.Label();
			this.@__authorWebsite = new System.Windows.Forms.LinkLabel();
			this.@__openingText.SuspendLayout();
			this.SuspendLayout();
			// 
			// __title
			// 
			this.@__title.BackColor = System.Drawing.Color.Transparent;
			this.@__title.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
			this.@__title.Text = "Installation Complete";
			// 
			// __openingText
			// 
			this.@__openingText.BackColor = System.Drawing.Color.Transparent;
			this.@__openingText.Controls.Add(this.@__authorWebsiteLbl);
			this.@__openingText.Controls.Add(this.@__authorWebsite);
			this.@__openingText.Controls.Add(this.@__anolisWebsiteLbl);
			this.@__openingText.Controls.Add(this.@__anolisWebsite);
			this.@__openingText.Controls.Add(this.@__installationComplete);
			// 
			// __anolisWebsite
			// 
			this.@__anolisWebsite.AutoSize = true;
			this.@__anolisWebsite.Location = new System.Drawing.Point(128, 186);
			this.@__anolisWebsite.Name = "__anolisWebsite";
			this.@__anolisWebsite.Size = new System.Drawing.Size(68, 13);
			this.@__anolisWebsite.TabIndex = 2;
			this.@__anolisWebsite.TabStop = true;
			this.@__anolisWebsite.Text = "http://anol.is";
			// 
			// __anolisWebsiteLbl
			// 
			this.@__anolisWebsiteLbl.Location = new System.Drawing.Point(16, 186);
			this.@__anolisWebsiteLbl.Name = "__anolisWebsiteLbl";
			this.@__anolisWebsiteLbl.Size = new System.Drawing.Size(106, 18);
			this.@__anolisWebsiteLbl.TabIndex = 3;
			this.@__anolisWebsiteLbl.Text = "Anolis Homepage";
			this.@__anolisWebsiteLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __installationComplete
			// 
			this.@__installationComplete.Location = new System.Drawing.Point(32, 92);
			this.@__installationComplete.Name = "__installationComplete";
			this.@__installationComplete.Size = new System.Drawing.Size(287, 94);
			this.@__installationComplete.TabIndex = 4;
			this.@__installationComplete.Text = "Installation has been completed succesfully. Your computer will now restart.\r\n\r\nP" +
				"lease close all open applications before continuing. Do not attempt to open any " +
				"new applications.";
			// 
			// __authorWebsiteLbl
			// 
			this.@__authorWebsiteLbl.Location = new System.Drawing.Point(0, 210);
			this.@__authorWebsiteLbl.Name = "__authorWebsiteLbl";
			this.@__authorWebsiteLbl.Size = new System.Drawing.Size(122, 46);
			this.@__authorWebsiteLbl.TabIndex = 6;
			this.@__authorWebsiteLbl.Text = "{packageAuthor}";
			this.@__authorWebsiteLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.@__authorWebsiteLbl.Visible = false;
			// 
			// __authorWebsite
			// 
			this.@__authorWebsite.AutoSize = true;
			this.@__authorWebsite.Location = new System.Drawing.Point(128, 210);
			this.@__authorWebsite.Name = "__authorWebsite";
			this.@__authorWebsite.Size = new System.Drawing.Size(68, 13);
			this.@__authorWebsite.TabIndex = 5;
			this.@__authorWebsite.TabStop = true;
			this.@__authorWebsite.Text = "http://anol.is";
			this.@__authorWebsite.Visible = false;
			// 
			// FinishedPage
			// 
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Name = "FinishedPage";
			this.@__openingText.ResumeLayout(false);
			this.@__openingText.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label __anolisWebsiteLbl;
		private System.Windows.Forms.LinkLabel __anolisWebsite;
		private System.Windows.Forms.Label __installationComplete;
		private System.Windows.Forms.Label __authorWebsiteLbl;
		private System.Windows.Forms.LinkLabel __authorWebsite;

	}
}
