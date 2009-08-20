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
			this.@__notes = new System.Windows.Forms.Label();
			this.@__cultureLbl = new System.Windows.Forms.Label();
			this.@__cultureAttrib = new System.Windows.Forms.LinkLabel();
			this.@__culture = new Anolis.Installer.Pages.LanguageComboBox();
			this.@__openingText.SuspendLayout();
			this.SuspendLayout();
			// 
			// __title
			// 
			this.@__title.BackColor = System.Drawing.Color.Transparent;
			this.@__title.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
			this.@__title.Text = "Welcome to the Anolis Package Installer";
			// 
			// __openingText
			// 
			this.@__openingText.BackColor = System.Drawing.Color.Transparent;
			this.@__openingText.Controls.Add(this.@__culture);
			this.@__openingText.Controls.Add(this.@__cultureAttrib);
			this.@__openingText.Controls.Add(this.@__notes);
			this.@__openingText.Controls.Add(this.@__cultureLbl);
			this.@__openingText.Location = new System.Drawing.Point(174, 89);
			this.@__openingText.Size = new System.Drawing.Size(322, 224);
			// 
			// __notes
			// 
			this.@__notes.BackColor = System.Drawing.Color.Transparent;
			this.@__notes.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.@__notes.Location = new System.Drawing.Point(0, 137);
			this.@__notes.Margin = new System.Windows.Forms.Padding(3, 100, 3, 0);
			this.@__notes.Name = "__notes";
			this.@__notes.Size = new System.Drawing.Size(322, 87);
			this.@__notes.TabIndex = 0;
			this.@__notes.Text = "This wizard will guide you through the steps involve in installing an Anolis pack" +
				"age\r\n\r\nTo continue, press Next";
			// 
			// __cultureLbl
			// 
			this.@__cultureLbl.AutoSize = true;
			this.@__cultureLbl.BackColor = System.Drawing.Color.Transparent;
			this.@__cultureLbl.Location = new System.Drawing.Point(69, 56);
			this.@__cultureLbl.Name = "__cultureLbl";
			this.@__cultureLbl.Size = new System.Drawing.Size(108, 13);
			this.@__cultureLbl.TabIndex = 3;
			this.@__cultureLbl.Text = "Installation Language";
			// 
			// __cultureAttrib
			// 
			this.@__cultureAttrib.AutoSize = true;
			this.@__cultureAttrib.Location = new System.Drawing.Point(69, 97);
			this.@__cultureAttrib.Name = "__cultureAttrib";
			this.@__cultureAttrib.Size = new System.Drawing.Size(0, 13);
			this.@__cultureAttrib.TabIndex = 5;
			// 
			// __culture
			// 
			this.@__culture.FormattingEnabled = true;
			this.@__culture.Location = new System.Drawing.Point(72, 72);
			this.@__culture.Name = "__culture";
			this.@__culture.Size = new System.Drawing.Size(189, 21);
			this.@__culture.TabIndex = 6;
			// 
			// WelcomePage
			// 
			this.Name = "WelcomePage";
			this.@__openingText.ResumeLayout(false);
			this.@__openingText.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label __notes;
		private System.Windows.Forms.Label __cultureLbl;
		private System.Windows.Forms.LinkLabel __cultureAttrib;
		private LanguageComboBox __culture;

	}
}
