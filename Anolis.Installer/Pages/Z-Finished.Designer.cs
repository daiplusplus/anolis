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
			this.label3 = new System.Windows.Forms.Label();
			this.@__installationComplete = new System.Windows.Forms.Label();
			this.@__openingText.SuspendLayout();
			this.SuspendLayout();
			// 
			// __title
			// 
			this.@__title.BackColor = System.Drawing.Color.Transparent;
			this.@__title.Text = "Installation Complete";
			// 
			// __openingText
			// 
			this.@__openingText.BackColor = System.Drawing.Color.Transparent;
			this.@__openingText.Controls.Add(this.@__installationComplete);
			this.@__openingText.Controls.Add(this.label3);
			this.@__openingText.Controls.Add(this.@__anolisWebsite);
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
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(32, 186);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Anolis Homepage";
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
			// FinishedPage
			// 
			
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackgroundImage = global::Anolis.Installer.Properties.Resources.Background;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Name = "FinishedPage";
			this.@__openingText.ResumeLayout(false);
			this.@__openingText.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel __anolisWebsite;
		private System.Windows.Forms.Label __installationComplete;

	}
}
