namespace Anolis.Installer.Pages {
	partial class WelcomePage {
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
			this.@__inst1 = new System.Windows.Forms.Label();
			this.@__culture = new System.Windows.Forms.ComboBox();
			this.@__cultureLbl = new System.Windows.Forms.Label();
			this.@__openingText.SuspendLayout();
			this.SuspendLayout();
			// 
			// __title
			// 
			this.@__title.BackColor = System.Drawing.Color.Transparent;
			this.@__title.Text = "Welcome to the Anolis Package Installer";
			// 
			// __openingText
			// 
			this.@__openingText.BackColor = System.Drawing.Color.Transparent;
			this.@__openingText.Controls.Add(this.@__inst1);
			this.@__openingText.Location = new System.Drawing.Point(174, 177);
			this.@__openingText.Size = new System.Drawing.Size(322, 123);
			// 
			// __inst1
			// 
			this.@__inst1.BackColor = System.Drawing.Color.Transparent;
			this.@__inst1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__inst1.Location = new System.Drawing.Point(0, 0);
			this.@__inst1.Margin = new System.Windows.Forms.Padding(3, 100, 3, 0);
			this.@__inst1.Name = "__inst1";
			this.@__inst1.Size = new System.Drawing.Size(322, 123);
			this.@__inst1.TabIndex = 0;
			this.@__inst1.Text = "This wizard will guide you through the steps involve in installing an Anolis pack" +
				"age\r\n\r\nDistributions of Anolis can customise the text placed here and throughout" +
				" the wizard\r\n\r\nTo continue, press Next";
			// 
			// __culture
			// 
			this.@__culture.FormattingEnabled = true;
			this.@__culture.Location = new System.Drawing.Point(177, 99);
			this.@__culture.Name = "__culture";
			this.@__culture.Size = new System.Drawing.Size(189, 21);
			this.@__culture.TabIndex = 2;
			// 
			// __cultureLbl
			// 
			this.@__cultureLbl.AutoSize = true;
			this.@__cultureLbl.Location = new System.Drawing.Point(174, 83);
			this.@__cultureLbl.Name = "__cultureLbl";
			this.@__cultureLbl.Size = new System.Drawing.Size(108, 13);
			this.@__cultureLbl.TabIndex = 3;
			this.@__cultureLbl.Text = "Installation Language";
			// 
			// WelcomePage
			// 
			this.Controls.Add(this.@__cultureLbl);
			this.Controls.Add(this.@__culture);
			this.Name = "WelcomePage";
			this.Controls.SetChildIndex(this.@__culture, 0);
			this.Controls.SetChildIndex(this.@__cultureLbl, 0);
			this.Controls.SetChildIndex(this.@__title, 0);
			this.Controls.SetChildIndex(this.@__openingText, 0);
			this.@__openingText.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label __inst1;
		private System.Windows.Forms.ComboBox __culture;
		private System.Windows.Forms.Label __cultureLbl;

	}
}
